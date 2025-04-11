namespace OfficePerformanceReview.Application.Common.Model
{
    public  record FilterBase(
        int PageNumber, 
        int PageSize,
        string SearchTerm,
        string SortDirection,
        string SortColumn);
   
}
