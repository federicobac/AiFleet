public interface IFeatureFlagService
{
    bool IsFeatureEnabled(string featureName);
}