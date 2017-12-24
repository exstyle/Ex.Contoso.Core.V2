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
    public  static partial class TableauApi
    {
        
        #region < Colonne >

        /// <summary>
        /// Méthode retournant une colonne du tableau par son nom
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="name"></param>
        /// <param name="position">Si plusieurs colonnes ont le même nom, on peut retrouver la position</param>
        /// <returns></returns>
        public static Colonne Colonne(this Tableau tableau, string name, int position = 0)
        {
            return tableau.Colonnes.Where(x => x.NomColonne == name).Skip(position).FirstOrDefault();
        }

        /// <summary>
        /// Méthode permettant d'ajouter une colonne
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="colonne"></param>
        /// <returns></returns>
        public static Colonne AddColonne(this Tableau tableau, string colonne = "")
        {

            Colonne currentColonne = new Colonne(colonne);
            if (tableau.Colonnes == null)
                tableau.Colonnes = new List<Colonne>();

            currentColonne.Tableau = tableau;
            currentColonne.Position = tableau.Colonnes.Count;
            tableau.Colonnes.Add(currentColonne);

            return currentColonne;

        }

        /// <summary>
        /// Méthode permettant d'ajouter une colonne
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="colonne"></param>
        /// <returns></returns>
        public static Colonne AddColonne(this Colonne colonne, string colonneNom = "")
        {
            Colonne newColonne = new Colonne(colonneNom)
            {
                Position = colonne.Position + 1,
                Tableau = colonne.Tableau
            };

            colonne.Tableau.Colonnes.Add(newColonne);

            return newColonne;

        }

        /// <summary>
        /// Méthode permettant d'ajouter une class à la colonne (th)
        /// </summary>
        /// <param name="colonne"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static Colonne AddColonneClass(this Colonne colonne, string style)
        {
            colonne.ColonneClass = style;
            return colonne;
        }

        /// <summary>
        /// Méthode permetttant d'ajouter une classe à toutes les celulles de la colonne
        /// </summary>
        /// <param name="colonne"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static Colonne AddColonneCellulesClass(this Colonne colonne, string style)
        {
            colonne.ColonneCelluleClass = style;
            return colonne;
        }
        
        /// <summary>
        /// Méthode permettant de mettre une valeur par default pour la colonne...
        /// </summary>
        /// <param name="colonne"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Colonne SetDefaultValue(this Colonne colonne, string value)
        {
            colonne.DefaultValue = value;

            return colonne;
        }

        /// <summary>
        /// Méthode permettant de mettre une classe où une autre sous condition
        /// Remplace la class qui aurait pu être mis
        /// </summary>
        /// <param name="colonne"></param>
        /// <param name="classTrue"></param>
        /// <param name="classFalse"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
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

    }
}