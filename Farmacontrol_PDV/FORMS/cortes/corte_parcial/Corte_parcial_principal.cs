using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.FORMS.ventas.ventas;
using Farmacontrol_PDV.HELPERS;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Farmacontrol_PDV.FORMS.cortes.corte_parcial
{
    public partial class Corte_parcial_principal : Form
    {
        DAO_Cortes dao_cortes = new DAO_Cortes();
        DTO_Corte dto_corte = new DTO_Corte();
        DTO_Corte ultimo_corte = new DTO_Corte();
        DAO_Entradas dao_entradas = new DAO_Entradas();
        private decimal tipoCambioDolar = 0m;

        public Corte_parcial_principal()
        {
            InitializeComponent();
        }

        private void Corte_parcial_principal_Load(object sender, EventArgs e)
        {
            try
            {

                get_ultimo_corte();
                get_informacion_corte();
                
                ConfigurarDataGridViewVaucheras();
                ConfigurarGridResumenExtras();
                CargarTipoCambioDolar();
            }
            catch (Exception ex)
            {
                Log_error.log(ex);
            }
        }

        public void get_ultimo_corte()
        {
            ultimo_corte = dao_cortes.get_ultimo_corte();
            lbl_folio_ultimo_corte.Text = (ultimo_corte.corte_id > 0) ? "#" + ultimo_corte.corte_folio : "AUN SIN CORTES";
            lbl_fecha_ultimo_corte.Text = (ultimo_corte.fecha != null) ? Misc_helper.fecha(ultimo_corte.fecha.ToString(), "LEGIBLE") : "AUN SIN CORTES";
            lbl_empleado.Text = (ultimo_corte.corte_id > 0) ? ultimo_corte.nombre_empleado : "AUN SIN CORTES";
        }

        public void get_informacion_corte()
        {
            dto_corte = dao_cortes.get_corte_parcial();

            lbl_iva.Text = dto_corte.importe_iva.ToString("C2");
            lbl_ieps.Text = dto_corte.importe_ieps.ToString("C2");
            lbl_excento.Text = dto_corte.importe_excento.ToString("C2");
            lbl_gravado.Text = dto_corte.importe_gravado.ToString("C2");
            lbl_venta_bruta.Text = dto_corte.importe_bruto.ToString("C2");

            lbl_folio_ventas.Text = "Ventas del corte del folio #" + dto_corte.venta_inicial + " - #" + dto_corte.venta_final;

            //prepagos
            lbl_prepagos_realizados.Text = dto_corte.importe_prepagado.ToString("C2");
            //lbl_prepagos_canjeados.Text = "-" + dto_corte.importe_prepagado_canjeado.ToString("C2");
            lbl_prepagos_cancelados.Text = dto_corte.importe_prepagado_cancelado.ToString("C2");

            //descuentos
            //lbl_descuento_excento.Text = "-" + dto_corte.importe_descuento_excento.ToString("C2");
            //lbl_descuento_gravado.Text = "-" + dto_corte.importe_descuento_gravado.ToString("C2");
            lbl_cancelados.Text = dto_corte.importe_cancelaciones.ToString("C2");
            //enddescuentos

            lbl_vales_emitidos.Text = dto_corte.vales_emitidos.ToString("C2");
            lbl_ventas_totales.Text = (dto_corte.importe_bruto - dto_corte.importe_cancelaciones).ToString("C2");
            //lbl_subtotal.Text = ((dto_corte.importe_gravado + dto_corte.importe_excento) - (dto_corte.importe_descuento_excento + dto_corte.importe_descuento_gravado)).ToString("C2");
            lbl_total.Text = dto_corte.importe_total.ToString("C2");

            lbl_total_mas_vales.Text = Convert.ToDecimal((dto_corte.importe_total + dto_corte.vales_emitidos + dto_corte.importe_prepagado) - dto_corte.importe_prepagado_cancelado).ToString("C2");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btn_generar_corte_Click(object sender, EventArgs e)
        {

            bool hacerCorte = true;
            #region Valida tiempo de corte
            /* 
            bool hacerCorte = false;
            DateTime Hoy = DateTime.Today;

            string fecha_actual = Hoy.ToString("dddd", new CultureInfo("es-ES")).ToUpper();
         
            if (fecha_actual.Equals("DOMINGO"))
            {
                hacerCorte = true;
            }
            else
            {

                string horariopermitido = dao_cortes.get_horario_corte_parcial();

                DateTime dtHoraPermitido = DateTime.Parse(horariopermitido);

                int horaactual = Convert.ToInt32(DateTime.Now.ToString("HHmm"));

                int horapermitida = Convert.ToInt32(dtHoraPermitido.ToString("HHmm"));

      
                if (horaactual >= horapermitida && horaactual <= (horapermitida+200))
                {
                    hacerCorte = true;
                }
                else
                {
                    if (horapermitida == 0)
                        hacerCorte = true;
                    else
                        hacerCorte = false;
                }
            }    
            */
            #endregion
            string folios_pendientes = "";
            DataTable entradas_pendientes = dao_entradas.get_entradas_pendientes();

            if (entradas_pendientes.Rows.Count > 0)
            {
                foreach (DataRow dr in entradas_pendientes.Rows)
                {
                    folios_pendientes += "#" + dr["entrada_id"] + ",";

                }

                hacerCorte = false;
                folios_pendientes = "Termina las siguientes recepciones para poder continuar " + folios_pendientes.TrimEnd(',');

            }





            #region Operacion de corte
            if (hacerCorte)
            {
                Login_form login = new Login_form("generar_corte_parcial");
                login.ShowDialog();

                if (login.empleado_id != null)
                {
                    DialogResult dr = MessageBox.Show(this, "¿Esta seguro de querer generar el corte?", "Corte Parcial", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (dr == DialogResult.Yes)
                    {
                        var val = dao_cortes.generar_corte_parcial((long)login.empleado_id, dto_corte);

                        if (val.status)
                        {
                            Cortes corte = new Cortes();
                            corte.construccion_ticket(val.elemento_id);
                            corte.print();
                        }

                        MessageBox.Show(this, val.informacion, (val.status) ? "Corte Parcial" : "Error", MessageBoxButtons.OK, (val.status) ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                        if (val.status)
                        {
                            this.Close();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(this, folios_pendientes, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion
        }

        private void Corte_parcial_principal_Shown(object sender, EventArgs e)
        {
            DAO_Ventas dao_ventas = new DAO_Ventas();
            var val = dao_ventas.get_venta_pendiente_corte();

            if (!val.status)
            {
                if (val.informacion.Equals("CIERRE"))
                {
                    Principal principal = this.ParentForm as Principal;

                    Ventas_principal ventas = new Ventas_principal();

                    foreach (Form form_busqueda in principal.MdiChildren)
                    {
                        if (form_busqueda.GetType() == ventas.GetType())
                        {
                            form_busqueda.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show(this, val.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }

        private void btn_reimprimir_corte_Click(object sender, EventArgs e)
        {
            if (ultimo_corte.corte_id > 0)
            {
                Cortes corte = new Cortes();
                corte.construccion_ticket(ultimo_corte.corte_id, true);
                corte.print();
            }
            else
            {
                MessageBox.Show(this, "Caja aun sin cortes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region FORMATO IMPRESION
        private void label46_Click(object sender, EventArgs e)
        {

        }

        private void lbl_iva_ct_Click(object sender, EventArgs e)
        {

        }

        private void textBoxdemorralla_TextChanged(object sender, EventArgs e)
        {
            CalcularValor(textBoxdemorralla, labelvalormorralla, 1);
            CalcularTotal();
        }

        private void textBoxde20_TextChanged(object sender, EventArgs e)
        {
            CalcularValor(textBoxde20, labelvalor20, 20);
            CalcularTotal();
        }

        private void textBoxde50_TextChanged(object sender, EventArgs e)
        {
            CalcularValor(textBoxde50, labelvalor50, 50);
            CalcularTotal();
        }

        private void textBoxde100_TextChanged(object sender, EventArgs e)
        {
            CalcularValor(textBoxde100, labelvalor100, 100);
            CalcularTotal();
        }

        private void textBoxde200_TextChanged(object sender, EventArgs e)
        {
            CalcularValor(textBoxde200, labelvalor200, 200);
            CalcularTotal();
        }

        private void textBoxde500_TextChanged(object sender, EventArgs e)
        {
            CalcularValor(textBoxde500, labelvalor500, 500);
            CalcularTotal();
        }

        private void textBoxde1000_TextChanged(object sender, EventArgs e)
        {
            CalcularValor(textBoxde1000, labelvalor1000, 1000);
            CalcularTotal();
        }

        private void CalcularValor(TextBox txt, Label lbl, decimal valorUnitario)
        {
            if (decimal.TryParse(txt.Text, out decimal cantidad))
            {
                lbl.Text = (cantidad * valorUnitario).ToString("C2");
            }
            else
            {
                lbl.Text = "$0.00";
            }
        }

        private void CalcularTotal()
        {
            decimal total = 0;

            total += ParseLabel(labelvalormorralla);
            total += ParseLabel(labelvalor20);
            total += ParseLabel(labelvalor50);
            total += ParseLabel(labelvalor100);
            total += ParseLabel(labelvalor200);
            total += ParseLabel(labelvalor500);
            total += ParseLabel(labelvalor1000);

            labelvalortotal.Text = total.ToString("C2");
            CalcularGranTotal();
        }



        private decimal ParseLabel(Label lbl)
        {
            if (lbl == null || string.IsNullOrWhiteSpace(lbl.Text))
                return 0m;

            // Toma el ÚLTIMO número con signo opcional y separadores (ej: "TOTAL: -$1,234.50")
            var match = Regex.Matches(lbl.Text, @"-?\s*\$?\s*\d{1,3}(?:[.,]\d{3})*(?:[.,]\d{1,2})?|-?\s*\$?\s*\d+(?:[.,]\d{1,2})?")
                             .Cast<Match>()
                             .LastOrDefault();

            if (match == null) return 0m;

            string num = match.Value
                .Replace(" ", "")
                .Replace("$", "")
                .Replace(",", ""); // dejamos '.' como separador decimal

            if (decimal.TryParse(num, NumberStyles.Number | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
                                 CultureInfo.InvariantCulture, out var val))
                return val;

            return 0m;
        }







        private void ConfigurarDataGridViewVaucheras()
        {

            dgvVaucheras.AllowUserToAddRows = true;
            dgvVaucheras.AllowUserToDeleteRows = true;
            dgvVaucheras.RowHeadersVisible = false;
            dgvVaucheras.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVaucheras.EditingControlShowing += dgvVaucheras_EditingControlShowing;

            dgvVaucheras.Columns.Clear();

            var colConcepto = new DataGridViewTextBoxColumn { Name = "Concepto", HeaderText = "Concepto" };
            var colCantidad = new DataGridViewTextBoxColumn { Name = "Cantidad", HeaderText = "Cantidad" };
            var colImporte = new DataGridViewTextBoxColumn { Name = "Importe", HeaderText = "Importe" };

            dgvVaucheras.Columns.AddRange(colConcepto, colCantidad, colImporte);

            // Formato de moneda SOLO visual
            dgvVaucheras.Columns["Importe"].DefaultCellStyle.Format = "C2";

            // Eventos
            dgvVaucheras.CellEndEdit += dgvVaucheras_CellEndEdit;
            dgvVaucheras.RowsRemoved += (s, ev) => CalcularTotalVaucheras();
            dgvVaucheras.UserAddedRow += (s, ev) => CalcularTotalVaucheras();



            // Fila inicial opcional
            dgvVaucheras.Rows.Add("Mostrador", null, null);
        }
        private void dgvVaucheras_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Sólo nos interesa cuando editan Cantidad o Importe
            var col = dgvVaucheras.CurrentCell?.OwningColumn?.Name;
            if (col == "Cantidad" || col == "Importe")
            {
                if (e.Control is TextBox tb)
                {
                    // Evita suscripciones duplicadas
                    tb.TextChanged -= VaucherasEditingTextChanged;
                    tb.TextChanged += VaucherasEditingTextChanged;
                }
            }
        }
        private void VaucherasEditingTextChanged(object sender, EventArgs e)
        {
            if (dgvVaucheras.CurrentCell == null) return;

            var tb = sender as TextBox;
            var row = dgvVaucheras.CurrentRow;
            var colName = dgvVaucheras.CurrentCell.OwningColumn.Name;

            decimal totalImporte = 0m;
            int totalCantidad = 0;

            foreach (DataGridViewRow r in dgvVaucheras.Rows)
            {
                if (r.IsNewRow) continue;

                // Importe
                if (r == row && colName == "Importe")
                {
                    // usa el texto en edición
                    if (TryGetDecimal(tb.Text, out var impLive))
                        totalImporte += impLive;
                }
                else
                {
                    if (TryGetDecimal(r.Cells["Importe"]?.Value, out var imp))
                        totalImporte += imp;
                }

                // Cantidad
                if (r == row && colName == "Cantidad")
                {
                    if (int.TryParse(tb.Text, out var cantLive))
                        totalCantidad += cantLive;
                }
                else
                {
                    if (int.TryParse((r.Cells["Cantidad"]?.Value ?? "0").ToString(), out var cant))
                        totalCantidad += cant;
                }
            }

            labelTotalVaucheras.Text = $"TOTAL CANTIDAD: {totalCantidad} | TOTAL IMPORTE: {totalImporte:C2}";
            CalcularGranTotal(); // refresca el GRAN TOTAL en vivo
        }

        private void dgvVaucheras_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Normalizar columna "Importe"
            if (e.ColumnIndex == dgvVaucheras.Columns["Importe"].Index)
            {
                var cell = dgvVaucheras.Rows[e.RowIndex].Cells["Importe"];
                if (cell.Value != null && decimal.TryParse(cell.Value.ToString(), out decimal v))
                    cell.Value = v; // ya queda numérico
                else
                    cell.Value = 0m;
            }

            // Normalizar columna "Cantidad"
            if (e.ColumnIndex == dgvVaucheras.Columns["Cantidad"].Index)
            {
                var cell = dgvVaucheras.Rows[e.RowIndex].Cells["Cantidad"];
                if (cell.Value != null && int.TryParse(cell.Value.ToString(), out int c))
                    cell.Value = c;
                else
                    cell.Value = 0;
            }

            // Recalcular totales
            CalcularTotalVaucheras();
        }

        private void CalcularTotalVaucheras()
        {
            decimal totalImporte = 0m;
            decimal totalCantidad = 0;

            foreach (DataGridViewRow row in dgvVaucheras.Rows)
            {
                if (row.IsNewRow) continue;

                // Sumar importe
                if (row.Cells["Importe"].Value != null &&
                    decimal.TryParse(row.Cells["Importe"].Value.ToString(), out decimal imp))
                {
                    totalImporte += imp;
                }

                // Sumar cantidad
                if (row.Cells["Cantidad"].Value != null &&
                    int.TryParse(row.Cells["Cantidad"].Value.ToString(), out int cant))
                {
                    totalCantidad += cant;
                }
            }

            // Mostrar en el label
            labelTotalVaucheras.Text = $"TOTAL CANTIDAD: {totalCantidad} | TOTAL IMPORTE: {totalImporte:C2}";
            CalcularGranTotal();
        }





        private bool TryParseDecimal(string s, out decimal value)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                value = 0;
                return false;
            }

            s = s.Replace("$", "").Replace(",", "").Trim();
            return decimal.TryParse(s, System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowDecimalPoint,
                                     System.Globalization.CultureInfo.InvariantCulture, out value);
        }


        private void ConfigurarGridResumenExtras()
        {
            dgvResumenExtras.AllowUserToAddRows = true;
            dgvResumenExtras.AllowUserToDeleteRows = true;
            dgvResumenExtras.RowHeadersVisible = false;
            dgvResumenExtras.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvResumenExtras.Columns.Clear();

            var colConcepto = new DataGridViewTextBoxColumn { Name = "Concepto", HeaderText = "Concepto" };
            var colCantidad = new DataGridViewTextBoxColumn { Name = "Cantidad", HeaderText = "Cantidad" };
            var colImporte = new DataGridViewTextBoxColumn { Name = "Importe", HeaderText = "Importe" };

            dgvResumenExtras.Columns.AddRange(colConcepto, colCantidad, colImporte);
            dgvResumenExtras.Columns["Importe"].DefaultCellStyle.Format = "C2";

            dgvResumenExtras.CellEndEdit += dgvResumenExtras_CellEndEdit;
            dgvResumenExtras.CellValueChanged += dgvResumenExtras_CellValueChanged;
            dgvResumenExtras.CurrentCellDirtyStateChanged += dgvResumenExtras_CurrentCellDirtyStateChanged;
            dgvResumenExtras.RowsRemoved += (s, ev) => CalcularTotalResumenExtras();
            dgvResumenExtras.UserAddedRow += (s, ev) => CalcularTotalResumenExtras();

            // Precargar conceptos típicos (puedes editar esta lista libremente)
            string[] conceptos = {
        "FAC.EFECTIVO","CREDITOS","TRANSFERENCIAS","HRS.EXTRA","DOLARES",
        "PAGO DOMINGO","CHEQUES","INCENTIVOS","BONAFONT","VALES"
    };

            foreach (var c in conceptos)
                dgvResumenExtras.Rows.Add(c, null, null);

            CalcularTotalResumenExtras();
        }
        private void dgvResumenExtras_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvResumenExtras.IsCurrentCellDirty)
                dgvResumenExtras.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvResumenExtras_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvResumenExtras.Columns[e.ColumnIndex].Name == "Importe")
                CalcularTotalResumenExtras();
        }

        private void dgvResumenExtras_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvResumenExtras.Rows[e.RowIndex];

            // Normaliza formato si editaron "Importe" (como ya lo haces)
            if (dgvResumenExtras.Columns[e.ColumnIndex].Name == "Importe")
            {
                var cell = row.Cells["Importe"];
                if (TryGetDecimal(cell.Value, out decimal v)) cell.Value = v; else cell.Value = 0m;
                CalcularTotalResumenExtras();
                return;
            }

            // >>> NUEVO: si Concepto = DOLARES y editaron "Cantidad", calcular Importe = Cantidad * tipoCambioDolar
            if (dgvResumenExtras.Columns[e.ColumnIndex].Name == "Cantidad")
            {
                var concepto = (row.Cells["Concepto"].Value?.ToString() ?? "").Trim().ToUpper();
                if (concepto == "DOLARES" || concepto == "DÓLARES") // contempla acento
                {
                    decimal cantidad = 0m;
                    // Permite negativos si los usas en tu flujo
                    if (TryGetDecimal(row.Cells["Cantidad"]?.Value, out cantidad))
                    {
                        var importeCalc = cantidad * tipoCambioDolar;
                        row.Cells["Importe"].Value = importeCalc; // se mostrará con C2 por DefaultCellStyle
                    }
                    else
                    {
                        row.Cells["Importe"].Value = 0m;
                    }
                }
            }

            CalcularTotalResumenExtras();
        }


        private void CalcularTotalResumenExtras()
        {
            decimal total = 0m;

            foreach (DataGridViewRow r in dgvResumenExtras.Rows)
            {
                if (r.IsNewRow) continue;
                if (TryGetDecimal(r.Cells["Importe"]?.Value, out decimal imp))
                    total += imp;
            }

            labelTotalExtras.Text = $"TOTAL: {total:C2}";
            CalcularGranTotal();
        }

        private bool TryGetDecimal(object cellValue, out decimal value)
        {
            value = 0m; if (cellValue == null) return false;

            if (cellValue is decimal d) { value = d; return true; }
            if (cellValue is double db) { value = Convert.ToDecimal(db); return true; }
            if (cellValue is int i) { value = i; return true; }

            var s = cellValue.ToString().Replace("$", "").Replace(",", "").Trim();
            return decimal.TryParse(s, System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowDecimalPoint,
                                    System.Globalization.CultureInfo.InvariantCulture, out value);
        }

        private void CalcularGranTotal()
        {
            decimal den = ParseLabel(labelvalortotal);               // denominaciones
            decimal vch = ParseLabel(labelTotalVaucheras);           // resumen vaucheras
            decimal ext = ParseLabel(labelTotalExtras);              // extras

            decimal granTotal = den + vch + ext;
            labelGranTotal.Text = $"GRAN TOTAL: {granTotal:C2}";

        }


        private void btnImprimir_Click(object sender, EventArgs e)
        {
            var dto = new DTO_ResumenVaucherasTicket
            {
                Turno = comboTurno.Text, // tu ComboBox de turno
                Fecha = DateTime.Now
            };

            // ==== Vaucheras (del grid) ====
            decimal totalCantV = 0;
            decimal totalImpV = 0m;

            foreach (DataGridViewRow r in dgvVaucheras.Rows)
            {
                if (r.IsNewRow) continue;

                string concepto = (r.Cells["Concepto"]?.Value ?? "").ToString();
                decimal cantidad = 0; decimal.TryParse((r.Cells["Cantidad"]?.Value ?? " ").ToString(), out cantidad);

                decimal importe = 0m;
                TryGetDecimal(r.Cells["Importe"]?.Value, out importe);

                // Guarda aunque sean ceros por registro de captura
                dto.Vaucheras.Add(new DTO_ItemResumen { Concepto = concepto, Cantidad = cantidad, Importe = importe });
                totalCantV += cantidad;
                totalImpV += importe;
            }

            dto.TotalVaucherasCantidad = totalCantV;
            dto.TotalVaucherasImporte = totalImpV;

            // ==== Extras (del grid) ====
            decimal totalExt = 0m;

            foreach (DataGridViewRow r in dgvResumenExtras.Rows)
            {
                if (r.IsNewRow) continue;

                string concepto = (r.Cells["Concepto"]?.Value ?? "").ToString();
                int cantidad = 0; int.TryParse((r.Cells["Cantidad"]?.Value ?? " ").ToString(), out cantidad);

                decimal importe = 0m;
                TryGetDecimal(r.Cells["Importe"]?.Value, out importe);

                dto.Extras.Add(new DTO_ItemResumen { Concepto = concepto, Cantidad = cantidad, Importe = importe });
                totalExt += importe;
            }

            dto.TotalExtras = totalExt;

            // ==== Denominaciones (de tus TextBox y total label) ====


            // Helpers
            int ParseInt(TextBox t)
            {
                int v;
                return int.TryParse(t.Text.Trim(),
                                    NumberStyles.Integer,
                                    CultureInfo.CurrentCulture,
                                    out v) ? v : 0;
            }

            decimal ParseDecimal(TextBox t)
            {
                var s = (t.Text ?? "").Trim();

                if (s == "") return 0m;

                decimal v;

                // 1) Intento con cultura del sistema (acepta coma o punto según región)
                if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.CurrentCulture, out v))
                    return v;

                // 2) Intento con InvariantCulture (punto decimal)
                if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out v))
                    return v;

                // 3) Normalizo coma -> punto y reintento
                s = s.Replace(',', '.');
                if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out v))
                    return v;

                return 0m;
            }

            // ==== Denominaciones (de tus TextBox y total) ====
            dto.Denoms.Morralla = ParseDecimal(textBoxdemorralla); // <- DECIMAL, no int

            dto.Denoms.D20 = ParseInt(textBoxde20);
            dto.Denoms.D50 = ParseInt(textBoxde50);
            dto.Denoms.D100 = ParseInt(textBoxde100);
            dto.Denoms.D200 = ParseInt(textBoxde200);
            dto.Denoms.D500 = ParseInt(textBoxde500);
            dto.Denoms.D1000 = ParseInt(textBoxde1000);

            // Si tu DTO no calcula Total automáticamente, hazlo aquí:
            dto.Denoms.Total =
                dto.Denoms.Morralla
              + dto.Denoms.D20 * 20m
              + dto.Denoms.D50 * 50m
              + dto.Denoms.D100 * 100m
              + dto.Denoms.D200 * 200m
              + dto.Denoms.D500 * 500m
              + dto.Denoms.D1000 * 1000m;


            // Tu label de total de denominaciones ya calculado:
            dto.Denoms.Total = ParseLabel(labelvalortotal); // reutiliza tu ParseLabel robusto

            // ==== Gran total (usa tus labels ya mostrados) ====
            var totalV = ParseLabel(labelTotalVaucheras);
            var totalE = ParseLabel(labelTotalExtras);
            var totalD = ParseLabel(labelvalortotal);
            dto.GranTotal = totalV + totalE + totalD;

            // ==== Imprimir ====
            using (var t = new Ticket_ResumenVaucheras())
            {
                t.Construir(dto);
                if (t.Print())
                    MessageBox.Show("No se pudo enviar el ticket a la impresora.", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CargarTipoCambioDolar()
        {
            try
            {
                // Lee el valor de la tabla farmacontrol_global.config
                string valor = Config_helper.get_config_global("tipo_cambio_dolar"); // devuelve string
                                                                                     // Intentamos parsear con culturas comunes
                if (!decimal.TryParse(valor, out tipoCambioDolar))
                {
                    // intenta con es-MX
                    if (!decimal.TryParse(valor, System.Globalization.NumberStyles.Any, new System.Globalization.CultureInfo("es-MX"), out tipoCambioDolar))
                    {
                        // intenta con InvariantCulture
                        decimal.TryParse(valor, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out tipoCambioDolar);
                    }
                }
            }
            catch
            {
                tipoCambioDolar = 0m; // si falla, evita excepciones
            }
        }

        #endregion
    }
}
