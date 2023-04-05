using Casino.Api.Domain.Entities;

namespace Casino.Api.Infrastructure.Services.Interfaces
{
    public interface IResultService
    {
        Task<List<Result>> GetResults();
    }
}
