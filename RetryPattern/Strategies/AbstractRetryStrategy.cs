namespace RetryPattern.Strategies
{
    public abstract class AbstractRetryStrategy : IRetryStrategy
    {
        protected int maximumRetries = 0;
        protected int delay = 0;

        public virtual bool CanContinue(int currentRetry)
        {
            return currentRetry < maximumRetries;
        }

        public abstract int GetNextRetryDelay();

        public void SetMaximumRetries(int maximumRetries)
        {
            this.maximumRetries = maximumRetries;
        }

        public virtual void SetDelay(int delay)
        {
            this.delay = delay;
        }

        public abstract void Reset();
    }
}
