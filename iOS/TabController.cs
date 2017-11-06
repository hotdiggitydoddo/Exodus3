using System;
using UIKit;

namespace Exodus3.iOS
{
    public class TabController : UITabBarController
    {
        private UIViewController _latestSemonTab, _currentSeriesTab, _browseSeriesTab;

        public TabController()
        {
            _latestSemonTab = new UIViewController();
            _latestSemonTab.View.BackgroundColor = new UIColor(47f / 255f, 43f / 255f, 33f / 255f, 1);
            _latestSemonTab.TabBarItem = new UITabBarItem("Latest Sermon", UIImage.FromFile("feed.png"), 0);

            _currentSeriesTab = new UIViewController();
            _currentSeriesTab.View.BackgroundColor = new UIColor(47f / 255f, 43f / 255f, 33f / 255f, 1);
            _currentSeriesTab.TabBarItem = new UITabBarItem("Current Series", UIImage.FromFile("list.png"), 0);

            _browseSeriesTab = new UIViewController();
            _browseSeriesTab.View.BackgroundColor = new UIColor(47f / 255f, 43f / 255f, 33f / 255f, 1);
            _browseSeriesTab.TabBarItem = new UITabBarItem("Browse Library", UIImage.FromFile("books.png"), 0);

            var tabs = new UIViewController[] 
            {
                _latestSemonTab, _currentSeriesTab, _browseSeriesTab
            };

            ViewControllers = tabs;
            this.TabBar.TintColor = new UIColor(251f / 255f, 195f / 255f, 69f / 255f, 1);
            this.TabBar.BackgroundImage = new UIImage();
            TabBar.BackgroundColor = new UIColor(47f / 255f, 43f / 255f, 33f / 255f, 1);
        }
    }
}
