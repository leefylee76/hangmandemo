using HangMan.Data;
using HangMan.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangMan.Services
{
    public class PersistService : IPersistService
    {
        private readonly ApplicationDbContext _context;

        public PersistService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Save(Game game)
        {
            _context.Add(game);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Game>> ListAsync(string searchString)
        {
            var games = from m in _context.Game
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(s => s.User.Contains(searchString));
            }

            return await games.ToListAsync();
        }
    }
}
