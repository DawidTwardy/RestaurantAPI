using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Entity;
using RestaurantAPI.Services;
namespace RestaurantAPI.Controllers

{
    [Route("/api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAcountInterface _accountService;
        public AccountController(IAcountInterface accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();

        }
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto login)
        {
            string token = _accountService.generateJwt(login);
        }
    }
}
