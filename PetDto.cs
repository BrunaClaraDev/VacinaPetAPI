using System;
using System.Collections.Generic;

namespace Back_VacinaPet
{
    public class PetDto
    {
        public DateTime DataNascimento { get; set; }
        public string Nome { get; set; }
        public string Raca { get; set; }
        public string Genero { get; set; }
        public List<VacinaDto> VacinasTomadas { get; set; }
    } 
}
