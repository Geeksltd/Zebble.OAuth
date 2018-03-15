namespace Zebble
{
    using System.Collections.Generic;
    using System.Net;
    using Xamarin.Auth;

    public class OAuthAccount : Account
    {
        public OAuthAccount() : base() { }
        public OAuthAccount(string username) : base(username) { }
        public OAuthAccount(string username, CookieContainer cookies) : base(username, cookies) { }
        public OAuthAccount(string username, IDictionary<string, string> properties) : base(username, properties) { }
        public OAuthAccount(string username, IDictionary<string, string> properties, CookieContainer cookies) : base(username, properties, cookies) { }
    }

    public class AuthCompletedEventArgs
    {
        public OAuthAccount Account { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
