using System;

namespace MonteCarlo
{
	/// <summary>
	/// https://en.wikipedia.org/wiki/Monte_Carlo_method
	/// </summary>
	internal class Program
	{
		// Program
		private static bool Continue = true;
		private const string Division = "==========================================================================================";

		// Options
		private static bool UseVerbose = true;

		// Simulation
		private static Random Randomizer;
		private const int Seed = 99;
		private static uint Total = 0;

		// Results
		private static float Hit = 0;
		private static float Iteration = 0;

		static void Main(string[] args)
		{
			Console.WriteLine("Monte Carlo Simulation");
			Console.WriteLine("The Monte Carlo methods are a broad class of computational algorithms that");
			Console.WriteLine("rely on repeated random sampling to obtain numerical results which are deterministic in nature.\n");
			Console.WriteLine("This program will demonstrate the Monte Carlo method by calculating the value of PI.\n");
			Console.WriteLine(Division + "\n");

			if (ConfigureUseSeed())
				Randomizer = new Random(Seed);
			else
				Randomizer = new Random();

			while (Continue)
			{
				Total = 0;
				Hit = 0;
				Iteration = 0;
				UseVerbose = ConfigureUseVerbose();
				Total = ConfigureTotal();
				Continue = Run();
			}
		}


		private static bool Run()
		{
			for (int index = 0; index < Total; index++)
			{
				Iteration += 1;
				Point point = Randomizer.NextPointSigned();
				var value = Math.Pow(point.X, 2) + Math.Pow(point.Y, 2);
				if (value <= 1)
				{
					Hit += 1;
					if (UseVerbose)
					{
						Console.WriteLine(string.Format("PI({0}) Hit:{1} Iteration:{2} Total:{3} Point:{4}", GetResult(Hit, Iteration), Hit, Iteration, Total, point.ToString()));
					}
				}
			}
			Console.WriteLine(string.Format("Calculated PI:{0}", GetResult(Hit, Iteration)));
			return OptionContinue();
		}


		private static float GetResult(float value, float total)
		{
			return (value / total) * 4;
		}


		#region Options

		private static bool ConfigureUseSeed()
		{
			Console.WriteLine("\nPress 'Y' to use a fixed random seed of " + Seed + ". Press 'N' to continue with a random seed.\n");
			var input = Console.ReadKey();
			Console.WriteLine("\n");

			if (input.Key == ConsoleKey.Y)
			{
				return true;
			}
			else if (input.Key == ConsoleKey.N)
			{
				return false;
			}
			else
			{
				return ConfigureUseSeed();
			}
		}


		private static bool ConfigureUseVerbose()
		{
			Console.WriteLine("\nPress 'Y' to enable verbose mode. Press 'N' to continue without verbose mode.\n");
			var input = Console.ReadKey();
			Console.WriteLine("\n");

			if (input.Key == ConsoleKey.Y)
			{
				return true;
			}
			else if (input.Key == ConsoleKey.N)
			{
				return false;
			}
			else
			{
				return ConfigureUseVerbose();
			}
		}


		private static uint ConfigureTotal()
		{
			Console.WriteLine("\nEnter the amount of points to use in this simulation.\n");
			string line = Console.ReadLine();
			Console.WriteLine("\n");

			if (uint.TryParse(line, out uint value))
			{
				if (value != 0)
				{
					Console.WriteLine(string.Format("You choose to use {0} points.", value));
					return value;
				}
				else
				{
					Console.WriteLine(string.Format("The amount cannot be zero. Try again.", line));
					return ConfigureTotal();
				}
			}
			else
			{
				Console.WriteLine(string.Format("The amount '{0}' was not a positive whole number. Try again.", line));
				return ConfigureTotal();
			}
		}


		private static bool OptionContinue()
		{
			Console.WriteLine("\nPress 'Y' to restart the simulation or 'N' to exit.\n");
			var input = Console.ReadKey();
			Console.WriteLine("\n");

			if (input.Key == ConsoleKey.Y)
			{
				return true;
			}
			else if (input.Key == ConsoleKey.N)
			{
				return false;
			}
			else
			{
				return OptionContinue();
			}
		}

		#endregion

	}
}
