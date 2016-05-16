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
using System.IO;

//TODO: complete print menu item

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

        // keep track of changes.
        private Boolean changesHaveBeenMade;

        // file and directory constants
        const string calculationFileExtension = "cal";
        const string calculationSaveDirectoryDefault = @"C:\temp";

        // file content constants
        const string fileLineSeparator = "\r\n";
        const string fileFieldHeader = "~AddStripCalculationLineFile";

        // Local Constants (UI messages)
        public const string messageOperandDescriptionWarning = "All Calculation line should have the form <operation>[+ or -]<numbers>." +
            "\r\n" + @"E.G: +10, -+20, \5, -3, *-2, \+6";
        public const string messageOperandAbsentWarning = "You did not enter a Calculation line in the calculation box.";
        public const string messageOperandInvalidFormatWarning = "The operand could not be converted to a valid number";
        public const string messageOperandInvalidTotalWarning = "There are no calculations to total or subtotal.";
        public const string messageOperatorInvalidTerminationWarning = "Invalid Termination symbol. Must be one of: " + 
            operatorTerminators;
        public const string messageSaveChanges = "Do you wish to save your changes?";
        public const string messageOpenFileNullError = "The specified file could not be opened.\r\n" + 
            "Check the file actually exists and is a calculation file.";
        public const string messageFileParseError = "Could not parse the selected file." +
                                "\r\nCheck the file has the correct format." +
                                "\r\nall files have the format:" +
                                "\r\n" + fileFieldHeader +
                                "\r\n" + "<calcLineString>" +
                                "\r\n" + "...";
        public const string messageSafeFileSuccess = "Your changes have successfully been saved.";
        public const string messageReadCalcLinesDiscardedWarning =
            "Calculation file was parsed but some Calculation Lines" +
            "\r\nCould not be parsed and will be missing. Please Check" +
            "\r\nyour loaded calculations. ";
        public const string messageNoCalculationsToSaveNotice = 
            "There are no Calculations to save.";

        // Local Constants (valid calculation symbols)
        // More flexible than using a textbox mask - check specific chars, not fixed groups of chars.
        public const string operatorTerminators = operatorCalculations + operatorTotals;
        public const string operatorCalculations = "+-*/";
        public const string operatorTotals = operatorSubTotal + operatorFullTotal;
        public const string operatorSubTotal = "#";
        public const string operatorFullTotal = "=";
        public const string operandSigns = "+-";
        public const string operandDigits = "0123456789";

        /// <summary>
        ///     Constructor
        /// </summary>
        public frmAddStrip()
        {
            InitializeComponent();
            saveFilename = null;
            changesHaveBeenMade = false;
            calculationManager = new Calculation(lstCalculations);
        }

        /// <summary>
        ///     Load additional objects. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddStripForm_Load(object sender, EventArgs e)
        {
            calculationManager.Clear();
            changesHaveBeenMade = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddStripForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changesHaveBeenMade &&
                MessageBox.Show(messageSaveChanges,
                "Changes have been made", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
            {
                saveToolStripMenuItem_Click(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (changesHaveBeenMade &&
                MessageBox.Show(messageSaveChanges,
                "Changes have been made", MessageBoxButtons.YesNo) 
                == DialogResult.Yes)
            {
                saveToolStripMenuItem_Click(sender, e);
            }
            calculationManager.Clear();
            txtSelectedCalculation.Text = "";
            txtNextCalculation.Text = "";
            saveFilename = null;
            changesHaveBeenMade = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (changesHaveBeenMade &&
                MessageBox.Show(messageSaveChanges,
                "Changes have been made", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
            {
                saveToolStripMenuItem_Click(sender, e);
            }

            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "calculation files (*." + calculationFileExtension + ")" +
                "|*." + calculationFileExtension;
            openDialog.FilterIndex = 0;
            openDialog.InitialDirectory = calculationSaveDirectoryDefault;
            openDialog.RestoreDirectory = false;

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // if file cannot be parsed, an exception will be raised.
                    calculationManager.LoadFromFile(openDialog.FileName);
                    saveFilename = openDialog.FileName;
                }
                catch (Exception ex)
                {
                    if (ex is FormatException || ex is IOException)
                    {
                        MessageBox.Show(messageFileParseError, "Error");
                        calculationManager.Clear();
                        txtSelectedCalculation.Text = "";
                        txtNextCalculation.Text = "";
                        saveFilename = null;
                    }
                }

                changesHaveBeenMade = false;
            }            
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstCalculations.Items.Count == 0)
            {
                MessageBox.Show(messageNoCalculationsToSaveNotice, "Notice");
                return;
            }

            if (saveFilename == null)
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            else
            {
                try
                {
                    calculationManager.SaveToFile(saveFilename);

                    changesHaveBeenMade = false;

                    MessageBox.Show(messageSafeFileSuccess, "Success");
                }
                catch (Exception ex)
                {
                    if (ex is NotSupportedException ||
                        ex is IOException)
                    {
                        MessageBox.Show(messageFileParseError, "Error");
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstCalculations.Items.Count == 0)
            {
                MessageBox.Show(messageNoCalculationsToSaveNotice, "Notice");
                return;
            }

            SaveFileDialog saveAsDialog = new SaveFileDialog();

            saveAsDialog.Filter = "calculation files (*." + calculationFileExtension + ")" +
                "|*." + calculationFileExtension;
            saveAsDialog.FilterIndex = 0;
            saveAsDialog.InitialDirectory = calculationSaveDirectoryDefault;
            saveAsDialog.RestoreDirectory = false;

            if (lstCalculations.Items.Count == 0)
            {
                MessageBox.Show(messageNoCalculationsToSaveNotice, "Notice");
            }

            if (saveAsDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    calculationManager.SaveToFile(saveAsDialog.FileName);
                    saveFilename = saveAsDialog.FileName;

                    changesHaveBeenMade = false;

                    MessageBox.Show(messageSafeFileSuccess, "Success");
                }
                catch (Exception ex)
                {
                    if (ex is NotSupportedException ||
                        ex is IOException)
                    {
                        MessageBox.Show(messageFileParseError, "Error");
                    }
                }
            }
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
            var text = txtNextCalculation.Text;
            var calctext = "";
            bool invalidCharFound = false;

            if (text.Length > 0 )
            {
                // check for invalid chars first, remove all invalid chars, 
                // if invalid char found, warn user, return
                for (int i = 0; i < text.Length; i++)
                {
                    if (!operandDigits.Contains(txtNextCalculation.Text[i])
                        && !operatorTerminators.Contains(txtNextCalculation.Text[i]))
                    {
                        invalidCharFound = true;
                    }
                    else
                    {
                        calctext += text[i].ToString();
                    }
                }

                if (invalidCharFound)
                {
                    tip.Show("Invalid Character.\r\n" + messageOperandDescriptionWarning,
                            txtNextCalculation, 10, -80, 2500);
                }
            }
            
        }

        /// <summary>
        ///     Verify a calculation typed into the editing box.
        ///     calculation line should be of form "operator operand".
        ///     e.g. * 10, / -5, + 20, - -4.
        /// </summary>
        private bool selectedCalculationIsValid(string calculation)
        {
            bool isValid = false;

            string[] calcParts = calculation.Split(new char[] { ' ' }, 2);
            if (calcParts.Length == 2 && operatorTerminators.Contains(calcParts[0]))
            {
                try
                {
                    Convert.ToDouble(calcParts[1]);
                    isValid = true;
                }
                catch (FormatException)
                {
                    // calculation not valid
                }
                
            }
            
            return isValid;
        }

        /// <summary>
        ///     Update a selected calculation line.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateCalculation_Click(object sender, EventArgs e)
        {
            if (lstCalculations.SelectedIndex < 0 ||
                !selectedCalculationIsValid(lstCalculations.Items[
                    lstCalculations.SelectedIndex].ToString()))
            {
                tip.Show("Please first select a calculation line to Update.", txtSelectedCalculation,
                    10, -40, 2500);
            }
            else
            {
                calculationManager.Replace(
                    new CalcLine(lstCalculations.Items[lstCalculations.SelectedIndex].ToString()), 
                    lstCalculations.SelectedIndex);
            }
        }

        /// <summary>
        ///     Delete a selected calculation line.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteCalculation_Click(object sender, EventArgs e)
        {
            if (lstCalculations.SelectedIndex < 0)
            {
                tip.Show("Please first select a calculation line to Delete.", 
                    txtSelectedCalculation,
                    10, -40, 2500);
            }
            else
            {
                calculationManager.Delete(lstCalculations.SelectedIndex);
                txtSelectedCalculation.Text = "";
            }
        }

        /// <summary>
        ///     Insert a selected calculation line.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsertCalculation_Click(object sender, EventArgs e)
        {
            if (lstCalculations.SelectedIndex < 0 ||
                !selectedCalculationIsValid(lstCalculations.Items[
                    lstCalculations.SelectedIndex].ToString()))
            {
                tip.Show("Please first select a calculation line to Update.", txtSelectedCalculation,
                    10, -40, 2500);
            }
            else
            {
                calculationManager.Insert(
                    new CalcLine(lstCalculations.Items[lstCalculations.SelectedIndex].ToString()),
                    lstCalculations.SelectedIndex);
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
                // indicates no items are present in listbox.
                return;
            }
            else if (lstCalculations.SelectedIndex > lstCalculations.Items.Count)
            {
                lstCalculations.SelectedIndex = lstCalculations.Items.Count;
            }

            try
            {
                txtSelectedCalculation.Text = calculationManager.Find(
                    lstCalculations.SelectedIndex).ToString();
            }
            catch (ArgumentOutOfRangeException)
            {
                // This can occur upon deleting or inserting, which may unselect any
                // selected items in the listbox.
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNextCalculation_KeyPress(object sender, KeyPressEventArgs e)
        {
            // if key pressed is operator or subtotal, check content
            if (operatorTerminators.Contains(e.KeyChar))
            {
                // if length of 1 and # or =, test if (sub)total is allowed
                if (txtNextCalculation.Text.Length == 1
                    && operatorTotals.Contains(e.KeyChar))
                {
                    // not allowed if no calc lines to total, or last calcline is same
                    // type of total
                    if (lstCalculations.Items.Count == 0 
                        || operatorTotals.Contains((lstCalculations.Items
                        [lstCalculations.Items.Count - 1] as string)
                        .ToString()[0]))
                    {
                        // raise warning
                        tip.Show("Either there are no calculations to total, or the previous\r\n" +
                            "calculation is the same as the subtotal or total just entered.", 
                            txtNextCalculation,
                            10, -40, 2500);
                    }
                    else
                    {
                        //   generate calcline if allowed, and clear textbox.
                        if (lstCalculations.Items
                            [lstCalculations.Items.Count - 1].ToString() == operatorFullTotal)
                        {
                            calculationManager.Add(new CalcLine(Operator.total));
                            txtNextCalculation.Text = "";
                        }
                        else if (lstCalculations.Items
                            [lstCalculations.Items.Count - 1].ToString() == operatorSubTotal)
                        {
                            calculationManager.Add(new CalcLine(Operator.subtotal));
                            txtNextCalculation.Text = "";
                        }
                    }
                }

                StringBuilder calcNumber = new StringBuilder();

                // if length > 1, check if calc line is valid
                if (txtNextCalculation.Text.Length > 1)
                {
                    // if first char is digit, append it
                    if (operandDigits.Contains( txtNextCalculation.Text[0]))
                    {
                        calcNumber.Append(txtNextCalculation.Text[0]);
                    }

                    // append all remaining chars, except last if not digit
                    // assume chars are digits - this will be tested shortly
                    for (int i = 1; i < txtNextCalculation.Text.Length - 1; i++)
                    {
                        calcNumber.Append(txtNextCalculation.Text[i]);
                    }

                    // if last char is digit, append it
                    if (operandDigits.Contains(
                        txtNextCalculation.Text[txtNextCalculation.Text.Length - 1]))
                    {
                        calcNumber.Append(txtNextCalculation.Text[
                            txtNextCalculation.Text.Length - 1]);
                    }
                }

                // convert to double.
                try
                {
                    Convert.ToDouble(calcNumber);
                    tip.Show("This calculation contains an error. Check that the calculation matches\r\n" +
                        "The format <operator><number><operator>, or <number><operator> if this is\r\n" +
                        "the start of a new set of calculations.",
                            txtNextCalculation,
                            10, -40, 2500);

                }
                catch (FormatException)
                {
                    // if conversion failed, raise warning

                    return;
                }

                // if last char is terminating operator, generate the calcline(s)
                if (operatorTerminators.Contains( 
                    txtNextCalculation.Text[txtNextCalculation.Text.Length - 1]) )
                {
                    // pull out the leading operator char and the terminating char.
                    // number already exists as calcNumber
                    char leadingOperator;
                    char terminatingOperator;

                    terminatingOperator = 
                        txtNextCalculation.Text[txtNextCalculation.Text.Length - 1];
 
                    if (operandDigits.Contains(txtNextCalculation.Text[0]))
                    {
                        leadingOperator = '+';
                    }
                    else
                    {
                        leadingOperator = txtNextCalculation.Text[0];
                    }

                    calculationManager.Add(
                        new CalcLine(leadingOperator + " " + calcNumber));

                    // if last char is = or #, also generate (sub)total calcline
                    //   and clear textbox
                    if (terminatingOperator.ToString() == operatorFullTotal)
                    {
                        calculationManager.Add(new CalcLine(Operator.total));
                        txtNextCalculation.Text = "";
                    }
                    else if (terminatingOperator.ToString() == operatorSubTotal)
                    {
                        calculationManager.Add(new CalcLine(Operator.subtotal));
                        txtNextCalculation.Text = "";
                    }
                    // if last char is not = or #, replace textbox contents with char
                    else
                    {
                        txtNextCalculation.Text = terminatingOperator.ToString();
                    }
                }



            }
        }
    }
}
