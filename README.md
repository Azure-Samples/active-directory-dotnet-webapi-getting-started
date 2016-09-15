#Getting Started Backend Sample 
This sample illustrates how to authorize access to your backend services or API by validating a token provided by one or more client applications.

To get step by step instructions on how to integrate this sample backend service, you can go to the [Microsoft Identity Portal](https://identity.microsoft.com/Docs/BackendService).

## About the Basic Backend Service 
This sample backend service is written as an ASP.Net Core web API. .Net Core ships with a library for performing JSON Web Token (JWT) Bearer Authentication. Listed below are key highlights for you to review:

1. Configure JWT Bearer Authentication. In startup.cs:
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

2. This code in startup.cs configures JWT Bearer Authentication for all routes

3. In ClaimsController.cs:
```
//We have one action method for returning the claims from the current token. 
public class ClaimsController : Controller 
{ 
    // GET api/values 
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
