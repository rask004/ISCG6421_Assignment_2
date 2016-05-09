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
                MessageBox.Show("You did not enter anything in the calculation box.", "Error");
                return;
            }
            // expect no letters
            else if (Regex.IsMatch(calcText, @"[a-zA-Z]"))
            {
                MessageBox.Show("All Operands should not contain letters.","Error");
                return;
            }
            // expect only calculation symbols */+-#=
            foreach (Char symbol in "!@$%^&()_[]{};:'<>,.?`~")
            {
                if (calcText.Contains(symbol))
                {
                    MessageBox.Show("All Operands should only use the symbols: +-/*#=", "Error");
                    return;
                }
            }

            string calcOperand = calcText.Substring(0, calcText.Length - 1);
            string calcOperator = calcText.Substring(calcText.Length - 1);

            if (calcOperand.Length == 0 || calcOperator.Length == 0)
            {
                MessageBox.Show("Absent Operator or Operand.", "Error");
                return;
            }

            bool operatorIsValidSymbol = false;
            foreach (Char symbol in "+-*/#=")
            {
                if (calcOperator.Equals(symbol))
                {
                    operatorIsValidSymbol = true;
                }
            }


            if (operatorIsValidSymbol)
            {
                MessageBox.Show("All Operands should only use the symbols: +-/*#=", "Error");
                return; 
            }

            try
            {
                int intOperand = Convert.ToInt32(calcOperand);
            }
            catch (FormatException)
            {
                MessageBox.Show("Operand was not a valid number.", "Error");
                return;
            }

            MessageBox.Show("Operand is: " + calcOperand + "; Operator is: " + calcOperator, "Notice");
        }
    }
}
