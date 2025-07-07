using System.Data;
using System;
using System.IO;

namespace MobileApp.Models
{
    public class MedDevice
    {
        public string device_id { get; set; }
        public string device_name { get; set; }
        public string device_manufacturer { get; set; }
        public string device_seri { get; set; }
        public string device_type { get; set; }
        public string device_group { get; set; }
        public int device_maintenance_cycle { get; set; }
        public DateTime? device_maintenance_start { get; set; } // Thay đổi ở đây
        public DateTime? device_stockout_date { get; set; }   // Thay đổi ở đây
        public string device_condition { get; set; }
        public DateTime? device_received_date { get; set; }    // Thay đổi ở đây
        public string device_note { get; set; }
        public string? FK_contact_id { get; set; }
        public string FK_status_id { get; set; }
        public string FK_room_id { get; set; }
        public string room_type { get; set; }
        public string contact_address { get; set; } // Thay đổi từ string sang byte[]
        public string room_name { get; set; }
        public string status_name { get; set; }
		public string contact_finance { get; set; }
		public string contact_type { get; set; }

		public string DisplayDeviceNameAndId
		{
			get
			{
				return $"{device_name} ({device_id})";
			}
		}

		public MedDevice() { }
    }
}