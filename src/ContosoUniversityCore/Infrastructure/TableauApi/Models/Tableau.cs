using ContosoUniversityCore.Infrastructure.TableauApi.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Test.TableauApi.Models
{
    public class Tableau
    {
        // Objets
        public List<Ligne> Lignes { get; set; }

        public List<Colonne> Colonnes { get; set; }
        
        public Dictionary<string,TableauValeur> Values { get; set; }

        // Mise en forme et propriétés
        public string TClass { get; set; } = "table table-sm";

        public string THeadClass { get; set; }

        public string TBodyClass { get; set; }
        
        public string Title { get; set; }

        public string Symbole { get; set; }

        public EnumFormat Format { get; set; } = EnumFormat.Initial;

        public string DefaultValue { get; set; }

        public Func<double, bool> CelulleClassPredicate { get; set; }

        public string ClassTrue { get; set; }

        public string ClassFalse { get; set; }

        public string TableauCelluleClass { get; set; }
    }
    
    public class TableauKeys
    {
        public TableauKeys(int colonne, int ligne)
        {
            ColonneKey = colonne;
            LigneKey= ligne;
        }

        public int ColonneKey { get; set; }

        public int LigneKey { get; set; }

        public override string ToString()
        {
            return $"{ColonneKey}-{LigneKey}";
        }

    }
}
