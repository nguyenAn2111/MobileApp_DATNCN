using System.Data;
using System;

namespace MobileApp.Models
{
    public class Room
    {
        public string room_id { get; set; } //Tòa + số phòng
        public string room_type { get; set; } //Loại phòng
        public string room_name { get; set; }

		public string DisplayRoomName => $"{room_id} - {room_type}";

		public Room() { }
    }
}

