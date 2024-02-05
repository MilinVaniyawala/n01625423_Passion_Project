using n01625423_Passion_Project.Models;
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
    public class HotelRoomController : Controller
    {
        
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        
        static HotelRoomController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44348/api/");
        }
        

        // GET: HotelRoom/List
        public ActionResult List()
        {
            // objective: communicate with our room data api to retrieve a list of rooms
            // curl https://localhost:44348/api/roomdata/listrooms

            string url = "roomdata/listrooms";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<RoomDto> rooms = response.Content.ReadAsAsync<IEnumerable<RoomDto>>().Result;

            return View(rooms);
        }

        
        // Get: HotelRoom/Details/2
        public ActionResult Details(int id)
        {
            // objective: communicate with our room data api to retrieve one room
            // curl https://localhost:44348/api/roomdata/findroom/{id}

            string url = "roomdata/findroom/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            RoomDto selectRoom = response.Content.ReadAsAsync<RoomDto>().Result;
            Debug.WriteLine("room data retrived : ");
            Debug.WriteLine(selectRoom.RoomNumber);

            return View(selectRoom);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: HotelRoom/Add
        public ActionResult Add()
        {
            // objective: communicate with our hotel data api to retrieve a list of hotels
            // curl https://localhost:44348/api/hoteldata/listhotels

            string url = "hoteldata/listhotels";
            
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<HotelDto> hotels = response.Content.ReadAsAsync<IEnumerable<HotelDto>>().Result;

            return View(hotels);
        }

        // POST: HotelRoom/Create
        [HttpPost]
        public ActionResult Create(Room room)
        {
            Debug.WriteLine("the room json load is : ");
            // Debug.WriteLine(room.RoomNumber);

            // objective: add a new room into our system using the API
            // curl -H "Content-Type:application/json" -d @room.json https://localhost:44348/api/roomData/addroom 

            string url = "roomdata/addroom";
            string jsonpayload = jss.Serialize(room);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        // GET: HotelRoom/Edit/2
        public ActionResult Edit(int id)
        {
            // Get Particular Room information

            // objective: communicate with our room data api to retrieve one room
            // curl https://localhost:44348/api/roomdata/findroom/{id}

            string url = "roomdata/findroom/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            RoomDto selectRoom = response.Content.ReadAsAsync<RoomDto>().Result;

            return View(selectRoom);
        }

        // POST: HotelRoom/Update/2
        [HttpPost]
        public ActionResult Update(int id, Room room)
        {
            try
            {
                Debug.WriteLine("The new room information is: ");
                Debug.WriteLine(room.RoomNumber);
                Debug.WriteLine(room.Type);
                Debug.WriteLine(room.Price);
                Debug.WriteLine(room.Status);
                Debug.WriteLine(room.HotelID);

                // serialize update roomdata into JSON
                // Send the request to the API
                // POST: api/RoomData/UpdateRoom/{id}
                // Header : Content-Type: application/json

                string url = "roomdata/updateroom/" + id;
                string jsonpayload = jss.Serialize(room);
                Debug.WriteLine(jsonpayload);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = client.PostAsync(url, content).Result;
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return View();
            }
        }

        // GET: HotelRoom/Delete/2
        public ActionResult DeleteRoom(int id)
        {
            // Get Particular Room information

            // objective: communicate with our room data api to retrieve one room
            // curl https://localhost:44348/api/roomdata/findroom/{id}

            string url = "roomdata/findroom/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            RoomDto selectRoom = response.Content.ReadAsAsync<RoomDto>().Result;

            return View(selectRoom);
        }

        // POST: HotelRoom/Delete/2
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                string url = "roomdata/deleteroom/" + id;
                HttpContent content = new StringContent("");
                content.Headers.ContentType.MediaType = "application/json";
                HttpResponseMessage response = client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
             catch(Exception ex) {
                Debug.WriteLine(ex.Message);
                return View();
            }
        }
        
    }
}