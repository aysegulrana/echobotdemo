// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.14.0

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace EchoBotExample.Bots
{
    public class EchoBot : ActivityHandler
    {
        //As oppose to parallel programming with many threads, asynchronis programming allows one thread to do different job itself. It can start an
        // activity then while that activity isnot finished yet, it can jump into another one and in the end, each task will be done no matter what order
        //they were performed. One thread can handle all the jobs not synchrounsly but if needed it can switch to another job while the others are already started and running.
        //If we do not use async programming when we have only one thread to handle everything the program will block very other task when one task is running. 
        //so that the time it takes to complete all the task would be the sum of total time each task take and it is time-inefficient.
        //In async programming, instead of blocking other tasks from running by the thread, "await" keyword is used. If we use only await word, it would prevent 
        //other tasks from blocking. However, it will not start them until the current task is done either. It will receive the task requests but will make
        //them wait şnstead of blocking. So, it is still sequential and takes nearly the same time as before. We do not want that for chatbots.(why??)
        //The namespace System.Threading.Tasks enables us to concurrently start the tasks that need to be done and when one needs an action that will be performed by
        //our thread, the other can still continue their work and await will be used for the tasks that are waiting for our thread to switch to that task that requires an action.
        //This way the code will finish its tasks much faster than working sync. In our EchoBot code, when two chatbots are working simultaneously, the actions that need attention
        //from our chatbot are indicated with "await" keyword. We know that they will wait for each other. However, tasks OnMessageActivityAsync and OnMembersAddedAsync
        //does not need to wait for each other to start doing their jobs. This creates a faster interaction with each user that chatbot is speaking with. The async keyword 
        //used before the method names makes the compiler aware that those methods have asynchronious actions.
        //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        { //This echo function is called if we are already in a conversation with a user. 
            var replyText = $"Parrot: {turnContext.Activity.Text}";
            await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
        }

        /*
        *********
        Parameters
        turnContext
        ITurnContext<IMessageActivity>
        A strongly-typed context object for this turn.

        cancellationToken
        CancellationToken
        A cancellation token that can be used by other objects or threads to receive notice of cancellation.

        Returns
        Task
        A task that represents the work queued to execute.
        **********  
        IMessageActivity type includes properties of the message activity that is performed by the user. 
        ITurnContext Provides context for a turn of a bot. Since here our activity is Message Activity, the object turnContext
        includes the text property that is sent by the user. We take that text and add to our return message since this is
        an Echo Bot. In different kinds of bots, we would use that text to find an answer for it in our previously defined 
        database of answers for example. After we create our response to the message, we use SendActivityAsync method to send a set of activities to the
        user that sent us the message activity. MessageFactory Class is used to indicate the type of the response that will be returned
        and prepare it to be sent. Since our bot is a simple bot that returns text messages, we use Text type of the class and
        put our message in it. So that the response will be given to the user.
        https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.builder.activityhandler.onmessageactivityasync?view=botbuilder-dotnet-stable
        https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.builder.iturncontext-1?view=botbuilder-dotnet-stable
        https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.builder.iturncontext.sendactivitiesasync?view=botbuilder-dotnet-stable#Microsoft_Bot_Builder_ITurnContext_SendActivitiesAsync_Microsoft_Bot_Schema_IActivity___System_Threading_CancellationToken_
        https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.builder.messagefactory?view=botbuilder-dotnet-stable
         */
        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        { //This function is called when a new chat is started by a new user. 
            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }
    }/*
       When a member other than the bot is added to the conversation, this method is used and it usually includes welcome
       message since we are welcoming the member for the first time. membersAdded has the list of members that started a 
       conversation with our bot while ITurnContext interface has the activity of IConversationUpdateActivity this time.
      in the for loop, we look at the members' id in order to check if they were already present in our context by checking the Recepient.Id of the activity
    we received. If the member is new, we call SendActivityAsync method and send back the response welcome text to the entered new user.
    https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.builder.activityhandler.onmembersaddedasync?view=botbuilder-dotnet-stable
      */
}
