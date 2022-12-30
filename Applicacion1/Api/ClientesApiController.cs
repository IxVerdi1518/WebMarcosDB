using Applicacion1.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Applicacion1.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesApiController : ControllerBase
    {
		// GET: api/<ClientesApiController>/<ClientesApiController>
		[HttpGet]
        public IEnumerable<Cliente> Get()
        {
            var cliente = new List<Cliente>
            {
                new Cliente { Identificacion = "1", Nombres = "Esteban", Apellidos = "Flores"},
                new Cliente { Identificacion = "2", Nombres = "Johanna", Apellidos = "Gamez"},
            };

            return cliente;
        }

		// GET api/<ClientesApiController>/<ClientesApiController>
		[HttpGet]
        [Route("[controller]/listarID/{id}")]
        public Cliente Get(int id)
        {
            var cliente = new List<Cliente>
            {
                new Cliente { Identificacion = "1", Nombres = "Esteban", Apellidos = "Flores"},
                new Cliente { Identificacion = "2", Nombres = "Johanna", Apellidos = "Gamez"},
            };
            
            return cliente.ElementAt(id);
        }

		// POST api/<ClientesApiController>/<ClientesApiController>
		[HttpPost]
        [Route("[controller]/guardar/")]
        public Cliente Post([FromBody] Cliente cliente)
        {
            Console.WriteLine("Post Method");
            Console.WriteLine("Identificacion " + cliente.Identificacion);
            Console.WriteLine("Apellidos" + cliente.Apellidos );
            Console.WriteLine("Nombres" + cliente.Nombres);
            return cliente;
        }

        //PUT api/<ClientesApiController>/<ClientesApiController>
        [HttpPut]
        [Route("[controller]/update/{id}")]
        public void Put(int id, [FromBody] Cliente cliente)
        {
            var cliente1 = new List<Cliente>
            {
                new Cliente { Identificacion = "1", Nombres = "Esteban", Apellidos = "Flores"},
                new Cliente { Identificacion = "2", Nombres = "Johanna", Apellidos = "Gamez"},
            };
            cliente1.RemoveAt(id);
            Console.WriteLine("Identificacion" + cliente.Identificacion );
			Console.WriteLine("Apellidos" + cliente.Apellidos);
			Console.WriteLine("Nombres" + cliente.Nombres);
            
		}

        // DELETE api/<ClientesApiController>/<ClientesApiController>
        [HttpDelete]
		[Route("[controller]/delete/{id}")]
		public void Delete(int id)
        {
			var cliente = new List<Cliente>
			{
                new Cliente { Identificacion = "1", Nombres = "Esteban", Apellidos = "Flores"},
                new Cliente { Identificacion = "2", Nombres = "Johanna", Apellidos = "Gamez"},
            };
			
			 cliente.RemoveAt(id);
		}
    }
}
