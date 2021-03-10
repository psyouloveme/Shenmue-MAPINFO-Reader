using System.Collections.Generic;
using System.Text;
using mapinforeader.Models.MapinfoSections;

namespace mapinforeader.Models {
  public class Mapinfo {
    public static readonly byte[] Terminator = new byte[] { 69, 78, 68, 00 };
    public List<MapinfoSection> Sections { get; set; }
    public Mapinfo() {
      Sections = new List<MapinfoSection>();
    }
    public string ToMDTable() {
      StringBuilder s = new StringBuilder("|");
      s.Append("Header");
      s.Append("|");
      s.Append("Size");
      s.Append("|");
      s.Append("HeaderOffset");
      s.Append("|");
      s.Append("SizeOffset");
      s.Append("|");
      s.Append("ContentOffset");
      s.AppendLine("|");
      s.AppendLine("|--|--|--|--|--|");
      foreach(var sec in Sections) {
        s.Append(sec.ToMDTableRow());
      }
      return s.ToString();
    }
  }
}