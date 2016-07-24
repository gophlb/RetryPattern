using RetryPattern.Exceptions;
using RetryPattern.Strategies;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RetryPattern.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpContent> RetryGetAsync(this HttpClient httpClient, string partialUrl, IRetryStrategy retryStrategy)
        {
            int currentRetry = 0;

            while (retryStrategy.CanContinue(currentRetry))
            {
                try
                {
                    Console.WriteLine($"Attempt #{currentRetry + 1}");

                    HttpResponseMessage response = await httpClient.GetAsync(partialUrl);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return response.Content;
                    }
                    else if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new DontRetryException("Forbidden / Unauthorized, abort");
                    }
                }
                catch (DontRetryException)
                {
                    throw;
                }
                catch
                {
                }

                int nextRetryDelay = retryStrategy.GetNextRetryDelay();

                Console.WriteLine($"Awaiting {nextRetryDelay / 1000} secs");

                await Task.Delay(nextRetryDelay);

                currentRetry++;
            }
            
            throw new RetryExceededException("Retry exceeded");
        }
    }
}