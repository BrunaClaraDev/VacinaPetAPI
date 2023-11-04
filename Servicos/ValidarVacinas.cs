using System;
using System.Collections.Generic;
using System.Linq;

namespace Back_VacinaPet
{
    public class ValidarVacinas : IValidarVacinas
    {
        public List<Vacina> PegaVacinaPet(Pet pet)
        {
            List<Vacina> vacinasDoPet = new List<Vacina>();
            DateTime dataHoje = DateTime.Now;
            int idadePetDias = (int)(dataHoje - pet.DataNascimento).TotalDays;

            if (pet.Raca == "Cachorro")
            {
                var vacinasV8 = ValidarVacinaV(pet, dataHoje, idadePetDias, "V8");
                foreach (Vacina vacina in vacinasV8)
                {
                    vacinasDoPet.Add(vacina);
                }

                var vacinasV10 = ValidarVacinaV(pet, dataHoje, idadePetDias, "V10");
                foreach (Vacina vacina in vacinasV10)
                {
                    vacinasDoPet.Add(vacina);
                }

                var vacinasAntirrabica = ValidarVacinaAntirrabica(pet, dataHoje, idadePetDias);
                foreach (Vacina vacina in vacinasAntirrabica)
                {
                    vacinasDoPet.Add(vacina);
                }

                var vacinasGiardiase = ValidarVacinaGiardiaseOuRinotraqueiteAsync(pet, dataHoje, idadePetDias, "Giardiase");
                foreach (Vacina vacina in vacinasGiardiase)
                {
                    vacinasDoPet.Add(vacina);
                }

                var vacinasRinotraqueite = ValidarVacinaGiardiaseOuRinotraqueiteAsync(pet, dataHoje, idadePetDias, "Rinotraqueite");
                foreach (Vacina vacina in vacinasRinotraqueite)
                {
                    vacinasDoPet.Add(vacina);
                }

            }
            else if (pet.Raca == "Gato")
            {
                var vacinasV3 = ValidarVacinaV(pet, dataHoje, idadePetDias, "V3");
                foreach (Vacina vacina in vacinasV3)
                {
                    vacinasDoPet.Add(vacina);
                }

                var vacinasV4 = ValidarVacinaV(pet, dataHoje, idadePetDias, "V4");
                foreach (Vacina vacina in vacinasV4)
                {
                    vacinasDoPet.Add(vacina);
                }

                var vacinasV5 = ValidarVacinaV(pet, dataHoje, idadePetDias, "V5");
                foreach (Vacina vacina in vacinasV5)
                {
                    vacinasDoPet.Add(vacina);
                }

                var vacinasAntirrabica = ValidarVacinaAntirrabica(pet, dataHoje, idadePetDias);
                foreach (Vacina vacina in vacinasAntirrabica)
                {
                    vacinasDoPet.Add(vacina);
                }
            }
            else
            {
                throw new Exception("ERRO! RAÇA DO PET NÃO ENCONTRADA");
            }

            return vacinasDoPet;
        }

        public List<Vacina> ValidarVacinaAntirrabica(Pet pet, DateTime dataHoje, int idadePetDias)
        {
            List<Vacina> vacinasDoPet = new List<Vacina>();

            int quantAntirrabica = (pet.VacinasTomadas.Where(vacina => vacina.NomeVacina.Contains("Antirrabica"))).Count();
            var ultimaAntirrabica = pet.VacinasTomadas.LastOrDefault(vacina => vacina.NomeVacina.Contains("Antirrabica"));
            var ultimaDataAntirrabica = ultimaAntirrabica != null ? (int)(dataHoje - ultimaAntirrabica.DataVacina).TotalDays : 0;

            var diasPrimeiraVacinaRabi = idadePetDias >= 120 ? 0 : 120 - idadePetDias;
            int atrasadaOuNaoRabi_2 = (365 - ultimaDataAntirrabica) <= 0 ? 0 : (365 - ultimaDataAntirrabica);

            Vacina antirrabica_1 = new Vacina("Antirrabica", dataHoje.AddDays(diasPrimeiraVacinaRabi));
            Vacina antirrabica_2 = new Vacina("Antirrabica", dataHoje.AddDays(diasPrimeiraVacinaRabi + 365));
            Vacina antirrabica_3 = new Vacina("Antirrabica", dataHoje.AddDays(diasPrimeiraVacinaRabi + 730));

            switch (quantAntirrabica)
            {
                case 0:
                    vacinasDoPet.Add(antirrabica_1);
                    vacinasDoPet.Add(antirrabica_2);
                    vacinasDoPet.Add(antirrabica_3);
                    break;
                default:
                    antirrabica_2 = new Vacina("Antirrabica", dataHoje.AddDays(atrasadaOuNaoRabi_2));
                    antirrabica_3 = new Vacina("Antirrabica", dataHoje.AddDays(atrasadaOuNaoRabi_2 + 365));
                    vacinasDoPet.Add(antirrabica_2);
                    vacinasDoPet.Add(antirrabica_3);
                    break;
            }
            return vacinasDoPet;
        }

