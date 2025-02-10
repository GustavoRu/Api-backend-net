using Backend.DTOs;

namespace Backend.Services
{
    public interface ICommonService<T, TInsertDto, TUpdateDto>
    {
        public List<string> Errors { get; }
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Add(TInsertDto beerDto);
        Task<T> Update(int id, TUpdateDto beerDto);
        Task<T> Delete(int id);
        bool Validate(TInsertDto dto);
        bool Validate(TUpdateDto dto);
    }
}