#region using
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace JeSuisUnSuperHeros.Dialogs
{
    // https://github.com/Microsoft/BotBuilder-Samples/blob/master/CSharp/core-MultiDialogs/Dialogs/RootDialog.cs


    /// <summary>
    /// Point d'entrée de notre bot
    /// </summary>
    [Serializable]
    public class RootDialog : IDialog
    {
        #region StartAsync
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }
        #endregion

        #region MessageReceivedAsync (private)
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            IMessageActivity message = await result;

            if (message.Text.ToLower().Contains("help"))
            {
                // l'utilisateur appelle à l'aide
                await context.Forward(new HelpDialog(), ResumeAfterHelpDialog, message, CancellationToken.None);
            }
            else
            {
                context.Call(new LuisHerosDialog(), Callback);
            }
        }

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceivedAsync);
        }
        #endregion

        #region ResumeAfterHelpDialog
        private async Task ResumeAfterHelpDialog(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            // on ne peut pas appeler directement MessageReceivedAsync à la place de ResumeAfterHelpDialog
            // autrement le code tente de récupérer un nouveau message.
            // mais la pile de message est vide, celà provoque une exception
            // ResumeAfterHelpDialog permet de placer un Wait() afin de réclamer un nouveau message

            await context.PostAsync("On fait quoi?");
            context.Wait(MessageReceivedAsync);
        }
        #endregion
    }
}