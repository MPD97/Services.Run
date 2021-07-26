using System;
using System.Collections.Generic;
using Convey.Logging.CQRS;
using Services.Run.Application.Commands;
using Services.Run.Application.Events.External;
using Services.Run.Application.Exceptions;

namespace Services.Run.Infrastructure.Logging
{
    internal sealed class MessageToLogTemplateMapper : IMessageToLogTemplateMapper
    {
        private static IReadOnlyDictionary<Type, HandlerLogTemplate> MessageTemplates 
            => new Dictionary<Type, HandlerLogTemplate>
            {
                {
                    typeof(CreateRun),     
                    new HandlerLogTemplate
                    {
                        After = "Created run with id: {RunId} for user: {UserId} and route: {RouteId}."
                    }
                },
                {
                    typeof(UserCreated),     
                    new HandlerLogTemplate
                    {
                        After = "Added a user with id: {UserId}.",
                        OnError = new Dictionary<Type, string>
                        {
                            {
                                typeof(UserAlreadyExistsException), "User with id: {UserId} already exists."
                            }
                        }
                    }
                },
                {
                    typeof(UserStateChanged),
                    new HandlerLogTemplate
                    {
                        After = "User id: {UserId} state changed from: {PreviousState} to: {CurrentState}.",
                        OnError = new Dictionary<Type, string>
                        {
                            {
                                typeof(UserNotFoundException), "User with id: {UserId} was not found."
                            },
                            {
                                typeof(CannotChangeUserStateException), "State cannot be changed to: {State} for user with id: {UserId}"
                            }
                        }

                    }
                }
            };
        
        public HandlerLogTemplate Map<TMessage>(TMessage message) where TMessage : class
        {
            var key = message.GetType();
            return MessageTemplates.TryGetValue(key, out var template) ? template : null;
        }
    }
}