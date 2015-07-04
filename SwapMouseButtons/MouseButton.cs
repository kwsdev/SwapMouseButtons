using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwapMouseButtons
{
    // The reason we're using this instead of the built-in MouseButton enum is that the 
    // int values in this one corresponds to the int values the registry expects
    public enum MouseButton
    {
        Left = 0,
        Right = 1
    }
}
