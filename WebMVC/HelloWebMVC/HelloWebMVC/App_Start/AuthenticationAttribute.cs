using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HelloWebMVC.App_Start
{
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            string cookieName = FormsAuthentication.FormsCookieName;//读取登录授权Cookies的名称
            HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[cookieName];//接收这个Cookies
            FormsAuthenticationTicket authTicket = null;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);//我们知道MVC登录授权的Cookies是加密的，所以我们在此需要解密
            }
            catch (Exception ex)
            {
                return;
            }
            if (authTicket != null && filterContext.HttpContext.User.Identity.IsAuthenticated)//如果Cookies不为Null 也通过验证
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.HttpContext.Response.Redirect("/Account/Index");//否则跳转至登陆页
            }
        }
    }
}