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
			}
			catch (Exception ex)
			{
				Console.WriteLine("No se conecto: "+ex.ToString());
			}
            Console.WriteLine("Conectado");
        }

		public string Insertar(Cliente cliente) 
		{
			string salida = "Se inserto";
			
				try
				{
					Cmd = new SqlCommand(" Insert into Cliente " + "(Nombres,Apellidos,Cedula) values (@Nombres,@Apellidos,@Cedula)", cn);
				}catch(SqlException ex)
				{
					Console.WriteLine("Error en la linea de comando: " + ex.ToString());
				}
				try
				{
					cmd.Parameters.Add("@Nombres", SqlDbType.NVarChar);
					cmd.Parameters.Add("@Apellidos", SqlDbType.NVarChar);
					cmd.Parameters.Add("@Cedula", SqlDbType.NVarChar);
					cmd.Parameters["@Nombres"].Value = cliente.Nombres;
					cmd.Parameters["@Apellidos"].Value = cliente.Apellidos;
					cmd.Parameters["@Cedula"].Value = cliente.Cedula;
					cmd.ExecuteNonQuery();
				}catch(Exception ex)
				{
					Console.WriteLine("Error en la asignación de las variables: " + ex.ToString());
				}
			return salida;
		}

		public List<Cliente>Consultar()
		{
			List<Cliente> lista = new List<Cliente>();
			string consulta = "Se Consulto";
			try
			{
				try
				{
					Cmd = new SqlCommand("select * from Cliente", cn);
				}catch(SqlException ex)
				{
					Console.WriteLine("Linea de comando incorrecto" + ex.ToString());
				}
				cmd.ExecuteNonQuery();
				SqlDataReader reader = cmd.ExecuteReader();
				{
					try
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
					}catch(Exception ex)
					{
						Console.WriteLine("Inconveniente con la asignación de las variables: " + ex.ToString());
					}
				}
			}
			catch (Exception ex) 
			{
				Console.WriteLine("No se pudo consultar: " + ex.ToString());
				
			}
            Console.WriteLine(consulta);
            return lista;
		}

		public Cliente Consultar_id(int IdCliente)
		{
			try
			{
				Cmd = new SqlCommand("Select IDCLIENTE,Nombres, Apellidos, Cedula FROM Cliente WHERE IDCLIENTE = @IDCLIENTE", cn);
			}catch(SqlException ex)
			{
				Console.WriteLine("Linea de comando incorrecto: " + ex.ToString());
			}
			try
			{
				cmd.Parameters.Add("@IDCLIENTE", SqlDbType.Int);
				cmd.Parameters["@IDCLIENTE"].Value = IdCliente;
			}catch(Exception ex)
			{
				Console.WriteLine("Asignacion de variables incorrecto: "+ex.ToString());
			}
			cmd.ExecuteNonQuery();
			SqlDataReader reader= cmd.ExecuteReader();
			Cliente cl = new Cliente();
			try
			{
				if (reader.Read())
				{
					cl.Nombres = reader["Nombres"].ToString();
					cl.Apellidos = reader["Apellidos"].ToString();
					cl.Cedula = reader["Cedula"].ToString();
				}
			}catch(Exception ex)
			{
				Console.WriteLine("Creacion incorrecta en el objeto Cliente: "+ex.ToString());
			}
			return cl;
		}

		public List<Cliente> Consultar_Ced_Ape() 
		{
			List<Cliente> lista = new List<Cliente>();
			string consulta_ced_ape = "Se consulto";
			try
			{
				try
				{
					Cmd = new SqlCommand("select Apellidos, Cedula from Cliente", cn);
				} catch(SqlException ex)
				{
					Console.WriteLine("Error en el comando de Consultar: " + ex.ToString());
				}
				cmd.ExecuteNonQuery();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					try
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
					}catch(Exception ex)
					{
						Console.WriteLine("Asignacion incorrecta en el objeto Cliente: " + ex.ToString());
					}
				}
			}
			catch(Exception ex) 
			{
				Console.WriteLine("No se pudo Consultar: " + ex.ToString());
				
			}
            Console.WriteLine(consulta_ced_ape);
            return lista;
		}

		public string Actualizar(int IDCLIENTE,Cliente cliente)
		{
			string actualizar = "Se Actualizo";
			try 
			{
				try
				{
					cmd = new SqlCommand("UPDATE Cliente Set Nombres = @Nombres, Apellidos = @Apellidos, Cedula = @Cedula where IDCLIENTE = @IDCLIENTE", cn);
				}catch(SqlException ex)
				{
					Console.WriteLine("Error en la linea de comando: " + ex.ToString());
				}
				try
				{
					cmd.Parameters.Add("@IDCLIENTE", SqlDbType.Int);
					cmd.Parameters["@IDCLIENTE"].Value = IDCLIENTE;
					cmd.Parameters.Add("@Nombres", SqlDbType.NVarChar);
					cmd.Parameters["@Nombres"].Value = cliente.Nombres;
					cmd.Parameters.Add("@Apellidos", SqlDbType.NVarChar);
					cmd.Parameters["@Apellidos"].Value = cliente.Apellidos;
					cmd.Parameters.Add("@Cedula", SqlDbType.NVarChar);
					cmd.Parameters["@Cedula"].Value = cliente.Cedula;
					cmd.ExecuteNonQuery();
				}catch(Exception ex)
				{
					Console.WriteLine("Incoveniente al asignar las variables:"+ex.ToString());
				}
			}
			catch(Exception ex) 
			{
				Console.WriteLine("No se ejecuto: " + ex.ToString());
			}
            return actualizar;
		}


		public string Eliminar(int IDCLIENTE) 
		{
			string eliminar = "Se elimino";
			try 
			{
				try
				{
					cmd = new SqlCommand("Delete Cliente where IDCLIENTE = @IDCLIENTE", cn);
				} catch(SqlException ex)
				{
					Console.WriteLine("Linea de comando incorrecto: " + ex.ToString());
				}
				try
				{
					cmd.Parameters.Add("@IDCLIENTE", SqlDbType.Int);
					cmd.Parameters["@IDCLIENTE"].Value = IDCLIENTE;
					cmd.ExecuteNonQuery();
				}catch (Exception ex)
				{
					Console.WriteLine("Asignación incorrecto: " + ex.ToString());
				}
			}
			catch(Exception ex) 
			{
				Console.WriteLine("No se elimino: " + ex.ToString());

			}
			return eliminar;
		}
	}
}
