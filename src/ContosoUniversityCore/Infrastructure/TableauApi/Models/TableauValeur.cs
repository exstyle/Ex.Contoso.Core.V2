using ContosoUniversityCore.Infrastructure.TableauApi.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Test.TableauApi.Models
{
    public class TableauValeur
    {
        public TableauValeur()
        {

        }

        public TableauValeur(Colonne colonne, Ligne ligne)
        {
            Colonne = colonne;
            Ligne = ligne;
        }

        public TableauValeur(string colonne, string ligne)
        {
            ColonneString = colonne;
            LigneString = ligne;
        }

        public TableauValeur(Colonne colonne, Ligne ligne, double? value)
        {
            Colonne = colonne;
            Ligne = ligne;
            Value = value;
        }

        public TableauValeur(string colonne, string ligne, double? value)
        {
            ColonneString = colonne;
            LigneString = ligne;
            Value = value;
        }

        public Colonne Colonne { get; set; }

        public Ligne Ligne { get; set; }


        public string ColonneString { get; set; }

        public string LigneString { get; set; }


        public double? Value { get; set; }

        public string ValueString { get; set; }

        public string DefaultValue { get; set; } = "";

        public string CelluleClass { get; set; }

        public string Symbole { get; set; }

        public EnumFormat? Format { get; set; }

        public Func<string> CelulleClassPredicate { get; set; }

        public TableauLink TableauLink { get; set; }

        public string TableauLinkUrl { get; set; }

    }

    public class TableauLink
    {
        public string Controller { get; set; }

        public string Action { get; set; }

        public string Filter { get; set; }
    }
}
