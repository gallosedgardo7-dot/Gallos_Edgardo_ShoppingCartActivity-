using System;

namespace Cart_System
{
    class Product
    {
        public int Id;
        public string Name;
        public double Price;
        public int RemainingStock;

        public Product(int id, string name, double price, int stock,string category)
        {
            Id = id;
            Name = name;
            Price = price;
            RemainingStock = stock;
            Category = category;
        }

        public void DisplayProduct()
        {
            Console.WriteLine($"{Id}. {Name} - ₱{Price} (Stock: {RemainingStock})");
        }
    }

    class Program
    {
        static void Main()
{
    Product[] products = new Product[]
    {
        new Product(1,"Grit",350,5,"Journal"),
        new Product(2,"Free Will",1000,2,"Journal"),
        new Product(3,"Inspire",500,4,"Books"),
        new Product(4,"Wisdom",400,2,"Books")
    };

    int[] cartProductID = new int[5];
    int[] cartQuantity = new int[5];
    double[] cartTotal = new double[5];

    int cartCount = 0;

    string orderHistory = new string[5];
    
    int orderCount = 0;
    string choice = "YES";
    
    while (choice.ToUpper() == "YES")
    {
        Console.Clear();

        Console.WriteLine("===============[PRODUCT MENU]===============");
        foreach (Product p in products)
        {
            p.DisplayProduct();
        }

        Console.Write("\nEnter product number: ");
        if (!int.TryParse(Console.ReadLine(), out int productChoice) ||
            productChoice < 1 || productChoice > products.Length)
        {
            Console.WriteLine("Invalid product.");
            Console.ReadKey();
            continue;
        }

        Product selected = products[productChoice - 1];

        if (selected.RemainingStock == 0)
        {
            Console.WriteLine("Out of stock.");
            Console.ReadKey();
            continue;
        }

        Console.Write("Enter quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
        {
            Console.WriteLine("Invalid quantity.");
            Console.ReadKey();
            continue;
        }

        if (qty > selected.RemainingStock)
        {
            Console.WriteLine("Not enough stock.");
            Console.ReadKey();
            continue;
        }

        if (cartCount >= 5)
        {
            Console.WriteLine("Cart is full.");
            Console.ReadKey();
            continue;
        }

        cartProductID[cartCount] = selected.Id;
        cartQuantity[cartCount] = qty;
        cartTotal[cartCount] = selected.Price * qty;
        cartCount++;

        selected.RemainingStock -= qty;

        Console.WriteLine("Added to cart!");

        Console.Write("\nAdd again? (YES/NO): ");
        choice = Console.ReadLine();
    }
}
