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
            AddStrip.Testing.TestForm testForm = new AddStrip.Testing.TestForm(this);
            testForm.Show();
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

        }


        // TODO: remove these handlers after completing test creation.

        private void lstCalculationsMOCK_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCalculations.SelectedIndex < 0)
            {
                // indicates no items are present in listbox.
                return;
            }
            else if (lstCalculations.SelectedIndex > lstCalculations.Items.Count)
            {
                lstCalculations.SelectedIndex = lstCalculations.Items.Count;
            }

            txtSelectedCalculation.Text
                        = lstCalculations.Items[lstCalculations.SelectedIndex].ToString();
        }

        private void btnUpdateCalculationMOCK_Click(object sender, EventArgs e)
        {

            lstCalculations.Items[lstCalculations.SelectedIndex]
                = txtSelectedCalculation.Text;

            txtSelectedCalculation.Text = "";
        }

        private void btnDeleteCalculationMOCK_Click(object sender, EventArgs e)
        {
            
            lstCalculations.Items.RemoveAt(lstCalculations.SelectedIndex);

            txtSelectedCalculation.Text = "";
        }

        private void btnInsertCalculationMOCK_Click(object sender, EventArgs e)
        {
            lstCalculations.Items.Insert(lstCalculations.SelectedIndex,
                txtSelectedCalculation.Text);

            txtSelectedCalculation.Text = "";
        }

        private void txtNextCalculationMOCK_TextChanged(object sender, EventArgs e)
        {
            if (txtNextCalculation.Text.Length >= 3
                && operatorTerminators.Contains(
                    txtNextCalculation.Text[txtNextCalculation.Text.Length - 1]))
            {
                var oldOperator = txtNextCalculation.Text[0];
                var operand = txtNextCalculation.Text.Substring(1, 
                    txtNextCalculation.Text.Length - 2);
                var newOperator = txtNextCalculation.Text
                    .Substring(txtNextCalculation.Text.Length - 1);
                lstCalculations.Items.Add(oldOperator.ToString() + " " + operand);
                txtNextCalculation.Text = newOperator;
                txtNextCalculation.Select(txtNextCalculation.Text.Length, 0);
            }
        }
    }
}
