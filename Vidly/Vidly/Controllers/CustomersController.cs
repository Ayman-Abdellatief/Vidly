using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _Context;

        public CustomersController()
        {
            _Context = new ApplicationDbContext();
        }


        protected override void Dispose(bool disposing)
        {
            _Context.Dispose();

        }
        // GET: Customers
   
        public ActionResult Index()
        {
            //var Customers = _Context.Customers.Include(c =>c.MembershipType).ToList();
            return View();
        }


        public ActionResult Details(int? id)
        {
            var Customer = _Context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            return View(Customer);

        }

       public ActionResult New()
        {
            var membershipTypes = _Context.MembershipTypes.ToList();
            var viewmodel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm",viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                var viewmodel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _Context.MembershipTypes
                };
                return View("CustomerForm", viewmodel);
            }
            if(customer.Id == 0 )
            { 
            _Context.Customers.Add(customer);
            }
            else
            {
                var CustomerInDb = _Context.Customers.Single(c => c.Id == customer.Id);

                CustomerInDb.Name = customer.Name;
                CustomerInDb.Birthdate = customer.Birthdate;
                CustomerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
                CustomerInDb.MembershipTypeId = customer.MembershipTypeId;
            }
            _Context.SaveChanges();
            return RedirectToAction("Index","Customers");
        }


        public ActionResult Edit(int Id)
        {
            var Customer = _Context.Customers.SingleOrDefault(c => c.Id == Id);

            if (Customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = Customer,
                MembershipTypes = _Context.MembershipTypes
            };
            return View("CustomerForm", viewModel);
        }
    }
}