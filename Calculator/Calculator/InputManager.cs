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
        public UIManager UiManager { get; set; }

        public InputManager(UIManager uiManager)
        {
            Trace.WriteLine("InputManager");
            UiManager = uiManager;
        }

        public void InputButtonClicked(string value)
        {
            int number = 0;

            if (value != ".")
                number = int.Parse(value);
            
            if (number == 0)
                UiManager.IsfirstZeroClicked = true;

            if (value == ".")
            {
                UiManager.IsDecimalPoint = true;
                UiManager.Window?.SetResultText(UiManager.CurrentValue + ".");
                return;
            }
                
            if (!UiManager.IsDecimalPoint)
            {
                UiManager.CurrentValue = UiManager.CurrentValue * 10 + number;
            }
            else
            {
                UiManager.DecimalPointCount++;
                UiManager.CurrentValue = UiManager.CurrentValue + Math.Pow(10, -1 * UiManager.DecimalPointCount) * number;
                Trace.WriteLine($"Math pow: {Math.Pow(10, -1 * UiManager.DecimalPointCount)}");
            }

            if (value != ".")
                UiManager.DisplayInputCurrnetValue();

            Trace.WriteLine($"2 InputButtonClicked currentValue: {UiManager.CurrentValue}");
        }

    }
}
