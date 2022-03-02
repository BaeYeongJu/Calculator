﻿using System;
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
        private Dictionary<Button, int> numberButtonDictionary = new Dictionary<Button, int>();
        private Dictionary<Button, string> functionButtonDictionary = new Dictionary<Button, string>();
        public MainWindow()
        {
            InitializeComponent();
            uiManager = new UIManager();
            uiManager.Window = this;

            numberButtonDictionary.Add(ZeroButton, 0);
            numberButtonDictionary.Add(OneButton, 1);
            numberButtonDictionary.Add(TwoButton, 2);
            numberButtonDictionary.Add(ThreeButton, 3);
            numberButtonDictionary.Add(FourButton, 4);
            numberButtonDictionary.Add(FiveButton, 5);
            numberButtonDictionary.Add(SixButton, 6);
            numberButtonDictionary.Add(SevenButton, 7);
            numberButtonDictionary.Add(EigthtButton, 8);
            numberButtonDictionary.Add(NineButton, 9);

            functionButtonDictionary.Add(DivideButton, "/");
            functionButtonDictionary.Add(MultiplyButton, "*");
            functionButtonDictionary.Add(SubtractButton, "-");
            functionButtonDictionary.Add(AddButton, "+");
            functionButtonDictionary.Add(ResultButton, "=");
            functionButtonDictionary.Add(DecimalPointButton, ".");
            functionButtonDictionary.Add(ClearButton, "C");
            functionButtonDictionary.Add(ClearAllButton, "CE");
            functionButtonDictionary.Add(DeleteButton, "Delete");
            functionButtonDictionary.Add(PlusOnMinus, "+/-");
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            uiManager.FunctionButtonClicked(functionButtonDictionary[button]);
        }

        private void ButtonNumberClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
                MessageBox.Show("ButtonNumberClick null");
            uiManager.NumberButtonClicked(numberButtonDictionary[button]);
        }
        public void SetOutputText(string data)
        {
            OutputText.Text = data;
        }
    }
}
