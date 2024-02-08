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
    public class HotelReservationController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static HotelReservationController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44348/api/");
        }

        // GET: HotelReservation/BookingList
        public ActionResult BookingList()
        {
            // objective: communicate with our reservation data api to retrieve a list of reservations
            // curl https://localhost:44348/api/reservationdata/listreservations

            string url = "reservationdata/listreservations";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<ReservationDto> reservations = response.Content.ReadAsAsync<IEnumerable<ReservationDto>>().Result;

            return View(reservations);
        }

        // GET: HotelReservation/ViewReservation/2
        public ActionResult ViewReservation(int id)
        {
            // objective: communicate with our reservation data api to retrieve one reservation
            // curl https://localhost:44348/api/reservationdata/findreservation/{id}

            string url = "reservationdata/findreservation/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            Debug.WriteLine(response.StatusCode);

            ReservationDto selectReservation = response.Content.ReadAsAsync<ReservationDto>().Result;
            
            Debug.WriteLine(selectReservation);
            Debug.WriteLine("#####Hello World!!#########");
            return View(selectReservation);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: HotelReservation/AddReservation
        public ActionResult AddReservation()
        {
            // objective: communicate with our room data api to retrieve a list of rooms
            // curl https://localhost:44348/api/roomdata/listrooms

            string url = "roomdata/listrooms";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<RoomDto> rooms = response.Content.ReadAsAsync<IEnumerable<RoomDto>>().Result;

            // objective: communicate with our user data api to retrieve a list of users
            // curl https://localhost:44348/api/userdata/listusers

            string urlUser = "userdata/listusers";
            HttpResponseMessage responseUser = client.GetAsync(urlUser).Result;

            IEnumerable<UserDto> users = responseUser.Content.ReadAsAsync<IEnumerable<UserDto>>().Result;

            var viewModel = new AddReservationViewModel
            {
                Rooms = rooms,
                Users = users
            };

            return View(viewModel);
        }

        // POST: HotelReservation/CreateReservation
        [HttpPost]
        public ActionResult CreateReservation(Reservation reservation)
        {
            string url = "reservationdata/addreservation";
            string jsonpayload = jss.Serialize(reservation);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage responseMessage = client.PostAsync(url,content).Result; 
            if(responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("BookingList");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: HotelReservation/EditReservation/3
        public ActionResult EditReservation(int id)
        {
            // objective: communicate with our reservation data api to retrieve one reservation
            // curl https://localhost:44348/api/reservationdata/findreservation/{id}

            string url = "reservationdata/findreservation/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            ReservationDto selectReservation = response.Content.ReadAsAsync<ReservationDto>().Result;

            return View(selectReservation);
        }

        // POST: HotelReservation/UpdateReservation/3
        [HttpPost]
        public ActionResult UpdateReservation(int id,Reservation reservation)
        {
            try
            {
                Debug.WriteLine("Reservation Details Check: ");
                Debug.WriteLine(reservation.CheckInDate);
                Debug.WriteLine(reservation.CheckOutDate);
                Debug.WriteLine(reservation.Status);

                // serialize update reservationData into JSON
                // Send the request to the API
                // POST: api/reservationdata/updatereservation/{id}
                // Header : Content-Type: application/json

                string url = "reservationdata/updatereservation/" + id;
                string jsonpayoad = jss.Serialize(reservation);
                Debug.WriteLine(jsonpayoad);

                HttpContent content = new StringContent(jsonpayoad);
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage responseMessage = client.PostAsync(url, content).Result;
                return RedirectToAction("BookingList");
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return View();
            }
        }

        // GET: HotelReservation/DeleteReservation/3
        public ActionResult DeleteReservation(int id)
        {
            // Get Particular Reservation information

            // objective: communicate with our reservation data api to retrieve one reservation
            // curl https://localhost:44348/api/reservationdata/findreservation/{id}

            string url = "reservationdata/findreservation/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            ReservationDto selectReservation = response.Content.ReadAsAsync<ReservationDto>().Result;

            return View(selectReservation);
        }

        // POST: HotelReservation/Delete/3
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                string url = "reservationdata/deletereservation/" + id;
                HttpContent content = new StringContent("");

                content.Headers.ContentType.MediaType = "application/json";
                HttpResponseMessage responseMessage = client.PostAsync(url, content).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("BookingList");
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return View();
            }
        }
    }
}