using UAHub.ViewModels;
using Windows.UI.Xaml.Controls;

namespace UAHub.AppFlyouts
{
    public sealed partial class PrivacyFlyout : SettingsFlyout
    {
        public PrivacyViewModel PrivacyViewModel { get; private set; }
        public PrivacyFlyout()
        {
            this.InitializeComponent();
            PrivacyViewModel = new PrivacyViewModel();
            this.DataContext = this;
        }
    }
}
