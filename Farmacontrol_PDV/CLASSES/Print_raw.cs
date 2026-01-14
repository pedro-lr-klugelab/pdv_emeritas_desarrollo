using System;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Farmacontrol_PDV.CLASSES
{
	class Print_raw
	{
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
		public class DOCINFOA
		{
			[MarshalAs(UnmanagedType.LPStr)]
			public string pDocName;
			[MarshalAs(UnmanagedType.LPStr)]
			public string pOutputFile;
			[MarshalAs(UnmanagedType.LPStr)]
			public string pDataType;
		}

		[DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

		[DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		public static extern bool ClosePrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

		[DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		public static extern bool EndDocPrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		public static extern bool StartPagePrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		public static extern bool EndPagePrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

		private IntPtr HandlePrinter;
		private PrinterSettings ps;

		public Print_raw()
		{
			HandlePrinter = IntPtr.Zero;
			ps = new PrinterSettings();
		}

		public string PrinterName
		{
			get
			{
				return ps.PrinterName;
			}
			set
			{
				ps.PrinterName = value;
			}
		}

		public bool ChoosePrinter()
		{
			PrintDialog pd = new PrintDialog();
			pd.PrinterSettings = ps;

			if (pd.ShowDialog() == DialogResult.OK)
			{
				ps = pd.PrinterSettings;
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool Open(string DocName)
		{
			if (HandlePrinter != IntPtr.Zero)
			{
				return false;
			}

			bool risp = OpenPrinter(ps.PrinterName, out HandlePrinter, IntPtr.Zero);

			if (risp == false)
			{
				return false;
			}

			DOCINFOA MyDocInfo = new DOCINFOA();
			MyDocInfo.pDocName = DocName;
			MyDocInfo.pOutputFile = null;
			MyDocInfo.pDataType = "RAW";

			if (StartDocPrinter(HandlePrinter, 1, MyDocInfo))
			{
				StartPagePrinter(HandlePrinter); //starts a page       
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool Close()
		{
			if (HandlePrinter == IntPtr.Zero)
			{
				return false;
			}

			if (EndPagePrinter(HandlePrinter) == false)
			{
				return false;
			}

			if (EndDocPrinter(HandlePrinter) == false)
			{
				return false;
			}

			if (ClosePrinter(HandlePrinter) == false)
			{
				return false;
			}

			HandlePrinter = IntPtr.Zero;
			return true;
		}

		public bool Print(string outputstring)
		{
			if (HandlePrinter == IntPtr.Zero) return false;
			IntPtr buf = Marshal.StringToCoTaskMemAnsi(outputstring);
			Int32 done = 0;
			bool result_write = WritePrinter(HandlePrinter, buf, outputstring.Length, out done);
			Marshal.FreeCoTaskMem(buf);
			return result_write;
		}
	}
}