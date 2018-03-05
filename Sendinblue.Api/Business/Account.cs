using System;

namespace Sendinblue.Api.Business
{
    public class Account
    {
        public PlanItem[] Plan { get; set; }
        public Relay Relay { get; set; }
        public Marketing MarketingAutomation { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public Address Address { get; set; }
    }

    public class Address{
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }

    public class Marketing{
        public string Key { get; set; }
        public bool Enabled { get; set; }
    }

    public class Relay { 
        public bool Enabled { get; set; }
        public DataRelay Data { get; set; }
    }

    public class DataRelay {
        public string UserName { get; set; }
        public string Relay { get; set; }
        public int? Port { get; set; }
    }

    public class PlanItem { 
        public string Type { get; set; }
        public decimal? Credits { get; set; }
        public string CreditsType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
