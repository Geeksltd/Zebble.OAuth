namespace Zebble
{
    using System.Threading.Tasks;
    using UIKit;

    public partial class OAuth
    {
        public readonly AsyncEvent UICompeletion = new AsyncEvent();

        public Task Authenticate()
        {
            if (Authenticator1 == null && Authenticator2 == null) return Task.CompletedTask;

            UIRuntime.OnOpenUrl.Handle(url =>
            {
                var uri = new System.Uri(url.AbsoluteString);
                Authenticator1?.OnPageLoading(uri);
                Authenticator2?.OnPageLoading(uri);
            });

            var ui = Authenticator1?.GetUI();
            if (ui == null) ui = Authenticator2?.GetUI();
            (UIRuntime.NativeRootScreen as UIViewController)?.PresentViewController(ui, animated: true, completionHandler: () => UICompeletion.Raise());

            return Task.CompletedTask;
        }
    }
}