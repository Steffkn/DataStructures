using System;
using System.Collections;
using System.Collections.Generic;
using Wintellect.PowerCollections;
using System.Linq;

public class Instock : IProductStock
{
    private Dictionary<string, Product> products;
    private OrderedBag<Product> byLabel;
    private List<Product> byInsertion;
    private OrderedBag<Product> byPrice;
    private Dictionary<int, HashSet<Product>> byQuantity;

    public Instock()
    {
        this.products = new Dictionary<string, Product>();
        this.byLabel = new OrderedBag<Product>((x, y) => x.Label.CompareTo(y.Label));
        this.byInsertion = new List<Product>();
        this.byPrice = new OrderedBag<Product>((x, y) => y.Price.CompareTo(x.Price));
        this.byQuantity = new Dictionary<int, HashSet<Product>>();
    }

    public int Count => this.products.Count;

    public void Add(Product product)
    {
        // TODO: can be bad!!!
        if (!this.products.ContainsKey(product.Label))
        {
            this.products.Add(product.Label, product);
            this.byLabel.Add(product);
            this.byInsertion.Add(product);
            this.byPrice.Add(product);

            if (!this.byQuantity.ContainsKey(product.Quantity))
            {
                this.byQuantity.Add(product.Quantity, new HashSet<Product>());
            }

            this.byQuantity[product.Quantity].Add(product);
        }
    }

    public void ChangeQuantity(string product, int quantity)
    {
        if (!this.products.ContainsKey(product))
        {
            throw new ArgumentException();
        }

        var result = this.products[product];
        this.byQuantity[result.Quantity].RemoveWhere(x => x.Label == result.Label);
        result.Quantity = quantity;

        if (!this.byQuantity.ContainsKey(quantity))
        {
            this.byQuantity.Add(quantity, new HashSet<Product>());
        }

        this.byQuantity[quantity].Add(result);
    }

    public bool Contains(Product product)
    {
        return this.products.ContainsKey(product.Label);
    }

    public Product Find(int index)
    {
        if (index < 0 || this.Count <= index)
        {
            throw new IndexOutOfRangeException();
        }
        else
        {
            return byInsertion[index];
        }
    }

    public IEnumerable<Product> FindAllByPrice(double price)
    {
        var result = this.products.Values.Where(x => x.Price == price);
        if (!result.Any())
        {
            return Enumerable.Empty<Product>();
        }

        return result;
    }

    public IEnumerable<Product> FindAllByQuantity(int quantity)
    {
        if (!this.byQuantity.ContainsKey(quantity))
        {
            return Enumerable.Empty<Product>();
        }

        return this.byQuantity[quantity];
    }

    public IEnumerable<Product> FindAllInRange(double lo, double hi)
    {
        var result = this.products.Values
            .Where(x => x.Price > lo && x.Price <= hi)
        .OrderByDescending(x => x.Price);

        if (!result.Any())
        {
            return Enumerable.Empty<Product>();
        }

        return result;
    }

    public Product FindByLabel(string label)
    {
        if (!this.products.ContainsKey(label))
        {
            throw new ArgumentException();
        }

        return this.products[label];
    }

    public IEnumerable<Product> FindFirstByAlphabeticalOrder(int count)
    {
        if (count < 0 || this.Count < count)
        {
            throw new ArgumentException();
        }

        var result = new List<Product>();

        for (int i = 0; i < count; i++)
        {
            result.Add(this.byLabel[i]);
        }

        if (!result.Any())
        {
            return Enumerable.Empty<Product>();
        }

        return result;
    }

    public IEnumerable<Product> FindFirstMostExpensiveProducts(int count)
    {
        if (count < 0 || this.Count < count)
        {
            throw new ArgumentException();
        }

        var result = this.byPrice.Take(count);

        return result;
    }

    public IEnumerator<Product> GetEnumerator()
    {
        foreach (var product in products)
        {
            yield return product.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
