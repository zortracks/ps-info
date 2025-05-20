using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Zortracks.PsInfo.Models;

namespace Zortracks.PsInfo.Application.Host.Areas.Contact.Pages {

    public partial class ContactPage {

        #region Injections

        [Inject]
        public IHttpClientFactory HttpClientFactory { get; set; }

        #endregion Injections

        #region Components

        private MudForm Form { get; set; }

        #endregion Components

        #region Data

        private ContactRequestModel ContactRequestModel { get; set; } = new ContactRequestModel();

        #endregion Data

        #region Methods

        private async Task SendContactRequestAsync() {
            await Form.Validate();

            if (Form.IsValid) {
                var httpClient = HttpClientFactory.CreateClient("apis");
                var response = await httpClient.PostAsJsonAsync("/contact-requests", ContactRequestModel);
            }
        }

        #endregion Methods
    }
}