using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    //입력 관리 (0~9,.)
    public class InputManager
    {
        public UIManager uIManager { get; set; }

        public InputManager(UIManager uiManager)
        {
            Trace.WriteLine("InputManager");
            uIManager = uiManager;
        }

        public void NumberButtonClicked(int number)
        {
            if (number == 0)
                uIManager.IsfirstZeroClicked = true;

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

        public void DecimalPointButtonClicked(string demicalPointButton)
        {
            uIManager.IsDecimalPoint = true;

            uIManager.Window?.SetResultText(uIManager.CurrentValue + ".");
        }

    }
}
