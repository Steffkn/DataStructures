public class Product
{
    // name, price and producer
    public string Name { get; set; }

    public decimal Price { get; set; }

    public string Producer { get; set; }

    public Product(string name, decimal price, string producer)
    {
        this.Name = name;
        this.Price = price;
        this.Producer = producer;
    }

    public override string ToString()
    {
        return $"{{{this.Name};{this.Producer};{this.Price:f2}" + "}";
    }
}
