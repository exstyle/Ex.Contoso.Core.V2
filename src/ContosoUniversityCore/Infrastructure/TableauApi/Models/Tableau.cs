using ContosoUniversityCore.Infrastructure.TableauApi.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Test.TableauApi.Models
{
    public class Tableau
    {
        public List<Ligne> Lignes { get; set; }

        public List<Colonne> Colonnes { get; set; }
        
        public Dictionary<string,TableauValeur> Values { get; set; }

        public string THeadClass { get; set; }

        public string TBodyClass { get; set; }

        public string Title { get; set; }

        public string Symbole { get; set; }

        public EnumFormat? Format { get; set; }
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
