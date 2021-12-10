using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WayfinderServer.DTOs
{
    public class MarkerDTO
    {
        public int Id { get; set; }
        public FloorDTO Floor { get; set; }
        public string Name { get; set; }
        public string QRCode { get; set; }
        public float XCoordinate { get; set; }
        public float YCoordinate { get; set; }
        public float ZCoordinate { get; set; }

        public float XRotation { get; set; }
        public float YRotation { get; set; }
        public float ZRotation { get; set; }

    }
}