        public List<Vacina> ValidarVacinaGiardiaseOuRinotraqueiteAsync(Pet pet, DateTime dataHoje, int idadePetDias, string tipoVacina)
        {
            List<Vacina> vacinasDoPet = new List<Vacina>();

            int quantVacina = (pet.VacinasTomadas.Where(vacina => vacina.NomeVacina.Contains(tipoVacina))).Count();
            var ultimaVacina = pet.VacinasTomadas.LastOrDefault(vacina => vacina.NomeVacina.Contains(tipoVacina));
            var ultimaDataVacina = ultimaVacina != null ? (int)(dataHoje - ultimaVacina.DataVacina).TotalDays : 0;

            var diasPrimeiraVacina = idadePetDias >= 45 ? 0 : 45 - idadePetDias;
            int atrasadaOuNaoVacina_2 = (30 - ultimaDataVacina) <= 0 ? 0 : (30 - ultimaDataVacina);
            int atrasadaOuNaoVacina_4 = (365 - ultimaDataVacina) <= 0 ? 0 : (365 - ultimaDataVacina);

            Vacina vacina_1 = new Vacina(tipoVacina, dataHoje.AddDays(diasPrimeiraVacina));
            Vacina vacina_2 = new Vacina(tipoVacina, dataHoje.AddDays(diasPrimeiraVacina + 30));
            Vacina vacina_3 = new Vacina(tipoVacina, dataHoje.AddDays(diasPrimeiraVacina + 410));
            Vacina vacina_4 = new Vacina(tipoVacina, dataHoje.AddDays(diasPrimeiraVacina + 775));

            switch (quantVacina)
            {
                case 0:
                    vacinasDoPet.Add(vacina_1);
                    vacinasDoPet.Add(vacina_2);
                    vacinasDoPet.Add(vacina_3);
                    vacinasDoPet.Add(vacina_4);
                    break;
                case 1:
                    vacina_2 = new Vacina(tipoVacina, dataHoje.AddDays(atrasadaOuNaoVacina_2));
                    vacina_3 = new Vacina(tipoVacina, dataHoje.AddDays(atrasadaOuNaoVacina_2 + 365));
                    vacina_4 = new Vacina(tipoVacina, dataHoje.AddDays(atrasadaOuNaoVacina_2 + 730));
                    vacinasDoPet.Add(vacina_2);
                    vacinasDoPet.Add(vacina_3);
                    vacinasDoPet.Add(vacina_4);
                    break;
                default:
                    vacina_3 = new Vacina(tipoVacina, dataHoje.AddDays(atrasadaOuNaoVacina_4));
                    vacina_4 = new Vacina(tipoVacina, dataHoje.AddDays(atrasadaOuNaoVacina_4 + 365));
                    vacinasDoPet.Add(vacina_3);
                    vacinasDoPet.Add(vacina_4);
                    break;
            }
            return vacinasDoPet;
        }

        public List<Vacina> ValidarVacinaV(Pet pet, DateTime dataHoje, int idadePetDias, string tipoVacina)
        {
            List<Vacina> vacinasDoPet = new List<Vacina>();

            int quantV = (pet.VacinasTomadas.Where(vacina => vacina.NomeVacina.Contains(tipoVacina))).Count();
            var ultimaV = pet.VacinasTomadas.LastOrDefault(vacina => vacina.NomeVacina.Contains(tipoVacina));
            var ultimaDataV = ultimaV != null ? (int)(dataHoje - ultimaV.DataVacina).TotalDays : 0;

            var diasPrimeiraVacinaV = idadePetDias >= 45 ? 0 : 45 - idadePetDias;
            int atrasadaOuNaoV_2 = (30 - ultimaDataV) <= 0 ? 0 : (30 - ultimaDataV);
            int atrasadaOuNaoV_4 = (365 - ultimaDataV) <= 0 ? 0 : (365 - ultimaDataV);

            Vacina v_1 = new Vacina(tipoVacina, dataHoje.AddDays(diasPrimeiraVacinaV));
            Vacina v_2 = new Vacina(tipoVacina, dataHoje.AddDays(diasPrimeiraVacinaV + 30));
            Vacina v_3 = new Vacina(tipoVacina, dataHoje.AddDays(diasPrimeiraVacinaV + 60));
            Vacina v_4 = new Vacina(tipoVacina, dataHoje.AddDays(diasPrimeiraVacinaV + 425));
            Vacina v_5 = new Vacina(tipoVacina, dataHoje.AddDays(diasPrimeiraVacinaV + 790));

            switch (quantV)
            {
                case 0:
                    vacinasDoPet.Add(v_1);
                    vacinasDoPet.Add(v_2);
                    vacinasDoPet.Add(v_3);
                    vacinasDoPet.Add(v_4);
                    vacinasDoPet.Add(v_5);
                    break;
                case 1:
                    v_2 = new Vacina(tipoVacina, dataHoje.AddDays(atrasadaOuNaoV_2));
                    v_3 = new Vacina(tipoVacina, dataHoje.AddDays(atrasadaOuNaoV_2 + 30));
                    v_4 = new Vacina(tipoVacina, dataHoje.AddDays(atrasadaOuNaoV_2 + 395));
                    v_5 = new Vacina(tipoVacina, dataHoje.AddDays(atrasadaOuNaoV_2 + 760));
                    vacinasDoPet.Add(v_2);
                    vacinasDoPet.Add(v_3);
                    vacinasDoPet.Add(v_4);
                    vacinasDoPet.Add(v_5);
                    break;
                case 2:
                    v_3 = new Vacina(tipoVacina, dataHoje.AddDays(atrasadaOuNaoV_2));
                    v_4 = new Vacina(tipoVacina, dataHoje.AddDays(atrasadaOuNaoV_2 + 365));
                    v_5 = new Vacina(tipoVacina, dataHoje.AddDays(atrasadaOuNaoV_2 + 730));
                    vacinasDoPet.Add(v_3);
                    vacinasDoPet.Add(v_4);
                    vacinasDoPet.Add(v_5);
                    break;
                default:
                    v_4 = new Vacina(tipoVacina, dataHoje.AddDays(atrasadaOuNaoV_4));
                    v_5 = new Vacina(tipoVacina, dataHoje.AddDays(atrasadaOuNaoV_4 + 365));
                    vacinasDoPet.Add(v_4);
                    vacinasDoPet.Add(v_5);
                    break;
            }

            return vacinasDoPet;
        }
    }
}
