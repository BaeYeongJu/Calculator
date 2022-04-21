using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    //입력 관련
    public class InputManager
    {
        public UIManager uIManager { get; set; }

        public InputManager()
        {
            Trace.WriteLine("InputManager");
        }

        public void NumberButtonClicked(int number)
        {

            //처음 숫자 클릭한 경우
            if (!uIManager.isOperatorClicked && !uIManager.isNumberClicked)
                uIManager.isNumberClicked = true;

            //+,= 연산자 둘다 사용시에
            if (uIManager.lastOperator == Operator.Plus && uIManager.isEqualClicked)
                uIManager.Clear();

            if (number == 0)
                uIManager.isfirstZeroClicked = true;

            //= 연산자 사용시에
            if (uIManager.isEqualClicked)
            {
                uIManager.currentValue = 0;
                uIManager.isEqualClicked = false;
            }

            if (!uIManager.isDecimalPoint)
            {
                uIManager.currentValue = uIManager.currentValue * 10 + number;
            }
            else
            {
                uIManager.decimalPointCount++;
                uIManager.currentValue = uIManager.currentValue + Math.Pow(10, -1 * uIManager.decimalPointCount) * number;
            }
            uIManager.Output();
            Trace.WriteLine($"num currentValue: {uIManager.currentValue} , num beforeValue: {uIManager.beforeValue}");
        }
    }
}
