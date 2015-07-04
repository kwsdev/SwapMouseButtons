using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SwapMouseButtons
{
    class Program
    {
        static void Main(string[] args)
        {
            int outValue;
            if (args == null || args.Length == 0 || args[0] == null || !Int32.TryParse(args[0], out outValue))
            {
                Console.WriteLine("No or invalid arguments, swapping mouse butons.");
                MouseButtonHelper.SwapMouseButtons();
            }
            else
            {
                MouseButton button = MouseButtonHelper.Parse(outValue);
                Console.WriteLine("Setting MouseButton." + button.ToString() + " as new primary button.");
                MouseButtonHelper.SetAsPrimaryMouseButton(button);
            }
        }
    }
}
