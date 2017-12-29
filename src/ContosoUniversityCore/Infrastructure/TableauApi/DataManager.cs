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

        public DataManager()
        {

            Tableau.SetTitle("Tableau Simple")
                .SetDefaultValue("0")
                .SetFormat(EnumFormat.Marge)
                .SetTableauCelluleClass("text-right")
                .AddTableauClass("table-bordered table-striped w-auto")
                .SetTHeadClass("blue-grey lighten-1")
                .SetCelulleConditionalClass("table-warning", "", (w) => w == 1);

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
            
            //Tableau.AddLigneTitre1("Marges")
            //        .AddLigne(TableauRes.LMargeBrute).SetDefaultValue("1000")
            //        .AddLigne(TableauRes.LMargeBruteSem1).SetDefaultValue("1300")
            //        .AddLigne(TableauRes.LMargeBruteSem2)
            //        .AddLigne(TableauRes.LMargeBruteSem3);

            //Tableau.AddLigne(TableauRes.LMargeBruteSansTacite, 1);
            //Tableau.AddLigne(TableauRes.LMargeExtreme, 1)
            //    .AddChildLigne(TableauRes.LMargeExtremeSem1)
            //    .AddLigne(TableauRes.LMargeExtremeSem2)
            //    .AddLigne(TableauRes.LMargeExtremeSem3)
            //    .AddLigne(TableauRes.LMargeExtremeSem4)
            //    .AddLigne(TableauRes.LMargeExtremeSem5)
            //    .AddLigneTitre3(TableauRes.LMargeExtremeSem6)
            //        .AddLigne(TableauRes.LMargeExtremeSem6Lundi);

            //Tableau.SetTitle("Mon premier tableau")
            //    .SetFormat(EnumFormat.Marge)
            //    .SetDefaultValue("1")
            //    .SetCelulleConditionalClass("warning", "", (x) => x < 10)
            //    .SetTableauClass("table table-sm table-dark table-hover");

            //Tableau.AddColonne()
            //    .AddColonne(TableauRes.Tableau1ColonneTotal)
            //    .AddColonne(TableauRes.Tableau1ColonneGaz)
            //        .SetCelullueDefaultValue("10")
            //        .SetCelullueConditionalClass("success", "", (x) => x > 10)
            //    .AddColonne(TableauRes.Tableau1ColonneElec)
            //        .SetCelullueConditionalClass("danger", "", (x) => x <= 1200)
            //        .SetColonneClass("sucess")
            //    .AddColonne(TableauRes.Tableau1ColonneAutre)
            //    .AddColonne(TableauRes.Tableau1ColonneTeleReleve)
            //    .AddColonne(TableauRes.Tableau1ColonneProfile);

            //Tableau.AddLigneTitre1("Marges")
            //    .AddChildLigne(TableauRes.LMargeBrute).SetDefaultValue("1000")
            //        .AddChildLigne(TableauRes.LMargeBruteSem1).SetDefaultValue("1300")
            //        .AddLigne(TableauRes.LMargeBruteSem2)
            //        .AddLigne(TableauRes.LMargeBruteSem3);

            //Tableau.AddLigne(TableauRes.LMargeBruteSansTacite, 1);
            //Tableau.AddLigne(TableauRes.LMargeExtreme, 1)
            //    .AddChildLigne(TableauRes.LMargeExtremeSem1)
            //    .AddLigne(TableauRes.LMargeExtremeSem2)
            //    .AddLigne(TableauRes.LMargeExtremeSem3)
            //    .AddLigne(TableauRes.LMargeExtremeSem4)
            //    .AddLigne(TableauRes.LMargeExtremeSem5)
            //    .AddLigneTitre3(TableauRes.LMargeExtremeSem6)
            //        .AddChildLigne(TableauRes.LMargeExtremeSem6Lundi);

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
                    SetValeur(item.Item2)
                    .SetLink("Course");
                //SetConditionalClass("success", "", x => x.Value > 10);
            }
        }

    }
}