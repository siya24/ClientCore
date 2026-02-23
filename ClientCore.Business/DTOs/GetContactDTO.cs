namespace ClientCore.Business.DTOs
{
    public class GetContactDTO
    {
        public required string Id { get; set; }
        public required string FullName { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public int TotalClients { get; set; }


    }
}
