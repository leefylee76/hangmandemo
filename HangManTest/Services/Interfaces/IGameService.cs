using HangMan.Models;
using System.Threading.Tasks;

namespace HangMan.Services
{
    public interface IGameService
    {
        string GetDisguisedWord();
        string GetActualWord();
        void GuessLetter(char letter);
        int GetLivesLeft();
        string GetGuessedLetters();
        Task StartNewGameAsync();
        StateOfPlay GetStateOfPlay();
        int CalculateScore(int lives);
        IGameViewModel GetGameState();
    }
}
