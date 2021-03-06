#Bidmytrip.Common.Workbench.Client

Azure Blockchain Workbench REST API
- API version: v1

The Azure Blockchain Workbench REST API is a Workbench extensibility point, which allows developers to create and manage blockchain applications, manage users and organizations within a consortium, integrate blockchain applications into services and platforms, perform transactions on a blockchain, and retrieve transactional and contract data from a blockchain.

Unlike clients for other languages, this client was not auto-generated, it was developed specifically for a mobile sample written by the Xamarin.  It incorporates Microsoft's ADAL library for authentication and additional resiliency with the Polly library. 


## Features
1. Polly.NET (https://github.com/App-vNext/Polly) a .NET resiliency library with exponential retry backing 
    - Currently, the library is used to handle TaskCancelledExceptions or timeouts from the server. 
2. Singleton Implementation - just include the library in your project and reference it from anywhere 
3. Thread-safe: only a single HttpClient is used
4. Event Handlers you can subscribe to: 
    - For expired access tokens (so you can refresh and get a new one)
    - When Exceptions are thrown 
5. Exception handling - JsonException, TaskCancelledException, HttpRequestException  
6. Generic GET and POST methods with C# Generics

## Getting Started

### 1. Setting Optional Parameters for Timeout and RetryCount

There are two parameters that can be set on the Gateway API: CLIENT_API_TIMEOUT and POLLY_RETRY_COUNT. 
The first one is for setting the default timeout interval of the HttpClient. The second is the number of times an API call will retry after at TaskCancelledException before giving up. 

### 2. Obtaining an Authentication Token via ADAL

Using the .NET ADAL library for Xamarin.iOS, Xamarin.Android, or .NET - authenticate with the Active Directory and retrieve then authentication token. 

Workbench doesn't require an API Key. It knows who the request is coming from by simply including the authentication token from ADAL in the authorization request header. 

For Xamarin Apps (Xamarin.Forms, Xamarin.iOS and Xamarin.Android) please follow this tutorial: 
https://blog.xamarin.com/put-adal-xamarin-forms/

### 3. Setting the Authentication Token on the API
Once you obtain the authentication token, use the following method to set it on the Gateway API instance: 

```GatewayApi.Instance.SetAuthToken(<yourTokenAsString>)```

### 4. Using the Gateway API
Refer to the swagger documentation of the Workbench API on getting started. Method names will be similar to the api documentation


