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
        static string operandDescriptionWarning = "All Operands should only contain a leading + or -, and numbers.";
        static string operandAbsentWarning = "You did not enter an Operand in the calculation box.";
        static string operandInvalidFormatWarning = "The operand could not be converted to a valid number";

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
            // expect only calculation symbols */+-#=
            foreach (Char symbol in @"!@$%^&()_[]{};:'<>,.?`~")
            {
                if (calcText.Contains(symbol))
                {
                    button3_Click(this, null);
                    return;
                }
            }

            string calcOperand = calcText.Substring(0, calcText.Length - 1);
            string calcOperator = calcText.Substring(calcText.Length - 1);

            try
            {
                int intOperand = Convert.ToInt32(calcOperand);
            }
            catch (FormatException)
            {
                MessageBox.Show(operandInvalidFormatWarning, "Error");
                return;
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
    }
}
