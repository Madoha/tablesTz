using Microsoft.EntityFrameworkCore;
using tablestz_api.Data;
using tablestz_api.Interfaces;
using tablestz_api.Models;

namespace tablestz_api.Repositories
{
    public class WordRepository : IWordRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public WordRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;   
        }

        public async Task<List<Word>> GetAllWordsAsync()
        {
            return await _dbContext.Words.ToListAsync();
        }

        public async Task<List<Word>> SearchWordsAsync(string letter)
        {
            return await _dbContext.Words
                .Where(word => EF.Functions.Like(word.Name, $"{letter}%"))
                .ToListAsync();
        }

        public async Task<bool> SaveWordsAsync(List<Word> words)
        {
            foreach(var word in words)
            {
                foreach(var wordToRemove in words)
                {
                    _dbContext.Words.Remove(wordToRemove);
                }

                await _dbContext.SavedWords.AddAsync(new SavedWord { WordId = word.Id, CreateTime = DateTime.UtcNow });
                
                await _dbContext.SaveChangesAsync();
            }
            return true;
        }
    }
}
