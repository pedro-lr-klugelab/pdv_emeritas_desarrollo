using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;
namespace Farmacontrol_PDV.FORMS.reportes.articulos_vendidos
{
    public partial class articulo_vendidos_principal : Form
    {
        public articulo_vendidos_principal()
        {
            InitializeComponent();
            dgv_prod_vendidos.ClearSelection();

            proBarr.Visible = false;
           

        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {

            proBarr.Visible = true;
            proBarr.Value = 30;

            DAO_Ventas dao = new DAO_Ventas();

            string f_inicial = dtInicial.Value.ToString("yyyy-MM-dd");

            string f_final = dtFinal.Value.ToString("yyyy-MM-dd");

            proBarr.Value = 70;
            dgv_prod_vendidos.DataSource = dao.get_productos_vendidos(f_inicial,f_final);

            proBarr.Value = 100;
            
            dgv_prod_vendidos.ClearSelection();

           
            proBarr.Visible = false;
        }




        
    }
}
