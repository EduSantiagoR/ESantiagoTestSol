using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SL.Controllers
{
    [RoutePrefix("api/Empleado")]
    public class EmpleadoController : ApiController
    {
        private class Result
        {
            public bool Correct { get; set; }
            public string Message { get; set; }
        }
        // GET: api/Empleado
        /// <summary>
        /// Obtiene un listado de empleados.
        /// </summary>
        /// <remarks>
        /// Obtiene un listado con los empleados registrados en la BD.
        /// </remarks>
        /// <response code="200">OK. Devuelve el listado de los empleados registrados en la BD.</response>        
        /// <response code="400">BadRequest. No se han podido recuperar los empleados de la BD.</response>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(ML.Empleado))]
        public IHttpActionResult GetAll()
        {
            List<ML.Empleado> empleados = BL.Empleado.GetAll();
            if (empleados != null)
            {
                return Content(HttpStatusCode.OK, empleados);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, empleados);
            }
        }

        // GET: api/Empleado/{idEmpleado}
        /// <summary>
        /// Obtiene un empleado registrado en la BD.
        /// </summary>
        /// <remarks>
        /// Obtiene un empleado que se encuentre registrado en la base de datos.
        /// </remarks>
        /// <param name="idEmpleado">ID del empleado que se desea consultar</param>
        /// <response code="200">OK. Devuelve la información de un empleado que se encuentre registrado en la BD.</response>        
        /// <response code="400">BadRequest. No se han podido recuperar la información del empleado.</response>
        [HttpGet]
        [Route("{idEmpleado}")]
        [ResponseType(typeof(ML.Empleado))]
        public IHttpActionResult GetById(int idEmpleado)
        {
            ML.Empleado empleado = BL.Empleado.GetById(idEmpleado);
            if (empleado.IdEmpleado != 0)
            {
                return Content(HttpStatusCode.OK, empleado);
            }
            else
            {
                object error = new
                {
                    Message = "Error al recuperar el empleado."
                };
                return Content(HttpStatusCode.BadRequest, error);
            }
        }

        // POST: api/Empleado
        /// <summary>
        /// Registra un nuevo empleado en la BD.
        /// </summary>
        /// <remarks>
        /// Registra un nuevo empleado en la BD.
        /// </remarks>
        /// <param name="empleado">Empleado a registrar en la BD</param>
        /// <response code="200">OK. Empleado registrado correctamente en la BD.</response>        
        /// <response code="400">BadRequest. Error al intentar registrar el empleado.</response>
        [HttpPost]
        [Route("")]
        [ResponseType (typeof(Result))]
        public IHttpActionResult Add([FromBody] ML.Empleado empleado)
        {
            bool correct = BL.Empleado.Add(empleado);
            if (correct)
            {
                object result = new
                {
                    Correct = correct,
                    Message = "Empleado registrado correctamente."
                };
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                object result = new
                {
                    Correct = correct,
                    Message = "Error al intentar registrar el empleado."
                };
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        // PUT: api/Empleado/{idEmpleado}
        /// <summary>
        /// Actualiza un empleado en la BD.
        /// </summary>
        /// <remarks>
        /// Actualiza un empleado en la BD, indicando su id en el header y la información en el body.
        /// </remarks>
        /// <param name="idEmpleado">ID del empleado que se desea actualizar</param>
        /// <param name="empleado">Empleado a actualizar en la BD</param>
        /// <response code="200">OK. Empleado actualizado correctamente en la BD.</response>        
        /// <response code="400">BadRequest. Error al intentar actualizar el empleado.</response>
        [HttpPut]
        [Route("{idEmpleado}")]
        [ResponseType(typeof(Result))]
        public IHttpActionResult Update(int idEmpleado, [FromBody] ML.Empleado empleado)
        {
            empleado.IdEmpleado = idEmpleado;
            bool correct = BL.Empleado.Update(empleado);
            if (correct)
            {
                object result = new
                {
                    Correct = correct,
                    Message = "Empleado actualizado correctamente."
                };
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                object result = new
                {
                    Correct = correct,
                    Message = "Error al intentar actualizar el empleado."
                };
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        // DELETE: api/Empleado/{idEmpleado}
        /// <summary>
        /// Elimina un empleado en la BD.
        /// </summary>
        /// <remarks>
        /// Elimina un empleado registrado en la BD indicando el ID en el header.
        /// </remarks>
        /// <param name="idEmpleado">ID del empleado que se desea eliminar</param>
        /// <response code="200">OK. Empleado eliminado correctamente en la BD.</response>        
        /// <response code="400">BadRequest. Error al intentar eliminar el empleado.</response>
        [HttpDelete]
        [Route("{idEmpleado}")]
        [ResponseType(typeof(Result))]
        public IHttpActionResult Delete(int idEmpleado)
        {
            bool correct = BL.Empleado.Delete(idEmpleado);
            if (correct)
            {
                object result = new
                {
                    Correct = correct,
                    Message = "Empleado eliminado correctamente."
                };
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                object result = new
                {
                    Correct = correct,
                    Message = "Error al intentar eliminar el empleado."
                };
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
    }
}
