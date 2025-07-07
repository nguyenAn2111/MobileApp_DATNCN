using System.Data;
using System;


namespace MobileApp.Models
{
    public class Repair
    {
        public string repair_id { get; set; }
        public DateTime repair_broken { get; set; }
        public int repair_priority { get; set; }
        public DateTime repair_date { get; set; }
        public DateTime repair_update_date { get; set; }
        public string repair_update_status { get; set; }
        public string repair_note { get; set; }
        public string repair_picture { get; set; }
        public string repair_update_note { get; set; }
        public string FK_contact_id { get; set; }
        public string FK_room_id { get; set; }
        public string FK_device_id { get; set; }
        public string FK_status_id { get; set; }
        public string device_name { get; set; }
        public string room_name { get; set; }
        public string status_name { get; set; }
        public string contact_finance { get; set; } // sửa int thành string
        public string contact_address { get; set; }
		public string device_id { get; set; } //thêm dòng này

		public Repair() { }

	}
}