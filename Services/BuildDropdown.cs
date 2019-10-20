using Identity.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Identity.Services
{
    public static class BuildDropdown
    {
        public static IEnumerable<SelectListItem> DropdownItems(IEnumerable<Supplier> suppliers)
        {
            var items = new List<SelectListItem>();

            foreach (var supplier in suppliers.ToList())
            {
                var item = new SelectListItem
                {
                    Value =  supplier?.Id.ToString(),
                    Text = supplier.CompanyName
                };
                items.Add(item);
            }
            return items;
        }
    }
}
