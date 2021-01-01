namespace Zebble
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xamarin.Auth;

    public partial class OAuth
    {
        OAuth1Authenticator Authenticator1;
        OAuth2Authenticator Authenticator2;

        OAuth1Request Request1;
        OAuth2Request Request2;

        public readonly AsyncEvent<AuthCompletedEventArgs> AuthCompleted = new AsyncEvent<AuthCompletedEventArgs>();

        public string ClientId
        {
            get
            {
                if (Authenticator2 != null)
                    return Authenticator2.ClientId;
                return string.Empty;
            }

        }
        public string ClientSecret
        {
            get
            {
                if (Authenticator2 != null)
                    return Authenticator2.ClientSecret;
                return string.Empty;
            }
        }
        public string Scope
        {
            get
            {
                if (Authenticator2 != null)
                    return Authenticator2.Scope;
                return string.Empty;
            }
        }

#if ANDROID || IOS
        public string AuthorizeUrl
        {
            get
            {
                if (Authenticator2 != null)
                    return Authenticator2.AuthorizeUrl.AbsoluteUri;
                return string.Empty;
            }
        }
        public string AccessTokenUrl
        {
            get
            {
                if (Authenticator2 != null)
                    return Authenticator2.AccessTokenUrl.AbsoluteUri;
                return string.Empty;
            }
        }

#endif

        public OAuth(string clientId, string scope, string authorizeURL, string redirectURL)
        {
            Authenticator2 = new OAuth2Authenticator(clientId, scope, new Uri(authorizeURL), new Uri(redirectURL));
            Authenticator2.Completed += Authenticator_Completed;
        }

        public OAuth(string clientId, string clientSecret, string scope, string authorizeURL, string redirectURL, string accessTokenURL)
        {
            Authenticator2 = new OAuth2Authenticator(clientId, clientSecret, scope, new Uri(authorizeURL), new Uri(redirectURL), new Uri(accessTokenURL));
            Authenticator2.Completed += Authenticator_Completed;
        }

        public OAuth(string consumerKey, string consumerSecret, string requestTokenUrl, string authorizeUrl, string accessTokenUrl, string callbackUrl, int version = 1)
        {
            Authenticator1 = new OAuth1Authenticator(consumerKey, consumerSecret, new Uri(requestTokenUrl),
                new Uri(authorizeUrl), new Uri(accessTokenUrl), new Uri(callbackUrl));
            Authenticator1.Completed += Authenticator_Completed;
        }

        public async Task<TResult> OAuth2Request<TResult>(RequestMethods method, string url, OAuthAccount account, Dictionary<string, string> parameters = null)
        {
            var stringMethod = Enum.GetName(typeof(RequestMethods), method);
            Request2 = new OAuth2Request(stringMethod, new Uri(url), parameters, account);

            var response = await Request2.GetResponseAsync();
            if (response != null)
            {
                string resultJson = response.GetResponseText();
                var result = JsonConvert.DeserializeObject<TResult>(resultJson);
                return result;
            }

            return Activator.CreateInstance<TResult>();
        }

        public async Task<TResult> OAuth1Request<TResult>(RequestMethods method, string url, OAuthAccount account, Dictionary<string, string> parameters = null, bool includeMultipart = false)
        {
            var stringMethod = Enum.GetName(typeof(RequestMethods), method);
            Request1 = new OAuth1Request(stringMethod, new Uri(url), parameters, account, includeMultipart);

            var response = await Request1.GetResponseAsync();
            if (response != null)
            {
                string resultJson = response.GetResponseText();
                var result = JsonConvert.DeserializeObject<TResult>(resultJson);
                return result;
            }

            return Activator.CreateInstance<TResult>();
        }

        public void AddMultipartData(string name, string data)
        {
            Request1?.AddMultipartData(name, data);
            Request2?.AddMultipartData(name, data);
        }

        public void AddMultipartData(string name, System.IO.Stream data, string mimeType = "", string filename = "")
        {
            Request1?.AddMultipartData(name, data, mimeType, filename);
            Request2?.AddMultipartData(name, data, mimeType, filename);
        }

        void Authenticator_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            AuthCompleted.Raise(new AuthCompletedEventArgs { Account = (OAuthAccount)e.Account, IsAuthenticated = e.IsAuthenticated });
        }
    }
}
