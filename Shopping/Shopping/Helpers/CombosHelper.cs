using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Shopping.Data;
using Microsoft.EntityFrameworkCore;
using Shopping.Data.Entities;

namespace Shopping.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCategoriesAsync()
        {
            List<SelectListItem> list = await _context.Categories.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = $"{x.Id}"
            }).OrderBy(x => x.Text)
              .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una Categoría...]",
                Value = "0"
            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCategoriesAsync(IEnumerable<Category> filter)
        {
            List<Category> categories = await _context.Categories.ToListAsync();
            List<Category> categoriesFiltered = new();
            foreach (var category in categories)
            {
                if(!filter.Any(c=> c.Id == category.Id))
                {
                    categoriesFiltered.Add(category);
                }
            }

            List<SelectListItem> list = categoriesFiltered.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = $"{x.Id}"
            }).OrderBy(x => x.Text)
              .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una Categoría...]",
                Value = "0"
            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCountriesAsync()
        {
            List<SelectListItem> list = _context.Countries.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = $"{x.Id}"
            })
                .OrderBy(x => x.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un País...]",
                Value = "0"
            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboStatesAsync(int countryId)
        {
            List<SelectListItem> list = _context.States
                .Where(x=>x.Country.Id == countryId)
                .Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = $"{x.Id}"
            })
                .OrderBy(x => x.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un Estado/Provincia...]",
                Value = "0"
            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCitiesAsync(int stateId)
        {
            List<SelectListItem> list = _context.Cities
                .Where(x => x.State.Id == stateId)
                .Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = $"{x.Id}"
            })
                .OrderBy(x => x.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una Ciudad...]",
                Value = "0"
            });

            return list;
        }
    }
}
