using System;

namespace RocketElevatorsRESTAPI.Models
{
    public class Column
    {
        public int id { get; set; }
        public string entity_type { get; set; }
        public int number_of_floors_served { get; set; }
        public string status { get; set; }
        public string information { get; set; }
        public string notes { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int battery_id { get; set; }
    }
}