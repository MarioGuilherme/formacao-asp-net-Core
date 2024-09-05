namespace DevFreela.Core.Models;

public class PaginationResult<T> {
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int ItemsCount { get; set; }
    public List<T> Data { get; set; }

    public PaginationResult() { }

    public PaginationResult(int page, int totalPages, int pageSize, int itemsCount, List<T> data) {
        this.CurrentPage = page;
        this.TotalPages = totalPages;
        this.PageSize = pageSize;
        this.ItemsCount = itemsCount;
        this.Data = data;
    }
}