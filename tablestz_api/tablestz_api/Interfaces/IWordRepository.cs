using tablestz_api.Models;

namespace tablestz_api.Interfaces
{
    public interface IWordRepository
    {
        Task<List<Word>> GetAllWordsAsync();
        Task<List<Word>> SearchWordsAsync(string letter);
        Task<bool> SaveWordsAsync(List<Word> words);
    }
}
