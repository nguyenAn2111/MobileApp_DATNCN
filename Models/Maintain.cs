using System.Data;
using System;


namespace MobileApp.Models
{
    public class Maintain
    {
        public string maintain_id { get; set; }
        public DateTime? maintain_date { get; set; }
        public string maintain_maintenance { get; set; }
        public string maintain_maintenance_phone { get; set; }
        public string maintain_delivery { get; set; }
        public string maintain_delivery_phone { get; set; }
        public string FK_device_id { get; set; }
        public string FK_status_id { get; set; }
        public string FK_room_id { get; set; }
        public string FK_finace_id { get; set; }
        public string FK_contact_id { get; set; }
        public string device_id { get; set; }
        public string device_name { get; set; }
        public string room_name { get; set; }
        public string status_name { get; set; }
		public string contact_finance { get; set; }
		public string contact_address { get; set; }
		public DateTime? device_maintenance_start { get; set; }
		public int? device_maintenance_cycle { get; set; }
		public DateTime? expiration_date { get; set; }
		public string DeviceStatusId { get; set; }

		public Maintain() { }
	}

}
