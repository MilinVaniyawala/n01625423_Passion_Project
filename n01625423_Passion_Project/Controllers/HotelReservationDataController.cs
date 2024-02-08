using n01625423_Passion_Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace n01625423_Passion_Project.Controllers
{
    public class HotelReservationDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ReservationData/ListReservations
        [HttpGet]
        [Route("api/ReservationData/ListReservations")]
        public IEnumerable<ReservationDto> ListReservations()
        {
            List <Reservation> Reservations = db.Reservations.ToList();
            List <ReservationDto> ReservationDtos = new List<ReservationDto>();

            Reservations.ForEach(r => ReservationDtos.Add(new ReservationDto()
            {
                ReservationID = r.ReservationID,
                UserID = r.Users.UserID,
                Username = r.Users.Username,
                RoomID = r.Rooms.RoomID,
                RoomNumber = r.Rooms.RoomNumber,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                Status = r.Status
            }));

            return ReservationDtos;
        }

        // GET: api/ReservationData/FindReservation/3
        [ResponseType(typeof(Reservation))]
        [HttpGet]
        [Route("api/ReservationData/FindReservation/{reservationId}")]
        public IHttpActionResult FindReservation(int reservationId)
        {
            Reservation res = db.Reservations.Find(reservationId);
            ReservationDto reservationDto = new ReservationDto() {
                ReservationID = res.ReservationID,
                UserID = res.Users.UserID,
                Username = res.Users.Username,
                RoomID = res.Rooms.RoomID,
                RoomNumber = res.Rooms.RoomNumber,
                CheckInDate = res.CheckInDate,
                CheckOutDate = res.CheckOutDate,
                Status = res.Status
            };
            if(reservationDto == null)
            {
                return NotFound(); // HTTP Status COde 404
            }
            return Ok(reservationDto); // return reservation Object
        }

        // POST: api/ReservationData/AddReservation
        [ResponseType(typeof (Reservation))]
        [HttpPost]
        [Route("api/ReservationData/AddReservation")]
        public IHttpActionResult AddReservation(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Reservations.Add(reservation);
            db.SaveChanges();

            return Ok();
        }

        // POST: api/ReservationData/UpdateReservation/3
        [ResponseType(typeof(Reservation))]
        [HttpPost]
        [Route("api/ReservationData/UpdateReservation/{id}")]
        public IHttpActionResult UpdateReservation(int id, Reservation reservation)
        {
            Debug.WriteLine("Is it reached to Update Reservation Method!!!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("State is not Valid For Reservation Model!!!");
                return BadRequest(ModelState);
            }
            if(id != reservation.ReservationID)
            {
                Debug.WriteLine("Reservation ID is not Match!!");
            }

            db.Entry(reservation).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }catch(DbUpdateConcurrencyException ex)
            {
                if (!ReservationExists(id))
                {
                    Debug.WriteLine("Reservation details not found!!");
                    return NotFound();
                }
                else
                {
                    Debug.WriteLine(ex);
                    throw;
                }
            }
            Debug.WriteLine("Nothing is trigger in update reservation function!!");
            return (StatusCode(HttpStatusCode.NoContent));
        }

        // POST: api/ReservationData/DeleteReservation/3
        [ResponseType(typeof(Reservation))]
        [HttpPost]
        [Route("api/ReservationData/DeleteReservation/{id}")]
        public IHttpActionResult DeleteReservation(int id)
        {
            Reservation res = db.Reservations.Find(id);
            if(res == null)
            {
                return NotFound();
            }
            db.Reservations.Remove(res);
            db.SaveChanges();

            return Ok();
        }

        // Check Is Booking(Reservation) Already Exists and Return Boolean Value Either True Or False
        private bool ReservationExists(int id)
        {
            return db.Reservations.Count(e => e.ReservationID == id) > 0;
        }
    }
}
