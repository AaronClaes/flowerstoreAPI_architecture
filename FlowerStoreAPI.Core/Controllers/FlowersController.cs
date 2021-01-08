using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FlowerStoreAPI.Dtos;
using FlowerStoreAPI.Dtos.FlowerDTOS;
using FlowerStoreAPI.Models;
using FlowerStoreAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace FlowerStoreAPI.Controllers
{
    [Route("api/flowers")]
    [ApiController]
    public class FlowersController : ControllerBase
    {
        private readonly IFlowerRepo _repository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public FlowersController(IFlowerRepo repository, IMapper mapper, IMemoryCache memoryCache)
        {
            _repository = repository;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }


        // GET api/flowers/
        /// <summary>
        /// Gets you a list of all the flowers.
        /// </summary>
        /// <returns>A list of flowers</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<FlowerReadDto>>> GetAllFlowers()
        {
            var flowerItems = await _repository.GetAllFlowers();

            return Ok(_mapper.Map<IEnumerable<FlowerReadDto>>(flowerItems));
        }

        //GET api/flowers/{id}
        /// <summary>
        /// Gets you a specific flower.
        /// </summary>
        /// <param name="id">The unique identifier of the flower</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FlowerReadDto>> GetFlowerById(int id)
        {
            var flowerItem = await _repository.GetFlowerById(id);

            if (flowerItem != null)
            {
                return Ok(_mapper.Map<FlowerReadDto>(flowerItem));
            }
            return NotFound();
        }

        //POST api/flowers
        /// <summary>
        /// Creates a new flower.
        /// </summary>
        /// <param name="flowerCreateDto">The unique identifier of the shop</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<FlowerReadDto> CreateFlower(FlowerCreateDto flowerCreateDto)
        {

            var flowerModel = _mapper.Map<Flower>(flowerCreateDto);
            _repository.CreateFlower(flowerModel);
            _repository.SaveChanges();

            var flowerReadDto = _mapper.Map<FlowerReadDto>(flowerModel);

            return CreatedAtRoute(nameof(GetFlowerById), new { Id = flowerReadDto.Id }, flowerReadDto);
        }


        // PUT api/flowers/{id}
        /// <summary>
        /// Changes an existing flower.
        /// </summary>
        /// <param name="id">The unique identifier of the flower</param>
        /// <param name="flowerUpdateDto">The unique identifier of the shop</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateFlower(int id, FlowerUpdateDto flowerUpdateDto)
        {
            var flowerModelFromRepo = await _repository.GetFlowerById(id);
            if (flowerModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(flowerUpdateDto, flowerModelFromRepo);

            _repository.UpdateFlower(flowerModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }


        //DELETE api/flowers/{id}
        /// <summary>
        /// Deletes an existing flower.
        /// </summary>
        /// <param name="id">The unique identifier of the flower</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteFlower(int id)
        {
            var flowerModelFromRepo = await _repository.GetFlowerById(id);
            if (flowerModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteFlower(flowerModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}