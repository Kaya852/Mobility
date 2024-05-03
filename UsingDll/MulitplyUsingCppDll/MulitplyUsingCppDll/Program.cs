using System;
using System.Runtime.InteropServices;

namespace MulitplyUsingCppDll
{

    internal class Program
    {        
        // Importing functions from the C++ DLL
        [DllImport(@"C:\Users\KAYA\source\repos\ProductDll\x64\Debug\ProductDll.dll")]
        public static extern IntPtr Create();
        [DllImport(@"C:\Users\KAYA\source\repos\ProductDll\x64\Debug\ProductDll.dll")]
        public static extern int multiply(IntPtr a, int x, int y);
        static void Main(string[] args)
        {
            // Creating a pointer to the object in the DLL
            IntPtr a = Create();
            string input = "";

            while (input != "exit")
            {
                Console.WriteLine("Enter two integers separated by space (Example: 3 4) or type 'exit' to close the program:");
                input = Console.ReadLine();

                // Splitting the user input based on spaces
                string[] inputs = input.Split(' ');

                // Check if the input is 'exit'
                if (input.ToLower() == "exit")
                {
                    Console.WriteLine("Exiting the program...");
                    break;
                }

                // Converting inputs to integers
                if (inputs.Length == 2 && int.TryParse(inputs[0], out int x) && int.TryParse(inputs[1], out int y))
                {
                    // Calling the C++ DLL function
                    int result = multiply(a, x, y);

                    Console.WriteLine($"Product: {result}");
                }
                else
                {
                    Console.WriteLine("Invalid input! Expected two integers.");
                }
            }

            Console.WriteLine("Program ended.");
        }
    }
}
