using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Notifications
{
    public class Notifies
    {
        public Notifies()
        {
            Notitycoes = new List<Notifies>();
        }
        [NotMapped]
        public string NomePropriedade { get; set; }
        [NotMapped]
        public string message { get; set; }
        [NotMapped]
        public List<Notifies> Notitycoes;

        public bool ValidarPropriedadeString(string valor,string nomePropriedade)
        {
            if(string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notitycoes.Add(new Notifies
                {
                    message = "Campo Obrigatório",
                    NomePropriedade = nomePropriedade
                });
                return false;
            }
            return true;
        }
        public bool ValidarPropriedadeInt(int valor,string nomePropriedade)
        {
            if(valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notitycoes.Add(new Notifies
                {
                    message = "Valor deve ser maior que 0",
                    NomePropriedade = nomePropriedade
                });
                return false;
            }
            return true;
        }
        public bool ValidarPropriedadeDecimal(decimal valor, string nomePropriedade)
        {
            if (valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notitycoes.Add(new Notifies
                {
                    message = "Valor deve ser maior que 0",
                    NomePropriedade = nomePropriedade
                });
                return false;
            }
            return true;
        }
    }
}
