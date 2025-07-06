using FlagX0.Data;
using FlagX0.Data.Entities;
using System.Security.Claims;

namespace FlagX0.UseCases
{
    public interface IAddFlagUseCase
    {
        Task<bool> Execute(string flagName, bool isActive);
    }

    public class AddFlagUseCase : IAddFlagUseCase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddFlagUseCase(
            ApplicationDbContext applicationDbContext,
            IHttpContextAccessor contextAccessor)
        {
            _applicationDbContext = applicationDbContext;
            _httpContextAccessor = contextAccessor;
        }

        public async Task<bool> Execute(string flagName, bool isActive)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes
                .NameIdentifier).Value;

            FlagEntity entity = new()
            {
                Name = flagName,
                UserId = userId,
                Value = isActive
            };

            var response = await _applicationDbContext.Flags.AddAsync(entity);

            await _applicationDbContext.SaveChangesAsync();

            return true;
        }
    }
}
