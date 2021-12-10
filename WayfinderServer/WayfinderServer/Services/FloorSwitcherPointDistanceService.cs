using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WayfinderServer.DTOs;
using WayfinderServer.Entities;

namespace WayfinderServer.Services
{
    public interface IFloorSwitcherPointDistanceService
    {
        public int AddTargetDistancesToFloorSwitcherPoint(List<TargetDistance> targetDistanceList, int floorSwitcherPointId);
        public int AddFloorSwithcerPointDistancesToFloorSwitcherPoint(List<FloorSwitcherPointDistance> floorSwitcherPointDistances, int floorSwitcherPointId);
    }
    public class FloorSwitcherPointDistanceService : IFloorSwitcherPointDistanceService
    {
        private WayfinderContext _context;

        public FloorSwitcherPointDistanceService(WayfinderContext context)
        {
            _context = context;
        }

        public int AddTargetDistancesToFloorSwitcherPoint(List<TargetDistance> targetDistanceList, int floorSwitcherPointId)
        {
            var floorSwitcherPoint = _context.FloorSwitcherPoints.SingleOrDefault(x => x.Id == floorSwitcherPointId);

            foreach (TargetDistance targetDistance in targetDistanceList)
            {
                //var floorSwitcherPointDistanceSaved = _context.FloorSwitcherPointDistances.SingleOrDefault(x => (x.FloorSwitcherPoint1.Id == floorSwitcherId && x.FloorSwitcherPoint2.Id == targetDistance.FloorSwitcherPoint.Id) || (x.FloorSwitcherPoint2.Id == floorSwitcherId && x.FloorSwitcherPoint1.Id == targetDistance.FloorSwitcherPoint.Id));
                var targetDistanceSaved = _context.TargetDistances.SingleOrDefault(x => x.Target.Id == targetDistance.Target.Id && x.FloorSwitcherPoint.Id == floorSwitcherPointId);
                var target = _context.Targets.SingleOrDefault(x => x.Id == targetDistance.Target.Id);


                if (targetDistanceSaved == null)
                {
                    targetDistance.Target = target;
                    targetDistance.FloorSwitcherPoint = floorSwitcherPoint;
                    var addResult = _context.TargetDistances.AddAsync(targetDistance);
                }
                else
                {
                    targetDistanceSaved.Distance = targetDistance.Distance;
                    targetDistanceSaved.Target = target;
                    targetDistanceSaved.FloorSwitcherPoint = floorSwitcherPoint;

                    var updateResult = _context.TargetDistances.Update(targetDistanceSaved);

                }

            }
            _context.SaveChanges();

            return 1;
        }


        public int AddFloorSwithcerPointDistancesToFloorSwitcherPoint(List<FloorSwitcherPointDistance> floorSwitcherPointDistanceList, int floorSwitcherPointId)
        {
            var floorSwitcherPoint = _context.FloorSwitcherPoints.SingleOrDefault(x => x.Id == floorSwitcherPointId);

            foreach (FloorSwitcherPointDistance floorSwitcherPointDistance in floorSwitcherPointDistanceList)
            {
                var floorSwitcherPointDistanceSaved = _context.FloorSwitcherPointDistances.SingleOrDefault(x => (x.FloorSwitcherPoint1.Id == floorSwitcherPointId && x.FloorSwitcherPoint2.Id == floorSwitcherPointDistance.FloorSwitcherPoint2.Id) || (x.FloorSwitcherPoint2.Id == floorSwitcherPointId && x.FloorSwitcherPoint1.Id == floorSwitcherPointDistance.FloorSwitcherPoint2.Id));
                
                var floorSwitcherPoint2 = _context.FloorSwitcherPoints.SingleOrDefault(x => x.Id == floorSwitcherPointDistance.FloorSwitcherPoint2.Id);


                if (floorSwitcherPointDistanceSaved == null)
                {
                    floorSwitcherPointDistance.FloorSwitcherPoint1 = floorSwitcherPoint;
                    floorSwitcherPointDistance.FloorSwitcherPoint2 = floorSwitcherPoint2;
                    var addResult = _context.FloorSwitcherPointDistances.AddAsync(floorSwitcherPointDistance);
                }
                else
                {
                    floorSwitcherPointDistanceSaved.Distance = floorSwitcherPointDistance.Distance;
                    floorSwitcherPointDistanceSaved.FloorSwitcherPoint1 = floorSwitcherPoint;
                    floorSwitcherPointDistanceSaved.FloorSwitcherPoint2 = floorSwitcherPoint2;

                    var updateResult = _context.FloorSwitcherPointDistances.Update(floorSwitcherPointDistanceSaved);

                }

            }
            _context.SaveChanges();

            return 1;
        }

    }

}
