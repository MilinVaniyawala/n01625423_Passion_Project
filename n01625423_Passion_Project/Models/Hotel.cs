using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace n01625423_Passion_Project.Models
{
    public class Hotel
    {
        // Key - Represents Primary Key Of table
        // Hotel ID is primary key for Hotel Datatable
        [Key]
        public int HotelID {  get; set; }
        public string Name {  get; set; }
        public string Location {  get; set; }
        public string Amenities {  get; set; }
    }

    public class HotelDto
    {
        public int HotelID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Amenities { get; set; }
    }
}