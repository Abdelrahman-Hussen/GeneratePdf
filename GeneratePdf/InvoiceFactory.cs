using Bogus;

namespace GeneratePdf
{
    public class InvoiceFactory
    {
        public Invoice Create()
        {
            var faker = new Faker();

            var invoice = new Invoice
            {
                Number = faker.Random.Number(100_000, 1_000_000).ToString(),
                IssuedDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DueDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10)),
                SellerAddress = new Address
                {
                    CompanyName = faker.Company.CompanyName(),
                    Street = faker.Address.StreetName(),
                    City = faker.Address.City(),
                    State = faker.Address.State(),
                    Email = faker.Internet.Email(),
                },
                CustomerAddress = new Address
                {
                    CompanyName = faker.Company.CompanyName(),
                    Street = faker.Address.StreetName(),
                    City = faker.Address.City(),
                    State = faker.Address.State(),
                    Email = faker.Internet.Email(),
                },
                LineItems = Enumerable
                    .Range(1, 10)
                    .Select(i => new LineItem
                    {
                        Id = i,
                        Name = faker.Commerce.ProductName(),
                        Price = faker.Random.Decimal(10, 1000),
                        Quantity = faker.Random.Decimal(1, 10)
                    }).ToList(),
            };

            return invoice;
        }
    }

    public class Address
    {
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public object Email { get; set; }
    }

    public sealed class Invoice
    {
        public string Number { get; set; } = string.Empty;
        public DateOnly IssuedDate { get; set; }
        public DateOnly DueDate { get; set; }
        public Address SellerAddress { get; set; }
        public Address CustomerAddress { get; set; }
        public List<LineItem> LineItems { get; set; } = [];
    }

    public sealed class LineItem
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }
}
