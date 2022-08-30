using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAirLine.Models;
using MVCAirLine.Repository;
using Newtonsoft.Json;

namespace MVCAirLine.Controllers
{
   [Authorize]
    public class AirController : Controller
    {
        //private readonly IDataRepository<AirViewModel> _airDataRepository;
      


        //public AirController(IDataRepository<AirViewModel> airDataRepository)
        //{
        //    _airDataRepository = airDataRepository;
        //    
        //}

        Uri baseuri = new Uri("https://localhost:7159/api");
        HttpClient client = new HttpClient();


        List<AirViewModel> airLineList = new List<AirViewModel>();

        private IMapper _mapper;
        public AirController(IMapper mapper)
        {
            client.BaseAddress = baseuri;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            client.BaseAddress = baseuri;
            HttpResponseMessage response = client.GetAsync(baseuri + "/AirLine").Result;
            if (response.IsSuccessStatusCode)
            {
                string Data = response.Content.ReadAsStringAsync().Result;
                airLineList = JsonConvert.DeserializeObject<List<AirViewModel>>(Data);
                var result = airLineList.OrderBy(e => e.AirLineName);
              //  IEnumerable<AirViewModel> airLine = (IEnumerable<AirViewModel>)_airDataRepository.GetAll();
                return View(result);

            }

            return View(airLineList);
        }

        [Authorize(policy: "writepolicy")]
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Creating AirLines
        /// </summary>
        /// <param name="airView"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateAirLines(AirViewModel airView)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7159/api/AirLine");
                var postTask = client.PostAsJsonAsync<AirViewModel>("AirLine", airView);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            ModelState.AddModelError(string.Empty, "server error");
            return View();

        }

        /// <summary>
        /// Editing the AirLines
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [Authorize(policy: "writepolicy")]
        public ActionResult Edit(int id)
        {
            client.BaseAddress = baseuri;
            HttpResponseMessage response = client.GetAsync(baseuri + "/AirLine").Result;
            string airLineData = response.Content.ReadAsStringAsync().Result;
            airLineList = JsonConvert.DeserializeObject<List<AirViewModel>>(airLineData);
            var Data = airLineList.Where(e => e.AirLineId == id).FirstOrDefault();
            return View(Data);
        }
        [HttpPost]
        public IActionResult Save(AirViewModel airView)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7159/api/");
                var put = client.PutAsJsonAsync($"AirLine?airLineId={airView.AirLineId}", airView);
                put.Wait();
                var result = put.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            ModelState.AddModelError(string.Empty, "server error");
            return View();

        }

        /// <summary>
        /// Deleting the AirLines
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [Authorize(policy: "writepolicy")]
        public IActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7159/api/AirLine/");
                var delete = client.DeleteAsync($"id?id={id}");
                delete.Wait();
                var result = delete.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            ModelState.AddModelError(string.Empty, "server error");
            return View();

        }

        /// <summary>
        /// Searching for the AirLines
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>

        [Authorize(policy: "writepolicy")]
        public ActionResult Search(string searchString)
        {
            List<AirViewModel> airLineList = new List<AirViewModel>();
            HttpResponseMessage response = client.GetAsync(baseuri + "/AirLine/" + searchString).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                airLineList = JsonConvert.DeserializeObject<List<AirViewModel>>(data);
            }
            return View("Index", airLineList);
        }

       

    }
}
