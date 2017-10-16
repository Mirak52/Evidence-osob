using RestSharp;
using System;
using System.Collections.Generic;
using System.Windows;

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
            Date.DisplayDateEnd = DateTime.Today;
            

        }

        public List<Person> GetPeople()
        {
            var client = new RestClient("https://student.sps-prosek.cz/~bastlma14/Evidence/");
            var request = new RestRequest(Method.GET);
            var res = client.Execute<List<Person>>(request);


            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            
            var queryResult = res.Data;
            foreach (var osoba in queryResult)
            {
                if (!string.IsNullOrEmpty((osoba.pohlavi = 1.ToString())))
                {
                    osoba.pohlavi = "muž";

                }
                else
                {
                    osoba.pohlavi = "žena";
                }
            }
            return queryResult;

        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            var client = new RestClient("https://student.sps-prosek.cz/~bastlma14/Evidence/");
            var request = new RestRequest(Method.POST);
             



//            var res = client.Execute(request);


        }
    }
}
