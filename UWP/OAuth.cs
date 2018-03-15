namespace Zebble
{
    using System;
    using System.Threading.Tasks;

    public partial class OAuth
    {
        public Task Authenticate()
        {
            if (Authenticator1 == null && Authenticator2 == null) return Task.CompletedTask;

            UIRuntime.OnActivated.Handle(arg =>
            {
                //var uri = new Uri(url.AbsoluteString);
                //Authenticator1?.OnPageLoading(uri);
                //Authenticator2?.OnPageLoading(uri);
            });

            var ui = Authenticator1?.GetUI();
            if (ui == null) ui = Authenticator2?.GetUI();
            var currentFrame = Windows.UI.Xaml.Window.Current.Content as Windows.UI.Xaml.Controls.Frame;
            currentFrame.Navigate(ui);

            return Task.CompletedTask;
        }
    }
}