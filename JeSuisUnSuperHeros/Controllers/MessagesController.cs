#region using
using JeSuisUnSuperHeros.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
#endregion

namespace JeSuisUnSuperHeros
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        ConnectorClient connector;

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            connector = new ConnectorClient(new Uri(activity.ServiceUrl), new MicrosoftAppCredentials());

            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new LuisHerosDialog());
            }
            else if (activity.Type == ActivityTypes.ConversationUpdate)
            {
                // peut ne pas marcher avec certains canaux

                IConversationUpdateActivity update = activity;

                //https://github.com/Microsoft/BotBuilder/issues/2401
                //https://github.com/Microsoft/BotBuilder/issues/1930

                if (update.MembersAdded != null && update.MembersAdded.Any() && update.MembersAdded[0].Name != "Bot")
                {
                    // ajout d'une image
                    // -----------------------

                    string url = HttpContext.Current.Request.Url + "/../../images/133.jpg";
                    string message = $"![Une image]({url})\n\n";
                    message += "Bienvenue dans le bot qui fera de vous un **Super Héros qui déchire**\n\n\n";

                    Activity reply = activity.CreateReply(message);
                    await connector.Conversations.ReplyToActivityAsync(reply);

                    //// attention: on ne peux pas utiliser Content
                    //// une activité, une fois sérialisée en JSON, ne peut dépasser 256 K
                    //// de plus seule la fourniture d'une Url garantit que le canal coble sera capable
                    //// d'afficher l'image
                    //Activity image = activity.CreateReply();
                    //Attachment attachement = new Attachment();
                    //attachement.Name = "HelloBot";
                    //attachement.ContentType = "image/jpg";
                    //attachement.ContentUrl = HttpContext.Current.Request.Url + "/../../images/133.jpg";
                    //image.Attachments.Add(attachement);
                    //await connector.Conversations.ReplyToActivityAsync(image);



                    // texte d'introduction
                    //Activity reply = activity.CreateReply("Bienvenue dans le bot qui fera de vous un Super Héros qui déchire");
                    //await connector.Conversations.ReplyToActivityAsync(reply);

                    //reply = activity.CreateReply("Faites **help** si vous avez besoin d'aide");
                    //await connector.Conversations.ReplyToActivityAsync(reply);
                }
            }
            else if (activity.Type == ActivityTypes.Ping)
            {
                Activity reply = activity.CreateReply("Ping");
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
 

            return null;
        }
    }
}