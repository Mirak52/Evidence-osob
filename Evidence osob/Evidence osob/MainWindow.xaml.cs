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
        public static List<Person> Pole = new List<Person>();
        public MainWindow()
        {
            InitializeComponent();
            People.ItemsSource = GetPeople();

        }

        public List<Person> GetPeople()
        {
            var client = new RestClient("https://student.sps-prosek.cz/~bastlma14/Evidence/");
            var request = new RestRequest(Method.GET);
            var res = client.Execute<List<Person>>(request);


            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var queryResult = res.Data;
            return queryResult;

        }
    }
}
