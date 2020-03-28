using CodeHollow.FeedReader;
using Covid19.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Covid19.Services
{
    public class NewsService: INewsService
    {
        const string REDDIT_WORLD_NEWS = "https://www.reddit.com/r/worldnews.rss";
        const string BBC_NEWS = "http://feeds.bbci.co.uk/news/world/rss.xml";
        const string THE_NEW_YORK_TIMES = "https://www.nytimes.com/svc/collections/v1/publish/https://www.nytimes.com/section/world/rss.xml";
        const string AL_JAZEERA = "https://www.aljazeera.com/xml/rss/all.xml";
        const string YAHOO_NEWS = "https://www.yahoo.com/news/rss/world";
        const string CNN = "http://rss.cnn.com/rss/edition_world.rss";
        const string TIMES_OF_INDIA = "https://timesofindia.indiatimes.com/rssfeeds/296589292.cms";

        public async Task<IEnumerable<News>> GetFeed(NewsSource source)
        {
            var newsItems = new List<News>();

            string feedUrl;
            switch(source)
            {
                case NewsSource.REDDITWorldNews:
                    feedUrl = REDDIT_WORLD_NEWS;
                    break;

                case NewsSource.BBCNews:
                    feedUrl = BBC_NEWS;
                    break;

                case NewsSource.TheNewYorkTimes:
                    feedUrl = THE_NEW_YORK_TIMES;
                    break;

                case NewsSource.AlJazeera:
                    feedUrl = AL_JAZEERA;
                    break;

                case NewsSource.YahooNews:
                    feedUrl = YAHOO_NEWS;
                    break;

                case NewsSource.CNN:
                    feedUrl = CNN;
                    break;

                default:
                    feedUrl = TIMES_OF_INDIA;
                    break;
            }

            var feed = await FeedReader.ReadAsync(feedUrl);
            foreach(var item in feed.Items)
            {
                var news = new News(source, item.Title);
                news.Description = item.Description;
                news.Content = item.Content;
                news.Author = item.Author;
                news.PublishingDate = item.PublishingDate;
                newsItems.Add(news);
            }

            return newsItems;
        }
    }
}
