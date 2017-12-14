using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ContosoUniversityCore.Infrastructure.Tableau
{
    public class Tableau
    {
        public Dictionary<int,string> Lignes {get;set;}

        public Dictionary<int, string> Colonnes { get; set; }

        public List<Values> Values { get; set; }
    }

    public class Values
    {
        public int Colonne { get; set; }

        public int Ligne { get; set; }

        public double Value { get; set; }
    }

    public class Test
    {
        public Test()
        {
            var tableau = new Tableau()
            {
                Lignes = new Dictionary<int, string>()
                {
                    { 0, "ligne 1"},
                    { 1, "ligne 2"},
                    { 2, "ligne 3"},
                    { 3, "ligne 4"},
                    { 4, "ligne 5"},
                    { 5, "ligne 6"}
                },
                Colonnes = new Dictionary<int, string>()
                {
                    { 0, "Colone 1"},
                    { 0, "Colone 2"},
                    { 0, "Colone 3"}
                },
                Values = new List<Values>()
                {
                    { new Values()
                        {
                            Colonne = 0,
                            Ligne= 0,
                            Value = 12
                        }
                    }
                }
            };

            Tableau tableau2 = new Tableau();

            tableau2.AddColonne("Colonne Toto").AddColonneStyle("background:yellow");
            tableau2.AddColonne("Colonne Titi");
            tableau2.AddColonne("Colonne Toto");
            tableau2.AddColonne("Colonne Tutu");
            tableau2.AddColonne("Colonne Toto");
            tableau2.AddColonne("Colonne Tintin");

            tableau2.AddValue(0, 1, 2000);

        }
    }
    public static class TestExtension
    {

        public static Tableau AddColonne ( this Tableau tableau, string colonneName)
        {
            return tableau;
        }

        public static void AddColonneStyle(this Tableau tableau, string colonneName)
        {

        }

        public static void AddValue(this Tableau tab, int ligne, int col, double value)
        {

        }
    }
    

}
