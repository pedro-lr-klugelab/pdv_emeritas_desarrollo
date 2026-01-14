
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace Farmacontrol_PDV.DAO
{
	class Conector : IDisposable
	{
		public MySqlConnection conexion { get; set; }
		public DataTable result_set { get; set; }
		public Stopwatch stopwatch { get; set; }
		
		public long insert_id { get; set; }
		public int filas_afectadas { get; set; }
		public int num_rows { get; set; }
		public string query_string = "";
		public long execution_time { get; set; }

		public string server = ConfigurationManager.AppSettings["server"];
		public string database = ConfigurationManager.AppSettings["database"];
		public string user = ConfigurationManager.AppSettings["user"];
		public string password = ConfigurationManager.AppSettings["password"];
		public string connection_string;
		private bool is_transaction = false;

		MySqlTransaction transaction = null;
		List<MySqlCommand> lista_comandos_transaccion = new List<MySqlCommand>();

		public bool Conectar()
		{
			try
			{
        
          // server = "10.202.1.172";
            //    server = "localhost";
                //server = "172.16.1.5";
               connection_string = String.Format("Server={0}; Database={1}; Uid={2}; Pwd={3}; Allow User Variables=True",
					server,
					database,
					user,
					password
				);

                //test

				conexion = new MySqlConnection();

				conexion.ConnectionString = connection_string;
				conexion.Open();
				return true;
			}
			catch (MySqlException exception)
			{
                MessageBox.Show(exception.ToString());
				Log_error.log(exception);
				throw new Exception(exception.Message);
				//return false;
			}
		}

		public void TransactionRollback()
		{
			transaction.Rollback();
			is_transaction = false;
		}

		public bool TransactionCommit()
		{
			bool status_transaction = false;

			if(is_transaction)
			{
				transaction = conexion.BeginTransaction();
				
				try
				{
					foreach (MySqlCommand command in lista_comandos_transaccion)
					{
						command.Connection = conexion;
						command.Transaction = transaction;
						command.ExecuteNonQuery();
					}

					transaction.Commit();
					status_transaction = true;
				}
				catch(Exception exception)
				{
					status_transaction = false;
					try
					{
						transaction.Rollback();
					}
					catch(MySqlException m_exception)
					{
						Log_error.log(m_exception);
					}

					Log_error.log(exception);
				}
			}

			is_transaction = false;
			lista_comandos_transaccion.Clear();

			return status_transaction;
		}

		public bool TransactionStart()
		{
			Conectar();
			is_transaction = true;

			return is_transaction;
		}

		public void Select(string sql, Dictionary<string, object> parametros = null)
		{
			if(Conectar())
			{
				MySqlCommand command = new MySqlCommand(sql, conexion);
				MySqlDataAdapter adapter = new MySqlDataAdapter(command);
				DataTable datatable = new DataTable();
				
				if(is_transaction)
				{
                    if (parametros == null)
                    {
                        parametros = new Dictionary<string, object>();

                    }

					foreach (KeyValuePair<string, object> parametro in parametros)
					{
						command.Parameters.AddWithValue(parametro.Key, parametro.Value);
					}

					lista_comandos_transaccion.Add(command);
				}
				else
				{
                    try
                    {
					    stopwatch = Stopwatch.StartNew();

					    if (parametros == null)
					    {
						    parametros = new Dictionary<string, object>();
					    }

					    foreach (KeyValuePair<string, object> parametro in parametros)
					    {
						    command.Parameters.AddWithValue(parametro.Key, parametro.Value);
					    }
					
						adapter.Fill(datatable);
						result_set = datatable;
						num_rows = result_set.Rows.Count;
					}
					catch (Exception exception)
					{
						if (is_transaction)
						{
							TransactionRollback();
						}

                        Log_error.log(exception);
					}

					stopwatch.Stop();

					execution_time = stopwatch.ElapsedMilliseconds;

					Desconectar();
				}
			}
			else
			{
				throw new Exception("No se pudo conectar");
			}
		}

		public long Insert(string sql, Dictionary<string, object> parametros = null)
		{
			Non_Query(sql, parametros);
			
			return insert_id;
		}

		public void Update(string sql, Dictionary<string, object> parametros = null)
		{
			Non_Query(sql, parametros);
		}

		public void Delete(string sql, Dictionary<string, object> parametros = null)
		{
			Non_Query(sql, parametros);
		}

		public void Non_Query(string sql, Dictionary<string, object> parametros = null)
		{
			if (Conectar())
			{
				MySqlCommand command = null;

				if(is_transaction)
				{
					command = new MySqlCommand(sql, conexion);

					foreach (KeyValuePair<string, object> parametro in parametros)
					{
						command.Parameters.AddWithValue(parametro.Key, parametro.Value);
					}

					lista_comandos_transaccion.Add(command);
				}
				else
				{
					command = new MySqlCommand(sql, conexion);
					MySqlDataAdapter adapter = new MySqlDataAdapter(command);

					if (parametros == null)
					{
						parametros = new Dictionary<string, object>();
					}

					foreach (KeyValuePair<string, object> parametro in parametros)
					{
						command.Parameters.AddWithValue(parametro.Key, parametro.Value);
					}

					try
					{
						filas_afectadas = command.ExecuteNonQuery();
						insert_id = command.LastInsertedId;
					}
					catch (MySqlException exepcion)
					{
                        Log_error.log(exepcion);
					}

					if (!is_transaction)
					{
						Desconectar();
					}	
				}
			}
		}

        public void Call(string sql, Dictionary<string, object> parametros = null){
            if (Conectar())
            {
                MySqlCommand command = new MySqlCommand(sql, conexion);
                command.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable datatable = new DataTable();

                if (is_transaction)
                {
                    if (parametros == null)
                    {
                        parametros = new Dictionary<string, object>();
                    }

                    foreach (KeyValuePair<string, object> parametro in parametros)
                    {
                        command.Parameters.AddWithValue(parametro.Key, parametro.Value);
                    }

                    lista_comandos_transaccion.Add(command);
                }
                else
                {
                    try
                    {
                        stopwatch = Stopwatch.StartNew();

                        if (parametros == null)
                        {
                            parametros = new Dictionary<string, object>();
                        }

                        foreach (KeyValuePair<string, object> parametro in parametros)
                        {
                            command.Parameters.AddWithValue(parametro.Key, parametro.Value);
                        }

                        adapter.Fill(datatable);
                        result_set = datatable;
                        num_rows = result_set.Rows.Count;
                    }
                    catch (Exception exception)
                    {
                        if (is_transaction)
                        {
                            TransactionRollback();
                        }

                        Log_error.log(exception);
                    }

                    stopwatch.Stop();

                    execution_time = stopwatch.ElapsedMilliseconds;

                    Desconectar();
                }
            }
            else
            {
                throw new Exception("No se pudo conectar");
            }
        }

		public void Desconectar()
		{
			if(conexion.State == System.Data.ConnectionState.Open)
			{
				conexion.Close();
			}
		}

		public void Dispose()
		{
			conexion.Close();
		}
	}
}