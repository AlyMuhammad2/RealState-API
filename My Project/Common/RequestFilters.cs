namespace My_Project.Common
{
    public class RequestFilters
    {
        public int pageNumber { get; init; } = 1;
        public int pageSize { get; init; } = 12;
        public string? SearchValue {  get; init; }
        public string? SortColumn { get; init; }
        public string? SortDirection { get; init; } = "ASC";
    }
}
