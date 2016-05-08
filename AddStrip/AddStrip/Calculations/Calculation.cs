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
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        public void Delete(int n)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public CalcLine Find(int n)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newCalc"></param>
        /// <param name="n"></param>
        public void Insert(CalcLine newCalc, int n)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void Replace(CalcLine newCalc, int n)
        {
            throw new NotImplementedException();
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
