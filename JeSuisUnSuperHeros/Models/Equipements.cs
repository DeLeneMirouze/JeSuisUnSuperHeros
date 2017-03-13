using Microsoft.Bot.Builder.FormFlow;
using System;

namespace JeSuisUnSuperHeros.Models
{
    [Serializable]
    public class Equipements
    {
        public bool Cape { get; set; }

        public PositionCulotte PositionCulotte { get; set; }

        public SuperPouvoirs SuperPouvoirs { get; set; }

        public String Arme { get; set; }
    }

    [Serializable]
    public enum PositionCulotte
    {
        SurCollant,
        SousCollant
    }

    [Serializable]
    public enum SuperPouvoirs
    {
        [Prompt("Etre invisible")]
        Invisibilite,
        [Prompt("Superforce")]
        SuperForce,
        [Prompt("Vue rayon X")]
        VueRayonX,
        [Prompt("Savoir monter un meuble Ikea")]
        SavoirMonterMeubleIkea,
        [Prompt("Vitesse supersonique")]
        SuperVitesse,
        [Prompt("Toujours un rab de frites")]
        RabDeFritesALaCantine,
        [Prompt("Congèle tout")]
        CongelerTout
    }

    [Serializable]
    public enum Armes
    {
        [Prompt("Laser super dangereux")]
        SuperLaser,
        [Prompt("Sabre en griffe de Wolferine")]
        SabreAdamantium,
        [Prompt("Rayon chatouilleur")]
        RayonChatouilleur,
        [Prompt("Bottes avec détecteur d'arrière-train")]
        BotteMagique,
        [Prompt("Bombe à eau")]
        BombeAEau,
        [Prompt("Champ de forces")]
        ChampForces
    }
}