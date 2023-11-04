using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace Back_VacinaPet
{
    [Table("Pet")]
    public class Pet
    {
        [ExplicitKey]
        public int IdPet { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Nome { get; set; }
        public string Raca { get; set; }
        public string Genero { get; set; }
        public List<Vacina> VacinasTomadas { get; set; }
    } 
}
