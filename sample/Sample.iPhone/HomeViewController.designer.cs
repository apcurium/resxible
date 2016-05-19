// WARNING
//
// This file has been generated automatically by Xamarin Studio Community to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Sample.iPhone
{
	[Register ("HomeViewController")]
	partial class HomeViewController
	{
		[Outlet]
		UIKit.UIButton btHome { get; set; }

		[Outlet]
		UIKit.UILabel lblEscape { get; set; }

		[Outlet]
		UIKit.UILabel lblHome { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btHome != null) {
				btHome.Dispose ();
				btHome = null;
			}

			if (lblHome != null) {
				lblHome.Dispose ();
				lblHome = null;
			}

			if (lblEscape != null) {
				lblEscape.Dispose ();
				lblEscape = null;
			}
		}
	}
}
