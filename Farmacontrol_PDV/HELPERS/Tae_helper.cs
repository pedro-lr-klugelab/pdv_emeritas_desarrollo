using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.tae_diestel_new;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Farmacontrol_PDV.DAO;
using System.ServiceModel;
using System.Windows.Forms;
using PXSecurity.Datalogic.Classes;

namespace Farmacontrol_PDV.HELPERS
{
	class Tae_helper
    {
        PxUniversalSoapClient client = new PxUniversalSoapClient("PxUniversalSoap");
        string tae_diestel_enc_key = Config_helper.get_config_global("tae_diestel_enc_key");

        public Tae_helper()
        {
            string tae_diestel_user = Config_helper.get_config_global("tae_diestel_user");
            string tae_diestel_pass = Config_helper.get_config_global("tae_diestel_pass");
            string tae_diestel_dominio = Config_helper.get_config_global("tae_diestel_dominio");

            client.ClientCredentials.Windows.ClientCredential = new NetworkCredential(tae_diestel_user, tae_diestel_pass, tae_diestel_dominio);
            client.Endpoint.Binding.SendTimeout = new TimeSpan(0, 0, 45);
        }

        public cCampo[] info_tae(int? empleado_id, string sku, string numero, long? venta_id)
        {
            DAO_Sucursales dao_sucursales = new DAO_Sucursales();
            DAO_Terminales dao_terminales = new DAO_Terminales();

            DAO_Tae dato_tae = new DAO_Tae();
            
            int sucursal_id = dao_sucursales.get_sucursal_data(Convert.ToInt32(Config_helper.get_config_local("sucursal_id"))).tae_diestel_tienda_id;
            int terminal_id = Convert.ToInt32(dao_terminales.get_tae_diestel_pos_id((int)Misc_helper.get_terminal_id()));

            bool tmp = dato_tae.inserta_control_transaccion_tae(sucursal_id, terminal_id);
            int numero_transaccion = 0;
            if (tmp)
            {
                numero_transaccion = dato_tae.get_numero_transaccion(sucursal_id, terminal_id);
            }
            else
            {
                MessageBox.Show("Ocurrió un error al generar el numero de transaccion, comuníquese a sistemas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cCampo[] campos = new cCampo[11];
            
            cCampo campo0 = new cCampo();
            campo0.sCampo = "IDGRUPO";
            campo0.iTipo = eTipo.NE;
            campo0.iLongitud = 4;
            campo0.iClase = 0;
            campo0.sValor = Convert.ToInt32(Config_helper.get_config_global("tae_diestel_grupo_id"));
            campo0.bEncriptado = false;

            cCampo campo1 = new cCampo();
            campo1.sCampo = "IDCADENA";
            campo1.iTipo = eTipo.NE;
            campo1.iLongitud = 5;
            campo1.iClase = 0;
            campo1.sValor = Convert.ToInt32(Config_helper.get_config_global("tae_diestel_cadena_id"));
            campo1.bEncriptado = false;

            cCampo campo2 = new cCampo();
            campo2.sCampo = "IDTIENDA";
            campo2.iTipo = eTipo.NE;
            campo2.iLongitud = 5;
            campo2.iClase = 0;
            campo2.sValor = sucursal_id;
            campo2.bEncriptado = false;

            cCampo campo3 = new cCampo();
            campo3.sCampo = "IDPOS";
            campo3.iTipo = eTipo.NE;
            campo3.iLongitud = 4;
            campo3.iClase = 0;
            campo3.sValor = terminal_id;
            campo3.bEncriptado = false;

            cCampo campo4 = new cCampo();
            campo4.sCampo = "IDCAJERO";
            campo4.iTipo = eTipo.NE;
            campo4.iLongitud = 10;
            campo4.iClase = 0;
            campo4.sValor = empleado_id;
            campo4.bEncriptado = false;

            cCampo campo5 = new cCampo();
            campo5.sCampo = "FECHALOCAL";
            campo5.iTipo = eTipo.AN;
            campo5.iLongitud = 10;
            campo5.iClase = 0;
            campo5.sValor = DateTime.Now.ToString("dd/MM/yyyy");
            campo5.bEncriptado = false;

            cCampo campo6 = new cCampo();
            campo6.sCampo = "HORALOCAL";
            campo6.iTipo = eTipo.AN;
            campo6.iLongitud = 8;
            campo6.iClase = 0;
            campo6.sValor = DateTime.Now.ToString("HH:mm:ss");
            campo6.bEncriptado = false;

            cCampo campo7 = new cCampo();
            campo7.sCampo = "TRANSACCION";
            campo7.iTipo = eTipo.NE;
            campo7.iLongitud = 12;
            campo7.iClase = 0;
            campo7.sValor = numero_transaccion;
            campo7.bEncriptado = false;

            cCampo campo8 = new cCampo();
            campo8.sCampo = "FECHACONTABLE";
            campo8.iTipo = eTipo.AN;
            campo8.iLongitud = 10;
            campo8.iClase = 0;
            campo8.sValor = DateTime.Now.ToString("dd/MM/yyyy");
            campo8.bEncriptado = false;

            cCampo campo9 = new cCampo();
            campo9.sCampo = "SKU";
            campo9.iTipo = eTipo.AN;
            campo9.iLongitud = 13;
            campo9.iClase = 0;
            campo9.sValor = sku;
            campo9.bEncriptado = false;

            cCampo campo10 = new cCampo();
            campo10.sCampo = "REFERENCIA";
            campo10.iTipo = eTipo.AN;
            campo10.iLongitud = 120;
            campo10.iClase = 0;
            campo10.sValor = PXCryptography.PXEncryptFX(numero, tae_diestel_enc_key);
            campo10.bEncriptado = true;

            campos[0]  = campo0;
            campos[1]  = campo1;
            campos[2]  = campo2;
            campos[3]  = campo3;
            campos[4]  = campo4;
            campos[5]  = campo5;
            campos[6]  = campo6;
            campos[7]  = campo7;
            campos[8]  = campo8;
            campos[9]  = campo9;
            campos[10] = campo10;

            cCampo[] test = client.Info(campos);

            bool inserta_log = false;

            if (test.Length > 2)
            {
                inserta_log = dato_tae.inserta_log_tae_diestel(sku, sucursal_id, terminal_id, numero, 0, venta_id, numero_transaccion, "INFO", true, "0");
            }
            else
            {
                inserta_log = dato_tae.inserta_log_tae_diestel(sku, sucursal_id, terminal_id, numero, 0, venta_id, numero_transaccion, "INFO", false, test[0].sValor.ToString());

                for (int i = 0; i <= 3; i++)
                {
                    var reversa = this.reversa_tae(empleado_id, sku, numero, venta_id, numero_transaccion);
                    if (reversa[0].sValor.ToString().Equals("0"))
                    {
                        break;
                    }
                }
            }
            return test;
        }

        public cCampo[] ejecuta_tae(int? empleado_id, string sku, string numero, string tipo_pago, long monto, long? venta_id, int numero_transaccion)
        {
            DAO_Sucursales dao_sucursales = new DAO_Sucursales();
            DAO_Terminales dao_terminales = new DAO_Terminales();

            DAO_Tae dato_tae = new DAO_Tae();
            
            int sucursal_id = dao_sucursales.get_sucursal_data(Convert.ToInt32(Config_helper.get_config_local("sucursal_id"))).tae_diestel_tienda_id;
            int terminal_id = Convert.ToInt32(dao_terminales.get_tae_diestel_pos_id((int)Misc_helper.get_terminal_id()));

            cCampo[] campos = new cCampo[13];

            cCampo campo0 = new cCampo();
            campo0.sCampo = "IDGRUPO";
            campo0.iTipo = eTipo.NE;
            campo0.iLongitud = 4;
            campo0.iClase = 0;
            campo0.sValor = Convert.ToInt32(Config_helper.get_config_global("tae_diestel_grupo_id"));
            campo0.bEncriptado = false;

            cCampo campo1 = new cCampo();
            campo1.sCampo = "IDCADENA";
            campo1.iTipo = eTipo.NE;
            campo1.iLongitud = 5;
            campo1.iClase = 0;
            campo1.sValor = Convert.ToInt32(Config_helper.get_config_global("tae_diestel_cadena_id"));
            campo1.bEncriptado = false;

            cCampo campo2 = new cCampo();
            campo2.sCampo = "IDTIENDA";
            campo2.iTipo = eTipo.NE;
            campo2.iLongitud = 5;
            campo2.iClase = 0;
            campo2.sValor = dao_sucursales.get_sucursal_data(Convert.ToInt32(Config_helper.get_config_local("sucursal_id"))).tae_diestel_tienda_id;
            campo2.bEncriptado = false;

            cCampo campo3 = new cCampo();
            campo3.sCampo = "IDPOS";
            campo3.iTipo = eTipo.NE;
            campo3.iLongitud = 4;
            campo3.iClase = 0;
            campo3.sValor = Convert.ToInt32(dao_terminales.get_tae_diestel_pos_id((int)Misc_helper.get_terminal_id()));
            campo3.bEncriptado = false;

            cCampo campo4 = new cCampo();
            campo4.sCampo = "IDCAJERO";
            campo4.iTipo = eTipo.NE;
            campo4.iLongitud = 10;
            campo4.iClase = 0;
            campo4.sValor = empleado_id;
            campo4.bEncriptado = false;

            cCampo campo5 = new cCampo();
            campo5.sCampo = "FECHALOCAL";
            campo5.iTipo = eTipo.AN;
            campo5.iLongitud = 10;
            campo5.iClase = 0;
            campo5.sValor = DateTime.Now.ToString("dd/MM/yyyy");
            campo5.bEncriptado = false;

            cCampo campo6 = new cCampo();
            campo6.sCampo = "HORALOCAL";
            campo6.iTipo = eTipo.AN;
            campo6.iLongitud = 8;
            campo6.iClase = 0;
            campo6.sValor = DateTime.Now.ToString("HH:mm:ss");
            campo6.bEncriptado = false;

            cCampo campo7 = new cCampo();
            campo7.sCampo = "FECHACONTABLE";
            campo7.iTipo = eTipo.AN;
            campo7.iLongitud = 10;
            campo7.iClase = 0;
            campo7.sValor = DateTime.Now.ToString("dd/MM/yyyy");
            campo7.bEncriptado = false;

            cCampo campo8 = new cCampo();
            campo8.sCampo = "TRANSACCION";
            campo8.iTipo = eTipo.NE;
            campo8.iLongitud = 12;
            campo8.iClase = 0;
            campo8.sValor = numero_transaccion;
            campo8.bEncriptado = false;

            cCampo campo9 = new cCampo();
            campo9.sCampo = "SKU";
            campo9.iTipo = eTipo.AN;
            campo9.iLongitud = 13;
            campo9.iClase = 0;
            campo9.sValor = sku;
            campo9.bEncriptado = false;

            cCampo campo10 = new cCampo();
            campo10.sCampo = "TIPOPAGO";
            campo10.iTipo = eTipo.AN;
            campo10.iLongitud = 120;
            campo10.iClase = 0;
            campo10.sValor = tipo_pago;
            campo10.bEncriptado = false;

            cCampo campo11 = new cCampo();
            campo11.sCampo = "REFERENCIA";
            campo11.iTipo = eTipo.AN;
            campo11.iLongitud = 120;
            campo11.iClase = 0;
            campo11.sValor = PXCryptography.PXEncryptFX(numero, tae_diestel_enc_key);
            campo11.bEncriptado = true;

            cCampo campo12 = new cCampo();
            campo12.sCampo = "MONTO";
            campo12.iTipo = eTipo.NE;
            campo12.iLongitud = 120;
            campo12.iClase = 0;
            campo12.sValor = monto;
            campo12.bEncriptado = false;

            campos[0]  = campo0;
            campos[1]  = campo1;
            campos[2]  = campo2;
            campos[3]  = campo3;
            campos[4]  = campo4;
            campos[5]  = campo5;
            campos[6]  = campo6;
            campos[7]  = campo7;
            campos[8]  = campo8;
            campos[9]  = campo9;
            campos[10] = campo10;
            campos[11] = campo11;
            campos[12] = campo12;

            //cCampo[] test = new cCampo[5];

            bool inserta_log = false;

            try
            {
                var test = client.Ejecuta(campos);

                if (test.Length > 2)
                {
                    long numero_autorizacion;
                    numero_autorizacion = Convert.ToInt64(test[2].sValor);
                    inserta_log = dato_tae.inserta_log_tae_diestel(sku, sucursal_id, terminal_id, numero, numero_autorizacion, venta_id, numero_transaccion, "EJECUTA", true, "0");
                }
                else
                {
                    inserta_log = dato_tae.inserta_log_tae_diestel(sku, sucursal_id, terminal_id, numero, 0, venta_id, numero_transaccion, "EJECUTA", false, test[0].sValor.ToString());
                    if (test[0].sValor.ToString().Equals("8") || test[0].sValor.ToString().Equals("71") || test[0].sValor.ToString().Equals("72"))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            var reversa = this.reversa_tae(empleado_id, sku, numero, venta_id, numero_transaccion);
                            if (reversa[0].sValor.ToString().Equals("0"))
                            {
                                break;
                            }
                        }
                    }
                }

                return test;
            }
            catch(Exception e)
            {
                string tmp = e.Message.ToString();
                cCampo[] test_tmp = new cCampo[5];
                inserta_log = dato_tae.inserta_log_tae_diestel(sku, sucursal_id, terminal_id, numero, 0, venta_id, numero_transaccion, "EJECUTA", true, "8");

                cCampo[] test = new cCampo[2];

                cCampo test0 = new cCampo();
                test0.sCampo = "NUMERO";
                test0.iTipo = eTipo.AN;
                test0.iLongitud = 4;
                test0.iClase = 0;
                test0.sValor = "8";
                test0.bEncriptado = false;

                cCampo test1 = new cCampo();
                test1.sCampo = "DESCRIPCION";
                test1.iTipo = eTipo.AN;
                test1.iLongitud = 4;
                test1.iClase = 0;
                test1.sValor = "Terminó el tiempo de espera";
                test1.bEncriptado = false;

                test[0] = test0;
                test[1] = test1;

                for (int i = 0; i < 3; i++)
                {
                    test_tmp = this.reversa_tae(empleado_id, sku, numero, venta_id, numero_transaccion);
                    if (test_tmp[0].sValor.ToString().Equals("0"))
                    {
                        break;
                    }
                }

                //MessageBox.Show("Código (8): Terminó el tiempo de espera.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return test;
            }
        }

