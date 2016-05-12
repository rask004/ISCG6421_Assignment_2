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
using System.Collections.ObjectModel;

//TODO: complete menu items
//TODO: complete calculation methods for saving and loading
//TODO: complete logic for update, insert validation

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
        private Calculation calculationManager;

        // the last file that calculations were saved to.
        private string saveFilename;


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
        // More flexible than using a textbox mask - check specific chars, not groups of chars.
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
            saveFilename = null;
            calculationManager = new Calculation(lstCalculations);
        }

        /// <summary>
        ///     Load additional objects. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddStripForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddStripForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            calculationManager.Clear();
            txtSelectedCalculation.Text = "";
            txtNextCalculation.Text = "";
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
            if (saveFilename == null)
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Calculations should be saved here.", "Notice");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {


            MessageBox.Show("Calculations should be saved as here.", "Notice");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Calculations should be printed here.", "Notice");
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
        ///     Verify calculation lines entered into the new calculation textbox.
        ///     
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNextCalculation_TextChanged(object sender, EventArgs e)
        {
            string text = txtNextCalculation.Text;
            string calcText = "";                       // to contain calculation value
            string terminator = null;                     // to contain termination operator
            string warning = "";

            // Start Validation of txtNextCalculation.Text

            // do nothing if txtNextCalculation is cleared.
            if (text.Length == 0)
            {
                return;
            }

            // first symbol must be sign or digit
            if (text.Length >= 1)
            {
                // special case: a Calcline was just added and the textbox has been updated
                // with the last operator symbol, if the symbol was not a (sub)total
                if (text.Length == 1
                    && operatorTerminators.Contains(text[0])
                    && !operatorTotals.Contains(text[0]))
                {
                    return;
                }
                else if (!operandSigns.Contains(text[0])
                    && !operandDigits.Contains(text[0]))
                {
                    warning = "The first character must be +, -, or a digit.\r\n" 
                        + operandDescriptionWarning;
                }
                else
                {
                    calcText += text[0];
                }
            }
            if (text.Length >= 2)
            {
                // first two symbols cannot be sign + terminator
                // must be sign + digit, or digit + terminator
                if (text.Length == 2 && operandSigns.Contains(text[0]) &&
                        operatorTerminators.Contains(text[1]))
                {
                    warning = "The calculation contains no digits.\r\n"
                        + operandDescriptionWarning;
                    
                }
                else
                {
                    // check for non digits, remove
                    for (int i = 1; i < text.Length - 1; i++)
                    {
                        if (!operandDigits.Contains(text[i]))
                        {
                            warning = "All characters between the first and the last must be a digit.\r\n"
                                + operandDescriptionWarning;
                        }
                        else
                        {
                            calcText += text[i];
                        }
                    }

                    // last character may be a digit.
                    if (operandDigits.Contains(text[text.Length - 1]))
                    {
                        calcText += text[text.Length - 1];
                    }

                    // or last character may be a terminating operation.
                    else if (operatorTerminators.Contains(text[text.Length - 1]))
                    {
                        terminator = text[text.Length - 1].ToString();
                    }

                    // any other last symbol is invalid
                    else
                    {
                        warning = "Last character must be a digit or one of the terminators: " + operatorTerminators + "\r\n"
                                + "Examples for using terminators: +10+, +10-, +10*, +10/, +10#, +10=";
                    }
                }
            }

            // special case: user requested (sub)total and there are no calculations to total
            if (lstCalculations.Items.Count == 0
                && operatorTotals.Contains(text[text.Length - 1]))
            {
                // prevent overwriting of warning is already assigned.
                if (!warning.Equals("")) 
                {
                    warning = operandInvalidTotalWarning;
                }
            }

            if (warning.Length > 0)
            {
                // remove invalid chars from textbox contents.
                txtNextCalculation.Text = calcText;
                txtNextCalculation.SelectionStart = txtNextCalculation.Text.Length;
                txtNextCalculation.SelectionLength = 0;
                tip.Show(warning, txtNextCalculation, 10, -80, 2000);
                return;
            }

            // End of Validation

            // Start of Calc Line Processing

            // only process calculations into Calc Lines if there is a terminating Char.
            if (terminator != null)
            {
                // Create CalcLine object here
                string calcString = terminator.ToString() + calcText;
                CalcLine newCalcLine = new CalcLine(calcString);
                calculationManager.Add(newCalcLine);

                // if terminating operator is not a (sub)total, change textbox to only show the operator
                // otherwise clear the textbox.
                tip.Hide(txtNextCalculation);
                if (!operatorTotals.Contains(terminator))
                {
                    txtNextCalculation.Text = terminator.ToString();
                    txtNextCalculation.Select(0, txtNextCalculation.Text.Length);
                }
                else
                {
                    txtNextCalculation.Text = "";
                }
            }

            // End of Calc Line Processing


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateCalculation_Click(object sender, EventArgs e)
        {
            // verify calc line, generate calcline and update

            calculationManager.Replace(new CalcLine(Operator.plus), lstCalculations.SelectedIndex);

            txtSelectedCalculation.Text = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteCalculation_Click(object sender, EventArgs e)
        {
            try
            {
                calculationManager.Delete(lstCalculations.SelectedIndex);
            }
            catch (IndexOutOfRangeException)
            {
                tip.Show("Either there are no calculation lines to delete,\r\nor the line you selected could not be found.", 
                    txtSelectedCalculation, 10, -40, 2000);
            }

            txtSelectedCalculation.Text = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsertCalculation_Click(object sender, EventArgs e)
        {
            try
            {
                // verify calc line, generate calcline and insert

                if (lstCalculations.Items.Count == 0)
                {
                    calculationManager.Insert(new CalcLine(Operator.plus), 0);
                }
                else
                {
                    calculationManager.Insert(new CalcLine(Operator.plus), lstCalculations.SelectedIndex);
                }
                
            }
            catch (IndexOutOfRangeException)
            {
                
                tip.Show("Either there are no calculation lines to delete,\r\nor the line you selected could not be found.",
                    txtSelectedCalculation, 10, -40, 2000);
            }

            txtSelectedCalculation.Text = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNextCalculation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtNextCalculation.Text.Length == 0)
            {
                tip.Show("Please enter a calculation in the text box.\r\n" +
                    operandDescriptionWarning, txtNextCalculation, 10, -80, 2000);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstCalculations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCalculations.SelectedIndex < 0)
            {
                lstCalculations.SelectedIndex = 0;
            }
            else if (lstCalculations.SelectedIndex > lstCalculations.Items.Count)
            {
                lstCalculations.SelectedIndex = lstCalculations.Items.Count;
            }

            try
            {
                txtSelectedCalculation.Text
                        = calculationManager.Find(lstCalculations.SelectedIndex).ToString();
            }
            catch (IndexOutOfRangeException)
            {

                tip.Show("The calculation line selected could not be found.\r\n Try selecting another calculation line.",
                    txtSelectedCalculation, 10, -40, 2000);
            }
        }
    }
}
