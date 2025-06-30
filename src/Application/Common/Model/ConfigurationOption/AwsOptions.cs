namespace OfficePerformanceReview.Application.Common.Options
{
    /// <summary>
    /// Represents the AWS configuration section for strongly-typed binding.
    /// </summary>
    public class AwsConfigurationOptions
    {
        public string Region { get; set; } = string.Empty;
        public string Credentials { get; set; } = string.Empty;
        public bool UseEnvironmentVariables {  get; set; } = false;
        public AwsSecretsManagerOptions SecretsManager { get; set; } = new();
    }
    public class AwsSecretsManagerOptions
    {
        public string SecretName { get; set; } = string.Empty;
        public string SecretVersion { get; set; } = string.Empty;
    }
}
