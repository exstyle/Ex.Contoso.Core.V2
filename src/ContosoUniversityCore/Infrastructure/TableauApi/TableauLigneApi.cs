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
        /// Récupère la ligne du tableau pour un nom donnée.
        /// </summary>
        /// <param name="tableau">Tableau</param>
        /// <param name="nameLine">Nom de la ligne</param>
        /// <param name="position">Utile sur plusieurs lignes ont le même nom</param>
        /// <returns></returns>
        public static Ligne Ligne(this Tableau tableau, string nameLine, int position = 0)
        {
            return tableau.Lignes.Where(x => x.NomLigne == nameLine).Skip(position).FirstOrDefault();
        }

        /// <summary>
        /// Méthode permettant d'ajouter une ligne
        /// </summary>
        /// <param name="tableau">Tableau de référence pour ajouter une ligne</param>
        /// <param name="name"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Méthode permettant de setter une value par default
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Ligne SetDefaultValue(this Ligne ligne, string value)
        {
            ligne.DefaultValue = value;

            return ligne;
        }

        /// <summary>
        /// Permet de donner un symbole par défault pour la ligne courante
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="symbole"></param>
        /// <returns></returns>
        public static Ligne SetSymbole(this Ligne ligne, string symbole)
        {
            ligne.Symbole = symbole;
            return ligne;
        }

        /// <summary>
        /// Permet de donner un format pour la ligne courante
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Ligne SetFormat(this Ligne ligne, EnumFormat format)
        {
            ligne.Format = format;
            return ligne;
        }

        /// <summary>
        /// Permet d'ajouter une class à la ligne (<tr>)
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static Ligne AddLigneClass(this Ligne ligne, string style)
        {
            ligne.LigneClass = style;
            return ligne;
        }

        /// <summary>
        /// Méthode permettant de donner une classe aux cellules appartenant à cette ligne
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static Ligne AddLigneCellulesClass(this Ligne ligne, string style)
        {
            ligne.LigneCelluleClass = style;
            return ligne;
        }

        /// <summary>
        /// Méthode permettant de mettre une classe où une autre sous condition
        /// Remplace la class qui aurait pu être mis
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="classTrue"></param>
        /// <param name="classFalse"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Ligne SetConditionalClass(this Ligne ligne,
                string classTrue,
                string classFalse,
                Func<double, bool> predicate)
        {
            ligne.ClassTrue = classTrue;
            ligne.ClassFalse = classFalse;
            ligne.CelulleClassPredicate = predicate;

            return ligne;
        }
        
    }
}