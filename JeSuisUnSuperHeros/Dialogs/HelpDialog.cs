#region using
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;
#endregion

namespace JeSuisUnSuperHeros.Dialogs
{
    /// <summary>
    /// Prise en charge du dialogue d'aide à l'utilisateur
    /// </summary>
    [Serializable]
    internal class HelpDialog : IDialog<IMessageActivity>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceived);
        }

        private async Task MessageReceived(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("Hello!");
            await context.PostAsync("Si vous êtes déjà un Super Héros, il vous suffit de demander une mission à une date, heure et un lieu donné");
            await context.PostAsync("Si vous souhaitez devenir un Super Héros, commencez par une formule d'accueil sympa");

            context.Done(this);
        }
    }
}