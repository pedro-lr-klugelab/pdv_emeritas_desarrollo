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

namespace Farmacontrol_PDV.FORMS.inventarios.materias_primas
{
    public partial class materias_primas : Form
    {
        public materias_primas()
        {
            InitializeComponent();
            listBox1.Visible = false;
            textBox1.Focus();
            textBox1.TextChanged += txtCodigo_TextChanged;
            textBox1.KeyDown += txtCodigo_KeyDown;
            listBox1.Click += lstSugerencias_Click;
            textBox1.Focus();
            



        }
       
        private List<GetArticulosDTO> articulosEncontrados = new List<GetArticulosDTO>();
        private List<GetArticulosDTO> articulosEncontrados_resultado = new List<GetArticulosDTO>();

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            string texto = textBox1.Text.Trim();

            if (texto.Length < 2)
            {
                listBox1.Visible = false;
                return;
            }

            try
            {
                articulosEncontrados = DAO_Articulos.GetArticulos_materia_prima(texto);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar artículos:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listBox1.Items.Clear();

            if (articulosEncontrados.Any())
            {
                foreach (var art in articulosEncontrados)
                {
                    listBox1.Items.Add($"{art.codigo} - {art.nombre}");
                }

                listBox1.Visible = true;
            }
            else
            {
                listBox1.Visible = false;
            }
        }
        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && listBox1.Visible && listBox1.Items.Count > 0)
            {
                SeleccionarArticulo(listBox1.SelectedIndex);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Down && listBox1.Visible)
            {
                listBox1.Focus();
                if (listBox1.Items.Count > 0)
                    listBox1.SelectedIndex = 0;
            }
        }
        private void lstSugerencias_Click(object sender, EventArgs e)
        {
            SeleccionarArticulo(listBox1.SelectedIndex);
        }
        private void SeleccionarArticulo(int index)
        {
            if (index >= 0 && index < articulosEncontrados.Count)
            {
                var seleccionado = articulosEncontrados[index];
                textBox1.Text = seleccionado.codigo;

                //MessageBox.Show($"Seleccionaste: {seleccionado.codigo} - {seleccionado.nombre}", "Producto");

                listBox1.Visible = false;
                
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
            dataGridView1.Columns.Add("VOLUMEN", "VOLUMEN");

            // Opcional: Ajustar tamaños automáticos
            dataGridView1.Columns["AMECOP"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["EXISTENCIA ANT"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["EXISTENCIA fin"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["DIFERENCIA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["VOLUMEN"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string codigoSeleccionado = textBox1.Text.Trim();
            decimal volumen = numericUpDown2.Value;


            if (string.IsNullOrEmpty(codigoSeleccionado))
            {
                MessageBox.Show("Primero selecciona un artículo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var articulo = articulosEncontrados.FirstOrDefault(a => a.codigo == codigoSeleccionado);

            if (articulo == null)
            {
                MessageBox.Show("El artículo no se encuentra en la lista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int diferencia = (int)numericUpDown1.Value;

            int existenciaFin = articulo.existencia_total - diferencia;

            // Validación de existencia
            if (existenciaFin < 0)
            {
                MessageBox.Show("No se puede añadir como materia prima porque no hay suficiente producto en existencia.", "Sin existencias", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Agregar fila al DataGridView
            int fila = dataGridView1.Rows.Add();
            var row = dataGridView1.Rows[fila];
            row.Cells["AMECOP"].Value = articulo.codigo;
            row.Cells["DESCRIPCION"].Value = articulo.nombre;
            row.Cells["EXISTENCIA ANT"].Value = articulo.existencia_total;
            row.Cells["EXISTENCIA FIN"].Value = existenciaFin;
            row.Cells["DIFERENCIA"].Value = -diferencia;
            row.Cells["AMECOP"].Tag = articulo.articulo_id;
            row.Cells["VOLUMEN"].Value = volumen;
            row.Tag = articulo;


            // Limpiar campos
            textBox1.Text = "";
            listBox1.Visible = false;
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            textBox1.Focus();
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "¿Estás seguro que deseas eliminar esta fila?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
            {
                e.Cancel = true; // Cancela la eliminación
            }
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
                dao_ajustes_existencias.set_comentario(ajuste_existencia_id, "AJUSTE DE MATERIA PRIMA");

                // 3. Generar lista de DTOs directamente desde filas del grid
                List<DTO_Detallado_ajustes_existencias> listaDetalles = new List<DTO_Detallado_ajustes_existencias>();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    var articulo = row.Tag as GetArticulosDTO;
                    if (articulo == null) continue;

                    int existenciaAnt = Convert.ToInt32(row.Cells["EXISTENCIA ANT"].Value);
                    int existenciaFin = Convert.ToInt32(row.Cells["EXISTENCIA FIN"].Value);
                   

                    DTO_Detallado_ajustes_existencias dto = new DTO_Detallado_ajustes_existencias
                    {
                        ajuste_existencia_id = ajuste_existencia_id,
                        articulo_id = articulo.articulo_id,
                        lote = articulo.lote ?? "",
                        caducidad = articulo.caducidad,
                        cantidad = existenciaFin
                    };

                    listaDetalles.Add(dto);
                }

                // 4. Registrar detalles
                dao_ajustes_existencias.registrar_detallado_ajuste_existencia(listaDetalles);

                // 5. Terminar ajuste
                var result = dao_ajustes_existencias.terminar_ajustes_existencias(ajuste_existencia_id, (long)login.empleado_id);
                if (result <= 0)
                    throw new Exception("El método terminar_ajustes_existencias no afectó ningún registro.");

                // 6. Imprimir ticket
                Ticket_Ajustes_materia_prima ticket = new Ticket_Ajustes_materia_prima();
                ticket.construccion_ticket(ajuste_existencia_id);
                ticket.print();

                // 6.1 Insertar materias primas
                List<DTO_Materia_Prima> materiasPrimas = new List<DTO_Materia_Prima>();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    var articulo = row.Tag as GetArticulosDTO;
                    if (articulo == null) continue;

                    decimal volumenUnitario = Convert.ToDecimal(row.Cells["VOLUMEN"].Value);
                    int cantidad = Math.Abs(Convert.ToInt32(row.Cells["DIFERENCIA"].Value)); // Asegura valor positivo
                    decimal volumenTotal = volumenUnitario * cantidad;
                    /* MessageBox.Show(
                                $"ARTICULO ID: {articulo.articulo_id}\n" +
                                $"VOLUMEN UNITARIO: {volumenUnitario}\n" +
                                $"CANTIDAD: {cantidad}\n" +
                                $"VOLUMEN TOTAL: {volumenTotal}",
                                "Datos de Materia Prima",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );*/
                    materiasPrimas.Add(new DTO_Materia_Prima
                    {
                        articulo_id = articulo.articulo_id,
                        volumen = volumenTotal,
                        cantidad = volumenUnitario,
                        existencia_actual = cantidad,
                        observaciones = "Ajuste generado el " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    });
                }


                DAO_Ajustes_existencias.set_materia_prima(materiasPrimas);



                // 7. Confirmación
                MessageBox.Show(this, "Los cambios fueron afectados correctamente", "Ajuste de Existencias", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error crítico al ejecutar el ajuste:\n" + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }

}
