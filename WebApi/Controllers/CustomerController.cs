using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VidlyTutorial.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CustomerController : ApiController
    {
        //[HttpGet]
        //public string Greet(string name)
        //{
        //    return "Welcome " + name;
        //}
        ApplicationDbContext context;

        public CustomerController()
        {
            context = new ApplicationDbContext(); 
        }
        
        [HttpGet]
        /// Get /api/customers
        public IEnumerable<Customer> Customers()
        {
            return context.Customer.Include(c => c.MembershipType).ToList();
        }

        [HttpGet]
        /// Get /api/customers
        public IEnumerable<MembershipType> Members()
        {
            return context.Members.ToList();
        }

        [HttpGet]
        //api/Customers/9
        public Customer CustomerById(int id)
        {
            var customer = context.Customer.FirstOrDefault(x => x.Id == id);
            //if (customer == null)
            //{
            //    throw new HttpResponseException(HttpStatusCode.NotFound);
            //}
            //else
                return customer;
        }

        [HttpPost]
        public HttpResponseMessage AddCustomer(Customer customer)
        {
            try
            {
                context.Customer.Add(customer);
                context.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.Created);
            }catch(Exception er)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                throw;
            }
        }

        [System.Web.Http.HttpPut]
        public HttpResponseMessage UpdateCustomer(Customer customer)
        {
            try
            {
                context.Customer.AddOrUpdate(customer);
                context.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (Exception er)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                throw;
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteCustomer(int id)
        {
            try
            {
                var Customer = context.Customer.FirstOrDefault(x=> x.Id == id);
                if(Customer==null)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
                context.Customer.Remove(Customer);
                context.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (Exception er)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                throw;
            }
        }
    }
}
