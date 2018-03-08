using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class ShoppingCenterFast
{
    private Dictionary<string, BigList<Product>> byProducts;
    private Dictionary<string, BigList<Product>> byName;
    private OrderedBag<Product> byPrice;
    private Dictionary<string, BigList<Product>> byNameAndProducer;

    public ShoppingCenterFast()
    {
        this.byProducts = new Dictionary<string, BigList<Product>>();
        this.byName = new Dictionary<string, BigList<Product>>();
        this.byPrice = new OrderedBag<Product>((x, y) => x.Price.CompareTo(y.Price));
        this.byNameAndProducer = new Dictionary<string, BigList<Product>>();
    }

    public void Add(Product product)
    {
        if (!this.byProducts.ContainsKey(product.Producer))
        {
            this.byProducts[product.Producer] = new BigList<Product>();
        }

        if (!this.byName.ContainsKey(product.Name))
        {
            this.byName[product.Name] = new BigList<Product>();
        }

        string nameAndProducer = $"{product.Name}{product.Producer}";
        if (!this.byNameAndProducer.ContainsKey(nameAndProducer))
        {
            this.byNameAndProducer[nameAndProducer] = new BigList<Product>();
        }

        this.byProducts[product.Producer].Add(product);
        this.byName[product.Name].Add(product);
        this.byPrice.Add(product);
        this.byNameAndProducer[nameAndProducer].Add(product);
    }

    public int DeleteProductsByProducer(string producer)
    {
        if (!this.byProducts.ContainsKey(producer))
        {
            return 0;
        }

        IEnumerable<Product> productsToDelete = this.byProducts[producer];
        int count = 0;

        foreach (var product in productsToDelete)
        {
            string key = $"{product.Name}{product.Producer}";
            this.byNameAndProducer.Remove(key);
            this.byName[product.Name].Remove(product);
            this.byPrice.Remove(product);
            count++;
        }

        this.byProducts.Remove(producer);
        return count;
    }

    public int DeleteProductsByProducerAndName(string productName, string producer)
    {
        string key = $"{productName}{producer}";
        if (!this.byNameAndProducer.ContainsKey(key))
        {
            return 0;
        }

        var result = this.byNameAndProducer[key];
        int count = result.Count;

        foreach (var product in result)
        {
            this.byProducts[product.Producer].Remove(product);
            this.byName[product.Name].Remove(product);
            this.byPrice.Remove(product);
        }

        this.byNameAndProducer.Remove(key);
        return count;
    }

    public IEnumerable<Product> FindProductsByName(string name)
    {
        if (!this.byName.ContainsKey(name))
        {
            return Enumerable.Empty<Product>();
        }

        return this.byName[name].OrderBy(x => x);
    }

    public IEnumerable<Product> FindProductsByProducer(string producer)
    {
        if (!this.byProducts.ContainsKey(producer))
        {
            return Enumerable.Empty<Product>();
        }

        return this.byProducts[producer].OrderBy(x => x);
    }

    public IEnumerable<Product> FindProductsByPriceRange(double fromPrice, double toPrice)
    {
        return this.byPrice.Range(new Product("", fromPrice, ""), true, new Product("", toPrice, ""), true)
            .OrderBy(x => x);
    }
}
