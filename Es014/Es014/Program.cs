using Microsoft.Extensions.DependencyInjection;
using Es014.Stores;
using Es014.Services;

namespace Es014
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//1
			ServiceCollection serviceCollection = new();

			//2
			serviceCollection.AddScoped<CameraStore>();
			serviceCollection.AddScoped<ClienteStore>();
			serviceCollection.AddScoped<PrenotazioneStore>();

			serviceCollection.AddScoped<CameraService>();
			serviceCollection.AddScoped<ClienteService>();
			serviceCollection.AddScoped<PrenotazioneService>();
			serviceCollection.AddScoped<GestionaleService>();

			//3
			ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

			//4
			GestionaleService? gestionaleService = serviceProvider.GetService<GestionaleService>();

			//5
			gestionaleService?.Menu();
		}
	}
}