using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppStudio.Common;
using AppStudio.Common.Actions;
using AppStudio.Common.Commands;
using AppStudio.Common.Navigation;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Menu;
using AppStudio.DataProviders.Html;
using AppStudio.DataProviders.LocalStorage;
using UAHub.Sections;


namespace UAHub.ViewModels
{
    public class MainViewModel : ObservableBase
    {
        public MainViewModel(int visibleItems)
        {
            PageTitle = "UA Hub";
            Section1 = new ListViewModel<LocalStorageDataConfig, MenuSchema>(new Section1Config());
            Section11 = new ListViewModel<LocalStorageDataConfig, MenuSchema>(new Section11Config());
            Section18 = new ListViewModel<LocalStorageDataConfig, MenuSchema>(new Section18Config());
            Section24 = new ListViewModel<LocalStorageDataConfig, MenuSchema>(new Section24Config());
            Section26 = new ListViewModel<LocalStorageDataConfig, HtmlSchema>(new Section26Config(), visibleItems);
            Actions = new List<ActionInfo>();

            if (GetViewModels().Any(vm => !vm.HasLocalData))
            {
                Actions.Add(new ActionInfo
                {
                    Command = new RelayCommand(Refresh),
                    Style = ActionKnownStyles.Refresh,
                    Name = "RefreshButton",
                    ActionType = ActionType.Primary
                });
            }
        }

        public string PageTitle { get; set; }
        public ListViewModel<LocalStorageDataConfig, MenuSchema> Section1 { get; private set; }
        public ListViewModel<LocalStorageDataConfig, MenuSchema> Section11 { get; private set; }
        public ListViewModel<LocalStorageDataConfig, MenuSchema> Section18 { get; private set; }
        public ListViewModel<LocalStorageDataConfig, MenuSchema> Section24 { get; private set; }
        public ListViewModel<LocalStorageDataConfig, HtmlSchema> Section26 { get; private set; }

        public RelayCommand<INavigable> SectionHeaderClickCommand
        {
            get
            {
                return new RelayCommand<INavigable>(item =>
                    {
                        NavigationService.NavigateTo(item);
                    });
            }
        }

        public DateTime? LastUpdated
        {
            get
            {
                return GetViewModels().Select(vm => vm.LastUpdated)
                            .OrderByDescending(d => d).FirstOrDefault();
            }
        }

        public List<ActionInfo> Actions { get; private set; }

        public bool HasActions
        {
            get
            {
                return Actions != null && Actions.Count > 0;
            }
        }

        public async Task LoadDataAsync()
        {
            var loadDataTasks = GetViewModels().Select(vm => vm.LoadDataAsync());

            await Task.WhenAll(loadDataTasks);

            OnPropertyChanged("LastUpdated");
        }

        private async void Refresh()
        {
            var refreshDataTasks = GetViewModels()
                                        .Where(vm => !vm.HasLocalData)
                                        .Select(vm => vm.LoadDataAsync(true));

            await Task.WhenAll(refreshDataTasks);

            OnPropertyChanged("LastUpdated");
        }

        private IEnumerable<DataViewModelBase> GetViewModels()
        {
            yield return Section1;
            yield return Section11;
            yield return Section18;
            yield return Section24;
            yield return Section26;
        }
    }
}
