using Azure.Core;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mono.TextTemplating;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Policy;
using System.Text;
using WebAPI.Models;
//using TravelUp.Models;
using static NuGet.Packaging.PackagingConstants;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TravelUp.Controllers
{
    public class DisplayController : Controller
    {
        
        private Processor _processor;
        
        public ActionResult Segments()
        {
            try
            {
                List<Segment> segment = new List<Segment>();
                _processor = new Processor();
                HttpResponseMessage responseMessage = _processor.GetAPICall("/Segments/GetSegments");
                if (responseMessage.IsSuccessStatusCode)
                {
                    string data = responseMessage.Content.ReadAsStringAsync().Result;
                    segment = JsonConvert.DeserializeObject<List<Segment>>(data);
                }

                return View(segment);
            }
            catch (Exception ex)
            {
                return View("Error", new TravelUp.Models.ErrorViewModel());
            }
        }

        public ActionResult Create()
        {
            try
            {
                List<City> city = new List<City>();
                Processor processor = new Processor();
                city = processor.GetCities();
                List<SelectListItem> citylist = processor.GetSelectedCityList();
                ViewBag.DepartureCity = citylist;
                ViewBag.ArrivalCity = citylist;
                return View();
            }
            catch (Exception)
            {
                return View("Error", new TravelUp.Models.ErrorViewModel());
            }

        }


        [HttpPost]
        public IActionResult Create(Segment segment)
        {
            try
            {
                Processor processor = new Processor();
                segment.ArrivalCityNavigation = processor.GetCity(segment.ArrivalCity);
                segment.DepartureCityNavigation = processor.GetCity(segment.DepartureCity);
                if (segment.ArrivalCity == segment.DepartureCity)
                {
                    ModelState.AddModelError("CityValidation", "Arrival city and Departure city cannot be same");
                    List<SelectListItem> citylist = processor.GetSelectedCityList();
                    ViewBag.DepartureCity = citylist;
                    ViewBag.ArrivalCity = citylist;
                    return View(segment);
                }
                else
                {
                    
                    
                    _processor = new Processor();
                    HttpResponseMessage responseMessage = _processor.PostAPICall("/Segments/CreateSegment", segment);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        TempData["successMsg"] = "Segmant created successfully";
                        string data = responseMessage.Content.ReadAsStringAsync().Result;

                    }

                    return RedirectToAction("Segments");
                }
                
            }
            catch (Exception ex)
            {
                TempData["errorMsg"] = ex.Message;
                return View();
            }
        }

        


        public ActionResult Edit(int id)
        {
            try
            {
                List<City> city = new List<City>();
                Processor processor = new Processor();
                city = processor.GetCities();
                Segment segment = new Segment();
                List<SelectListItem> citylist = processor.GetSelectedCityList();
                ViewBag.DepartureCity = citylist;
                ViewBag.ArrivalCity = citylist;
                _processor = new Processor();
                HttpResponseMessage responseMessage = _processor.GetAPICall("/Segments/GetSegment/", id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    string data = responseMessage.Content.ReadAsStringAsync().Result;
                    segment = JsonConvert.DeserializeObject<Segment>(data);

                }

                return View(segment);
            }
            catch (Exception)
            {
                return View("Error", new TravelUp.Models.ErrorViewModel());
            }


        }

        [HttpPost]
        public ActionResult Edit(Segment segment)
        {
            try
            {
                _processor = new Processor();
                segment.ArrivalCityNavigation = _processor.GetCity(segment.ArrivalCity);
                segment.DepartureCityNavigation = _processor.GetCity(segment.DepartureCity);
                if (segment.ArrivalCity == segment.DepartureCity)
                {
                    ModelState.AddModelError("CityValidation", "Arrival city and Departure city cannot be same");
                    List<SelectListItem> citylist = _processor.GetSelectedCityList();
                    ViewBag.DepartureCity = citylist;
                    ViewBag.ArrivalCity = citylist;
                    return View(segment);
                }
                else
                {
                    HttpResponseMessage responseMessage = _processor.PutAPICall("/Segments/UpdateSegment", segment);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        TempData["successMsg"] = "Segmant Details Updated successfully";
                        string data = responseMessage.Content.ReadAsStringAsync().Result;
                    }
                    return RedirectToAction("Segments");
                }
                

                
            }
            catch (Exception ex)
            {
                TempData["errorMsg"] = ex.Message;
                return View();
            }
            

        }
        

        public ActionResult Delete(int id)
        {
            try
            {
                _processor = new Processor();
                HttpResponseMessage responseMessage = _processor.DeleteAPICall("/Segments/DeleteSegment/", id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    TempData["successMsg"] = "Segment and passangers deleted successfully";
                    return RedirectToAction("Segments");
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["errorMsg"] = ex.Message;
                return View();

            }
            
        }

        //Passanger Details
        public ActionResult PassangerDetails(int id)
        {
            try
            {
                List<Passenger> passangersDetails = new List<Passenger>();
                _processor = new Processor();
                HttpResponseMessage responseMessage = _processor.GetAPICall("/Passanger/GetPassangers/", id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    string data = responseMessage.Content.ReadAsStringAsync().Result;
                    passangersDetails = JsonConvert.DeserializeObject<List<Passenger>>(data);

                }

                return View(passangersDetails);
            }
            catch (Exception)
            {
                return View("Error", new TravelUp.Models.ErrorViewModel());
            }
        }


    }
}
