using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace n01625423_Passion_Project.Models
{
    public class Room
    {
        // Room ID is primary key for Hotel Datatable
        [Key]
        public int RoomID { get; set; }
        // Hotel ID is primary key in Hotel Datatable but in Room Datatable it is foreign key.
        // However it represents one to many relationship
        // Ex. XYZ hotel have n Numbers of Rooms.
        [ForeignKey("Hotels")]
        public int HotelID { get; set; }
        public virtual Hotel Hotels { get; set; }
        public string RoomNumber {  get; set; }
        public string Type { get; set;}
        public decimal Price { get; set;}
        public string Status { get; set;}
    }

    public class RoomDto
    {
        public int RoomID { get; set; }
        public string RoomNumber { get; set;}
        public string Type { get; set;}
        public decimal Price { get; set;}
        public string Status { get; set;}
        public string HotelName { get; set;}
        public int HotelID { get; set;}
    }
}