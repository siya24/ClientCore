namespace ClientCore.Models
{
    public class ClientContact
    {
        [StringLength(100)]
        public required string Id { get; set; }
        public required string ClientId { get; set; }
        public required string ContactId { get; set; }
        public virtual Contact? Contact { get; set; }
        public virtual Client? Client { get; set; }
    }
}
