#Getting Started Backend Sample 
This sample shows how to validate tokens in a backend service.

For step by step instructions, see the [Microsoft Identity Portal](https://identity.microsoft.com/Docs/BackendService).

## About the Basic Backend Service 
This sample backend service is an ASP.NET Core web API. .NET Core ships with a library for performing JSON Web Token (JWT) Bearer Authentication. Here are some key highlights from the code:

1.  This code in startup.cs configures JWT Bearer Authentication for all routes:
   ```
var tokenValidationParameters = new TokenValidationParameters();

// If you wish to restrict access to specific organizations, you must configure a list of valid issuers
tokenValidationParameters.ValidateIssuer = false;

var options = new JwtBearerOptions 
{ 
    Audience = Configuration["MicrosoftIdentity:ClientId"], 
    Authority = Configuration["MicrosoftIdentity:Authority"],
    TokenValidationParameters = tokenValidationParameters
}; 

app.UseJwtBearerAuthentication(options); 
   ```

2.  This code in ClaimsController.cs sends the claims in the token back to the caller:
   ```
//We have one action method for returning the claims from the current token. 
public class ClaimsController : Controller 
{ 
    [Authorize] 
    [HttpGet] 
    public object Get() 
    { 
        return User.Claims.Select(x => 
            new  
            { 
                Type = x.Type, 
                Value = x.Value 
            } 
        ); 
    } 
} 
   ```
You can read more about validating tokens [here](https://azure.microsoft.com/en-us/documentation/articles/active-directory-v2-tokens/#validating-tokens).

## Community Help and Support

We leverage [Stack Overflow](http://stackoverflow.com/) to work with the community on supporting Azure Active Directory and its SDKs, including this one. We highly recommend you ask your questions on Stack Overflow (we're all on there). Also browse existing issues to see if someone has asked your question already. 

We recommend you use the "adal" tag so we see your post. Here is the latest Q&A on Stack Overflow for ADAL: [http://stackoverflow.com/questions/tagged/adal](http://stackoverflow.com/questions/tagged/adal)

## Security Reporting

If you find a security issue with our libraries or services please report it to [secure@microsoft.com](mailto:secure@microsoft.com) with as much detail as possible. Your submission may be eligible for a bounty through the [Microsoft Bounty](http://aka.ms/bugbounty) program. Please do not post security issues to GitHub Issues or any other public site. We will contact you shortly upon receiving the information. We encourage you to get notifications of when security incidents occur by visiting [this page](https://technet.microsoft.com/en-us/security/dd252948) and subscribing to Security Advisory Alerts.

## Contributing

All code is licensed under the MIT license and we triage actively on GitHub. We enthusiastically welcome contributions and feedback. You can clone the repo and start contributing now, but check [this document](./contributing.md) first.

## License

Copyright (c) Microsoft Corporation.  All rights reserved. Licensed under the MIT License (the "License"); 

## We Value and Adhere to the Microsoft Open Source Code of Conduct

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
