using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private StoreContext _context; //sera eliminado ya que se usara el servicio
        private IValidator<BeerInsertDto> _beerInsertValidator;
        private IValidator<BeerUpdateDto> _beerUpdateValidator;
        private IBeerService _beerService;

        public BeerController(StoreContext context, IValidator<BeerInsertDto> beerInsertValidator, IValidator<BeerUpdateDto> beerUpdateValidator, IBeerService beerService)
        {
            _context = context;//sera eliminado ya que se usara el servicio
            _beerInsertValidator = beerInsertValidator;
            _beerUpdateValidator = beerUpdateValidator;
            _beerService = beerService;
        }

        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get() => await _beerService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetByid(int id)
        {
            var beerDto = await _beerService.GetById(id);
            return beerDto == null ? NotFound() : Ok(beerDto);
        }

        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
        {
            var validationResult = await _beerInsertValidator.ValidateAsync(beerInsertDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var beerDto = await _beerService.Add(beerInsertDto);

            return CreatedAtAction(nameof(GetByid), new { id = beerDto.Id }, new BeerDto
            {
                Id = beerDto.Id,
                Name = beerDto.Name,
                BrandId = beerDto.BrandId,
                Alcohol = beerDto.Alcohol
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto beerUpdatetDto)
        {
            var validationResult = await _beerUpdateValidator.ValidateAsync(beerUpdatetDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var beerDto = await _beerService.Update(id, beerUpdatetDto);
            if (beerDto == null)
            {
                return NotFound();
            }

            return Ok(beerDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDto>> Delete(int id)
        {
            var beerDto = await _beerService.Delete(id);
            if (beerDto == null)
            {
                return NotFound();
            }
            return Ok(beerDto);
        }
    }
}
