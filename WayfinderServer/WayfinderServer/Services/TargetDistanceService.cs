using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WayfinderServer.DTOs;
using WayfinderServer.Entities;

namespace WayfinderServer.Services
{
    public interface ITargetDistanceService
    {
        public int AddFloorSwithcerPointDistancesToTarget(List<TargetDistance> targetDistanceList, int targetId);

    }
    public class TargetDistanceService : ITargetDistanceService
    {
        private WayfinderContext _context;

        public TargetDistanceService(WayfinderContext context)
        {
            _context = context;
        }

        public int AddFloorSwithcerPointDistancesToTarget(List<TargetDistance> targetDistanceList, int targetId)
        {
            var target = _context.Targets.SingleOrDefault(x => x.Id == targetId);

            foreach (TargetDistance targetDistance in targetDistanceList)
            {
                var targetDistanceSaved = _context.TargetDistances.SingleOrDefault(x => x.Target.Id == targetId && x.FloorSwitcherPoint.Id == targetDistance.FloorSwitcherPoint.Id);
                var floorSwitcherPoint = _context.FloorSwitcherPoints.SingleOrDefault(x => x.Id == targetDistance.FloorSwitcherPoint.Id);

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


    }

}
