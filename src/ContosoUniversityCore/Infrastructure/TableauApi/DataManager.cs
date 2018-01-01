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
            DataManager ma = new DataManager();
        }

    }

    public class DataManager
    {
        
        public Tableau Tableau { get; set; } = new Tableau();

        public DataManager(string titre = "Tableau sur les marges")
        {

            Tableau.SetTitle(titre)
                .SetDefaultValue("0")
                .SetFormat(EnumFormat.Marge)
                .SetTableauCelluleClass("text-right")
                .AddTableauClass("table-bordered table-striped w-auto")
                .SetTHeadClass("blue-grey lighten-1")
                .AddCelulleConditionalClass("table-warning", "", (w) => w == 1);

            Tableau.AddColonne()
                .AddColonne(TableauRes.Tableau1ColonneTotal)
                .AddColonne(TableauRes.Tableau1ColonneGaz)
                .AddColonne(TableauRes.Tableau1ColonneElec)
                .AddColonne(TableauRes.Tableau1ColonneTeleReleve)
                .AddColonne(TableauRes.Tableau1ColonneAutre)
                .AddColonne(TableauRes.Tableau1ColonneProfile);

            Tableau.NewLineTitle("Marges").AddLigneClass("blue-grey lighten-3")
                    .NewChildLine(TableauRes.LMargeBrute).SetDefaultValue("1000")
                    .NewChildLine(TableauRes.LMargeBruteSem1).SetDefaultValue("1300")
                    .NewChildLine(TableauRes.LMargeBruteSem2)
                    .NewChildLine(TableauRes.LMargeBruteSem3)
                .NewEmptyLine()
                .NewLineTitle(TableauRes.LMargeBruteSansTacite).AddLigneClass("blue-grey lighten-3")
                    .NewChildLineTitle(TableauRes.LMargeExtreme)
                      .NewGrandChildrenLine(TableauRes.LMargeExtremeSem1)
                      .NewGrandChildrenLine(TableauRes.LMargeExtremeSem1)
                    .NewChildLine(TableauRes.LMargeExtremeSem2)
                    .NewChildLine(TableauRes.LMargeExtremeSem3)
                    .NewChildLine(TableauRes.LMargeExtremeSem4)
                    .NewChildLine(TableauRes.LMargeExtremeSem5)
                    .NewChildLine(TableauRes.LMargeExtremeSem6)
                        .NewGrandChildrenLine(TableauRes.LMargeExtremeSem6Lundi);
            
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

            List<TableauValeur> data = new List<TableauValeur>();
            
            Tableau.Value(TableauRes.Tableau1ColonneAutre, TableauRes.LMargeBruteSem1)
                .SetValeur(32)
                .SetSymbole("£");
            
            foreach (var item in query)
            {
                Tableau.Value(TableauRes.Tableau1ColonneElec, item.Item1).
                    SetValeur(item.Item2)
                    .SetLink("Course");
                //SetConditionalClass("success", "", x => x.Value > 10);
            }


        }

    }
}