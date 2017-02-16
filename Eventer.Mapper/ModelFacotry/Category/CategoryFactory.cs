using System.Net.Http;
using Eventer.Domain.Entity.Common;
using Eventer.Model.Dto.Category;
using Eventer.Model.Dto.Common;

namespace Eventer.Mapper.ModelFacotry.Category
{
    public class CategoryFactory : Common.ModelFactory
    {
        public CategoryFactory(HttpRequestMessage request) : base(request)
        {
        }

        public override TDto GetModel<TDto>(EntityBase domainEntity)
        {
            if (TypesEqual<TDto, CategoryDto>())
            {
                var categoryEntity = (Domain.Entity.EventerEntity.Category)domainEntity;

                var model = new CategoryDto
                {
                    Id = categoryEntity.Id,
                    CategoryName = categoryEntity.CategoryName
                };

                AddLinks(model, "CategoryRoute", "category");
                model.Links.Add(new Link
                {
                    Rel = "self by categoryName",
                    Href = Url.Link("CategoryByNameRoute", new { categoryName = model.CategoryName }),
                    Method = "GET"
                });

                return model as TDto;
            }
            return null;
        }

        public override TDomainEntity GetModel<TDomainEntity>(DtoBase dtoModel)
        {
            if (TypesEqual<CategoryPostDto>(dtoModel))
            {
                var categoryDto = (CategoryPostDto)dtoModel;
                return new Domain.Entity.EventerEntity.Category
                {
                    CategoryName = categoryDto.CategoryName
                } as TDomainEntity;
            }
            if (TypesEqual<CategoryDto>(dtoModel))
            {
                var categoryDto = (CategoryDto)dtoModel;
                return new Domain.Entity.EventerEntity.Category
                {
                    Id = categoryDto.Id,
                    CategoryName = categoryDto.CategoryName
                } as TDomainEntity;
            }
            return null;
        }
    }
}