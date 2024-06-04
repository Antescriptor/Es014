using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es014.Utils
{
	internal static class ImmissioneUtility
	{
		public static uint? NumeroNaturale(string nomeVariabile = "numero naturale")
		{
			string? input;
			bool verificaNumeroNaturale;
			uint numeroNaturale;

			bool verificaIdNullo;
			do
			{
				do
				{
					verificaIdNullo = false;
					Console.WriteLine($"Immettere {nomeVariabile}:");
					input = Console.ReadLine();
				
					if (nomeVariabile.ToLower().Trim() != "id" && string.IsNullOrEmpty(input))
					{
						return null;
					}
					else if (string.IsNullOrEmpty(input))
					{
						verificaIdNullo = true;
					}
				}
				while (verificaIdNullo);

				verificaNumeroNaturale = uint.TryParse(input, out numeroNaturale);
			}
			while (!verificaNumeroNaturale);

			return numeroNaturale;
		}
		public static string? Stringa(string nomeVariabile = "stringa")
		{
			string? input;

			do
			{
				Console.WriteLine($"Immettere {nomeVariabile}:");
				input = Console.ReadLine();
				if (string.IsNullOrEmpty(input))
				{
					return null;
				}
			}
			while (input.Length < 1);

			return input;
		}
		public static DateOnly? Data(uint arrivoOPartenza)
		{
			string? input;
			bool verificaData;
			CultureInfo localizzazione = new("it-IT");
			DateOnly data;

			do
			{
				Console.WriteLine("Immettere data di " + ((arrivoOPartenza == 0) ? "arrivo" : "partenza") +  " nel formato \"gg/mm/aaaa\":");
				input = Console.ReadLine();

				if (string.IsNullOrEmpty(input))
				{
					return null;
				}

				verificaData = DateOnly.TryParse(input, localizzazione, out data);
			}
			while (!verificaData);

			return data;
		}
	}
}
