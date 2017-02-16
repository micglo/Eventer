using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Routing;
using Eventer.Mapper.ModelFacotry.Common;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.Common;
using Eventer.Repository.UoW;
using Eventer.Utility.CustomException;
using Eventer.Utility.HashGenerator;

namespace Eventer.Service.Common
{
    public abstract class ServiceBase<TDto, TPostDto, TPutDto> : IServiceBase<TDto, TPostDto, TPutDto> where TDto : DtoBase where TPostDto : DtoBase where TPutDto : DtoBase
    {
        protected IUnitOfWork UnitOfWork;
        protected IModelFactory ModelFactory;
        protected IGenerator Generator;
        protected ICustomException CustomException;
        protected UrlHelper Url { get; set; }
        protected HttpRequestMessage RequestMessage { get; set; }

        protected ServiceBase()
        {
            
        }

        protected ServiceBase(ICustomException customException, HttpRequestMessage request)
        {
            CustomException = customException;
            Url = new UrlHelper(request);
            RequestMessage = request;
        }

        protected ServiceBase(IUnitOfWork unitOfWork, IModelFactory modelFactory, ICustomException customException, HttpRequestMessage request)
        {
            UnitOfWork = unitOfWork;
            ModelFactory = modelFactory;
            CustomException = customException;
            Url = new UrlHelper(request);
        }

        public abstract Task<PagedItems<TDto>> GetAllAsync(string skip, string take);
        public abstract Task<TDto> GetByIdAsync(object id);
        public abstract Task<TDto> AddAsync(TPostDto dtoModel);
        public abstract Task<TDto> Update(TPutDto dtoModel);
        public abstract Task RemoveAsync(object id);

        public Task<int> SaveChangesAsync() => UnitOfWork.SaveChangesAsync();

        protected abstract Task<bool> ExistsAsync(object id);
        protected PagedItems<T> CreatePagedItems<T>(IEnumerable<T> query, string urlLink, int page, int pageSize, long totalNumberOfRecords)
            where T : DtoBase
        {
            var mod = totalNumberOfRecords % pageSize;
            var totalPageCount = totalNumberOfRecords / pageSize + (mod == 0 ? 0 : 1);

            string nextPageUrl;
            if (page == totalPageCount || page > totalPageCount)
                nextPageUrl = null;
            else
            {
                nextPageUrl = Url.Link(urlLink, new
                {
                    page = page + 1,
                    pageSize
                });
            }

            string prevPageUrl;
            if (page < 2)
            {
                prevPageUrl = null;
            }
            else
            {
                prevPageUrl = Url.Link(urlLink, new
                {
                    page = page - 1,
                    pageSize
                });
            }

            return new PagedItems<T>
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalNumberOfPages = totalPageCount,
                TotalNumberOfRecords = totalNumberOfRecords,
                NextPageUrl = nextPageUrl,
                PreviousPageUrl = prevPageUrl,
                Items = query
            };
        }
    }
}