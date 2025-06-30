namespace OfficePerformanceReview.Application.Common.Options
{
    /// <summary>
    /// Represents the AWS configuration section for strongly-typed binding.
    /// </summary>
    public class AWSConfigurationOptions
    {
        public string Region { get; set; } = string.Empty;
        public string CredentialProfile { get; set; } = string.Empty;
        public bool UseEnvironmentVariables {  get; set; } = false;
        public AwsSecretsManagerOptions SecretsManager { get; set; } = new();
    }
    public class AwsSecretsManagerOptions
    {
        public string SecretName { get; set; } = string.Empty;
        public string DefaultSecretVersion { get; set; } = string.Empty;
    }
}
