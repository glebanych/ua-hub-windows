using System;
using System.Collections.Generic;
using AppStudio.Common.Actions;
using AppStudio.Common.Commands;
using AppStudio.Common.Navigation;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Twitter;
using UAHub.Config;
using UAHub.ViewModels;

namespace UAHub.Sections
{
    public class Section23Config : SectionConfigBase<TwitterDataConfig, TwitterSchema>
    {
        public override DataProviderBase<TwitterDataConfig, TwitterSchema> DataProvider
        {
            get
            {
                return new TwitterDataProvider(new TwitterOAuthTokens
                {
                    ConsumerKey = "0J33wWLr7wOmJ9Xe0O6ovrntX",
                        ConsumerSecret = "nMdNzAGeX9RLw1N1Rbb5LoQ7n4VoMevFnJp0TkRxDzzMDQEJSH",
                        AccessToken = "2169550185-htDfdCZImjyMLR9MsmV7a4lFIkodAQp5QEfL0sa",
                        AccessTokenSecret = "GrukNOFTI71CBdM9ONEocWwKxzHAyLwrEJbvFP997Yx7W"

                });
            }
        }

        public override TwitterDataConfig Config
        {
            get
            {
                return new TwitterDataConfig
                {
                    QueryType = TwitterQueryType.User,
                    Query = @"forest_brother"
                };
            }
        }

        public override NavigationInfo ListNavigationInfo
        {
            get 
            {
                return NavigationInfo.FromPage("Section23ListPage");
            }
        }


        public override ListPageConfig<TwitterSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<TwitterSchema>
                {
                    Title = "Лісовий Брат",

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.UserName.ToSafeString();
                        viewModel.SubTitle = item.Text.ToSafeString();
                        viewModel.Description = "";
                        viewModel.Image = item.UserProfileImageUrl.ToSafeString();

                    },
                    NavigationInfo = (item) =>
                    {
                        return new NavigationInfo
                        {
                            NavigationType = NavigationType.DeepLink,
                            TargetUri = new Uri(item.Url)
                        };
                    }
                };
            }
        }

        public override DetailPageConfig<TwitterSchema> DetailPage
        {
            get { return null; }
        }

        public override string PageTitle
        {
            get { return "Лісовий Брат"; }
        }

    }
}
