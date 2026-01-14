using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;

namespace Farmacontrol_PDV.CLASSES
{
	class ClienteImpresionIP
	{
		public static void enviar_mensaje(string server, string message)
		{
            Cursor.Current = Cursors.WaitCursor;

			try
			{
				// Create a TcpClient. 
				// Note, for this client to work you need to have a TcpServer  
				// connected to the same address as specified by the server, port 
				// combination.
				Int32 port = 13000;
				TcpClient client = new TcpClient(server, port);

				// Translate the passed message into ASCII and store it as a Byte array.
				Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

				// Get a client stream for reading and writing. 
				//  Stream stream = client.GetStream();

				NetworkStream stream = client.GetStream();

				// Send the message to the connected TcpServer. 
				//stream.Write(data, 0, data.Length);
                stream.WriteAsync(data, 0, data.Length);/*/*/
               
				/*
				// Receive the TcpServer.response. 

				// Buffer to store the response bytes.
				data = new Byte[8192];

				// String to store the response ASCII representation.
				String responseData = String.Empty;

				// Read the first batch of the TcpServer response bytes.
				Int32 bytes = stream.Read(data, 0, data.Length);
				responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
				*/
				// Close everything.
				stream.Close();
				client.Close();
			}
			catch (Exception e)
			{
				MessageBox.Show(null,"Error al intentar enviar la impresión: \n"+e.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}

            Cursor.Current = Cursors.Default;
		}
	}
}
