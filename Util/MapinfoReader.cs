using System;
using System.IO;
using mapinforeader.Models;
using mapinforeader.Models.MapinfoSections;
using System.Text;

namespace mapinforeader.Util {
  public class MapinfoReader : BinaryReader {
    public MapinfoReader(Stream s) : base(s) { }

    public MapinfoReader(Stream s, Encoding e) : base(s, e) { }

    public MapinfoReader(Stream s, Encoding e, Boolean b) : base(s, e, b) { }

    public MapinfoSection ReadSectionMetadata() {
      MapinfoSection section = null;
      long? position = this.BaseStream.Position;
      if (position.HasValue) {
          section = new MapinfoSection();
          section.HeaderOffset = position.Value;
          byte[] headerBytes = this.ReadBytes(MapinfoSection.HeaderLength);
          if (!SMFileUtils.MatchByteArrays(headerBytes, Mapinfo.Terminator)){
            string headerString = System.Text.Encoding.ASCII.GetString(headerBytes);
            section.Header = headerString; 
            section.SizeOffset = this.BaseStream.Position;
            byte[] sectionSizeBytes = this.ReadBytes(MapinfoSection.SectionSizeLength);
            section.Size = BitConverter.ToUInt32(sectionSizeBytes, 0);
            section.ContentOffset = this.BaseStream.Position;
          } else {
            section = null;
          }
      }
      return section;
    }

    public Mapinfo ReadMapinfo() {
      Mapinfo m = new Mapinfo();
      MapinfoSection section = this.ReadSectionMetadata();
      while (section != null) {
        m.Sections.Add(section);
        if (section.HeaderOffset + section.Size <= this.BaseStream.Length) {
          this.BaseStream.Seek(section.HeaderOffset + section.Size, SeekOrigin.Begin);
          section = this.ReadSectionMetadata();
        } else {
          section = null;
        }
      }
      return m;
    }
  }
}