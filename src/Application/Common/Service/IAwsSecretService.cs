
namespace OfficePerformanceReview.Application.Common.Service
{
    /// <summary>
    /// Service to retrieve secrets from AWS Secrets Manager.
    /// </summary>
    public interface IAwsSecretService
    {
        Task<string> GetSecretStringAsync(string secretName);
    }
}
