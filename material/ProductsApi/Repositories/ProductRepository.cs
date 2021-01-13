using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ProductsApi.Models;
using ProductsApi.Data;

namespace Repositories
{
    public class ProductRepository
    {
        private readonly AdventureWorksDbContext _context;

        public ProductRepository(AdventureWorksDbContext context)
        {
            _context = context;
        }

        public Product[] Get()
        {
            Console.WriteLine(_context.Products);
            return _context.Products.ToArray();
        }

        public Product Add(Product value)
        {
            _context.Products.Add(value);

            _context.SaveChanges();

            return value;
        }

        public void Delete(int id)
        {
            var match = _context.Products.Find(id);
            if (match != null)
            {
                _context.Products.Remove(match);
            }
            _context.SaveChanges();
        }
    }
}