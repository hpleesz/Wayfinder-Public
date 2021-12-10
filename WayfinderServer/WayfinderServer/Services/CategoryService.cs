using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WayfinderServer.Entities;

namespace WayfinderServer.Services
{
    public interface ICategoryService
    {
        public int AddCategory(Category category, int placeId);
        public int EditCategory(int categoryId, Category category);
        public Category GetCategory(int id);
        public IEnumerable<Category> GetAllCategoriesByPlace(int placeId);
        public IEnumerable<Category> GetCategoriesSearch(string term, int placeId);

    }
    public class CategoryService : ICategoryService
    {
        private WayfinderContext _context;

        public CategoryService(WayfinderContext context)
        {
            _context = context;
        }

        public int AddCategory(Category category, int placeId)
        {
            var place = _context.Places.SingleOrDefault(x => x.Id == placeId);
            category.Place = place;
            if (place.Categories == null)
            {
                place.Categories = new List<Category>();
            }
            place.Categories.Add(category);

            var addResult = _context.Categories.AddAsync(category);
            var updateResult = _context.Places.Update(place);

            _context.SaveChanges();

            return category.Id;
        }

        public int EditCategory(int id, Category category)
        {
            var result = _context.Categories.SingleOrDefault(x => x.Id == id);
            result.Name = category.Name;

            var updated = _context.Categories.Update(category);
            _context.SaveChanges();
            //return result.IsCompletedSuccessfully;
            return result.Id;
        }

        public Category GetCategory(int id)
        {
            //return _context.Events.Include(x => x.Categories).Where(x => x.Active).SingleOrDefault(x => x.Id == id);
            return _context.Categories.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Category> GetAllCategoriesByPlace(int placeId)
        {
            return _context.Categories.Include(f=> f.Targets).Where(f => f.Place.Id == placeId).ToList();
        }

        public IEnumerable<Category> GetCategoriesSearch(string term, int placeId)
        {
            if (term != null)
            {
                term = term.ToUpper();
                return _context.Categories.Where(f => f.Place.Id == placeId && f.Name.ToUpper().Contains(term)).ToList();

            }
            else
            {
                return GetAllCategoriesByPlace(placeId);
            }
        }
    }

}
