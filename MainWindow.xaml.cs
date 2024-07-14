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
using PastebinAPI;

namespace BrowserSite
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeWebViewAsync();
        }

        private async void InitializeWebViewAsync()
        {
            await webView.EnsureCoreWebView2Async();
            webView.CoreWebView2.NavigateToString("<!DOCTYPE html>\r\n<html>\r\n<head>\r\n  <title>Анимация загрузки</title>\r\n  \r\n  <style>\r\n    .loader-container {\r\n      position: fixed;\r\n      top: 50%;\r\n      left: 50%; \r\n      transform: translate(-50%, -50%);\r\n      \r\n      background: rgba(255,255,255,0.8);\r\n      padding: 20px;\r\n      border-radius: 10px;  \r\n    }\r\n    \r\n    .loader {\r\n      border: 10px solid #f3f3f3;\r\n      border-top: 10px solid #3498db;\r\n      border-radius: 50%;\r\n      width: 80px;\r\n      height: 80px;\r\n      animation: spin 1s linear infinite;\r\n    }\r\n    \r\n    @keyframes spin {\r\n      0% { transform: rotate(0deg); }\r\n      100% { transform: rotate(360deg); }\r\n    }\r\n  </style>\r\n  \r\n</head>\r\n\r\n<body>\r\n\r\n  <div class=\"loader-container\">\r\n    <div class=\"loader\"></div>\r\n  </div>\r\n\r\n</body>\r\n</html>");
            await nav();
        }

        private async Task nav()
        {
            Pastebin.DevKey = "5ln6HOXq_RjBLBI1iSXr-joc66hVi3C7";
            User user = await Pastebin.LoginAsync("torbetsite", "betternerfIrelia1602");
            var pastes = await user.ListPastesAsync(5);
            foreach(var paste in pastes)
            {
                if(paste.Title == "torbet site")
                {
                    string text = await paste.GetRawAsync();
                    webView.CoreWebView2.Navigate(text);
                    return;
                }
            }
            MessageBox.Show("Ошибка получения ссылки");
            Environment.Exit(0);
            return;
        }
    }
}
