using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplicationHangFire2.Models;

namespace WebApplicationHangFire2.ApiControllers
{
    public class ProductsController : ApiController
    {
        private ProductDBContext db = new ProductDBContext();

        public IEnumerable<Product> GetAllProducts()
        {
            var result = db.Products.ToList();
            return result;
        }

        public Product GetProduct(int? id)
        {
            if (id == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            Product result = db.Products.Find(id);
            if (result == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return result;
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return db.Products.ToList().Where(
                p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase)); 
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        public HttpResponseMessage PostProduct(Product item)
        {
            HttpResponseMessage response;
            if (ModelState.IsValid)
            {
                db.Products.Add(item);
                db.SaveChanges();
                response = Request.CreateResponse<Product>(HttpStatusCode.Created, item);

                string uri = Url.Link("DefaultApi", new { id = item.Id });
                response.Headers.Location = new Uri(uri);

                //TODO: http://www.asp.net/web-api/overview/formats-and-model-binding/model-validation-in-aspnet-web-api 

                //Try to fire a hangfire task
                var id = Hangfire.BackgroundJob.Enqueue(() => Console.WriteLine("Hangfire: New Product with ID {0} added to repository.", item.Id));
                Console.WriteLine("Hangfire task created with ID: {0}",id);
            }
            else
            {
                response = Request.CreateResponse<Product>(HttpStatusCode.BadRequest, item);
            }

            return response;
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        public void PutProduct(int id, Product product)
        {
            product.Id = id;
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteProduct(int? id)
        {
            if (id == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
