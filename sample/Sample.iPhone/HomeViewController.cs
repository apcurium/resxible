
using System;
using System.Drawing;

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
            btHome.SetTitle(NSBundle.MainBundle.LocalizedString("Save", null), UIControlState.Normal);
        }
    }
}

