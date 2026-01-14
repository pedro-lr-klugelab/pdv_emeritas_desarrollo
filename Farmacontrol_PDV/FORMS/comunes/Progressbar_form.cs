using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Farmacontrol_PDV.FORMS.comunes
{
	public partial class Progressbar_form : Form
	{
		public Progressbar_form()
		{
			InitializeComponent();
		}

		private void Progressbar_form_KeyDown(object sender, KeyEventArgs e)
		{
			if(Convert.ToInt32(e.KeyCode) == 27)
			{
				this.Close();
			}
		}
	}
}
