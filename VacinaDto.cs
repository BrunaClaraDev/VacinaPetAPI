using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_VacinaPet
{
    public class VacinaDto
    {
        public string NomeVacina { get; set; }
        public DateTime DataVacina { get; set; }
        public string SobreVacina { get; set; }

        public VacinaDto(string nomeVacina, DateTime dataVacina)
        {
            NomeVacina = nomeVacina;
            DataVacina = dataVacina;
        }
    }
}
