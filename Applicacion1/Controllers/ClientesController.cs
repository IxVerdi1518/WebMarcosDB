using Applicacion1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Applicacion1.Session;
using Applicacion1.Conexion;

namespace Applicacion1.Controllers
{
	public class ClientesController : Controller
	{
		public IActionResult ClientesAc()
		{
			Connection db = new Connection();
			return View(db.Consultar());
		}

		public IActionResult Clientes_ced_ape()
		{
			Connection db = new Connection();
			return View(db.Consultar_Ced_Ape());
		}
		
		List<Cliente> list = new List<Cliente>();

		public ClientesController()
		{
			Console.WriteLine("ClientesController Constructor");
		}


		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Crear()
		{

			return View();
		}

		[HttpPost]
		public IActionResult Crear(IFormCollection collection)
		{
			Connection db = new Connection();
			Cliente cl = new Cliente
			{
				Nombres = collection["Nombres"],
				Apellidos = collection["Apellidos"],
				Cedula = collection["Cedula"]
			};
			db.Insertar(cl);
			return RedirectToAction("ClientesAc");
		}

		public IActionResult Editar(int id)
		{
			Connection db = new Connection();
			Cliente cl = db.Consultar_id(id);
			return View(cl);
		}
		[HttpPost]
		public IActionResult Editar(int id,IFormCollection collection)
		{
			Connection db =new Connection();
			Cliente cl = new Cliente
			{
				Nombres = collection["Nombres"],
				Apellidos = collection["Apellidos"],
				Cedula = collection["Cedula"]
			};
			db.Actualizar(id, cl);
			return RedirectToAction("ClientesAc");
		}

		public IActionResult Eliminar (int id) 
		{
			Connection db = new Connection();
			Cliente cl = db.Consultar_id(id);
			return View();
		}

		[HttpPost]
		public IActionResult Eliminar (int id,IFormCollection collection) 
		{
			Connection db =new Connection();
			db.Eliminar(id);
			return RedirectToAction("ClientesAc");
			
		}
	}
}
