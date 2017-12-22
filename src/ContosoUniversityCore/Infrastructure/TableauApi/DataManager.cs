using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Resources;
using CSharp.Test.TableauApi.Models;
using ContosoUniversityCore.Infrastructure.TableauApi;
using ContosoUniversityCore.Infrastructure.TableauApi.Models.Enums;

namespace CSharp.Test.TableauApi
{
    public static class ManagerRun
    {
        public static void Run()
        {
            DateManager ma = new DateManager();
        }

    }

    public class DateManager 
    {

        public Tableau Tableau { get; set; } = new Tableau();

        public DateManager()
        {
            Tableau.Title = "Super tableau";
            Tableau.Symbole = "$";
            Tableau.Format = EnumFormat.Marge;
            Tableau.DefaultValue = "-";
            Tableau.AddColonne();
            Tableau.AddColonne(TableauRes.Tableau1ColonneTotal);
            Tableau.AddColonne(TableauRes.Tableau1ColonneGaz).AddColonneCellulesClass("danger");
            Tableau.AddColonne(TableauRes.Tableau1ColonneElec);
            Tableau.AddColonne(TableauRes.Tableau1ColonneAutre);
            Tableau.AddColonne(TableauRes.Tableau1ColonneTeleReleve);
            Tableau.AddColonne(TableauRes.Tableau1ColonneProfile);

            Tableau.AddLigne(TableauRes.LMargeBrute).DefaultValue("1000").
                AddChildLigne(TableauRes.LMargeBruteSem1).DefaultValue("1300").
                AddLigne(TableauRes.LMargeBruteSem2).
                AddLigne(TableauRes.LMargeBruteSem3);

            Tableau.AddLigne(TableauRes.LMargeBruteSansTacite);
            Tableau.AddLigne(TableauRes.LMargeExtreme).AddLigneClass("warning")
                .AddChildLigne(TableauRes.LMargeExtremeSem1)
                .AddLigne(TableauRes.LMargeExtremeSem2)
                .AddLigne(TableauRes.LMargeExtremeSem3)
                .AddLigne(TableauRes.LMargeExtremeSem4)
                .AddLigne(TableauRes.LMargeExtremeSem5)
                .AddLigne(TableauRes.LMargeExtremeSem6)
                    .AddChildLigne(TableauRes.LMargeExtremeSem6Lundi);

            Tableau.Generate();
            GetResultats1();
        }
        
        public void GetResultats1()
        {
            List<TableauValeur> results = new List<TableauValeur>();

            List<Tuple<string, double, double, double, double, double>> query = new List<Tuple<string, double, double, double, double, double>>() {
                                        { new Tuple<string, double,double, double, double, double>("Marge brute 2017", 1,2,3,4,5) },
                                        { new Tuple<string, double, double, double, double,double>("Marge brute sans tacite 2017", 1001,1002,1003,1004,1005.002) },
                                        { new Tuple<string, double, double, double, double,double>("Marge extrême sur année 2017", 2001,2002,2003,2004,2005) }};

            Dictionary<string, TableauValeur> test = new Dictionary<string, TableauValeur>();

            foreach (var item in query)
            {
                Tableau.Value(TableauRes.Tableau1ColonneElec, item.Item1).
                    SetValeur(item.Item2).
                    SetSymbole("ù").
                    SetCelluleClass("danger").
                    SetCelluleClassPredicate("warning", X => X.Value < 10).
                    SetFormat(EnumFormat.Custom);
            }
            
        }
        
    }
}