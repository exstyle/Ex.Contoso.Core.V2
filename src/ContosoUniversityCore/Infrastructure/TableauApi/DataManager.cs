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
            Tableau.AddColonne();
            Tableau.AddColonne(TableauRes.Tableau1ColonneTotal).AddColonneClass("background:yellow");
            Tableau.AddColonne(TableauRes.Tableau1ColonneGaz).AddColonneClass("danger").AddColonneCellulesClass("danger");
            Tableau.AddColonne(TableauRes.Tableau1ColonneElec);
            Tableau.AddColonne(TableauRes.Tableau1ColonneAutre);
            Tableau.AddColonne(TableauRes.Tableau1ColonneTeleReleve);
            Tableau.AddColonne(TableauRes.Tableau1ColonneProfile);

            Tableau.AddLigne(TableauRes.LMargeBrute).Symbole("€").DefaultValue("100").
                AddChildLigne(TableauRes.LMargeBruteSem1).AddLigneClass("success").Symbole("$").DefaultValue("130").
                AddLigne(TableauRes.LMargeBruteSem2).
                AddLigne(TableauRes.LMargeBruteSem3);

            Tableau.AddLigne(TableauRes.LMargeBruteSansTacite).AddLigneClass("SuperStyle");
            Tableau.AddLigne(TableauRes.LMargeExtreme).Symbole("€").Format(EnumFormat.Marge).AddLigneClass("warning")
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
                // Syntaxe simple
                test.AddValue(Tableau, new TableauValeur(Tableau.Colonne(TableauRes.Tableau1ColonneElec), Tableau.Ligne(item.Item1), item.Item2));

                // Syntaxe avec constructeur et détail
                test.AddValue(Tableau,
                                new TableauValeur(Tableau.Colonne(TableauRes.Tableau1ColonneTotal), Tableau.Ligne(item.Item1), item.Item2)
                                {
                                    DefaultValue = "0",
                                    CelluleClass = "warning",
                                    Format = EnumFormat.Marge,
                                });

                // Syntaxe complexe -- Ne garanti pas le minimum pour faire fonctionner le tableau
                test.AddValue(Tableau,
                                new TableauValeur()
                                {
                                    Colonne = Tableau.Colonne(TableauRes.Tableau1ColonneGaz),
                                    Ligne = Tableau.Ligne(item.Item1),
                                    Value = item.Item2,
                                    DefaultValue = "0",
                                    CelluleClass = "success",
                                    Format = EnumFormat.Marge,
                                });
            }

            Tableau.Values = Tableau.Values.Concat(test).ToDictionary(x => x.Key, y=>  y.Value);
        }
        
    }
}