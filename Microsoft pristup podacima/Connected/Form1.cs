using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Connected;

namespace Connected
{
    public partial class Form1 : Form
    {
        Form2 formInsert = new Form2();
        clsDataAccess crud = new clsDataAccess();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                int val = crud.Read();
                dataGridView1.DataSource = crud.ds.Tables[0];
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToDeleteRows = false;
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            formInsert.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if(dataGridView1.SelectedRows.Count==0)
                {
                    MessageBox.Show("Morate odabrati klijenta!");
                }

                else
                {
                    int KlijentID = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                    if (MessageBox.Show("Da li zelite da obrisete klijenta?", "Brisanje klijenta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int rez = crud.Delete(KlijentID);
                        if (rez != 0)
                        {
                            MessageBox.Show("Doslo je do greske!");
                        }

                        else
                        {
                            MessageBox.Show("Klijent uspesno obrisan.");
                        }
                    }
                }
                
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if(dataGridView1.SelectedRows.Count==0)
                {
                    MessageBox.Show("Morate odabrati klijenta!");
                }

                else
                {
                    int KlijentID = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                    string Naziv = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    string Kontakt = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    string Grad = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                    string Zemlja = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                    this.Hide();
                    Form3 formUpdate = new Form3(KlijentID, Naziv, Kontakt, Grad, Zemlja);
                    formUpdate.ShowDialog();
                }
                
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                crud.ds.Clear();
                int val = crud.Read();
                dataGridView1.DataSource = crud.ds.Tables[0];
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
    }
}
