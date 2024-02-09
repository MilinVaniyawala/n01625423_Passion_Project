using n01625423_Passion_Project.Models;
using n01625423_Passion_Project.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace n01625423_Passion_Project.Controllers
{
    public class HotelUserController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static HotelUserController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44348/api/");
        }

        // GET: HotelUser/UserList
        public ActionResult UserList()
        {
            // objective: communicate with our user data api to retrieve a list of users
            // curl https://localhost:44348/api/userdata/listusers

            string url = "userdata/listusers";
            HttpResponseMessage responseMessage = client.GetAsync(url).Result;

            IEnumerable<UserDto> users = responseMessage.Content.ReadAsAsync<IEnumerable<UserDto>>().Result;

            return View(users);
        }

        // GET: HotelUser/ViewUser/2
        public ActionResult ViewUser(int id)
        {
            // objective: communicate with our user data api to retrieve one user
            // curl https://localhost:44348/api/userdata/finduser/{id}

            string url = "userdata/finduser/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            Debug.WriteLine(response.StatusCode);

            UserDto selectUser = response.Content.ReadAsAsync<UserDto>().Result;

            Debug.WriteLine(selectUser);
            Debug.WriteLine("#####Hello World --- User!!#########");
            return View(selectUser);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: HotelUser/AddUser
        public ActionResult AddUser()
        {
            return View();
        }

        // POST: HotelUser/CreateUser
        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            string url = "userdata/adduser";
            string jsonpayload = jss.Serialize(user);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage responseMessage = client.PostAsync(url, content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("UserList");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: HotelUser/EditUser/2
        public ActionResult EditUser(int id)
        {
            // objective: communicate with our user data api to retrieve one user
            // curl https://localhost:44348/api/userdata/finduser/{id}

            string url = "userdata/finduser/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            UserDto selectUser = response.Content.ReadAsAsync<UserDto>().Result;

            return View(selectUser);
        }

        // POST: HotelUser/UpdateUser/2
        [HttpPost]
        public ActionResult UpdateUser(int id, User user)
        {
            try
            {
                Debug.WriteLine("User Details Check: ");
                Debug.WriteLine(user.UserID);
                Debug.WriteLine(user.Username);
                Debug.WriteLine(user.Email);

                // serialize update userData into JSON
                // Send the request to the API
                // POST: api/userdata/updateuser/{id}
                // Header : Content-Type: application/json

                string url = "userdata/updateuser/" + id;
                string jsonpayoad = jss.Serialize(user);
                Debug.WriteLine(jsonpayoad);

                HttpContent content = new StringContent(jsonpayoad);
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage responseMessage = client.PostAsync(url, content).Result;
                return RedirectToAction("UserList");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return View();
            }
        }

        // GET: HotelUser/DeleteUser/2
        public ActionResult DeleteUser(int id)
        {
            // Get Particular User information

            // objective: communicate with our user data api to retrieve one user
            // curl https://localhost:44348/api/userdata/finduser/{id}

            string url = "userdata/finduser/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            UserDto selectUser = response.Content.ReadAsAsync<UserDto>().Result;

            return View(selectUser);
        }

        // POST: HotelUser/Delete/2
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                string url = "userdata/deleteuser/" + id;
                HttpContent content = new StringContent("");

                content.Headers.ContentType.MediaType = "application/json";
                HttpResponseMessage responseMessage = client.PostAsync(url, content).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("UserList");
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return View();
            }
        }
    }
}