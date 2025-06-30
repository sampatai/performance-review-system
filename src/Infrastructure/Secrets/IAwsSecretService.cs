using Amazon;
using Amazon.SecretsManager;

using Microsoft.Extensions.Options;
using OfficePerformanceReview.Application.Common.Options;
using OfficePerformanceReview.Application.Common.Service;
using Amazon.Runtime;
using Amazon.SecretsManager.Model;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
namespace OfficePerformanceReview.Infrastructure.Secrets
{
    /// <summary>
    /// Service to retrieve secrets from AWS Secrets Manager.
    /// </summary>
    public class AwsSecretService : IAwsSecretService
    {
        private readonly IAmazonSecretsManager _secretsManager;
        private readonly ILogger<AwsSecretService> _logger;
        private readonly IOptions<AWSConfigurationOptions> _awsOptions;
        public AwsSecretService(IOptions<AWSConfigurationOptions> options, 
            ILogger<AwsSecretService> logger,
           IAmazonSecretsManager amazonSecretsManager )
        {
            _awsOptions = options ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            // Do NOT pass credentials explicitly in production
            _secretsManager = amazonSecretsManager;
            //var chain = new CredentialProfileStoreChain();
            //AWSCredentials awsCredentials;
            //if (chain.TryGetAWSCredentials(options.Value.Credentials, out awsCredentials))
            //{
            //    _secretsManager = new AmazonSecretsManagerClient(awsCredentials, RegionEndpoint.APSoutheast2);
            //}

        }

        /// <summary>
        /// Retrieves a secret string from AWS Secrets Manager.
        /// </summary>
        /// <param name="secretName">The name or ARN of the secret.</param>
        /// <returns>The secret string value.</returns>
        public async Task<string> GetSecretStringAsync(string secretName)
        {
            try
            {
                var request = new GetSecretValueRequest
                {
                    SecretId = secretName,
                    VersionStage = _awsOptions.Value.SecretsManager.DefaultSecretVersion
                };

    
                var response = await _secretsManager.GetSecretValueAsync(request);
                return response.SecretString;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve secret {SecretName} from AWS Secrets Manager", secretName);
                throw;
            }
        }
    }
}
