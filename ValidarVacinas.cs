using System;
using System.Collections.Generic;
using System.Linq;

namespace Back_VacinaPet
{
    public class ValidarVacinas
    {
        public List<VacinaDto> PegaVacinaPet(PetDto pet)
        {
            List<VacinaDto> vacinasDoPet = new List<VacinaDto>();
            DateTime dataHoje = DateTime.Now;
            int idadePetDias = (int)(dataHoje - pet.DataNascimento).TotalDays;

            if (pet.Raca == "Cachorro")
            {
                var vacinasV8 = ValidarVacinaV8(pet, dataHoje, idadePetDias);
                foreach(VacinaDto vacina in vacinasV8)
                {
                    vacinasDoPet.Add(vacina);
                }

                var vacinasAntirrabica = ValidarVacinaAntirrabica(pet, dataHoje, idadePetDias);
                foreach (VacinaDto vacina in vacinasAntirrabica)
                {
                    vacinasDoPet.Add(vacina);
                }

                var vacinasGiardiase = ValidarVacinaGiardiase(pet, dataHoje, idadePetDias);
                foreach (VacinaDto vacina in vacinasGiardiase)
                {
                    vacinasDoPet.Add(vacina);
                }

                var vacinasRinotraqueite = ValidarVacinaRinotraqueite(pet, dataHoje, idadePetDias);
                foreach (VacinaDto vacina in vacinasRinotraqueite)
                {
                    vacinasDoPet.Add(vacina);
                }

            }
            if (pet.Raca == "Gato")
            {

            }
            return vacinasDoPet;
        }

        public List<VacinaDto> ValidarVacinaV8(PetDto pet, DateTime dataHoje, int idadePetDias)
        {
            List<VacinaDto> vacinasDoPet = new List<VacinaDto>();

            int quantV8 = (pet.VacinasTomadas.Where(vacina => vacina.NomeVacina.Contains("V8"))).Count();
            var ultimaV8 = pet.VacinasTomadas.LastOrDefault(vacina => vacina.NomeVacina.Contains("V8"));
            var ultimaDataV8 = ultimaV8 != null ? (int)(dataHoje - ultimaV8.DataVacina).TotalDays : 0;

            var diasPrimeiraVacinaV8 = idadePetDias >= 45 ? 0 : 45 - idadePetDias;
            int atrasadaOuNaoV8_2 = (30 - ultimaDataV8) <= 0 ? 0 : (30 - ultimaDataV8);
            int atrasadaOuNaoV8_4 = (365 - ultimaDataV8) <= 0 ? 0 : (365 - ultimaDataV8);

            VacinaDto v8_1 = new VacinaDto("V8", dataHoje.AddDays(diasPrimeiraVacinaV8));
            VacinaDto v8_2 = new VacinaDto("V8", dataHoje.AddDays(diasPrimeiraVacinaV8 + 30));
            VacinaDto v8_3 = new VacinaDto("V8", dataHoje.AddDays(diasPrimeiraVacinaV8 + 60));
            VacinaDto v8_4 = new VacinaDto("V8", dataHoje.AddDays(diasPrimeiraVacinaV8 + 425));
            VacinaDto v8_5 = new VacinaDto("V8", dataHoje.AddDays(diasPrimeiraVacinaV8 + 790));

            switch (quantV8)
            {
                case 0:
                    vacinasDoPet.Add(v8_1);
                    vacinasDoPet.Add(v8_2);
                    vacinasDoPet.Add(v8_3);
                    vacinasDoPet.Add(v8_4);
                    vacinasDoPet.Add(v8_5);
                    break;
                case 1:
                    v8_2 = new VacinaDto("V8", dataHoje.AddDays(atrasadaOuNaoV8_2));
                    v8_3 = new VacinaDto("V8", dataHoje.AddDays(atrasadaOuNaoV8_2 + 30));
                    v8_4 = new VacinaDto("V8", dataHoje.AddDays(atrasadaOuNaoV8_2 + 395));
                    v8_5 = new VacinaDto("V8", dataHoje.AddDays(atrasadaOuNaoV8_2 + 760));
                    vacinasDoPet.Add(v8_2);
                    vacinasDoPet.Add(v8_3);
                    vacinasDoPet.Add(v8_4);
                    vacinasDoPet.Add(v8_5);
                    break;
                case 2:
                    v8_3 = new VacinaDto("V8", dataHoje.AddDays(atrasadaOuNaoV8_2));
                    v8_4 = new VacinaDto("V8", dataHoje.AddDays(atrasadaOuNaoV8_2 + 365));
                    v8_5 = new VacinaDto("V8", dataHoje.AddDays(atrasadaOuNaoV8_2 + 730));
                    vacinasDoPet.Add(v8_3);
                    vacinasDoPet.Add(v8_4);
                    vacinasDoPet.Add(v8_5);
                    break;
                default:
                    v8_4 = new VacinaDto("V8", dataHoje.AddDays(atrasadaOuNaoV8_4));
                    v8_5 = new VacinaDto("V8", dataHoje.AddDays(atrasadaOuNaoV8_4 + 365));
                    vacinasDoPet.Add(v8_4);
                    vacinasDoPet.Add(v8_5);
                    break;
            }

            return vacinasDoPet;
        }

