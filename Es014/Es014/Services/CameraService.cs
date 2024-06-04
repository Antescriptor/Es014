using Es014.Models;
using Es014.Stores;
using Es014.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es014.Services
{
	public class CameraService(CameraStore cameraStore)
	{
		private readonly CameraStore _cameraStore = cameraStore;

		public void Menu()
		{
			string input;
			bool verificaNumeroNaturale;
			uint scelta;

			do
			{
				Console.WriteLine("\nMenu camera\n\n1. Aggiungi camera\n2. Modifica camera\n3. Cancella camera\n4. Visualizza camere\n5. Cerca camera\n0. Indietro");
				input = Console.ReadLine() ?? "";
				verificaNumeroNaturale = uint.TryParse(input, out scelta);
				if (!verificaNumeroNaturale) scelta = uint.MaxValue;

				switch (scelta)
				{
					case 1:
						Aggiungi();
						break;
					case 2:
						Modifica();
						break;
					case 3:
						Cancella();
						break;
					case 4:
						Visualizza();
						break;
					case 5:
						Cerca();
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
		public void Aggiungi()
		{
			uint? numeroCamera = ImmissioneUtility.NumeroNaturale("numero camera");
			string tipoCamera = ImmissioneUtility.Stringa("tipo camera") ?? "standard";
			uint numeroLetti = ImmissioneUtility.NumeroNaturale("numero letti") ?? 1;

			Camera camera = new(numeroCamera, tipoCamera, numeroLetti);

			if (_cameraStore.Add(camera))
			{
				Console.WriteLine("Camera aggiunta");
			}
			else
			{
				Console.WriteLine("Errore nell'aggiunta della camera");
			}
		}
		public void Modifica()
		{
			uint? id = ImmissioneUtility.NumeroNaturale("id");
			if (id is null)
			{
				return;
			}

			Camera? camera = _cameraStore.Get(id.Value);

			if (camera is null)
			{
				Console.WriteLine("Camera non trovata");
				return;
			}
			else
			{
				if (_cameraStore.Update(camera))
				{
					Console.WriteLine("Camera modificata");
				}
				else
				{
					Console.WriteLine("Errore nella modifica della camera");
				}
			}
		}
		public void Cancella()
		{
			uint? id = ImmissioneUtility.NumeroNaturale("id");
			if (id is null)
			{
				return;
			}

			if (_cameraStore.Delete(id.Value))
			{
				Console.WriteLine("Camera eliminata");
			}
			else
			{
				Console.WriteLine("Camera non trovata");
			}
		}
		public void Visualizza()
		{
			List<Camera> camere = _cameraStore.Get();
			if (camere.Count < 1)
			{
				Console.WriteLine("Nessuna camera presente");
			}
			else
			{
				StampaUtility.Lista(camere);
			}
		}
		public void Cerca()
		{
			List<Camera> camere = _cameraStore.Get();

			uint? numeroCamera = ImmissioneUtility.NumeroNaturale("numero camera");
			string? tipoCamera = ImmissioneUtility.Stringa("tipo camera");
			uint? numeroLetti = ImmissioneUtility.NumeroNaturale("numero letti");

			List<Camera>? camereTrovate = camere
				.Where(c =>
					(c.Numero is not null && numeroCamera is not null) ? c.Numero == numeroCamera : true &&
					(tipoCamera is not null) ? c.Tipo == tipoCamera : true &&
					(numeroLetti is not null) ? c.NumeroLetti == numeroLetti : true)
				?.ToList();

			if (camereTrovate is not null && camereTrovate.Count > 0)
			{
				StampaUtility.Lista(camereTrovate);
			}
			else
			{
				Console.WriteLine("Nessuna camera trovata");
			}
		}
		public List<Camera> Ottieni()
		{
			return _cameraStore.Get();
		}
	}
}