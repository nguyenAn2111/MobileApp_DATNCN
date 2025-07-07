using System.Data;
using System;

namespace MobileApp.Models
{
    public class Storage
    {
        public string device_name { get; set; }
        public int str_quantity { get; set; }
        public int storage_id { get; set; }
        public DateTime storage_date { get; set; }
        public string FK_device_id { get; set; }
        public string FK_room_id_from { get; set; }
        public string FK_room_id_to { get; set; }

        public Storage() { }
      
    }
}
