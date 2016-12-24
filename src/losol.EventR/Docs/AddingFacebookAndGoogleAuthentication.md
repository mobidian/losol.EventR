# Authentication with Facebook and Google

## Facebook authentication
Install the package: 
dotnet install Microsoft.AspNetCore.Authentication.Facebook

Add user-secrets
* dotnet user-secrets set Authentication:Facebook:AppId <app-Id>
* dotnet user-secrets set Authentication:Facebook:AppSecret <app-secret>

Add the Facebook middleware in the Configure method in Startup.cs:

app.UseFacebookAuthentication(new FacebookOptions()
{
    AppId = Configuration["Authentication:Facebook:AppId"],
    AppSecret = Configuration["Authentication:Facebook:AppSecret"]
});


## Google authentication
Install google authentication package: 
dotnet install Microsoft.AspNetCore.Authentication.Google

Login into https://console.developers.google.com/projectselector/apis/library, create a project and enable a "Google+ API". Press "Create credentials".

Redirect uri: https://localhost:5443/signin-google

* dotnet user-secrets set Authentication:Google:ClientID <client_id>
* dotnet user-secrets set Authentication:Google:ClientSecret <client-secret>

In startup.cs 
app.UseGoogleAuthentication(new GoogleOptions()
{
    ClientId = Configuration["Authentication:Google:ClientId"],
    ClientSecret = Configuration["Authentication:Google:ClientSecret"]
});
