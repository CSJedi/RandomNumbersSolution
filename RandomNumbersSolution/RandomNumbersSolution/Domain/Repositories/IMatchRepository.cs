using RandomNumbersSolution.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomNumbersSolution.Domain.Repositories
{
    public interface IMatchRepository : ICrudRepository<int, Match>
    {
        Task<List<Match>> GetAllMatches();
    }
}
