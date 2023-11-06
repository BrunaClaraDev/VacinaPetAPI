using System.Threading.Tasks;

namespace Back_VacinaPet
{
    public interface IPetRepositorio
    {
        public Task SalvarPetAsync(Pet pet);
    }
}
