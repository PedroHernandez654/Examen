using ExamenPrueba2.Model.ViewModel;

namespace ExamenPrueba2.Interfaces
{
    public interface IRegisterRandomDataService
    {
        Task<(string, bool)> SendRandomDataToClients(RandomViewModel randomData);
    }
}
