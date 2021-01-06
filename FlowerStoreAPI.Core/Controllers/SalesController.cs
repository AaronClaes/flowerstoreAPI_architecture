using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FlowerStoreAPI.Data;
using FlowerStoreAPI.Dtos.SaleDTOS;
using FlowerStoreAPI.Models;
using FlowerStoreAPI.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowerStoreAPI.Controllers
{
    [Route("api/sales")]
    [ApiController]

    public class SalesController : ControllerBase 
    {
        private readonly ISaleRepo _repository;
        private readonly IMapper _mapper;

        public SalesController(ISaleRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET api/sales
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<SaleReadDto>> GetAllSales()
        {
            var saleItems = _repository.GetAllSales();
            
            return Ok(_mapper.Map<IEnumerable<SaleReadDto>>(saleItems));
        }

        //GET api/sales/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<SaleReadDto>> GetSaleById(int id)
        {
            var saleItem = _repository.GetSaleById(id);
            if(saleItem != null)
            {
                return Ok(_mapper.Map<SaleReadDto>(saleItem));
            }

            return NotFound();
        }

        //POST api/sales
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<SaleReadDto> CreateSale(SaleCreateDto saleCreateDto){
            var saleModel = _mapper.Map<Sale>(saleCreateDto);
            _repository.CreateSale(saleModel);
            _repository.SaveChanges();

            var saleReadDto = _mapper.Map<SaleCreateDto>(saleModel);

            return CreatedAtRoute(nameof(GetSaleById), new{Id = saleReadDto. Id}, saleReadDto);
        }

        //PUT api/sales
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateSale(int id, SaleUpdateDto saleUpdateDto)
        {
            var saleModelFromRepo =await _repository.GetSaleById(id);
            if(saleModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(saleUpdateDto, saleModelFromRepo);

            _repository.UpdateSale(saleModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/sales/{id}
        /// <summary>
        /// Deletes an existing store.
        /// </summary>
        /// <param >The unique identifier of the store</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteSale(int id)
        {
            var saleModelFromRepo = await _repository.GetSaleById(id);
            if(saleModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteSale(saleModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }

}