using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.CLASSES
{
	public class ComboBoxItem
	{
		public string Text { get; set; }
		public object Value { get; set; }
		public int? elemento_id { get; set; }

		public override string ToString()
		{
			return Text;
		}
	}
}
