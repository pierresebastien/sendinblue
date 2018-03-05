namespace Sendinblue.Api.Business
{
    public class SenderResult
    {
        public Sender[] Senders { get; set; }

    }

    public class Sender
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public IpSender[] Ips { get; set; }
    }

    public class CreateSenderResult
    {
        public int Id { get; set; }
        public bool SpfError { get; set; }
        public bool DkimError { get; set; }
    }

    public class IpsResult
    {
        public IpSender[] Ips { get; set; }
    }

    public class IpSender
    {
        public string Ip { get; set; }
        public string Domain { get; set; }
        public int Weight { get; set; }
    }
}
