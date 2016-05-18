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
        public const string messageSaveFileSuccess = "Your changes have successfully been saved.";
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
        /// <param name="sender">Furrent form</param>
        /// <param name="e"></param>
        private void AddStripForm_Load(object sender, EventArgs e)
        {
            calculationManager.Clear();
            changesHaveBeenMade = false;
        }

        /// <summary>
        ///     When closing, ask to save changes, and save changes as requested
        ///     if changesHaveBeenMade is true.
        /// </summary>
        /// <param name="sender">current form</param>
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
        ///     If requested, save changes first
        ///     Then clear the form of calculations.
        /// </summary>
        /// <param name="sender">either the Form or the New ToolStrip Item</param>
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
        ///     Open a calculations file and load the calculations.
        ///     give opportunity to save changes first, and save as requested.
        ///     If loading fails, warn the user and start a new session.
        /// </summary>
        /// <param name="sender">current form ot the toolstrip item</param>
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
                    else
                    {
                        throw ex;
                    }
                }

                changesHaveBeenMade = false;
            }            
        }
        
        /// <summary>
        ///     Save the current set of calculations.
        ///     If no filename is stored, defer to the Save As method.
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

                    MessageBox.Show(messageSaveFileSuccess, "Success");
                }
                catch (Exception ex)
                {
                    if (ex is NotSupportedException ||
                        ex is IOException)
                    {
                        MessageBox.Show(messageFileParseError, "Error");
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }
        }

        /// <summary>
        ///     Save the current set of calculations, with the option to choose
        ///     a filename. 
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

                    MessageBox.Show(messageSaveFileSuccess, "Success");
                }
                catch (Exception ex)
                {
                    if (ex is NotSupportedException ||
                        ex is IOException)
                    {
                        MessageBox.Show(messageFileParseError, "Error");
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }
        }

        /// <summary>
        ///     Print the calculations.
        ///     Show a preview window first.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Calculations should be printed here.", "Notice");
        }

        /// <summary>
        ///     Exit the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     Verify calculation lines entered into the new calculation textbox.
        ///     Then determine if a calculation has been typed.
        ///     If so, try to add it to the current set of calculations.
        ///     If not possible (because it violates the calculation rules), warn user
        ///     also if a calculation is added, note that changes have been made.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNextCalculation_TextChanged(object sender, EventArgs e)
        {
            // skip all processing if there is no text to process.
            if (txtNextCalculation.Text.Length == 0)
            {
                return;
            }
            
            var calctext = "";              // use calctext as temporary store

            // firstly remove all invalid chars.
            // valid ones are stored in calctext.
            // helps manage copy and pastes as well as changes by keypress.
            if (txtNextCalculation.Text.Length > 0 )
            {
                bool invalidCharsFound = false;

                // check for invalid chars first, remove all invalid chars, 
                // if invalid char found, warn user, return
                for (int i = 0; i < txtNextCalculation.Text.Length; i++)
                {
                    if (!operandDigits.Contains(txtNextCalculation.Text[i])
                        && !operatorTerminators.Contains(txtNextCalculation.Text[i]))
                    {
                        // don't store invalid chars.
                        invalidCharsFound = true;
                    }
                    else
                    {
                        calctext += txtNextCalculation.Text[i].ToString();
                    }
                }

                if (invalidCharsFound)
                {
                    showToolTipMessageNearNextCalculationTextbox(
                        "Only the characters \"0123456789#=+-*/\" are allowed.");
                }
            }

            // now, process the remaining text, for valid calc lines.
            // possible when removing invalid chars, there is no text left, so skip if so.
            // otherwise, check calcttext

            // no text to process
            if (calctext.Length == 0)
            {
                // don't process if there is no text to process
            }
            else
            {
                // this is start of a new calculation.
                // when the next calculation would be the start of a new set of calculations...
                if ((lstCalculations.Items.Count == 0 ||
                    calculationManager.Find(lstCalculations.Items.Count - 1).Op
                    == Operator.total) && !operandDigits.Contains(calctext[0]))
                {
                    // ... first char is not digit, raise an Error
                    showToolTipMessageNearNextCalculationTextbox(
                        "This is the start of a new Calculation. \r\n" +
                        "Your first Calc Line must begin with a digit.\r\n" +
                        "Format: <numbers><one of " + operatorTerminators + ">");
                    calctext = "";
                    
                }

                // Calc Line is for only a subtotal.
                // if we are dealing with a starting calcline and it passed the above branch,
                // it will skip this branch.
                else if (operatorSubTotal.Equals(calctext[0].ToString()))
                {
                    // subtotal Calc Lines must have a previous calc line to subtotal from.
                    // cannot have multiple subtotal calclines in a row.

                    // assumed this is not the start of a new set of calculations.
                    // as a starting calcline must lead with a digit, the previous branch
                    // should prevent this issue.

                    if (calculationManager.Find(lstCalculations.Items.Count - 1).Op
                    == Operator.subtotal)
                    {
                        //... cannot have multiple subtotals in a row.
                        showToolTipMessageNearNextCalculationTextbox(
                            "The previous Calc Line is a subtotal. \r\n" +
                            "You cannot have multiple subtotals in a row.");
                    }
                    else
                    {
                        // create a subtotal calcline.
                        calculationManager.Add(new CalcLine(Operator.subtotal));
                    }

                    calctext = "";
                }

                // CalcLine is for a total
                // if we are dealing with a starting calcline and it passed the above branch,
                // it will skip this branch.
                else if (operatorFullTotal.Equals(calctext[0].ToString()))
                {
                    // assumed this is not the start of a new set of calculations.
                    // as a starting calcline must lead with a digit, the previous branch
                    // should prevent this issue.

                    // create a subtotal calcline.
                    calculationManager.Add(new CalcLine(Operator.total));
                    calctext = "";

                }

                // A terminating char (see terminatingOperators) has been pressed.
                // If was the first char of the first calcline in a new calculation,
                //   or the first char of a following calcline and a subtotal char,
                //   above branches would have already dealt with this.
                else if (operatorTerminators.Contains(calctext[calctext.Length - 1]) && calctext.Length > 1)
                {

                    // break the text into the lead operator, and the string of numbers
                    // also break out the terminating operator, as the last char
                    char leadOperator;
                    StringBuilder numString =
                        new StringBuilder(calctext.Substring(0, calctext.Length - 1));
                    char terminatingChar = calctext[calctext.Length - 1];

                    // if no leading Operator, assume it is a plus.
                    if (operandDigits.Contains(numString[0]))
                    {
                        leadOperator = '+';
                    }
                    else
                    {
                        leadOperator = numString[0];
                        numString.Remove(0, 1);
                    }

                    // check the number is a valid number
                    // if not, issue an error
                    try
                    {
                        Convert.ToDouble(numString.ToString());
                        // numString is valid Number
                        // add the calculation
                        calculationManager.Add(new CalcLine(leadOperator + " " + numString));

                        // if terminating char is not subtotal or total, the textbox should 
                        // be changed to show only it
                        // otherwise if it is a total or subtotal, add a (sub)total calcline
                        // and clear the textbox
                        if (terminatingChar.ToString() == operatorSubTotal)
                        {
                            calculationManager.Add(new CalcLine(Operator.subtotal));
                            calctext = "";
                        }
                        else if (terminatingChar.ToString() == operatorFullTotal)
                        {
                            calculationManager.Add(new CalcLine(Operator.total));
                            calctext = "";
                        }
                        else
                        {
                            calctext = terminatingChar.ToString();
                        }


                    }
                    catch (FormatException)
                    {
                        showToolTipMessageNearNextCalculationTextbox(
                            "The Calculation noes not contain a valid number. \r\n" +
                            "Format: [One of " + operatorCalculations + "]<digits><One of "
                            + operatorTerminators + ">");
                    }
                }
            }

            // update the textbox with the new text
            // this may trigger an event recursion.
            // the new text is unlikely to trigger more recursions.
            txtNextCalculation.Text = calctext;
            txtNextCalculation.Select(txtNextCalculation.Text.Length, 0);
            changesHaveBeenMade = true;

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

            // calculation can be "#" or "="
            if (calcParts.Length == 1 
                && operatorTotals.Contains(calcParts[0]))
            {
                isValid = true;
            }
            
            // calculation can be "<operator> <digits>"
            if (calcParts.Length == 2
                && operatorCalculations.Contains(calcParts[0]))
            {
                try
                {
                    Convert.ToDouble(calcParts[1]);
                    isValid = true;
                }
                catch (FormatException)
                {
                    // calculation not valid
                    isValid = false;
                }
                
            }
            
            return isValid;
        }

        /// <summary>
        ///     Update a selected calculation line.
        ///     Update according to the calculation rules
        ///     If rules are violated, warn the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateCalculation_Click(object sender, EventArgs e)
        {
            if (lstCalculations.SelectedIndex == -1 )
            {
                showToolTipMessageNearNextCalculationTextbox(
                    "Please first select a calculation line to Update.");
            }
            else if (!selectedCalculationIsValid(txtSelectedCalculation.Text))
            {
                showToolTipMessageNearNextCalculationTextbox(
                    "The calculation you entered was not valid.\r\n" +
                    "A valid calculation has the form \"<operator>  <digits>\"");
            }
            // first calcline in any set of calculations must start with - or +
            else if ((lstCalculations.SelectedIndex == 0
                || lstCalculations.Items.Count > 1 &&
                lstCalculations.SelectedIndex > 0 &&
                calculationManager.Find(lstCalculations.SelectedIndex - 1).Op
                == Operator.total)
                && ( !operandSigns.Contains(txtSelectedCalculation.Text[0])
                ))
            {
                showToolTipMessageNearNextCalculationTextbox(
                    "The first calc line in any calculation must have a + or - operator.");
            }

            // cannot put a subtotal after another subtotal or before another subtotal
            else if (lstCalculations.Items.Count > 1 
                && operatorSubTotal.Contains(txtSelectedCalculation.Text[0])
                && ((lstCalculations.SelectedIndex - 1 > -1
                        && calculationManager.Find(lstCalculations.SelectedIndex - 1).Op
                        == Operator.subtotal)) 
                    || (lstCalculations.SelectedIndex + 1 < lstCalculations.Items.Count
                        && calculationManager.Find(lstCalculations.SelectedIndex + 1).Op
                        == Operator.subtotal)
                )
            {
                showToolTipMessageNearNextCalculationTextbox(
                    "You cannot place multiple subtotals in a row.\r\n" +
                    "Check your calculations and where you are placing the subtotal.");
            }

            // cannot place a total before another total.
            else if (lstCalculations.Items.Count > 1
                && operatorFullTotal.Contains(txtSelectedCalculation.Text[0])
                && ((lstCalculations.SelectedIndex + 1 < lstCalculations.Items.Count
                        && calculationManager.Find(lstCalculations.SelectedIndex + 1).Op
                        == Operator.total)
                ))
            {
                showToolTipMessageNearNextCalculationTextbox(
                    "You cannot place a total before an existing total.\r\n" +
                    "Check your calculations and where you are placing the subtotal.");
            }

            else
            {
                calculationManager.Replace(
                    new CalcLine(txtSelectedCalculation.Text),
                    lstCalculations.SelectedIndex);
            }
        }

        /// <summary>
        ///     Delete a selected calculation line.
        ///     Update according to the calculation rules
        ///     If rules are violated, warn the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteCalculation_Click(object sender, EventArgs e)
        {
            if (lstCalculations.SelectedIndex < 0)
            {
                showToolTipMessageNearNextCalculationTextbox(
                    "Please first select a calculation line to Delete.");
            }
            else
            {
                calculationManager.Delete(lstCalculations.SelectedIndex);
                txtSelectedCalculation.Text = "";
            }
        }

        /// <summary>
        ///     Insert a selected calculation line.
        ///     Update according to the calculation rules
        ///     If rules are violated, warn the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsertCalculation_Click(object sender, EventArgs e)
        {
            
            if (lstCalculations.Items.Count == 0)
            {
                showToolTipMessageNearNextCalculationTextbox(
                    "There are no calculations to insert above.\r\n");
            }
            else if (lstCalculations.SelectedIndex == -1)
            {
                showToolTipMessageNearNextCalculationTextbox(
                    "Select a calculation to insert above.");
            }
            else if (!selectedCalculationIsValid(lstCalculations.Items[
                    lstCalculations.SelectedIndex].ToString()))
            {
                showToolTipMessageNearNextCalculationTextbox(
                    "The calculation you entered was not valid.\r\n" +
                    "A valid calculation has the form \"<operator>  <digits>\"");
            }



            // first calcline in any set of calculations must start with - or +
            else if ((lstCalculations.SelectedIndex == 0
                || lstCalculations.Items.Count > 1 &&
                lstCalculations.SelectedIndex > 0 &&
                calculationManager.Find(lstCalculations.SelectedIndex - 1).Op
                == Operator.total)
                && (!operandSigns.Contains(txtSelectedCalculation.Text[0])
                ))
            {
                showToolTipMessageNearNextCalculationTextbox(
                    "The first calc line in any calculation must have a + or - operator.");
            }

            // cannot put a subtotal after another subtotal or before another subtotal
            else if (lstCalculations.Items.Count > 1
                && operatorSubTotal.Contains(txtSelectedCalculation.Text[0])
                && ((lstCalculations.SelectedIndex - 1 > -1
                        && calculationManager.Find(lstCalculations.SelectedIndex - 1).Op
                        == Operator.subtotal))
                    || (calculationManager.Find(lstCalculations.SelectedIndex).Op
                        == Operator.subtotal)
                )
            {
                showToolTipMessageNearNextCalculationTextbox(
                    "You cannot place multiple subtotals in a row.\r\n" +
                    "Check your calculations and where you are placing the subtotal.");
            }

            // cannot insert a total before another total or after another total.
            else if (lstCalculations.Items.Count > 1
                && operatorFullTotal.Contains(txtSelectedCalculation.Text[0])
                && ((lstCalculations.SelectedIndex - 1 > -1
                        && calculationManager.Find(lstCalculations.SelectedIndex - 1).Op
                        == Operator.total))
                    || (calculationManager.Find(lstCalculations.SelectedIndex).Op
                        == Operator.total)
                )
            {
                showToolTipMessageNearNextCalculationTextbox(
                    "You cannot place a total before or after an existing total.\r\n" +
                    "Check your calculations and where you are placing this total.");
            }



            else
            {
                calculationManager.Insert(
                    new CalcLine(txtSelectedCalculation.Text),
                    lstCalculations.SelectedIndex);
            }
        }

        /// <summary>
        ///     When choosing a new calculation line, load the text into the editing box
        ///     load from the calculation manager, not the display control.
        /// </summary>
        /// <param name="sender">the indexable control displaying the calculation set.</param>
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
        ///     If enter is pressed and the add calculation textbox is empty,
        ///     send notice to user to enter a calculation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNextCalculation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtNextCalculation.Text.Length == 0)
            {
                showToolTipMessageNearNextCalculationTextbox(
                    "Please enter a calculation.");

            }
        }

        /// <summary>
        ///     helper method for tool tips.
        ///     places tooltip in same location each time.
        /// </summary>
        /// <param name="message"></param>
        private void showToolTipMessageNearNextCalculationTextbox(string message)
        {
            tip.Show(message, txtNextCalculation,
                    -10, 25, 3000);
        }

    }
}
