using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_VacinaPet
{
    public interface IValidarVacinas
    {
        public List<Vacina> ValidarVacinaV(Pet pet, DateTime dataHoje, int idadePetDias, string tipoVacina);
        public List<Vacina> ValidarVacinaGiardiaseOuRinotraqueiteAsync(Pet pet, DateTime dataHoje, int idadePetDias, string tipoVacina);
        public List<Vacina> PegaVacinaPet(Pet pet);
        public List<Vacina> ValidarVacinaAntirrabica(Pet pet, DateTime dataHoje, int idadePetDias);

    }
}
