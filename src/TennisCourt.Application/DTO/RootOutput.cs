using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCourt.Application.DTO
{
    public class RootOutput
    {
        protected RootOutput()
        {
            Success = true;
            Messages = new List<string>();
        }

        private RootOutput(IEnumerable<string> messages)
        {
            Success = false;
            Messages = messages;
        }

        private RootOutput(string message)
        {
            Success = false;
            Messages = new List<string>() { message };
        }

        public bool Success { get; }
        public IEnumerable<string> Messages { get; }

        public static RootOutput WithErrors(IEnumerable<string> messages) => new(messages);

        public static RootOutput WithErrors(string message) => new (message);
    }

    public class RootOutput<T> : RootOutput
    {
        private RootOutput(T data) : base()
        {
            Data = data;
        }

        public T Data { get; }
        public static RootOutput<TData> Successful<TData>(TData data) => new RootOutput<TData>(data);

    }

}
