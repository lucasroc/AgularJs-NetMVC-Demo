using MongoDB.Bson;

namespace VeiculosApp.Models
{
    public class Veiculo
    {        
        public ObjectId Id { get; set; }
        public string Placa { get; set; }
        public string Renavan { get; set; }
        public string NomeProprietario { get; set; }
        public string Cpf { get; set; }
        public bool Bloqueado { get; set; }
    }
}