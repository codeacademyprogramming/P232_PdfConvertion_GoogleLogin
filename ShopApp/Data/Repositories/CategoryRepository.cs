using Core.Entities;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CategoryRepository: Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(ShopDbContext context):base(context)
        {

        }
    }
}
