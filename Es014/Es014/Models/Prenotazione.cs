using System;

namespace Es014.Models
{
	internal class Prenotazione(uint idCliente, uint idCamera, DateOnly dataArrivo, DateOnly dataPartenza, string? note = null)
	{
		public static uint Contatore { get; private set; } = 0;
		public uint Id { get; } = Contatore++;
		public uint IdCliente { get; set; } = idCliente;
		public uint IdCamera { get; set; } = idCamera;
		public DateOnly DataArrivo { get; set; } = dataArrivo;
		public DateOnly DataPartenza { get; set; } = dataPartenza;
		public string? Note { get; set; } = note;
	}
}