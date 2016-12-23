# Make Kestrel use SSL connections


## Make self signed SSL certificate
1. Run powershell as administrator (Start --> Type in 'powershell' --> Right click and run as administrator)
1. Navigate to your project folder
2. Run this command: ``` New-SelfSignedCertificate -certstorelocation cert:\localmachine\my -dnsname self-signed-cert ```
3. Take a copy of the thumbprint
4. Run ``` $pwd = ConvertTo-SecureString -String "N1cePa$$w0rd" -Force -AsPlainText ```
5. Run ``` Export-PfxCertificate -cert cert:\localMachine\my\###THUMBRINT### -FilePath selfsignedcert.pfx -Password $pwd ```


## Add https capabilities to Kestrel 
* Open Nuget Package manager, and install the package "Microsoft.AspNetCore.Server.Kestrel.Https"
* In program.cs
```
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
```
* To open browser on run in Visual Studio: Go to Project --> _your_solution_ properties --> Debug. Set Launch Url to: https://localhost:5443

## Require SSL
Add the following code to ConfigureServices in Startup.cs:
```
	services.Configure<MvcOptions>(options =>
	{
		options.Filters.Add(new RequireHttpsAttribute ());
	});
```

Add the [RequireHttps] attribute to all controllers. This will  redirect HTTP GET requests to HTTPS, and reject HTTP POSTs.

