using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_matura
{
    //Třída město (NE krajske ale male)


    internal class City
    {

        //privatni promene
        private string _city;
        private int _x;
        private int _y;
        private int _population;
        private int _infected;


        //Properties
        public string Cityname => _city; //Specialni zapis pro propertis stejny jako klasicky ale lze vyuzit pouze pokud vracenou hodnotu neupravujeme

        public int X { get { return _x; } }
        public int Y { get { return _y; } }

        public int Population { get { return _population; } }
        public int Infected {  get { return _infected; } }


        //Konstruktor
        public City(string city, int x, int y, int population, int infected)
        {
            _city = city;
            _x = x;
            _y = y;
            _population = population;
            _infected = infected;
        }


        /// <summary>
        /// Funkce pro vraceni procent
        /// </summary>
        /// <returns>Vraceni procent nakazenych malyho mesta</returns>
        public double InfcPercent()
        {
            return Math.Round(100.0 * Infected / Population, 2);
        }
    }
}
