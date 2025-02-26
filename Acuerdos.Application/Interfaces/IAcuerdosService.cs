using Acuerdos.Domain.Models;
namespace Acuerdos.Application.Interfaces
{
    public interface IAcuerdosService
    {
        Task<PagedResult<AcuerdoComercial>> GetAllAsync(int page, int pageSize);
        Task<AcuerdoComercial?> GetByIdAsync(int id);
        Task<AcuerdoComercial> CreateAsync(AcuerdoComercial acuerdo);
        Task<AcuerdoComercial> UpdateAsync(int id, AcuerdoComercial acuerdo);
        Task<bool> DeleteAsync(int id);
        Task<List<TarifaConsumo>> GetTarifasAsync();
        Task<List<TarifaDetalleDto>> GetTarifasByAcuerdoAsync(int idAcuerdo);
        Task UpdateTarifasAsync(int idAcuerdo, List<TarifaAcuerdo> tarifas);
        Task<List<AgenteComercial>> GetAllAsyncAgenteComercial();
    }
}
