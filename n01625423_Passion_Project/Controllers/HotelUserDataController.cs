using n01625423_Passion_Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace n01625423_Passion_Project.Controllers
{
    public class HotelUserDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: All users in the database, including their usernames, passwords, and emails.
        /// </returns>
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

        /// <summary>
        /// Retrieves information about a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: The user information, including their username, password, and email.
        /// </returns>
        // GET: api/UserData/FindUser/2
        [ResponseType(typeof(User))]
        [HttpGet]
        [Route("api/UserData/FindUser/{userId}")]
        public IHttpActionResult FindUser(int userId)
        {
            User user = db.Users.Find(userId);
            UserDto userDto = new UserDto()
            {
                UserID = user.UserID,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
            };
            if (userDto == null)
            {
                return NotFound(); // HTTP Status COde 404
            }
            return Ok(userDto); // return user Object
        }

        /// <summary>
        /// Adds a new user to the system.
        /// </summary>
        /// <param name="user">The user object containing the information to add.</param>
        /// <returns>
        /// HEADER: 200 (OK) if the user is added successfully.
        /// </returns>
        /// <remarks>
        /// The user object must contain valid data for the user's properties, such as username, email, and password.
        /// </remarks>
        // POST: api/UserData/AddUser
        [ResponseType(typeof(User))]
        [HttpPost]
        [Route("api/UserData/AddUser")]
        public IHttpActionResult AddUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Users.Add(user);
            db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Updates the information of an existing user in the system.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="user">The updated user object with new information.</param>
        /// <returns>
        /// HEADER: 204 (No Content) if the user is updated successfully.
        /// </returns>
        /// <remarks>
        /// The user object must contain valid data for the user's properties, such as username, email, and password.
        /// </remarks>
        // POST: api/UserData/UpdateUser/2
        [ResponseType(typeof(User))]
        [HttpPost]
        [Route("api/UserData/UpdateUser/{id}")]
        public IHttpActionResult UpdateUser(int id, User user)
        {
            Debug.WriteLine("Is it reached to Update User Method!!!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("State is not Valid For User Model!!!");
                return BadRequest(ModelState);
            }
            if (id != user.UserID)
            {
                Debug.WriteLine("User ID is not Match!!");
            }

            db.Entry(user).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!UserExists(id))
                {
                    Debug.WriteLine("User details not found!!");
                    return NotFound();
                }
                else
                {
                    Debug.WriteLine(ex);
                    throw;
                }
            }
            Debug.WriteLine("Nothing is trigger in update user function!!");
            return (StatusCode(HttpStatusCode.NoContent));
        }

        /// <summary>
        /// Deletes a user from the system by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>
        /// HEADER: 200 (OK) if the user is deleted successfully.
        /// HEADER: 404 (Not Found) if the user with the given ID is not found.
        /// </returns>
        // POST: api/UserData/DeleteUser/2
        [ResponseType(typeof(User))]
        [HttpPost]
        [Route("api/UserData/DeleteUser/{id}")]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            db.Users.Remove(user);
            db.SaveChanges();

            return Ok();
        }

        // Check Is User Already Exists and Return Boolean Value Either True Or False
        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserID == id) > 0;
        }
    }
}
