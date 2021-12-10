using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WayfinderServer.Entities;
using WayfinderServer.Helpers;

namespace WayfinderServer.DTOs
{
    public class TargetPathStepsDTO
    {
        public TargetBaseDTO target { get; set; }
        //public PathStepListDTO pathSteps { get; set; }
        //public List<PathStep> pathSteps { get; set; }
        public List<FloorSwitcherPointStep> floorSwitcherPointSteps { get; set; }
    }
}
