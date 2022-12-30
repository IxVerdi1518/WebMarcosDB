using Applicacion1.Models;
using System.Data;
using System.Data.SqlClient;

namespace Applicacion1.Conexion
{
	public class Connection
	{
		SqlConnection cn;
		SqlCommand cmd;

		public SqlConnection Cn { get => cn; set => cn = value; }
		public SqlCommand Cmd { get => cmd; set => cmd = value; }

		public Connection()
		{
			try
			{
				cn = new SqlConnection("Data Source=DESKTOP-KBQIMBE;Initial Catalog=Clientes;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
				cn.Open();
				Console.WriteLine("Conectado");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		public string Insertar(Cliente cliente) 
		{
			string salida = "Se inserto";
			try 
			{
				Cmd = new SqlCommand(" Insert into Cliente " + "(Nombres,Apellidos,Cedula) values (@Nombres,@Apellidos,@Cedula)", cn);
				cmd.Parameters.Add("@Nombres", SqlDbType.NVarChar);
				cmd.Parameters.Add("@Apellidos", SqlDbType.NVarChar);
				cmd.Parameters.Add("@Cedula", SqlDbType.NVarChar);
				cmd.Parameters["@Nombres"].Value = cliente.Nombres;
				cmd.Parameters["@Apellidos"].Value = cliente.Apellidos;
				cmd.Parameters["@Cedula"].Value = cliente.Cedula;
				cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				salida = "No se Inserto: " + ex.ToString();
				Console.WriteLine(salida);
			}
			return salida;
		}

		public List<Cliente>Consultar()
		{
			List<Cliente> lista = new List<Cliente>();
			string consulta = "Se Consulto";
			try
			{
				Cmd = new SqlCommand("select * from Cliente", cn);
				cmd.ExecuteNonQuery();
				SqlDataReader reader = cmd.ExecuteReader();
				{
					while (reader.Read()) 
					{
						Cliente cl = new Cliente
						{
							Identificacion = reader["IDCLIENTE"].ToString(),
							Nombres = reader["Nombres"].ToString(),
							Apellidos = reader["Apellidos"].ToString(),
							Cedula = reader["Cedula"].ToString()
						};
						lista.Add(cl);
					}
				}
			}
			catch (Exception ex) 
			{
				consulta = "No se pudo consultar: " + ex.ToString();
				Console.WriteLine(consulta);
			}
			return lista;
		}

		public Cliente Consultar_id(int IdCliente)
		{
			Cmd = new SqlCommand("Select IDCLIENTE,Nombres, Apellidos, Cedula FROM Cliente WHERE IDCLIENTE = @IDCLIENTE", cn);
			cmd.Parameters.Add("@IDCLIENTE", SqlDbType.Int);
			cmd.Parameters["@IDCLIENTE"].Value = IdCliente;
			cmd.ExecuteNonQuery();
			SqlDataReader reader= cmd.ExecuteReader();
			Cliente cl = new Cliente();
			if (reader.Read()) 
			{
				cl.Nombres = reader["Nombres"].ToString();
				cl.Apellidos = reader["Apellidos"].ToString();
				cl.Cedula = reader["Cedula"].ToString();
			}
			return cl;
		}

		public List<Cliente> Consultar_Ced_Ape() 
		{
			List<Cliente> lista = new List<Cliente>();
			string consulta_ced_ape = "Se consulto";
			try
			{
				Cmd = new SqlCommand("select Apellidos, Cedula from Cliente", cn);				
				cmd.ExecuteNonQuery();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						Cliente cl = new Cliente
						{
							Cedula = reader["Cedula"].ToString(),
							Apellidos = reader["Apellidos"].ToString()
						};
						lista.Add(cl);
					}
				}
			}
			catch(Exception ex) 
			{
				consulta_ced_ape = "No se pudo Consultar: " + ex.ToString();
				Console.WriteLine (consulta_ced_ape);
			}
			return lista;
		}

		public string Actualizar(int IDCLIENTE,Cliente cliente)
		{
			string actualizar = "Se Actualizo";
			try 
			{
				cmd = new SqlCommand("UPDATE Cliente Set Nombres = @Nombres, Apellidos = @Apellidos, Cedula = @Cedula where IDCLIENTE = @IDCLIENTE", cn);
				cmd.Parameters.Add("@IDCLIENTE", SqlDbType.Int);
				cmd.Parameters["@IDCLIENTE"].Value = IDCLIENTE;
				cmd.Parameters.Add("@Nombres", SqlDbType.NVarChar);
				cmd.Parameters["@Nombres"].Value = cliente.Nombres;
				cmd.Parameters.Add("@Apellidos", SqlDbType.NVarChar);
				cmd.Parameters["@Apellidos"].Value = cliente.Apellidos;
				cmd.Parameters.Add("@Cedula", SqlDbType.NVarChar);
				cmd.Parameters["@Cedula"].Value = cliente.Cedula;
				cmd.ExecuteNonQuery();
			}
			catch(Exception ex) 
			{
				actualizar = "No se actualizo: " + ex.ToString();
				Console.WriteLine (actualizar);
			}
			return actualizar;
		}


		public string Eliminar(int IDCLIENTE) 
		{
			string eliminar = "Se elimino";
			try 
			{
				cmd = new SqlCommand("Delete Cliente where IDCLIENTE = @IDCLIENTE", cn);
				cmd.Parameters.Add("@IDCLIENTE", SqlDbType.Int);
				cmd.Parameters["@IDCLIENTE"].Value=IDCLIENTE;
				cmd.ExecuteNonQuery();
			}
			catch(Exception ex) 
			{
				eliminar = "No se elimino: " + ex.ToString();

			}
			return eliminar;
		}
	}
}
