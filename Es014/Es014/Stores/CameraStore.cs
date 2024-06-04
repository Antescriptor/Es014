using Es014.Stores.Interfaces;
using Es014.Models;
using System.Collections.Generic;
using System.Linq;

namespace Es014.Stores
{
	public class CameraStore : IStore<Camera>
	{
		private readonly List<Camera> _camere = new();
		public bool Add(Camera camera)
		{
			_camere.Add(camera);
			return true;
		}
		public bool Delete(uint id)
		{
			Camera? cameraDaEliminare = Get(id);
			if (cameraDaEliminare is not null)
			{
				_camere.Remove(cameraDaEliminare);
				return true;
			}
			else
			{
				return false;
			}
		}
		public List<Camera> Get()
		{
			return _camere;
		}
		public Camera? Get(uint id)
		{
			return _camere.FirstOrDefault(c => c.Id == id);
		}
		public bool Update(Camera camera)
		{
			Camera? cameraDaAggiornare = _camere.FirstOrDefault(c => c.Id == camera.Id);
			if (cameraDaAggiornare is not null)
			{
				cameraDaAggiornare = camera;
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}