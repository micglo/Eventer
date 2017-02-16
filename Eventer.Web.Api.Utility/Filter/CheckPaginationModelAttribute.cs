using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Eventer.Model.QueryString.Pagination;
using Eventer.Utility.CustomException;

namespace Eventer.Web.Api.Utility.Filter
{
    public class CheckPaginationModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ActionArguments.ContainsKey("paginationModel"))
            {
                var model = (Pagination)actionContext.ActionArguments["paginationModel"];

                long numericPage;
                long numericPageSize;
                var isPageNumeric = long.TryParse(model.Page, out numericPage);
                var isPageSizeNumeric = long.TryParse(model.PageSize, out numericPageSize);


                if (!(isPageNumeric && isPageSizeNumeric))
                {
                    var ex = new CustomException();
                    ex.ThrowBadRequestException("Value of page and pageSize has to be numeric string.");
                }
            }
            
        }
    }
}