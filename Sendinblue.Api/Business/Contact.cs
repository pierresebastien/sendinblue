using System;
using System.Collections.Generic;

namespace Sendinblue.Api.Business
{
    public class Contact
    {
        public string Email { get; set; }
        public int Id { get; set; }
        public bool EmailBlacklisted { get; set; }
        public bool SmsBlacklisted { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int[] ListIds { get; set; }
        public ContactAttributes Attributes { get; set; }
        public Dictionary<string, dynamic> Statistics { get; set; }
    }

    public class ContactsResult
    {
        public Contact[] Contacts { get; set; }
    }

    public class ContactAttributes
    {
        public string SMS { get; set; }
        public string Identification { get; set; }
        public string Civ { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime? Dob { get; set; }
        public string Address { get; set; }
        public string Zip_Code { get; set; }
        public string City { get; set; }
        public string Action_Code { get; set; }
    }

    public class ContactStatistic
    {
        public string CampaignId { get; set; }
        public DateTime? EventTime { get; set; }
        public int Count { get; set; }
        public string Ip { get; set; }

    }

    public class Link
    {
        public int Count { get; set; }
        public DateTime? EventTime { get; set; }
        public string Ip { get; set; }
        public string Url { get; set; }
    }

}
