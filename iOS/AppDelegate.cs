using Exodus3.Core;
using Exodus3.iOS.Helpers;
using Foundation;
using UIKit;

namespace Exodus3.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations

        private TabController _tabController;
        private MediaPlayerViewController _mediaPlayerController;
        private LoadingViewController _loadingVController;


        public override UIWindow Window
        {
            get;
            set;
        }

        //Public property to access our MainStoryboard.storyboard file
        public UIStoryboard MainStoryboard
        {
            get { return UIStoryboard.FromName("Main", NSBundle.MainBundle); }
        }


        //Creates an instance of viewControllerName from storyboard
        public UIViewController GetViewController(UIStoryboard storyboard, string viewControllerName)
        {
            return storyboard.InstantiateViewController(viewControllerName);
        }


        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // Override point for customization after application launch.
            // If not required for your application you can safely delete this method

            Load();

            return true;
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }

        private async void Load()
        {
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            var vc = GetViewController(MainStoryboard, "LoadingViewController");

            Window.RootViewController = vc;
            Window.MakeKeyAndVisible();

            var fHelper = new FileHelper();
            App.Init(fHelper.GetLocalFilePath(App.DB_FILE_NAME), new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS());

            await App.Database.SyncCloudAndLocal();
           

            var root = GetViewController(MainStoryboard, "TabController");
            root.View.Frame = Window.RootViewController.View.Frame;
            root.View.LayoutIfNeeded();

            UIView.Transition(Window, 0.3, UIViewAnimationOptions.TransitionCrossDissolve, () => { Window.RootViewController = root; }, () => { });
        }
    }
}

