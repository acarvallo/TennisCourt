using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCourt.Application.DTO
{
    public class RootOutput<TData> 
    {
        private RootOutput(TData data)
        {
            Data = data;
            Success = true;
        }
        private RootOutput(IEnumerable<string> messages)
        {
            Success = false;
            Messages = messages.ToList();
        }
        public bool Success { get; }
        public List<string> Messages { get; }
        public TData Data { get; }
        public static RootOutput<TData> Sucess<TData>(TData data) => new RootOutput<TData>(data);
        public static RootOutput<TData> WithErrors(IEnumerable<string> messages) => new RootOutput<TData>(messages);

    }

}
