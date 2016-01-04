using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;

namespace WebApplicationHangFire2
{
    class Program
    {

        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:8080/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }

    }
}
