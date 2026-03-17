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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Promenna pro dialogove okno
        private AddCity addcity;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Vytvoreni noveho mesta
        private void vytvořitMěstoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Otevreni dialogoveho okna
            addcity = new AddCity();

            if (addcity.ShowDialog() == DialogResult.OK)
            {
                if (addcity.Regional == true)//Vytvoreni krajskeho mesta
                {
                    RegionalCity regcity = new RegionalCity(addcity.City, addcity.X, addcity.Y, addcity.Population, addcity.Infected);
                    listBoxRegional.Items.Add(regcity);
                }
                else if (addcity.Regional == false)//Vytvoreni NEkrajskeho mesta
                {
                    City cit = new City(addcity.City, addcity.X, addcity.Y, addcity.Population, addcity.Infected);

                    //Část pro nalezeni nejblizsiho krajskeho mesta
                    RegionalCity nearestRegCit = null;
                    double minDistance = double.MaxValue;

                    for (int i = 0; i < listBoxRegional.Items.Count; i++)
                    {
                        RegionalCity r = (RegionalCity)listBoxRegional.Items[i];

                        double dx = r.X - cit.X;
                        double dy = r.Y - cit.Y;

                        double distanceSquared = dx * dx + dy * dy;

                        if (distanceSquared < minDistance)
                        {
                            minDistance = distanceSquared;
                            nearestRegCit = r;
                        }
                    }

                    if (nearestRegCit != null)
                    {
                        nearestRegCit.AddCity(cit);

                        int index = listBoxRegional.Items.IndexOf(nearestRegCit);
                        listBoxRegional.Items[index] = nearestRegCit;
                    }                 
                }

                //Prevedeni pro LINQ
                var Regcities = listBoxRegional.Items.Cast<RegionalCity>();

                //LINQ pro nalezeni mest s nejnizsi a nejvyssi hodnotou nakazenych
                RegionalCity minRegCity = Regcities.OrderBy(rc => rc.InfcPercent()).First();
                RegionalCity maxRegCity = Regcities.OrderByDescending(rc => rc.InfcPercent()).First();

                //Zapsani do labelu
                labelMostInficted.Text = $"{minRegCity.City} ({minRegCity.InfcPercent()}%)";
                labelLeastInficted.Text = $"{maxRegCity.City} ({maxRegCity.InfcPercent()}%)";

                //refresh panelu jelikoz kazdy pridani mesta zmeni neco v grafice
                panel1.Refresh();
            }

        }

        //Vytvoreni pomocne promene pro mesto se kterym se zrovna pracuje
        RegionalCity selectedCity = null;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //Vykresleni vsech kraju podle jejich aktualnich hodnot
            for (int i = 0; i < listBoxRegional.Items.Count; i++)
            {
                RegionalCity r = (RegionalCity)listBoxRegional.Items[i];

                g.FillEllipse(new SolidBrush(r.ColorC), r.LeftTopX, r.LeftTopY, r.Scale, r.Scale);

                Font font = new Font("Arial", 10);
                g.DrawString(r.City, font, Brushes.Black,r.X, r.Y);

                if (r == selectedCity) //pokud je nejake mesto vybranó zvyraznit ho
                {
                    Pen pen = new Pen(Color.Black, 4);
                    g.DrawEllipse(pen, r.LeftTopX, r.LeftTopY, r.Scale, r.Scale);
                }
            }
        }

        private void listBoxRegional_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pokud na nejake mesto klikneme v listboxu ulozi se do pomocne promene pro vybrane mesto
            if (listBoxRegional.SelectedItem != null)
            {
                selectedCity = (RegionalCity)listBoxRegional.SelectedItem;
                panel1.Refresh();//Refresh panelu aby se zvyraznilo
                CityLabel();
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            //Pokud klikneme do panelu mysi zjisti jestli jsme klikli na souradnice mesta (celej vypocet)
            for (int i = 0; i < listBoxRegional.Items.Count; i++)
            {
                RegionalCity r = (RegionalCity)listBoxRegional.Items[i];

                int radius = r.Scale / 2;

                int dx = e.X - r.X;
                int dy = e.Y - r.Y;

                //Pokud ano ulozi do pomocne pro vybrane mesto mesto na ktere jsme klikli
                if (dx * dx + dy * dy <= radius * radius)
                {
                    selectedCity = r;
                    listBoxRegional.SelectedItem = r;
                    panel1.Refresh(); //Refresh panelu aby se zvyraznilo
                    CityLabel();
                    return;
                }
            }
        }

        //Vypise do labelu vsechni pozadovane podrobnosti vybraneho mesta
        private void CityLabel()
        {
            labelDetails.Text = "";
            labelDetails.Text = $"--- {selectedCity.City} ---" + Environment.NewLine;
            labelDetails.Text += $"Celková populace: {selectedCity.Population}" + Environment.NewLine;
            labelDetails.Text += $"Celkem nakažených: {selectedCity.InfcPercent()}%" + Environment.NewLine;
            labelDetails.Text += Environment.NewLine;
            labelDetails.Text += $"Města ve shluku:" + Environment.NewLine;
            labelDetails.Text += $"{selectedCity.City} - {selectedCity.OnlyRegPopulation} obyv. ({selectedCity.OnlyRegInfcPerc()}%)" + Environment.NewLine;
            for (int i = 0; i < selectedCity.Cities.Count; i++)
            {
                labelDetails.Text += $"{selectedCity.Cities[i].Cityname} - {selectedCity.Cities[i].Population} obyv. ({selectedCity.Cities[i].InfcPercent()}%)" + Environment.NewLine;
            }

        }


        //Neco takovyho tam pak musime uvyst aby byl poznat autor prace (V zadani bude specifikovano jak presne to bude vypadat)
        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Maturitni prace 2026 Vich programovani");
        }
    }
}
