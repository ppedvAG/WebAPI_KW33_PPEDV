using UnfallPortal.Shared.Entities;

namespace UnfallPortal.UI.Services
{
    public interface IMandantService
    {
        Task<IList<Mandant>> GetAll();
        
        Task<Mandant> GetById (int id);

        Task Insert(Mandant mandant);
        Task Update(int id, Mandant mandant);
        Task Delete(int id);
    }
}
