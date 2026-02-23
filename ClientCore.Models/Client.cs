namespace ClientCore.Models
{
    public class Client : ClientShared
    {
        [StringLength(100)]
        public required string Id { get; set; }
        [StringLength(6)]
        public required string Code { get; set; }

        public virtual ICollection<ClientContact> Contacts { get; set; } = new List<ClientContact>();
    }
}
