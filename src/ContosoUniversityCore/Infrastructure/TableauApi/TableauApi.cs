using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CSharp.Test.TableauApi.Models;
using ContosoUniversityCore.Infrastructure.TableauApi.Models.Enums;

namespace CSharp.Test.TableauApi
{
    public static class TableauApi
    {
        #region < Ligne >
        public static Ligne Ligne(this Tableau tableau, string name, int position = 0)
        {
            try
            {
                return tableau.Lignes.Where(x => x.NomLigne == name).Skip(position).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public static Ligne AddLigne(this Tableau tableau, string name)
        {
            if (tableau.Lignes == null)
                tableau.Lignes = new List<Ligne>();

            Ligne ligne = new Ligne(name);
            ligne.Position = tableau.Lignes.Count;
            ligne.Indentation = 0;
            ligne.Tableau = tableau;
            tableau.Lignes.Add(ligne);

            return ligne;

        }

        public static Ligne AddLigne(this Ligne ligne, string name)
        {
            Ligne currentLigne = new Ligne(name);
            currentLigne.Position = ligne.Position + 1;
            currentLigne.Indentation = ligne.Indentation;
            currentLigne.Tableau = ligne.Tableau;
            ligne.Tableau.Lignes.Add(currentLigne);

            return currentLigne;

        }

        public static Ligne AddChildLigne(this Tableau tableau, string name)
        {
            Ligne ligne = new Ligne(name);
            var last = tableau.Lignes.Last();
            ligne.Position = last.Position + 1;
            ligne.Indentation = last.Indentation + 1;
            ligne.Tableau = tableau;
            tableau.Lignes.Add(ligne);

            return ligne;

        }

        public static Ligne AddChildLigne(this Ligne ligne, string name)
        {
            Ligne currentLigne = new Ligne(name);
            currentLigne.Tableau = ligne.Tableau;
            currentLigne.Position = ligne.Position + 1;
            currentLigne.Indentation = ligne.Indentation + 1;
            ligne.Tableau.Lignes.Add(currentLigne);

            return currentLigne;
            
        }

        public static Ligne DefaultValue(this Ligne ligne, string value)
        {
            ligne.DefaultValue = value;

            return ligne;
        }

        public static Ligne Symbole(this Ligne ligne, string symbole)
        {
            ligne.Symbole = symbole;
            return ligne;
        }

        public static Ligne Format(this Ligne ligne, EnumFormat format)
        {
            ligne.Format = format;
            return ligne;
        }

        public static Ligne AddLigneClass(this Ligne ligne, string style)
        {
            ligne.LigneClass = style;
            return ligne;
        }

        public static Ligne AddLigneCellulesClass(this Ligne ligne, string style)
        {
            ligne.LigneCelluleClass = style;
            return ligne;
        }

        public static Ligne ToLigne(this string name)
        {
            return new Ligne(name);
        }

        #endregion

        #region < Colonne >
        
        public static Colonne ToCol(this string name)
        {
            return new Colonne(name);
        }

        public static Colonne AddColonneClass(this Colonne colonne, string style)
        {
            colonne.ColonneClass = style;
            return colonne;
        }

        public static Colonne AddColonneCellulesClass(this Colonne colonne, string style)
        {
            colonne.ColonneCelluleClass = style;
            return colonne;
        }

        public static Colonne Colonne(this Tableau tableau, string name, int position = 0)
        {
            try
            {
                return tableau.Colonnes.Where(x => x.NomColonne == name).Skip(position).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        
        public static Colonne AddColonne(this Tableau tableau, string colonne = "")
        {
            Colonne currentColonne = colonne.ToCol();
            if (tableau.Colonnes == null)
                tableau.Colonnes = new List<Colonne>();

            currentColonne.Position = tableau.Colonnes.Count;
            tableau.Colonnes.Add(currentColonne);

            return currentColonne;

        }

        public static Colonne SetConditionalClass(this Colonne colonne,
                string classTrue,
                string classFalse,
                Func<double, bool> predicate)
        {
            colonne.ClassTrue = classTrue;
            colonne.ClassFalse = classFalse;
            colonne.CelulleClassPredicate = predicate;
            
            return colonne;
        }

        #endregion

        #region < Values >

        public static TableauValeur Value (this Tableau tableau, string colonneName, string ligneName)
        {
            Colonne colonne = tableau.Colonne(colonneName);
            Ligne ligne = tableau.Ligne(ligneName);
            return tableau.Values.Where(x => x.Key == $"{colonne.Position}-{ligne.Position}").Single().Value;
        }

        public static TableauValeur SetFormat(this TableauValeur tableauValeur, EnumFormat format)
        {
            tableauValeur.Format = format;
            return tableauValeur;
        }

        public static TableauValeur SetValeur(this TableauValeur tableauValeur, double value)
        {
            tableauValeur.Value = value;
            return tableauValeur;
        }

        public static TableauValeur SetSymbole(this TableauValeur tableauValeur, string symbole)
        {
            tableauValeur.Symbole = symbole;
            return tableauValeur;
        }

        public static TableauValeur SetCelluleClass (this TableauValeur tableauValeur, string style)
        {
            tableauValeur.CelluleClass = style;
            return tableauValeur;
        }

        public static TableauValeur SetConditionalClass(this TableauValeur tableauValeur,
                string classTrue,
                string classFalse,
                Func<TableauValeur,bool> predicate)
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

        #endregion  

        public static IEnumerable<TableauValeur> TableauValeur(this Tableau tableau, Colonne colonne, Ligne ligne)
        {
            return tableau.Values.Where(x => x.Key == $"{colonne.Position}-{ligne.Position}").Select(z => z.Value);
        }

        /// <summary>
        ///  Gérer les valeurs par défaut ici
        /// </summary>
        /// <param name="tableau"></param>
        public static void Generate(this Tableau tableau)
        {
            tableau.Values = new Dictionary<string, TableauValeur>();
            foreach (Colonne colonne in tableau.Colonnes.Skip(1))
            {
                foreach (Ligne ligne in tableau.Lignes)
                {
                    var currentCellule = new TableauKeys(colonne.Position, ligne.Position).ToString();
                    
                    // Default Value
                    if ((ligne.DefaultValue == null) && (!string.IsNullOrEmpty(tableau.DefaultValue)))
                        ligne.DefaultValue = tableau.DefaultValue;

                    // Symbole ligne
                    if (string.IsNullOrEmpty(ligne.Symbole) && !String.IsNullOrEmpty(tableau.Symbole))
                        ligne.Symbole = tableau.Symbole;
                    
                    // Enum Format
                    if (ligne.Format == null && tableau.Format != null)
                        ligne.Format = tableau.Format;
                    
                    tableau.Values[currentCellule] = new TableauValeur(colonne, ligne)
                    {
                        Symbole = ligne.Symbole,
                        Format = ligne.Format,
                        DefaultValue = ligne.DefaultValue,
                        CelluleClass = string.Concat(ligne.LigneCelluleClass, colonne.ColonneCelluleClass)
                    };

                }
            }

        }
        
        public static void AddValue(this Tableau tab, Colonne colonne, Ligne ligne, double value)
        {
            tab.Values[new TableauKeys(colonne.Position, ligne.Position).ToString()] = new TableauValeur(colonne, ligne, value);
        }

        public static void AddValue(this Dictionary<string, TableauValeur> dictionary, Tableau tab, TableauValeur valeur)
        {
            tab.Values[new TableauKeys(valeur.Colonne.Position, valeur.Ligne.Position).ToString()] = valeur;
        }

        public static void AddValue(this Dictionary<string, TableauValeur> dictionary, Tableau tableau, Colonne colonne, Ligne ligne, double value)
        {
            tableau.AddValue(colonne, ligne, value);
        }

        public static string ToHtml(this Tableau tableau, int height = 300, int width = 500)
        {
            // Ici, le tableau est généré et les valeurs ont été généré.
            // Il serait peut-être plus propre de tout déterminer ici, les valeurs et format attendu.
            // En dehors des boucles html, où l'on perd en visibilité.
            
            foreach (TableauValeur val in tableau.Values.Select(x=> x.Value))
            {
                // Value to string
                if (val.Value == null)
                    val.ValueString = val.DefaultValue;
                else val.ValueString = val.Value.ToString();

                double doubleValue;
                var isDouble = (double.TryParse(val.ValueString, out doubleValue));

                if (isDouble)
                {
                    //Class cellule
                    if (val.CelulleClassPredicate != null)
                    {
                        val.CelluleClass = val.CelulleClassPredicate();
                    }
                    else if (val.Ligne.CelulleClassPredicate != null)
                    {
                        val.CelluleClass = val.Ligne.CelulleClassPredicate(doubleValue) ?
                                                        val.Ligne.ClassTrue :
                                                        val.Ligne.ClassFalse;
                    }
                    else if (val.Colonne.CelulleClassPredicate != null)
                    {
                        val.CelluleClass = val.Colonne.CelulleClassPredicate(doubleValue) ?
                                                        val.Colonne.ClassTrue :
                                                        val.Colonne.ClassFalse;
                    }
                    
                }

                // Value
                if (val.Format == EnumFormat.Custom)
                {
                    val.ValueString = $"{val.ValueString} {val.Symbole}".TrimEnd();
                }
                else if (val.Format == EnumFormat.Marge)
                {
                    if (isDouble)
                        val.ValueString = $"{doubleValue:C}";
                }
            }
            
            var style = height == 0 ? "" : $"height: {height}px; overflow: auto;";
            string tableauHtml = $"<div class=\"card mb-3\">";
            tableauHtml += $"<p class=\"card-header red white-text small-header\">{ tableau.Title}</p>";
            tableauHtml += $"<div class=\"card-body\" style=\"{style}\">";
            tableauHtml += $"<table class=\"table table-sm table-dark\">";

            tableauHtml += $"<thead>";
            tableauHtml += $"<tr>";
            tableau.Colonnes.ForEach(x =>
                tableauHtml += $"<th class=\"{x.ColonneClass}\">{x.NomColonne}</th>"
            );

            tableauHtml += $"</tr>";
            tableauHtml += $"</thead>";
            tableauHtml += $"<tbody>";
            tableau.Lignes.ForEach(x =>
                {
                    tableauHtml += $"<tr class=\"{x.LigneClass}\"><td>{x.NomLigne}</td>";
                    tableau.Values.Where(y => y.Key.EndsWith($"-{x.Position}")).ToList().ForEach(z =>
                        {
                            tableauHtml += $"<td class=\"{z.Value.CelluleClass}\">";
                            tableauHtml += $"{z.Value.ValueString}";   
                        });

                    tableauHtml += $"</tr>";
                }
            );
            tableauHtml += $"</tbody>";
            tableauHtml += $"</table>";

            //string chart = $"<div style='height:{height};width:{width}'>" + "\n";
            //chart += $"<canvas id='{chartID}'></canvas>" + "\n";
            //chart += "</div>" + "\n";
            //chart += "<script>" + "\n";
            //chart += $"var {chartID}_ctx = document.getElementById('{chartID}').getContext('2d');" + "\n";
            //chart += $"var {chartID}_Chart = new Chart({chartID}_ctx, {{" + "\n";
            //chart += MyConverter.ToJSON(chartTypeObject) + "\n";
            //chart += "});" + "\n";
            //chart += "</script>" + "\n";

            return tableauHtml;
        }

    }
}