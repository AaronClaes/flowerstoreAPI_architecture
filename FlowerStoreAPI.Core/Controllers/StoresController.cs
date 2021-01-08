using System.Collections.Generic;
using AutoMapper;
using FlowerStoreAPI.Dtos;
using FlowerStoreAPI.Dtos.FlowerDTOS;
using FlowerStoreAPI.Dtos.StoreDTOS;
using FlowerStoreAPI.Models;
using FlowerStoreAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FlowerStoreAPI.Controllers
{   
    [Route("api/stores")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepo _repository;
        private readonly IMapper _mapper;
        // private readonly IBasisRegisterService _basisRegisterService;
        
        public StoreController(IStoreRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        //GET api/stores
        /// <summary>
        /// Gets you a list of all the stores.
        /// </summary>
        /// <returns>A list of stores</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult <IEnumerable<StoreReadDto>>> GetAllStores()
        {
            var storeItems = await _repository.GetAllStores();

            return Ok(_mapper.Map<IEnumerable<StoreReadDto>>(storeItems));
        }


        // GET api/stores/{id}
        /// <summary>
        /// Gets you a specific store.
        /// </summary>
        /// <param >The unique identifier of the stores</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult <StoreReadDto>> GetStoreById(int id)
        {
            var storeItem =await _repository.GetStoreById(id);
            if(storeItem != null)
            {
                return Ok(_mapper.Map<StoreReadDto>(storeItem));
            }
            return NotFound();

        }


        //POST api/stores
        /// <summary>
        /// Creates a new store.
        /// </summary>
        /// <param >The unique identifier of the store</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<StoreReadDto>> CreateStore(StoreCreateDto storeCreateDto){

            var storeModel = _mapper.Map<Store>(storeCreateDto);
            _repository.CreateStore(storeModel);
            _repository.SaveChanges();

            var storeReadDto =_mapper.Map<StoreReadDto>(storeModel);

            return await Task.FromResult(CreatedAtRoute(nameof(GetStoreById), new{Id = storeReadDto. Id}, storeReadDto));
        }


        //PUT api/stores/{id}
        /// <summary>
        /// Changes an existing store.
        /// </summary>
        /// <param >The unique identifier of the store</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateStore(int id, StoreUpdateDto storeUpdateDto)
        {
            var storeModelFromRepo =await _repository.GetStoreById(id);
            if(storeModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(storeUpdateDto, storeModelFromRepo);

            _repository.UpdateStore(storeModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }


        //DELETE api/stores/{id}
        /// <summary>
        /// Deletes an existing store.
        /// </summary>
        /// <param >The unique identifier of the store</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteStore(int id)
        {
            var storeModelFromRepo = await _repository.GetStoreById(id);
            if(storeModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteStore(storeModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        // //PUT api/sales
        // /// <summary>
        // /// updates a sale in a certain store
        // /// </summary>
        // [HttpPut("{sale}")]
        // [ProducesResponseType(StatusCodes.Status204NoContent)]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        // [ProducesDefaultResponseType]
        // public async Task<ActionResult> RegisterSale(int id, SaleUpdateDto saleUpdateDto){
        //     var storeModelFromRepo =await _repository.GetStoreById(id);
        //     if(storeModelFromRepo == null)
        //     {
        //         return NotFound();
        //     }

        //     _mapper.Map(saleUpdateDto, storeModelFromRepo);

        //     _repository.RegisterSale(storeModelFromRepo);

        //     _repository.SaveChanges();

        //     return NoContent();
        // }
        
    }
}

    

