using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;


        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        //Get/api/Customers
        public IHttpActionResult GetCustomers()
        {
            var customerDtos = _context.Customers.ToList().Select(Mapper.Map<Customer,CustomerDto>);

         return   Ok(customerDtos);
        }

        //Get/api/customer/1

        public IHttpActionResult GetCustomer(int Id)
        {

            var customer = _context.Customers.SingleOrDefault(c => c.Id == Id);
            if (customer == null)
                return NotFound();
            return Ok(Mapper.Map<Customer,CustomerDto>(customer));
        }

        //post/api/customer
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri +"/"+customer.Id), customerDto);

        }

        //Put/api/Customer
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int Id , CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var CustomerInDb = _context.Customers.SingleOrDefault(c => c.Id == Id);

            if(CustomerInDb == null)
                return NotFound();

            Mapper.Map(customerDto, CustomerInDb);

            
            _context.SaveChanges();
            return Ok();
        }

        //Delete/api/Customer
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int Id)
        {
            var CustomerInDb = _context.Customers.SingleOrDefault(c => c.Id == Id);

            if (CustomerInDb == null)
                return NotFound();

            _context.Customers.Remove(CustomerInDb);

            _context.SaveChanges();

            return Ok();
        }
    }
}
