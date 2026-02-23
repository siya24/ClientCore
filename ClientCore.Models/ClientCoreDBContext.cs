namespace ClientCore.Models;

public class ClientCoreDBContext(DbContextOptions<ClientCoreDBContext> options) : DbContext(options)
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<ClientContact> ClientContacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

      
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.HasIndex(c => c.Code).IsUnique();
            entity.Property(c => c.Id).HasMaxLength(100);
            entity.Property(c => c.Code).HasMaxLength(6);
        });


        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).HasMaxLength(100);
            entity.Property(c => c.Name).HasMaxLength(200);
            entity.Property(c => c.Surname).HasMaxLength(200);
            entity.Property(c => c.Email).HasMaxLength(500);
        });

   
        modelBuilder.Entity<ClientContact>(entity =>
        {
            entity.HasKey(cc => cc.Id);
            entity.Property(cc => cc.Id).HasMaxLength(100);

           
            entity.HasOne(cc => cc.Client)
                .WithMany(c => c.Contacts) 
                .HasForeignKey(cc => cc.ClientId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            
            entity.HasOne(cc => cc.Contact)
                .WithMany(c => c.Clients) 
                .HasForeignKey(cc => cc.ContactId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();


            entity.HasIndex(cc => cc.ClientId);
            entity.HasIndex(cc => cc.ContactId);
            entity.HasIndex(cc => new { cc.ClientId, cc.ContactId }).IsUnique();
        });

        base.OnModelCreating(modelBuilder);

    }
}
