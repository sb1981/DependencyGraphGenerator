using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace PEAnalyzer
{
    public class FileAnalyzer : DependencyGraphGeneratorFramework.IDGFileAnalyzer
    {
        public string GetName()
        {
            return "PE File Analyzer";
        }
        public int AnalyzeFile(ref DependencyGraphGeneratorFramework.FileInfo fi)
        {
            try
            {
                using (BinaryReader b = new BinaryReader(File.Open(fi.FileNameFullPath, FileMode.Open, FileAccess.Read, FileShare.Read)))
                {
                    int length = (int)b.BaseStream.Length;
                    ulong pos = 0;
                    List<PEStructs.Section_Data_Header> Sections = new List<PEStructs.Section_Data_Header>();
                    PEStructs.Data_Directory[] DataDirectories = null;

                    // read the binary data
                    byte[] data = b.ReadBytes(length);

                    // get PE header
                    PEStructs.DOS_Header pe = (PEStructs.DOS_Header)rawDeserialize(data, ref pos, typeof(PEStructs.DOS_Header));

                    // check for PE Signature
                    UInt32 signature = data[pe.e_lfanew];
                    signature += Convert.ToUInt32(data[pe.e_lfanew + 1]) << 8;
                    signature += Convert.ToUInt32(data[pe.e_lfanew + 2]) << 16;
                    signature += Convert.ToUInt32(data[pe.e_lfanew + 3]) << 24;

                    if (signature == 0x00004550)
                    {
                        // get COFF header
                        pos = Convert.ToUInt64(pe.e_lfanew) + 4;
                        PEStructs.COFF_Header coff = (PEStructs.COFF_Header)rawDeserialize(data, ref pos, typeof(PEStructs.COFF_Header));

                        int sizeOptHeader;
                        uint numRVA;

                        if (coff.TargetMachine == 0x014c) // IMAGE_FILE_MACHINE_I386
                        {
                            fi.Properties.Add("Platform", "x86");
                            sizeOptHeader = Marshal.SizeOf(typeof(PEStructs.Opt_Header));

                            // get Opt. Header
                            PEStructs.Opt_Header opt = (PEStructs.Opt_Header)rawDeserialize(data, ref pos, typeof(PEStructs.Opt_Header));
                            numRVA = opt.NumberOfRVAandSizes;
                            AddDLLCharacteristics(opt.DLLCharacteristics, fi.Properties);
                        }
                        else if (coff.TargetMachine == 0x8664) // IMAGE_FILE_MACHINE_AMD64
                        {
                            fi.Properties.Add("Platform", "AMD64");
                            sizeOptHeader = Marshal.SizeOf(typeof(PEStructs.Opt_Header64));

                            // get Opt. Header
                            PEStructs.Opt_Header64 opt = (PEStructs.Opt_Header64)rawDeserialize(data, ref pos, typeof(PEStructs.Opt_Header64));
                            numRVA = opt.NumberOfRVAandSizes;
                            AddDLLCharacteristics(opt.DLLCharacteristics, fi.Properties);
                        }
                        else // unsupported -> leave function
                        {
                            fi.Properties.Add(this.GetName(), "Unsupported Machine");
                            return 1;
                        }

                        DataDirectories = new PEStructs.Data_Directory[numRVA];
                        for (int i = 0; i < numRVA; i++)
                        {
                            DataDirectories[i] = (PEStructs.Data_Directory)rawDeserialize(data, ref pos, typeof(PEStructs.Data_Directory));
                        }

                        // get sections
                        for (int i = 0; i < coff.NumberOfSections; i++)
                        {
                            Sections.Add((PEStructs.Section_Data_Header)rawDeserialize(data, ref pos, typeof(PEStructs.Section_Data_Header)));
                        }

                        // there were no data-directories
                        if (DataDirectories == null || DataDirectories.Length == 0)
                        {
                            fi.Properties.Add(this.GetName(), "No DataDirectories");
                            return 2;
                        }

                        foreach (PEStructs.Section_Data_Header s in Sections)
                        {
                            // get import data
                            if ((DataDirectories[1].RVA >= s.VirtualAddress) && (DataDirectories[1].RVA < (s.VirtualAddress + s.VirtualSize)))
                            {
                                pos = getFileoffsetFromRVA(DataDirectories[1].RVA, s);
                                while (true)
                                {
                                    PEStructs.Image_Import_Descriptor iid = (PEStructs.Image_Import_Descriptor)rawDeserialize(data, ref pos, typeof(PEStructs.Image_Import_Descriptor));
                                    if (iid.Name == 0) // if the name-pointer is zero, we reached the end of the list
                                    {
                                        break;
                                    }
                                    DependencyGraphGeneratorFramework.DGFileImport di = new DependencyGraphGeneratorFramework.DGFileImport(getStringFromRVA(data, iid.Name, s));

                                    ulong p = getFileoffsetFromRVA(iid.OriginalFirstThunk, s);
                                    UInt32 offset = 0;
                                    do
                                    {
                                        offset = (UInt32)rawDeserialize(data, ref p, typeof(UInt32));

                                        if (offset >= 0x80000000) // is it in ordinal?
                                        {
                                            di.Imports.Add("Ordinal: " + (offset - 0x80000000));
                                        }
                                        else
                                        {
                                            if (offset != 0)
                                            {
                                                di.Imports.Add(getStringFromRVA(data, offset + 2, s));
                                            }
                                        }

                                    } while (offset != 0);

                                    fi.Imports.Add(di);
                                }
                            }

                            // get export data
                            if ((DataDirectories[0].RVA >= s.VirtualAddress) && (DataDirectories[0].RVA < (s.VirtualAddress + s.VirtualSize)))
                            {
                                pos = getFileoffsetFromRVA(DataDirectories[0].RVA, s);

                                PEStructs.Image_Export_Directory ied = (PEStructs.Image_Export_Directory)rawDeserialize(data, ref pos, typeof(PEStructs.Image_Export_Directory));

                                pos = getFileoffsetFromRVA(ied.AddressOfNames, s);
                                for (int i = 0; i < ied.NumberOfNames; i++)
                                {
                                    ulong offset = (UInt32)rawDeserialize(data, ref pos, typeof(UInt32));
                                    fi.Exports.Add(getStringFromRVA(data, offset, s));
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // An error occured, so we just return an 'empty' fileinfo with a error-comment as property
                fi.Properties.Add(this.GetName(), "Exception thrown during analyzing the file");
                return 3;
            }
            return 0;
        }

        private ulong getFileoffsetFromRVA(ulong RVA, PEStructs.Section_Data_Header section)
        {
            return RVA - section.VirtualAddress + section.PointerToRawData;
        }

        private string getStringFromRVA(byte[] array, ulong RVA, PEStructs.Section_Data_Header section)
        {
            StringBuilder sb = new StringBuilder();
            for (ulong i = getFileoffsetFromRVA(RVA, section); array[i] != 0; i++)
            {
                sb.Append((char)array[i]);
            }
            return sb.ToString();
        }

        private static object rawDeserialize(byte[] rawData, ref ulong position, Type anyType)
        {
            object retVal = null;

            if (rawData != null && rawData.Length > 0)
            {
                int rawsize = Marshal.SizeOf(anyType);
                if (rawsize > rawData.LongLength)
                    return null;
                IntPtr buffer = Marshal.AllocHGlobal(rawsize);
                Marshal.Copy(rawData, (int)position, buffer, rawsize);
                retVal = Marshal.PtrToStructure(buffer, anyType);
                Marshal.FreeHGlobal(buffer);
                position += (ulong)rawsize;
            }

            return retVal;
        }
        private void AddDLLCharacteristics(ushort characteristics, Dictionary<string, string> properties)
        {
            properties.Add("ASLR", ((characteristics & 0x0040) > 0)?"Yes":"No");
            properties.Add("DEP", ((characteristics & 0x0100) > 0) ? "Yes" : "No");
            properties.Add("NO_SEH", ((characteristics & 0x0400) > 0) ? "Yes" : "No");
        }
    }
}
