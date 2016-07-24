using RetryPattern.Strategies;

namespace RetryPattern.Factories
{
    public class GenericRetryStrategyFactory<T> : IRetryStrategyFactory<T> where T: IRetryStrategy, new()
    {
        private T strategy;

        public IRetryStrategyFactory<T> CreateStrategy()
        {
            strategy = new T();

            return this;
        }

        public IRetryStrategy GetStrategy()
        {
            return strategy;
        }

        public IRetryStrategyFactory<T> SetMillisecondsDelay(int delay)
        {
            strategy.SetDelay(delay);

            return this;
        }

        public IRetryStrategyFactory<T> SetMaximumRetries(int maximumRetries)
        {
            strategy.SetMaximumRetries(maximumRetries);

            return this;
        }
    }
}