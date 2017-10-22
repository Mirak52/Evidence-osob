using RestSharp;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;

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
            if (name.Text != "" & sirName.Text != "" & Date.Text != "" & rcNumber.Text != "")
            {
                GetRc();
            }
            else
            {
                Error.Content = "zadej všechny parametry!!";
            }    
        }

        public void GetRc()
        {
            string bornNumber;
            string month;
            string date = Date.Text;
            int den = int.Parse(date.Substring(0, 2));
            int mes = int.Parse(date.Substring(3, 2));
            int rok = int.Parse(date.Substring(8, 2));
            den.ToString();
            rok.ToString();
            mes.ToString();
            if (man.IsChecked == true)
            {
                month = mes.ToString();
                if (month.Length <= 10)
                {
                    month = "0" + month;
                }
                bornNumber = "" + rok + month + den;
            }
            else
            {
                mes = mes + 50;
                month = mes.ToString();
                if (month.Length <= 10)
                {
                    month = "0" + month;
                }
                bornNumber = "" + rok + month + den;
            }
          
            if (RcCheck(bornNumber))
            {
                saveData(bornNumber);
            }
            else
            {
                Error.Content = "Špatně zadané rodné číslo:";
            }
        }

        public void saveData(string bornNumber)
        {
            int sex;
            bornNumber = bornNumber + "/" + rcNumber;
            if (man.IsChecked == true)
            {
                sex = 0;
            }
            else
            {
                sex = 1;
            }
            var client = new RestClient("https://student.sps-prosek.cz/~bastlma14/Evidence/");
            var request = new RestRequest(Method.POST);
            request.AddParameter("Name", name.Text);
            request.AddParameter("sirName", sirName.Text);
            request.AddParameter("lifeNumber", bornNumber);
            request.AddParameter("dateOfBirth", Date.Text);
            request.AddParameter("sex", sex);
            IRestResponse response = client.Execute(request);
            Error.Content = "uspěšně uloženo";
            People.ItemsSource = GetPeople();

        }
        public bool RcCheck(string bornNumber)
        {
            var client = new RestClient("http://overeni-rodneho-cisla.peckapc.cz/");
            var request = new RestRequest(Method.GET);
            request.AddParameter("a", "dovalid");
            request.AddParameter("number", bornNumber);
            request.AddParameter("number1", rcNumber.Text);
            IRestResponse response = client.Execute(request);
            string content = response.Content;
            string valiVPořádku = "V POŘÁDKU";
            bool valid = content.Contains(valiVPořádku);
            if (valid)
            {
                return true;
            }
            return false;
        }

        private void Date_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            

        }
    }
}
