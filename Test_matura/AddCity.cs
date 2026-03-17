using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_matura
{
    public partial class AddCity : Form
    {
        public AddCity()
        {
            InitializeComponent();
        }

        //Dialogove okno je cele postaveno jako třída


        //privatni promene
        private string _city;
        private int _x;
        private int _y;
        private int _population;
        private bool _regional;
        private int _infected;


        //Properties (žádné specialni upravy)
        public string City { get { return _city; }  }
        public int X { get { return _x; } }
        public int Y { get { return _y; } }
        public int Population { get { return _population; } }
        public bool Regional { get { return _regional; } }
        public int Infected { get { return _infected; } }


        private void button1_Click(object sender, EventArgs e)
        {

            //Nacteni zadaných hodnot
            _city = textBoxCity.Text;
            try //kontrola
            {
                _x = int.Parse(textBoxX.Text);
                _y = int.Parse(textBoxY.Text);
                _population = int.Parse(textBoxPopulation.Text);
                _infected = int.Parse(textBoxInfected.Text);
            }
            catch
            {
                DialogResult = DialogResult.Cancel;
                MessageBox.Show("Nějaká ze zadaných hodnot je špatná.");
            }

            if (checkBoxRegional.Checked)
                _regional = true;
            else
                _regional = false;


            //Podmínka aby neprisli neplatny hodnoty (nejaky veci tam chybi)
            if(_x  < 0 || _x > 500 || _y < 0 || _y > 300 || _infected > _population || _city == null)
            {
                DialogResult = DialogResult.Cancel;
                MessageBox.Show("Zadal si souřadnice mimo panel, nezadal si název města nebo si zadal více nakažených než je populace.");
            }
            else
            {
                DialogResult= DialogResult.OK;
                Close();
            }

        }


        //Tlacitko pro zavreni
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
