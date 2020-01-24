/*Post Build Instructions
 *
 * Copy the built DLL files to: C:\Program Files\Corepoint Health\Custom Objects
 * Open a command prompt as administator
 * Change directory to the Custom Objects folder
 * Run the following command to register the UserDefinedFunction DLL:
 * %systemroot%\Microsoft.NET\Framework64\v4.0.30319\regasm UserDefinedFunctions.dll /tlb /nologo
 */

using CorepointHealth.Common.Interfaces.Gear.ItemInvoke;
using Newtonsoft.Json;
using System;
using System.Runtime.InteropServices;

namespace Corepoint.Plugin
{
    // This GUID is required for a Com Interop application

    [Guid("F0D20274-4717-4AD0-B1D3-8F71EBCA9F9F")]
    public class HtmlConverter : IInvoke
    {
        /// <summary>
        /// This function is the entry point for the ItemInvoke action in Corepoint.
        /// </summary>
        /// <param name="sourceOperand">The value of the "Source Operand" field.</param>
        /// <param name="options">The value of the "Object Options" field.</param>
        /// <param name="destinationOperand">The value that will be returned in the "Destination Operand" variable.</param>
        /// <param name="status">The value that will be returned in the "Status" variable</param>
        public void Invoke(ref string sourceOperand, ref string options, ref string destinationOperand, ref string status)
        {
            
            try
            {
                // Deserialize the JSON and make it an Object
                Function function = JsonConvert.DeserializeObject<Function>(sourceOperand);

                // Converts the provided text to Markdown text.
                HtmlToMarkdown.ConvertToMarkdown(function, out destinationOperand, out status);
            }
            catch (ArgumentException exception)
            {
                status = exception.ToString();
                
            }

            return;
        }

        [ComRegisterFunction]
        public static void RegisterFunction(Type t)
        {
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(
              "CLSID\\" + t.GUID.ToString("B") + "\\Implemented Categories\\{740B2584-57D6-46CB-A85D-B2D255115A97}");

            if (key != null)
            {
                key.Close();
            }
        }

        [ComUnregisterFunction]
        public static void UnregisterFunction(Type t)
        {
            Microsoft.Win32.Registry.ClassesRoot.DeleteSubKey(
              "CLSID\\" + t.GUID.ToString("B") + "\\Implemented Categories\\{740B2584-57D6-46CB-A85D-B2D255115A97}", false);
        }
    }
}