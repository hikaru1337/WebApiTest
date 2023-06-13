public class TusaDb : DbContext
{
    public TusaDb(DbContextOptions<TusaDb> options) : base(options) { }
    public DbSet<User> User { get; set; }
}
