using System;
using System.IO;
using mapinforeader.Models.MapinfoSections;
using mapinforeader.Util;

namespace mapinforeader
{   
    class Program
    {
        private class ProgramFlags {
            
            public string MapInfoFile { get; set; }

            public bool DumpCols { get; set; }

            public string DumpColsFile { get; set; }

            public bool ShowHelp { get; set; }

            public static readonly string Help = "usage: mapinforeader [-h|--help] | [-d|--dumpcolis] <MAPINFO File>";  

            public ProgramFlags() : this(null) { }
            
            public ProgramFlags(string[] args) {
                if (args == null || args.Length == 0 || Array.IndexOf(args, "-h") >= 0 || Array.IndexOf(args, "--help") >= 0) {
                    DumpCols = false;
                    DumpColsFile = null;
                    MapInfoFile = null;
                    ShowHelp = true;
                } else {
                    if (Array.IndexOf(args, "-d") >= 0) {
                        var pos = Array.IndexOf(args, "-d");
                        if (args.Length == (pos+1)){
                            Console.WriteLine("Error: No output file was provided for -d option");
                            ShowHelp = true;
                            DumpCols = false;
                        } else {
                            DumpColsFile = args[pos+1];
                            DumpCols = true;
                        }
                    }
                    MapInfoFile = args[args.Length - 1];
                    if ((MapInfoFile != null || (DumpCols && DumpColsFile != null)) && (DumpColsFile == MapInfoFile)){
                        Console.WriteLine("Error: Must provide both an input MAPINFO file path and output path");
                        ShowHelp = true;
                    }
                }
            }
        }

        private const string FILE_NAME = "./COLIMBUI.PKS";

        static void Main(string[] args)
        {
            var parsedArgs = new ProgramFlags(args);

            if (parsedArgs.ShowHelp) {
                Console.WriteLine(ProgramFlags.Help);
                return;
            }
            if (!File.Exists(parsedArgs.MapInfoFile))
            {
                Console.WriteLine($"{parsedArgs.MapInfoFile} doesn't exist!");
                return;
            }
            Cols cols = null;
            using (FileStream fs = new FileStream(parsedArgs.MapInfoFile, FileMode.Open, FileAccess.Read)) {
                using (ColsReader reader = new ColsReader(fs)) {
                    cols = reader.ReadCols();
                }
            }
            if (parsedArgs.DumpCols) {
                Analysis.DumpColsBySectionIdx(cols, parsedArgs.DumpColsFile);
            }
        }
    }
}
