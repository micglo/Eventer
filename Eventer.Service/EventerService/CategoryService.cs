using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Eventer.Domain.Entity.EventerEntity;
using Eventer.Mapper.ModelFacotry.Common;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.Category;
using Eventer.Repository.UoW;
using Eventer.Service.Common;
using Eventer.Service.EventerService.Interface;
using Eventer.Utility.CustomException;
using Ninject;

namespace Eventer.Service.EventerService
{
    public class CategoryService : ServiceBase<CategoryDto, CategoryPostDto, CategoryDto>, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, [Named("CategoryFactory")]IModelFactory modelFactory, 
            ICustomException customException, HttpRequestMessage request) 
            : base(unitOfWork, modelFactory, customException, request)
        {
        }

        public override async Task<PagedItems<CategoryDto>> GetAllAsync(string skip, string take)
        {
            var intSkip = int.Parse(skip);
            var intTake = int.Parse(take);
            var skipAmount = intTake*(intSkip - 1);

            var categories = await UnitOfWork.CategoryRepository.GetAllAsync(x => x.OrderBy(y => y.CategoryName), skipAmount, intTake);
            var allCategoriesCount = await UnitOfWork.CategoryRepository.Count();
            var categoriesDto = categories.Select(ModelFactory.GetModel<CategoryDto>).ToList();

            return CreatePagedItems(categoriesDto, "CategoryRoute", intSkip, intTake, allCategoriesCount);
        }

        public override async Task<CategoryDto> GetByIdAsync(object id)
        {
            if (!await ExistsAsync(id))
                CustomException.ThrowNotFoundException($"There is no category with id = {id}.");

            var category = await UnitOfWork.CategoryRepository.FindAsync(id);
            return ModelFactory.GetModel<CategoryDto>(category);
        }

        public override async Task<CategoryDto> AddAsync(CategoryPostDto dtoModel)
        {
            var categoryDomain = ModelFactory.GetModel<Category>(dtoModel);
            var newEntity = await UnitOfWork.CategoryRepository.AddAsync(categoryDomain);
            return ModelFactory.GetModel<CategoryDto>(newEntity);
        }

        public override async Task<CategoryDto> Update(CategoryDto dtoModel)
        {
            if (!Exists(dtoModel.Id))
                CustomException.ThrowNotFoundException($"There is no category with id = {dtoModel.Id}.");

            var categoryDomain = ModelFactory.GetModel<Category>(dtoModel);
            await UnitOfWork.CategoryRepository.Update(categoryDomain);

            return await GetByIdAsync(dtoModel.Id);
        }

        public override async Task RemoveAsync(object id)
        {
            if (!await ExistsAsync(id))
                CustomException.ThrowNotFoundException($"There is no category with id = {id}.");

            var categoryDomain = await UnitOfWork.CategoryRepository.FindAsync(id);
            await UnitOfWork.CategoryRepository.RemoveAsync(categoryDomain);
        }

        public async Task<CategoryDto> GetByNameAsync(string categoryName)
        {
            if (!await ExistsByNameAsync(categoryName))
                CustomException.ThrowNotFoundException($"There is no category with name {categoryName}.");

            var category = await UnitOfWork.CategoryRepository.SingleOrDefaultAsync(x=>x.CategoryName.Equals(categoryName));
            return ModelFactory.GetModel<CategoryDto>(category);
        }

        public async Task<ICollection<CategoryDto>> AddListAsync(CategoryPostAsListDto categoryList)
        {
            var newCategorylist = new List<CategoryDto>();
            foreach (var category in categoryList.CategoryNames)
            {
                var categoryDomain = new Category
                {
                    CategoryName = category
                };
                var newEntity = await UnitOfWork.CategoryRepository.AddAsync(categoryDomain);
                var newCategory = ModelFactory.GetModel<CategoryDto>(newEntity);
                newCategorylist.Add(newCategory);
            }

            return newCategorylist;
        }

        #region Helpers

        protected override async Task<bool> ExistsAsync(object id)
            => await UnitOfWork.CategoryRepository.AnyAsync(x => x.Id == (int)id);

        private async Task<bool> ExistsByNameAsync(string categoryName)
            => await UnitOfWork.CategoryRepository.AnyAsync(x => x.CategoryName.Equals(categoryName));

        private bool Exists(object id)
            => UnitOfWork.CategoryRepository.Any(x => x.Id == (int)id);

        #endregion
    }
}