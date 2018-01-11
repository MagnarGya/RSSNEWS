using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;
using System.Xml;
using System.IO;
using System.Windows.Media.Imaging;

namespace RSSNEWS
{
    class RSSFetcher
    {
        List<News> newsList;
        string[] url;
    
        public RSSFetcher(List<News> _newsList)
        {
            newsList = _newsList;
            ReadUrlFromFile();
        }

        private async void ReadUrlFromFile(){
            using (StreamReader sr = new StreamReader("C:\\Users\\Magna\\source\\repos\\RSSNEWS\\RSSNEWS\\Feeds.txt"))
            {
                String line = await sr.ReadToEndAsync();
                url = line.Split('\n');
            }
            GetRSSFeed();
        }
    
        private void GetRSSFeed()
        {
            SinglyLinkedNode list = new SinglyLinkedNode();
            for (int i = 0; i < url.Length; i++){
                string[] source = url[i].Split(' ');
                XmlReader reader = XmlReader.Create(source[0]);
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                reader.Close();
                foreach (SyndicationItem item in feed.Items)
                {
                    if (item.Title.Text.Length != 1)
                    {
                        BitmapImage image = null;
                        try
                        {
                            image = new BitmapImage(new Uri(item.Links[1].Uri.ToString()));
                        }
                        catch (Exception ex) { }
                        list.AddChild(new News(image, item.Title.Text, item.PublishDate, item.Links[0].Uri.ToString(), source[1]));
                    }
                }
            }

            list.createList(newsList);
        }

        
    }

    class SinglyLinkedNode
    {
        SinglyLinkedNode child;
        News data;

        public SinglyLinkedNode(){
            data = null;
            child = null;
        }
        public SinglyLinkedNode(News news){
            data = news;
            child = null;
        }
        public SinglyLinkedNode(News news, SinglyLinkedNode node)
        {
            data = news;
            child = node;
        }

        public void AddChild(News news){
            if(data==null){
                data = news;
            }else{
                if(DateTimeOffset.Compare(data.date, news.date)<0){
                    SinglyLinkedNode newChild = new SinglyLinkedNode(data, child);
                    child = newChild;
                    data = news;
                }else{
                    if(child!=null){
                        child.AddChild(news);
                    }else{
                        child = new SinglyLinkedNode(news);
                    }
                }
            }
        }
        public void createList(List<News> list){
            list.Add(data);
            if(child!=null){
                child.createList(list);
            }
        }
    }
}
