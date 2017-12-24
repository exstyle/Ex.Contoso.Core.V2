using ContosoUniversityCore.Infrastructure.TableauApi.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Test.TableauApi.Models
{
    public class Colonne
    {
        public Colonne(string nomColonne)
        {
            NomColonne = nomColonne;
        }

        public Tableau Tableau { get; set; }

        public string NomColonne { get; set; }

        public int Position { get; set; }

        public string ColonneClass { get; set; }

        public string ColonneCelluleClass { get; set; }

        public EnumFormat Format { get; set; }

        public Func<double, bool> CelulleClassPredicate { get; set; }

        public string ClassTrue { get; set; }

        public string ClassFalse { get; set; }

        public string DefaultValue { get; set; }

    }
}
