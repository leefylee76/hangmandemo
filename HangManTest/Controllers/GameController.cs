using HangMan.Data;
using HangMan.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HangMan.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly ILogger _logger;

        public GameController(ApplicationDbContext context, 
                              IGameService gameService,
                              ILogger<GameController> logger)
        {
            _gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IActionResult Index()
        {
            return View(_gameService.GetGameState());
        }

        public async Task<IActionResult> NewGame()
        {
            await _gameService.StartNewGameAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Guess(string letter) // TODO : ideally this should be a char
        {
            try
            {
                if (Validate(letter))
                    _gameService.GuessLetter(letter[0]);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erroring trying to Guess Letter {letter}", ex);
            }
        }

        private bool Validate(string value)
        {  
            if (string.IsNullOrWhiteSpace(value))
                _logger.LogError("value cannot be empty");
            else if (value.Length > 1)
                _logger.LogError($"value {value} cannot be more than 1 character");
            else if (!Char.IsLetter(value[0]))
                _logger.LogError($"value {value} has to be a letter");
            else
                return true;

            return false;
        }
    }
}
