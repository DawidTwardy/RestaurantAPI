using AutoMapper;
using RestaurantAPI.Entity;
using RestaurantAPI.Exceptions;

namespace RestaurantAPI.Services
{
    public interface IDishService
    {
        int Create(int restaurantId, Models.CreateDishdto dto);
    }
    public class DishService: IDishService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        public DishService(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext =dbContext ;
            _mapper = mapper;
        }
        public int Create(int restaurantId, Models.CreateDishdto dto)
        {
           var restaurant = _dbContext.Restaurants.FirstOrDefault(r => r.Id == restaurantId);
            if(restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }
            var dishEnity = _mapper.Map<Dish>(dto);
            _dbContext.Dishes.Add(dishEnity);
            _dbContext.SaveChanges();

            return dishEnity.Id;

        }
    }
}
