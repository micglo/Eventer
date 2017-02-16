using System.Collections.Generic;
using Eventer.Domain.Entity.Common;
using Eventer.Model.Dto.Common;
using System.Net.Http;
using System.Web.Http.Routing;

namespace Eventer.Mapper.ModelFacotry.Common
{
    public abstract class ModelFactory : IModelFactory
    {
        protected UrlHelper Url { get; set; }

        /// <summary>
        /// Request otrzymywany z ControllerActivator przez wstrzykniecie(aby konstruowac link na bazie routingu zapisanego w api)
        /// </summary>
        protected HttpRequestMessage RequestMessage { get; set; }

        protected ModelFactory()
        {

        }
        protected ModelFactory(HttpRequestMessage request)
        {
            Url = new UrlHelper(request);
            RequestMessage = request;
        }

        /// <summary>
        /// Mapowanie z encji bazodanowej do modelu
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="domainEntity"></param>
        /// <returns></returns>
        public abstract TDto GetModel<TDto>(EntityBase domainEntity) where TDto : DtoBase;


        /// <summary>
        /// Mapowanie z modelu do encji bazodanowej
        /// </summary>
        /// <typeparam name="TDomainEntity"></typeparam>
        /// <param name="dtoModel"></param>
        /// <returns></returns>
        public abstract TDomainEntity GetModel<TDomainEntity>(DtoBase dtoModel) where TDomainEntity : EntityBase;


        protected bool TypesEqual<T1, T2>()
            => typeof(T1) == typeof(T2);

        protected bool TypesEqual<T>(object model)
            => model.GetType() == typeof(T);


        /// <summary>
        /// Podstawowe hateos
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="route"></param>
        /// <param name="itemName"></param>
        /// <param name="text"></param>
        protected void AddLinks<T>(CommonDto<T> item, string route, string itemName, string text = null)
        {
            item.Links = new List<Link>
            {
                new Link
                {
                    Rel = "self" + text,
                    Href = Url.Link(route, new {id = item.Id}),
                    Method = "GET"
                },
                new Link
                {
                    Rel = "put " + itemName + " - Administrators only",
                    Href = Url.Link(route, new {id = item.Id}),
                    Method = "PUT"
                },
                new Link
                {
                    Rel = "delete " + itemName + " - Administrators only",
                    Href = Url.Link(route, new {id = item.Id}),
                    Method = "DELETE"
                }
            };
        }
    }
}