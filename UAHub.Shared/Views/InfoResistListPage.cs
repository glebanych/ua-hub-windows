using Windows.UI.Xaml.Navigation;
using AppStudio.Common;
using AppStudio.DataProviders.Twitter;
using UAHub;
using UAHub.Sections;
using UAHub.ViewModels;

namespace UAHub.Views
{
    public sealed partial class InfoResistListPage : PageBase
    {
        public ListViewModel<TwitterDataConfig, TwitterSchema> ViewModel { get; set; }

        public InfoResistListPage()
        {
            this.ViewModel = new ListViewModel<TwitterDataConfig, TwitterSchema>(new InfoResistConfig());
            this.InitializeComponent();
        }

        protected async override void LoadState(object navParameter)
        {
            await this.ViewModel.LoadDataAsync();
        }

    }
}
