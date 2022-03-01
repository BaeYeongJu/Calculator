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
using System.Diagnostics;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UIManager UIManager;
      
        public MainWindow()
        {
            InitializeComponent();
            UIManager = new UIManager();
            UIManager.Window = this;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == DivideButton)
                UIManager.FunctionButtonClicked("/");
            else if (button == MultiplyButton)
                UIManager.FunctionButtonClicked("*");
            else if (button == SubtractButton)
                UIManager.FunctionButtonClicked("-");
            else if (button == AddButton)
                UIManager.FunctionButtonClicked("+");
            else if (button == ResultButton)
                UIManager.FunctionButtonClicked("=");
            else if (button ==DecimalPointButton)
                UIManager.FunctionButtonClicked(".");
        }

        private void ButtonNumberClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == OneButton)
                UIManager.NumberButtonClicked(1);
            else if (button == TwoButton)
                UIManager.NumberButtonClicked(2);
            else if (button == ThreeButton)
                UIManager.NumberButtonClicked(3);
            else if (button == FourButton)
                UIManager.NumberButtonClicked(4);
            else if (button == FiveButton)
                UIManager.NumberButtonClicked(5);
            else if (button == SixButton)
                UIManager.NumberButtonClicked(6);
            else if (button == SevenButton)
                UIManager.NumberButtonClicked(7);
            else if (button == EigthtButton)
                UIManager.NumberButtonClicked(8);
            else if (button == NineButton)
                UIManager.NumberButtonClicked(9);
            else if (button == ZeroButton)
                UIManager.NumberButtonClicked(0);
        }

        public void SetOutputText(string data)
        {
            OutputText.Text = data;
        }
    }
}
