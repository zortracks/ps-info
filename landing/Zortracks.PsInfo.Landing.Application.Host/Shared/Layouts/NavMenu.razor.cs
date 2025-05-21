using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System;
using System.Collections.Generic;
using Zortracks.PsInfo.Application.Host.Shared.Resources;
using Zortracks.PsInfo.Landing.Application.Host.Shared.DataHolders;

namespace Zortracks.PsInfo.Landing.Application.Host.Shared.Layouts {

    public partial class NavMenu {

        #region Injections

        [Inject]
        public IStringLocalizer<NavMenuLocalizations> Localizer { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        #endregion Injections

        #region Data

        private IEnumerable<NavigationElement> NavigationElements { get; set; }

        #endregion Data

        #region Methods

        protected override void OnInitialized() {
            NavigationManager.LocationChanged += NavigationManager_LocationChanged;
            NavigationElements = new List<NavigationElement>()
            {
                new NavigationElement() {
                    Title = Localizer[nameof(NavMenuLocalizations.Menu_Home)],
                    Icon = Icons.Material.Filled.Home,
                    Href = "/"
                },
                new NavigationElement() {
                    Title = "Services",
                    Icon = Icons.Material.Filled.MiscellaneousServices,
                    Href = "/services"
                },
                new NavigationElement() {
                    Title = "Contact",
                    Icon = Icons.Material.Filled.Email,
                    Href = "/contact"
                }
            };
        }

        private void NavigationManager_LocationChanged(object sender, LocationChangedEventArgs e) => StateHasChanged();

        private Color GetColorForMenu(NavigationElement navigationElement) => new Uri(NavigationManager.Uri).LocalPath == navigationElement.Href ? Color.Primary : Color.Default;

        #endregion Methods
    }
}