using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Task_43_Seller
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Seller seller = new Seller();
            Buyer buyer = new Buyer();
            bool isWork = true;
            int money = 100;

            while (isWork == true)
            {
                Console.WriteLine($"Ваши средства: {money} руб.\n\nтовары в наличии: \n");
                seller.ShowGoods();
                Console.WriteLine("\n1. Купить товар \n2. Посмотреть свою сумку \n3. Выход");

                switch (Console.ReadLine())
                {
                    case "1":
                        buyer.PutInBag(seller.TransferGoods(ref money));
                        break;

                    case "2":
                        buyer.LookInBag();
                        break;

                    case "3":
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Неккоректный ввод");
                        break;
                }
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }

    class Product
    {
        public string Name { get; private set; }
        public int Cost { get; private set; }

        public Product(string name, int cost)
        {
            Name = name;
            Cost = cost;
        }

        public void OutputInfo()
        {
            Console.WriteLine($"{Name} - {Cost} руб.");
        }
    }

    class Human
    {
        public List<Product> _bag = new List<Product>();

        public void ShowGoods()
        {
            foreach (Product product in _bag)
            {
                product.OutputInfo();
            }
        }
    }

    class Seller : Human
    {
        private Dictionary<string, int> _typesGoods = new Dictionary<string, int>();
        public Product Goods { get; private set; }

        public Seller()
        {
            _typesGoods.Add("помидоры", 14);
            _typesGoods.Add("огурцы", 38);
            _typesGoods.Add("колбаса", 105);
            _typesGoods.Add("салат", 74);
            _typesGoods.Add("шаверма", 144);

            CreateAssortmentGoods();
        }

        private void CreateAssortmentGoods()
        {
            Random random = new Random();
            int minQuantityGoods = 0;
            int maxQuantityGoods = 5;
            int randomQuantityGoods;

            for (int i = 0; i < _typesGoods.Count; i++)
            {
                randomQuantityGoods = random.Next(minQuantityGoods, maxQuantityGoods);

                for (int j = 0; j < randomQuantityGoods; j++)
                {
                    Product product = new Product(_typesGoods.ElementAt(i).Key, _typesGoods.ElementAt(i).Value);
                    _bag.Add(product);
                }
            }
        }

        private void SaleGoods(ref int money)
        {
            if (_bag.Count > 0)
            {
                bool isFound = false;
                Console.WriteLine("\nВведите наименование товара: \n");
                string input = Console.ReadLine();

                foreach (Product product in _bag)
                {
                    if (product.Name == input && product.Cost <= money)
                    {
                        Console.WriteLine("\nвы купили товар\n");
                        money -= product.Cost;
                        Goods = product;
                        _bag.Remove(product);
                        isFound = true;
                        break;
                    }
                    else if (product.Name == input && product.Cost > money)
                    {
                        Console.WriteLine("\nнет денег на покупку этого товара\n");
                        isFound = true;
                        break;
                    }
                }

                if (isFound == false)
                {
                    Console.WriteLine("\nэтого товара нет в наличии\n");
                }
            }
            else
            {
                Console.WriteLine("\nтовары кончились\n");
            }
        }

        public Product TransferGoods(ref int money)
        {
            SaleGoods(ref money);
            return Goods;
        }
    }

    class Buyer : Human
    {
        public void PutInBag(Product goods)
        {
            _bag.Add(goods);
        }

        public void LookInBag()
        {
            foreach (Product product in _bag)
            {
                Console.WriteLine(product.Name);
            }
        }
    }
}

