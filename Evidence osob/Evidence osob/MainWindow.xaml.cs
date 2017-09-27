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
using RestSharp;

namespace Evidence_osob
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var client = new RestClient("https://student.sps-prosek.cz/~bastlma14/koment%C3%A1%C5%99e/bastl_databaze.php");

            var request = new RestRequest("", Method.GET);

            //request.AddParameter("token", "saga001", ParameterType.UrlSegment);

            // request.AddUrlSegment("token", "saga001"); 

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var queryResult = client.Execute(request);

            Content.Content = queryResult.Content;
        }
    }
}
