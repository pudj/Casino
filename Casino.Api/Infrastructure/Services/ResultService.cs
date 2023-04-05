using Casino.Api.Domain.Entities;
using Casino.Api.Infrastructure.Persistence;
using Casino.Api.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Casino.Api.Infrastructure.Services
{
    public class ResultService : IResultService
    {
        private readonly CasinoContext _context;

        public ResultService(CasinoContext context)
        {
            _context = context;
        }
        public async Task<List<Result>> GetResults()
        {
            return await _context.Results.ToListAsync();
        }
    }
}
