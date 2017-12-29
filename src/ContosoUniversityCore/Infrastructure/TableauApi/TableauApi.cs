using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CSharp.Test.TableauApi.Models;
using ContosoUniversityCore.Infrastructure.TableauApi.Models.Enums;
using Microsoft.AspNetCore.Mvc.Routing;

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
        /// Méthode pour mettre le titre du tableau
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static Tableau SetTitle(this Tableau tableau, string title)
        {
            tableau.Title = title;
            return tableau;
        }

        /// <summary>
        /// Méthode pour mettre le symbole du tableau
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="symbole"></param>
        /// <returns></returns>
        public static Tableau SetSymbole (this Tableau tableau, string symbole)
        {
            tableau.Symbole = symbole;
            return tableau;
        }

        /// <summary>
        /// Méthode pour mettre le format du tableau
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Tableau SetFormat(this Tableau tableau, EnumFormat format)
        {
            tableau.Format = format;
            return tableau;
        }

        /// <summary>
        /// Méthode pour mettre les valeur par default du tableau
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Tableau SetDefaultValue(this Tableau tableau, string defaultValue)
        {
            tableau.DefaultValue = defaultValue;
            return tableau;
        }

        /// <summary>
        /// Méthode permettant de déclarer une class
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static Tableau SetTableauClass(this Tableau tableau, string style)
        {
            tableau.TClass = style;
            return tableau;
        }

        /// <summary>
        /// Méthode permettant de déclarer une class
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static Tableau AddTableauClass(this Tableau tableau, string style)
        {
            tableau.TClass += $" {style}";
            return tableau;
        }
        
        public static Tableau SetTableauCelluleClass(this Tableau tableau, string style)
        {
            tableau.TableauCelluleClass = style;
            return tableau;
        }

        /// <summary>
        /// Méthode permettant de déclarer une class
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static Tableau SetTHeadClass(this Tableau tableau, string style)
        {
            tableau.THeadClass = style;
            return tableau;
        }

        /// <summary>
        /// Méthode permettant de déclarer une class
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static Tableau SetTBodyClass(this Tableau tableau, string style)
        {
            tableau.TBodyClass = style;
            return tableau;
        }


        /// <summary>
        /// Méthode permettant de mettre une classe où une autre sous condition
        /// Remplace la class qui aurait pu être mis
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="classTrue"></param>
        /// <param name="classFalse"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Tableau SetCelulleConditionalClass(this Tableau tableau,
                string classTrue,
                string classFalse,
                Func<double, bool> predicate)
        {
            tableau.ClassTrue = classTrue;
            tableau.ClassFalse = classFalse;
            tableau.CelulleClassPredicate = predicate;

            return tableau;
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
                    // Récupération de la cellule courante
                    var currentCellule = new TableauKeys(colonne.Position, ligne.Position).ToString();

                    // Quel defaultValue prendre
                    var defaultValue = ligne.DefaultValue ?? colonne.DefaultValue ?? tableau.DefaultValue;

                    // Quel default Format prendre
                    var format = EnumFormat.Initial;
                    List<EnumFormat> test = new List<EnumFormat>() { ligne.Format, colonne.Format, tableau.Format };
                    if (test.Where(x => x != EnumFormat.Initial).Any())
                    {
                        format = test.Where(x => x != EnumFormat.Initial).First();
                    }

                    // Quel Symbole ligne prendre
                    if (string.IsNullOrEmpty(ligne.Symbole) && !String.IsNullOrEmpty(tableau.Symbole))
                        ligne.Symbole = tableau.Symbole;

                    // Initialisation des paramètres
                    tableau.Values[currentCellule] = new TableauValeur(colonne, ligne)
                    {
                        Symbole = ligne.Symbole,
                        Format = format,
                        DefaultValue = defaultValue,
                        CelluleClass = ligne.LigneCelluleClass ?? colonne.ColonneCelluleClass ?? tableau.TableauCelluleClass
                    };

                }
            }

        }
            
        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static string Render(this Tableau tableau, int height = 0, int width=500)
        {
            PreparteTableData(tableau);
            return ToHtml(tableau, height, width);
        }

        /// <summary>
        /// Méthode permettant de preparer les données du tableau, une fois celui-ci complété.
        /// </summary>
        /// <param name="tableau"></param>
        public static void PreparteTableData(Tableau tableau)
        {
            foreach (TableauValeur val in tableau.Values.Select(x => x.Value))
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
                else if (val.Format == EnumFormat.Energie)
                {
                    if (isDouble)
                    {
                        val.ValueString = $"{doubleValue:# ###} GWh";
                    }
                }
            }

            foreach (Ligne ligne in tableau.Lignes)
            {
                ligne.NomLigne = $"{new string(' ', ligne.Indentation * 4).Replace(" ", "&nbsp;")}{ligne.NomLigne}";

            }

        }

        /// <summary>
        /// Transformation en Html du tableau.
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static string ToHtml(this Tableau tableau, int height = 0, int width = 500)
        {
            var style = height == 0 ? "" : $"height: {height}px; overflow: auto;";
            string tableauHtml = $"<div class=\"card mb-3\">";
            tableauHtml += $"<p class=\"card-header red white-text small-header\">{ tableau.Title}</p>";
            tableauHtml += $"<div class=\"card-body\" style=\"{style}\">";
            tableauHtml += $"<table class=\"{tableau.TClass}\">";

            tableauHtml += $"<thead class=\"{tableau.THeadClass}\">";
            tableauHtml += $"<tr>";
            tableau.Colonnes.ForEach(x =>
                tableauHtml += $"<th class=\"{x.ColonneClass}\">{x.NomColonne}</th>"
            );

            tableauHtml += $"</tr>";
            tableauHtml += $"</thead>";
            tableauHtml += $"<tbody class=\"{tableau.TBodyClass}\">";
            tableau.Lignes.ForEach(x =>
                {   
                    tableauHtml += $"<tr><td class=\"{x.LigneClass}\">{x.NomLigne}</td>";
                    
                    tableau.Values.Where(y => y.Key.EndsWith($"-{x.Position}")).ToList().ForEach(z =>
                        {
                            string tableauHtmlCelulleContent = $"{z.Value.ValueString}";

                            if (!string.IsNullOrEmpty(z.Value.TableauLinkUrl))
                            {
                                tableauHtmlCelulleContent = $"<a href=\"{z.Value.TableauLinkUrl}\"/>{tableauHtmlCelulleContent}</a>";
                            }

                            tableauHtml += $"<td class=\"{z.Value.CelluleClass}\">{tableauHtmlCelulleContent}</td>";
                        });

                    tableauHtml += $"</tr>";
                }
            );

            tableauHtml += $"</tbody>";
            tableauHtml += $"</table>";

            return tableauHtml;
        }

    }
}