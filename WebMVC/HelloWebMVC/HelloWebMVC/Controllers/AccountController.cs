using Component.Utility;
using Hello.IDAL;
using Hello.Model;
using HelloWebMVC.Models;
using System.Text;
using System.Web.Mvc;

namespace HelloWebMVC.Controllers
{
    public class AccountController : Controller
    {
        public IAuthentication Authentication { get; set; }

        // GET: Account
        public ActionResult Index()//呈现视图
        {
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object Login(UserLogin loginMol)
        {
            if (ModelState.IsValid)//是否通过Model验证
            {
                UserEntity entity = loginMol.LoginAction(Authentication);
                if (entity != null)
                {
                    Session["UserName"] = loginMol.UserName;//SESSION
                    Session["UserRoleName"] = entity.PermissionCode == "1" ? "管理员" : "用户";
                    Session["UserAuthority"] = entity.ChildCompanyAuthority;
                    LogUtils.logger.Info("登录成功！");
                    return 200;
                }
                else
                {
                    return "账户或密码有误！";
                }
            }
            else
            {
                //读取错误信息
                StringBuilder stb = new StringBuilder("");
                var errors = ModelState.Values;
                foreach (var item in errors)
                {
                    foreach (var item2 in item.Errors)
                    {
                        if (!string.IsNullOrEmpty(item2.ErrorMessage))
                        {
                            if (string.IsNullOrEmpty(stb.ToString()))
                            {
                                stb.AppendLine(item2.ErrorMessage);//读取一个错误信息 并返回
                            }
                        }
                    }
                }
                return stb.ToString();
            }
        }
    }
}