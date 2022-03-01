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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UIManager uiManager;
        public MainWindow()
        {
            InitializeComponent();
            uiManager = new UIManager();
            uiManager.Window = this;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if(button != null)
                MessageBox.Show($"{button.Name} Click");
        }

        private void ButtonNumberClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
        }

        public void SetOutputText(string data)
        {
            OutputText.Text = data;
        }
    }
}
