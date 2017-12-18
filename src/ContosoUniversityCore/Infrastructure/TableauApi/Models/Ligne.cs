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

        public string Class { get; set; }

        public Tableau Tableau { get; set; }

        public double DefaultValue { get; set; } = 0;

        public string Symbole { get; set; }

        public EnumFormat? Format { get; set; }
    }
}