        public cCampo[] reversa_tae(int? empleado_id, string sku, string numero, long? venta_id, int numero_transaccion)
        {
            DAO_Sucursales dao_sucursales = new DAO_Sucursales();
            DAO_Terminales dao_terminales = new DAO_Terminales();

            DAO_Tae dato_tae = new DAO_Tae();

            int sucursal_id = dao_sucursales.get_sucursal_data(Convert.ToInt32(Config_helper.get_config_local("sucursal_id"))).tae_diestel_tienda_id;
            int terminal_id = Convert.ToInt32(dao_terminales.get_tae_diestel_pos_id((int)Misc_helper.get_terminal_id()));

            cCampo[] campos = new cCampo[12];

            cCampo campo0 = new cCampo();
            campo0.sCampo = "IDGRUPO";
            campo0.iTipo = eTipo.NE;
            campo0.iLongitud = 4;
            campo0.iClase = 0;
            campo0.sValor = Convert.ToInt32(Config_helper.get_config_global("tae_diestel_grupo_id"));
            campo0.bEncriptado = false;

            cCampo campo1 = new cCampo();
            campo1.sCampo = "IDCADENA";
            campo1.iTipo = eTipo.NE;
            campo1.iLongitud = 5;
            campo1.iClase = 0;
            campo1.sValor = Convert.ToInt32(Config_helper.get_config_global("tae_diestel_cadena_id"));
            campo1.bEncriptado = false;

            cCampo campo2 = new cCampo();
            campo2.sCampo = "IDTIENDA";
            campo2.iTipo = eTipo.NE;
            campo2.iLongitud = 5;
            campo2.iClase = 0;
            campo2.sValor = sucursal_id;
            campo2.bEncriptado = false;

            cCampo campo3 = new cCampo();
            campo3.sCampo = "IDPOS";
            campo3.iTipo = eTipo.NE;
            campo3.iLongitud = 4;
            campo3.iClase = 0;
            campo3.sValor = terminal_id;
            campo3.bEncriptado = false;

            cCampo campo4 = new cCampo();
            campo4.sCampo = "IDCAJERO";
            campo4.iTipo = eTipo.NE;
            campo4.iLongitud = 10;
            campo4.iClase = 0;
            campo4.sValor = empleado_id;
            campo4.bEncriptado = false;

            cCampo campo5 = new cCampo();
            campo5.sCampo = "FECHALOCAL";
            campo5.iTipo = eTipo.AN;
            campo5.iLongitud = 10;
            campo5.iClase = 0;
            campo5.sValor = DateTime.Now.ToString("dd/MM/yyyy");
            campo5.bEncriptado = false;

            cCampo campo6 = new cCampo();
            campo6.sCampo = "HORALOCAL";
            campo6.iTipo = eTipo.AN;
            campo6.iLongitud = 8;
            campo6.iClase = 0;
            campo6.sValor = DateTime.Now.ToString("HH:mm:ss");
            campo6.bEncriptado = false;

            cCampo campo7 = new cCampo();
            campo7.sCampo = "FECHACONTABLE";
            campo7.iTipo = eTipo.AN;
            campo7.iLongitud = 10;
            campo7.iClase = 0;
            campo7.sValor = DateTime.Now.ToString("dd/MM/yyyy");
            campo7.bEncriptado = false;

            cCampo campo8 = new cCampo();
            campo8.sCampo = "TRANSACCION";
            campo8.iTipo = eTipo.NE;
            campo8.iLongitud = 12;
            campo8.iClase = 0;
            campo8.sValor = numero_transaccion;
            campo8.bEncriptado = false;

            cCampo campo9 = new cCampo();
            campo9.sCampo = "SKU";
            campo9.iTipo = eTipo.AN;
            campo9.iLongitud = 13;
            campo9.iClase = 0;
            campo9.sValor = sku;
            campo9.bEncriptado = false;

            cCampo campo10 = new cCampo();
            campo10.sCampo = "REFERENCIA";
            campo10.iTipo = eTipo.AN;
            campo10.iLongitud = 120;
            campo10.iClase = 0;
            campo10.sValor = PXCryptography.PXEncryptFX(numero, tae_diestel_enc_key); ;
            campo10.bEncriptado = true;

            cCampo campo11 = new cCampo();
            campo11.sCampo = "REFERENCIA";
            campo11.iTipo = eTipo.AN;
            campo11.iLongitud = 120;
            campo11.iClase = 0;
            campo11.sValor = numero;
            campo11.bEncriptado = false;

            campos[0]  = campo0;
            campos[1]  = campo1;
            campos[2]  = campo2;
            campos[3]  = campo3;
            campos[4]  = campo4;
            campos[5]  = campo5;
            campos[6]  = campo6;
            campos[7]  = campo7;
            campos[8]  = campo8;
            campos[9]  = campo9;
            campos[10] = campo10;
            campos[11] = campo11;
            
            bool inserta_log = false;
            
            try
            {
                cCampo[] test = client.Reversa(campos);

                if (test.Length > 2)
                {
                    inserta_log = dato_tae.inserta_log_tae_diestel(sku, sucursal_id, terminal_id, numero, 0, venta_id, numero_transaccion, "REVERSA", true, "0");
                }
                else
                {
                    inserta_log = dato_tae.inserta_log_tae_diestel(sku, sucursal_id, terminal_id, numero, 0, venta_id, numero_transaccion, "REVERSA", false, test[0].sValor.ToString());
                }
                return test;
            }
            catch(Exception e)
            {
                cCampo[] test =  new cCampo[2];
                string tmp = e.Message.ToString();

                cCampo test0 = new cCampo();
                test0.sCampo = "NUMERO";
                test0.iTipo = eTipo.AN;
                test0.iLongitud = 4;
                test0.iClase = 0;
                test0.sValor = "8";
                test0.bEncriptado = false;

                cCampo test1 = new cCampo();
                test1.sCampo = "DESCRIPCION";
                test1.iTipo = eTipo.AN;
                test1.iLongitud = 4;
                test1.iClase = 0;
                test1.sValor = "Terminó el tiempo de espera";
                test1.bEncriptado = false;

                test[0] = test0;
                test[1] = test1;

                inserta_log = dato_tae.inserta_log_tae_diestel(sku, sucursal_id, terminal_id, numero, 0, venta_id, numero_transaccion, "REVERSA", false, "8");
                return test;
            }
        }

        public static decimal get_importe_tae(string sku)
        {
            decimal importe = 0;
            DAO_Tae dao_tae = new DAO_Tae();
            DTO_tae_diestel dto_tae = new DTO_tae_diestel();

            dto_tae = dao_tae.get_info_tae_por_sku(sku);

            importe = dto_tae.precio_publico - (dto_tae.precio_publico * (dto_tae.descuento / 100));

            return importe;
        }
    }
}
