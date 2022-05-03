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
        public UIManager uIManager { get; set; }

        public OutputManager()
        {
            Trace.WriteLine("OutputManager");
        }

        public void DisplayInputCurrnetValue() //현재 입력한 값 출력
        {
            Trace.WriteLine($"DisplayInputCurrnetValue decimalPointCount: {uIManager.DecimalPointCount}");
            uIManager.Window?.SetResultText(string.Format("{0:N" + (uIManager.DecimalPointCount) + "}", uIManager.CurrentValue));
        }

        public void DisplayCurrentCalculatedValue() //현재 계산된 값 출력
        {
            Trace.WriteLine($"DisplayCurrentCalculatedValue: {uIManager.BeforeValue.ToString(uIManager.OutputFormat)}");
            uIManager.Window?.SetResultText(uIManager.BeforeValue.ToString(uIManager.OutputFormat));
        }

        public void DisplayZeroValue() //0인 경우
        {
            uIManager.Window?.SetResultText("0으로 나눌 수 없습니다");
        }

        //사칙연산 클릭후, 사칙연산 출력 + 현재 값 출력
        public void DisplayOperatorAndValue(string operatorValue, double currentValue)
        {
            Trace.WriteLine($"DisplayOperatorAndValue operatorValue: {operatorValue}, currentValue: {currentValue.ToString(uIManager.OutputFormat)}");
            uIManager.Window?.SetCalculatedText(currentValue.ToString(uIManager.OutputFormat) + operatorValue);
        }

        //= 클릭 후, = 출력 & 현재 값 출력
        public void DisplayOperatorWithResultAndValue(string lastOperator, string operatorValue, double beforeValue, double currentValue)
        {
            Trace.WriteLine($"DisplayOperatorWithResultAndValue operatorValue: {operatorValue}, lastMark: {lastOperator}");
            uIManager.Window?.SetCalculatedText(beforeValue.ToString(uIManager.OutputFormat) + lastOperator + currentValue.ToString(uIManager.OutputFormat) + operatorValue);
        }
    }
}
