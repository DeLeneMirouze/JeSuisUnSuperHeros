#region using
using JeSuisUnSuperHeros.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
#endregion

namespace JeSuisUnSuperHeros.Dialogs
{
    [Serializable]
    public class EquipementDialog : IDialog<Equipements>
    {
        private Equipements _equipements { get; set; }

        #region StartAsync
        public async Task StartAsync(IDialogContext context)
        {
            _equipements = new Equipements();
            await context.PostAsync("Maintenant il faut vous équiper");

            PromptDialog.Confirm(context, ReponseCapeAsync, "Portez-vous une cape?");
        }
        #endregion

        #region ReponseCapeAsync (private)
        private async Task ReponseCapeAsync(IDialogContext context, IAwaitable<bool> result)
        {
            _equipements.Cape = await result;

            
            await context.PostAsync("THE Question");

            #region Construction des cartes
            HeroCard heroCard1 = new HeroCard();
            heroCard1.Title = "Par dessus le collant";
            heroCard1.Subtitle = "Modèle: SuperLoose 10.45 en Lycra";

            HeroCard heroCard2 = new HeroCard();
            heroCard2.Title = "Sous le collant";
            heroCard2.Subtitle = "Modèle: MegaWinner 3000 en coton ultra résistant";
            #endregion

            #region Images
            heroCard1.Images = new List<CardImage>();
            CardImage cardImage = new CardImage()
            {
                Url = HttpContext.Current.Request.Url + "/../../images/img_1.png"
            };
            heroCard1.Images.Add(cardImage);

            heroCard2.Images = new List<CardImage>();
            cardImage = new CardImage()
            {
                Url = HttpContext.Current.Request.Url + "/../../images/img_2.png"
            };
            heroCard2.Images.Add(cardImage);
            #endregion

            #region Bouton
            heroCard1.Buttons = new List<CardAction>();
            CardAction button = new CardAction()
            {
                Value = "looser",
                Type = ActionTypes.PostBack,
                Title = "Acheter"
            };
            heroCard1.Buttons.Add(button);

            heroCard2.Buttons = new List<CardAction>();
            button = new CardAction()
            {
                Value = "winner",
                Type = ActionTypes.PostBack,
                Title = "Acheter"
            };
            heroCard2.Buttons.Add(button);
            #endregion

            #region On envoi le dialogue
            IMessageActivity reply = context.MakeMessage();
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            reply.Attachments.Add(heroCard1.ToAttachment());
            reply.Attachments.Add(heroCard2.ToAttachment());

            await context.PostAsync(reply);
            context.Wait(AfterCostumeAsync);
            #endregion;
        }
        #endregion

        #region AfterCostumeAsync (private)
        private async Task AfterCostumeAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            IMessageActivity message = await result;
            if (message.Text == "winner")
            {
                _equipements.PositionCulotte = PositionCulotte.SousCollant;
            }
            else
            {
                _equipements.PositionCulotte = PositionCulotte.SurCollant;
            }

            PromptDialog.Choice(
                 context: context,
                 resume: AfterSuperPouvoirsCompleted,
                 options: Enum.GetValues(typeof(SuperPouvoirs)).Cast<SuperPouvoirs>().ToArray(),
                 prompt: "Choisissez un super pouvoir",
                 retry: "Pas compris, recommencez SVP");
        }
        #endregion

        #region AfterSuperPouvoirsCompleted (private)
        private async Task AfterSuperPouvoirsCompleted(IDialogContext context, IAwaitable<SuperPouvoirs> result)
        {
            _equipements.SuperPouvoirs = await result;

            string[] armes = new string[] { "Laser super dangereux", "Sabre en griffe de Wolferine", "Rayon chatouilleur", "Bottes avec détecteur d'arrière-train", "Bombe à eau", "Champ de forces" };

            PromptDialog.Choice(
         context: context,
         resume: AfterArmesCompleted,
         options: armes,
         prompt: "Il vous faut aussi une arme",
         retry: "Pas compris, recommencez SVP");
        }
        #endregion

