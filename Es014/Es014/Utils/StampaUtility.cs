using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Es014.Utils
{
	internal class StampaUtility
	{
		public static void Lista<T>(List<T> lista)
		{
			foreach (T oggetto in lista)
			{
				if (oggetto is not null)
				{
					foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(oggetto))
					{
						string name = descriptor.Name;
						object? value = descriptor.GetValue(oggetto);
						Console.WriteLine($"{name}={value}");
					}
				}
			}
		}
		public static void Oggetto<T>(T oggetto)
		{
			if (oggetto is not null)
			{
				foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(oggetto))
				{
					string name = descriptor.Name;
					object? value = descriptor.GetValue(oggetto);
					Console.WriteLine($"{name}={value}");
				}
			}
		}
	}
}
