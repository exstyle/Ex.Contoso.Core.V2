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
        /// Méthode permettant d'ajouter une ligne
        /// </summary>
        /// <param name="tableau">Tableau de référence pour ajouter une ligne</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Ligne AddLigne(this Tableau tableau, string name, int? indentation = null)
        {
            if (tableau.Lignes == null)
                tableau.Lignes = new List<Ligne>();

            Ligne ligne = new Ligne(name);
            ligne.Position = tableau.Lignes.Count;
            ligne.Indentation = indentation ?? 0;
            ligne.Tableau = tableau;
            tableau.Lignes.Add(ligne);

            return ligne;

        }

        /// <summary>
        /// Méthode permettant d'ajouter une ligne au même niveau que la ligne de référence
        /// </summary>
        /// <param name="ligne">Ligne de reference pour ajouter une ligne sibling</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Ligne AddLigne(this Ligne ligne, string name)
        {
            Ligne currentLigne = new Ligne(name);
            currentLigne.Position = ligne.Position + 1;
            currentLigne.Indentation = ligne.Indentation;
            currentLigne.Tableau = ligne.Tableau;
            ligne.Tableau.Lignes.Add(currentLigne);

            return currentLigne;

        }
        
        /// <summary>
        /// Méthode permettant d'ajouter une ligne enfant au tableu courant (ne devrait pas servir)
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="name"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Méthode permettant d'ajouter une ligne enfant à la ligne courante
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Ligne AddChildLigne(this Ligne ligne, string name)
        {
            Ligne currentLigne = new Ligne(name);
            currentLigne.Tableau = ligne.Tableau;
            currentLigne.Position = ligne.Position + 1;
            currentLigne.Indentation = ligne.Indentation + 1;
            ligne.Tableau.Lignes.Add(currentLigne);

            return currentLigne;

        }

        public static Ligne AddLigneTitre1(this Tableau tableau, string name, int position = 0)
        {
            return tableau.AddLigne(name).
                ResetLigne().
                SetLigneClass("h3").
                SetIndentation(position);
        }

        public static Ligne AddLigneTitre2(this Tableau tableau, string name, int indentation = 1)
        {
            return tableau.AddLigne(name)
                .ResetLigne()
                .SetLigneClass("h4")
                .SetIndentation(indentation);
        }

        public static Ligne AddLigneTitre3(this Tableau tableau, string name, int indentation = 2)
        {
            return tableau.AddLigne(name)
                .ResetLigne()
                .SetLigneClass("h5")
                .SetIndentation(indentation);
        }

        public static Ligne AddLigneTitre4(this Tableau tableau, string name, int indentation = 3)
        {
            return tableau.AddLigne(name)
                .ResetLigne()
                .SetLigneClass("h6")
                .SetIndentation(indentation);
        }

        public static Ligne AddLigneTitre1(this Ligne ligne, string name, int indentation = 0)
        {
            return ligne.AddLigne(name)
                .ResetLigne()
                .SetLigneClass("h3")
                .SetIndentation(indentation);
        }

        public static Ligne AddLigneTitre2(this Ligne ligne, string name, int indentation = 1)
        {
            ligne.AddLigne(name)
                .SetLigneClass("h4")
                .ResetLigne()
                .SetIndentation(indentation);
            return ligne;
        }

        public static Ligne AddLigneTitre3(this Ligne ligne, string name, int indentation = 2)
        {
            ligne.AddLigne(name)
                .ResetLigne()
                .SetLigneClass("h5")
                .SetIndentation(indentation);
            return ligne;
        }

        public static Ligne AddLigneTitre4(this Ligne ligne, string name, int indentation = 3)
        {
            ligne
                .AddLigne(name)
                .ResetLigne()
                .SetLigneClass("h6")
                .SetIndentation(indentation);
            return ligne;
        }
        
    }
}