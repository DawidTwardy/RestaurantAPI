using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entity;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IDishService
    {
        int Create(int restaurantId, Models.CreateDishdto dto);
        DishDto GetById(int restaurantId, int dishId);
        List<DishDto> GetAll(int restaurantId);
        void DeleteAll(int restaurantId);
        void Delete(int restaurantId, int dishId);
    }

    public class DishService : IDishService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;

        public DishService(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int Create(int restaurantId, Models.CreateDishdto dto)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var dishEnity = _mapper.Map<Dish>(dto);
            dishEnity.RestaurantId = restaurantId;
            _dbContext.Dishes.Add(dishEnity);
            _dbContext.SaveChanges();

            return dishEnity.Id;
        }

        public DishDto GetById(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var dish = _dbContext.Dishes.FirstOrDefault(d => d.Id == dishId && d.RestaurantId == restaurantId);

            if (dish is null)
            {
                throw new NotFoundException("Dish not found");
            }

            return _mapper.Map<DishDto>(dish);
        }

        public List<DishDto> GetAll(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var dishes = _dbContext.Dishes.Where(d => d.RestaurantId == restaurantId).ToList();

            return _mapper.Map<List<DishDto>>(dishes);
        }

        public void DeleteAll(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var dishes = _dbContext.Dishes.Where(d => d.RestaurantId == restaurantId);

            _dbContext.Dishes.RemoveRange(dishes);
            _dbContext.SaveChanges();
        }

        public void Delete(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var dish = _dbContext.Dishes.FirstOrDefault(d => d.Id == dishId && d.RestaurantId == restaurantId);

            if (dish is null)
            {
                throw new NotFoundException("Dish not found");
            }

            _dbContext.Dishes.Remove(dish);
            _dbContext.SaveChanges();
        }

        private Restaurant GetRestaurantById(int restaurantId)
        {
            var restaurant = _dbContext.Restaurants.FirstOrDefault(r => r.Id == restaurantId);
            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }
            return restaurant;
        }
    }
}