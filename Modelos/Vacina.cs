using System;

namespace Back_VacinaPet
{
    public class Vacina
    {
        public string NomeVacina { get; set; }
        public DateTime DataVacina { get; set; }

        public Vacina(string nomeVacina, DateTime dataVacina)
        {
            NomeVacina = nomeVacina;
            DataVacina = dataVacina;
        }
    }
}
