namespace Es014.Models
{
	internal class Cliente(string? nome = null, string? cognome = null, string? email = null)
	{
		public static uint Contatore { get; private set; } = 0;
		public uint Id { get; } = Contatore++;
		public string? Nome { get; set; } = nome;
		public string? Cognome { get; set; } = cognome;
		public string? Email { get; set; } = email;
	}
}
