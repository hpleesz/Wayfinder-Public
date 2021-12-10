using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WayfinderServer.DTOs;
using WayfinderServer.Entities;
using WayfinderServer.Helpers;

namespace WayfinderServer.Services
{
    public interface ITargetService
    {
        public int AddTarget(Target target, int floorId);
        public Target GetTarget(int id);
        public IEnumerable<Target> GetAllTargetsByFloor(int floorId);
        public IEnumerable<Target> GetAllTargetsByPlace(int placeId);
        public IEnumerable<Target> GetTargetsSearch(string term, int placeId);
        public IEnumerable<Target> GetTargetsSearchByCategory(string term, int categpryId, int placeId);
        public int EditCoordinates(int id, Target target);
        //
        public TargetPathStepsDTO GetPathToTarget(int targetId, List<PathStep> floorSwitcherPointDistancesList);
        public List<TargetPathStepsDTO> GetPathToTargets(int placeId, List<PathStep> floorSwitcherPointDistancesList);
        public List<TargetPathStepsDTO> GetPathToTargetsSearchByCategory(int placeId, List<PathStep> floorSwitcherPointDistancesList, string term, int categoryId);
    }
    public class TargetService : ITargetService
    {
        private WayfinderContext _context;

        public TargetService(WayfinderContext context)
        {
            _context = context;
        }

        public int AddTarget(Target target, int floorId)
        {
            var floor = _context.Floors.SingleOrDefault(x => x.Id == floorId);

            var category = _context.Categories.SingleOrDefault(x => x.Id == target.Category.Id);

            target.Floor = floor;
            if (floor.Targets == null)
            {
                floor.Targets = new List<Target>();
            }
            target.Category = category;
            floor.Targets.Add(target);
            if (category.Targets == null)
            {
                category.Targets = new List<Target>();
            }
            category.Targets.Add(target);
            var addResult = _context.Targets.AddAsync(target);
            var updateResult = _context.Floors.Update(floor);
            var updateResult2 = _context.Categories.Update(category);

            _context.SaveChanges();

            target.QRCode = "TARGET_" + target.Id;
            var updateTarget = _context.Targets.Update(target);
            _context.SaveChanges();


            return target.Id;
        }

