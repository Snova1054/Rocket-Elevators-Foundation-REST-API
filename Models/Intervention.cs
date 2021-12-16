using System;
using System.Text.Json.Serialization;

namespace RocketElevatorsRESTAPI.Models
{
    public class Intervention
    {
        public int id { get; set; }
        public string author { get; set; }
        public string result { get; set; }
        public string report { get; set; }
        public string status { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public int customer_id { get; set; }
        public int building_id { get; set; }
        public int battery_id { get; set; }
        public int? column_id {get; set;}
        public int? elevator_id { get; set; }
        public int? employee_id { get; set; }
    }
}
