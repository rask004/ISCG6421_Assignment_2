﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AddStrip.Calculations;
using System.IO;
using System.Drawing.Printing;

namespace AddStrip
{
    /// <summary>
    ///     Main user form for the AddStrip application
    /// </summary>
    public partial class FrmAddStrip : Form
    {
        // Stores calc lines and manages displayed results.
        private readonly Calculation calculationManager;

        // the last file that calculations were saved to.
        private string saveFilename;

        // keep track of changes.
        private bool changesHaveBeenMade;

        // store string data for printing
        private List<string> printLines;

        public static readonly Font PrintFont = new Font("Arial", 12);
        public string MessageSuccessTitle;

        // file and directory constants
        public const string CalculationFileExtension = "cal";
        public const string CalculationFileExtensionExtended = 
            "calculation files (*." + CalculationFileExtension + ") | *." + CalculationFileExtension;
        public const string CalculationSaveDirectoryDefault = @"C:\temp";

        // file content constants
        public const string FileLineSeparator = "\r\n";
        public const string FileFieldHeader = "~AddStripCalculationLineFile";

        // Local Constants (UI messages)
        public const string MessageOperandDescriptionWarning = "All Calculation line should have the form <operation>[+ or -]<numbers>." +
            "\r\n" + @"E.G: +10, -+20, \5, -3, *-2, \+6";
        public const string MessageOperandAbsentWarning = "You did not enter a Calculation line in the calculation box.";
        public const string MessageOperandInvalidFormatWarning = "The operand could not be converted to a valid number";
        public const string MessageOperandInvalidTotalWarning = "There are no calculations to total or subtotal.";
        public const string MessageOperatorInvalidTerminationWarning = "Invalid Termination symbol. Must be one of: " + 
            OperatorTerminators;
        public const string MessageSaveChanges = "Do you wish to save your changes?";
        public const string MessageOpenFileNullError = "The specified file could not be opened.\r\n" + 
            "Check the file actually exists and is a calculation file.";
        public const string MessageFileParseError = "Could not parse the selected file." +
                                "\r\nCheck the file has the correct format." +
                                "\r\nall files have the format:" +
                                "\r\n" + FileFieldHeader +
                                "\r\n" + "<calcLineString>" +
                                "\r\n" + "...";
        public const string MessageEditingConsecutiveTotalsError = "You cannot place a total before or after an existing total.\r\n" +
                                                                   "Check your calculations and where you are placing this total.";

        public const string MessageSaveFileSuccess = "Your changes have successfully been saved.";
        public const string MessageReadCalcLinesDiscardedWarning =
            "Calculation file was parsed but some Calculation Lines" +
            "\r\nCould not be parsed and will be missing. Please Check" +
            "\r\nyour loaded calculations. ";
        public const string MessageNoCalculationsToSaveNotice = 
            "There are no Calculations to save.";
        public const string MessageEditingInvalidCalculationEnteredError = "The calculation you entered was not valid.\r\n" +
                                                                      "A valid calculation has the form \"<operator>  <digits>\"";
        public const string MessageInitialCalcLineMissingSignError = "The first calc line in any calculation must have a + or - operator.";
        public const string MessageEditingPlaceConsecutiveSubtotalsError = "You cannot place multiple subtotals in a row.\r\n" +
                                                                           "Check your calculations and where you are placing the subtotal.";

        public const string MessageSaveChangesTitle = "Changes have been made";
        public const string MessageNoticeTitle = "Notice";
        public const string MessageErrorTitle = "Error";

        // Local Constants (valid calculation symbols)
        // More flexible than using a textbox mask - check specific chars, not fixed groups of chars.
        public const string OperatorTerminators = OperatorCalculations + OperatorTotals;
        public const string OperatorCalculations = "+-*/";
        public const string OperatorTotals = OperatorSubTotal + OperatorFullTotal;
        public const string OperatorSubTotal = "#";
        public const string OperatorFullTotal = "=";
        public const string OperandSigns = "+-";
        public const string OperandDigits = "0123456789";

        public const string IndicatorTotalText = "<<";

        

