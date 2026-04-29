namespace BeverageBackend.Application.Common.Query
{
    public class QueryParameters
    {
        private const int MaxPageSize = 30;
        private int pageSize = 10;
        private int pageNumber = 1;

        public int PageNumber 
        {
            get => pageNumber;
            set => pageNumber = value < 1 ? 1 : value;
        }

        public int PageSize 
        {
            get => pageSize;
            set
            {
                if (pageSize < 1)
                    pageSize = 10;
                else if (pageSize > MaxPageSize)
                    pageSize = MaxPageSize;
                else
                    pageSize = value;
            }
        }

        public string? SortBy { get; set; }
        public bool Desc { get; set; } = false;
    }
}
