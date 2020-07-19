using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MTZPPPrviProjekat
{
    public partial class Form1 : Form
    {
        DataSet ds = new DataSet();

        public Form1()            
        {
            InitializeComponent();
  
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //entitet Kupci
            DataTable kupci = new DataTable("Kupci");

            DataColumn KupacID = new DataColumn("KupacID", typeof(int));
            KupacID.AllowDBNull = false;
            KupacID.AutoIncrement = true;
            KupacID.AutoIncrementSeed = 1;
            KupacID.AutoIncrementStep = 1;
            kupci.Columns.Add(KupacID);

           

            DataColumn Adresa = new DataColumn("Adresa", typeof(string));
            Adresa.MaxLength = 200;
            Adresa.AllowDBNull = true;
            kupci.Columns.Add(Adresa);



            DataColumn NazivKupca = new DataColumn("NazivKupca", typeof(string));
            NazivKupca.MaxLength = 50;
            NazivKupca.AllowDBNull = false;
            kupci.Columns.Add(NazivKupca);

            kupci.PrimaryKey = new DataColumn[] { KupacID };

            ds.Tables.Add(kupci);

            //entitet Fakture
            DataTable fakture = new DataTable("Fakture");

            DataColumn FakturaID = new DataColumn("FakturaID", typeof(int));
            FakturaID.AllowDBNull = false;
            FakturaID.AutoIncrement = true;
            FakturaID.AutoIncrementSeed = 1;
            FakturaID.AutoIncrementStep = 1;
            fakture.Columns.Add(FakturaID);

            DataColumn kupacID = new DataColumn("KupacID", typeof(int));
            kupacID.AllowDBNull = false;
            fakture.Columns.Add(kupacID);

            DataColumn Datum = new DataColumn("Datum", typeof(DateTime));
            Datum.AllowDBNull = false;
            Datum.DefaultValue = DateTime.Now;
            fakture.Columns.Add(Datum);

            fakture.PrimaryKey = new DataColumn[] { FakturaID };

            ds.Tables.Add(fakture);

            //Relacija Kupac-Faktura
            DataRelation relation1 = new DataRelation("KupacFaktura", kupci.Columns["KupacID"], fakture.Columns["KupacID"]);
            ds.Relations.Add(relation1);
            ForeignKeyConstraint relation1FKC = (ForeignKeyConstraint)fakture.Constraints["KupacFaktura"];
            relation1FKC.UpdateRule = Rule.None;
            relation1FKC.DeleteRule = Rule.None;

            //entitet FaktureStavke
            DataTable faktureStavke = new DataTable("FaktureStavke");

            DataColumn fakturaID = new DataColumn("FakturaID", typeof(int));
            fakturaID.AllowDBNull = false;
            faktureStavke.Columns.Add(fakturaID);

            DataColumn NazivStavke = new DataColumn("NazivFakture", typeof(string));
            NazivStavke.AllowDBNull = false;
            NazivStavke.MaxLength = 40;
            faktureStavke.Columns.Add(NazivStavke);

            DataColumn CenaStavke = new DataColumn("NazivStavke", typeof(decimal));
            CenaStavke.AllowDBNull = false;
            faktureStavke.Columns.Add(CenaStavke);

            DataColumn[] kljucevi = new DataColumn[2];
            kljucevi[0] = faktureStavke.Columns["FakturaID"];
            kljucevi[1] = faktureStavke.Columns["NazivStavke"];
            faktureStavke.PrimaryKey = kljucevi;

            ds.Tables.Add(faktureStavke);

            //relacija Faktura-Stavka
            DataRelation relacija1 = new DataRelation("FakturaStavke", fakture.Columns["FakturaID"], faktureStavke.Columns["FakturaID"]);
            ds.Relations.Add(relacija1);
            ForeignKeyConstraint relacija1FKC = (ForeignKeyConstraint)faktureStavke.Constraints["FakturaStavke"];
            relacija1FKC.UpdateRule = Rule.None;
            relacija1FKC.DeleteRule = Rule.None;

            comboBox1.DataSource = kupci;
            comboBox1.DisplayMember = "NazivKupca";
            comboBox1.ValueMember = "KupacID";
            comboBox1.SelectedText = "Select";

            comboBox2.DataSource = fakture;
            comboBox2.DisplayMember = "FakturaID";
            comboBox2.ValueMember = "FakturaID";
            comboBox2.SelectedText = "Select";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.Tables["Kupci"];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = ds.Tables["Fakture"];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView3.DataSource = ds.Tables["FaktureStavke"];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Trim()!="")
            {
                ds.Tables["Kupci"].Rows.Add(null, textBox1.Text, textBox2.Text);
            }

            else
            {
                MessageBox.Show("Naziv kupca je obavezan!");
                textBox1.Focus();
            }

            comboBox1.SelectedItem = null;
            comboBox1.SelectedText = "Select";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem!=null)
            {
                ds.Tables["Fakture"].Rows.Add(null, comboBox1.SelectedValue, dateTimePicker1.Value);
            }
            else
            {
                MessageBox.Show("Morate odabrati ID Kupca!");
            }
            comboBox2.SelectedItem = null;
            comboBox2.SelectedText = "Select";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(comboBox2.SelectedItem!=null)
            {
                if (textBox3.Text.Trim() != "")
                {
                    if (textBox4.Text.Trim() != "")
                    {
                        ds.Tables["FaktureStavke"].Rows.Add(comboBox2.SelectedValue, textBox3.Text, textBox4.Text);
                    }

                    else
                    {
                        MessageBox.Show("Cena se mora uneti");
                        
                    }
                }

                else
                {
                    MessageBox.Show("Naziv se mora uneti");
                   
                }
            }
            else
            {
                MessageBox.Show("ID fakture mora biti odabran");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ds.WriteXmlSchema(@"C:\Users\Lenovo\Desktop\PrviProjekat\podaci.xsd");
            ds.WriteXml(@"C:\Users\Lenovo\Desktop\PrviProjekat\podaci.xml", XmlWriteMode.WriteSchema);
            MessageBox.Show("OK");
        }

        private void button8_Click(object sender, EventArgs e)
        {
          
        }
    }
}
