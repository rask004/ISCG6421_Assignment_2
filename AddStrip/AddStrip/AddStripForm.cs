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

//TODO: create a test form for creating messages (clickdummy)
//TODO: create a main form, dummy form with no actions or validation.
//TODO: create namespace structure, and update files.

/// <summary>
///     Addstrip Project (ISCG6421 Assignment 2)
/// </summary>
namespace AddStrip
{
    /// <summary>
    ///     Main user form for the AddStrip application
    /// </summary>
    public partial class AddStripForm : Form
    {
        // Stores calc lines and manages displayed results.
        Calculation calculationManager;



        //TODO: remove this when testing finished.
        AddStrip.Testing.TestForm testForm;

        /// <summary>
        ///     Constructor
        /// </summary>
        public AddStripForm()
        {
            InitializeComponent();
            calculationManager = new Calculation(lstCalculations);
        }

        /// <summary>
        ///     Load additional objects. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddStripForm_Load(object sender, EventArgs e)
        {
            testForm = new AddStrip.Testing.TestForm();
            testForm.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddStripForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            testForm.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCalculation_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateCalculation_Click(object sender, EventArgs e)
        {

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
    }
}
