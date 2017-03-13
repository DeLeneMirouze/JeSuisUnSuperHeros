#region using
using JeSuisUnSuperHeros.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Threading.Tasks;
#endregion

namespace JeSuisUnSuperHeros.Dialogs
{
    [Serializable]
    public class CongratulationDialog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Salut je suis M. Bot à votre service");
            await context.PostAsync("Avant tout, il vous faut un nom de Super Héros!");

            var infoDialog = FormDialog.FromType<InfoHeros>(FormOptions.PromptInStart);
            context.Call(infoDialog, ResumeAfterInfoHerosDialog);
        }

        #region ResumeAfterInfoHerosDialog (private)
        private async Task ResumeAfterInfoHerosDialog(IDialogContext context, IAwaitable<InfoHeros> result)
        {
            try
            {
                InfoHeros message = await result;
                string superName = Generator.GenerateSuperherosName(message);

                // TODO sauvegarder
                await context.PostAsync($"Voici votre nom de Super Héros: **{superName}**");

                context.Call(new EquipementDialog(), this.ResumeAfterEquipementDialog);
            }
            catch (Exception ex)
            {
                await context.PostAsync($"Une erreur avec le message: {ex.Message}");
            }
            //finally
            //{
            //    context.Wait(MessageReceivedAsync);
            //}
        }
        #endregion

        #region ResumeAfterEquipementDialog (private)
        private async Task ResumeAfterEquipementDialog(IDialogContext context, IAwaitable<Equipements> result)
        {
            Equipements equipement = await result;
            // TODO sauvegarder

            await context.PostAsync("Prêt pour voler de mission en mission?");
            context.Done(this);
        }
        #endregion
    }

}