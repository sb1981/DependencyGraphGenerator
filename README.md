# Dependency Graph Generator
A tool to analyze the dependencies of different files, written in Visual Studio 2015.

Usage:
Should be more or less self-explaining. Simply open a file to start the (recursive) analyzation, afterwards you can export to different file formats.

To visualize the graph, Microsoft Automatic Graph Layout (MSAGL) is used. MSAGL is a .NET tool for graph layout and viewing. It was developed by Microsoft by Lev Nachmanson, Sergey Pupyrev, Tim Dwyer, Ted Hart, and Roman Prutkin. MSAGL is available as open source at https://github.com/Microsoft/automatic-graph-layout.git.

## Supported Fileformats

This software uses a simple plugin-system and currently supports the following fileformats:

Importers:
* PE - binaries

Exporters:
* CVS
* GraphML
* Graphviz

## Bugs
- It could be possible, that the plugin-dlls are not put in the right directories, so you have to do this manually.
- To create the Setup-Project, you may need to install 'Microsoft Visual Studio 2015 Installer Projects' https://visualstudiogallery.msdn.microsoft.com/f1cc3f3e-c300-40a7-8797-c509fb8933b9
