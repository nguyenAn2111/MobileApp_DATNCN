﻿using System.Data;
using System;

namespace MobileApp.Models
{
    public class Contact
    {
        public string contact_id { get; set; }
        public int contact_type { get; set; }
        public string contact_address { get; set; }
        public int contact_finance { get; set; }
		public DateTime? contact_date { get; set; }

		public Contact(DataRow row)
        {
            this.contact_id = row["contact_id"] != DBNull.Value ? row["contact_id"].ToString() : "";
            contact_address = row["contact_address"] != DBNull.Value ? row["contact_address"].ToString() : "";
			//contact_type = row["contact_type"] != DBNull.Value ? row["room_type"].ToString() : "";
			
		}

		public Contact() { }
    }
}
