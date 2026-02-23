namespace ClientCore.Models
{
    public class Contact
    {
        [StringLength(100)]
        public required string Id { get; set; }
        [StringLength(200)]
        public required string Name { get; set; }
        [StringLength(200)]
        public required string Surname { get; set; }
        [StringLength(500)]
        public required string Email { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;

        public virtual ICollection<ClientContact> Clients { get; set; } = new List<ClientContact>();
    }
}
