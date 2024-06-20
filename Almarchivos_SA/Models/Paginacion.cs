namespace Almarchivos_SA.Models
{
    public class Paginacion
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRegistros { get; set; }
    }
}