        public Target GetTarget(int id)
        {
            return _context.Targets.Include(x => x.Floor.FloorPlan3D).SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Target> GetAllTargetsByFloor(int floorId)
        {
            return _context.Targets.Where(t => t.Floor.Id == floorId).ToList();
        }

        public IEnumerable<Target> GetAllTargetsByPlace(int placeId)
        {
            return _context.Targets.Include(t => t.Floor).Include(t => t.Category).Where(t => t.Floor.Place.Id == placeId).ToList();
        }

        public IEnumerable<Target> GetTargetsSearch(string term, int placeId)
        {
            if (term != null)
            {
                term = term.ToUpper();
                return _context.Targets.Include(f => f.Floor).Include(f => f.Category).Where(f => f.Floor.Place.Id == placeId && f.Name.ToUpper().Contains(term)).ToList();

            }
            else
            {
                return GetAllTargetsByPlace(placeId);
            }
        }

        public IEnumerable<Target> GetTargetsSearchByCategory(string term, int categoryId, int placeId)
        {
            if (term != null)
            {
                term = term.ToUpper();
                if(categoryId > 0)
                {
                    return _context.Targets.Include(f => f.Floor).Include(f => f.Category).Where(f => f.Floor.Place.Id == placeId && f.Name.ToUpper().Contains(term) && f.Category.Id == categoryId).ToList();
                }
                else
                {
                    return _context.Targets.Include(f => f.Floor).Include(f => f.Category).Where(f => f.Floor.Place.Id == placeId && f.Name.ToUpper().Contains(term)).ToList();
                }

            }
            else
            {
                if(categoryId > 0)
                {
                    return _context.Targets.Include(f => f.Floor).Include(f => f.Category).Where(f => f.Floor.Place.Id == placeId && f.Category.Id == categoryId).ToList();
                }
                else
                {
                    return GetAllTargetsByPlace(placeId);
                }
            }

        }
        public int EditCoordinates(int id, Target target)
        {
            var result = _context.Targets.SingleOrDefault(x => x.Id == id);
            result.XCoordinate = target.XCoordinate;
            result.YCoordinate = target.YCoordinate;
            result.ZCoordinate = target.ZCoordinate;
            result.XRotation = target.XRotation;
            result.YRotation = target.YRotation;
            result.ZRotation = target.ZRotation;

            var updated = _context.Targets.Update(result);
            _context.SaveChanges();
            //return result.IsCompletedSuccessfully;
            return result.Id;
        }

        //List<FloorSwitcherPoint> floorSwitcherPoints, List<int> distances
        //floor switcher point distances: (floorSwitcherPoint id, distance)
        public TargetPathStepsDTO GetPathToTarget(int targetId, List<PathStep> floorSwitcherPointDistancesList)
        {
            Dictionary<int, double> floorSwitcherPointDistances = new Dictionary<int, double>();
            foreach(PathStep pathStep in floorSwitcherPointDistancesList)
            {
                floorSwitcherPointDistances.Add(pathStep.StepId, pathStep.Distance);
            }
        
            const int SOURCE_ID = 0;
            const int DESTINATION_ID = -1;

            Target targetDestination = _context.Targets.Include(x => x.Floor).Include(x => x.Category).Include(x => x.Floor.Place).SingleOrDefault(x => x.Id == targetId);
            //return target;
            
            var directedAL = new Graph();

            Dictionary<int, Vertex> vertices = new Dictionary<int, Vertex>();

            var source = new Vertex(SOURCE_ID);
            vertices.Add(SOURCE_ID, source);
            var destination = new Vertex(DESTINATION_ID);
            vertices.Add(DESTINATION_ID, destination);

            List<FloorSwitcher> floorSwitchersByPlace = _context.FloorSwitchers.Where(fs => fs.Place.Id == targetDestination.Floor.Place.Id).ToList();

            foreach(FloorSwitcher floorSwitcherByPlace in floorSwitchersByPlace)
            {
                List<FloorSwitcherPoint> pointsByFloorSwitcher = _context.FloorSwitcherPoints.Include(x => x.FloorSwitcher).Include(x => x.Floor).Where(x => x.FloorSwitcher.Id == floorSwitcherByPlace.Id).OrderBy(x => x.Floor.Number).ToList();

                //add all floor switcher points in place
                foreach (FloorSwitcherPoint point in pointsByFloorSwitcher)
                {
                    var v = new Vertex(point.Id);
                    vertices.Add(point.Id, v);
                }

                //add all floor switcher point connections
                for(int i = 0; i < pointsByFloorSwitcher.Count-1; i++)
                {
                    if(floorSwitcherByPlace.Up)
                    {
                        Vertex from = vertices[pointsByFloorSwitcher[i].Id];
                        Vertex to = vertices[pointsByFloorSwitcher[i+1].Id];
                        int distance = pointsByFloorSwitcher[i + 1].Floor.Number - pointsByFloorSwitcher[i].Floor.Number;
                        directedAL.AddEdgeDirected(from, to, distance);
                    }
                    if (floorSwitcherByPlace.Down)
                    {
                        Vertex from = vertices[pointsByFloorSwitcher[i+1].Id];
                        Vertex to = vertices[pointsByFloorSwitcher[i].Id];
                        int distance = pointsByFloorSwitcher[i + 1].Floor.Number - pointsByFloorSwitcher[i].Floor.Number;
                        directedAL.AddEdgeDirected(from, to, distance);
                    }
                }
            }

            //starting point to floor switchers on same floor
            for(int i = 0; i < floorSwitcherPointDistances.Count; i++)
            {

                Vertex v = vertices[floorSwitcherPointDistances.ElementAt(i).Key];
                double distance = floorSwitcherPointDistances.ElementAt(i).Value;
                directedAL.AddEdgeDirected(vertices[SOURCE_ID], v, distance);
            }


            List<TargetDistance> destinationTargetDistances = _context.TargetDistances.Include(td => td.FloorSwitcherPoint).Include(td => td.Target).Where(td => td.Target.Id == targetId).ToList();
            
            //add all distances from floor switcher points on target floor to target
            for(int i = 0; i < destinationTargetDistances.Count; i++)
            {
                Vertex from = vertices[destinationTargetDistances[i].FloorSwitcherPoint.Id];
                //double distance = (double)Math.Round(destinationTargetDistances[i].Distance);
                double distance = Double.Parse(destinationTargetDistances[i].Distance.ToString());
                directedAL.AddEdgeDirected(from, vertices[DESTINATION_ID], distance);
            }

            List<FloorSwitcherPointDistance> floorSwitcherPointDistancesDb = _context.FloorSwitcherPointDistances.Include(td => td.FloorSwitcherPoint1).Include(td => td.FloorSwitcherPoint2).Where(td => td.FloorSwitcherPoint1.Floor.Place.Id == targetDestination.Floor.Place.Id).ToList();

            //add all distances from floor switcher points on target floor to target
            for (int i = 0; i < floorSwitcherPointDistancesDb.Count; i++)
            {
                Vertex from = vertices[floorSwitcherPointDistancesDb[i].FloorSwitcherPoint1.Id];
                Vertex to = vertices[floorSwitcherPointDistancesDb[i].FloorSwitcherPoint2.Id];

                //double distance = (double)Math.Round(destinationTargetDistances[i].Distance);
                double distance = Double.Parse(floorSwitcherPointDistancesDb[i].Distance.ToString());
                directedAL.AddEdgeDirected(from, to, distance);
                directedAL.AddEdgeDirected(to, from, distance);

            }

            var dijkstra = PathFinding.Instance.DijstrasShortestPath(directedAL, vertices[SOURCE_ID]);

            PathFinding.Instance.PrintShortestPath(directedAL, vertices[SOURCE_ID], vertices[DESTINATION_ID]);

            TargetPathStepsDTO targetPathStepsDTO = new TargetPathStepsDTO();
            targetPathStepsDTO.target = new TargetBaseDTO();
            targetPathStepsDTO.target.Id = targetDestination.Id;
            targetPathStepsDTO.target.Name = targetDestination.Name;
            targetPathStepsDTO.target.Floor = new FloorBaseDTO();
            targetPathStepsDTO.target.Floor.Id = targetDestination.Floor.Id;
            targetPathStepsDTO.target.Floor.Name = targetDestination.Floor.Name;
            targetPathStepsDTO.target.Floor.Number = targetDestination.Floor.Number;

            targetPathStepsDTO.target.Category = new CategoryNewDTO();
            targetPathStepsDTO.target.Category.Id = targetDestination.Category.Id;
            targetPathStepsDTO.target.Category.Name = targetDestination.Category.Name;

            targetPathStepsDTO.target.XCoordinate = targetDestination.XCoordinate;
            targetPathStepsDTO.target.YCoordinate = targetDestination.YCoordinate;
            targetPathStepsDTO.target.ZCoordinate = targetDestination.ZCoordinate;
            targetPathStepsDTO.target.XRotation = targetDestination.XRotation;
            targetPathStepsDTO.target.YRotation = targetDestination.YRotation;
            targetPathStepsDTO.target.ZRotation = targetDestination.ZRotation;

            //targetPathStepsDTO.pathSteps = PathFinding.pathSteps;
            List<FloorSwitcherPointStep> floorSwitcherPointSteps = new List<FloorSwitcherPointStep>();

            foreach (PathStep pathStep in PathFinding.pathSteps)
            {
                FloorSwitcherPointStep floorSwitcherPointStep = new FloorSwitcherPointStep();
                floorSwitcherPointStep.distance = pathStep.Distance;
                floorSwitcherPointStep.floorSwitcherPoint = new FloorSwitcherPointDTO();

                FloorSwitcherPoint floorSwitcherPoint = new FloorSwitcherPoint();
                floorSwitcherPointStep.floorSwitcherPoint.Id = pathStep.StepId;

                if (pathStep.StepId > 0)
                {
                    floorSwitcherPoint = _context.FloorSwitcherPoints.Include(x => x.Floor).Include(x => x.Floor.FloorPlan3D).Include(x => x.FloorSwitcher).SingleOrDefault(x => x.Id == pathStep.StepId);
                    
                    floorSwitcherPointStep.floorSwitcherPoint.Id = floorSwitcherPoint.Id;
                    floorSwitcherPointStep.floorSwitcherPoint.Name = floorSwitcherPoint.Name;
                    floorSwitcherPointStep.floorSwitcherPoint.XCoordinate = floorSwitcherPoint.XCoordinate;
                    floorSwitcherPointStep.floorSwitcherPoint.YCoordinate = floorSwitcherPoint.YCoordinate;
                    floorSwitcherPointStep.floorSwitcherPoint.ZCoordinate = floorSwitcherPoint.ZCoordinate;

                    floorSwitcherPointStep.floorSwitcherPoint.Floor = new FloorNewDTO();
                    floorSwitcherPointStep.floorSwitcherPoint.Floor.Id = floorSwitcherPoint.Floor.Id;
                    floorSwitcherPointStep.floorSwitcherPoint.Floor.Name = floorSwitcherPoint.Floor.Name;
                    floorSwitcherPointStep.floorSwitcherPoint.Floor.Number = floorSwitcherPoint.Floor.Number;

                    floorSwitcherPointStep.floorSwitcherPoint.Floor.floorPlan3D = new FloorPlan3DDTO();
                    floorSwitcherPointStep.floorSwitcherPoint.Floor.floorPlan3D.FileLocation = floorSwitcherPoint.Floor.FloorPlan3D.FileLocation;

                    floorSwitcherPointStep.floorSwitcherPoint.FloorSwitcher = new FloorSwitcherNewDTO();
                    floorSwitcherPointStep.floorSwitcherPoint.FloorSwitcher.Id = floorSwitcherPoint.FloorSwitcher.Id;
                    floorSwitcherPointStep.floorSwitcherPoint.FloorSwitcher.Name = floorSwitcherPoint.FloorSwitcher.Name;
                    floorSwitcherPointStep.floorSwitcherPoint.FloorSwitcher.Up = floorSwitcherPoint.FloorSwitcher.Up;
                    floorSwitcherPointStep.floorSwitcherPoint.FloorSwitcher.Down = floorSwitcherPoint.FloorSwitcher.Down;




                }
                floorSwitcherPointSteps.Add(floorSwitcherPointStep);

            }
            targetPathStepsDTO.floorSwitcherPointSteps = floorSwitcherPointSteps;
            return targetPathStepsDTO;
        }

        public List<TargetPathStepsDTO> GetPathToTargets(int placeId, List<PathStep> floorSwitcherPointDistancesList)
        {
            List<TargetPathStepsDTO> targetPathSteps = new List<TargetPathStepsDTO>();
            IEnumerable<Target> targets = GetAllTargetsByPlace(placeId);

            foreach(Target target in targets)
            {

                TargetPathStepsDTO targetPathStepsDTO = GetPathToTarget(target.Id, floorSwitcherPointDistancesList);

                targetPathSteps.Add(targetPathStepsDTO);
            }

            return targetPathSteps;
        }

        public List<TargetPathStepsDTO> GetPathToTargetsSearchByCategory(int placeId, List<PathStep> floorSwitcherPointDistancesList, string term, int categoryId)
        {
            List<TargetPathStepsDTO> targetPathSteps = new List<TargetPathStepsDTO>();
            IEnumerable<Target> targets = GetTargetsSearchByCategory(term, categoryId, placeId);

            foreach (Target target in targets)
            {

                TargetPathStepsDTO targetPathStepsDTO = GetPathToTarget(target.Id, floorSwitcherPointDistancesList);

                targetPathSteps.Add(targetPathStepsDTO);
            }
            List<TargetPathStepsDTO> SortedList = targetPathSteps.OrderBy(o => o.floorSwitcherPointSteps[o.floorSwitcherPointSteps.Count-1].distance).ToList();

            return SortedList;
        }

    }

}
