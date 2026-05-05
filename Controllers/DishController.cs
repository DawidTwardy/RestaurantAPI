using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }
        [HttpPost]
        public ActionResult Post([FromRoute]int restaurantId,Models.CreateDishdto dto)
        {
          var newdishID=  _dishService.Create(restaurantId, dto);
            return Created($"/api/{restaurantId}/dish/{newdishID}", null);
        }
    }
}
