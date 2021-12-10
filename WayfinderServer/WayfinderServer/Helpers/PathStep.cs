using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WayfinderServer.Helpers
{
    public class PathStep
    {
        public PathStep(int stepId, double distance)
        {
            StepId = stepId;
            Distance = distance;
        }

        public int StepId { get; set; }
        public double Distance { get; set; }

    }
}
