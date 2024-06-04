namespace Es014.Models
{
	public class Camera(uint? numero = null, string tipo = "standard", uint numeroLetti = 1)
	{
		public static uint Contatore { get; private set; } = 0;
		public uint Id { get; } = Contatore++;

		public uint? Numero { get; set; } = numero;
		public string Tipo { get; set; } = tipo;
		public uint NumeroLetti { get; set; } = numeroLetti;
	}
}