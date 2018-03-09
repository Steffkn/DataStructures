using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        var shopping = new ShoppingCenterFast();

        string input = string.Empty;
        for (int i = 0; i < n; i++)
        {
            input = Console.ReadLine();
            int firstSpaceIndex = input.IndexOf(' ');
            string command = input.Substring(0, firstSpaceIndex);
            string[] args = input.Substring(firstSpaceIndex + 1).Split(';');

            switch (command)
            {
                case "AddProduct":
                    string name = args[0];
                    double price = double.Parse(args[1]);
                    string producer = args[2];
                    var product = new Product(name, price, producer);
                    shopping.Add(product);
                    Console.WriteLine("Product added");
                    break;
                case "FindProductsByName":
                    List<Product> resultsByName = shopping
                        .FindProductsByName(args[0])
                        .ToList();
                    PrintResult(resultsByName);
                    break;
                case "FindProductsByProducer":
                    List<Product> resultsByProducer = shopping
                       .FindProductsByProducer(args[0])
                       .ToList();
                    PrintResult(resultsByProducer);
                    break;
                case "FindProductsByPriceRange":
                    List<Product> resultsByPriceRange = shopping
                       .FindProductsByPriceRange(double.Parse(args[0]), double.Parse(args[1]))
                       .ToList();
                    PrintResult(resultsByPriceRange);
                    break;
                case "DeleteProducts":
                    int count = 0;
                    if (args.Length == 1)
                    {
                        count = shopping.DeleteProductsByProducer(args[0]);
                    }
                    else
                    {
                        count = shopping.DeleteProductsByProducerAndName(args[0], args[1]);
                    }

                    PrintDeletedCount(count);
                    break;
            }
        }

    }

    private static void PrintResult(List<Product> result)
    {
        if (result.Count != 0)
        {
            Console.WriteLine(string.Join("\n", result));
        }
        else
        {
            Console.WriteLine("No products found");
        }
    }

    private static void PrintDeletedCount(int count)
    {
        if (count == 0)
        {
            Console.WriteLine("No products found");
        }
        else
        {
            Console.WriteLine($"{count} products deleted");
        }
    }

    private static void ExecuteCommandShopSlow(ShoppingCenter shopping, string input)
    {
        string[] inputArgs = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        switch (inputArgs[0])
        {
            case "AddProduct":
                var productArgs = input.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                var productName = productArgs[0].Replace("AddProduct ", "");
                shopping.AddProduct(productName, double.Parse(productArgs[1]), productArgs[2]);
                break;
            case "FindProductsByName":
                shopping.FindProductsByName(input.Replace("FindProductsByName ", ""));
                break;
            case "FindProductsByProducer":
                shopping.FindProductsByProducer(input.Replace("FindProductsByProducer ", ""));
                break;
            case "FindProductsByPriceRange":
                var productPriceRangeArgs = input.Split(new char[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                shopping.FindProductsByPriceRange(double.Parse(productPriceRangeArgs[1]), double.Parse(productPriceRangeArgs[2]));
                break;
            case "DeleteProducts":
                var productsToDeleteArgs = input.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (productsToDeleteArgs.Length == 1)
                {
                    shopping.DeleteProducts(productsToDeleteArgs[0].Replace("DeleteProducts ", ""));
                }
                else
                {
                    shopping.DeleteProducts(productsToDeleteArgs[0].Replace("DeleteProducts ", ""), productsToDeleteArgs[1]);
                }
                break;
        }
    }
}
