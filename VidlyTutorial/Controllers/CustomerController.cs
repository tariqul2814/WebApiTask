using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using VidlyTutorial.Models;
using VidlyTutorial.Models.ViewModel;
using Newtonsoft;
using Newtonsoft.Json;
using System.Text;

namespace VidlyTutorial.Controllers
{
    public class CustomerController : Controller
    {

        Uri baseAddress = new Uri("http://localhost:62930/api");
        HttpClient client;

        public CustomerController()
        {
            client = new HttpClient(); 
            client.BaseAddress = baseAddress;
        }

        public ActionResult Index()
        {
            List<Customer> Obj = new List<Customer>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress+ "/CustomerApi/Customers").Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                Obj = JsonConvert.DeserializeObject<List<Customer>>(data);
            }
            
            return View(Obj);
        }
        [HttpPost]
        public ActionResult Create(NewCustomerViewModel New)
        {
            string data = JsonConvert.SerializeObject(New.customer);
            StringContent content = new StringContent(data, Encoding.UTF8,"application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/CustomerApi/AddCustomer", content).Result;
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int Id)
        {
            RootObject Obj = new RootObject();
            List<MembershipType> Mem = new List<MembershipType>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/CustomerApi/CustomerById?id=" + Id).Result;
            HttpResponseMessage response1 = client.GetAsync(client.BaseAddress + "/CustomerApi/Members").Result;
            if (response.IsSuccessStatusCode && response1.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                Obj = JsonConvert.DeserializeObject<RootObject>(data);
                var Customer = new Customer()
                {
                    Id = Obj.Id,
                    Birth = Obj.Birth,
                    IsSubscribedToNewsletter = Obj.IsSubscribedToNewsletter,
                    MembershipTypeId = Obj.MembershipTypeId,
                    Name = Obj.Name
                };
                data = response1.Content.ReadAsStringAsync().Result;
                Mem = JsonConvert.DeserializeObject<List<MembershipType>>(data);
                
                var NewCustomerViewModel = new NewCustomerViewModel
                {
                    customer = Customer,
                    membershipType = Mem
                };
                return View(NewCustomerViewModel);
            }
            else
            {
                var NewCustomerViewModel = new NewCustomerViewModel();
                return View(NewCustomerViewModel);
            }
            
        }
        [HttpPost]
        public ActionResult EditCustomer(NewCustomerViewModel newCustomer)
        {
            string data = JsonConvert.SerializeObject(newCustomer.customer);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            //HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Customer/AddCustomer", content).Result;
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/CustomerApi/UpdateCustomer", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult CreateCustomer()
        {
            List<MembershipType> members = new List<MembershipType>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/CustomerApi/Members").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                members = JsonConvert.DeserializeObject<List<MembershipType>>(data);
            }
            NewCustomerViewModel newVModel = new NewCustomerViewModel()
            {
                membershipType = members
            };
            return View(newVModel);
        }

        public ActionResult DeleteCustomer(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/CustomerApi/DeleteCustomer?id=" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}