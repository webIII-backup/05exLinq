﻿using SportsStore.Data;
using SportsStore.Models;
using SportsStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore
{
    class Program
    {
        static void Main(string[] args)
        {

            IEnumerable<Category> categories = DataSourceProvider.Categories;
            IEnumerable<Customer> customers = DataSourceProvider.Customers;
            IEnumerable<Product> products = DataSourceProvider.Products;

            #region Toon de gemiddelde prijs van de producten
            var gemiddelde = products.Average(p => p.Price);
            Console.WriteLine($"De gemiddelde prijs van de producten is { gemiddelde:0.00}");
            Console.ReadLine();
            #endregion

            #region Toon hoeveel categorieen en hoeveel customers er zijn
            Console.WriteLine($"Er zijn {categories.Count()} categorieen.");
            Console.WriteLine($"Er zijn {customers.Count()} customers.");
            Console.ReadLine();
            #endregion

            #region Hoeveel karakters telt de langste productnaam?
            Console.WriteLine($"De langste productnaam is {products.Max(p => p.Name.Length)} karakters lang.");
            Console.ReadLine();
            #endregion

            #region Toon de naam van het product met de langste productnaam

            string productnaam = products.OrderByDescending(p => p.Name.Length).First().Name; 
            Console.WriteLine($"De langste productnaam is {productnaam}.");
            Console.ReadLine();
            #endregion

            #region Toon alle customers gesorteerd op naam, en vervolgens dalend op voornaam
            IEnumerable<Customer> customersSorted = customers.OrderBy(c => c.Name).ThenByDescending(c => c.FirstName);
            PrintCustomers("Klanten gesorteerd op naam, en dan dalend op voornaam:", customersSorted);
            Console.ReadLine();
            #endregion

            #region Toon alle producten die meer dan 92.5 dollar kosten, dalend op prijs
            IEnumerable<Product> expensiveProducts = products.Where(p => p.Price > 92.5M).OrderByDescending(p => p.Price);
            PrintProducts("Producten die meer dan 92.5 dollar kosten", expensiveProducts);
            Console.ReadLine();
            #endregion

            #region  Toon de categorieen die meer dan twee producten bevatten

            IEnumerable<Category> myCategories = categories.Where(c => c.Products.Count > 2);
            PrintCategories("Categorieën met meer dan twee producten", myCategories);
            Console.ReadLine();
            #endregion

            #region  Maak een lijst van strings die alle productnamen bevat
            IEnumerable<string> productNamen = products.Select(p => p.Name);
            PrintStrings("Namen van producten", productNamen);
            Console.ReadLine();
            #endregion

            #region Maak een lijst van namen van steden waar customers wonen (zonder dubbels) 
            IEnumerable<string> steden = customers.Select(p => p.City.Name).Distinct();
            PrintStrings("Namen van steden waar klanten wonen", steden);
            Console.ReadLine();
            #endregion

            #region Maak een lijst van ProductViewModels (vorm elk product om tot een productViewModel)
            IEnumerable<ProductViewModel> pvm = products.Select(p => new ProductViewModel
            {
                Name = p.Name,
                Price = p.Price,
                PriceEuro = p.Price * 0.9M
            });

            Console.WriteLine("Lijst van ProductViewModels");
            foreach (var p in pvm)
            {
                Console.WriteLine($"{p.Name} kost { p.Price:0.00} dollar, dat is {p.PriceEuro:0.00} euro");
            }
            Console.WriteLine();
            Console.ReadLine();
            #endregion

            #region Maak gebruik van een anoniem type 
            // maak een lijst die de naam, de voornaam
            // en de naam van de stad van elke customer bevat
            var customerDetails = customers.Select(c => new
            {
                c.Name,
                c.FirstName,
                CityName = c.City.Name,
            });

            Console.WriteLine("Details van customers");
            foreach (var c in customerDetails)
            {
                Console.WriteLine($"{c.Name } { c.FirstName } woont in { c.CityName}");
            }
            Console.ReadLine();
            #endregion

            #region Pas vorige query aan 
            // zodat het anoniem type nu ook een boolse property bevat
            // die aangeeft of de customer reeds orders heeft
            var customerDetails2 =  customers.Select(c => new
            {
                c.Name,
                c.FirstName,
                CityName = c.City.Name,
                HasOrders = c.Orders.Any()
            });

            Console.WriteLine("Details van customers");
            foreach (var c in customerDetails2)
            {
                Console.WriteLine($"{c.Name } { c.FirstName } woont in { c.CityName } en heeft { (c.HasOrders ? "" : "geen")} bestellingen");
            }
            Console.ReadLine();
            #endregion

            #region Geef de namen van de categorieën met enkel producten die de letter 'o' in de naam hebben

            IEnumerable<string> oCategories = categories
                .Where(c => c.Products.All(p => p.Name.Contains("o")))
                .Select(c => c.Name);
            PrintStrings("Categorieën waarbij alle producten de letter 'o' bevatten", oCategories);
            Console.ReadLine();
            #endregion

            #region Geef het eerste product die de letter 'f' bevat, vertrek van de lijst van producten gesorteerd op naam

            Product myProductF = products.OrderBy(p => p.Name).FirstOrDefault(p => p.Name.Contains("f"));
            PrintProduct("Eerste product met letter f", myProductF);
            Console.ReadLine();
            #endregion

            #region Maak een lijst van customers die reeds een product met de naam Football hebben besteld

            IEnumerable<Customer> customersWithFootball = customers.Where(c => c.Orders.Any(o => o.OrderLines.Any(ol => ol.Product.Name == "Football")));

            PrintCustomers("Klanten die reeds Football bestelden:", customersWithFootball);
            Console.ReadLine();
            #endregion

            #region Toon de voornaam van de customer met naam Student1.
            // Geef een gepaste melding indien
            //  - er geen customers zijn met die naam, 
            //  - of indien er meerdere customers zijn met die naam  
            // Test je code ook met Student0 en Student9
            string naam = "Student1";
            try
            {
                var customer = customers.SingleOrDefault(c => c.Name == naam);
                if (customer != null)
                    Console.WriteLine($"Customer { customer.Name} {customer.FirstName}" );
                else
                    Console.WriteLine($"Er is geen customer met de naam {naam}");

            }
            catch (Exception)
            {
                Console.WriteLine($"Meerdere customers noemen {naam}");

            }
            Console.ReadLine();

            //Enstension method shuffle for IEnumerable<T>
            var products2 = products.Shuffle();
            PrintProducts("Shuffle extension method", products2);
            Console.ReadLine();
        }

        #endregion Pas de klasse Cart aan en maak zoveel mogelijk gebruik van expression bodied members
        //Pas de klasse Cart aan
        #region
        #endregion

        #region Print helpmethodes
        private static void PrintCustomers(string message, IEnumerable<Customer> customers)
        {
            Console.WriteLine(message);
            foreach (Customer c in customers)
                Console.WriteLine($"{ c.Name} {c.FirstName}");
            Console.WriteLine();
        }

        private static void PrintProducts(string message, IEnumerable<Product> products)
        {
            Console.WriteLine(message);
            foreach (Product p in products)
                Console.WriteLine($"{ p.Name}, prijs: { p.Price}");
            Console.WriteLine();
        }

        private static void PrintCategories(string message, IEnumerable<Category> categories)
        {
            Console.WriteLine(message);
            foreach (Category c in categories)
                Console.WriteLine(c.Name);
            Console.WriteLine();
        }

        private static void PrintStrings(string message, IEnumerable<string> strings)
        {
            Console.WriteLine(message);
            foreach (string s in strings)
                Console.WriteLine(s);
            Console.WriteLine();
        }

        private static void PrintProduct(string message, Product product)
        {
            Console.WriteLine(message);
            if (product == null)
                Console.WriteLine("Product is null");
            else
                Console.WriteLine($"{ product.Name}, prijs: {product.Price}");

            Console.WriteLine();
        }
        #endregion
    }
}

