using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProductRepository :Repository<Product>, IProductRepository
    {
        private readonly ShopDbContext _context;

        public ProductRepository(ShopDbContext context):base(context)
        {
            _context = context;
        }
       
    }
}
