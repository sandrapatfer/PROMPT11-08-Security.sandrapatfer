using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new WhoAmI.WhoAmIClient();

            client.ClientCredentials.UserName.UserName = "alice";
            client.ClientCredentials.UserName.Password = "spf123";
            client.ClientCredentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            Console.WriteLine("Calling service");
            var response = client.Get();
            Console.Write("--Service returned:\n{0}", response);
            Console.WriteLine("--End test", response);
            Console.ReadKey();
        }
    }
}
