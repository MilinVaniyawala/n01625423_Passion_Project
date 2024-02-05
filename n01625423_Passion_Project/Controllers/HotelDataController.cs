using n01625423_Passion_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace n01625423_Passion_Project.Controllers
{
    public class HotelDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/HotelData/ListHotels
        [HttpGet]
        [Route("api/HotelData/ListHotels")]
        public IEnumerable<HotelDto> ListHotels()
        {
            List<Hotel> Hotels = db.Hotels.ToList();
            List<HotelDto> HotelDtos = new List<HotelDto>();

            Hotels.ForEach(hotel => HotelDtos.Add(new HotelDto()
            {
                HotelID = hotel.HotelID,
                Name = hotel.Name,
                Location = hotel.Location,
                Amenities = hotel.Amenities
            }));

            return HotelDtos;
        }
    }
}