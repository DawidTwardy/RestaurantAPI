using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entity;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _DbContext;
        private readonly IMapper _Mapper;
        private readonly ILogger<RestaurantService> logger;

        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper
            ,ILogger<RestaurantService> logger)
        {
            _DbContext = dbContext;
            _Mapper = mapper;
            this.logger = logger;
        }
        public bool Delete(int id)
        {
            logger.LogError($"Restaurant with id:{id} DELETE action invoked");
            var restaurant = _DbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);
            if (restaurant is null) return false;
            else
            {
                _DbContext.Restaurants.Remove(restaurant);
                _DbContext.SaveChanges();
                return true;
            }
        }
        public bool Update(int id,UpdateRestuarantDto dto)
        {
            var restaurant = _DbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);
            if (restaurant is null) return false;
            else
            {
                restaurant.Name = dto.name;
                restaurant.Description = dto.description;
                restaurant.HasDelivery = dto.hasDelivery;
                _DbContext.Restaurants.Update(restaurant);
                _DbContext.SaveChanges();
                return true;
            }
        } 

        public RestaurantDto GetById(int id)
        {
            var restaurant = _DbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == id);
            if (restaurant is null) return null;
            var result = _Mapper.Map<RestaurantDto>(restaurant);
            return result;
        }
        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = _DbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .ToList();
            var result = _Mapper.Map<List<RestaurantDto>>(restaurants);
            return result;
        }
        public int CreateRestaurant(CreateRestaurantDto dto)
        {
            var restaurant = _Mapper.Map<Restaurant>(dto);
            _DbContext.Restaurants.Add(restaurant);
            _DbContext.SaveChanges();
            return restaurant.Id;
        }
    }
}
