
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ordering.Domain.Core
{
    public class Response
    {
        private readonly IList<string> _messages = new List<string>();

        public Response()
        {
            this.Errors = new ReadOnlyCollection<string>(this._messages);
        }

        public Response(object result) : this()
        {
            this.Result = result;
        }

        public IEnumerable<string> Errors { get; }
        public object Result { get; }


        public Response AddError(string message)
        {
            this._messages.Add(message);
            return this;
        }
    }
}
