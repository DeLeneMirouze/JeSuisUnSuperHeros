using System;
using System.Collections.Generic;

namespace JeSuisUnSuperHeros.Models
{
    public static class Generator
    {
        #region Constructeurs
        private static Dictionary<string, string> _firstDictionary = new Dictionary<string, string>();
        private static Dictionary<string, string> _lastDictionary = new Dictionary<string, string>();
        private static Dictionary<int, string> _missionsDictionary = new Dictionary<int, string>();
        static Random rnd = new Random();

        static Generator()
        {
            #region _firstDictionary
            _firstDictionary.Add("a", "Mister");
            _firstDictionary.Add("b", "Miss");
            _firstDictionary.Add("c", "Chuck");
            _firstDictionary.Add("d", "Magic");
            _firstDictionary.Add("e", "Super");
            _firstDictionary.Add("f", "Charles-Antoine");
            _firstDictionary.Add("g", "Mega");
            _firstDictionary.Add("h", "Maxi");
            _firstDictionary.Add("i", "Cool");
            _firstDictionary.Add("j", "Super");
            _firstDictionary.Add("k", "Black");
            _firstDictionary.Add("l", "Madame");
            _firstDictionary.Add("m", "Drusila");
            _firstDictionary.Add("n", "Captain");
            _firstDictionary.Add("o", "Be-Bop");
            _firstDictionary.Add("p", "Mysterious");
            _firstDictionary.Add("q", "Super");
            _firstDictionary.Add("r", "Hyper");
            _firstDictionary.Add("s", "Betonner");
            _firstDictionary.Add("t", "The");
            _firstDictionary.Add("u", "Prince");
            _firstDictionary.Add("v", "Monsieur");
            _firstDictionary.Add("w", "Mini");
            _firstDictionary.Add("x", "Red");
            _firstDictionary.Add("y", "Black");
            _firstDictionary.Add("z", "Mad");
            #endregion

            #region _lastDictionary
            _lastDictionary.Add("a", "Mygale");
            _lastDictionary.Add("b", "Léon (nettoyeur)");
            _lastDictionary.Add("c", "Man");
            _lastDictionary.Add("d", "Le Desosseur");
            _lastDictionary.Add("e", "Hulk");
            _lastDictionary.Add("f", "Magic");
            _lastDictionary.Add("g", "Shark");
            _lastDictionary.Add("h", "Pistolet d'Or");
            _lastDictionary.Add("i", "Lampadaire");
            _lastDictionary.Add("j", "Coyotte");
            _lastDictionary.Add("k", "T.");
            _lastDictionary.Add("l", "Fatal Girl");
            _lastDictionary.Add("m", "Spider");
            _lastDictionary.Add("n", "Power");
            _lastDictionary.Add("o", "Widows");
            _lastDictionary.Add("p", "Boy");
            _lastDictionary.Add("q", "Quantic Cocktail");
            _lastDictionary.Add("r", "Di Gorgonzolla");
            _lastDictionary.Add("s", "Norris");
            _lastDictionary.Add("t", "French TV");
            _lastDictionary.Add("u", "The Killer");
            _lastDictionary.Add("v", "Terminator");
            _lastDictionary.Add("w", "Women");
            _lastDictionary.Add("x", "The Destructor");
            _lastDictionary.Add("y", "Slider");
            _lastDictionary.Add("z", "X");
            #endregion

            #region _missionsDictionary
            _missionsDictionary.Add(0, "Papy Boyington doit monter un meuble. Mais c'est écrit en suédois, enfin peut-être");
            _missionsDictionary.Add(1, "Le Président vient d'appeler. Vous avez 24 h pour sauver les otages du train Lille/Bapaume (sud). Comment lui dire qu'aujourd'hui y a piscine?");
            _missionsDictionary.Add(2, "Chuck Norris n'a pas le temps de faire du social avec Super-Vilain. Il lui faut un assistant");
            _missionsDictionary.Add(3, "Le chat de la mère Michel sait monter sur un toit, mais pas redescendre. Vous c'est l'inverse, vous allez bien vous entendre");
            _missionsDictionary.Add(4, "Régler un conflit entre Anabelle, 6 ans (et demi), qui veut regarder 'Princesse Léa' sur Gully et Kévin son grand frère. Spoiler: le 'Hard Rock Metal Festival' n'est pas diffusé sur Gully");
            _missionsDictionary.Add(5, "Le petit Charlie s'est perdu. Sa maman est très inquiète. Sa grande soeur moins");
            _missionsDictionary.Add(6, "Salut, c'est Zézette. Peux tu poser les clefs du camion sur la table basse");
            _missionsDictionary.Add(7, "Votre meilleur(e) ami(e) doit faire de la conduite accompagnée avec sa fille. Malheureusement en pleine crise d'adolescence. L'avenir du monde libre ne tient qu'à vos talents de diplomate");
            _missionsDictionary.Add(8, "Hulk s'est encore trompé de film. Il faut l'arrêter avant qu'il ne débarque dans la scène romantique avec la demande en mariage de Princesse Léa");
            _missionsDictionary.Add(9, "Vous êtes mis au défi de gagner le prochain Grand Prix, mais en marche arrière. Trop facile!!!!");
            _missionsDictionary.Add(10, "Julien doit rendre une dissert hier matin et a besoin d'aide");
            _missionsDictionary.Add(11, "C'est maman, quand tu auras fini de jouer avec ton déguisement, va ranger ta chambre");
            _missionsDictionary.Add(12, "Un développeur de chez Yahoo s'apprête à déployer un nouveau module de sécurité. Oula! faut tout de suite l'arrêter");
            _missionsDictionary.Add(13, "Aider un sympathique couple de personnes âgées à traverser l'avenue. Ben oui, un super héros c'est aussi ça");
            _missionsDictionary.Add(14, "Votre voisin se prend pour un chanteur lyrique. C'est pathétique, vous devez sauver l'univers");
            _missionsDictionary.Add(15, "Le Schmilblic a été perdu dans le code source de votre projet. Bonne chance!"); 
            #endregion
        }
        #endregion

        #region GenerateSuperherosName
        public static string GenerateSuperherosName(InfoHeros infoHeros)
        {
            string first = infoHeros.Prenom.ToLowerInvariant().Substring(0, 1);
            string last = infoHeros.Nom.ToLowerInvariant().Substring(0, 1);

            try
            {
                string superName = string.Concat(_firstDictionary[first], " ", _lastDictionary[last]);
                return superName;
            }
            catch
            {
                return "GrosBidon";
            }
        }
        #endregion

        #region GetMission
        public static string GetMission()
        {
            int index = rnd.Next(0, 16);

            string mission = _missionsDictionary[index];
            mission = mission.Replace(".", "\n\n");

            return mission;
        } 
        #endregion
    }
}