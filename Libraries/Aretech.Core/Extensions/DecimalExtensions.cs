namespace Aretech.Core.Extensions
{
	public static class DecimalExtensions
	{
		public static decimal RoundUpToTwoDecimal(this decimal value)
		{
			return Math.Ceiling(value * 100) / 100;
		}

		public static decimal RoundUpToDecimal(this decimal value)
		{
			return Convert.ToInt32(Math.Ceiling(value));
		}
	}
}