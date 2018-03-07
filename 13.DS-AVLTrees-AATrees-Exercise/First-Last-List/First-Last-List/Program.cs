using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;

public class Program
{
    static void Main(string[] args)
    {
        // Arrange
        var items = FirstLastListFactory.Create<Product>();
        items.Add(new Product(0.50m, "coffee"));
        items.Add(new Product(1.20m, "mint drops"));
        items.Add(new Product(1.20m, "beer"));
        items.Add(new Product(0.50m, "candy"));
        items.Add(new Product(1.20m, "cola"));

        // Act
        var returnedItems = items.Max(4).Select(p => p.Title).ToList();

        // Assert
        var expectedItems = new string[] {
            "mint drops", "beer", "cola", "coffee" };

        foreach (var returnedItem in returnedItems)
        {
            Console.WriteLine(returnedItem);
        }
        Console.WriteLine();

        // Assert
    }
    class Product : IComparable<Product>
    {
        public string Title { get; set; }
        public decimal Price { get; set; }

        public Product(decimal price, string title)
        {
            this.Price = price;
            this.Title = title;
        }

        public int CompareTo(Product other)
        {
            return this.Price.CompareTo(other.Price);

        }
    }

}
