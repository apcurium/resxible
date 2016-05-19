using Foundation;
using UIKit;

namespace Sample.iPhone
{
    public partial class HomeViewController : UIViewController
    {
        public HomeViewController()
            : base("HomeViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            lblHome.Text = NSBundle.MainBundle.LocalizedString("HelloWorld", null);
            lblEscape.Text = NSBundle.MainBundle.LocalizedString("EscapeTest", null);
            btHome.SetTitle(NSBundle.MainBundle.LocalizedString("Save", null), UIControlState.Normal);
        }
    }
}

