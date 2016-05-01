using System;
using System.Runtime.InteropServices;

namespace PEAnalyzer
{
    class PEStructs
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct DOS_Header
        {
            public UInt16 signature; // = 0x5A4D "MZ";
            public UInt16 lastsize;
            public UInt16 PagedInFile;
            public UInt16 relocations;
            public UInt16 headerSizeInParagraph;
            public UInt16 MinExtraParagraphNeeded;
            public UInt16 MaxExtraParagraphNeeded;
            public UInt16 InitialSS;
            public UInt16 InitialSP;
            public UInt16 checksum;
            public UInt16 InitialIP;
            public UInt16 InitialCS;
            public UInt16 FileAddOfRelocTable;
            public UInt16 OverlayNumber;
            public UInt16 reserved0;
            public UInt16 reserved1;
            public UInt16 reserved2;
            public UInt16 reserved3;
            public UInt16 OEMIdentifier;
            public UInt16 OEMInformation;
            public UInt16 reserved4;
            public UInt16 reserved5;
            public UInt16 reserved6;
            public UInt16 reserved7;
            public UInt16 reserved8;
            public UInt16 reserved9;
            public UInt16 reservedA;
            public UInt16 reservedB;
            public UInt16 reservedC;
            public UInt16 reservedD;
            public UInt32 e_lfanew;  // point to pe_header
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct COFF_Header
        {
            public UInt16 TargetMachine;
            public UInt16 NumberOfSections;
            public UInt32 TimeDateStamp;
            public UInt32 PointerToSymbolTable;
            public UInt32 NumberOfSymbols;
            public UInt16 SizeOfOptionalHeaders;
            public UInt16 Characteristics;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Opt_Header
        {
            public UInt16 magic;
            public Byte lnMajVer;
            public Byte lnMnrVer;
            public UInt32 SizeOfCode;
            public UInt32 SizeOfInitializedData;
            public UInt32 SizeOfUninitializedData;
            public UInt32 AddressOfEntryPoint;
            public UInt32 BaseOfCode;
            public UInt32 BaseOfData;
            public UInt32 ImageBase;
            public UInt32 SectionAlignment;
            public UInt32 FileAlignment;
            public UInt16 MajorOSVersion;
            public UInt16 MinorOSVersion;
            public UInt16 MajorImageVersion;
            public UInt16 MinorImageVersion;
            public UInt16 MajorSubSystemVersion;
            public UInt16 MinorSubsystemVersion;
            public UInt32 Win32VersionValue;
            public UInt32 SizeOfImage;
            public UInt32 SizeOfHeaders;
            public UInt32 CheckSum;
            public UInt16 SubSystem;
            public UInt16 DLLCharacteristics;
            public UInt32 SizeOfStackReserve;
            public UInt32 SizeOfStackCommit;
            public UInt32 SizeOfHeapReserve;
            public UInt32 SizeOfHeapCommit;
            public UInt32 LoaderFlags;
            public UInt32 NumberOfRVAandSizes;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Opt_Header64
        {
            public UInt16 magic;
            public Byte lnMajVer;
            public Byte lnMnrVer;
            public UInt32 SizeOfCode;
            public UInt32 SizeOfInitializedData;
            public UInt32 SizeOfUninitializedData;
            public UInt32 AddressOfEntryPoint;
            public UInt32 BaseOfCode;
            public UInt64 ImageBase;
            public UInt32 SectionAlignment;
            public UInt32 FileAlignment;
            public UInt16 MajorOSVersion;
            public UInt16 MinorOSVersion;
            public UInt16 MajorImageVersion;
            public UInt16 MinorImageVersion;
            public UInt16 MajorSubSystemVersion;
            public UInt16 MinorSubsystemVersion;
            public UInt32 Win32VersionValue;
            public UInt32 SizeOfImage;
            public UInt32 SizeOfHeaders;
            public UInt32 CheckSum;
            public UInt16 SubSystem;
            public UInt16 DLLCharacteristics;
            public UInt64 SizeOfStackReserve;
            public UInt64 SizeOfStackCommit;
            public UInt64 SizeOfHeapReserve;
            public UInt64 SizeOfHeapCommit;
            public UInt32 LoaderFlags;
            public UInt32 NumberOfRVAandSizes;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Data_Directory
        {
            public UInt32 RVA;
            public UInt32 Size;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Section_Data_Header
        {
            public UInt64 name;
            public UInt32 VirtualSize;
            public UInt32 VirtualAddress;
            public UInt32 SizeOfRawData;
            public UInt32 PointerToRawData;
            public UInt32 PointerToRelocations;
            public UInt32 PointerToLineNumbers;
            public UInt16 NumberOfRelocations;
            public UInt16 NumberOfLineNumbers;
            public UInt32 Characteristics;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Image_Import_Descriptor
        {
            public UInt32 OriginalFirstThunk;
            public UInt32 TimeDateStamp;
            public UInt32 ForwarderChain;
            public UInt32 Name;
            public UInt32 FirstThunk;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Image_Export_Directory
        {
            public UInt32 Characteristics;
            public UInt32 TimeDateStamp;
            public UInt16 MajorVersion;
            public UInt16 MinorVersion;
            public UInt32 Name;
            public UInt32 Base;
            public UInt32 NumberOfFunctions;
            public UInt32 NumberOfNames;
            public UInt32 AddressOfFunctions;
            public UInt32 AddressOfNames;
            public UInt32 AddressOfNameOrdinals;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Image_Resource_Directory
        {
            public UInt32 Characteristics;
            public UInt32 TimeDateStamp;
            public UInt16 MajorVersion;
            public UInt16 MinorVersion;
            public UInt16 NumberOfNamedEntries;
            public UInt16 NumberOfIdEntries;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Image_Resource_Directory_Entry
        {
            public UInt32 Name;
            public UInt32 OffsetToData;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Image_Resource_Data_Entry
        {
            public UInt32 OffsetToData;
            public UInt32 Size;
            public UInt32 CodePage;
            public UInt32 Reserved;
        }
    }
}
