Install the packake: Microsoft.AspNetCore.Authentication.Facebook

Add user-secrets
* dotnet user-secrets set Authentication:Facebook:AppId <app-Id>
* dotnet user-secrets set Authentication:Facebook:AppSecret <app-secret>

Add the Facebook middleware in the Configure method in Startup.cs:

app.UseFacebookAuthentication(new FacebookOptions()
{
    AppId = Configuration["Authentication:Facebook:AppId"],
    AppSecret = Configuration["Authentication:Facebook:AppSecret"]
});