using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
///     Addstrip Project (ISCG6421 Assignment 2)
///     Sub space for calculation related objects.
/// </summary>
namespace AddStrip.Calculations
{
    /// <summary>
    ///     Calculation class for Assignment 2 (see namespace summary).
    ///     Uses required methods in the CalculationContract object.
    /// </summary>
    class Calculation: CalculationContract
    {
        // The listbox to show calculation lines in
        private ListBox lstDisplay;

        // The list of calculation lines made.
        private ArrayList theCalcs;

        // file content constants
        const string fileLineSeparator = "\r\n";
        const string fileFieldHeader = "~AddStripCalculationLineFile";

        const string operatorCalculations = "+-*/";
        const string operatorSubTotal = "#";
        const string operatorFullTotal = "=";

        public const string indicatorTotalText = "<<";


        /// <summary>
        ///     construct the Calculation object, given a listbox to display calculation results to.
        /// </summary>
        /// <param name="lb">The listbox to show calculation lines in.</param>
        public Calculation(ListBox lb)
        {
            if (lb == null)
            {
                throw new ArgumentNullException("Calculation.Calculation(Listbox lb): lb must be an initialised listbox.");
            }
            lstDisplay = lb;

            theCalcs = new ArrayList();
        }

        /// <summary>
        ///     Add a calc line object.
        /// </summary>
        /// <param name="cl">calc line object</param>
        public void Add(CalcLine cl)
        {
            theCalcs.Add(cl);

            Redisplay();
        }

        /// <summary>
        ///     Remove all calc line objects.
        /// </summary>
        public void Clear()
        {
            theCalcs.Clear();

            Redisplay();
        }

        /// <summary>
        ///     Remove a calc line object at an index.
        /// </summary>
        /// <param name="n">index of the calc line object to remove.</param>
        public void Delete(int n)
        {
            if (n >= 0 && n <= theCalcs.Count)
            {
                theCalcs.RemoveAt(n);
            }
            else
            {
                throw new IndexOutOfRangeException("index n = " + n +
                    "; range = 0 to " + theCalcs.Count);
            }

            Redisplay();
        }

        /// <summary>
        ///     Find a calc line object at an index.
        /// </summary>
        /// <param name="n">an index integer.</param>
        /// <returns>The calc line object found at n.</returns>
        public CalcLine Find(int n)
        {
            if (n >= 0 && n <= theCalcs.Count)
            {
                CalcLine cl = (CalcLine)theCalcs[n];
                return cl;
            }
            else
            {
                throw new IndexOutOfRangeException("index n = " + n +
                    "; range = 0 to " + theCalcs.Count);
            }
        }

        /// <summary>
        ///     Insert a new calc line object at an index.
        /// </summary>
        /// <param name="newCalc">new calc line object.</param>
        /// <param name="n">index integer.</param>
        public void Insert(CalcLine newCalc, int n)
        {
            // if lstDisplay is empty, selectedindex will be -1
            // or if the index is too big
            if (n == -1 || n > theCalcs.Count)
            {
                Add(newCalc);
            }
            else if (n >= 0 && n <= theCalcs.Count)
            {
                theCalcs.Insert(n, newCalc);
            }
            else
            {
                throw new IndexOutOfRangeException("index n = " + n +
                    "; range = 0 to " + theCalcs.Count);
            }

            Redisplay();
        }

