using Newtonsoft.Json;
using NYTimesSearch.ViewModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using NYTimesSearch.Config;

namespace NYTimesSearch.Services
{
    /// <summary>
    /// News service
    /// </summary>
    public class NewsService : INewsService
    {
        #region configuration
        private readonly string APIKEY = CustomConfiguration.Settings.ApiKey;
        private readonly string URL = CustomConfiguration.Settings.ApiUrl;
        private readonly HttpClient client;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public NewsService()
        {
            client = new HttpClient();
        }

        /// <summary>
        /// Search news on New York Times service
        /// </summary>
        /// <param name="keywords">keywords to search</param>
        /// <param name="page">Used for pagination</param>
        /// <returns></returns>
        public async Task<SearchResultsViewModel> SearchNews(string keywords, string page)
        {
            SearchResultsViewModel result = new SearchResultsViewModel();
            try
            {
                var builder = new UriBuilder(URL);
                var query = HttpUtility.ParseQueryString(builder.Query);
                query["api-key"] = APIKEY;
                query["page"] = page;
                query["q"] = keywords;
                builder.Query = query.ToString();
                string url = builder.ToString();

                string responseBody = await client.GetStringAsync(url).ConfigureAwait(false);

                dynamic jsonResponse = JsonConvert.DeserializeObject(responseBody);

                SearchResultItemViewModel item;
                int itemsFound = jsonResponse.response.meta.hits > 10 ? 10 : jsonResponse.response.meta.hits;
                for (int i = 1; i < itemsFound; i++)
                {
                    item = new SearchResultItemViewModel();
                    item.ArticleName = jsonResponse.response.docs[i].headline.main;
                    item.ArticleLink = jsonResponse.response.docs[i].web_url;
                    item.ArticleSource = jsonResponse.response.docs[i].source;

                    result.SearchResultsList.Add(item);
                }

                return result;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return result;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }
    }
}