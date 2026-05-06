using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }
        [HttpDelete]
        public ActionResult Delete([FromRoute]int restaurantId)
        {
            _dishService.DeleteAll(restaurantId);
            return NoContent();
        }
        [HttpDelete("{dishId}")]
        public ActionResult Delete([FromRoute]int restaurantId, [FromRoute] int dishId)
        {
            _dishService.Delete(restaurantId, dishId);
            return NoContent();
        }
        [HttpPost]
        public ActionResult Post([FromRoute]int restaurantId,[FromBody] Models.CreateDishdto dto)
        {
          var newdishID=  _dishService.Create(restaurantId, dto);
            return Created($"/api/restaurant/{restaurantId}/dish/{newdishID}", null);
        }
        [HttpGet("{dishId}")]
        public ActionResult<DishDto> Get([FromRoute]int restaurantId, [FromRoute] int dishId)
        {
           DishDto dish= _dishService.GetById(restaurantId, dishId);
            return Ok(dish);
        }
        [HttpGet]
        public ActionResult<List<DishDto>> Get([FromRoute] int restaurantId)
        {
            var result = _dishService.GetAll(restaurantId);
            return Ok(result);
        }
    }
}
