using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AddStrip.Calculations;

//TODO: introduce string validation of new calculation strings.
//TODO: introduce generating CalcLines (only debug at this stage).

/// <summary>
///     Addstrip Project (ISCG6421 Assignment 2)
/// </summary>
namespace AddStrip
{
    /// <summary>
    ///     Main user form for the AddStrip application
    /// </summary>
    public partial class frmAddStrip : Form
    {
        // Stores calc lines and manages displayed results.
        Calculation calculationManager;



        //TODO: remove this when testing finished.
        AddStrip.Testing.TestForm testForm;


        // Local Constants: UI messages
        public const string operandDescriptionWarning = "All Operands should have the form [+ or -]<numbers>." +
            "\r\nE.G: +10, -20, 5";
        public const string operandAbsentWarning = "You did not enter an Operand in the calculation box.";
        public const string operandInvalidFormatWarning = "The operand could not be converted to a valid number";
        public const string operandInvalidTotalWarning = "There are no calculations to total or subtotal.";
        public const string operatorInvalidTerminationWarning = "Invalid Termination symbol. Must be one of: " + 
            operatorTerminators +
            "\r\nE.G: +10+, +10-, +10*, +10/, +10#, +10=";

        // Local Constants: valid calculation symbols
        public const string operatorTerminators = "+-*/#=";
        public const string operatorTotals = "#=";
        public const string operandSigns = "+-";
        public const string operandDigits = "0123456789";

        /// <summary>
        ///     Constructor
        /// </summary>
        public frmAddStrip()
        {
            InitializeComponent();
            calculationManager = new Calculation(lstCalculations);
        }

        /// <summary>
        ///     Load additional objects. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddStripForm_Load(object sender, EventArgs e)
        {
            testForm = new AddStrip.Testing.TestForm();
            testForm.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddStripForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            testForm.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNewCalculation_TextChanged(object sender, EventArgs e)
        {
            // check that all chars are permitted chars.
            // if not, warn user.

            bool invalidCharsFound = false;

            string text = txtNextCalculation.Text;
            string calcText = "";

            // check for invalid chars, remove, 
            for (int i = 0; i < text.Length; i++)
            {
                if (!(operatorTerminators.Contains(text[i]) 
                    || operandDigits.Contains(text[i])))
                {
                    invalidCharsFound = true;
                }
                else
                {
                    calcText += text[i];
                }
            }
            txtNextCalculation.Text = calcText;
            text = calcText;
            calcText = "";

            if (invalidCharsFound)
            {
                MessageBox.Show(operandDescriptionWarning, "Error");
            }

            else if (text.Length > 1
                && operatorTerminators.Contains(text.Substring(text.Length - 1)))
            {


                // remove any whitespace - easier to process.
                foreach (char c in text)
                {
                    if (!Char.IsWhiteSpace(c))
                    {
                        calcText += c;
                    }
                }

                // separate operand and operator
                string calcOperand = calcText.Substring(0, calcText.Length - 1);
                string calcOperator = calcText.Substring(calcText.Length - 1);

                label3.Text = calcOperand;
                label4.Text = calcOperator;

                // (sub)total operators cannot be used if calculation list is empty, as
                // there are no calculations to total.
                if (operatorTotals.Contains(calcOperator) && lstCalculations.Items.Count == 0)
                {
                    MessageBox.Show(operandInvalidTotalWarning, "Error");
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateCalculation_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteCalculation_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsertCalculation_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSelectedCalculation_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
