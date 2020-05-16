<Query Kind="Program">
  <Connection>
    <ID>f2f6e2b4-0f40-48df-b2c7-82584a77c394</ID>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AttachFile>true</AttachFile>
    <Server>(localdb)\MSSQLLocalDB</Server>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Nutshell.mdf</AttachFileName>
    <DisplayName>Nutshell</DisplayName>
    <Persist>true</Persist>
    <DriverData>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	// LINQPad enables lazy loading automatically, so to demonstrate life without
	// lazy loading, we've created our own typed DbContext class below:  
	using var dbContext = new NutshellContext();
	
	var cust = dbContext.Customers.First();
	Console.WriteLine (cust.Purchases?.Count ?? 0);    // Always 0

	cust = dbContext.Customers
		.Include (c => c.Purchases)
		.Where (c => c.ID == 2)
		.First()
		.Dump ("Using Include");

	var custInfo = dbContext.Customers
	 .Where (c => c.ID == 2)
	 .Select (c => new
	 {
		 Name = c.Name,
		 Purchases = c.Purchases.Select (p => new { p.Description, p.Price })
	 })
	 .First()
	 .Dump ("Using a projection");
	 
	// Yet another solution:
	dbContext.Entry (cust).Collection (b => b.Purchases).Load();
	// cust.Purchases is now populated.
}

public class NutshellContext : DbContext
{
	public DbSet<Customer> Customers { get; set; }
	public DbSet<Purchase> Purchases { get; set; }

	protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer (Util.CurrentCxString);
	}

	protected override void OnModelCreating (ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Customer> (entity =>
		 {
			 entity.ToTable ("Customer");
			 entity.Property (e => e.Name).IsRequired();  // Column is not nullable
		 });
		modelBuilder.Entity<Purchase> (entity =>
		 {
			 entity.ToTable ("Purchase");
			 entity.Property (e => e.Date).IsRequired();
			 entity.Property (e => e.Description).IsRequired();
		 });
	}
}