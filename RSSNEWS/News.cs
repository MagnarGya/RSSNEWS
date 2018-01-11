using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RSSNEWS
{
    class News
    {
        BitmapImage image;
        public BitmapImage Image { get { return image; } }
        string title;
        public string Title { get { return title; } }
        public DateTimeOffset date;
        public string Date { get { return date.ToLocalTime().ToString("dddd, HH:mm"); } }
        string link;
        public string Link { get { return link; } }
        string source;
        public string Source { get { return source; } }

        public News(BitmapImage _image, string _title, DateTimeOffset _date, string _link, string _source){
            image = _image;
            title = _title;
            date = _date;
            link = _link;
            source = _source;
        }
    }
}
