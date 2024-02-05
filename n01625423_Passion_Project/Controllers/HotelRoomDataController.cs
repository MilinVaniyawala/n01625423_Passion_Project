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
    public class HotelRoomDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/RoomData/ListRooms
        [HttpGet]
        [Route("api/RoomData/ListRooms")]
        public IEnumerable<RoomDto> ListRooms()
        {
            List<Room> Rooms = db.Rooms.ToList();
            List<RoomDto> RoomDtos = new List<RoomDto>();

            Rooms.ForEach(room => RoomDtos.Add(new RoomDto() {
                RoomID = room.RoomID,
                RoomNumber = room.RoomNumber,
                Type = room.Type,
                Price = room.Price,
                Status = room.Status,
                HotelName = room.Hotels.Name
            }));

            return RoomDtos;
        }

        // GET: api/RoomData/FindRoom/2
        [ResponseType(typeof(Room))]
        [HttpGet]
        [Route("api/RoomData/FindRoom/{roomId}")]
        public IHttpActionResult FindRoom(int roomId)
        {
            Room room = db.Rooms.Find(roomId);
            RoomDto roomDto = new RoomDto()
            {
                RoomID = room.RoomID,
                RoomNumber = room.RoomNumber,
                Type = room.Type,
                Price = room.Price,
                Status = room.Status,
                HotelName = room.Hotels.Name,
                HotelID = room.Hotels.HotelID
            };
            if (roomDto == null)
            {
                return NotFound(); // HTTP Status Code 404
            }
            return Ok(roomDto); // return Room Object
        }

        // POST: api/RoomData/AddRoom
        [ResponseType(typeof (Room))]
        [HttpPost]
        [Route("api/RoomData/AddRoom")]
        public IHttpActionResult AddRoom(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Rooms.Add(room);
            db.SaveChanges();

            return Ok();
        }

        // POST: api/RoomData/UpdateRoom/2
        [ResponseType(typeof(Room))]
        [HttpPost]
        [Route("api/RoomData/UpdateRoom/{id}")]
        public IHttpActionResult UpdateRoom(int id, Room room)
        {
            Debug.WriteLine("Check!! Is It Reached to the update method or not!!");
            // curl -H "Content-Type:application/json" -d @room.json https://localhost:44348/api/roomData/updateroom/6
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("State is not valid for this Model");
                return BadRequest (ModelState);
            }

            if(id != room.RoomID)
            {
                Debug.WriteLine("ID is not match!!");
                Debug.WriteLine("Fetch ID: " + id);
                Debug.WriteLine("POST Parameter - Room ID: " + room.RoomID);
                Debug.WriteLine("POST Parameter - Room Number: " + room.RoomNumber);
                Debug.WriteLine("POST Parameter - Type Of Room: " + room.Type);
                Debug.WriteLine("POST Parameter - Price of Room: " + room.Price);
                Debug.WriteLine("POST Parameter - Status of Room: " + room.Status);
                Debug.WriteLine("POST Parameter - Hotel Name of that Room: " + room.Hotels.Name);
                Debug.WriteLine("POST Parameter - Hotel ID of that Room: " + room.Hotels.HotelID);
            }

            db.Entry(room).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch(DbUpdateConcurrencyException e)
            {
                if (!RoomExists(id))
                {
                    Debug.WriteLine("Room not found");
                    return NotFound();
                }
                else
                {
                    Debug.WriteLine(e);
                    throw;
                }
            }
            Debug.WriteLine("Nothing is trigger in update function !!");
            return (StatusCode(HttpStatusCode.NoContent));
        }

        // POST: api/RoomData/DeleteRoom/2
        [ResponseType(typeof(Room))]
        [HttpPost]
        [Route("api/RoomData/DeleteRoom/{id}")]
        public IHttpActionResult DeleteRoom(int id)
        {
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }
            db.Rooms.Remove(room);
            db.SaveChanges();

            return Ok();
        }

        // Check Is Room Already Exists and return boolean value either true or false
        private bool RoomExists(int id)
        {
            return db.Rooms.Count(e => e.RoomID == id) > 0;
        }
    }
}
