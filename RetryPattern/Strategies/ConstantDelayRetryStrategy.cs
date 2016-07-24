namespace RetryPattern.Strategies
{
    public class ConstantDelayRetryStrategy : AbstractRetryStrategy
    {
        public override int GetNextRetryDelay()
        {
            return delay;
        }

        public override void Reset()
        {
            
        }
    }
}