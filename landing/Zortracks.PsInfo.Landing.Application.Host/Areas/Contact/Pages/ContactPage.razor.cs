using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Zortracks.PsInfo.Landing.Models;

namespace Zortracks.PsInfo.Landing.Application.Host.Areas.Contact.Pages {

    public partial class ContactPage {

        #region Injections

        [Inject]
        public IHttpClientFactory HttpClientFactory { get; set; }
        [Inject]
        public ISnackbar Snackbar { get; set; }

        #endregion Injections

        #region Components

        private MudForm Form { get; set; }

        #endregion Components

        #region Data
        private bool IsLoading { get; set; }
        private ContactRequestModel ContactRequestModel { get; set; } = new ContactRequestModel();

        #endregion Data

        #region Methods

        private async Task SendContactRequestAsync() {
            await Form.Validate();

            if (Form.IsValid) {
                IsLoading = true;

                var httpClient = HttpClientFactory.CreateClient("apis");
                var isSuccess = false;

                try {
                    var response = await httpClient.PostAsJsonAsync("/contact-requests", ContactRequestModel);

                    isSuccess = response.IsSuccessStatusCode;
                }
                catch { isSuccess = false; }
                finally {
                    if (isSuccess)
                        Snackbar.Add("Demande de contact envoyée.", Severity.Success);
                    else
                        Snackbar.Add("Echec de l'envoie de la demande de contact.", Severity.Error);

                    IsLoading = false;
                }
            }
        }

        #endregion Methods
    }
}