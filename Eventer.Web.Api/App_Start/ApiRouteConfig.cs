using System.Web.Http;

namespace Eventer.Web.Api
{
    public static class ApiRouteConfig
    {
        public static void Config(HttpConfiguration config)
        {
            //accout routes
            config.Routes.MapHttpRoute(
                "GetUserInfoRoute",
                "api/{apiVersion}/Account/GetUserInfo/{id}",
                new
                {
                    controller = "Account"
                }
            );
            config.Routes.MapHttpRoute(
                "GetUserRolesRoute",
                "api/{apiVersion}/Account/GetUserInfo/{id}/Roles",
                new
                {
                    controller = "Account"
                }
            );

            //api activity log routes
            config.Routes.MapHttpRoute(
                "ApiActivityLogsRoute",
                "api/{apiVersion}/ApiActivityLogs/{id}",
                new
                {
                    controller = "ApiActivityLogs",
                    id = RouteParameter.Optional
                }
            );
            config.Routes.MapHttpRoute(
                "ApiActivityLogsByUserRoute",
                "api/{apiVersion}/ApiActivityLogs/{userName}",
                new
                {
                    controller = "ApiActivityLogs"
                }
            );
            config.Routes.MapHttpRoute(
                "ApiActivityLogsByHostRoute",
                "api/{apiVersion}/ApiActivityLogs/{hostAddress}",
                new
                {
                    controller = "ApiActivityLogs"
                }
            );


            //Category routes
            config.Routes.MapHttpRoute(
                "CategoryRoute",
                "api/{apiVersion}/Categories/{id}",
                new
                {
                    controller = "Categories",
                    id = RouteParameter.Optional
                }
            );
            config.Routes.MapHttpRoute(
                "CategoryByNameRoute",
                "api/{apiVersion}/Categories/{categoryName}",
                new
                {
                    controller = "Categories"
                }
            );


            //city routes
            config.Routes.MapHttpRoute(
                "CityRoute",
                "api/{apiVersion}/Cities/{id}",
                new
                {
                    controller = "Cities",
                    id = RouteParameter.Optional
                }
            );
            config.Routes.MapHttpRoute(
                "CityByNameRoute",
                "api/{apiVersion}/Cities/{cityName}",
                new
                {
                    controller = "Cities"
                }
            );


            //client routes
            config.Routes.MapHttpRoute(
                "ClientRoute",
                "api/{apiVersion}/Clients/{id}",
                new
                {
                    controller = "Clients",
                    id = RouteParameter.Optional
                }
            );
            config.Routes.MapHttpRoute(
                "UserClientsRoute",
                "api/{apiVersion}/Clients/{userName}",
                new
                {
                    controller = "Clients"
                }
            );
            config.Routes.MapHttpRoute(
                "PutClientNoSecretRoute",
                "api/{apiVersion}/Clients/PutClientNoSecret/{id}",
                new
                {
                    controller = "Clients"
                }
            );
            config.Routes.MapHttpRoute(
                "ResetClientSecretRoute",
                "api/{apiVersion}/Clients/ResetClientSecret",
                new
                {
                    controller = "Clients"
                }
            );
            config.Routes.MapHttpRoute(
                "GetMyClientsRoute",
                "api/{apiVersion}/Clients/GetMyClients/{id}",
                new
                {
                    controller = "Clients",
                    id = RouteParameter.Optional
                }
            );
            config.Routes.MapHttpRoute(
                "AddClientRoute",
                "api/{apiVersion}/Clients/AddClient",
                new
                {
                    controller = "Clients"
                }
            );
            config.Routes.MapHttpRoute(
                "ResetMyClientSecretRoute",
                "api/{apiVersion}/Clients/ResetMyClientSecret",
                new
                {
                    controller = "Clients"
                }
            );
            config.Routes.MapHttpRoute(
                "PutMyClientRoute",
                "api/{apiVersion}/Clients/PutMyClient/{id}",
                new
                {
                    controller = "Clients"
                }
            );


            //errorLog routes
            config.Routes.MapHttpRoute(
                "ErrorLogRoute",
                "api/{apiVersion}/ErrorLogs/{id}",
                new
                {
                    controller = "ErrorLogs",
                    id = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                "UserErrorLogsRoute",
                "api/{apiVersion}/ErrorLogs/{userName}",
                new
                {
                    controller = "ErrorLogs"
                }
            );


            //event routes
            config.Routes.MapHttpRoute(
                "EventRoute",
                "api/{apiVersion}/Events/{id}",
                new
                {
                    controller = "Events",
                    id = RouteParameter.Optional
                }
            );


            //refreshToken routes
            config.Routes.MapHttpRoute(
                "RefreshTokenRoute",
                "api/{apiVersion}/RefreshTokens/{id}",
                new
                {
                    controller = "RefreshTokens",
                    id = RouteParameter.Optional
                }
            );


            //role routes
            config.Routes.MapHttpRoute(
                "RoleRoute",
                "api/{apiVersion}/Roles/{id}",
                new
                {
                    controller = "Roles",
                    id = RouteParameter.Optional
                }
            );
            config.Routes.MapHttpRoute(
                "RoleByRoleNameRoute",
                "api/{apiVersion}/Roles/{roleName}",
                new
                {
                    controller = "Roles"
                }
            );
            config.Routes.MapHttpRoute(
                "AddUserToRoleRoute",
                "api/{apiVersion}/Roles/AddUserToRole",
                new
                {
                    controller = "Roles"
                }
            );
            config.Routes.MapHttpRoute(
                "RemoveUserRoleRoute",
                "api/{apiVersion}/Roles/RemoveUserRole",
                new
                {
                    controller = "Roles"
                }
            );
            config.Routes.MapHttpRoute(
                "GetMyRolesRoute",
                "api/{apiVersion}/Roles/GetMyRoles",
                new
                {
                    controller = "Roles"
                }
            );


            //state routes
            config.Routes.MapHttpRoute(
                "StateRoute",
                "api/{apiVersion}/States/{id}",
                new
                {
                    controller = "States",
                    id = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                "StateByNameRoute",
                "api/{apiVersion}/States/{stateName}",
                new
                {
                    controller = "States"
                }
            );
        }
    }
}