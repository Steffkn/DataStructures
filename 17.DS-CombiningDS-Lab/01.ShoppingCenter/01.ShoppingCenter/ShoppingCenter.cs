using System;
using System.Collections.Generic;
using System.Linq;

public class ShoppingCenter : IShoppingCenter
{
    private Dictionary<string, List<Product>> productsByProducer;
    private Dictionary<string, List<Product>> productsByName;
    private Dictionary<double, List<Product>> productsByPrice;

    public ShoppingCenter()
    {
        productsByProducer = new Dictionary<string, List<Product>>();
        productsByName = new Dictionary<string, List<Product>>();
        productsByPrice = new Dictionary<double, List<Product>>();
    }

    public void AddProduct(string name, double price, string producer)
    {
        var product = new Product(name, price, producer);
        if (!productsByProducer.ContainsKey(producer))
        {
            productsByProducer.Add(producer, new List<Product>());
        }

        if (!productsByName.ContainsKey(name))
        {
            productsByName.Add(name, new List<Product>());
        }

        if (!productsByPrice.ContainsKey(price))
        {
            productsByPrice.Add(price, new List<Product>());
        }

        productsByProducer[producer].Add(product);
        productsByName[name].Add(product);
        productsByPrice[price].Add(product);
        Console.WriteLine("Product added");
    }

    public void DeleteProducts(string producer)
    {
        if (productsByProducer.ContainsKey(producer))
        {
            int count = productsByProducer[producer].Count;
            productsByProducer.Remove(producer);

            foreach (var pr in productsByName)
            {
                pr.Value.RemoveAll(p => p.Producer.Equals(producer));
            }

            foreach (var pr in productsByPrice)
            {
                pr.Value.RemoveAll(p => p.Producer.Equals(producer));
            }

            Console.WriteLine($"{count} products deleted");
        }
        else
        {
            Console.WriteLine("No products found");
        }
    }

    public void DeleteProducts(string name, string producer)
    {
        if (productsByProducer.ContainsKey(producer))
        {
            int count = this.productsByProducer[producer].Count;
            productsByProducer[producer].RemoveAll(p => p.Name.Equals(name));

            foreach (var pr in productsByName)
            {
                pr.Value.RemoveAll(p => p.Producer.Equals(producer) && p.Name.Equals(name));
            }

            foreach (var pr in productsByPrice)
            {
                pr.Value.RemoveAll(p => p.Producer.Equals(producer) && p.Name.Equals(name));
            }

            count -= this.productsByProducer[producer].Count;
            Console.WriteLine($"{count} products deleted");
        }
        else
        {
            Console.WriteLine("No products found");
        }
    }

    public void FindProductsByName(string name)
    {
        if (productsByName.ContainsKey(name) && productsByName[name].Count > 0)
        {
            foreach (var product in productsByName[name]
                .OrderBy(p => p.Producer)
                .ThenBy(p => p.Price))
            {
                Console.WriteLine(product);
            }
        }
        else
        {
            Console.WriteLine("No products found");
        }
    }

    public void FindProductsByProducer(string producer)
    {
        if (productsByProducer.ContainsKey(producer) && productsByProducer[producer].Count > 0)
        {
            foreach (var product in productsByProducer[producer].OrderBy(p => p.Name).ThenBy(p => p.Price))
            {
                Console.WriteLine(product);
            }
        }
        else
        {
            Console.WriteLine("No products found");
        }
    }

    public void FindProductsByPriceRange(double fromPrice, double toPrice)
    {
        var products = this.productsByPrice.Where(p => p.Key >= fromPrice && p.Key <= toPrice);
        if (products.Any())
        {
            foreach (var productKvP in products.OrderBy(p => p.Key))
            {
                foreach (var product in productKvP.Value)
                {
                    Console.WriteLine(product);
                }
            }
        }
        else
        {
            Console.WriteLine("No products found");
        }
    }
}
