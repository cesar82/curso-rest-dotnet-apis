﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductsApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        //private readonly AdventureWorksDbContext _context;
        private readonly object[] _products;

        public ProductsController(ILogger<ProductsController> logger,
            object[] products
            //, AdventureWorksDbContext context
            )
        {
            _logger = logger;
            //_context = context;
            _logger.LogInformation("ProductsController constructor");
            _products = products;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<object> Get()
        {
            return _products;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_products[0]);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            //return CreatedAtAction(nameof(GetById), new { Id = value.Id }, value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return new ObjectResult(new object()) { StatusCode = (int)HttpStatusCode.NotImplemented };
        }
    }
}
