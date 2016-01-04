using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using WebApplicationHangFire2.Models;

namespace ClientApp
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            client.BaseAddress = new Uri("http://localhost:8080");

            ListAllProducts();
            fillLine();
            ListProduct(1);
            fillLine();
            ListProducts("toys");
            fillLine();
            var dummiResponse = createDummy();
            ListAllProducts();
            fillLine();
            if (dummiResponse.IsSuccessStatusCode)
            {
                Uri dummiUri = dummiResponse.Headers.Location;
                Console.WriteLine("DummiItem succesfully created. URI={0}",dummiUri);
                fillLine();
                changeDummyPrice(dummiUri);
                ListAllProducts();
                fillLine();
                deleteDummy(dummiUri);
                ListAllProducts();
            }
            else
            {
                Console.WriteLine("DummiItem could not be created.");
            }

            fillLine();
            Console.WriteLine("Press Enter to quit.");
            Console.ReadLine();
        }

        static void ListAllProducts()
        {
            HttpResponseMessage resp = client.GetAsync("api/products").Result;
            resp.EnsureSuccessStatusCode();

            var products = resp.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            foreach (var p in products)
            {
                Console.WriteLine("{0} {1} {2} ({3})", p.Id, p.Name, p.Price, p.Category);
            }
        }

        static void ListProduct(int id)
        {
            var resp = client.GetAsync(string.Format("api/products/{0}", id)).Result;
            resp.EnsureSuccessStatusCode();

            var product = resp.Content.ReadAsAsync<Product>().Result;
            Console.WriteLine("ID {0}: {1}", id, product.Name);
        }

        static void ListProducts(string category)
        {
            Console.WriteLine("Products in '{0}':", category);

            string query = string.Format("api/products?category={0}", category);

            var resp = client.GetAsync(query).Result;
            //var resp = client.
            resp.EnsureSuccessStatusCode();

            var products = resp.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            foreach (var product in products)
            {
                Console.WriteLine(product.Name);
            }
        }

        static HttpResponseMessage createDummy()
        {
            // HTTP POST
            var gizmo = new Product() { Name = "Gizmo", Price = 100, Category = "Widget" };
            HttpResponseMessage response = client.PostAsJsonAsync("api/products", gizmo).Result;
            return response;
        }


        static void changeDummyPrice(Uri dummyUri)
        {
            var gizmo = new Product() { Name = "Frank", Price = 1000, Category = "Widget" };
            // HTTP PUT
            gizmo.Price = 80;   // Update price
            var result =  client.PutAsJsonAsync(dummyUri, gizmo).Result;
        }

        static void deleteDummy(Uri dummyUri)
        {
            // HTTP DELETE
            var result = client.DeleteAsync(dummyUri).Result;
        }

        static void fillLine()
        {
            Console.WriteLine("____________________________________________________");
        }
    }
}

