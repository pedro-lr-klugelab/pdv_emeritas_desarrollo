﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DAO;
using System.Data;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.DTO;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
    class Vale_efectivo
    {
        private StringBuilder ticket = new StringBuilder();
        //private DAO.DAO_Cotizaciones dao_cotizaciones = new DAO.DAO_Cotizaciones();
        DTO_Vale vale_data = new DTO_Vale();
        bool es_prepago = false;

        public void construccion_ticket(string vale_efectivo_id, bool es_prepago = false, bool para_corte = false)
        {
            DAO_Vales_efectivo dao_vale_efectivo = new DAO_Vales_efectivo();
            vale_data = dao_vale_efectivo.vale_data(vale_efectivo_id);
            this.es_prepago = es_prepago;
            DAO_Sucursales dao_sucursales = new DAO_Sucursales();
            DAO_Empleados dao_empleados = new DAO_Empleados();

            var empleado_data = dao_empleados.get_empleado_data((int)vale_data.empleado_id);

            string sucursal_id = HELPERS.Config_helper.get_config_local("sucursal_id");
            var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(sucursal_id));

            //ticket.AppendLine(POS_Control.logo);
            //ticket.Append("[LOGO]");

            ticket.Append(POS_Control.printer_reset + POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
            ticket.AppendLine(string.Format("{0}RFC:{1}\n{2}\n{3} COLONIA {4}\n{5} CP {6}, TEL: {7}", "", sucursal_data.rfc, sucursal_data.nombre, sucursal_data.direccion, sucursal_data.colonia, sucursal_data.ciudad, sucursal_data.codigo_postal, sucursal_data.telefono));
            ticket.AppendLine("IDENTIFICADOR: " + vale_efectivo_id.ToUpper());
            ticket.AppendLine("EMPLEADO: " + empleado_data.Nombre.ToUpper());
            ticket.AppendLine("FECHA: " + Misc_helper.fecha(vale_data.fecha_creacion.ToString(), "LEGIBLE"));
            ticket.AppendLine("********************************************************");
            //ticket.Append(POS_Control.printer_reset + POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
            //ticket.Append(POS_Control.font_size_0 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_0][ALIGN_CENTER]");
            ticket.AppendLine("ESTE VALE AMPARA MERCANCIA EQUIVALENTE A:");
            //ticket.Append(POS_Control.align_center + POS_Control.font_size_48);
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_48]");
            ticket.AppendLine(string.Format("{0:C2}", Convert.ToDecimal(vale_data.total)));
            //ticket.Append(POS_Control.align_center + POS_Control.font_size_0);
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]");
            ticket.AppendLine("PESOS MXN EN LA SUCURSAL DONDE FUE EMITIDO");
            ticket.AppendLine("********************************************************");
            //ticket.Append(POS_Control.align_center + POS_Control.font_size_48);
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_48]");
            ticket.AppendLine("VALE FARMACIA");

            if (para_corte)
            {
                //ticket.Append(POS_Control.align_center + POS_Control.font_size_64);
                ticket.Append("[ALIGN_CENTER][FONT_SIZE_64]");
                ticket.AppendLine(" \n");
                ticket.AppendLine("ENVIAR CON");
                ticket.AppendLine("EL CORTE");
                ticket.AppendLine(" \n");
                ticket.AppendLine("MARCAR");
                ticket.AppendLine("ORIGINAL");
                ticket.AppendLine("CANCELADO");
                ticket.AppendLine(" \n");
                //ticket.Append(POS_Control.align_center + POS_Control.font_size_0);
                ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]");
            }
            else
            {
                ticket.AppendLine("NO VALIDO SIN SELLO");
                //ticket.Append(POS_Control.align_center + POS_Control.font_size_0);
                ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]");
                ticket.AppendLine("********************************************************");
                ticket.AppendLine("*                                                      *");
                ticket.AppendLine("*                                                      *");
                ticket.AppendLine("*                                                      *");
                ticket.AppendLine("*                                                      *");
                ticket.AppendLine("*                                                      *");
                ticket.AppendLine("*                                                      *");
                ticket.AppendLine("*                                                      *");
                ticket.AppendLine("*                                                      *");
                ticket.AppendLine("*                                                      *");
                ticket.AppendLine("*                                                      *");
                ticket.AppendLine("*                                                      *");
                ticket.AppendLine("********************************************************");
                ticket.AppendLine("ESTE VALE ES CANJEABLE EXCLUSIVAMENTE POR PRODUCTOS Y");
                ticket.AppendLine("NO CAUSA ENTREGA DE CAMBIO EN EFECTIVO.\n");
                ticket.AppendLine("AL MOMENTO DE SU CANJE ESTE VALE PERDERA SU VALOR Y");
                ticket.AppendLine("SERA RETENIDO POR LA SUCURSAL.");
                ticket.AppendLine("LUGAR DE EXPEDICION: MERIDA, YUC.");
            }

            string hash = string.Format("{0}${1}", sucursal_id, vale_efectivo_id.ToUpper());
            ticket.AppendLine(sucursal_data.domicilio_fiscal);
            ticket.Append("\n");
            ticket.AppendLine(POS_Control.qr_code(hash));

            ticket.Append("[ALIGN_CENTER][FONT_SIZE_48]");
            ticket.AppendLine(hash);

            ticket.AppendLine(); ticket.AppendLine(); ticket.AppendLine(); ticket.AppendLine();
            //ticket.AppendLine(POS_Control.corte);
            ticket.Append("[CORTE]");
        }

        public bool print()
        {
            return HELPERS.Print_new_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "VALE_FARMACIA", (es_prepago == true) ? vale_data.elemento_id : vale_data.elemento_id, true);
        }

        public void Dispose()
        {
            ticket = new StringBuilder();
            //dao_cotizaciones = new DAO.DAO_Cotizaciones();
        }
    }
}
