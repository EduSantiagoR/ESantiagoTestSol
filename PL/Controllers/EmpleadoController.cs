using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44330/api/");
                var taskResponse = client.GetAsync("Empleado");
                taskResponse.Wait();

                var resultService = taskResponse.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<List<ML.Empleado>>();
                    readTask.Wait();
                    empleado.Empleados = new List<ML.Empleado>();
                    foreach(ML.Empleado empleadoResult in readTask.Result)
                    {
                        empleado.Empleados.Add(empleadoResult);
                    }
                }
            }
            return View(empleado);
        }
        [HttpGet]
        public ActionResult Form(int? idEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Area = new ML.Area();
            if(idEmpleado != null)
            {
                empleado = GetById(idEmpleado.Value);
            }
            empleado.Area.Areas = GetAreas();
            return View(empleado);
        }
        [HttpPost]
        public ActionResult Form(ML.Empleado empleado)
        {
            if(empleado.IdEmpleado == 0)
            {
                bool result = Add(empleado);
                ViewBag.Mensaje = result ? "Empleado registrado correctamente." : "Error al registrar el empleado.";
            }
            else
            {
                bool result = Update(empleado);
                ViewBag.Mensaje = result ? "Empleado actualizado correctamente." : "Error al actualizar el empleado.";
            }
            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult Delete(int idEmpleado)
        {
            bool result = DeleteEmpleado(idEmpleado);
            ViewBag.Mensaje = result ? "Empleado eliminado correctamente." : "Error al eliminar el empleado.";
            return PartialView("Modal");
        }

        public ML.Empleado GetById(int idEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44330/api/");
                var taskResponse = client.GetAsync($"Empleado/{idEmpleado}");
                taskResponse.Wait();
                var resultService = taskResponse.Result;
                if(resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Empleado>();
                    readTask.Wait();
                    empleado = readTask.Result;
                }
            }
            return empleado;
        }
        public List<ML.Area> GetAreas()
        {
            List<ML.Area> areas = new List<ML.Area>();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44330/api/");
                var taskResponse = client.GetAsync("Area");
                taskResponse.Wait();
                var resultService = taskResponse.Result;
                if(resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<List<ML.Area>>();
                    readTask.Wait();
                    areas = readTask.Result;
                }
            }
            return areas;
        }
        public bool Add(ML.Empleado empleado)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44330/api/");
                var taskResponse = client.PostAsJsonAsync("Empleado", empleado);
                taskResponse.Wait();
                var resultService = taskResponse.Result;
                return resultService.IsSuccessStatusCode ? true : false;
            }
        }
        public bool Update(ML.Empleado empleado)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44330/api/");
                var taskResponse = client.PutAsJsonAsync($"Empleado/{empleado.IdEmpleado}", empleado);
                taskResponse.Wait();
                var resultService = taskResponse.Result;
                return resultService.IsSuccessStatusCode ? true : false;
            }
        }
        public bool DeleteEmpleado(int idEmpleado)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44330/api/");
                var taskResponse = client.DeleteAsync($"Empleado/{idEmpleado}");
                taskResponse.Wait();
                var resultService = taskResponse.Result;
                return resultService.IsSuccessStatusCode ? true : false;
            }
        }
    }
}