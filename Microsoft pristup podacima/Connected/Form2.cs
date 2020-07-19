using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Connected;

namespace Connected
{
    public partial class Form2 : Form
    {
        clsDataAccess crud = new clsDataAccess();
        public Form2()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(textBox1.Text.Trim()!="")
                {
                    if (textBox2.Text.Trim() != "")
                    {
                        if (textBox3.Text.Trim() != "")
                        {
                            if (textBox4.Text.Trim() != "")
                            {
                                int rez;
                                rez = crud.Insert(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
                                if (rez != 0)
                                {
                                    MessageBox.Show("Doslo je do greske!");
                                }

                                else
                                {
                                    MessageBox.Show("Klijent uspesno upisan.");
                                    this.Hide();
                                    Form1 formGrid = new Form1();
                                    formGrid.ShowDialog();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Zemlja je obavezna!");
                                textBox4.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Grad je obavezan!");
                            textBox3.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kontakt je obavezan!");
                        textBox2.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Naziv je obavezan!");
                    textBox1.Focus();
                }
   
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 formGrid = new Form1();
            formGrid.ShowDialog();
        }
    }
}
