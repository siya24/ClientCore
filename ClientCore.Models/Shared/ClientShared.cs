namespace ClientCore.Models.Shared
{
    public abstract class ClientShared
    {
        [StringLength(500)]
        public required string Name { get; set; }

    }
}
