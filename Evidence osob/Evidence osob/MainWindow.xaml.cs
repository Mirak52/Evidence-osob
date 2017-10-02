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
        public static List<int> Pole = new List<int>();
        public MainWindow()
        {
            InitializeComponent();
            var client = new RestClient("https://student.sps-prosek.cz/~bastlma14/bastl_databaze.php");
            var request = new RestRequest(Method.POST);
            string name = "Marek";
            int sex =0;
            request.AddParameter("jmeno", name);
            request.AddParameter("pohlavi", sex);

           request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var queryResult = client.Execute(request);
            Content.Content = queryResult.Content;
        }
    }
}
