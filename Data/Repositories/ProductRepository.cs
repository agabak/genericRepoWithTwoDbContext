using Identity.Data.DataContexts;
using Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Identity.Data.Repositories
{
    public class ProductRepository: BaseRepository<Product>
    { 
        public ProductRepository(DataContext context): base(context)
        {
        }

        public override async Task<IEnumerable<Product>> GetAllWithInclude(params Expression<Func<Product, object>>[] includeProperties)
        {
            var products = await base.GetAllWithInclude(includeProperties).ConfigureAwait(true);
            return products.Where(p => !p.IsDiscontinued).OrderBy(x => x.ProductName);
        }
    }
}
