using Identity.Data.DataContexts;
using Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Data.Repositories
{
    public class SupplierRepository:BaseRepository<Supplier>
    {

        public SupplierRepository(DataContext  context): base(context)
        {
        }
    }
}
