using System;
using System.Text.Json.Serialization;

namespace RocketElevatorsRESTAPI.Models
{
    public class Elevator
    {
        public int id { get; set; }
        public int serial_number { get; set; }
        public string model { get; set; }
        public string entity_type { get; set; }
        public string status { get; set; }
        public DateTime date_of_commissioning { get; set; }
        public DateTime date_of_last_inspection { get; set; }
        public string certificate_of_inspection { get; set; }
        public string information { get; set; }
        public string Notes { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int column_id { get; set; }
    }
}