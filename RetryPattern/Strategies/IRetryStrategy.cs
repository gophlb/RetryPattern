namespace RetryPattern.Strategies
{
    public interface IRetryStrategy
    {
        int GetNextRetryDelay();

        bool CanContinue(int currentRetry);

        void SetMaximumRetries(int maximumRetries);

        void SetDelay(int delay);

        void Reset();
    }
}