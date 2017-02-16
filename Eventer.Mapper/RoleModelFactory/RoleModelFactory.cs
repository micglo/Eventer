using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;
using Eventer.Model.Dto.Common;
using Eventer.Model.Dto.IdentityRole;
using Eventer.Model.Dto.User;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Eventer.Mapper.RoleModelFactory
{
    public class RoleModelFactory : IRoleModelFactory
    {
        private readonly UrlHelper _url;
        public RoleModelFactory(HttpRequestMessage request)
        {
            _url = new UrlHelper(request);
        }

        public IdentityRoleDto GetModel (IdentityRole role)
        {
            return new IdentityRoleDto
            {
                Id = role.Id,
                RoleName = role.Name,
                Users = role.Users.Select(u => new UserDto
                {
                    Id = u.UserId,
                    Links = new List<Link>
                    {
                        new Link
                        {
                            Rel = "get user info - Administrators only",
                            Href = _url.Link("GetUserInfoRoute", new { id = u.UserId }),
                            Method = "GET"
                        },
                        new Link
                        {
                            Rel = "get user roles - Administrators only",
                            Href = _url.Link("GetUserRolesRoute", new { id = u.UserId }),
                            Method = "GET"
                        }
                    }
                }),
                Links = new List<Link>
                {
                    new Link
                    {
                        Rel = "self - Administrators only",
                        Href = _url.Link("RoleRoute", new { id = role.Id }),
                        Method = "GET"
                    },
                    new Link
                    {
                        Rel = "self by roleName- Administrators only",
                        Href = _url.Link("RoleByRoleNameRoute", new { roleName = role.Name }),
                        Method = "GET"
                    },
                    new Link
                    {
                        Rel = "put role - Administrators only",
                        Href = _url.Link("RoleRoute", new {id = role.Id}),
                        Method = "PUT"
                    },
                    new Link
                    {
                        Rel = "delete role - Administrators only",
                        Href = _url.Link("RoleRoute", new {id = role.Id}),
                        Method = "DELETE"
                    },
                    new Link
                    {
                        Rel = "add user to role - Administrators only",
                        Href = _url.Link("AddUserToRoleRoute", null),
                        Method = "POST"
                    },
                    new Link
                    {
                        Rel = "remove role from user - Administrators only",
                        Href = _url.Link("RemoveUserRoleRoute", null),
                        Method = "POST"
                    },
                    new Link
                    {
                        Rel = "get my roles",
                        Href = _url.Link("GetMyRolesRoute", null),
                        Method = "GET"
                    }
                }
            };
        }

        public IdentityRole GetModel(IdentityRoleDto role)
        {
            return new IdentityRole
            {
                Id = role.Id,
                Name = role.RoleName
            };
        }

        public IdentityRole GetModel(IdentityRolePostModel role)
        {
            return new IdentityRole
            {
                Name = role.RoleName
            };
        }
    }
}