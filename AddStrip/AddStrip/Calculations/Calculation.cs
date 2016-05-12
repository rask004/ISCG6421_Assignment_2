using System;
using System.Collections;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Redisplay()
        {

            // check all known Calc Lines
            for (int i = 0; i < theCalcs.Count; i++)
            {
                var calcString = theCalcs[i].ToString();

                // prepare string if (sub)total


                // if new calc line, add to listbox contents
                // otherwise update contents at relevant indices
                try
                {
                    if (!lstDisplay.Items[i].Equals(theCalcs[i].ToString()))
                    {
                        lstDisplay.Items[i] = calcString;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    lstDisplay.Items.Add(calcString);
                }
            }

            // if calc lines have been removed, delete the excess listbox lines.
            while (theCalcs.Count < lstDisplay.Items.Count)
            {
                lstDisplay.Items.RemoveAt(lstDisplay.Items.Count - 1);
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
