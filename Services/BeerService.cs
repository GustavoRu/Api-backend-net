using Backend.DTOs;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class BeerService : IBeerService
    {
        private StoreContext _context;

        public BeerService(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BeerDto>> Get() => await _context.Beers.Select(b => new BeerDto
        {
            Id = b.BeerId,
            Name = b.Name,
            BrandId = b.BrandId,
            Alcohol = b.Alcohol
        }).ToListAsync();

        public async Task<BeerDto> GetById(int id)
        {
            var beer = await _context.Beers.FindAsync(id);
            if (beer != null)
            {
                return new BeerDto
                {
                    Id = beer.BeerId,
                    Name = beer.Name,
                    BrandId = beer.BrandId,
                    Alcohol = beer.Alcohol
                };
            }
            return null;
        }

        public async Task<BeerDto> Add(BeerInsertDto beerDto)
        {
            var beer = new Beer
            {
                Name = beerDto.Name,
                BrandId = beerDto.BrandId,
                Alcohol = beerDto.Alcohol
            };
            await _context.Beers.AddAsync(beer);
            await _context.SaveChangesAsync();

            return new BeerDto
            {
                Id = beer.BeerId,
                Name = beer.Name,
                BrandId = beer.BrandId,
                Alcohol = beer.Alcohol
            };
        }

        public async Task<BeerDto> Update(int id, BeerUpdateDto beerDto)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer != null)
            {
                beer.Name = beerDto.Name;
                beer.BrandId = beerDto.BrandId;
                beer.Alcohol = beerDto.Alcohol;
                await _context.SaveChangesAsync();

                return new BeerDto
                {
                    Id = beer.BeerId,
                    Name = beer.Name,
                    BrandId = beer.BrandId,
                    Alcohol = beer.Alcohol
                };
            }
            return null;


        }

        public async Task<BeerDto> Delete(int id)
        {
            var beer = await _context.Beers.FindAsync(id);
            if (beer != null)
            {
                _context.Beers.Remove(beer);
                await _context.SaveChangesAsync();

                var beerDto = new BeerDto
                {
                    Id = beer.BeerId,
                    Name = beer.Name,
                    BrandId = beer.BrandId,
                    Alcohol = beer.Alcohol
                };
                return beerDto;
            }

            return null;

        }

    }
}