using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Dictionary<Button, string> InputButtonDictionary = new Dictionary<Button, string>();
        private Dictionary<Button, string> operatorButtonDictionary = new Dictionary<Button, string>();
        public Button ClickedButton = null;
        public MainWindow()
        {
            InitializeComponent();
            uiManager = new UIManager();
            uiManager.Window = this;

            InputButtonDictionary.Add(ZeroButton, "0");
            InputButtonDictionary.Add(OneButton, "1");
            InputButtonDictionary.Add(TwoButton, "2");
            InputButtonDictionary.Add(ThreeButton, "3");
            InputButtonDictionary.Add(FourButton, "4");
            InputButtonDictionary.Add(FiveButton, "5");
            InputButtonDictionary.Add(SixButton, "6");
            InputButtonDictionary.Add(SevenButton, "7");
            InputButtonDictionary.Add(EigthtButton, "8");
            InputButtonDictionary.Add(NineButton, "9");
            InputButtonDictionary.Add(DecimalPointButton, ".");

            operatorButtonDictionary.Add(DivideButton, "/");
            operatorButtonDictionary.Add(MultiplyButton, "*");
            operatorButtonDictionary.Add(SubtractButton, "-");
            operatorButtonDictionary.Add(AddButton, "+");
            operatorButtonDictionary.Add(EqualButton, "=");
            operatorButtonDictionary.Add(Negate, "+/-");
            operatorButtonDictionary.Add(ClearButton, "C");
            operatorButtonDictionary.Add(ClearAllButton, "CE");
            operatorButtonDictionary.Add(DeleteButton, "Delete");
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if(button != null)
                MessageBox.Show($"{button.Name} Click");
        }

        private void OperatorButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
                MessageBox.Show("ButtonNumberClick null");
            uiManager.OperatorButtonClicked(operatorButtonDictionary[button]);
            ClickedButton = button;
        }

        private void InputButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
                MessageBox.Show("ButtonNumberClick null");
            uiManager.InputButtonClicked(InputButtonDictionary[button]);
        }

        public void SetResultText(string data)
        {
            OutputText.Text = data;
        }

        public void SetCalculatedText(string data)
        {
            ComputeText.Text = data;
        }
    }
}
