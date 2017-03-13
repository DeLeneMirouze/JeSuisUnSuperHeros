#region using
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace JeSuisUnSuperHeros.Dialogs
{
    [Serializable]
    [LuisModel("175a0273-5051-4296-b8a8-cc2a9fc41b73", "32910e551add4572bc25f23317fb53c0")]
    //[LuisModel("3c34bc60-bc0f-40f0-8bf7-3cbe5f5a817b", "32910e551add4572bc25f23317fb53c0")]
    public class LuisHerosDialog: LuisDialog<Object>
    {
        #region NoneAsync
        [LuisIntent("")]
        public async Task NoneAsync(IDialogContext context, LuisResult luisResult)
        {
            var message = luisResult.ToString();

            if (message.ToLower().Contains("help"))
            {
                // l'utilisateur appelle à l'aide
                await context.Forward(new HelpDialog(), ResumeAfterHelpDialog, message, CancellationToken.None);
            }
            else
            {
                await context.PostAsync("Désolé, je ne comprends pas ce que vous voulez");
            }

            context.Done(this);
        }

        #region ResumeAfterHelpDialog
        private async Task ResumeAfterHelpDialog(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            // on ne peut pas appeler directement MessageReceivedAsync à la place de ResumeAfterHelpDialog
            // autrement le code tente de récupérer un nouveau message. 
            // mais la pile de message est vide, celà provoque une exception
            // ResumeAfterHelpDialog permet de placer un Wait() afin de réclamer un nouveau message

            await context.PostAsync("On fait quoi?");
            context.Wait(MessageReceived);
        }
        #endregion
        #endregion

        #region Congratulation
        [LuisIntent("Congratulation")]
        public async Task CongratulationAsync(IDialogContext context, LuisResult result)
        {
            context.Call(new CongratulationDialog(), CallbackCongratulation);
        }

        private async Task CallbackCongratulation(IDialogContext context, IAwaitable<object> result)
        {
            context.Done(this);
        }
        #endregion

        #region Mission
        [LuisIntent("Mission")]
        //[LuisIntent("Reservation")]
        public async Task MissionAsync(IDialogContext context, LuisResult result)
        {
            context.Done(this);
        }
        #endregion
    }
}