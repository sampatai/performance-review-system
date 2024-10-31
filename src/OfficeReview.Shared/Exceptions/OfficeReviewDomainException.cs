namespace OfficeReview.Shared.Exceptions;
  
public class OfficeReviewDomainException : Exception
{
    public OfficeReviewDomainException()
    { }

    public OfficeReviewDomainException(string message)
        : base(message)
    { }

    public OfficeReviewDomainException(string message, Exception innerException)
        : base(message, innerException)
    { }
}

