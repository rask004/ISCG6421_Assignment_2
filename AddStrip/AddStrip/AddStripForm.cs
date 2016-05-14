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

//TODO: complete menu items
//TODO: complete calculation methods for saving and loading

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

        private Boolean changesHaveBeenMade;

        const string calculationFileExtension = "cal";
        const string calculationSaveDirectoryDefault = @"C:\temp";

        const string fileLineSeparator = "\r\n";
        const string fileFieldHeader = "~AddStripCalculationLineFile";

        // Local Constants: UI messages
        public const string messageOperandDescriptionWarning = "All Calculation line should have the form <operation>[+ or -]<numbers>." +
            @"\r\nE.G: +10, -+20, \5, -3, *-2, \+6";
        public const string messageOperandAbsentWarning = "You did not enter a Calculation line in the calculation box.";
        public const string messageOperandInvalidFormatWarning = "The operand could not be converted to a valid number";
        public const string messageOperandInvalidTotalWarning = "There are no calculations to total or subtotal.";
        public const string messageOperatorInvalidTerminationWarning = "Invalid Termination symbol. Must be one of: " + 
            operatorTerminators;
        public const string messageSaveChanges = "Do you wish to save your changes?";
        public const string messageOpenFileNullError = "The specified file could not be opened.\r\n" + 
            "Check the file actually exists and is a calculation file.";
        public const string messageOpenFileParseError = "Could not parse the selected file." +
                                "\r\nCheck the file has the correct format." +
                                "\r\nall files have the format:" +
                                "\r\n" + fileFieldHeader +
                                "\r\n" + "<calcLineString>" +
                                "\r\n" + "...";
        public const string messageSafeFileSuccess = "Your changes have successfully been saved.";
        // Local Constants: valid calculation symbols
        // More flexible than using a textbox mask - check specific chars, not groups of chars.
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

            Stream openStream;
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "calculation files (*." + calculationFileExtension + ")" +
                "|*." + calculationFileExtension;
            openDialog.FilterIndex = 0;
            openDialog.InitialDirectory = calculationSaveDirectoryDefault;
            openDialog.RestoreDirectory = false;

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                using (openStream = openDialog.OpenFile())
                {
                    // if file selected, parse and load if valid
                    if (openStream != null)
                    {

                        string[] calcLines = ReadFromCalculationFile(openStream);
                        
                        // if file invalid, warn user and load new state.
                        if (calcLines == null)
                        {
                            MessageBox.Show(messageOpenFileParseError, "Error");
                            calculationManager.Clear();
                            txtSelectedCalculation.Text = "";
                            txtNextCalculation.Text = "";
                        }
                        else
                        {
                            // Code to generate the CalcLines here.
                            calculationManager.Clear();
                            foreach (string cl in calcLines)
                            {
                                calculationManager.Add(new CalcLine(cl));
                            }
                        }

                        changesHaveBeenMade = false;
                    }
                    else
                    {
                        MessageBox.Show(messageOpenFileNullError, "Error");
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="readStream"></param>
        /// <returns></returns>
        private string[] ReadFromCalculationFile(Stream readStream)
        {
            List<string> calcStrings = new List<string>();

            // read from stream.

            // if first line is not calc field header, reject steam.

            // read each calc line
            // discard invalid lines

            // process calc line into correct format
            // add formatted calc line to calcStrings

            return calcStrings.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="readStream"></param>
        /// <param name="calcStrings"></param>
        /// <returns></returns>
        private bool WriteToCalculationFile(Stream writeStream, string[] calcStrings)
        {
            bool successful = false;

            using (writeStream)
            {
                // write calculation field header

                // for each calcStrings string, write the string, in order given.
                // if string is not valid, discard it.
            }

            return successful;
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
                Stream saveStream = new FileStream(saveFilename, FileMode.Create);

                // Code to convert the calc lines to text.
                List<string> calcStrings = new List<string>();

                // Code to write the stream goes here.
                WriteToCalculationFile(saveStream, calcStrings.ToArray());

                saveStream.Close();

                changesHaveBeenMade = false;

                MessageBox.Show(messageSafeFileSuccess, "Success");
                        
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream saveStream;
            SaveFileDialog saveAsDialog = new SaveFileDialog();

            saveAsDialog.Filter = "calculation files (*." + calculationFileExtension + ")" +
                "|*." + calculationFileExtension;
            saveAsDialog.FilterIndex = 0;
            saveAsDialog.InitialDirectory = calculationSaveDirectoryDefault;
            saveAsDialog.RestoreDirectory = false;

            if (saveAsDialog.ShowDialog() == DialogResult.OK)
            {
                saveStream = new FileStream(saveFilename, FileMode.Create);

                // Code to convert the calc lines to text.
                List<string> calcStrings = new List<string>();

                // Code to write the stream goes here.
                WriteToCalculationFile(saveStream, calcStrings.ToArray());

                saveStream.Close();

                changesHaveBeenMade = false;

                MessageBox.Show(messageSafeFileSuccess, "Success");
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
            var calcText = "";

            string errorMessage = "";

            // check for invalid chars first, remove all invalid chars, 
            // if invalid char found, warn user, return
            for (int i = 0; i < text.Length; i++)
            {
                if (!operandDigits.Contains( text[i]) 
                    && !operatorTerminators.Contains(text[i]))
                {
                    errorMessage = messageOperandDescriptionWarning;
                }
                else
                {
                    calcText += text[i].ToString();
                }
            }

            if (errorMessage != "")
            {
                // omit any terminating chars.
                if (operatorTerminators.Contains(calcText[calcText.Length - 1]))
                {
                    calcText = calcText.Substring(0, calcText.Length - 1);
                }

                txtNextCalculation.Text = calcText;
                tip.Show(errorMessage, txtNextCalculation, 10,
                    -80, 2500);
                return;
            }

            if (calcText.Length == 0)
            {
                txtNextCalculation.Text = calcText;
                return;
            }

            // otherwise if terminating char has been given, verify this is a calculation
            // calculation is at least 3 chars long: <operator><operand><terminating char>

            // length=1, check is <operator>
            if (calcText.Length >= 1)
            {
                if (!operatorTerminators.Contains(calcText[0]))
                {
                    tip.Show("First symbol must be one of " + operatorTerminators +
                        "\r\n" + messageOperandDescriptionWarning, txtNextCalculation, 10,
                        -80, 2500);
                    txtNextCalculation.Text = calcText;
                    return;
                }
            }

            // length=2, check is <operator><operand sign> or <operator><operand digit>
            if (calcText.Length >= 2)
            {
                if (!(operandSigns.Contains(calcText[1]) || operandDigits.Contains(calcText[1])))
                {
                    tip.Show("Second symbol must be an operand sign (" + operandSigns + ") or a digit" +
                        "\r\n" + messageOperandDescriptionWarning, txtNextCalculation, 10,
                        -80, 2500);
                    txtNextCalculation.Text = calcText;
                    return;
                }
                
            }

            // length>2, check all chars except last are digits.
            // verify last is terminating char or digit.
            // then if last is terminating char, process the calculation line.
            if (calcText.Length >= 3)
            {
                for (int i = 2; i < calcText.Length - 1; i++)
                {
                    if (!operandDigits.Contains(calcText[i]))
                    {
                        tip.Show("Symbols after the leading operation (and any sign) must be digits" +
                            "\r\n" + messageOperandDescriptionWarning, txtNextCalculation, 10,
                            -80, 2500);
                        txtNextCalculation.Text = calcText;
                        return;
                    }
                }

                if (!operatorTerminators.Contains(calcText[calcText.Length - 1])
                    && !operandDigits.Contains(calcText[calcText.Length - 1]))
                {
                    tip.Show("The last symbol must be a digit or one of " + operatorTerminators +
                        "\r\n" + messageOperandDescriptionWarning, txtNextCalculation, 10,
                        -80, 2500);
                    txtNextCalculation.Text = calcText;
                    return;
                }
                else if (operatorTerminators.Contains(calcText[calcText.Length - 1]))
                {
                    // MOSTLY verified calculation, and there is a terminating char.

                    // special case: lstBox is empty or last calculation line is =
                    if (lstCalculations.Items.Count == 0 
                        || (lstCalculations.Items[
                            lstCalculations.Items.Count - 1]).ToString().Substring(0,1) == 
                                operatorFullTotal)
                    {
                        // ...and <operator> is not + or -
                        if (!operandSigns.Contains(calcText[0]))
                        {
                            // warn user this is invalid as is start of new calculation
                            // (+ or -) are required.
                            tip.Show(
                                "For a new calculation, the first line must have a + or - operator." +
                                "\r\n" + messageOperandDescriptionWarning, txtNextCalculation, 10,
                                -80, 3500);
                            txtNextCalculation.Text = calcText;
                            return;
                        }

                        // ...and terminating char is # or =
                        else if (operatorTotals.Contains(calcText[calcText.Length - 1]))
                        {
                            // warn user this is invalid as there are no calculations to sum.
                            tip.Show(
                                "For a new calculation, terminating char cannot be a # or = total because there are no lines to sum." +
                                "\r\n" + messageOperandDescriptionWarning, txtNextCalculation, 10,
                                -80, 3500);
                            txtNextCalculation.Text = calcText;
                            return;
                        }
                    }

                    // pull out each part.
                    var oldOperator = txtNextCalculation.Text[0].ToString();
                    var operand = Convert.ToDouble(txtNextCalculation.Text.Substring(1,
                        txtNextCalculation.Text.Length - 2));
                    var newOperator = txtNextCalculation.Text
                        .Substring(txtNextCalculation.Text.Length - 1);

                    MessageBox.Show("oldOperator = " + oldOperator + "\r\n" +
                        "operand = " + operand + "\r\n" +
                        "newOperator = " + newOperator);

                    // add the new calculation line.
                    calculationManager.Add(new CalcLine(oldOperator + " " + operand));

                    // if terminating char is a totalling operator, add (sub)total calc line
                    if (operatorTotals.Contains(newOperator))
                    {
                        switch (newOperator)
                        {
                            case operatorSubTotal:
                                calculationManager.Add(new CalcLine(Operator.subtotal));
                                newOperator = "";
                                break;

                            case operatorFullTotal:
                                calculationManager.Add(new CalcLine(Operator.total));
                                newOperator = "";
                                break;
                        }
                        
                    }
                    
                    txtNextCalculation.Text = newOperator;
                    txtNextCalculation.Select(txtNextCalculation.Text.Length, 0);
                }

                // if reached here, then we have <operation><operand> without terminating char.
                // don't process contents, leave alone.

            }
        }

        /// <summary>
        ///     Verify a calculation typed into the editing box.
        ///     calculation line should be of form "operator operand".
        ///     e.g. * 10, / -5, + 20, - -4.
        /// </summary>
        private bool SelectedCalculationIsValid(string calculation)
        {
            bool isValid = false;

            string[] calcParts = calculation.Split(new char[] { ' ' });
            if (calcParts.Length >= 2)
            {
                if (operatorTerminators.Contains(calcParts[0]))
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
                !SelectedCalculationIsValid(lstCalculations.Items[
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
                !SelectedCalculationIsValid(lstCalculations.Items[
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
        private void txtNextCalculation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtNextCalculation.Text.Length == 0)
            {
                tip.Show("Please enter a calculation in the text box.\r\n" +
                    messageOperandDescriptionWarning, txtNextCalculation, 10, -80, 2000);
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


        // TODO: remove these handlers after completing test creation.

        

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
