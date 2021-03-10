using System;

namespace mapinforeader {
  public class ProgramFlags {
    public string MapInfoFile { get; set; }

    public bool DumpCols { get; set; }

    public string DumpColsFile { get; set; }
    
    public bool SplitMapinfo { get; set; }
    public bool IsColsInfo { get; set; }

    public string SplitMapinfoPath { get; set; }

    public bool ShowHelp { get; set; }

    public static readonly string Help = "usage: mapinforeader [-h|--help] | [-d|--dumpcolis] <MAPINFO File>";

    public ProgramFlags() : this(null) { }

    public ProgramFlags(string[] args) {
      if (args == null || args.Length == 0 || Array.IndexOf(args, "-h") >= 0 || Array.IndexOf(args, "--help") >= 0) {
        DumpCols = false;
        DumpColsFile = null;
        MapInfoFile = null;
        ShowHelp = true;
        IsColsInfo = false;
      } else {
        if (Array.IndexOf(args, "-d") >= 0) {
          var pos = Array.IndexOf(args, "-d");
          if (args.Length == (pos + 1)) {
            Console.WriteLine("Error: No output file was provided for -d option");
            ShowHelp = true;
            DumpCols = false;
          } else {
            DumpColsFile = args[pos + 1];
            DumpCols = true;
          }
        }
        if (Array.IndexOf(args, "-s") >= 0) {
          var pos = Array.IndexOf(args, "-s");
          if (args.Length == (pos + 1)) {
            Console.WriteLine("Error: No output directory was provided for -s option");
            ShowHelp = true;
            SplitMapinfo = false;
          } else {
            var colipos = Array.IndexOf(args, "--cols");
            if (colipos >= 0) {
              IsColsInfo = true;
            }
            SplitMapinfoPath = args[pos + 1];
            SplitMapinfo = true;
          }
        }
        MapInfoFile = args[args.Length - 1];
        if ((MapInfoFile != null || (DumpCols && DumpColsFile != null)) && (DumpColsFile == MapInfoFile)) {
          Console.WriteLine("Error: Must provide both an input MAPINFO file path and output path");
          ShowHelp = true;
        }
      }
    }
  }
}
