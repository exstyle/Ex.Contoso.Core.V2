using ContosoUniversityCore.Infrastructure.TableauApi.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Test.TableauApi.Models
{
    public class TableauValeur
    {
        public TableauValeur(Colonne colonne, Ligne ligne, Double value)
        {
            Colonne = colonne;
            Ligne = ligne;
            Value = value;
        }

        public Colonne Colonne { get; set; }

        public Ligne Ligne { get; set; }

        public double Value { get; set; }

        public string DefaultValue { get; set; }

        public EnumFormat? Format { get; set; }
    }
}
