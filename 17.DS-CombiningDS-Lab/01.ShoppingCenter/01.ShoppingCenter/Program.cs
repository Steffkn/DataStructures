using System;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        var shopping = new ShoppingCenter();

        var inputList = new List<string>();
        for (int i = 0; i < n; i++)
        {
            inputList.Add(Console.ReadLine());
        }

        string input = string.Empty;
        for (int i = 0; i < inputList.Count; i++)
        {
            input = inputList[i];
            string[] inputArgs = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            switch (inputArgs[0])
            {
                case "AddProduct":
                    var productArgs = input.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    var productName = productArgs[0].Replace("AddProduct ", "");
                    shopping.AddProduct(productName, decimal.Parse(productArgs[1]), productArgs[2]);
                    break;
                case "FindProductsByName":
                    shopping.FindProductsByName(input.Replace("FindProductsByName ", ""));
                    break;
                case "FindProductsByProducer":
                    shopping.FindProductsByProducer(input.Replace("FindProductsByProducer ", ""));
                    break;
                case "FindProductsByPriceRange":
                    var productPriceRangeArgs = input.Split(new char[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    shopping.FindProductsByPriceRange(decimal.Parse(productPriceRangeArgs[1]), decimal.Parse(productPriceRangeArgs[2]));
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
}
