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
                int quantV8 = (pet.VacinasTomadas.Where(vacina => vacina.NomeVacina.Contains("V8"))).Count();
                int quantAntirrabica = (pet.VacinasTomadas.Where(vacina => vacina.NomeVacina.Contains("Antirrabica"))).Count();
                int quantGiardiase = (pet.VacinasTomadas.Where(vacina => vacina.NomeVacina.Contains("Giardiase"))).Count();
                int quantRinotraqueite = (pet.VacinasTomadas.Where(vacina => vacina.NomeVacina.Contains("Rinotraqueite"))).Count();

                var ultimaV8 = pet.VacinasTomadas.LastOrDefault(vacina => vacina.NomeVacina.Contains("V8"));
                var ultimaAntirrabica = pet.VacinasTomadas.LastOrDefault(vacina => vacina.NomeVacina.Contains("Antirrabica"));
                var ultimaGiardiase = pet.VacinasTomadas.LastOrDefault(vacina => vacina.NomeVacina.Contains("Giardiase"));
                var ultimaRinotraqueite = pet.VacinasTomadas.LastOrDefault(vacina => vacina.NomeVacina.Contains("Rinotraqueite"));

                var ultimaDataV8 = ultimaV8 != null ? (int)(dataHoje - ultimaV8.DataVacina).TotalDays : 0;
                var ultimaDataAntirrabica = ultimaAntirrabica != null ? (int)(dataHoje - ultimaAntirrabica.DataVacina).TotalDays : 0;
                var ultimaDataGiardiase = ultimaGiardiase != null ? (int)(dataHoje - ultimaGiardiase.DataVacina).TotalDays : 0;
                var ultimaDataRinotraqueite = ultimaRinotraqueite != null ? (int)(dataHoje - ultimaRinotraqueite.DataVacina).TotalDays : 0;

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

                var diasPrimeiraVacinaRabi = idadePetDias >= 120 ? 0 : 120 - idadePetDias;
                int atrasadaOuNaoRabi_2 = (365 - ultimaDataV8) <= 0 ? 0 : (365 - ultimaDataV8);

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

                var diasPrimeiraVacinaGiard = idadePetDias >= 45 ? 0 : 45 - idadePetDias;
                int atrasadaOuNaoGiard_2 = (30 - ultimaDataV8) <= 0 ? 0 : (30 - ultimaDataV8);
                int atrasadaOuNaoGiard_4 = (365 - ultimaDataV8) <= 0 ? 0 : (365 - ultimaDataV8);

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

                var diasPrimeiraVacinaRino = idadePetDias >= 45 ? 0 : 45 - idadePetDias;
                int atrasadaOuNaoRino_2 = (30 - ultimaDataV8) <= 0 ? 0 : (30 - ultimaDataV8);
                int atrasadaOuNaoRino_4 = (365 - ultimaDataV8) <= 0 ? 0 : (365 - ultimaDataV8);

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
            }
            if (pet.Raca == "Gato")
            {

            }
            return vacinasDoPet;
        }
    }
}
