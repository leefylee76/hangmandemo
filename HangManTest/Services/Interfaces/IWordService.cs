using System.Threading.Tasks;

namespace HangMan.Services
{
    public interface IWordService
    {
        Task<string> GetWord();
    }
}