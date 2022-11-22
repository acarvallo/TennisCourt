using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCourt.Application.DTO
{
    public class RootOutput<TData> 
    {
        public RootOutput()
        {

        }
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
        public bool Success { get; set; }
        public List<string> Messages { get; set; }
        public TData Data { get;set; }
        public static RootOutput<TData> Sucess<TData>(TData data) => new RootOutput<TData>(data);
        public static RootOutput<TData> WithErrors(IEnumerable<string> messages) => new RootOutput<TData>(messages);

    }

}
