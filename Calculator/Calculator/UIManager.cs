using System;
using System.Windows;
using System.Diagnostics;

namespace Calculator
{
    public enum LastOperator
    {
       None,
       Division,
       Plus,
       Minus,
       Multiply,
       Equal,
       Negate,
       Clear,
       ClearAll,
       Delete,
    }

    class UIManager
    {
        private MemoryManager memoryManager;
        private ComputeManager computeManager = new ComputeManager();
        public MainWindow Window { get; set; }

        private double currentValue = 0; 
        private double beforeValue = 0; 
        private int decimalPointcount = 1; //소수인 경우 count
        private bool isDecimalPoint = false; //소수 이니?

        private LastOperator lastOperator = LastOperator.None; //숫자

        private bool isOperatorClicked = false; //연산자 클릭했니?
        private bool isNumberClicked = false; //숫자 클릭했니?
        private bool isResultClicked = false; //= 클릭했니?
        private bool isFunctionClicked = false; //function 기능 클릭했니?

        private bool isNull = false; //연산자용 null 체크
        public UIManager()
        {
            Trace.WriteLine("UIManager");
        }

        //사칙연산 버튼을 실행
        public void OperatorButtonClicked(string stringValue) 
        {
            if (!isOperatorClicked && !isNumberClicked)
                isOperatorClicked = true;

            if (stringValue == ".")
            {
                Trace.WriteLine($".");
                isDecimalPoint = true;
            }
            else if (stringValue == "/")
            {

            }
            else if (stringValue == "*")
            {

            }
            else if (stringValue == "-")
            {

            }
            else if (stringValue == "+")
            {
                isNull = true;
                if (isResultClicked && beforeValue == 0)
                {
                    beforeValue = currentValue;
                    Trace.WriteLine($"+ LastOperator.Equal : {beforeValue}");
                    Window.SetComputeText("+" + beforeValue.ToString("0.################"));
                }
                else if(isResultClicked && beforeValue != 0)
                {
                    Window.SetComputeText("+" + beforeValue.ToString("0.################"));
                }
                else
                {
                    //이전 값 가져오고, 연산자를 넣어라
                    currentValue = computeManager.Add(beforeValue, currentValue);
                    beforeValue = currentValue;
                    Trace.WriteLine($"+ decimalPointcount: {decimalPointcount}");
                    Window.SetComputeText("+" + currentValue.ToString());
                    Window.SetOutputText(currentValue.ToString());
                }

                Trace.WriteLine($"+ beforeValue: {beforeValue}");
                currentValue = 0;
                lastOperator = LastOperator.Plus;
                isDecimalPoint = false;
                decimalPointcount = 1;
            }
            else if(stringValue == "=")
            {
                
                if (lastOperator == LastOperator.Plus)
                {

                    if(isNull)
                    {
                        currentValue = beforeValue; 
                        Window.SetComputeText(beforeValue.ToString() + "+" + currentValue.ToString());
                        beforeValue = computeManager.Add(beforeValue, currentValue);                                                 //beforeValue = computeManager.Add(beforeValue, beforeValue); 
                        Window.SetOutputText(beforeValue.ToString());
                        isNull = false;
                    }
                    else if(isNumberClicked && Window.ClickedButton == Window.ResultButton)
                    {

                        Trace.WriteLine($"=2 beforeValue: {beforeValue}");
                        Trace.WriteLine($"=2 currentValue: {currentValue}");

                        Window.SetComputeText("=" + beforeValue.ToString() + "+" + currentValue.ToString());
                        beforeValue = computeManager.Add(beforeValue, currentValue);
                        Window.SetOutputText(beforeValue.ToString());

                    }
                    else
                    {
                        string result = beforeValue.ToString() + "+" + currentValue.ToString();
                        Window.SetComputeText("=" + result);
                        beforeValue = computeManager.Add(beforeValue, currentValue);
                        Window.SetOutputText(beforeValue.ToString());
                    }

                    
                }
                else if (lastOperator == LastOperator.Minus)
                    currentValue = computeManager.Subtract(beforeValue, currentValue);
                else if (lastOperator == LastOperator.Multiply)
                    currentValue = computeManager.Multiply(beforeValue, currentValue);
                else if (lastOperator == LastOperator.Division)
                    currentValue = computeManager.Divide(beforeValue, currentValue);
                else
                    Window.SetComputeText("="+currentValue.ToString("0.################"));

                isResultClicked = true;
                //lastOperator = LastOperator.Equal;
            }
            else if (stringValue == "+/-")
            {
                currentValue = currentValue * -1;
                Window.SetOutputText(currentValue.ToString());
            }
            else if (stringValue == "C")
            {
                Clear();
            }
            else if (stringValue == "CE")
            {
                
            }
            else if (stringValue == "Delete")
            {

            }

            Trace.WriteLine($"isOperatorClicked:{isOperatorClicked} ,isNumberClicked:{isNumberClicked}");
        }

        //숫자를 출력
        public void NumberButtonClicked(int number)
        {
            if (!isOperatorClicked && !isNumberClicked)
                isNumberClicked = true;

            isNull = false;
            if (isResultClicked && lastOperator != LastOperator.Plus)
            {
                //초기화 (중복 부분)
                Clear();
            }

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

            if (isDecimalPoint)
                currentValue = double.Parse(string.Format(stringformat, currentValue));

            Window.SetOutputText(currentValue.ToString("0.################")); //"0.################"

            Trace.WriteLine($"num currentValue: {currentValue}");
            Trace.WriteLine($"num beforeValue: {beforeValue}");
            Trace.WriteLine($"isOperatorClicked:{isOperatorClicked} ,isNumberClicked:{isNumberClicked}");
        }

        private void Clear()
        {
            isDecimalPoint = false;
            decimalPointcount = 1;
            isOperatorClicked = false;
            isNumberClicked = false;
            currentValue = 0;
            beforeValue = 0;
            Window.SetComputeText(string.Empty);
            Window.SetOutputText("0");
            lastOperator = LastOperator.None;
            isResultClicked = false;
            Window.ClickedButton = null;
            isNull = false;
        }
    }
}

public static class ExtensionClass
{
    public static double ToStiringFormat(double currnentValue, int decimalPointCount)
    {
        string stringformat = "{0:N" + (decimalPointCount - 1) + "}";
        return double.Parse(string.Format(stringformat, currnentValue));
    }
}