using System.Collections.Generic;

namespace ProAgil.Domain
{
    public class Evento
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public string DateTime { get; set; }
        public string Tema { get; set; }
        public int QtdPessoas { get; set; }
        public string ImageURL { get; set; }
        public string telefone { get; set; }
        public string email { get; set; }
        public List<Lote> Lotes { get; set; }
        public List<RedeSocial> RedesSociais { get; set; }
        public List<PalestranteEvento> PalestrantesEvento { get; set; }
        
    }
}