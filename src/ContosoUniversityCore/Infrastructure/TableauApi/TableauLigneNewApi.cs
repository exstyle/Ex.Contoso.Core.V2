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
        /// Méthode permettant d'ajouter une ligne enfant au tableu courant (ne devrait pas servir)
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Ligne NewLine(this Tableau tableau, string name)
        {
            if (tableau.Lignes == null)
                tableau.Lignes = new List<Ligne>();

            Ligne ligne = new Ligne(name);;
            
            ligne.Position = tableau.Lignes.Count;
            ligne.Indentation = 1;
            ligne.Tableau = tableau;
            tableau.Lignes.Add(ligne);

            return ligne;

        }

        /// <summary>
        /// Méthode permettant d'ajouter une ligne enfant au tableu courant (ne devrait pas servir)
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Ligne NewLine(this Ligne ligne, string name)
        {
            Ligne newLigne = new Ligne(name)
            {
                Position = ligne.Position + 1,
                Indentation = 1,

                Tableau = ligne.Tableau
            };
            ligne.Tableau.Lignes.Add(newLigne);

            return newLigne;

        }

        /// <summary>
        /// Méthode permettant d'ajouter une ligne enfant au tableu courant (ne devrait pas servir)
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Ligne NewEmptyLine(this Ligne ligne)
        {
            Ligne newLigne = new Ligne("")
            {
                Position = ligne.Position + 1,
                Indentation = 1,
                DefaultValue = "",
                Tableau = ligne.Tableau
            };
            ligne.Tableau.Lignes.Add(newLigne);

            return newLigne;

        }

        /// <summary>
        /// Méthode permettant d'ajouter une ligne enfant au tableu courant (ne devrait pas servir)
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Ligne NewChildLine(this Ligne ligne, string name)
        {

            var tableau = ligne.Tableau;
            var last = ligne.Tableau.Lignes.Last();

            Ligne newLigne = new Ligne(name);
            newLigne.Position = ligne.Position + 1;
            newLigne.Indentation = 2;
            newLigne.Tableau = tableau;
            tableau.Lignes.Add(newLigne);

            return newLigne;

        }

        /// <summary>
        /// Méthode permettant d'ajouter une ligne enfant au tableu courant (ne devrait pas servir)
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Ligne NewGrandChildrenLine(this Ligne ligne, string name)
        {
            var tableau = ligne.Tableau;
            var last = ligne.Tableau.Lignes.Last();

            Ligne newLigne = new Ligne(name);
            newLigne.Position = ligne.Position + 1;
            newLigne.Indentation = 3;
            newLigne.Tableau = tableau;
            tableau.Lignes.Add(newLigne);

            return newLigne;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static Ligne NewLineTitle(this Tableau tableau, string name, int position = 0)
        {
            return tableau.NewLine(name).
                ResetLigne().
                SetLigneClass("h4").
                SetIndentation(position);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="name"></param>
        /// <param name="indentation"></param>
        /// <returns></returns>
        public static Ligne NewLineTitle(this Ligne ligne, string name, int indentation = 0)
        {
            return ligne.NewLine(name)
                .ResetLigne()
                .SetLigneClass("h4")
                .SetIndentation(indentation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="name"></param>
        /// <param name="indentation"></param>
        /// <returns></returns>
        public static Ligne NewChildLineTitle(this Ligne ligne, string name, int indentation = 1)
        {
            ligne.NewChildLine(name)
                .SetLigneClass("h5")
                .ResetLigne()
                .SetIndentation(indentation);
            return ligne;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="indentation"></param>
        /// <returns></returns>
        public static Ligne SetIndentation(this Ligne ligne, int indentation)
        {
            ligne.Indentation = indentation;
            return ligne;
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
        /// L'objectif de cette méthode est de remettre les paramètres par default.
        /// </summary>
        /// <param name="ligne"></param>
        /// <returns></returns>
        public static Ligne ResetLigne(this Ligne ligne)
        {
            ligne.SetIndentation(0).SetDefaultValue(null).SetSymbole(null).SetFormat(EnumFormat.Initial);
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
        public static Ligne SetLigneClass(this Ligne ligne, string style)
        {
            ligne.LigneClass = style;
            return ligne;
        }

        public static Ligne AddLigneClass(this Ligne ligne, string style)
        {
            ligne.LigneClass += $" {style}";
            return ligne;
        }

        /// <summary>
        /// Méthode permettant de donner une classe aux cellules appartenant à cette ligne
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static Ligne SetLigneCellulesClass(this Ligne ligne, string style)
        {
            ligne.LigneCellulesClass = style;
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