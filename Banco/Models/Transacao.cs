namespace Banco.Models
{
    public class Transacao
    {
        public int Id { get; set; }

        public int IdCorrentista { get; set; }

        public DateTime Data { get; set; }

        public required string TipoTransacao { get; set; }

        public decimal Valor { get; set; }
    }
}
