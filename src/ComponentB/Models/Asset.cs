using System;
using System.Collections.Generic;
using System.Text;

namespace ComponentB.Models
{
    public class Asset
    {
        public uint Id { get; set; }
        public string Blockchain { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public int Precision { get; set; }
    }
}
