using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.InteropServices;
using WebAPI.Models;

namespace TravelUp.Controllers
{
    public class Processor
    {

        Uri baseAddress = new Uri("https://localhost:7177/api");
        public readonly HttpClient _httpClient;

        public Processor()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        public List<City> GetCities()
        {
            HttpResponseMessage responseMessage = _httpClient.GetAsync(_httpClient.BaseAddress + "/Cities/GetCities").Result;
            string cityresponse = responseMessage.Content.ReadAsStringAsync().Result;
            List<City> cities = JsonConvert.DeserializeObject<List<City>>(cityresponse);
            return cities;
        }

        public City GetCity(int id)
        {
            HttpResponseMessage responseMessage = _httpClient.GetAsync(_httpClient.BaseAddress + "/Cities/GetCity/" + id).Result;
            string cityresponse = responseMessage.Content.ReadAsStringAsync().Result;
            City city = JsonConvert.DeserializeObject<City>(cityresponse);
            return city;
        }

        public HttpResponseMessage GetAPICall(string url, [Optional] int id)
        {
            //id = 0   -- calling API without id parameter
            //id = non zero -- calling API for specific Id parameter
            HttpResponseMessage responseMessage;
            if (id ==0 )
            {
                responseMessage = _httpClient.GetAsync(_httpClient.BaseAddress + url).Result;
            }
            else
            {
                responseMessage = _httpClient.GetAsync(_httpClient.BaseAddress + url +id).Result;
            }
        
            return responseMessage;
        }

        public HttpResponseMessage PostAPICall(string url, Segment segment)
        {
           
            HttpResponseMessage responseMessage;
            responseMessage = _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + url, segment).Result;
            return responseMessage;
        }


        public HttpResponseMessage PutAPICall(string url, Segment segment)
        {
            
            HttpResponseMessage responseMessage;
            responseMessage =  _httpClient.PutAsJsonAsync(_httpClient.BaseAddress + url, segment).Result;
            return responseMessage;
        }

        public HttpResponseMessage DeleteAPICall(string url, int id)
        {

            HttpResponseMessage responseMessage;
            responseMessage = _httpClient.DeleteAsync(_httpClient.BaseAddress + url +id).Result;
            return responseMessage;
        }

        public List<SelectListItem> GetSelectedCityList()
        {
            List<City>  city = GetCities();
            List<SelectListItem> citylist = new SelectList(city.ToDictionary(x => x.CityId, x => x.CityName), "Key", "Value").ToList();
            return citylist;
        }


    }
}
