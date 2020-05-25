using Newtonsoft.Json;
using NYTimesSearch.ViewModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using NYTimesSearch.Config;

namespace NYTimesSearch.Services
{
    //TODO: Comments
    public class NewsService : INewsService
    {
        private readonly string APIKEY = CustomConfiguration.Settings.ApiKey;
        private readonly string URL = CustomConfiguration.Settings.ApiUrl;
        private readonly HttpClient client;

        public NewsService()
        {
            client = new HttpClient();
        }

        public async Task<SearchResults> SearchNews(string keywords, string page)
        {
            SearchResults result = new SearchResults();
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

                SearchResultItem item;
                int itemsFound = jsonResponse.response.meta.hits > 10 ? 10 : jsonResponse.response.meta.hits;
                for (int i = 1; i < itemsFound; i++)
                {
                    item = new SearchResultItem();
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