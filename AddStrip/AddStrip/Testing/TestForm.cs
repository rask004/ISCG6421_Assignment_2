using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddStrip.Testing
{
    public partial class TestForm : Form
    {
        private const string CalculationFileExtension = "cal";
        private const string CalculationSaveDirectoryDefault = @"C:\temp";

        FrmAddStrip parentFrm;

        public TestForm(FrmAddStrip frm)
        {
            InitializeComponent();
            parentFrm = frm;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show(textBox1.Text, "Error");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parentFrm.tip.Show(textBox1.Text,
                parentFrm.txtNextCalculation,
                10, -80, 5000);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            parentFrm.tip.Show(textBox1.Text,
                parentFrm.txtSelectedCalculation,
                10, -80, 5000);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Do you wish to save your changes?", 
                "Changes have been made", 
                MessageBoxButtons.YesNo);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveAsDialog = new SaveFileDialog();

            saveAsDialog.Filter = "calculation files (*."+ CalculationFileExtension + ")" +
                "|*." + CalculationFileExtension;
            saveAsDialog.FilterIndex = 0;
            saveAsDialog.InitialDirectory = CalculationSaveDirectoryDefault;
            saveAsDialog.RestoreDirectory = false;

            if (saveAsDialog.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveAsDialog.OpenFile()) != null)
                {
                    // Code to write the stream goes here.
                    myStream.Close();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = CalculationSaveDirectoryDefault;
            openFileDialog1.Filter = "calculation files (*." + CalculationFileExtension + ")" +
                "|*." + CalculationFileExtension;
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = false;

            openFileDialog1.ShowDialog();
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string[] text = textBox1.Text.Split(new string[] { "\r\n" }, 2, StringSplitOptions.None);

            MessageBox.Show(text[1], text[0]);
        }
    }
}
