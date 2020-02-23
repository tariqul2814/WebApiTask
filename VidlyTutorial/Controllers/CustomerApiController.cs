using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using VidlyTutorial.Models;

namespace VidlyTutorial.Controllers
{
    public class CustomerApiController : ApiController
    {
        Uri baseAddress = new Uri("http://localhost:54519/api");
        HttpClient client;

        public CustomerApiController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        [HttpGet]
        /// Get /api/customers
        public IEnumerable<Customer> Customers()
        {
            List<Customer> Obj = new List<Customer>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Customer/Customers").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                Obj = JsonConvert.DeserializeObject<List<Customer>>(data);
            }

            return Obj;
        }

        [HttpGet]
        /// Get /api/customers
        public IEnumerable<MembershipType> Members()
        {
            List<MembershipType> Obj = new List<MembershipType>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Customer/Members").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                Obj = JsonConvert.DeserializeObject<List<MembershipType>>(data);
            }

            return Obj;
        }

        [HttpGet]
        //api/Customers/9
        public Customer CustomerById(int id)
        {
            Customer customer = new Customer();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Customer/CustomerById?id=" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                customer = JsonConvert.DeserializeObject<Customer>(data);
                
            }
            return customer;
        }

        [HttpPost]
        public HttpResponseMessage AddCustomer(Customer customer)
        {
            try
            {
                string data = JsonConvert.SerializeObject(customer);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Customer/AddCustomer", content).Result;
                
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (Exception er)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                throw;
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateCustomer(Customer customer)
        {
            try
            {
                string data = JsonConvert.SerializeObject(customer);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                //HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Customer/AddCustomer", content).Result;
                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Customer/UpdateCustomer", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return new HttpResponseMessage(HttpStatusCode.Created);
                }
                else
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);

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
                HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/Customer/DeleteCustomer?id=" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return new HttpResponseMessage(HttpStatusCode.Created);
                }
                else
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            catch (Exception er)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                throw;
            }
        }
    }
}
