using Component;
using Component.Encrypt;
using Hello.IDAL;
using Hello.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace HelloWebMVC.Models
{
    public class UserLogin
    {
        private readonly object LOCK = new object();
        public static string cookieUserName = "statica03002";
        public static string cookieUserPwd = "statica03003";
        //
        [Required(ErrorMessage = "请输入账户号码")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "请输入账户密码")]
        [DataType(DataType.Password, ErrorMessage = "密码格式不正确")]
        [MinLength(1, ErrorMessage = "密码长度介于1~15位之间")]
        [MaxLength(15, ErrorMessage = "密码长度介于1~15位之间")]
        public string UserPwd { get; set; }

        public bool RememberPwd { get; set; }

        public UserLogin()
        {
            HttpCookie cookie1 = HttpContext.Current.Request.Cookies[cookieUserName],
                cookie2 = HttpContext.Current.Request.Cookies[cookieUserPwd];
            try
            {
                if (cookie1 != null)
                    UserName = CMD5.Decryptor(cookie1.Value, "userinfo");
                if (cookie2 != null)
                    UserPwd = CMD5.Decryptor(cookie2.Value, "userinfo");
                RememberPwd = !string.IsNullOrEmpty(UserPwd);
            }
            catch (Exception ex)
            { }
        }

        public UserEntity LoginAction(IAuthentication Authentication)
        {
            lock (LOCK)
            {
                OperationResult result = Authentication.Sign(UserName, UserPwd);
                if (result.ResultType == OperationResultType.Success)
                {
                    UserEntity entity = (UserEntity)result.AppendData;
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                           1,
                           UserName,
                           DateTime.Now,
                           DateTime.Now.AddMinutes(30),
                           false,
                           entity.PermissionCode == "1" ? RightType.Admins.ToString() : RightType.Commons.ToString(),
                           "/"
                           );
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Current.Response.Cookies.Add(authCookie);

                    //记住密码
                    if (RememberPwd)
                    {
                        if (HttpContext.Current.Request.Cookies[cookieUserName] != null)
                        {
                            HttpContext.Current.Response.Cookies[cookieUserName].Value = CMD5.Encrypt(UserName, "userinfo");
                            HttpContext.Current.Response.Cookies[cookieUserName].Expires = DateTime.Now.AddYears(1);
                        }
                        else
                        {
                            HttpCookie cookie1 = new HttpCookie(cookieUserName, CMD5.Encrypt(UserName, "userinfo"));
                            cookie1.Expires = DateTime.Now.AddYears(1);
                            HttpContext.Current.Response.Cookies.Add(cookie1);
                        }
                        if (HttpContext.Current.Request.Cookies[cookieUserPwd] != null)
                        {
                            HttpContext.Current.Response.Cookies[cookieUserPwd].Value = CMD5.Encrypt(UserPwd, "userinfo");
                            HttpContext.Current.Response.Cookies[cookieUserPwd].Expires = DateTime.Now.AddYears(1);
                        }
                        else
                        {
                            HttpCookie cookie2 = new HttpCookie(cookieUserPwd, CMD5.Encrypt(UserPwd, "userinfo"));
                            cookie2.Expires = DateTime.Now.AddYears(1);
                            HttpContext.Current.Response.Cookies.Add(cookie2);
                        }
                    }
                    else
                    {
                        if (HttpContext.Current.Request.Cookies[cookieUserName] != null)
                        {
                            HttpContext.Current.Response.Cookies[cookieUserName].Expires = DateTime.Now.AddDays(-1);
                        }
                        if (HttpContext.Current.Request.Cookies[cookieUserPwd] != null)
                        {
                            HttpContext.Current.Response.Cookies[cookieUserPwd].Expires = DateTime.Now.AddDays(-1);
                        }
                    }

                    return entity;
                }
                else
                {
                    return null;
                }
            }
        }

        public string GetRemStr()
        {
            if (RememberPwd)
                return "checked=\"checked\"";
            else
                return string.Empty;
        }

        /// <summary>
        /// 清除cookies中的用户密码信息
        /// </summary>
        public static void ClearCookie()
        {
            if (HttpContext.Current.Request.Cookies[cookieUserPwd] != null)
            {
                HttpContext.Current.Response.Cookies[cookieUserPwd].Expires = DateTime.Now.AddDays(-1);
            }
            if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddDays(-1);
            }
        }
    }
}