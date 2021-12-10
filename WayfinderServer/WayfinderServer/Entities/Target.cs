using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WayfinderServer.Entities
{
    public class Target: IQRCodePoint
    {
        public int Id { get; set;}
        public Category Category { get; set; }
        public Floor Floor { get; set; }
        public string Name { get; set; }
        public string QRCode { get; set; }
        public float XCoordinate { get; set; }
        public float YCoordinate { get; set; }
        public float ZCoordinate { get; set; }

        public float XRotation { get; set; }
        public float YRotation { get; set; }
        public float ZRotation { get; set; }

        public List<VirtualObject> VirtualObjects { get; set; }
        public List<TargetDistance> TargetDistances { get; set; }

    }
}
