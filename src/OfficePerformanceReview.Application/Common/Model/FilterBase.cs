namespace OfficePerformanceReview.Application.Common.Model
{
    public  record FilterBase(
        int Page, 
        int PageSize,
        string? SearchTerm,
        string SortDirection,
        string? SortColumn);
   
}
