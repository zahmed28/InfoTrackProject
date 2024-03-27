namespace SearchReadService.Domain
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Records { get; set; }
        public int TotalRecords { get; set; }

        public int CurrentPageSize { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}
