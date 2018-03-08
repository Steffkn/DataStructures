public interface IShoppingCenter
{
    void AddProduct(string name, double price, string producer);
    void DeleteProducts(string producer);
    void DeleteProducts(string name, string producer);
    void FindProductsByName(string name);
    void FindProductsByProducer(string producer);
    void FindProductsByPriceRange(double fromPrice, double toPrice);
}

