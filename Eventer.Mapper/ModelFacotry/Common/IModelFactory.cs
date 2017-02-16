using Eventer.Domain.Entity.Common;
using Eventer.Model.Dto.Common;

namespace Eventer.Mapper.ModelFacotry.Common
{
    public interface IModelFactory
    {
        TDto GetModel<TDto>(EntityBase domainEntity) where TDto : DtoBase;
        TDomainEntity GetModel<TDomainEntity>(DtoBase dtoModel) where TDomainEntity : EntityBase;
    }
}