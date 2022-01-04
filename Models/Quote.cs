using System;

namespace RocketElevatorsRESTAPI.Models
{
    public class Quote
    {
        public int id { get; set; }
        public string email { get; set; }
        public string company_name { get; set; }
        public string building_type { get; set; }
        public int appartement { get; set; }
        public int floor { get; set; }
        public int basement { get; set; }
        public string plan { get; set; }
        public int business { get; set; }
        public int parking { get; set; }
        public int cages { get; set; }
        public int occupant { get; set; }
        public int elevator_needed { get; set; }
        public string price { get; set; }
        public string fees { get; set; }
        public string total_price { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}