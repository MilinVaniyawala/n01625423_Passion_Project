using n01625423_Passion_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace n01625423_Passion_Project.Controllers
{
    public class HotelUserDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/UserData/ListUsers
        [HttpGet]
        [Route("api/UserData/ListUsers")]
        public IEnumerable<UserDto> ListUsers()
        {
            List<User> Users = db.Users.ToList();
            List<UserDto> UserDtos = new List<UserDto>();

            Users.ForEach(user => UserDtos.Add(new UserDto()
            {
                UserID = user.UserID,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
            }));

            return UserDtos;
        }
    }
}
