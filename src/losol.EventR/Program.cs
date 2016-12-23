using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace losol.EventR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Define ssl certificate
            var certFile = Directory.GetCurrentDirectory() + "\\selfsignedcert.pfx";
            var signingCertificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(certFile, "Th1sSh0uldBe1Secret");

            var host = new WebHostBuilder()
                .UseKestrel(options =>
                {
                    options.UseHttps(signingCertificate);
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls("https://localhost:5443")
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
   

            host.Run();
        }
    }
}
