using HangMan.Models;
using HangMan.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HangMan.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        private readonly IPersistService _persistService;
        private readonly IGameService _gameService;

        public GamesController(IPersistService persistService, IGameService gameService)
        {
            _persistService = persistService;
            _gameService = gameService;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var games = await _persistService.ListAsync(searchString);

            return View(games);
        }

        public async Task<IActionResult> Save(int lives, string actualWord, string user)
        {
            var game = new Game()
            {
                DatePlayed = DateTime.Now,
                Lives = lives, 
                Score = _gameService.CalculateScore(lives),
                User = user,
                Word = actualWord
            };

            await _persistService.Save(game);

            return RedirectToAction(nameof(Index));
        }
    }
}
