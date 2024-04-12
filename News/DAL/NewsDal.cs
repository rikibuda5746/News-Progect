using DAL.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions;
using System.Xml.Linq;

namespace DAL
{
    public class NewsDal
    {
        private const string Url = "https://www.jdn.co.il/feed/";
        private readonly IMemoryCache cache;
        private const string CacheKey = "jdn";

       //Constractor
        public NewsDal(IMemoryCache memoryCache)
        {
            cache = memoryCache;
        }

        
        //Function whice return news data from cache.
        public async Task<IEnumerable<Item>> GetNews()
        {
            IEnumerable<Item> cacheNews;
            //The data in cache
            if (cache.TryGetValue(CacheKey,out cacheNews))
            {
                return cacheNews;
            }

            //The data is'nt in cache.
            try
            {
                cacheNews = await this.FetchNews();
                return cacheNews;
            }
            catch (Exception ex)
            {
                //Print exception to log file.
                Log.AddToLog("exception: " + ex.Message);
                return null;
            }
        }

        // Function that take news according url and save data in cache.
        private async Task<IEnumerable<Item>> FetchNews()
        {

            using (var httpClient = new HttpClient())
            {
                //Get the data from API.
                var data = await httpClient.GetStringAsync(Url);

                //Parse to XML.
                var xmlDoc = XDocument.Parse(data);

                //Read XML and convert to Item Objects to list.
                var listNews = new List<Item>();
                foreach (var item in xmlDoc.Descendants("item"))
                {
                    var itemNew = new Item
                    {
                        title = item.Element("title")?.Value,
                        description = item.Element("description")?.Value,
                        link = item.Element("link")?.Value,
                        pubDate = item.Element("pubDate")?.Value.Substring(0, (int)(item.Element("pubDate")?.Value.Length - 7))

                    };
                    listNews.Add(itemNew);
                }

                //The definition of cache.
                var cacheEntryOptions = new MemoryCacheEntryOptions();
                cache.Set(CacheKey, listNews, cacheEntryOptions);

                //Return the list of News
                return listNews;
            }
        }

        // The function returne one Item acorrding to title.
        public async Task<Item> GetItem(string title)
        {
            try
            {
                // Fetch all news items
                var allNews = await GetNews();

                // Find the news item with the specified title
                var item = allNews?.FirstOrDefault(item => item.title == title);

                return item;
            }
            catch (Exception ex)
            {
                //Print exception to log file, if there is exeption.
                Log.AddToLog("exception: " + ex.Message);
                return null;
            }
        }
    }
}
