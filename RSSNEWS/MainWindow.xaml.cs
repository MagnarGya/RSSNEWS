using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace RSSNEWS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<News> newsList;
        RSSFetcher fetcher;

        public MainWindow()
        {
            newsList = new List<News>();
            fetcher = new RSSFetcher(newsList);
            InitializeComponent();
            
            NewsList.ItemsSource = newsList;
        }

        

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListViewItem).DataContext as News;

            System.Diagnostics.Process.Start(item.Link);
        }
    }
}
