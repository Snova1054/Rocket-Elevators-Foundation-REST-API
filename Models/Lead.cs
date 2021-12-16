using System;

namespace RocketElevatorsRESTAPI.Models
{
    public class Lead
    {
        public int id { get; set; }
        public string full_name { get; set; }
        public string company_name { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string project_name { get; set; }
        public string project_description { get; set; }
        public string departement_in_charge_of_the_elevators { get; set; }
        public string message { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}