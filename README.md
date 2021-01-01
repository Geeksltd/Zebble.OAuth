[logo]: https://raw.githubusercontent.com/Geeksltd/Zebble.OAuth/master/icon.png "Zebble.OAuth"


## Zebble.OAuth

![logo]

A Zebble plugin to make OAuth authentication request.


[![NuGet](https://img.shields.io/nuget/v/Zebble.OAuth.svg?label=NuGet)](https://www.nuget.org/packages/Zebble.OAuth/)

> This plugin enables you to authenticate users via standard authentication mechanism (OAuth) in Zebble applications and it is implemented for Android and iOS platforms.

<br>


### Setup
* Available on NuGet: [https://www.nuget.org/packages/Zebble.OAuth/](https://www.nuget.org/packages/Zebble.OAuth/)
* Install in your platform client projects.
* Available for iOS, Android.
<br>


### Api Usage

To authenticate users you need to use OAuth object and set some parameters like below:
```csharp
var auth = new Zebble.OAuth("clientId","scope", "authorizeURL", "redirectURL");

await auth.Authenticate();

auth.AuthCompleted.Handle(arg =>
{
    if (arg.IsAuthenticated)
    {
        //send some request
    }
});
```
<br>

#### Facebook Sample
```csharp
var auth = new Zebble.OAuth("appId", "", "https://m.facebook.com/dialog/oauth/", "fb" + "appId" + "://authorize");
await auth.Authenticate();

auth.AuthCompleted.Handle(arg =>
{
    if (arg.IsAuthenticated)
    {
        //send some request
    }
});
```
<br>

#### Microsoft Sample
```csharp
var auth = new Zebble.OAuth("appId", "your secret id", "scope (ex: wl.imap)",
              "https://login.live.com/oauth20_authorize.srf", "msal" + "appId" + "://auth", "https://login.live.com/oauth20_token.srf");
await auth.Authenticate();

auth.AuthCompleted.Handle(arg =>
{
    if (arg.IsAuthenticated)
    {
        //send some request
    }
});
```

<br>

### Properties
| Property     | Type         | Android | iOS | Windows |
| :----------- | :----------- | :------ | :-- | :------ |
| ClientId           | string          | x       | x   |        |
| Scope           | string          | x       | x   |        |
| AuthorizeUrl           | string          | x       | x   |        |
| AccessTokenUrl           | string          | x       | x   |       |
| ConsumerKey           | string          | x       | x   |        |
| ConsumerSecret           | string          | x       | x   |        |
| RequestTokenUrl           | string          | x       | x   |        |



<br>


### Events
| Event             | Type                                          | Android | iOS | Windows |
| :-----------      | :-----------                                  | :------ | :-- | :------ |
| AuthCompleted            | AsyncEvent<AuthCompletedEventArgs&gt;    | x       | x   |        |


<br>


### Methods
| Method       | Return Type  | Parameters                          | Android | iOS | Windows |
| :----------- | :----------- | :-----------                        | :------ | :-- | :------ |
| OAuth1Request         | Task<TResult&gt;         | method -> RequestMethods, url -> string, account -> OAuthAccount, parameters -> Dictionary<string, string&gt;, includeMultipart -> bool| x       | x   |        |
| OAuth2Request         | Task<TResult&gt;         | method -> RequestMethods, url -> string, account -> OAuthAccount, parameters -> Dictionary<string, string&gt;| x       | x   |        |
| AddMultipartData | void | name -> string, data -> string | x | x |
| AddMultipartData | void | name -> string, data -> Stream, mimeType -> string, filename -> string | x | x |
| Authenticate  | Task         | -| x       | x   |        |
