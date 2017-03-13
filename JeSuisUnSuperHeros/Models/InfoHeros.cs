using Microsoft.Bot.Builder.FormFlow;
using System;

namespace JeSuisUnSuperHeros.Models
{
    [Serializable]
    public class InfoHeros
    {

        [Prompt("Votre prénom?")]
        public string Prenom { get; set; }

        [Prompt("Quel est votre nom?")]
        public string Nom { get; set; }
    }
}