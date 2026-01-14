using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	[Serializable]

	public class DTO_Impresion_ip : IDisposable
	{
		public string texto				{ set; get; }
		public string tipo				{ set; get; }
		public long folio				{ set; get; }
		public bool impresora_tickets	{ set; get; }
        public long terminal_id         { set; get; }
        public bool reimpresion         { set; get; }

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}

    [Serializable]
    public class DTO_Impresion_ip_new : IDisposable
    {
        public long id { set; get; }
        public bool impresora_tickets { set; get; }
        public bool reimpresion { set; get; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
