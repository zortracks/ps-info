using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System;
using System.Collections.Generic;
using Zortracks.PsInfo.Application.Host.Shared.DataHolders;
using Zortracks.PsInfo.Application.Host.Shared.Resources;

namespace Zortracks.PsInfo.Application.Host.Shared.Layouts {

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
            NavigationElements = new List<NavigationElement>()
            {
                new NavigationElement() {
                    Title = Localizer[nameof(NavMenuLocalizations.Menu_Home)],
                    Icon = Icons.Material.Filled.Home,
                    Href = "/"
                }
            };
        }

        private Color GetColorForMenu(NavigationElement navigationElement) => new Uri(NavigationManager.Uri).LocalPath == navigationElement.Href ? Color.Primary : Color.Default;

        #endregion Methods
    }
}