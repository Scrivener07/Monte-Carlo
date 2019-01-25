using System;

namespace MonteCarlo
{
	public static class RandomExtensions
	{

		public static double NextDoubleSigned(this Random random)
		{
			if (random.Next(0, 2) != 0)
			{
				return random.NextDouble() * -1.0;
			}
			else
			{
				return random.NextDouble();
			}
		}


		public static Point NextPointSigned(this Random random)
		{
			var x = random.NextDoubleSigned();
			var y = random.NextDoubleSigned();
			return new Point(x, y);
		}


	}
}
