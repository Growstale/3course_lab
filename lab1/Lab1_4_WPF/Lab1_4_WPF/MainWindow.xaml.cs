using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
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

namespace Lab1_4_WPF
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }
        private async void Result_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(X_TextBox.Text, out int x) && int.TryParse(Y_TextBox.Text, out int y))
                {
                    string url = $"https://localhost:7192/SUM";

                    using (HttpClient client = new HttpClient())
                    {
                        
                        var content = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string, string>("X", x.ToString()),
                            new KeyValuePair<string, string>("Y", y.ToString())
                        });

                        HttpResponseMessage response = await client.PostAsync(url, content);
                        response.EnsureSuccessStatusCode();

                        string responseText = await response.Content.ReadAsStringAsync();
                        Result_TextBox.Text = $"X + Y = {responseText}";
                        
                    }
                    
                    /*
                    string url = $"https://localhost:7192/SUM?X={x}&Y={y}";

                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.PostAsync(url, null);
                        response.EnsureSuccessStatusCode();

                        string responseText = await response.Content.ReadAsStringAsync();
                        Result_TextBox.Text = responseText.ToString();
                    }
                    */
                }
                else
                {
                    Result_TextBox.Text = "Необходимо ввести числа";
                }
            }
            catch (Exception ex)
            {
                Result_TextBox.Text = $"Ошибка: {ex.Message}";
            }
        }

    }
}
