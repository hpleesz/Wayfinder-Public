using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WayfinderServer.Entities
{
    public class VirtualObject
    {
        public int Id { get; set; }
        public Target Target { get; set; }
        public VirtualObjectType Type { get; set; }
        public string FileLocation { get; set; }
        public string TextureLocation { get; set; }
        public float XCoordinate { get; set; }
        public float YCoordinate { get; set; }
        public float ZCoordinate { get; set; }
        public float XRotation { get; set; }
        public float YRotation { get; set; }
        public float ZRotation { get; set; }
        public float XScale { get; set; }
        public float YScale { get; set; }
        public float ZScale { get; set; }
    }
}
