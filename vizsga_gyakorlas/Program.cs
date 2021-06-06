using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vizsga_gyakorlas
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("elso feladat");
            using (var db = new vizsgaEntities()) {
            
                //var result = from p in db.Products
                //                     where p.Stock > 30
                //                     select p;
                var result = db.Products.Where(p => p.Stock > 30).Select(p => new { p.Name, p.Stock });
            
                foreach (var p in result) {
                    
                    Console.WriteLine("\t\tName={0}\tStock={1}", p.Name, p.Stock);
                }
            }
            Console.WriteLine("masodik feladat");
            using(var db = new vizsgaEntities()){
            
                var result = db.Products.Where(p => p.OrderItems.Count(x => x.ProductID == p.ID) >= 2);
            
               // var result = from p in db.Products
               //                     where p.OrderItems.Count >= 2
               //                     select p;
            
                foreach (var p in result)
                {
                    Console.WriteLine("\t\tName={0}\tStock={1}", p.Name, p.Stock);
                }
            
            }
            Console.WriteLine("harmadik feladat");
            using (var db = new vizsgaEntities())
            {
            
                var result = db.Orders.Where(o => o.OrderItems.Sum(oi => oi.Amount * oi.Price) > 30000);
                foreach (var o in result)
                {
                    Console.WriteLine("\t\tName={0}", o.CustomerSite.Customer.Name);
                    foreach (var oi in o.OrderItems)
                        Console.WriteLine("\t\t\tProduct={0}\tPrice={1}\tAmount={2}", oi.Product.Name, oi.Price, oi.Amount);
                }
            
            
            }
            Console.WriteLine("negyedik feladat");
            using (var db = new vizsgaEntities())
            {
            
               var result = db.Products.Where(p => p.Price == db.Products.Max(p1 => p1.Price));
               // var result = from p in db.Products
               //                 where p.Price == db.Products.Max(a => a.Price)
               //                 select p;
            
                foreach (var t in result)
                    Console.WriteLine("\t\tName={0}\tPrice={1}", t.Name, t.Price);
            
            
            }
            Console.WriteLine("otodik feladat");
            using (var db = new vizsgaEntities())
            {
            
                var result = from s1 in db.CustomerSites
                             join s2 in db.CustomerSites on s1.City equals s2.City
                             where s1.Customer.ID > s2.Customer.ID
                             select new { name1 = s1.Customer.Name, name2 = s2.Customer.Name };
                foreach (var v in result)
                    Console.WriteLine("\t\tCustomer 1={0}\tCustomer 2={1}", v.name1, v.name2);
            
                Console.WriteLine("megoldas" );
                var qJoin = from s1 in db.CustomerSites
                            join s2 in db.CustomerSites on s1.City equals s2.City
                            where s1.CustomerID > s2.CustomerID
                            select new { c1 = s1.Customer, c2 = s2.Customer };
                foreach (var v in qJoin)
                    Console.WriteLine("\t\tCustomer 1={0}\tCustomer 2={1}", v.c1.Name, v.c2.Name);
            
            
            }
            Console.WriteLine("hatodik feladat");
            using (var db = new vizsgaEntities())
            {
                var update = db.Products.Where(p => p.Category.Name.ToString().Equals("LEGO"));
                
                
                foreach (var p in update)
                {
                    Console.WriteLine("\t\t\tName={0}\tStock={1}\tPrice={2}", p.Name, p.Stock, p.Price);
                    p.Price = 1.1 * p.Price;
                }
                
                Console.WriteLine("megoldas");
                
                var qProductsLego = from p in db.Products
                                    where p.Category.Name == "LEGO"
                                    select p;
                Console.WriteLine("\tMódosítás utan:");
                foreach (var p in qProductsLego)
                {
                    Console.WriteLine("\t\t\tName={0}\tStock={1}\tPrice={2}", p.Name, p.Stock, p.Price);
                    // p.Price = 1.1 * p.Price;
                }
                db.SaveChanges();
                var newCategory = new Category();
                newCategory.Name = "Expensive toys";
                db.Categories.Add(newCategory);

                var products = db.Products.Where(p => p.Price > 8000);
                foreach (var p in products) {
                    p.Category = newCategory;
                }
                db.SaveChanges();

                var qProductExpensive = from p in db.Products
                                    where p.Category.Name == "Expensive toys"
                                    select p;

                foreach (var t in qProductExpensive)
                    Console.WriteLine("\t\tName={0}\tPrice={1}", t.Name, t.Price);

                var categoryCount = db.Products.RemoveRange(qProductExpensive);




            }


            





            Console.ReadKey();
        }
    }
}
