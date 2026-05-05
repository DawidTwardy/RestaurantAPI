using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entity;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
namespace RestaurantAPI.Controllers

{
    [Route("api/restaurant")]
    public class RestaurantController:ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public  RestaurantController(IRestaurantService restaurantService,RestaurantDbContext dbContext)
        {
            _restaurantService= restaurantService;
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]  int id)
        {
            var isDelete=_restaurantService.Delete(id);
            if (isDelete) return NoContent();
            else return NotFound();
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromRoute]int  id ,[FromBody] UpdateRestuarantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isUpdate=_restaurantService.Update(id,dto);
            if (isUpdate) return Ok();
            else return NotFound();


        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody]CreateRestaurantDto dto)
        {
            if(ModelState.IsValid==false)
            {
                return BadRequest(ModelState);
            }
           var id= _restaurantService.CreateRestaurant(dto);
            return Created($"/api/restaurant/{id}",null);
        }
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
           var restaurantsDTO= _restaurantService.GetAll();
            return Ok(restaurantsDTO);
        }
        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute]int id)
        {
            var restaurant= _restaurantService.GetById(id);
            if (restaurant is null)
            {
                return NotFound();
            }
            else
            {
                
                return Ok(restaurant);
            }
        }
    }
}
