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
        /// 
        /// </summary>
        /// <param name="cl"></param>
        public void Add(CalcLine cl)
        {
            theCalcs.Add(cl);

            Redisplay();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            theCalcs.Clear();

            Redisplay();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
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
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="newCalc"></param>
        /// <param name="n"></param>
        public void Insert(CalcLine newCalc, int n)
        {
            if (n > theCalcs.Count)
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
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public void LoadFromFile(string filename)
        {
            Stream readStream;
            string fileStringData = "";

            // throws IOException, NotSupportedException if file reading error occurs
            using (readStream = new FileStream(filename, FileMode.Open))
            {
                
                char currentChar;

                while ((currentChar = Convert.ToChar(readStream.ReadByte())) != -1)
                {
                    fileStringData += currentChar.ToString();

                }
            }

            string[] fileStrings = fileStringData.Split(new string[] { fileLineSeparator },
                StringSplitOptions.None);

            theCalcs.Clear();

            foreach (string fileString in fileStrings)
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
            
            Redisplay();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Redisplay()
        {
            // TODO: complete logic for handling (sub)totals.

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
                    calcString += " <" + total + ">";
                }

                if (currentCalcLine.Op == Operator.total)
                {
                    total = 0;
                }

                lstDisplay.Items.Add(calcString);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newCalc"></param>
        /// <param name="n"></param>
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public void SaveToFile(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
