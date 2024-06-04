using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es014.Services
{
	internal class GestionaleService(CameraService cameraService, ClienteService clienteService, PrenotazioneService prenotazioneService)
	{
		private readonly CameraService _cameraService = cameraService;
		private readonly ClienteService _clienteService = clienteService;
		private readonly PrenotazioneService _prenotazioneService = prenotazioneService;

		public void Menu()
		{
			string input;
			bool verificaNumeroNaturale;
			uint scelta;
			do
			{
				Console.WriteLine("\nMenu gestionale\n\n1. Menu prenotazione\n2. Menu camera\n3. Menu cliente\n0. Esci");
				input = Console.ReadLine() ?? "";
				verificaNumeroNaturale = uint.TryParse(input, out scelta);
				if (!verificaNumeroNaturale) scelta = uint.MaxValue;

				switch (scelta)
				{
					case 1:
						_prenotazioneService.Menu();
						break;
					case 2:
						_cameraService.Menu();
						break;
					case 3:
						_clienteService.Menu();
						break;
					case 0:
						break;
					default:
						Console.WriteLine("Scelta invalida");
						break;
				}
			}
			while (scelta != 0);
		}
	}
}
