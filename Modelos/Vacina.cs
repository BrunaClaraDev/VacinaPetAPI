using Dapper.Contrib.Extensions;
using System;

namespace Back_VacinaPet
{
    [Table("Vacina")]
    public class Vacina
    {
        [ExplicitKey]
        public int IdVacina { get; set; }
        public int IdPet { get; set; }
        public string NomeVacina { get; set; }
        public DateTime DataVacina { get; set; }
        public string SobreVacina { get; set; }

        public Vacina(string nomeVacina, DateTime dataVacina, int idPet)
        {
            NomeVacina = nomeVacina;
            DataVacina = dataVacina;
            IdPet = idPet;
        }
    }
}
