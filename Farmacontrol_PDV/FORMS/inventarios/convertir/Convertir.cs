using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.FORMS.comunes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Farmacontrol_PDV.FORMS.inventarios.convertir
{
    public partial class Convertir : Form
    {
        public Convertir()
        {
            InitializeComponent();

            codigo_Resultado.Focus();
            codigo_Resultado.TextChanged += txtCodigo_TextChanged;
            codigo_Resultado.KeyDown += txtCodigo_KeyDown;
            lstSugerencias_Resultado.Click += lstSugerencias_Click;
            codigo_Origen.TextChanged += txtCodigo_TextChanged_Resultado;
            codigo_Origen.KeyDown += txtCodigo_KeyDown_Resultado;
            lstSugerencias_Origen.Click += lstSugerencias_Click_Resultado;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            dataGridView1.UserDeletingRow += dataGridView1_UserDeletingRow;
            dataGridView1.UserDeletedRow += dataGridView1_UserDeletedRow;
            lstSugerencias_Origen.KeyDown += lstSugerencias_Origen_KeyDown;
            lstSugerencias_Resultado.KeyDown += lstSugerencias_Resultado_KeyDown;

            DTO_Ajustes_existencias dto_ajustes_existencias = new DTO_Ajustes_existencias();
            DAO_Ajustes_existencias dao_ajustes_existencias = new DAO_Ajustes_existencias();
            DAO_Articulos dao_articulos = new DAO_Articulos();
            DTO_Articulo articulo_registro = new DTO_Articulo();


            codigo_Resultado.Focus();
        }

        private Guid? ultimoTagEliminado = null;
        private List<GetArticulosDTO> articulosEncontrados = new List<GetArticulosDTO>();
        private List<GetArticulosDTO> articulosEncontrados_resultado = new List<GetArticulosDTO>();

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            string texto = codigo_Resultado.Text.Trim();

            if (texto.Length < 2)
            {
                lstSugerencias_Resultado.Visible = false;
                return;
            }

            try
            {
                articulosEncontrados = DAO_Articulos.GetArticulos(texto);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar artículos:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lstSugerencias_Resultado.Items.Clear();

            if (articulosEncontrados.Any())
            {
                foreach (var art in articulosEncontrados)
                {
                    lstSugerencias_Resultado.Items.Add($"{art.codigo} - {art.nombre}");
                }

                lstSugerencias_Resultado.Visible = true;
            }
            else
            {
                lstSugerencias_Resultado.Visible = false;
            }
        }
        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && lstSugerencias_Resultado.Visible && lstSugerencias_Resultado.Items.Count > 0)
            {
                SeleccionarArticulo(lstSugerencias_Resultado.SelectedIndex);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Down && lstSugerencias_Resultado.Visible)
            {
                lstSugerencias_Resultado.Focus();
                if (lstSugerencias_Resultado.Items.Count > 0)
                    lstSugerencias_Resultado.SelectedIndex = 0;
            }
        }
        private void lstSugerencias_Click(object sender, EventArgs e)
        {
            SeleccionarArticulo(lstSugerencias_Resultado.SelectedIndex);
        }
        private void SeleccionarArticulo(int index)
        {
            if (index >= 0 && index < articulosEncontrados.Count)
            {
                var seleccionado = articulosEncontrados[index];
                codigo_Resultado.Text = seleccionado.codigo;

                //MessageBox.Show($"Seleccionaste: {seleccionado.codigo} - {seleccionado.nombre}", "Producto");

                lstSugerencias_Resultado.Visible = false;
                comboBox_Resultado.Focus();
                comboBox_Resultado.DroppedDown = true;
            }
        }
        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            numericUpDown1_Resultado.Focus();
            numericUpDown1_Resultado.Select(0, numericUpDown1_Resultado.Text.Length);
            
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ActualizarLabelResumen();
            button1.Focus();

        }
        private void txtCodigo_TextChanged_Resultado(object sender, EventArgs e)
        {
            string texto = codigo_Origen.Text.Trim();

            if (texto.Length < 2)
            {
                lstSugerencias_Origen.Visible = false;
                return;
            }

            try
            {
                articulosEncontrados_resultado = DAO_Articulos.GetArticulos(texto);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar artículos:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lstSugerencias_Origen.Items.Clear();

            if (articulosEncontrados_resultado.Any())
            {
                foreach (var art in articulosEncontrados_resultado)
                {
                    lstSugerencias_Origen.Items.Add($"{art.codigo} - {art.nombre}");
                }

                lstSugerencias_Origen.Visible = true;
            }
            else
            {
                lstSugerencias_Origen.Visible = false;
            }
        }
        private void txtCodigo_KeyDown_Resultado(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && lstSugerencias_Origen.Visible && lstSugerencias_Origen.Items.Count > 0)
            {
                SeleccionarArticulo_Resultado(lstSugerencias_Origen.SelectedIndex);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Down && lstSugerencias_Origen.Visible)
            {
                lstSugerencias_Origen.Focus();
                
                if (lstSugerencias_Origen.Items.Count > 0)
                    lstSugerencias_Origen.SelectedIndex = 0;
            }
        }
        private void lstSugerencias_Click_Resultado(object sender, EventArgs e)
        {
            SeleccionarArticulo_Resultado(lstSugerencias_Origen.SelectedIndex);
        }
        private void lstSugerencias_Resultado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && lstSugerencias_Resultado.SelectedIndex >= 0)
            {
                SeleccionarArticulo(lstSugerencias_Resultado.SelectedIndex);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void lstSugerencias_Origen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && lstSugerencias_Origen.SelectedIndex >= 0)
            {
                SeleccionarArticulo_Resultado(lstSugerencias_Origen.SelectedIndex);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void SeleccionarArticulo_Resultado(int index)
        {
            if (index >= 0 && index < articulosEncontrados_resultado.Count)
            {
                var seleccionado = articulosEncontrados_resultado[index];

                // Verificar existencia
                if (seleccionado.existencia_total <= 0)
                {
                    MessageBox.Show($"El producto \"{seleccionado.nombre}\" no tiene existencia.", "Sin existencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                codigo_Origen.Text = seleccionado.codigo;

               // MessageBox.Show($"Seleccionaste: {seleccionado.codigo} - {seleccionado.nombre} - {seleccionado.existencia_total}", "Producto");

                lstSugerencias_Origen.Visible = false;
                comboBox_Origen.Focus();
                comboBox_Origen.DroppedDown = true;
            }
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ActualizarLabelResumenResultado();
            codigo_Resultado.Focus();

        }
        private void ActualizarLabelResumen()
        {
            int cantidad = (int)numericUpDown1_Resultado.Value;
            string codigoTexto = codigo_Resultado.Text;
            string nombreTexto = articulosEncontrados.FirstOrDefault(a => a.codigo == codigoTexto)?.nombre ?? "";
            string unidad = comboBox_Resultado.SelectedItem?.ToString() ?? "";

            if (!string.IsNullOrWhiteSpace(nombreTexto) && !string.IsNullOrWhiteSpace(unidad))
            {
                label2_Resultado.Text = $"{cantidad} piezas de \n {codigoTexto} - {nombreTexto} \n de un {unidad} (kg - l)";
            }
        }
        private void ActualizarLabelResumenResultado()
        {
           // int cantidad = (int)numericUpDown1.Value;
            string codigoTexto = codigo_Origen.Text;
            string nombreTexto = articulosEncontrados_resultado.FirstOrDefault(a => a.codigo == codigoTexto)?.nombre ?? "";
            string unidad = comboBox_Origen.SelectedItem?.ToString() ?? "";

            if (!string.IsNullOrWhiteSpace(nombreTexto) && !string.IsNullOrWhiteSpace(unidad))
            {
                label1_Origen.Text = $" {codigoTexto} - {nombreTexto} \n de un {unidad} (kg - l)";
            }
        }
        private void Convertir_Load(object sender, EventArgs e)
        {
            dataGridView1.RowHeadersVisible = false;

            dataGridView1.Columns.Clear();


            dataGridView1.Columns.Add("AMECOP", "AMECOP");
            dataGridView1.Columns.Add("DESCRIPCION", "DESCRIPCIÓN");
            dataGridView1.Columns.Add("EXISTENCIA ANT", "EXISTENCIA ANT");
            dataGridView1.Columns.Add("EXISTENCIA FIN", "EXISTENCIA FIN");
            dataGridView1.Columns.Add("DIFERENCIA", "DIFERENCIA");

            // Opcional: Ajustar tamaños automáticos
            dataGridView1.Columns["AMECOP"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["EXISTENCIA ANT"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["EXISTENCIA fin"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["DIFERENCIA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        private double ObtenerFactorNumerico(string unidad)
        {
            if (unidad == "1L/K") return 1.0;
            else if (unidad == "500") return 0.5;
            else if (unidad == "250") return 0.25;
            else if (unidad == "125") return 0.125;
            else if (unidad == "100") return 0.1;
            else if (unidad == "50") return 0.05;
            else if (unidad == "25") return 0.025;
            else if (unidad == "20") return 0.02;
            else if (unidad == "10") return 0.01;
            else if (unidad == "5") return 0.005;
            else if (unidad == "1") return 0.001;
            else return 1.0; // valor por defecto si no se reconoce la unidad
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Obtener la unidad del origen y resultado
            string unidadOrigen = comboBox_Origen.SelectedItem?.ToString();
            string unidadResultado = comboBox_Resultado.SelectedItem?.ToString();

            // Verificamos existencia y cantidad deseada
            string codigoOrigen = codigo_Origen.Text;
            string codigoResultado = codigo_Resultado.Text;

            var articuloOrigen = articulosEncontrados_resultado.FirstOrDefault(a => a.codigo == codigoOrigen);
            var articuloResultado = articulosEncontrados.FirstOrDefault(a => a.codigo == codigoResultado);

            if (articuloOrigen == null || articuloResultado == null)
            {
                MessageBox.Show("Debes seleccionar ambos productos.", "Error");
                return;
            }

            int cantidadDeseada = (int)numericUpDown1_Resultado.Value;

            // Convertimos unidad a factor numérico
            double factorOrigen = ObtenerFactorNumerico(unidadOrigen);
            double factorResultado = ObtenerFactorNumerico(unidadResultado);


            // ¿Cuánto volumen total se desea?
            double volumenRequerido = cantidadDeseada * factorResultado;

            // ¿Cuántas piezas del origen necesito?
            double piezasNecesarias = volumenRequerido / factorOrigen;

            // Redondeo hacia arriba
            int piezasReales = (int)Math.Ceiling(piezasNecesarias);

            if (piezasReales > articuloOrigen.existencia_total)
            {
                MessageBox.Show("No hay suficiente existencia para hacer la conversión.", "Error");
                return;
            }

            // ¿Cuánto volumen real se va a generar?
            double volumenGenerado = piezasReales * factorOrigen;

            // ¿Cuántas piezas de resultado podemos producir con ese volumen?
            int cantidadResultadoFinal = (int)Math.Ceiling(volumenGenerado / factorResultado);

            // Mostrar aviso si hubo ajuste por redondeo
            if (cantidadResultadoFinal > cantidadDeseada)
            {
                double sobrante = volumenGenerado - (cantidadDeseada * factorResultado);
                MessageBox.Show($"Se generó un {unidadResultado} adicional para evitar desperdicio ({sobrante:0.##}L sobrantes utilizados).", "Volumen ajustado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Identificador único para el par de filas
            Guid parId = Guid.NewGuid();

            // Fila ORIGEN (salida)
            int filaOrigen = dataGridView1.Rows.Add();
            var rowOrigen = dataGridView1.Rows[filaOrigen];
            rowOrigen.Cells["AMECOP"].Value = articuloOrigen.codigo;
            rowOrigen.Cells["AMECOP"].Tag = articuloOrigen.articulo_id;
            rowOrigen.Cells["DESCRIPCION"].Value = articuloOrigen.nombre;
            rowOrigen.Cells["EXISTENCIA ANT"].Value = articuloOrigen.existencia_total;
            rowOrigen.Cells["EXISTENCIA FIN"].Value = articuloOrigen.existencia_total - piezasReales;
            rowOrigen.Cells["DIFERENCIA"].Value = -piezasReales;
            rowOrigen.Tag = parId;

            // Fila RESULTADO (entrada)
            int filaResultado = dataGridView1.Rows.Add();
            var rowResultado = dataGridView1.Rows[filaResultado];
            rowResultado.Cells["AMECOP"].Value = articuloResultado.codigo;
            rowResultado.Cells["AMECOP"].Tag = articuloResultado.articulo_id;
            rowResultado.Cells["DESCRIPCION"].Value = articuloResultado.nombre;
            rowResultado.Cells["EXISTENCIA ANT"].Value = articuloResultado.existencia_total;
            rowResultado.Cells["EXISTENCIA FIN"].Value = articuloResultado.existencia_total + cantidadResultadoFinal;
            rowResultado.Cells["DIFERENCIA"].Value = cantidadResultadoFinal;
            rowResultado.Tag = parId;

            // Fila separadora
            int filaSeparador = dataGridView1.Rows.Add();
            var rowSeparador = dataGridView1.Rows[filaSeparador];
            rowSeparador.DefaultCellStyle.BackColor = Color.LightGray;
            rowSeparador.DefaultCellStyle.ForeColor = Color.DarkSlateGray;
            rowSeparador.DefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            rowSeparador.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            rowSeparador.Tag = "SEPARADOR";

            LimpiarCampos();
        }
        private void LimpiarCampos()
        {
            codigo_Origen.Text = "";
            codigo_Resultado.Text = "";
            comboBox_Origen.SelectedIndex = -1;
            comboBox_Resultado.SelectedIndex = -1;
            numericUpDown1_Resultado.Value = 0;
            label1_Origen.Text = "";
            label2_Resultado.Text = "";
            articulosEncontrados.Clear();
            articulosEncontrados_resultado.Clear();
        }
        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            // Confirmación antes de eliminar
            DialogResult result = MessageBox.Show(
                "¿Estás seguro que deseas eliminar este movimiento y su par relacionado?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            // Guardamos el Tag de la fila que se va a eliminar
            if (e.Row.Tag != null)
                ultimoTagEliminado = (Guid)e.Row.Tag;
            else
                ultimoTagEliminado = null;
        }
        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (ultimoTagEliminado == null)
                return;

            // Lista temporal para filas a eliminar
            List<DataGridViewRow> filasAEliminar = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Eliminar la otra fila del par
                if (row.Tag is Guid tagGuid && tagGuid == ultimoTagEliminado)
                {
                    filasAEliminar.Add(row);
                }

                // Eliminar el separador si lo encuentras
                if (row.Tag is string tagStr && tagStr == "SEPARADOR")
                {
                    filasAEliminar.Add(row);
                }
            }

            // Eliminar todas las filas encontradas
            foreach (var fila in filasAEliminar)
            {
                dataGridView1.Rows.Remove(fila);
            }

            ultimoTagEliminado = null;
        }
        private void btn_terminar_Click(object sender, EventArgs e)
        {
            Login_form login = new Login_form("crear_ajuste_conversion");
            login.ShowDialog();

            if (login.empleado_id == null) return;

            DialogResult dr = MessageBox.Show(
                this,
                "Está a punto de crear una conversión, ¿Desea continuar?",
                "Crear Ajuste de Existencias",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            if (dr != DialogResult.Yes) return;

            DAO_Ajustes_existencias dao_ajustes_existencias = new DAO_Ajustes_existencias();
            long ajuste_existencia_id = 0;

            try
            {
                // 1. Crear ajuste
                ajuste_existencia_id = dao_ajustes_existencias.crear_ajuste_existencia((long)login.empleado_id);

                // 2. Insertar comentario
                string comentario = "AJUSTE DE CONVERSION";
                dao_ajustes_existencias.set_comentario(ajuste_existencia_id, comentario);

                // 3. Agrupar artículos
                Dictionary<int, Tuple<int, int>> agrupados = new Dictionary<int, Tuple<int, int>>();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.Tag is string tagStr && tagStr == "SEPARADOR") continue;

                    object tag = row.Cells["AMECOP"].Tag;
                    if (tag == null) continue;

                    int articuloId = Convert.ToInt32(tag);
                    int existenciaAnt = Convert.ToInt32(row.Cells["EXISTENCIA ANT"].Value);
                    int existenciaFin = Convert.ToInt32(row.Cells["EXISTENCIA FIN"].Value);
                    int diferencia = existenciaFin - existenciaAnt;

                    if (agrupados.ContainsKey(articuloId))
                    {
                        Tuple<int, int> prev = agrupados[articuloId];
                        agrupados[articuloId] = new Tuple<int, int>(prev.Item1, prev.Item2 + diferencia);
                    }
                    else
                    {
                        agrupados[articuloId] = new Tuple<int, int>(existenciaAnt, diferencia);
                    }
                }

                // 4. Generar lista de DTOs
                List<DTO_Detallado_ajustes_existencias> listaDetalles = new List<DTO_Detallado_ajustes_existencias>();

                foreach (var kvp in agrupados)
                {
                    int articuloId = kvp.Key;
                    int existenciaAnterior = kvp.Value.Item1;
                    int diferencia = kvp.Value.Item2;

                    DTO_Detallado_ajustes_existencias dto = new DTO_Detallado_ajustes_existencias
                    {
                        ajuste_existencia_id = ajuste_existencia_id,
                        articulo_id = articuloId,
                        caducidad = "0000-00-00",
                        lote = "",
                        cantidad = existenciaAnterior + diferencia
                    };

                    listaDetalles.Add(dto);
                }

                // 5. Registrar detalles
                dao_ajustes_existencias.registrar_detallado_ajuste_existencia(listaDetalles);

                // 6. Terminar ajuste
                var result = dao_ajustes_existencias.terminar_ajustes_existencias(ajuste_existencia_id, (long)login.empleado_id);

                if (result <= 0)
                {
                    throw new Exception("El método terminar_ajustes_existencias no afectó ningún registro. Puede que no existan artículos o el ajuste ya fue finalizado.");
                }

                // 7. Imprimir ticket (no afecta BD)
                Ticket_Ajustes_conversion ticket = new Ticket_Ajustes_conversion();
                ticket.construccion_ticket(ajuste_existencia_id);
                ticket.print();

                // 8. Mostrar mensaje y limpiar
                MessageBox.Show(this, "Los cambios fueron afectados correctamente", "Ajuste de Existencias", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.Rows.Clear();
            }
            catch (Exception ex)
            {
                if (ajuste_existencia_id > 0)
                {
                   // dao_ajustes_existencias.cancelar_ajuste_si_falla(ajuste_existencia_id);
                }

                MessageBox.Show(this, "Error crítico al ejecutar el ajuste:\n" + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }


}















