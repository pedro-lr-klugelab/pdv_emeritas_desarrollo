using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.DAO;
using System.IO;
using System.Runtime.Serialization;
using Farmacontrol_PDV.DTO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Farmacontrol_PDV.CLASSES
{
	class ImpresionIP
	{
		public static TcpListener server = null;
		public static bool cerrar_conexion = false;
        public static TcpClient client = new TcpClient();

		public static void conectar()
		{
            Int32 port = 13000;
            DAO_Terminales dao_terminales = new DAO.DAO_Terminales();

            IPAddress localAddr = IPAddress.Parse(dao_terminales.get_ip_terminal());
            server = new TcpListener(localAddr, port);

			try
			{
				//server.Start();

                Byte[] bytes = new Byte[1048576];
				String data = null;
                
				while (!cerrar_conexion)
				{
                    server.Start();

                    client = server.AcceptTcpClient();

					data = null;

					NetworkStream stream = null;
					stream = client.GetStream();

					int i;

					while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
					{
						try
						{
							data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
							byte[] objeto_impresion = Convert.FromBase64String(data);
								
							ejecucion_impresion(objeto_impresion);
						}
						catch (Exception ex)
						{
                            Console.WriteLine(ex.ToString());
                            Log_error.log(ex);
							client.Close();
						}
					}

					client.Close();
                    server.Stop();
				}
			}
			catch (Exception e)
			{
                Console.WriteLine(e.ToString());
                client.Close();
                server.Stop();
                Console.WriteLine(e.ToString());
			}
			finally
			{
				server.Stop();
			}
		}

		public static void ejecucion_impresion(byte[] objeto_impresion)
		{
            try
            {
                Ejecucion_impresion workerObject = new Ejecucion_impresion();
                Thread workerThread = new Thread(() => workerObject.DoWork(objeto_impresion));

                workerThread.Start();
                while (!workerThread.IsAlive) ;
                Thread.Sleep(1);

                workerObject.RequestStop();

                workerThread.Join();
            }
            catch(Exception ex)
            {
                Log_error.log(ex);
            }
		}
	}


	class Ejecucion_impresion
	{
		public void DoWork(byte[] objeto_impresion)
		{
			while (!_shouldStop)
			{

			}

			try
			{
				MemoryStream memStream = new MemoryStream();
				memStream.Write(objeto_impresion, 0, objeto_impresion.Length);
				memStream.Seek(0, SeekOrigin.Begin);

				BinaryFormatter binForm = new BinaryFormatter();
                /*
				DTO_Impresion_ip obj = new DTO_Impresion_ip();
				obj = (DTO_Impresion_ip)binForm.Deserialize(memStream);

				Print_helper.print(obj.terminal_id, obj.texto, obj.tipo, obj.folio, obj.impresora_tickets, true,false,obj.reimpresion);
                 */

                DTO_Impresion_ip_new obj = new DTO_Impresion_ip_new();
                obj = (DTO_Impresion_ip_new)binForm.Deserialize(memStream);
                Print_new_helper.print_to_remote(obj.id, obj.impresora_tickets, obj.reimpresion);
			}
			catch(SerializationException ex)
			{
				Log_error.log(ex);

				RequestStop();
			}
		}
		public void RequestStop()
		{
			_shouldStop = true;
		}

		private volatile bool _shouldStop;
	}
}