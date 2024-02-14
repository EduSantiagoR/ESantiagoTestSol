using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SL.Controllers
{
    [RoutePrefix("api/Area")]
    public class AreaController : ApiController
    {
        // GET: api/Area
        /// <summary>
        /// Obtiene las áreas registradas.
        /// </summary>
        /// <remarks>
        /// Obtiene un listado con las áreas registradas en la base de datos.
        /// </remarks>
        /// <response code="200">OK. Devuelve el listado de las áreas registradas.</response>        
        /// <response code="400">BadRequest. No se han podido recuperar las áreas.</response>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(ML.Area))]
        public IHttpActionResult GetAll()
        {
            List<ML.Area> areas = BL.Area.GetAll();
            if(areas != null)
            {
                return Content(HttpStatusCode.OK, areas);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, areas);
            }
        }
    }
}