        #region AfterArmesCompleted
        private async Task AfterArmesCompleted(IDialogContext context, IAwaitable<String> result)
        {
            _equipements.Arme = await result;

            ReceiptCard receiptCard1 = new ReceiptCard()
            {
                Title = "Récapitulatif panier",
                Total = "3368.0€",
                Tax = "54.2€"
            };

            #region Le costume du héros
            if (_equipements.Cape)
            {
                receiptCard1.Items = new List<ReceiptItem>();
                var item = new ReceiptItem
                {
                    Title = "Cape",
                    Price = "148€",
                    Quantity = "1",
                    Subtitle = "Bio, Vegan, Gluten-Free",
                    Text = ""
                };
                receiptCard1.Items.Add(item);
            }

            // costume
            if (_equipements.PositionCulotte == PositionCulotte.SurCollant)
            {
                ReceiptItem item = new ReceiptItem
                {
                    Title = "Costume",
                    Subtitle = "SuperLoose 10.45 en Lycra.... mwaaarf!!!!",
                    Price = "450€",
                    Quantity = "1",
                    Text = "",
                    Image=new CardImage(HttpContext.Current.Request.Url + "/../../images/img_1.png")
                };
                receiptCard1.Items.Add(item);
            }
            else
            {
                ReceiptItem item = new ReceiptItem
                {
                    Title = "Costume",
                    Subtitle = "MegaWinner 3000 en coton ultra résistant",
                    Price = "958€",
                    Quantity = "1",
                    Text = "",
                    Image = new CardImage(HttpContext.Current.Request.Url + "/../../images/img_2.png")
                };
                receiptCard1.Items.Add(item);
            } 
            #endregion

            #region Pouvoirs
            ReceiptItem item1 = new ReceiptItem
            {
                Title = "Super Pouvoirs",
                Subtitle = "Pouvoirs garantis sans OGM",
                Price = "482€",
                Quantity = "1",
                Text = ""
            };
            switch (_equipements.SuperPouvoirs)
            {
                case SuperPouvoirs.Invisibilite:
                    item1.Subtitle = "Devenir invisible";
                    break;
                case SuperPouvoirs.SuperForce:
                    item1.Subtitle = "Une force sur-humaine";
                    break;
                case SuperPouvoirs.VueRayonX:
                    item1.Subtitle = "Vue rayon X";
                    break;
                case SuperPouvoirs.SavoirMonterMeubleIkea:
                    item1.Subtitle = "Savoir monter meuble Ikea (+10€)";
                    break;
                case SuperPouvoirs.SuperVitesse:
                    item1.Subtitle = "Vitesse ultra-rapide";
                    break;
                case SuperPouvoirs.RabDeFritesALaCantine:
                    item1.Subtitle = "Avoir toujours un rab de frite à la cantine";
                    break;
                case SuperPouvoirs.CongelerTout:
                    item1.Subtitle = "Congélateur humain";
                    break;
                default:
                    break;
            }
            receiptCard1.Items.Add(item1);
            #endregion

            #region Arme
            item1 = new ReceiptItem
            {
                Title = "Armes",
                Subtitle= _equipements.Arme,
                Price = "1780€",
                Quantity = "1",
                Text = ""
            };

            receiptCard1.Items.Add(item1);
            #endregion

            #region Boutons
            receiptCard1.Buttons = new List<CardAction>();
            CardAction button = new CardAction()
            {
                Value = "achat",
                Type = ActionTypes.PostBack,
                Title = "Acheter",
                Image = "https://jack35.files.wordpress.com/2016/09/133.jpg"
            };
            receiptCard1.Buttons.Add(button);
            #endregion


            IMessageActivity message = context.MakeMessage();
            message.Attachments.Add(receiptCard1.ToAttachment());
            await context.PostAsync(message);
            context.Wait(AfterEquipementAsync);
        }
        #endregion

        #region AfterEquipementAsync (private)
        private async Task AfterEquipementAsync(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("Voilà, vous êtes équipé");
            context.Done(_equipements);
        } 
        #endregion

    }
}