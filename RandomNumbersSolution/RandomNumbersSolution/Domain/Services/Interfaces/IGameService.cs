using RandomNumbersSolution.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomNumbersSolution.Domain.Services.Interfaces
{
    public interface IGameService
    {
        Task Play(int number, string currentUser);
        Task<List<Match>> GetAllMatches();
    }
}
