using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Button1 click event handler
        private void Button1_Click(object sender, EventArgs e)
        {
            //FindPrimeValues(textBox1, listBox1); // Call FindPrimeValues method with TextBox1 and ListBox1
            Wait(); // Uncomment this line if you want to test threading
        }

        // Button2 click event handler
        private void Button2_Click(object sender, EventArgs e)
        {
            FindPrimeValues(textBox2, listBox2); // Call FindPrimeValues method with TextBox2 and ListBox2
        }

        // Method to find prime values
        private void FindPrimeValues(System.Windows.Forms.TextBox paramTextBox, ListBox paramListBox)
        {
            int number;
            // Check if the input is a positive integer
            if (!int.TryParse(paramTextBox.Text, out number) || number < 0)
            {
                MessageBox.Show("Please enter a positive integer.");
                return;
            }

            // Create a new thread to find prime numbers
            Thread primeThread = new Thread(() =>
            {
                // Find prime numbers up to the specified maximum number
                List<int> primes = PrimeNumberCalculator.FindPrimes(number);

                // Update the UI with the prime numbers found
                this.Invoke(new Action(() =>
                {
                    DisplayPrimes(primes, paramListBox);
                }));
            });
            primeThread.Start(); // Start the thread
        }

        // Method to display prime numbers in the ListBox
        private void DisplayPrimes(List<int> primes, ListBox listBox)
        {
            listBox.Items.Clear(); // Clear the ListBox
            foreach (int prime in primes)
            {
                listBox.Items.Add(prime); // Add each prime number to the ListBox
            }
        }

        public static void Wait()
        {
            // Create a new thread to execute DelayedWork method
            Thread workThread = new Thread(new ThreadStart(PrimeNumberCalculator.DelayedWork));

            // Start the thread
            workThread.Start();
        }
    }

    

    // Class to calculate prime numbers
    public class PrimeNumberCalculator
    {
        // Method to simulate delayed work
        public static void DelayedWork()
        {
            // Simulate a delay of 10 seconds
            Thread.Sleep(10000);
        }

        // Method to find prime numbers up to the specified maximum number
        public static List<int> FindPrimes(int maxNumber)
        {
            List<int> primes = new List<int>();
            // Loop through numbers from 2 to maxNumber
            for (int i = 2; i <= maxNumber; i++)
            {
                // Check if the current number is prime
                if (IsPrime(i))
                {
                    primes.Add(i); // Add the prime number to the list
                }
            }
            return primes; // Return the list of prime numbers
        }

        // Method to check if a number is prime
        private static bool IsPrime(int number)
        {
            if (number <= 1)
                return false;
            if (number == 2)
                return true;
            if (number % 2 == 0)
                return false;

            int boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }
    }
}
