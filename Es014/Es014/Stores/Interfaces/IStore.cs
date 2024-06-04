using System.Collections.Generic;

namespace Es014.Stores.Interfaces
{
	internal interface IStore<T>
	{
		public bool Add(T item);
		public bool Delete(uint id);
		public List<T> Get();
		public T? Get(uint id);
		public bool Update(T item);
	}
}