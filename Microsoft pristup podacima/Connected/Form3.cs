using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connected
{
    public partial class Form3 : Form
    {
        clsDataAccess crud = new clsDataAccess();
        public Form3(int KlijentID, string Naziv, string Kontakt, string Grad, string Zemlja)
        {
            InitializeComponent();
            textBox1.Text = Naziv;
            textBox2.Text = Kontakt;
            textBox3.Text = Grad;
            textBox4.Text = Zemlja;
            textBox5.Text = KlijentID.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 formGrid = new Form1();
            formGrid.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() != "")
                {
                    if (textBox2.Text.Trim() != "")
                    {
                        if (textBox3.Text.Trim() != "")
                        {
                            if (textBox4.Text.Trim() != "")
                            {
                                int rez;
                                int ID;
                                int.TryParse(textBox5.Text, out ID);
                                rez = crud.Update(ID, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);

                                if (rez != 0)
                                {
                                    MessageBox.Show("Doslo je do greske!");
                                }

                                else
                                {
                                    MessageBox.Show("Klijent uspesno izmenjen.");
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
    }
}
