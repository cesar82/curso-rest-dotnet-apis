﻿using System;
using System.Collections.Generic;
using System.Reflection;
using ProductsApi.Models;

namespace Repositories
{
    public class ProductRepository
    {
        private readonly List<Product> _products;

        public ProductRepository()
        {
            _products = new List<Product>(new[] { new Product { Id = 1, Name = "Pants" } });

        }

        public Product[] Get()
        {
            return _products.ToArray();
        }

        internal Product Add(Product value)
        {
            _products.Add(value);
            value.Id = _products.Count;
            return value;
        }
    }
}