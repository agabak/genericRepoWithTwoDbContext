using Identity.Data.Repositories;
using Identity.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Services
{
    public  class BuildDropdown : IBuildDropdown
    {
        private readonly SupplierRepository _supplierRepository;

        public BuildDropdown(SupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async  Task<IEnumerable<SelectListItem>> DropdownItems()
        {
            var suppliers = await _supplierRepository.GetAll()
                                              .ConfigureAwait(true);
            var items = new List<SelectListItem>();

            foreach (var supplier in suppliers.ToList())
            {
                var item = new SelectListItem
                            {
                                Value = supplier?.Id.ToString(),
                                Text = supplier.CompanyName
                            };
                items.Add(item);
            }
            return items;
        }
    }
}
