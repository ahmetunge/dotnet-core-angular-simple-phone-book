namespace Api.Entities.Helpers
{
    public class CustomerParams
    {
        private const int MaxPageSize = 50;

        private int _pageSize = 6;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public int PageIndex { get; set; } = 1;

        private string _search;
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}