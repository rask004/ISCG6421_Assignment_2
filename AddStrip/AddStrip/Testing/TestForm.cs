using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

//TODO: figure out why error checks are failing.

namespace AddStrip.Testing
{
    public partial class TestForm : Form
    {
        static string operandDescriptionWarning = "All Operands should have the form [+ or -]<numbers>." +
            "\r\nE.G: +10, -20, 5";
        static string operandAbsentWarning = "You did not enter an Operand in the calculation box.";
        static string operandInvalidFormatWarning = "The operand could not be converted to a valid number";
        static string operandInvalidTotalWarning = "There are no calculations to total or subtotal.";
        static string operatorInvalidTerminationWarning = "Invalid Termination symbol. Must be one of: " + terminators +
            "\r\nE.G: +10+, +10-, +10*, +10/, +10#, +10=";

        const string terminators = "+-*/#=";
        const string signs = "+-";


        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            string calcText = "";

            // remove any whitespace - easier to process.
            foreach (char c in text)
            {
                if (!Char.IsWhiteSpace(c))
                {
                    calcText += c;
                }
            }

            // calculation text box is empty.
            if (calcText == "")
            {
                button2_Click(this, null);
                return;
            }
            // expect no letters
            else if (Regex.IsMatch(calcText, @"[a-zA-Z]"))
            {
                button3_Click(this, null);
                return;
            }
            // expect only calculation symbols */+-#= and signs +-
            foreach (Char symbol in @"!@$%^&()_[]{};:'<>,.?`~")
            {
                if (calcText.Contains(symbol))
                {
                    button3_Click(this, null);
                    return;
                }
            }

            string calcOperand = calcText.Substring(0, calcText.Length - 1);
            string calcOperator = calcText.Substring(calcText.Length - 1, 1);

            // must terminate with one of -+/*#=, or for first calculation -+/*
            if (!terminators.Contains(calcOperator))
            {
                button7_Click(this, null);
            }

            // for first calculation, would check if listbox is empty, then check if terminator is
            // # or = for error.
            // don't do this for Testform.

            try
            {
                int intOperand = Convert.ToInt32(calcOperand);
            }
            catch (FormatException)
            {
                button5_Click(this, null);
            }

            MessageBox.Show("Operand is: " + calcOperand + "; Operator is: " + calcOperator, "Notice");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(operandAbsentWarning, "Error");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(operandDescriptionWarning, "Error");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(operandInvalidFormatWarning, "Error");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(operandInvalidTotalWarning, "Error");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show(operatorInvalidTerminationWarning, "Error");
        }
    }
}
