﻿using System;
using System.Windows;
using System.Diagnostics;

namespace Calculator
{
    public enum Function
    {
       None,
       Division,
       Plus,
       Minus,
       Multiply,
    }
    class UIManager
    {
        private HistoryManager historyManager;
        private ComputeManager computeManager = new ComputeManager();
        public MainWindow Window { get; set; }

        private double currentValue = 0;
        private double beforeValue = 0;
        private int decimalPointcount = 1;
        private bool isDecimalPoint = false;

        private Function function = Function.None;

        public UIManager()
        {
            Trace.WriteLine("UIManager");
        }

        //사칙연산 버튼을 실행
        public void FunctionButtonClicked(string stringValue)
        {
           
        }

        //숫자를 출력
        public void NumberButtonClicked(int number)
        {
            if (!isDecimalPoint)
            {
                currentValue = currentValue * 10 + number;
            }
            else
            {
                currentValue = currentValue + Math.Pow(10, -1 * decimalPointcount) * number;
                decimalPointcount++;
            }

            string stringformat = "{0:N" + (decimalPointcount - 1) + "}";
            Window.SetOutputText(string.Format(stringformat, currentValue));
        }
    }
}
