namespace Fragrance_Web_App.Models
{
    public class IndexViewModel
    {
        public int FragrancesCount { get; set; }
        public int ReviewsCount { get; set; }
        public int UsersCount { get; set; }
        public List<FragranceListingViewModel> Fragrances {  get; set; }
    }
}
