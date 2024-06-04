using System.Collections.Generic;
using Es014.Stores.Interfaces;
using Es014.Models;
using System.Linq;

namespace Es014.Stores
{
	internal class ClienteStore : IStore<Cliente>
	{
		private readonly List<Cliente> _clienti = new();
		public bool Add(Cliente cliente)
		{
			_clienti.Add(cliente);
			return true;
		}
		public bool Delete(uint id)
		{
			Cliente? clienteDaCancellare = Get(id);
			if (clienteDaCancellare is not null)
			{
				_clienti.Remove(clienteDaCancellare);
				return true;
			}
			else
			{
				return false;
			}
		}
		public List<Cliente> Get()
		{
			return _clienti;
		}
		public Cliente? Get(uint id)
		{
			return _clienti.FirstOrDefault(c => c.Id == id);
		}
		public bool Update(Cliente cliente)
		{
			Cliente? clienteDaAggiornare = _clienti.FirstOrDefault(c => c.Id == cliente.Id);
			if (clienteDaAggiornare is not null)
			{
				clienteDaAggiornare = cliente;
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
