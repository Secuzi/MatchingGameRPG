using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Matching_RPG.Implementation_Classes
{
	internal class CustomFont
	{
		string _directory = AppDomain.CurrentDomain.BaseDirectory;

		string _path;
		int _size;
        
        public CustomFont(int size)
        {
			this._size = size;

		}
		public Font GetCustomFontInPixelUnit()
		{
			Font _customFont;
			_path = Path.Combine(_directory, @"..\..\Font\upheavtt.ttf");

			if (File.Exists(_path))
			{
				using (PrivateFontCollection privateFontCollection = new PrivateFontCollection())
				{
					privateFontCollection.AddFontFile(_path);
					_customFont = new Font(privateFontCollection.Families[0], _size, FontStyle.Regular, GraphicsUnit.Pixel);

				}
			}
			else
			{
				throw new FileNotFoundException("Custom font file not found.");
			}
			return _customFont;
		}

		public Font GetCustomFont()
        {
			Font _customFont;
			_path = Path.Combine(_directory, @"..\..\Font\upheavtt.ttf");

			if (File.Exists(_path))
			{
				using (PrivateFontCollection privateFontCollection = new PrivateFontCollection())
				{
					privateFontCollection.AddFontFile(_path);
					_customFont = new Font(privateFontCollection.Families[0], _size, FontStyle.Regular);
					
				}
			}
			else
			{
				throw new FileNotFoundException("Custom font file not found.");
			}
			return _customFont;
        }

		public void ChangeFontSize(int fontSize)
		{
			this._size = fontSize;
		}

	}
}