        /// <summary>
        ///     Load a set of calc line objects to a file.
        /// </summary>
        /// <param name="filename">filename to load from.</param>
        public void LoadFromFile(string filename)
        {
            Stream readStream;
            string fileStringData = "";

            // throws IOException, NotSupportedException if file reading error occurs
            using (readStream = new FileStream(filename, FileMode.Open))
            {
                
                char currentChar;
                int currentByte;

                while ((currentByte = readStream.ReadByte()) != -1)
                {
                    currentChar = Convert.ToChar(currentByte);
                    fileStringData += currentChar.ToString();
                }
            }

            string[] fileStrings = fileStringData.Split(new string[] { fileLineSeparator },
                StringSplitOptions.None);

            // verify this is a calc line file.
            // first line should be header
            // rest assumed to be calc line strings.

            if (!fileStrings[0].Equals(fileFieldHeader))
            {
                throw new FormatException("File format is not that of a calculation line file.");
            }

            theCalcs.Clear();

            foreach (string fileString in fileStrings)
            {

                if (fileString.Length > 0)
                {
                    string[] calcParts = fileString.Split(
                                        new char[] { ' ' }, 2);

                    Operator op;
                    Double num = 0;
                    try
                    {
                        switch (calcParts[0])
                        {
                            case "=":
                                op = Operator.total;
                                num = 0;
                                break;
                            case "#":
                                op = Operator.subtotal;
                                num = 0;
                                break;
                            case "*":
                                op = Operator.times;
                                num = Convert.ToDouble(calcParts[1]);
                                break;
                            case "/":
                                op = Operator.divide;
                                num = Convert.ToDouble(calcParts[1]);
                                break;
                            case "-":
                                op = Operator.minus;
                                num = Convert.ToDouble(calcParts[1]);
                                break;
                            case "+":
                                op = Operator.plus;
                                num = Convert.ToDouble(calcParts[1]);
                                break;
                            default:
                                op = Operator.error;
                                num = 0;
                                break;
                        }
                    }
                    // could not convert. Skip
                    catch (FormatException)
                    {
                        op = Operator.error;
                        num = 0;
                    }

                    if (op != Operator.error)
                    {
                        theCalcs.Add(new CalcLine(op, num));
                    } 
                }
            }
            
            Redisplay();
        }

        /// <summary>
        ///     update an associated display object (likely a form control).
        /// </summary>
        public void Redisplay()
        {

            double total = 0;

            lstDisplay.Items.Clear();

            // check all known Calc Lines
            for (int i = 0; i < theCalcs.Count; i++)
            {
                CalcLine currentCalcLine = (theCalcs[i] as CalcLine);
                var calcString = currentCalcLine.ToString();

                total = currentCalcLine.NextResult(total);

                // prepare subtotal string if (sub)total
                if (currentCalcLine.Op == Operator.subtotal ||
                    currentCalcLine.Op == Operator.total)
                {
                    calcString += "  " + indicatorTotalText + " " + total.ToString("F4") ;
                }

                if (currentCalcLine.Op == Operator.total)
                {
                    total = 0;
                }

                lstDisplay.Items.Add(calcString);
            }

        }

        /// <summary>
        ///     Replace a calc line object at an index.
        /// </summary>
        /// <param name="newCalc">new calc line object.</param>
        /// <param name="n">index integer.</param>
        public void Replace(CalcLine newCalc, int n)
        {
            if (n >= 0 && n <= theCalcs.Count)
            {
                theCalcs[n] = newCalc;
            }
            else
            {
                throw new IndexOutOfRangeException("index n = " + n +
                    "; range = 0 to " + theCalcs.Count);
            }

            Redisplay();
            lstDisplay.SelectedIndex = n;
        }

        /// <summary>
        ///     Save a set of calc line objects to a file.
        /// </summary>
        /// <param name="filename">filename to save to.</param>
        public void SaveToFile(string filename)
        {
            List<byte> buffer = new List<byte>();

            // buffer the header
            foreach (char c in (fileFieldHeader + fileLineSeparator))
            {
                buffer.Add(Convert.ToByte(c));
            }

            // buffer each CalcLine string
            foreach (CalcLine cl in theCalcs)
            {
                foreach (char c in (cl.ToString() + fileLineSeparator))
                {
                    buffer.Add(Convert.ToByte(c));
                }
            }

            // Write buffer to file.
            using (FileStream writeStream = new FileStream(filename, FileMode.Create))
            {
                writeStream.Write(buffer.ToArray(), 0, buffer.Count);
            }
        }
    }
}
