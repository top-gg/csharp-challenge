using System;
using System.IO;
using Nancy;
using Nancy.Hosting.Self;

namespace Library {
    class Program {
        static void Main(string[] args) {
            // https://volkanpaksoy.com/archive/2015/11/11/building-a-simple-http-server-with-nancy/
            string url = "http://localhost";
            int port = 8000;
            var server = new NancyHost(new Uri($"{url}:{port}/"));
            server.Start();
            Console.WriteLine("Server is running");
            Console.ReadKey();
            server.Stop();
        }
    }
}
