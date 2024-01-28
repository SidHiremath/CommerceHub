namespace Data;

public static class ConfigurationSeeder
{
    public static void Seed(DatabaseContext context)
    {
        if (context.ProductData.Any())
            return;

        context.ProductData.Add(new ProductData
        {
            Name = "product1",
            Price = 10.0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });
        context.SaveChanges();
    }
}