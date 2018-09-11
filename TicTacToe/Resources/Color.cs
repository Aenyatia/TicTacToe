using System.Windows.Media;

namespace TicTacToe.Resources
{
	public static class Color
	{
		public static SolidColorBrush Win =>
			new SolidColorBrush(System.Windows.Media.Color.FromRgb(0xBE, 0xDB, 0x39));

		public static SolidColorBrush Draw
			=> new SolidColorBrush(System.Windows.Media.Color.FromRgb(0x1F, 0x8A, 0x70));

		public static SolidColorBrush Circle
			=> new SolidColorBrush(System.Windows.Media.Color.FromRgb(0xFD, 0x74, 0x00));

		public static SolidColorBrush Cross
			=> new SolidColorBrush(System.Windows.Media.Color.FromRgb(0x00, 0x43, 0x58));
	}
}
