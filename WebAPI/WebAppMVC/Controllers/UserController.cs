using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using WebAppMVC.Models;
using Newtonsoft.Json;


namespace WebAppMVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User

        Uri baseAddress = new Uri("https://localhost:44328/api/");
        HttpClient client;

        public UserController()
        {

            client = new HttpClient();

            client.BaseAddress = baseAddress;

        }



        public ActionResult Index()
        {

            List<UserViewModel> modelList = new List<UserViewModel>();

            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/user").Result;
            

            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                modelList=JsonConvert.DeserializeObject<List<UserViewModel>>(data);

            }
            return View(modelList);
        }



        public ActionResult Create()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Create(UserViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/user", content).Result;

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }


            return View();
        }


        public ActionResult Edit(int id)
        {


            UserViewModel model = new UserViewModel();

            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/user/"+id).Result;


            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                model = JsonConvert.DeserializeObject<UserViewModel>(data);

            }
            return View("Create",model);

           
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/user/"+model.UserId, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }


            return View("Create",model);
        }



        public ActionResult Delete(int id)
        {

            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/user/" + id).Result;


            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");


        }







    }
}