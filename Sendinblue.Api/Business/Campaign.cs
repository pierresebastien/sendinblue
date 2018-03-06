using System;
using System.Collections.Generic;

namespace Sendinblue.Api.Business
{
    public class Campaign
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Type { get; set; }
        public bool TestSent { get; set; }
        public string Header { get; set; }
        public string Footer { get; set; }
        public Sender Sender { get; set; }
        public string ReplyTo { get; set; }
        public string ToField { get; set; }
        public string HtmlContent { get; set; }
        public string Tag { get; set; }
        public bool InlineImageActivation { get; set; }
        public bool MirrorActive { get; set; }
        public Recipient Recipients { get; set; }
        public CampaignStatistic Statistics { get; set; }
        public DateTime? ScheduledAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ShareLink { get; set; }
    }

    public class CampaignResult
    {
        public int Count { get; set; }
        public Campaign[] Campaigns { get; set; }
    }

    public class Recipient
    {
        public List<string> Lists { get; set; }
        public List<string> ExclusionLists { get; set; }
    }

    public class CampaignStatistic
    {
        public CampaignStat[] CampaignStats { get; set; }
        public int MirrorClick { get; set; }
        public int Remaining { get; set; }
        public Dictionary<string, int> LinksStats { get; set; }
        public Dictionary<string, StatByDomain> StatsByDomain { get; set; }
    }

    public class CampaignStat
    {
        public int ListId { get; set; }
        public int UniqueClicks { get; set; }
        public int Clickers { get; set; }
        public int Complaints { get; set; }
        public int Delivered { get; set; }
        public int Sent { get; set; }
        public int SoftBounces { get; set; }
        public int HardBounces { get; set; }
        public int UniqueViews { get; set; }
        public int Unsubscriptions { get; set; }
        public int Viewed { get; set; }
        public int Deferred { get; set; }
    }

    public class StatByDomain
    {
        public int UniqueClicks { get; set; }
        public int Clickers { get; set; }
        public int Complaints { get; set; }
        public int Sent { get; set; }
        public int SoftBounces { get; set; }
        public int HardBounces { get; set; }
        public int UniqueViews { get; set; }
        public int Unsubscriptions { get; set; }
        public int Viewed { get; set; }
        public int Delivered { get; set; }
    }
}