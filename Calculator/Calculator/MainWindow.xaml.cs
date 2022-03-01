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
            if (button == null)
                MessageBox.Show("ButtonNumberClick null");

            if (button == OneButton)
                uiManager.NumberButtonClicked(1);
            else if (button == TwoButton)
                uiManager.NumberButtonClicked(2);
            else if (button == ThreeButton)
                uiManager.NumberButtonClicked(3);
            else if (button == FourButton)
                uiManager.NumberButtonClicked(4);
            else if (button == FiveButton)
                uiManager.NumberButtonClicked(5);
            else if (button == SixButton)
                uiManager.NumberButtonClicked(6);
            else if (button == SevenButton)
                uiManager.NumberButtonClicked(7);
            else if (button == EigthtButton)
                uiManager.NumberButtonClicked(8);
            else if (button == NineButton)
                uiManager.NumberButtonClicked(9);
            else if (button == ZeroButton)
                uiManager.NumberButtonClicked(0);
        }

        public void SetOutputText(string data)
        {
            OutputText.Text = data;
        }
    }
}
