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
            if (button == DivideButton)
                uiManager.FunctionButtonClicked("/");
            else if (button == MultiplyButton)
                uiManager.FunctionButtonClicked("*");
            else if (button == SubtractButton)
                uiManager.FunctionButtonClicked("-");
            else if (button == AddButton)
                uiManager.FunctionButtonClicked("+");
            else if (button == ResultButton)
                uiManager.FunctionButtonClicked("=");
            else if (button ==DecimalPointButton)
                uiManager.FunctionButtonClicked(".");
            else if (button == ClearButton)
                uiManager.FunctionButtonClicked("C");
            else if (button == ClearAllButton)
                uiManager.FunctionButtonClicked("CE");
            else if (button == DeleteButton)
                uiManager.FunctionButtonClicked("Delete");
            else if (button == PlusOnMinus)
                uiManager.FunctionButtonClicked("+/-");
        }

        private void ButtonNumberClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
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
