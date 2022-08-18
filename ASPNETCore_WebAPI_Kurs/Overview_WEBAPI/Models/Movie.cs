namespace Overview_WEBAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } = 9.99m;
        public MovieEnum MovieEnum { get; set; }
    }

    public enum MovieEnum { Action, Drama, Comedy, Romance, Thriller, Crime, Horror, Animation, ScienceFiction, Docu}

}
