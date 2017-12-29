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
        /// Méthode permettant de définir une valeur
        /// </summary>
        /// <param name="tableauValeur"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TableauValeur SetLink(this TableauValeur tableauValeur, string controller, string action = "", string[] filter = null)
        {
            string linkControler = string.IsNullOrEmpty(controller) ? "" : $"/{controller}";
            string linkAction = string.IsNullOrEmpty(action)? "": $"/{action}";
            string linkFilter = "";

            tableauValeur.TableauLinkUrl = $"{linkControler}{linkAction}{linkFilter}";
            
            return tableauValeur;
        }
        
    }
}