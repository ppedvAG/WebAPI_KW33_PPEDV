namespace KonventionenSamplesWebAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; } = 9.99m;
        public MovieEnum MovieEnum { get; set; }
    }

    public enum MovieEnum { Action, Drama, Comedy, Romance, Thriller, Crime, Horror, Animation, ScienceFiction, Docu}

}
