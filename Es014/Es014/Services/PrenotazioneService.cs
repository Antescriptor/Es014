using Es014.Models;
using Es014.Stores;
using Es014.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Es014.Services
{
	internal class PrenotazioneService(PrenotazioneStore prenotazioneStore, CameraService cameraService, ClienteService clienteService)
	{
		private readonly PrenotazioneStore _prenotazioneStore = prenotazioneStore;
		private readonly CameraService _cameraService = cameraService;
		private readonly ClienteService _clienteService = clienteService;

		public void Menu()
		{
			string input;
			bool verificaNumeroNaturale;
			uint scelta;

			do
			{
				Console.WriteLine("\nMenu prenotazione\n\n1. Aggiungi prenotazione\n2. Modifica prenotazione\n3. Cancella prenotazione\n4. Visualizza prenotazioni\n5. Cerca prenotazione\n0. Indietro");
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
			while (scelta == 0);
		}
		public void Aggiungi()
		{
			List<Camera> camere = _cameraService.Ottieni();

			if (camere.Count < 1)
			{
				Console.WriteLine("Nessuna camera inserita nella banca dati");
				return;
			}

			List<Cliente> clienti = _clienteService.Ottieni();
			if (clienti.Count < 1)
			{
				Console.WriteLine("Nessun cliente inserito nella banca dati");
				return;
			}

			List<Prenotazione> prenotazioni = _prenotazioneStore.Get();

			DateOnly? dataArrivo = ImmissioneUtility.Data(0);
			if (dataArrivo is null) return;
			DateOnly? dataPartenza = ImmissioneUtility.Data(1);
			if (dataPartenza is null) return;

			if (dataArrivo >= dataPartenza)
			{
				Console.WriteLine("La data di partenza non può essere minore o uguale a quella di arrivo");
				return;
			}

			string tipoCamera = ImmissioneUtility.Stringa("tipo camera") ?? "standard";

			uint numeroLettiRichiesti = ImmissioneUtility.NumeroNaturale("numero letti richiesti") ?? 1;

			List<uint> idClienti =
				(from cliente in clienti
				select cliente.Id).ToList();

			var camereSenzaPrenotazioni =
				from camera in camere
				where !prenotazioni.Any(p => p.IdCamera == camera.Id)
				select camera;

			var camereConPrenotrazioniEFiltrate = 
				from camera in camere
				join prenotazione in prenotazioni
				on camera.Id equals prenotazione.IdCamera
				where
					camera.Tipo.ToLower().Trim() == tipoCamera.ToLower().Trim() &&
					camera.NumeroLetti >= numeroLettiRichiesti &&
					(prenotazione.DataPartenza <= dataArrivo || prenotazione.DataArrivo >= dataPartenza)

				select camera;

			var camereDisponibili = camereSenzaPrenotazioni.Union(camereConPrenotrazioniEFiltrate);

			List<uint>? idCamereFiltrate = camereDisponibili.Select(p => p.Id).ToList();

			if (idCamereFiltrate.Count < 1)
			{
				Console.WriteLine("Nessuna camera disponibile");
				return;
			}

			Console.WriteLine("\nCampere disponibili:");
			StampaUtility.Lista(camereDisponibili.ToList());

			uint? idCliente = ImmissioneUtility.NumeroNaturale("id cliente");
			if (idCliente is null) return;
			uint? idCamera = ImmissioneUtility.NumeroNaturale("id camera");
			if (idCamera is null) return;

			bool idClienteTrovato = false;

			foreach (uint id in idClienti)
			{
				if (idCliente == id)
				{
					idClienteTrovato = true;
					break;
				}
			}

			if (!idClienteTrovato)
			{
				Console.WriteLine("Cliente non trovato");
				return;
			}

			bool idCameraTrovato = false;

			foreach (uint id in idCamereFiltrate)
			{
				if (idCamera == id)
				{
					idCameraTrovato = true;
					break;
				}
			}

			if (!idCameraTrovato)
			{
				Console.WriteLine("Camera non disponibile");
				return;
			}


			Prenotazione prenotazioneDaAggiungere = new(idCliente.Value, idCamera.Value, dataArrivo.Value, dataPartenza.Value);

			if (_prenotazioneStore.Add(prenotazioneDaAggiungere))
			{
				Console.WriteLine("Prenotazione aggiunta");
			}
			else
			{
				Console.WriteLine("Errore nell'aggiunta della prenotazione");
			}
		}
		public void Modifica()
		{
			uint? id = ImmissioneUtility.NumeroNaturale("id");
			if (id is null)
			{
				return;
			}

			Prenotazione? prenotazione = _prenotazioneStore.Get(id.Value);

			if (prenotazione is null)
			{
				Console.WriteLine("Prenotazione non trovata");
				return;
			}
			else
			{
				if (_prenotazioneStore.Update(prenotazione))
				{
					Console.WriteLine("Prenotazione modificata");
				}
				else
				{
					Console.WriteLine("Errore nella modifica della prenotazione");
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

			if (_prenotazioneStore.Delete(id.Value))
			{
				Console.WriteLine("Prenotazione eliminata");
			}
			else
			{
				Console.WriteLine("Prenotazione non trovata");
			}
		}
		public void Visualizza()
		{
			List<Prenotazione> prenotazioni = _prenotazioneStore.Get();
			if (prenotazioni.Count < 1)
			{
				Console.WriteLine("Nessuna prenotazione presente");
			}
			else
			{
				StampaUtility.Lista(prenotazioni);
			}
		}
		public void Cerca()
		{
			List<Prenotazione> prenotazioni = _prenotazioneStore.Get();

			uint? idCliente = ImmissioneUtility.NumeroNaturale("id cliente");
			uint? idCamera = ImmissioneUtility.NumeroNaturale("id camera");
			DateOnly? dataArrivo = ImmissioneUtility.Data(0);
			DateOnly? dataPartenza = ImmissioneUtility.Data(1);
			string? note = ImmissioneUtility.Stringa("note");

			List<Prenotazione>? prenotazioniTrovate = prenotazioni
				.Where(p =>
					(idCliente is not null) ? p.IdCliente == idCliente : true &&
					(idCamera is not null) ? p.IdCamera == idCamera : true &&
					(dataArrivo is not null) ? p.DataArrivo == dataArrivo : true &&
					(dataPartenza is not null) ? p.DataPartenza == dataPartenza : true &&
					(p.Note is not null && note is not null) ? p.Note.ToLower().Contains(note.ToLower().Trim()) : true)
				?.ToList();

			if (prenotazioniTrovate is not null && prenotazioniTrovate.Count > 0)
			{
				StampaUtility.Lista(prenotazioniTrovate);
			}
			else
			{
				Console.WriteLine("Nessuna prenotazione trovata");
			}
		}
	}
}
