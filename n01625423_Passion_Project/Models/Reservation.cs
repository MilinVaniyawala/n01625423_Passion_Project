using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace n01625423_Passion_Project.Models
{
    public class Reservation
    {
        // Reservation ID is primary key for Reservation Datatable
        [Key]
        public int ReservationID { get; set; }
        // User ID is primary key in User table but in Reservation table it is foreign key.
        // However it represents one to many relationship
        // Ex. ABC user have n Numbers of Reservation.
        [ForeignKey("Users")]
        public int UserID { get; set; }
        public virtual User Users { get; set; }
        // Room ID is primary key in Room table but in Reservation table it is foreign key.
        // However it represents one to many relationship
        // Ex. PQR room have n Numbers of Reservation on different dates or different time.
        [ForeignKey("Rooms")]
        public int RoomID { get; set; }
        public virtual Room Rooms { get; set; }
        public DateTime CheckInDate {  get; set; }
        public DateTime CheckOutDate {  get; set; }
        public string Status {  get; set; }
    }
}