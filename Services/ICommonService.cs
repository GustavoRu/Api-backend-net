using Backend.DTOs;

namespace Backend.Services
{
    public interface ICommonService<T, TInsertDto, TUpdateDto>{
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Add(TInsertDto beerDto);
        Task<T> Update(int id, TUpdateDto beerDto);
        Task<T> Delete(int id);
    }
}