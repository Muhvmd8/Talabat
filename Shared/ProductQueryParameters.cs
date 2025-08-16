namespace Shared;
public class ProductQueryParameters
{
    private const int _defaultPageSize = 3;
    private const int _maxPageSize = 6;
    public int? BrandId { get; set; }
    public int? TypeId { get; set; }
    public ProductSortingOptions Option { get; set; }
    public string? SearchValue { get; set; }
    public int PageIndex { get; set; } = 1;
    private int _pageSize = _defaultPageSize;
    public int PageSize 
    {
        get { return _pageSize; }
        set { _pageSize = value > _maxPageSize? _maxPageSize : value; }
    }
}
