using System.Collections.Generic;
using System.Linq;
using Es014.Models;
using Es014.Stores.Interfaces;

namespace Es014.Stores
{
	internal class PrenotazioneStore : IStore<Prenotazione>
	{
		private readonly List<Prenotazione> _prenotazioni = new();
		public bool Add(Prenotazione prenotazione)
		{
			_prenotazioni.Add(prenotazione);
			return true;
		}
		public bool Delete(uint id)
		{
			Prenotazione? prenotazioneDaCancellare = Get(id);
			if (prenotazioneDaCancellare is not null)
			{
				_prenotazioni.Remove(prenotazioneDaCancellare);
				return true;
			}
			else
			{
				return false;
			}
		}
		public List<Prenotazione> Get()
		{
			return _prenotazioni;
		}
		public Prenotazione? Get(uint id)
		{
			return _prenotazioni.FirstOrDefault(p => p.Id == id);
		}
		public bool Update(Prenotazione prenotazione)
		{
			Prenotazione? prenotazioneDaAggiornare = _prenotazioni.FirstOrDefault(p => p.Id == prenotazione.Id);
			if (prenotazioneDaAggiornare is not null)
			{
				prenotazioneDaAggiornare = prenotazione;
				return true;
			}
			else
			{
				return false;
			}
		}

	}
}