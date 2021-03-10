using System;
using System.IO;
using mapinforeader.Models;
using mapinforeader.Models.MapinfoSections;
using mapinforeader.Util;

namespace mapinforeader {
  class Program {
    static void Main(string[] args) {
      var parsedArgs = new ProgramFlags(args);

      if (parsedArgs.ShowHelp) {
        Console.WriteLine(ProgramFlags.Help);
        return;
      }
      if (!File.Exists(parsedArgs.MapInfoFile)) {
        Console.WriteLine($"{parsedArgs.MapInfoFile} doesn't exist!");
        return;
      }

      if (parsedArgs.SplitMapinfo) {
        Mapinfo mapinfo = null;
        using (FileStream fs = new FileStream(parsedArgs.MapInfoFile, FileMode.Open, FileAccess.Read)) {
          using (MapinfoReader reader = new MapinfoReader(fs)) {
            mapinfo = reader.ReadMapinfo();
            Analysis.DumpMapinfoToDirectory(mapinfo, reader, parsedArgs.SplitMapinfoPath);
          }
        }
      }

      if (parsedArgs.DumpCols) {
        Cols cols = null;
        using (FileStream fs = new FileStream(parsedArgs.DumpColsFile, FileMode.Open, FileAccess.Read)) {
          using (ColsReader reader = new ColsReader(fs)) {
            cols = reader.ReadCols();
          }
        }
        Analysis.DumpColsBySectionIdx(cols, parsedArgs.DumpColsFile);
      }
    }
  }
}
