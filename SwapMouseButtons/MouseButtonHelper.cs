using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace SwapMouseButtons
{
    public static class MouseButtonHelper
    {
        [DllImport("user32.dll")]
        private static extern Int32 SwapMouseButton(Int32 bSwap);

        public static void SwapMouseButtons()
        {
            if (GetCurrentPrimaryButton() == MouseButton.Right)
            {
                SetAsPrimaryMouseButton(MouseButton.Left);
            }
            else
            {
                SetAsPrimaryMouseButton(MouseButton.Right);
            }
        }

        public static MouseButton GetCurrentPrimaryButton()
        {
            int currentMouseButton = GetRegistryValueFromInt("Control Panel\\Mouse\\", "SwapMouseButtons");
            return Parse(currentMouseButton);
        }

        public static MouseButton Parse(int value)
        {
            return (MouseButton)value;
        }

        public static void SetAsPrimaryMouseButton(MouseButton newPrimaryButton)
        {
            if (!IsValidButton(newPrimaryButton))
            {
                Console.WriteLine("Invalid button - aborting");
                return;
            }

            // Make the change take effect immidiately.
            SwapMouseButton((int)newPrimaryButton);

            // Store the change in registry so it persists past logout
            PersistInRegistry((int)newPrimaryButton);
        }

        private static bool IsValidButton(MouseButton button)
        {
            return button == MouseButton.Left || button == MouseButton.Right;
        }

        private static void PersistInRegistry(int mouseButton)
        {
            try
            {
                SetRegistryValueFromInt("Control Panel\\Mouse\\", "SwapMouseButtons", mouseButton.ToString(), RegistryValueKind.String);
            }
            catch (Exception ex)
            {
#if DEBUG
                throw ex;
#else 
                Console.WriteLine("Error: " + ex.Message);
#endif
            }
        }

        private static void SetRegistryValueFromInt(string pathToSubKey, string registryValue, object value, RegistryValueKind valueKind)
        {
            var key = Registry.CurrentUser.CreateSubKey(pathToSubKey);
            var oldValue = key.GetValue(registryValue.ToString());

            if (oldValue == null)
            {
                throw new KeyNotFoundException("No key named " + registryValue + " was found at " + pathToSubKey + ", aborting.");
            }

            key.SetValue(registryValue, value, valueKind);
        }

        private static int GetRegistryValueFromInt(string pathToSubKey, string registryValue)
        {
            var key = Registry.CurrentUser.CreateSubKey(pathToSubKey);
            var oldValue = key.GetValue(registryValue.ToString());

            if (oldValue == null)
            {
                throw new KeyNotFoundException("No key named " + registryValue + " was found at " + pathToSubKey + ", aborting.");
            }

            int outValue;
            bool success = Int32.TryParse(oldValue.ToString(), out outValue);

            if (!success)
            {
                throw new InvalidCastException("Could not cast current registry value to int");
            }

            return outValue;
        }
    }
}
