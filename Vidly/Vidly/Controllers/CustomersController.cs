using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {

        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            //base.Dispose(disposing);

        }
        // GET: Customers
        public ActionResult Index()
        {

            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }

        [Route("customers/details/{id}")]
        public ActionResult Details(int id)
        {


            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c=> c.Id == id);

            if (customer!=null)
            {
                ViewResult detailResult = View();
                detailResult.ViewData.Model = customer;
                return detailResult;
            }
            else
            {
                return HttpNotFound();

            }
   
           

        }


        public ActionResult Edit(int id)
        {


            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            var viewModel = new NewCustomerViewModel()
            {
                Customer = customer,
                MembershipType = _context.MembershipType.ToArray()

            };

            if (customer != null)
            {
                ViewResult detailResult = View("CustomerForm");
                detailResult.ViewData.Model = viewModel;
                return detailResult;
            }
            else
            {
                return HttpNotFound();

            }



        }


        public ActionResult New()
        {
            var membershipType = _context.MembershipType.ToList();
            var viewModel = new NewCustomerViewModel()
            {
                MembershipType = membershipType
            };
            return View("CustomerForm",viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (customer.Id == 0)            
                _context.Customers.Add(customer);          
            else
            {
                var customerInDB = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDB.Name = customer.Name;
                customerInDB.DateOfBirth = customer.DateOfBirth;
                customerInDB.MembershipTypeId = customer.MembershipTypeId;
                customerInDB.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }



    }
}