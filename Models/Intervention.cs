using System;

namespace RocketElevatorsRESTAPI.Models
{
    public class Intervention
    {
        public int id { get; set; }
        public string author { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public string result { get; set; }
        public string report { get; set; }
        public string status { get; set; }
        public int customer_id { get; set; }
        public int building_id { get; set; }
        public int battery_id { get; set; }
        public Nullable<int> column_id { get; set; }
        public Nullable<int> elevator_id { get; set; }
        public Nullable<int> employee_id { get; set; }
    }
}