        /// <summary>
        ///     Constructor
        /// </summary>
        public FrmAddStrip()
        {
            InitializeComponent();
            saveFilename = null;
            changesHaveBeenMade = false;
            calculationManager = new Calculation(lstCalculations);
            MessageSuccessTitle = "Success";
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
                MessageBox.Show(MessageSaveChanges,
                MessageSaveChangesTitle, MessageBoxButtons.YesNo)
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
                MessageBox.Show(MessageSaveChanges,
                MessageSaveChangesTitle, MessageBoxButtons.YesNo) 
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
                MessageBox.Show(MessageSaveChanges,
                MessageSaveChangesTitle, MessageBoxButtons.YesNo)
                == DialogResult.Yes)
            {
                saveToolStripMenuItem_Click(sender, e);
            }

            var openDialog = new OpenFileDialog();
            openDialog.Filter = CalculationFileExtensionExtended;
            openDialog.FilterIndex = 0;
            openDialog.InitialDirectory = CalculationSaveDirectoryDefault;
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
                        MessageBox.Show(MessageFileParseError, MessageErrorTitle);
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
            if (calculationManager.Count == 0)
            {
                MessageBox.Show(MessageNoCalculationsToSaveNotice, MessageNoticeTitle);
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

                    MessageBox.Show(MessageSaveFileSuccess, MessageSuccessTitle);
                }
                catch (Exception ex)
                {
                    if (ex is NotSupportedException ||
                        ex is IOException)
                    {
                        MessageBox.Show(MessageFileParseError, MessageErrorTitle);
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
            if (calculationManager.Count == 0)
            {
                MessageBox.Show(MessageNoCalculationsToSaveNotice, MessageNoticeTitle);
                return;
            }

            SaveFileDialog saveAsDialog = new SaveFileDialog();

            saveAsDialog.Filter = CalculationFileExtensionExtended;
            saveAsDialog.FilterIndex = 0;
            saveAsDialog.InitialDirectory = CalculationSaveDirectoryDefault;
            saveAsDialog.RestoreDirectory = false;

            if (saveAsDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    calculationManager.SaveToFile(saveAsDialog.FileName);
                    saveFilename = saveAsDialog.FileName;

                    changesHaveBeenMade = false;

                    MessageBox.Show(MessageSaveFileSuccess, MessageSuccessTitle);
                }
                catch (Exception ex)
                {
                    if (ex is NotSupportedException ||
                        ex is IOException)
                    {
                        MessageBox.Show(MessageFileParseError, MessageErrorTitle);
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
        ///     First generate the list of strings which will be used for printing.
        ///     Next load the Print Preview Dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // list filled with string lines, to print
            printLines = new List< string >();

            // don't manage pages - the print handler will manage breaking up the lines into pages.
            
            // each line is in format <operator> tab <number>
            // or <totallingOperator> tab tab tab <(sub)total>
            foreach (string line in lstCalculations.Items)
            {
                // use stringbuilder for speed.
                StringBuilder printLine = new StringBuilder("");
                printLine.Append(line[0]);
                printLine.Append('\t');

                if (OperatorFullTotal.Contains(line[0])
                    || OperatorSubTotal.Contains(line[0]))
                {
                    printLine.Append('\t');
                    printLine.Append('\t');
                    printLine.Append(line.Split(
                        new[] { IndicatorTotalText },
                        StringSplitOptions.None)[1].Trim());
                }
                else
                {
                    printLine.Append(line.Split(new[] { ' ' })[1].Trim());
                }

                printLines.Add(printLine.ToString());
            }

            // 

            // check if the preview form is disposed. if so, recreate it.
            if (printPreviewDialogCalculation.IsDisposed)
            {
                printPreviewDialogCalculation = new PrintPreviewDialog();
                printPreviewDialogCalculation.Document = printCalculation;
            }

            // put the preview dialog in front over the main form.
            printPreviewDialogCalculation.Show();
            printPreviewDialogCalculation.Left = Left + 15;
            printPreviewDialogCalculation.Top = Top + 15;
            printPreviewDialogCalculation.BringToFront();
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
                var invalidCharsFound = false;

                // check for invalid chars first, remove all invalid chars, 
                // if invalid char found, warn user, return
                foreach (var text in txtNextCalculation.Text)
                {
                    if (!OperandDigits.Contains(text)
                        && !OperatorTerminators.Contains(text))
                    {
                        // don't store invalid chars.
                        invalidCharsFound = true;
                    }
                    else
                    {
                        calctext += text.ToString();
                    }
                }

                if (invalidCharsFound)
                {
                    ShowToolTipMessageNearNextCalculationTextbox(
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
                if ((calculationManager.Count == 0 ||
                    calculationManager.Find(calculationManager.Count - 1).Op
                    == Operator.total) && !OperandDigits.Contains(calctext[0])
                    && !OperandSigns.Contains(calctext[0]))
                {
                    // ... first char is not digit or sign, raise an Error
                    ShowToolTipMessageNearNextCalculationTextbox(
                        "This is the start of a new Calculation. \r\n" +
                        "Your first Calc Line must begin with a digit or a sign symbol.\r\n" +
                        "Format: [+ or -]<numbers><one of " + OperatorTerminators + ">");
                    calctext = "";
                    
                }

                // Calc Line is for only a subtotal.

                // if we are dealing with a starting calcline and it passed the above branch,
                // it will skip this branch.
                else if (OperatorSubTotal.Equals(calctext[0].ToString()))
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
                        ShowToolTipMessageNearNextCalculationTextbox(
                            MessageEditingPlaceConsecutiveSubtotalsError);
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
                else if (OperatorFullTotal.Equals(calctext[0].ToString()))
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
                else if (OperatorTerminators.Contains(calctext[calctext.Length - 1]) && calctext.Length > 1)
                {

                    // break the text into the lead operator, and the string of numbers
                    // also break out the terminating operator, as the last char
                    char leadOperator;
                    StringBuilder numString =
                        new StringBuilder(calctext.Substring(0, calctext.Length - 1));
                    char terminatingChar = calctext[calctext.Length - 1];

                    // if no leading Operator, assume it is a plus.
                    if (OperandDigits.Contains(numString[0]))
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
                        if (terminatingChar.ToString() == OperatorSubTotal)
                        {
                            calculationManager.Add(new CalcLine(Operator.subtotal));
                            calctext = "";
                        }
                        else if (terminatingChar.ToString() == OperatorFullTotal)
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
                        if (calculationManager.Count == 0 ||
                            calculationManager.Find(calculationManager.Count - 1).Op
                            == Operator.total)
                        {
                            ShowToolTipMessageNearNextCalculationTextbox(
                            "The Calculation noes not contain a valid number. \r\n" +
                            "Format: [One of " + OperandSigns +
                            "]<digits><One of " + OperatorTerminators + ">");
                        }
                        else
                        {
                            ShowToolTipMessageNearNextCalculationTextbox(
                            "The Calculation noes not contain a valid number. \r\n" +
                            "Format: [One of " + OperatorCalculations + "]<digits><One of "
                            + OperatorTerminators + ">");
                        }
                        
                        if (calctext.Length > 0)
                        {
                            calctext = calctext[0].ToString();
                        }
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
        public static bool SelectedCalculationIsValid(string calculation)
        {
            var isValid = false;

            var calcParts = calculation.Split(new[] { ' ' }, 2);

            // calculation cannot be "" or null
            if (calculation.Equals(""))
            {
                return false;
            }

            // calculation can be "#" or "="
            if (calcParts.Length == 1 
                && OperatorTotals.Contains(calcParts[0]))
            {
                isValid = true;
            }
            
            // calculation can be "<operator> <digits>"
            if (calcParts.Length == 2
                && OperatorCalculations.Contains(calcParts[0]))
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
                ShowToolTipMessageNearNextCalculationTextbox(
                    "Please first select a calculation line to Update.");
            }
            else if (!SelectedCalculationIsValid(txtSelectedCalculation.Text))
            {
                ShowToolTipMessageNearNextCalculationTextbox(
                    MessageEditingInvalidCalculationEnteredError);
            }
            // first calcline in any set of calculations must start with - or +
            else if ((lstCalculations.SelectedIndex == 0
                || calculationManager.Count > 1 &&
                lstCalculations.SelectedIndex > 0 &&
                calculationManager.Find(lstCalculations.SelectedIndex - 1).Op
                == Operator.total)
                && ( !OperandSigns.Contains(txtSelectedCalculation.Text[0])
                ))
            {
                ShowToolTipMessageNearNextCalculationTextbox(
                    MessageInitialCalcLineMissingSignError);
            }

            // cannot put a subtotal after another subtotal or before another subtotal
            else if (calculationManager.Count > 1 
                && OperatorSubTotal.Contains(txtSelectedCalculation.Text[0])
                && ((lstCalculations.SelectedIndex - 1 > -1
                        && calculationManager.Find(lstCalculations.SelectedIndex - 1).Op
                        == Operator.subtotal)) 
                    || (lstCalculations.SelectedIndex + 1 < lstCalculations.Items.Count
                        && calculationManager.Find(lstCalculations.SelectedIndex + 1).Op
                        == Operator.subtotal)
                )
            {
                ShowToolTipMessageNearNextCalculationTextbox(
                    MessageEditingPlaceConsecutiveSubtotalsError);
            }

            // cannot place a total before another total.
            else if (calculationManager.Count > 1
                && OperatorFullTotal.Contains(txtSelectedCalculation.Text[0])
                && ((lstCalculations.SelectedIndex + 1 < lstCalculations.Items.Count
                        && calculationManager.Find(lstCalculations.SelectedIndex + 1).Op
                        == Operator.total)
                ))
            {
                ShowToolTipMessageNearNextCalculationTextbox(
                    MessageEditingConsecutiveTotalsError);
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
            if (calculationManager.Count == 0)
            {
                ShowToolTipMessageNearNextCalculationTextbox(
                    "There are no calculations to delete.");
            }
            else if (lstCalculations.SelectedIndex < 0)
            {
                ShowToolTipMessageNearNextCalculationTextbox(
                    "Please first select a calculation line to delete.");
            }
            else if (lstCalculations.SelectedIndex > 0
                     && calculationManager.Find(lstCalculations.SelectedIndex - 1).Op ==
                     Operator.total
                     && lstCalculations.SelectedIndex < lstCalculations.Items.Count
                     && (calculationManager.Find(lstCalculations.SelectedIndex + 1).Op ==
                         Operator.subtotal || calculationManager.Find(
                         lstCalculations.SelectedIndex + 1).Op == Operator.total)
                    )
            {
                ShowToolTipMessageNearNextCalculationTextbox(
                    "Cannot delete: A total or subtotal Calc Line is \r\nnot permitted after a total Calc Line");
            }
            else if (lstCalculations.SelectedIndex > 0
                     && calculationManager.Find(lstCalculations.SelectedIndex - 1).Op ==
                     Operator.subtotal
                     && lstCalculations.SelectedIndex < lstCalculations.Items.Count
                     && calculationManager.Find(lstCalculations.SelectedIndex + 1).Op ==
                     Operator.subtotal
                    )
            {
                ShowToolTipMessageNearNextCalculationTextbox(
                    "Cannot delete: A subtotal Calc Line is \r\nnot permitted after a subtotal Calc Line");
            }

            else
            {
                if (lstCalculations.SelectedIndex > 0 
                    && calculationManager.Find(lstCalculations.SelectedIndex - 1).Op ==
                    Operator.total)
                {
                    // all calc lines at the start of a set of calculations must begin with a plus.
                    calculationManager.Find(lstCalculations.SelectedIndex - 1).Op = Operator.plus;
                }


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
            
            if (calculationManager.Count == 0)
            {
                ShowToolTipMessageNearNextCalculationTextbox(
                    "There are no calculations to insert above.\r\n");
            }
            else if (lstCalculations.SelectedIndex == -1)
            {
                ShowToolTipMessageNearNextCalculationTextbox(
                    "Select a calculation to insert above.");
            }
            else if (!SelectedCalculationIsValid(txtSelectedCalculation.Text))
            {
                ShowToolTipMessageNearNextCalculationTextbox(
                    MessageEditingInvalidCalculationEnteredError);
            }

            // first calcline in any set of calculations must start with - or +
            else if ((lstCalculations.SelectedIndex == 0
                || calculationManager.Count > 1 &&
                lstCalculations.SelectedIndex > 0 &&
                calculationManager.Find(lstCalculations.SelectedIndex - 1).Op
                == Operator.total)
                && (!OperandSigns.Contains(txtSelectedCalculation.Text[0])
                ))
            {
                ShowToolTipMessageNearNextCalculationTextbox(
                    MessageInitialCalcLineMissingSignError);
            }

            // cannot put a subtotal after another subtotal or before another subtotal
            else if (calculationManager.Count > 1
                && OperatorSubTotal.Contains(txtSelectedCalculation.Text[0])
                && ((lstCalculations.SelectedIndex - 1 > -1
                        && calculationManager.Find(lstCalculations.SelectedIndex - 1).Op
                        == Operator.subtotal))
                    || (calculationManager.Find(lstCalculations.SelectedIndex).Op
                        == Operator.subtotal)
                )
            {
                ShowToolTipMessageNearNextCalculationTextbox(
                    MessageEditingPlaceConsecutiveSubtotalsError);
            }

            // cannot insert a total before another total or after another total.
            else if (calculationManager.Count > 1
                && OperatorFullTotal.Contains(txtSelectedCalculation.Text[0])
                && ((lstCalculations.SelectedIndex - 1 > -1
                        && calculationManager.Find(lstCalculations.SelectedIndex - 1).Op
                        == Operator.total))
                    || (calculationManager.Find(lstCalculations.SelectedIndex).Op
                        == Operator.total)
                )
            {
                ShowToolTipMessageNearNextCalculationTextbox(
                    MessageEditingConsecutiveTotalsError);
            }



            else
            {
                int index = lstCalculations.SelectedIndex;
                calculationManager.Insert(
                    new CalcLine(txtSelectedCalculation.Text),
                    lstCalculations.SelectedIndex);
                lstCalculations.SelectedIndex = index;

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
                ShowToolTipMessageNearNextCalculationTextbox(
                    "Please enter a calculation.");

            }
        }

        /// <summary>
        ///     Helper method for tool tips.
        ///     places tooltip in same location each time.
        /// </summary>
        /// <param name="message">the message to show in the tooltip.</param>
        private void ShowToolTipMessageNearNextCalculationTextbox(string message)
        {
            tip.Show(message, txtNextCalculation,
                    -10, 25, 3000);
        }

        /// <summary>
        ///     Print the calculations.
        ///     A font and font size are assumed (see the class constants above).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printCalculation_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (printLines == null || printLines.Count == 0 )
            {
                return;
            }

            var lineHeight = PrintFont.Height;
            var linesPrinted = 0;
            var leftMargin = e.MarginBounds.Left;
            // identify the maximum allowed length for lines
            var maxLineLength = e.MarginBounds.Width;
            var topMargin = e.MarginBounds.Top;
            // identify the maximum allowed number of lines per page.
            var maxLinesToPrint = e.MarginBounds.Height / lineHeight;

            var g = e.Graphics;

            while (linesPrinted < maxLinesToPrint
                && printLines.Count > 0)
            {
                double currentLineLength = g.MeasureString(printLines[0], PrintFont).Width;

                string line;

                // if line is too long, break it apart and print over multiple lines.
                // do the line break with respect to max lines per page.
                if (currentLineLength > maxLineLength)
                {
                    line = printLines[0].Substring(0, 1);
                    printLines[0] = '\t' + printLines[0].Substring(1).Trim();
                }
                else
                {
                    line = printLines[0];
                    printLines.RemoveAt(0);
                }

                g.DrawString(line, PrintFont, Brushes.Black,
                    leftMargin, topMargin + (linesPrinted * lineHeight));
                linesPrinted++;


            }

            if (printLines.Count > 0)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
        }
    }
}
