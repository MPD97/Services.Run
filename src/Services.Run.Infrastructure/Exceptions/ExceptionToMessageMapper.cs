using System;
using Convey.MessageBrokers.RabbitMQ;
using Services.Run.Application.Commands;
using Services.Run.Application.Events.Rejected;
using Services.Run.Application.Exceptions;
using Services.Run.Core.Exceptions;

namespace Services.Run.Infrastructure.Exceptions
{
    public class ExceptionToMessageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
            => exception switch
            {
                UserNotFoundException ex => message switch
                {
                    CreateRun command => new CreateRunRejected(ex.Message, ex.Code),
                    _ => null
                },
                InvalidUserStateException ex => message switch
                {
                    CreateRun command => new CreateRunRejected(ex.Message, ex.Code),
                    _ => null
                },
                RouteNotFoundException ex => message switch
                {
                    CreateRun command => new CreateRunRejected(ex.Message, ex.Code),
                    _ => null
                },
                InvalidRouteStatusException ex => message switch
                {
                    CreateRun command => new CreateRunRejected(ex.Message, ex.Code),
                    _ => null
                },
                RouteStatusIsNotAcceptedException ex => message switch
                {
                    CreateRun command => new CreateRunRejected(ex.Message, ex.Code),
                    _ => null
                },
                UserTooFarException ex => message switch
                {
                    CreateRun command => new CreateRunRejected(ex.Message, ex.Code),
                    _ => null
                },
                InvalidAccuracyException ex => message switch
                {
                    CreateRun command => new CreateRunRejected(ex.Message, ex.Code),
                    _ => null
                },
                InvalidAggregateIdException ex => message switch
                {
                    CreateRun command => new CreateRunRejected(ex.Message, ex.Code),
                    _ => null
                },
                InvalidLatitudeException ex => message switch
                {
                    CreateRun command => new CreateRunRejected(ex.Message, ex.Code),
                    _ => null
                },
                InvalidLocationIdException ex => message switch
                {
                    CreateRun command => new CreateRunRejected(ex.Message, ex.Code),
                    _ => null
                },
                InvalidLongitudeException ex => message switch
                {
                    CreateRun command => new CreateRunRejected(ex.Message, ex.Code),
                    _ => null
                },
                InvalidNextPointToCompleteException ex => message switch
                {
                    CreateRun command => new CreateRunRejected(ex.Message, ex.Code),
                    _ => null
                },
                InvalidPointOrderException ex => message switch
                {
                    CreateRun command => new CreateRunRejected(ex.Message, ex.Code),
                    _ => null
                },
                InvalidRadiusException ex => message switch
                {
                    CreateRun command => new CreateRunRejected(ex.Message, ex.Code),
                    _ => null
                },
                InvalidRunStatusException ex => message switch
                {
                    CreateRun command => new CreateRunRejected(ex.Message, ex.Code),
                    _ => null
                },
                RunNotChangeableException ex => message switch
                {
                    CreateRun command => new CreateRunRejected(ex.Message, ex.Code),
                    _ => null
                },
                _ => null
            };
    }
}