// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

namespace Bodix.Evolunity.Extensions
{
	public static class CharExtensions
	{
		public static bool IsDigit(this char @char)
		{
			return char.IsDigit(@char);
		}

		public static bool IsLetter(this char @char)
		{
			return char.IsLetter(@char);
		}

		public static bool IsLetterOrDigit(this char @char)
		{
			return char.IsLetterOrDigit(@char);
		}

		public static bool IsSymbol(this char @char)
		{
			return char.IsSymbol(@char);
		}

		public static bool IsWhiteSpace(this char @char)
		{
			return char.IsWhiteSpace(@char);
		}

		public static bool IsLower(this char @char)
		{
			return char.IsLower(@char);
		}

		public static bool IsUpper(this char @char)
		{
			return char.IsUpper(@char);
		}

		public static char ToLower(this char @char)
		{
			return char.ToLower(@char);
		}

		public static char ToUpper(this char @char)
		{
			return char.ToUpper(@char);
		}
	}
}