using AutoMapper;
using Datos;
using Datos.DTO;
using InterfazMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;

namespace InterfazMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public HomeController(IHttpClientFactory httpClientFactory, IMapper _mp)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7167/api");
            _mapper = _mp;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/api/tareasPendientes/ListarTareas");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tareas = JsonConvert.DeserializeObject<IEnumerable<Tarea>>(content);
                return View("Index", tareas);
            }
            return View(new List<Tarea>());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Tarea dto)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/tareasPendientes/GuardarTareas", content);
                
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear la tarea");
                }
            }
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"/api/tareasPendientes/VerTarea?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tarea = JsonConvert.DeserializeObject<Tarea>(content);
                return View(tarea);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var response = await _httpClient.GetAsync($"/api/tareasPendientes/VerTarea?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tarea = JsonConvert.DeserializeObject<Tarea>(content);
                return View(tarea);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Tarea dto)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"/api/tareasPendientes/EditarTarea?id={id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new { id });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al actualizar la tarea");
                }
            }
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/tareasPendientes/Eliminar/{id}");
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Error al eliminar la tarea,";
                return RedirectToAction("Index");
            }
        }
    }
}