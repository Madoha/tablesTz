namespace tablestz_api.Models
{
    public class SavedWord
    {
        public int Id { get; set; }
        public int WordId { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;
    }
}
