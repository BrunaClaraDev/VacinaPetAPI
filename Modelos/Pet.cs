using System;
using System.Collections.Generic;

namespace Back_VacinaPet
{
    public class Pet
    {
        public DateTime DataNascimento { get; set; }
        public string Nome { get; set; }
        public string Raca { get; set; }
        public string Genero { get; set; }
        public List<Vacina> VacinasTomadas { get; set; }
    } 
}
