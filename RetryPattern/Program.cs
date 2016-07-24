using RetryPattern.Factories;
using RetryPattern.Strategies;
using RetryPattern.Extensions;
using System.Net.Http;
using System;
using System.Threading.Tasks;

namespace RetryPattern
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://www.checkupdown.com");

            string[] partialUrls = new string[] { "/", "thisIsA404Error", "/accounts/grpb/B1394343/" };
            
            GetAllWithRetryStrategy<ConstantDelayRetryStrategy>(httpClient, partialUrls).Wait();
            GetAllWithRetryStrategy<ProgressiveDelayRetryStrategy>(httpClient, partialUrls).Wait();
            
            Console.WriteLine();
            Console.WriteLine("End of process. Press ENTER key");
            Console.ReadLine();    
        }


        private static async Task GetAllWithRetryStrategy<T>(HttpClient httpClient, string[] partialUrls) where T : IRetryStrategy, new()
        {
            Console.WriteLine($"\n\nStarting process with strategy {typeof(T).Name}");
            Console.WriteLine("----------------------------------------------------------------------------------");

            IRetryStrategy retryStrategy = GetRetryStrategy<T>();

            HttpContent content;
            foreach (string partialUrl in partialUrls)
            {
                Console.WriteLine($"\n************** {httpClient.BaseAddress}{partialUrl}");
                try
                {
                    content = await httpClient.RetryGetAsync(partialUrl, retryStrategy);

                    Console.WriteLine($"------------- Success");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"------------- Failure: {e.Message}");
                }

                retryStrategy.Reset();             
            }
        }


        private static IRetryStrategy GetRetryStrategy<T>() where T : IRetryStrategy, new()
        {
            IRetryStrategyFactory<T> retryStrategyFactory = new GenericRetryStrategyFactory<T>();

            IRetryStrategy retryStrategy = retryStrategyFactory
                                                .CreateStrategy()
                                                .SetMaximumRetries(3)
                                                .SetMillisecondsDelay(1000)
                                                .GetStrategy()
                                            ;

            return retryStrategy;
        }
    }
}