using HangMan.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangMan.Services
{
    public class GameService : IGameService
    {
        private readonly IWordService _wordService;
        private readonly ILogger _logger;
        private readonly IDisplayFormatting _displayFormattingService;
        private readonly int _totalLives;

        private string _disguisedWord { get; set; }
        private string _actualWord { get; set; }
        private int _livesLeft { get; set; }

        private List<char> _guessedLetters = new List<char>();

        public GameService(IWordService wordService, IDisplayFormatting formatService, ILogger<GameService> logger)
        {
            _wordService = wordService ?? throw new ArgumentNullException(nameof(wordService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _displayFormattingService = formatService ?? throw new ArgumentNullException(nameof(formatService));

            _totalLives = 11; // TODO : move to appsettings, to store default difficulty 
                              // TODO : modify this for difficulty level

            StartNewGame();
        }

        public IGameViewModel GetGameState()
        {
            return new GameViewModel()
            {
                DisguisedWord = GetDisguisedWord(),
                ActualWord = GetActualWord(),
                Lives = GetLivesLeft(),
                GuessedLetters = GetGuessedLetters(),
                StateOfPlay = GetStateOfPlay()
            };
        }

        private async Task GetWordAsync()
        {
            _actualWord = await GetNewWordAsync();

            if (string.IsNullOrWhiteSpace(_actualWord))
                throw new InvalidOperationException($"The word cannot be empty or null");
        }

        private Task<string> GetNewWordAsync()
            => _wordService.GetWord();

        public string GetDisguisedWord()
            => _displayFormattingService.FormatLettersForDisplay(_disguisedWord);

        public string GetGuessedLetters()
            => _displayFormattingService.FormatLettersForDisplay(_guessedLetters);

        private void DisguiseWord()
        {
            _disguisedWord = "";
            for (var i = 0; i < _actualWord.Length; i++)
                _disguisedWord += "_";
        }

        public void GuessLetter(char letter)
        {
            letter = Char.ToUpper(letter);

            AddToGuessedLetters(letter);

            RevealCorrectLetter(letter);
        }

        private bool RevealCorrectLetter(char letter)
        {
            var result = false;
            var actualWord = _actualWord.ToUpper().ToArray();
            var hiddenWord = _disguisedWord.ToUpper().ToArray();

            for (var index = 0; index < actualWord.Length; index++)
            {
                if (actualWord[index] == letter)
                {
                    hiddenWord[index] = letter;
                    result = true;
                }
            }

            _disguisedWord = new string(hiddenWord);

            if (!result)
                _livesLeft--;

            return result;
        }

        private void AddToGuessedLetters(char letter)
        {
            if (!_guessedLetters.Contains(letter))
                _guessedLetters.Add(letter);
        }

        public int GetLivesLeft()
            => _livesLeft;

        public StateOfPlay GetStateOfPlay()
        {
            if (_livesLeft <= 0)
                return StateOfPlay.Lost;

            if (_disguisedWord.ToUpper()== _actualWord.ToUpper())
                return StateOfPlay.Won;

            return StateOfPlay.InPlay;
        }

        public string GetActualWord()
            => _actualWord;

        // Todo : When we have a difficulty rating, then we need to increase the score relevant to the difficulty
        public int CalculateScore(int lives)
            => lives;

        // Called by the constructor ? Possible code smell
        private void StartNewGame()
             => Task.Run(() => StartNewGameAsync()).Wait();

        public async Task StartNewGameAsync()
        {
            await GetWordAsync();
            DisguiseWord();

            _livesLeft = _totalLives;
            _guessedLetters = new List<char>();
        }
    }
}
