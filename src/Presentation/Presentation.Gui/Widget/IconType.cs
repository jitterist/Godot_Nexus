using System;
using System.IO;
using Gtk;

namespace App.Presentation.Gui
{
	/// Defines images that can be used within custom widgets
	public enum IconType
	{
		Projects,
		Engines,
		General,
		GoPrevious,
		Preferences,
		Reset
	}

	/// Contains all info needed to load an icon.
	public record IconInfo(IconType Type, ThemeTone ThemeTone);

	/// Extensions for icon type classes.
	public static class IconExtensions
	{
		private const string BaseIconPath = "Resources/Images/";

		public static Image CreateGtkImage(this IconInfo source)
		{
			var iconPath = source.Type.GetIconPath(source.ThemeTone);
			var buffer = File.ReadAllBytes(iconPath);
			var pixbuf = new Gdk.Pixbuf(buffer);
			return new Image {Pixbuf = pixbuf, Name = source.ToString()};
		}

		internal static string GetIconPath(this IconType source, ThemeTone themeTone)
		{
			var themeSuffix = themeTone switch
			{
				ThemeTone.Lighter => "light",
				ThemeTone.Darker => "dark",
				_ => throw new Exception($"Unknown theme {themeTone}, cannot find image suffix.")
			};

			var fileName = $"{source.ToString().ToLower()}_{themeSuffix}.svg";
			return Path.Combine(BaseIconPath, fileName);
		}
	}
}
