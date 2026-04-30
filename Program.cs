  using System;

namespace Cart_System { class Product { public int Id; public string Name; public double Price; public int RemainingStock; public string Category;

public Product(int id, string name, double price, int stock, string category)
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
    static int receiptCounter = 1;

    static void Main()
    {
        Product[] products = new Product[]
        {
            new Product(1, "Grit", 350, 5, "Journal"),
            new Product(2, "Free Will", 1000, 2, "Journal"),
            new Product(3, "Inspire", 500, 4, "Books"),
            new Product(4, "Wisdom", 400, 2, "Books")
        };

        int[] cartProductID = new int[5];
        int[] cartQuantity = new int[5];
        double[] cartTotal = new double[5];
        int cartCount = 0;

        string[] orderHistory = new string[5];
        int orderCount = 0;

        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("======[SHOP MENU]======");
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. View Cart");
            Console.WriteLine("3. Checkout");
            Console.WriteLine("4. View Order History");
            Console.WriteLine("5. Exit");

            Console.Write("Choose: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddItem(products, cartProductID, cartQuantity, cartTotal, ref cartCount);
                    break;

                case "2":
                    ViewCart(cartProductID, cartQuantity, cartTotal, cartCount);
                    break;

                case "3":
                    Checkout(products, cartProductID, cartQuantity, cartTotal, cartCount,
                             orderHistory, ref orderCount);
                    break;

                case "4":
                    Console.WriteLine("======[ORDER HISTORY]======");
                    for (int i = 0; i < orderCount; i++)
                    {
                        Console.WriteLine(orderHistory[i]);
                    }
                    Console.ReadKey();
                    break;

                case "5":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void AddItem(Product[] products, int[] cartProductID, int[] cartQuantity, double[] cartTotal, ref int cartCount)
    {
        Console.Clear();

        foreach (var p in products)
        {
            p.DisplayProduct();
        }

        Console.Write("\nEnter product number: ");
        if (!int.TryParse(Console.ReadLine(), out int choice) ||
            choice < 1 || choice > products.Length)
        {
            Console.WriteLine("Invalid input.");
            Console.ReadKey();
            return;
        }

        Product selected = products[choice - 1];

        if (selected.RemainingStock == 0)
        {
            Console.WriteLine("Out of stock.");
            Console.ReadKey();
            return;
        }

        Console.Write("Enter quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
        {
            Console.WriteLine("Invalid quantity.");
            Console.ReadKey();
            return;
        }

        if (qty > selected.RemainingStock)
        {
            Console.WriteLine("Not enough stock.");
            Console.ReadKey();
            return;
        }

        cartProductID[cartCount] = selected.Id;
        cartQuantity[cartCount] = qty;
        cartTotal[cartCount] = selected.Price * qty;
        cartCount++;

        selected.RemainingStock -= qty;

        Console.WriteLine("Added to cart!");
        Console.ReadKey();
    }

    static void ViewCart(int[] cartProductID, int[] cartQuantity, double[] cartTotal, int cartCount)
    {
        Console.Clear();
        Console.WriteLine("=== CART ===");

        for (int i = 0; i < cartCount; i++)
        {
            Console.WriteLine($"ID: {cartProductID[i]} | Qty: {cartQuantity[i]} | ₱{cartTotal[i]}");
        }

        Console.ReadKey();
    }

    static void Checkout(Product[] products, int[] cartProductID, int[] cartQuantity,
        double[] cartTotal, int cartCount, string[] orderHistory, ref int orderCount)
    {
        double grandTotal = 0;

        for (int i = 0; i < cartCount; i++)
        {
            grandTotal += cartTotal[i];
        }

        double discount = grandTotal >= 5000 ? grandTotal * 0.10 : 0;
        double finalTotal = grandTotal - discount;

        double payment;

        while (true)
        {
            Console.WriteLine($"Final Total: ₱{finalTotal}");
            Console.Write("Enter payment: ");

            if (!double.TryParse(Console.ReadLine(), out payment))
            {
                Console.WriteLine("Invalid input.");
                continue;
            }

            if (payment < finalTotal)
            {
                Console.WriteLine("Insufficient payment.");
                continue;
            }

            break;
        }

        double change = payment - finalTotal;

        Console.Clear();
        Console.WriteLine($"Receipt #: {receiptCounter:D4}");
        Console.WriteLine($"Date: {DateTime.Now}");

        for (int i = 0; i < cartCount; i++)
        {
            Console.WriteLine($"ID: {cartProductID[i]} | Qty: {cartQuantity[i]} | ₱{cartTotal[i]}");
        }

        Console.WriteLine($"Final Total: ₱{finalTotal}");
        Console.WriteLine($"Payment: ₱{payment}");
        Console.WriteLine($"Change: ₱{change}");

        orderHistory[orderCount] = $"Receipt #{receiptCounter:D4} - ₱{finalTotal}";
        orderCount++;
        receiptCounter++;

        Console.WriteLine("\nLOW STOCK ALERT:");
        foreach (var p in products)
        {
            if (p.RemainingStock <= 5)
            {
                Console.WriteLine($"{p.Name} - {p.RemainingStock} left");
            }
        }

        Console.ReadKey();
    }
}

}