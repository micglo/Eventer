using System;
using System.Linq.Expressions;
using Eventer.Repository.Common;

namespace Eventer.Repository.Client.Interface
{
    public interface IClientRepository : IRepositoryBase<Domain.Entity.Client.Client>
    {
        bool Any(Expression<Func<Domain.Entity.Client.Client, bool>> filter);
    }
}