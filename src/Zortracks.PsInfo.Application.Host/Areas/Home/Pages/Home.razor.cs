using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zortracks.PsInfo.Application.Host.Areas.Home.DataHolders;
using Zortracks.PsInfo.Application.Host.Areas.Home.Resources;

namespace Zortracks.PsInfo.Application.Host.Areas.Home.Pages {

    public partial class Home {

        #region Injections

        [Inject]
        public IStringLocalizer<HomeLocalizations> Localizer { get; set; }

        #endregion Injections

        #region Data

        private IEnumerable<HomeCarouselDataHolder> HomeCarouselDataHolders { get; set; }
        private IEnumerable<ReadyTouseDataHolder> ReadyTouseDataHolders { get; set; }

        #endregion Data

        #region Methods

        protected override Task OnInitializedAsync() {
            HomeCarouselDataHolders = [
                new HomeCarouselDataHolder {
                    Title = Localizer[nameof(HomeLocalizations.Outsourcing_Title)],
                    Description = Localizer[nameof(HomeLocalizations.Outsourcing_Description)],
                    ImageUrl = "/img/outsourcing.jpg",
                    Color = Color.Warning
                },
                new HomeCarouselDataHolder() {
                    Title = Localizer[nameof(HomeLocalizations.Equipment_Title)],
                    Description = Localizer[nameof(HomeLocalizations.Equipment_Description)],
                    ImageUrl = "/img/network.jpg",
                    Color = Color.Info
                },
                new HomeCarouselDataHolder() {
                    Title = Localizer[nameof(HomeLocalizations.Backup_Title)],
                    Description = Localizer[nameof(HomeLocalizations.Backup_Description)],
                    ImageUrl = "/img/backup.jpg",
                    Color = Color.Success
                },
                new HomeCarouselDataHolder() {
                    Title = Localizer[nameof(HomeLocalizations.Security_Title)],
                    Description = Localizer[nameof(HomeLocalizations.Security_Description)],
                    ImageUrl = "/img/firewall.jpg",
                    Color = Color.Error
                },
            ];
            ReadyTouseDataHolders = [
                new ReadyTouseDataHolder() {
                    Title = Localizer[nameof(HomeLocalizations.Section_ReadyToUse_PhoneAssist_Title)],
                    Description = Localizer[nameof(HomeLocalizations.Section_ReadyToUse_PhoneAssist_Description)],
                },
                new ReadyTouseDataHolder() {
                    Title = Localizer[nameof(HomeLocalizations.Section_ReadyToUse_Outsourcing_Title)],
                    Description = Localizer[nameof(HomeLocalizations.Section_ReadyToUse_Outsourcing_Description)],
                },
                new ReadyTouseDataHolder() {
                    Title = Localizer[nameof(HomeLocalizations.Section_ReadyToUse_MoveToSite_Title)],
                    Description = Localizer[nameof(HomeLocalizations.Section_ReadyToUse_MoveToSite_Description)],
                },
            ];

            return base.OnInitializedAsync();
        }

        #endregion Methods
    }
}