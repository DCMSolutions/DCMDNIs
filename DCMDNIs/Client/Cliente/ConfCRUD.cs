﻿using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using DCMDNIs.Shared.Models;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace DCMDNIs.Client.Cliente
{
    public class ConfCRUD
    {
        private readonly HttpClient _cliente;
        private readonly NotificationService _notif;

        public ConfCRUD(HttpClient cliente, NotificationService notif)
        {
            _cliente = cliente;
            _notif = notif;
        }

        public async Task<List<DniDTO>> GetDnis()
        {
            try
            {
                var result = await _cliente.GetAsync("api/dni");
                if (!result.IsSuccessStatusCode)
                {
                    var error = await result.Content.ReadAsStringAsync();
                    ShowNotification(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = error, Duration = 4000 });
                    return new();
                }
                else
                {
                    var rta = await result.Content.ReadFromJsonAsync<List<DniDTO>>();
                    return rta;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DniDTO> GetDniById(int idDni)
        {
            try
            {
                var result = await _cliente.GetAsync($"api/dni/{idDni}");
                if (!result.IsSuccessStatusCode)
                {
                    var error = await result.Content.ReadAsStringAsync();
                    ShowNotification(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = error, Duration = 4000 });
                    return new();
                }
                else
                {
                    var rta = await result.Content.ReadFromJsonAsync<DniDTO>();
                    return rta;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddDni(DniDTO dni)
        {
            try
            {
                var result = await _cliente.PostAsJsonAsync("api/dni" , dni);
                if (!result.IsSuccessStatusCode)
                {
                    var error = await result.Content.ReadAsStringAsync();
                    ShowNotification(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = error, Duration = 4000 });
                    return false;
                }
                else
                {
                    var rta = await result.Content.ReadFromJsonAsync<bool>();
                    return rta;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> EditDni(DniDTO dni)
        {
            try
            {
                var result = await _cliente.PutAsJsonAsync("api/dni", dni);
                if (!result.IsSuccessStatusCode)
                {
                    var error = await result.Content.ReadAsStringAsync();
                    ShowNotification(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = error, Duration = 4000 });
                    return false;
                }
                else
                {
                    var rta = await result.Content.ReadFromJsonAsync<bool>();
                    return rta;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteDni(int idDni)
        {
            try
            {
                var result = await _cliente.DeleteAsync($"api/dni/{idDni}");
                if (!result.IsSuccessStatusCode)
                {
                    var error = await result.Content.ReadAsStringAsync();
                    ShowNotification(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = error, Duration = 4000 });
                    return false;
                }
                else
                {
                    var rta = await result.Content.ReadFromJsonAsync<bool>();
                    return rta;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        void ShowNotification(NotificationMessage message)
        {
            _notif.Notify(message);
        }
    }
}