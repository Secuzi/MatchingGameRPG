using System;

using System.Windows.Forms;
using Matching_RPG.Implementation_Classes;

namespace Matching_RPG
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainMenu());
		}
	}
}
