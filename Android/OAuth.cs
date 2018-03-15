namespace Zebble
{
    using System;
    using System.Threading.Tasks;

    public partial class OAuth
    {
        public Task Authenticate()
        {
            if (Authenticator1 == null && Authenticator2 == null) return Task.CompletedTask;

            UIRuntime.OnNewIntent.Handle(intent =>
            {
                var uri = new Uri(intent.Data.ToString());
                Authenticator1?.OnPageLoading(uri);
                Authenticator2?.OnPageLoading(uri);
            });

            var ui = Authenticator1?.GetUI(UIRuntime.CurrentActivity);
            if (ui == null) ui = Authenticator2?.GetUI(UIRuntime.CurrentActivity);
            UIRuntime.CurrentActivity.StartActivity(ui);

            return Task.CompletedTask;
        }
    }
}