        public List<VacinaDto> ValidarVacinaAntirrabica(PetDto pet, DateTime dataHoje, int idadePetDias)
        {
            List<VacinaDto> vacinasDoPet = new List<VacinaDto>();

            int quantAntirrabica = (pet.VacinasTomadas.Where(vacina => vacina.NomeVacina.Contains("Antirrabica"))).Count();
            var ultimaAntirrabica = pet.VacinasTomadas.LastOrDefault(vacina => vacina.NomeVacina.Contains("Antirrabica"));
            var ultimaDataAntirrabica = ultimaAntirrabica != null ? (int)(dataHoje - ultimaAntirrabica.DataVacina).TotalDays : 0;

            var diasPrimeiraVacinaRabi = idadePetDias >= 120 ? 0 : 120 - idadePetDias;
            int atrasadaOuNaoRabi_2 = (365 - ultimaDataAntirrabica) <= 0 ? 0 : (365 - ultimaDataAntirrabica);

            VacinaDto antirrabica_1 = new VacinaDto("Antirrabica", dataHoje.AddDays(diasPrimeiraVacinaRabi));
            VacinaDto antirrabica_2 = new VacinaDto("Antirrabica", dataHoje.AddDays(diasPrimeiraVacinaRabi + 365));
            VacinaDto antirrabica_3 = new VacinaDto("Antirrabica", dataHoje.AddDays(diasPrimeiraVacinaRabi + 730));

            switch (quantAntirrabica)
            {
                case 0:
                    vacinasDoPet.Add(antirrabica_1);
                    vacinasDoPet.Add(antirrabica_2);
                    vacinasDoPet.Add(antirrabica_3);
                    break;
                default:
                    antirrabica_2 = new VacinaDto("Antirrabica", dataHoje.AddDays(atrasadaOuNaoRabi_2));
                    antirrabica_3 = new VacinaDto("Antirrabica", dataHoje.AddDays(atrasadaOuNaoRabi_2 + 365));
                    vacinasDoPet.Add(antirrabica_2);
                    vacinasDoPet.Add(antirrabica_3);
                    break;
            }
            return vacinasDoPet;
        }

        public List<VacinaDto> ValidarVacinaGiardiase(PetDto pet, DateTime dataHoje, int idadePetDias)
        {
            List<VacinaDto> vacinasDoPet = new List<VacinaDto>();

            int quantGiardiase = (pet.VacinasTomadas.Where(vacina => vacina.NomeVacina.Contains("Giardiase"))).Count();
            var ultimaGiardiase = pet.VacinasTomadas.LastOrDefault(vacina => vacina.NomeVacina.Contains("Giardiase"));
            var ultimaDataGiardiase = ultimaGiardiase != null ? (int)(dataHoje - ultimaGiardiase.DataVacina).TotalDays : 0;

            var diasPrimeiraVacinaGiard = idadePetDias >= 45 ? 0 : 45 - idadePetDias;
            int atrasadaOuNaoGiard_2 = (30 - ultimaDataGiardiase) <= 0 ? 0 : (30 - ultimaDataGiardiase);
            int atrasadaOuNaoGiard_4 = (365 - ultimaDataGiardiase) <= 0 ? 0 : (365 - ultimaDataGiardiase);

            VacinaDto giardiase_1 = new VacinaDto("Giardiase", dataHoje.AddDays(diasPrimeiraVacinaGiard));
            VacinaDto giardiase_2 = new VacinaDto("Giardiase", dataHoje.AddDays(diasPrimeiraVacinaGiard + 30));
            VacinaDto giardiase_3 = new VacinaDto("Giardiase", dataHoje.AddDays(diasPrimeiraVacinaGiard + 410));
            VacinaDto giardiase_4 = new VacinaDto("Giardiase", dataHoje.AddDays(diasPrimeiraVacinaGiard + 775));

            switch (quantGiardiase)
            {
                case 0:
                    vacinasDoPet.Add(giardiase_1);
                    vacinasDoPet.Add(giardiase_2);
                    vacinasDoPet.Add(giardiase_3);
                    vacinasDoPet.Add(giardiase_4);
                    break;
                case 1:
                    giardiase_2 = new VacinaDto("Giardiase", dataHoje.AddDays(atrasadaOuNaoGiard_2));
                    giardiase_3 = new VacinaDto("Giardiase", dataHoje.AddDays(atrasadaOuNaoGiard_2 + 365));
                    giardiase_4 = new VacinaDto("Giardiase", dataHoje.AddDays(atrasadaOuNaoGiard_2 + 730));
                    vacinasDoPet.Add(giardiase_2);
                    vacinasDoPet.Add(giardiase_3);
                    vacinasDoPet.Add(giardiase_4);
                    break;
                default:
                    giardiase_3 = new VacinaDto("Giardiase", dataHoje.AddDays(atrasadaOuNaoGiard_4));
                    giardiase_4 = new VacinaDto("Giardiase", dataHoje.AddDays(atrasadaOuNaoGiard_4 + 365));
                    vacinasDoPet.Add(giardiase_3);
                    vacinasDoPet.Add(giardiase_4);
                    break;
            }
            return vacinasDoPet;
        }

