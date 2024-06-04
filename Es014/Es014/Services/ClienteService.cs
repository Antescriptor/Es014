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
	internal class ClienteService(ClienteStore clienteStore)
	{
		private readonly ClienteStore _clienteStore = clienteStore;

		public void Menu()
		{
			string input;
			bool verificaNumeroNaturale;
			uint scelta;

			do
			{
				Console.WriteLine("\nMenu clienti\n\n1. Aggiungi cliente\n2. Modifica cliente\n3. Cancella cliente\n4. Visualizza clienti\n5. Cerca cliente\n0. Indietro");
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
			string? nome = ImmissioneUtility.Stringa("nome");
			string? cognome = ImmissioneUtility.Stringa("cognome");
			string? email = ImmissioneUtility.Stringa("email");

			Cliente cliente = new Cliente(nome, cognome, email);

			if (_clienteStore.Add(cliente))
			{
				Console.WriteLine("Cliente aggiunto");
			}
			else
			{
				Console.WriteLine("Errore nell'aggiunta del cliente");
			}
		}
		public void Modifica()
		{
			uint? id = ImmissioneUtility.NumeroNaturale("id");
			if (id is null)
			{
				return;
			}

			Cliente? cliente = _clienteStore.Get(id.Value);

			if (cliente is null)
			{
				Console.WriteLine("Cliente non trovato");
				return;
			}
			else
			{
				if (_clienteStore.Update(cliente))
				{
					Console.WriteLine("Cliente modificato");
				}
				else
				{
					Console.WriteLine("Errore nella modifica del cliente");
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

			if (_clienteStore.Delete(id.Value))
			{
				Console.WriteLine("Cliente eliminato");
			}
			else
			{
				Console.WriteLine("Cliente non trovato");
			}
		}
		public void Visualizza()
		{
			List<Cliente> clienti = _clienteStore.Get();
			if (clienti.Count < 1)
			{
				Console.WriteLine("Nessun cliente presente");
			}
			else
			{
				StampaUtility.Lista(clienti);
			}
		}
		public void Cerca()
		{
			List<Cliente> clienti = _clienteStore.Get();

			string? nome = ImmissioneUtility.Stringa("nome");
			string? cognome = ImmissioneUtility.Stringa("cognome");
			string? email = ImmissioneUtility.Stringa("email");

			List<Cliente>? clientiTrovati = clienti
				.Where(c =>
					(c.Nome is not null && nome is not null) ? c.Nome == nome : true &&
					(c.Cognome is not null && cognome is not null) ? c.Cognome == cognome : true &&
					(c.Email is not null && email is not null) ? c.Email == email : true)
				?.ToList();

			if (clientiTrovati is not null && clientiTrovati.Count > 0)
			{
				StampaUtility.Lista(clientiTrovati);
			}
			else
			{
				Console.WriteLine("Nessun cliente trovato");
			}
		}
		public List<Cliente> Ottieni()
		{
			return _clienteStore.Get();
		}
	}
}