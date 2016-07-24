namespace RetryPattern.Strategies
{
    public class ProgressiveDelayRetryStrategy : AbstractRetryStrategy
    {
        private int nextDelay;

        public override void SetDelay(int delay)
        {
            this.delay = delay;
            nextDelay = delay;
        }

        public override bool CanContinue(int currentRetry)
        {
            return currentRetry < maximumRetries;
        }

        public override int GetNextRetryDelay()
        {
            nextDelay *= 2; 
            return nextDelay;
        }

        public override void Reset()
        {
            nextDelay = delay;
        }
    }
}
