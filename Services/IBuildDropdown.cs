using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Services
{
    public interface IBuildDropdown
    {
        Task<IEnumerable<SelectListItem>> DropdownItems();
    }
}
