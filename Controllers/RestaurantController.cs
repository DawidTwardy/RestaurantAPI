using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entity;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
namespace RestaurantAPI.Controllers

{
    [Route("api/restaurant")]
    [ApiController]
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
            _restaurantService.Delete(id);
            return NoContent();
            
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromRoute]int  id ,[FromBody] UpdateRestuarantDto dto)
        {
           
            _restaurantService.Update(id,dto);
             return Ok();
        

        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody]CreateRestaurantDto dto)
        {
           
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
        
            
                return Ok(restaurant);
            
        }
    }
}
