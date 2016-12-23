# Configure SendGrid for email

Add a new class file in the services folder (EmailSenderOptions.cs)

```{
public string SendGridUser { get; set; }
public string SendGridKey { get; set; }
}```

## Add user secrets
In the package manager console
```  Install-Package SendGrid.NetCore -Pre ```

Then add a command window in the project folder (under src folder)
* dotnet user-secrets set SendGridUser LenoLos
* dotnet user-secrets set SendGridKey 123asdf123

## Add the config to startup.cs

    // Add application services.
    services.AddTransient<IEmailSender, AuthMessageSender>();
    services.AddTransient<ISmsSender, AuthMessageSender>();
    services.Configure<EmailSenderOptions>(Configuration);
    
## Install the SendGrid.NetCore package
In the package manager console
```  Install-Package SendGrid.NetCore -Pre ```

## Configure AuthMessageSender
```
  public AuthMessageSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            var myMessage = new SendGrid.SendGridMessage();
            myMessage.AddTo(email);
            myMessage.From = new System.Net.Mail.MailAddress("Joe@contoso.com", "Joe Smith");
            myMessage.Subject = subject;
            myMessage.Text = message;
            myMessage.Html = message;
            var credentials = new System.Net.NetworkCredential(
                Options.SendGridUser,
                Options.SendGridKey);
            // Create a Web transport for sending email.
            var transportWeb = new SendGrid.Web(credentials);
            return transportWeb.DeliverAsync(myMessage);
        }
```

## Uncommet these lines in AccountController
```
           await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
            //await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation(3, "User created a new account with password.");
            return RedirectToLocal(returnUrl);
```


Remember to comment out the automatic login
                   ```
				    // await _signInManager.SignInAsync(user, isPersistent: false);
					```


## Uncomment the lines in view 
In Views/Account/ForgotPassword.cshtml, to get a working view for setting a new password


## Edit Login action in Accountcontroller
```
    if (ModelState.IsValid)
    {
        // Require the user to have a confirmed email before they can log on.
        var user = await _userManager.FindByNameAsync(model.Email);
        if (user != null)
        {
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError(string.Empty, "You must have a confirmed email to log in.");
                return View(model);
            }
        }

```



