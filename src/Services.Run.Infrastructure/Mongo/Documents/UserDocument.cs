using System;
using Convey.Types;
using Services.Run.Core.Entities;

namespace Services.Run.Infrastructure.Mongo.Documents
{
    public sealed class UserDocument: IIdentifiable<Guid>
    {
        public Guid Id { get; set; }  
        public State State { get; set; }
    }
}