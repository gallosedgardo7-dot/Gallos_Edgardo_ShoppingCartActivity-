using System;

namespace Cart_System
{
    class Product
    {
        public int Id;
        public string Name;
        public double Price;
        public int RemainingStock;

        public Product(int id, string name, double price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            RemainingStock = stock;
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
                new Product(1,"Grit",350,5),
                new Product(2,"Free Will",1000,2),
                new Product(3,"Inspire",500,4),
                new Product(4,"Wisdom",400,2)
            };

            Console.WriteLine("===============[PRODUCT MENU]===============");

            foreach (Product p in products)
            {
                p.DisplayProduct();
            }

            Console.ReadKey();
        }
    }
}
