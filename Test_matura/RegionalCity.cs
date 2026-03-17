using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Test_matura
{
    internal class RegionalCity
    {

        //Třída pro krajske mesta

        //priivatni promene
        private string _city;
        private int _x;
        private int _y;
        private int _population;
        private int _infected;

        private List<City> _cities = new List<City>();


        public List<City> Cities => _cities;


        //Jednoduche properties
        public string City => _city;
        public int Scale => Population / 1000; //Scale pro vykreslovani (1000 obyvatel = 1px) (muze byt jiny mozna to bude i v zadani)
        public int X => _x;
        public int Y => _y;

        //Pro upravu pozice kruhu aby byla na stredu (Neni treba nebojujeteli o znamku 1 (mozna 2))
        public int LeftTopX => _x - Scale / 2;
        public int LeftTopY => _y - Scale / 2;
        //Vraci hodnoty samotnyho mesta
        public int OnlyRegPopulation => _population;
        public int OnlyRegInfected => _infected;

        //Vraci hodnotu populace celeho kraje(krajske mesto + mala mesta)
        public int Population {
            get
            {
                int sum = 0;

                for (int i = 0; i < _cities.Count; i++)
                {
                    sum += _cities[i].Population;
                }
                return _population + sum;
            }
        }

        //Vraci hodnotu infikovanych celeho kraje(krajske mesto + mala mesta)
        public int Infected {
            get
            {
                int sum = 0;

                for (int i = 0; i < _cities.Count; i++)
                {
                    sum += _cities[i].Infected;
                }

                return _infected + sum;
            }
        }


        //Vraci barvu mesta podle poctu infikovanych
        public Color ColorC
        {
            get
            {
                double ratio = (double)Infected / Population;

                //Dva intervaly aby byly hezci barvy(lze udelat na jeden interval, ale barvy potom nejsou tak hezky)
                if (ratio <= 0.5)
                {
                    double t = ratio / 0.5;
                    int red = (int)(255 * t);
                    return Color.FromArgb(red, 255, 0);
                }
                else
                {
                    double t = (ratio - 0.5) / 0.5;
                    int green = (int)(255 * (1 - t));
                    return Color.FromArgb(255, green, 0);
                }
            }
        }

        //Konstruktor
        public RegionalCity(string city, int x, int y, int population, int infected)
        {
            _city = city;
            _x = x;
            _y = y;
            _population = population;
            _infected = infected;
        }


        // Funkce na pridani mesta do kraje
        public void AddCity(City c)
        {
            _cities.Add(c);
        }

        /// <summary>
        /// Procenta infikovanych celeho kraje
        /// </summary>
        /// <returns>Procenta infikovanych celeho kraje</returns>
        public double InfcPercent()
        {
            return Math.Round(100.0 * Infected / Population, 2);
        }

        /// <summary>
        /// Procenta infikovanych pouze krajskeho mesta
        /// </summary>
        /// <returns>Procenta infikovanych pouze krajskeho mesta</returns>
        public double OnlyRegInfcPerc()
        {
            return Math.Round(100.0 * _infected / _population, 2);
        }


        //String toho co bude v listboxu
        public override string ToString()
        {
            return $"{_city} [{_cities.Count} dalších měst]";
        }


    }
}
