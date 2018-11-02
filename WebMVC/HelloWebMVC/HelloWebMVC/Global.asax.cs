using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace HelloWebMVC
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AutofacIoc.Register();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        /// <summary>
        /// 登录验证、s授权
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = Context.Request.Cookies[cookieName];
            FormsAuthenticationTicket authTicket = null;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch (Exception ex)
            {
                return;
            }
            string[] roles = authTicket.UserData.Split(',');
            FormsIdentity id = new FormsIdentity(authTicket);
            GenericPrincipal principal = new GenericPrincipal(id, roles);
            Context.User = principal;//存到HttpContext.User中
        }
    }
}
