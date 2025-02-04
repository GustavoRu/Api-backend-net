using Backend.DTOs;

namespace Backend.Services
{
    public interface IBeerService{
        Task<IEnumerable<BeerDto>> Get();
        Task<BeerDto> GetById(int id);
        Task<BeerDto> Add(BeerInsertDto beerDto);
        Task<BeerDto> Update(int id, BeerUpdateDto beerDto);
        Task<BeerDto> Delete(int id);
    }
}