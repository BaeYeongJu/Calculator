using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    //출력 관리
    public class OutputManager
    {
        public UIManager UiManager { get; set; }

        public OutputManager(UIManager uiManager)
        {
            Trace.WriteLine("OutputManager");
            UiManager = uiManager;
        }

        public void DisplayInputCurrnetValue() //현재 입력한 값 출력
        {
            Trace.WriteLine($"DisplayInputCurrnetValue decimalPointCount: {UiManager.DecimalPointCount}");
            UiManager.Window?.SetResultText(string.Format("{0:N" + (UiManager.DecimalPointCount) + "}", UiManager.CurrentValue));
        }

        public void DisplayCurrentCalculatedValue() //현재 계산된 값 출력
        {
            Trace.WriteLine($"DisplayCurrentCalculatedValue: {UiManager.BeforeValue.ToString(UiManager.OutputFormat)}");
            UiManager.Window?.SetResultText(UiManager.BeforeValue.ToString(UiManager.OutputFormat));
        }

        public void DisplayZeroValue() //0인 경우 출력
        {
            UiManager.Window?.SetResultText("정의되지 않는 결과입니다.");
        }

        public void DisplayNegate() //0인 경우, +/- 클릭시 출력
        {
            UiManager.Window?.SetCalculatedText("negate");
        }

        //사칙연산 클릭후, 사칙연산 출력 + 현재 값 출력
        public void DisplayOperatorAndValue(string operatorValue, double currentValue)
        {
            Trace.WriteLine($"DisplayOperatorAndValue operatorValue: {operatorValue}, currentValue: {currentValue.ToString(UiManager.OutputFormat)}");
            UiManager.Window?.SetCalculatedText(currentValue.ToString(UiManager.OutputFormat) + operatorValue);
        }

        //= 클릭 후, = 출력 & 현재 값 출력
        public void DisplayOperatorWithResultAndValue(string lastOperator, string operatorValue, double beforeValue, double currentValue)
        {
            Trace.WriteLine($"DisplayOperatorWithResultAndValue operatorValue: {operatorValue}, lastMark: {lastOperator}");
            UiManager.Window?.SetCalculatedText(beforeValue.ToString(UiManager.OutputFormat) + lastOperator + currentValue.ToString(UiManager.OutputFormat) + operatorValue);
        }

        public void DisplayDecimalPointValue(double value)
        {
            UiManager.Window?.SetResultText(value + ".");
        }
    }
}
