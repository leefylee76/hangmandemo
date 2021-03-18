using HangMan.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HangMan.Services
{
    public interface IPersistService
    {
        Task Save(Game game);
        Task<List<Game>> ListAsync(string searchString);
    };
}