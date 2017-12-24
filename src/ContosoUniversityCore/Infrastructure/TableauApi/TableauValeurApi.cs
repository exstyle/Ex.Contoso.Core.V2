using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CSharp.Test.TableauApi.Models;
using ContosoUniversityCore.Infrastructure.TableauApi.Models.Enums;

namespace CSharp.Test.TableauApi
{
    /// <summary>
    /// API pour générer des tableaux de type tableau de bord
    /// chaque valeur de cellule est manuellement indiqué.
    /// Ne convient pas (à voir) pour des valeurs issus d'une seul requête en BDD.
    /// </summary>
    public static partial class TableauApi
    {
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="colonneName"></param>
        /// <param name="ligneName"></param>
        /// <returns></returns>
        public static TableauValeur Value(this Tableau tableau, string colonneName, string ligneName)
        {
            Colonne colonne = tableau.Colonne(colonneName);
            Ligne ligne = tableau.Ligne(ligneName);
            return tableau.Values.Where(x => x.Key == $"{colonne.Position}-{ligne.Position}").Single().Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableauValeur"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static TableauValeur SetFormat(this TableauValeur tableauValeur, EnumFormat format)
        {
            tableauValeur.Format = format;
            return tableauValeur;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableauValeur"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TableauValeur SetValeur(this TableauValeur tableauValeur, double value)
        {
            tableauValeur.Value = value;
            return tableauValeur;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableauValeur"></param>
        /// <param name="symbole"></param>
        /// <returns></returns>
        public static TableauValeur SetSymbole(this TableauValeur tableauValeur, string symbole)
        {
            tableauValeur.Symbole = symbole;
            return tableauValeur;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableauValeur"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static TableauValeur SetCelluleClass(this TableauValeur tableauValeur, string style)
        {
            tableauValeur.CelluleClass = style;
            return tableauValeur;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableauValeur"></param>
        /// <param name="classTrue"></param>
        /// <param name="classFalse"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static TableauValeur SetConditionalClass(this TableauValeur tableauValeur,
                string classTrue,
                string classFalse,
                Func<TableauValeur, bool> predicate)
        {

            string testFunctionLocal(string t, string f, Func<TableauValeur, bool> p)
            {
                if (p(tableauValeur))
                {
                    return t;
                }
                return f;
            }

            Func<string> tests = () =>
            {
                if (predicate(tableauValeur))
                {
                    return classTrue;
                }
                return classFalse;
            };

            tableauValeur.CelulleClassPredicate = tests;

            return tableauValeur;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="colonne"></param>
        /// <param name="ligne"></param>
        /// <returns></returns>
        public static IEnumerable<TableauValeur> TableauValeur(this Tableau tableau, Colonne colonne, Ligne ligne)
        {
            return tableau.Values.Where(x => x.Key == $"{colonne.Position}-{ligne.Position}").Select(z => z.Value);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="colonne"></param>
        /// <param name="ligne"></param>
        /// <param name="value"></param>
        public static void AddValue(this Tableau tab, Colonne colonne, Ligne ligne, double value)
        {
            tab.Values[new TableauKeys(colonne.Position, ligne.Position).ToString()] = new TableauValeur(colonne, ligne, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="tab"></param>
        /// <param name="valeur"></param>
        public static void AddValue(this Dictionary<string, TableauValeur> dictionary, Tableau tab, TableauValeur valeur)
        {
            tab.Values[new TableauKeys(valeur.Colonne.Position, valeur.Ligne.Position).ToString()] = valeur;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="tableau"></param>
        /// <param name="colonne"></param>
        /// <param name="ligne"></param>
        /// <param name="value"></param>
        public static void AddValue(this Dictionary<string, TableauValeur> dictionary, Tableau tableau, Colonne colonne, Ligne ligne, double value)
        {
            tableau.AddValue(colonne, ligne, value);
        }
        
    }
}