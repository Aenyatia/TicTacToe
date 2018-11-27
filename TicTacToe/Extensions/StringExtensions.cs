namespace TicTacToe.Extensions
{
	public static class StringExtensions
	{
		/// <summary>
		/// Indicates whether a specified string is null, empty or consists only of white-space charactets.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool IsEmpty(this string value)
			=> string.IsNullOrWhiteSpace(value);
	}
}
