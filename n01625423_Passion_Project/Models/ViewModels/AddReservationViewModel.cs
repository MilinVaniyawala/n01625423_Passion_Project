using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace n01625423_Passion_Project.Models.ViewModels
{
    public class AddReservationViewModel
    {
        public IEnumerable<RoomDto> Rooms { get; set; }
        public IEnumerable<UserDto> Users { get; set; }
    }
}