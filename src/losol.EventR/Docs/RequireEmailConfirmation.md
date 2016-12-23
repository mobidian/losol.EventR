# Require new users to confirm their email

In Startup.cs we need a new config part on the line adding Identity
```    services.AddIdentity<ApplicationUser, IdentityRole>(config =>
        {
            config.SignIn.RequireConfirmedEmail = true;
        })
		```