        public List<VacinaDto> ValidarVacinaRinotraqueite(PetDto pet, DateTime dataHoje, int idadePetDias)
        {
            List<VacinaDto> vacinasDoPet = new List<VacinaDto>();

            int quantRinotraqueite = (pet.VacinasTomadas.Where(vacina => vacina.NomeVacina.Contains("Rinotraqueite"))).Count();
            var ultimaRinotraqueite = pet.VacinasTomadas.LastOrDefault(vacina => vacina.NomeVacina.Contains("Rinotraqueite"));
            var ultimaDataRinotraqueite = ultimaRinotraqueite != null ? (int)(dataHoje - ultimaRinotraqueite.DataVacina).TotalDays : 0;

            var diasPrimeiraVacinaRino = idadePetDias >= 45 ? 0 : 45 - idadePetDias;
            int atrasadaOuNaoRino_2 = (30 - ultimaDataRinotraqueite) <= 0 ? 0 : (30 - ultimaDataRinotraqueite);
            int atrasadaOuNaoRino_4 = (365 - ultimaDataRinotraqueite) <= 0 ? 0 : (365 - ultimaDataRinotraqueite);

            VacinaDto rinotraqueite_1 = new VacinaDto("Rinotraqueite", dataHoje.AddDays(diasPrimeiraVacinaRino));
            VacinaDto rinotraqueite_2 = new VacinaDto("Rinotraqueite", dataHoje.AddDays(diasPrimeiraVacinaRino + 30));
            VacinaDto rinotraqueite_3 = new VacinaDto("Rinotraqueite", dataHoje.AddDays(diasPrimeiraVacinaRino + 410));
            VacinaDto rinotraqueite_4 = new VacinaDto("Rinotraqueite", dataHoje.AddDays(diasPrimeiraVacinaRino + 775));

            switch (quantRinotraqueite)
            {
                case 0:
                    vacinasDoPet.Add(rinotraqueite_1);
                    vacinasDoPet.Add(rinotraqueite_2);
                    vacinasDoPet.Add(rinotraqueite_3);
                    vacinasDoPet.Add(rinotraqueite_4);
                    break;
                case 1:
                    rinotraqueite_2 = new VacinaDto("Rinotraqueite", dataHoje.AddDays(atrasadaOuNaoRino_2));
                    rinotraqueite_3 = new VacinaDto("Rinotraqueite", dataHoje.AddDays(atrasadaOuNaoRino_2 + 365));
                    rinotraqueite_4 = new VacinaDto("Rinotraqueite", dataHoje.AddDays(atrasadaOuNaoRino_2 + 730));
                    vacinasDoPet.Add(rinotraqueite_2);
                    vacinasDoPet.Add(rinotraqueite_3);
                    vacinasDoPet.Add(rinotraqueite_4);
                    break;
                default:
                    rinotraqueite_3 = new VacinaDto("Rinotraqueite", dataHoje.AddDays(atrasadaOuNaoRino_4));
                    rinotraqueite_4 = new VacinaDto("Rinotraqueite", dataHoje.AddDays(atrasadaOuNaoRino_4 + 365));
                    vacinasDoPet.Add(rinotraqueite_3);
                    vacinasDoPet.Add(rinotraqueite_4);
                    break;
            }
            return vacinasDoPet;
        }
    }
}
