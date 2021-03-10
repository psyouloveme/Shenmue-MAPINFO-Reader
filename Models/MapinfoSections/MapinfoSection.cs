using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace mapinforeader.Models.MapinfoSections {

    ///<summary>Models collision data from a <c>MAPINFO.BIN</c> files's <c>COLI</c> structure.
    ///This is a generic <c>COLI</c>, prefer using a specific type class.</summary>
    ///<seealso cref="ColiType1"/>
    ///<seealso cref="ColiType2"/>
    public class MapinfoSection {
        public static readonly int HeaderLength = 4;
        public static readonly int SectionSizeLength = 4;
        public string Header { get; set;}
        public long HeaderOffset { get; set; }
        public long SizeOffset { get; set; }   
        public uint Size { get; set; }
        public long ContentOffset { get; set; }

        public string ToMDTableRow() {
          StringBuilder s = new StringBuilder("|");
          s.Append(Header);
          s.Append("|");
          s.Append(Size);
          s.Append("|");
          s.Append(HeaderOffset);
          s.Append("|");
          s.Append(SizeOffset);
          s.Append("|");
          s.Append(ContentOffset);
          s.AppendLine("|");
          return s.ToString();
        }
    }
}