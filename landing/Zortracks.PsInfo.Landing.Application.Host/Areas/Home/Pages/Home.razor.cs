using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Zortracks.PsInfo.Application.Host.Areas.Home.Resources;

namespace Zortracks.PsInfo.Landing.Application.Host.Areas.Home.Pages {

    public partial class Home {

        #region Injections

        [Inject]
        public IStringLocalizer<HomeLocalizations> Localizer { get; set; }

        #endregion Injections
    }
}