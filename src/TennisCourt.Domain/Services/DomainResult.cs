using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisCourt.Domain.Models;
using TennisCourt.Domain.Models.Base;

namespace TennisCourt.Domain.Services
{
    public class DomainResult<TEntity> where TEntity : BaseEntity
    {
        private DomainResult()
        {

        }
        public TEntity Entity { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public void AddMessage(string error) => Errors.Add(error);
        public bool IsValid() => Errors.Any();
        public static DomainResult<TEntity> Create() => new();

        public void AddErrors(IList<string> errors)
        {
            Errors.AddRange(errors);
        }

        public DomainResult<TEntity> WithSucess(TEntity entity)
        {
            Entity = entity;
            return this;
        }
    }
}
