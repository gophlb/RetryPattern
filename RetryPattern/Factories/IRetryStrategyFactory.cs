using RetryPattern.Strategies;

namespace RetryPattern.Factories
{
    public interface IRetryStrategyFactory<T> where T : IRetryStrategy, new()
    {
        IRetryStrategyFactory<T> CreateStrategy();

        IRetryStrategyFactory<T> SetMaximumRetries(int maximumRetries);

        IRetryStrategyFactory<T> SetMillisecondsDelay(int delay);

        IRetryStrategy GetStrategy();
    }
}