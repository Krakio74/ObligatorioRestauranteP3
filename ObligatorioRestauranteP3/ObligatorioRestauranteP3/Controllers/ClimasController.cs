using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObligatorioP3.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ObligatorioP3.Controllers
{
    public class ClimasController : Controller
    {
        private readonly ObligatorioContext _context;
        private readonly IHttpClientFactory _clientFactory;

        public ClimasController(ObligatorioContext context, IHttpClientFactory clientFactory)
        {
            _context = context;
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var clima = await _context.Climas.OrderByDescending(c => c.Fecha).Take(1).ToListAsync();
            return View(clima);
        }

        // GET: Climas/IndexFromAPI
        // Acción para obtener datos de clima desde la API de OpenWeatherMap y guardarlos en la base de datos local
        public async Task<IActionResult> IndexFromAPI()
        {
            // URL de la API de OpenWeatherMap (reemplazar con la ciudad y tu clave API válida)
            var apiUrl = "https://api.openweathermap.org/data/2.5/weather?q=Maldonado&appid=TU_CLAVE_API";
            var httpClient = _clientFactory.CreateClient();

            try
            {
                // Realizar solicitud GET a la API de OpenWeatherMap
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Leer el contenido JSON de la respuesta
                    var content = await response.Content.ReadAsStringAsync();

                    // Deserializar el JSON en un objeto ClimaFromAPI
                    var climaFromAPI = Newtonsoft.Json.JsonConvert.DeserializeObject<ClimaFromAPI>(content);

                    // Crear un objeto Clima usando los datos de la API
                    var clima = new Clima
                    {
                        Fecha = DateTime.Now,
                        Temperatura = climaFromAPI.Main.TempC,
                        DescripcionClima = climaFromAPI.Weather[0].Description  // Aquí usamos Weather[0].Description para la descripción del clima
                    };

                    // Agregar el objeto Clima a la base de datos local
                    _context.Add(clima);
                    await _context.SaveChangesAsync();

                    // Redirigir a la acción Index para mostrar todos los climas
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Manejar el caso de respuesta no exitosa
                    ViewBag.Error = "Error al obtener datos del clima desde la API";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción
                ViewBag.Error = "Error al conectar con la API de clima";
                return View("Error");
            }
        }
    }

    public class ClimaFromAPI
    {
        public MainWeather Main { get; set; }
        public ConditionWeather[] Weather { get; set; }
    }

    public class MainWeather
    {
        public double TempC { get; set; }
    }

    public class ConditionWeather
    {
        public string Description { get; set; }
    }
}
