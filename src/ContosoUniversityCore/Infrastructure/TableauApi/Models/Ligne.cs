using ContosoUniversityCore.Infrastructure.TableauApi.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Test.TableauApi.Models
{
    public class Ligne
    {
        public Ligne(string nomLigne)
        {
            NomLigne = nomLigne;
        }

        public string NomLigne { get; set; }

        public int Position { get; set; }

        public int Indentation { get; set; }

        public string LigneClass { get; set; }

        public string LigneCelluleClass { get; set; }

        public Tableau Tableau { get; set; }

        public string DefaultValue { get; set; }

        public string Symbole { get; set; }

        public EnumFormat? Format { get; set; }

        public Func<double, bool> CelulleClassPredicate { get; set; }

        public string ClassTrue { get; set; }

        public string ClassFalse { get; set; }

    }
}
