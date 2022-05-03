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
            OnClickfirstNumber();

            //+,= 연산자 둘다 사용시에
            OnClickPlusWithEuqalOperator();

            if (number == 0)
                uIManager.IsfirstZeroClicked = true;

            //= 연산자 사용시에
            OnClickEqualOperator();

            if (!uIManager.IsDecimalPoint)
            {
                uIManager.CurrentValue = uIManager.CurrentValue * 10 + number;
            }
            else
            {
                uIManager.DecimalPointCount++;
                uIManager.CurrentValue = uIManager.CurrentValue + Math.Pow(10, -1 * uIManager.DecimalPointCount) * number;
            }
            uIManager.DisplayInputCurrnetValue();
            Trace.WriteLine($"num currentValue: {uIManager.CurrentValue} , num beforeValue: {uIManager.BeforeValue}");
        }

        private void OnClickfirstNumber()
        {
            if (!uIManager.IsOperatorClicked && !uIManager.IsNumberClicked)
                uIManager.IsNumberClicked = true;
        }

        private void OnClickPlusWithEuqalOperator()
        {
            if (uIManager.LastOperator == Operator.Plus && uIManager.IsEqualClicked)
                uIManager.Clear();
        }

        private void OnClickEqualOperator()
        {
            if (uIManager.IsEqualClicked)
            {
                uIManager.CurrentValue = 0;
                uIManager.IsEqualClicked = false;
            }
        }
    }
}
