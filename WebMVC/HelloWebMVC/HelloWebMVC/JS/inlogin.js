
$(document).ready(function () {
    // 回车事件
    $("input").keydown(function (event) {
        if (event.keyCode == 13) {
            $(this)[0].blur();
            Login();
        }
    });
    $("#UserName").focus(function () {
        $("#UserName").removeClass("errinput");
        $("#errUserName").empty();
    });
    $("#UserPwd").focus(function () {
        $("#UserPwd").removeClass("errinput");
        $("#errUserPwd").empty();
    });
    settextasPwd("UserPwd");
    //自动登录
    if ($("#RememberPwd").prop("checked")) {
        if ($("#UserPwd").val() != "") {
            Login();
        } else {
            window.location.reload();
        }
    }
});

function Login() {
    $("input[type='button']")[0].blur();
    var UserName = $("#UserName").val();
    if (UserName == "") {
        $("#UserName").addClass("errinput");
        $("#errUserName").text("请输入用户名");
        return;
    }
    var UserPwd = $("#UserPwd").attr("pwdval");
    if (UserPwd == "") {
        $("#UserPwd").addClass("errinput");
        $("#errUserPwd").text("请输入登录密码");
        return;
    }
    if (UserPwd.length > 15) {
        $("#UserPwd").addClass("errinput");
        $("#errUserPwd").text("请输入1~15个字符");
        return;
    }
    var RememberPwd = $("#RememberPwd").prop("checked");
    $.ajax({
        url: "/Account/Login",
        type: "post",
        contentType: "application/json",
        dataType: "text",
        data: JSON.stringify({ UserName: UserName, UserPwd: UserPwd, RememberPwd: RememberPwd }),
        success: function (result, status) {
            if (result == "200") {
                //登录成功 跳转 
                $("#UserPwd").val("");
                $("#UserPwd").attr("pwdval", "");
                location.href = "../Home/Index";
            }
            else {
                //弹出错误码
                Ewin.alert({ title: "提示信息", message: result, btnOk: "确定", width: 200, auto: true });
            }
        },
        error: function (error) {
            Ewin.alert({ title: "提示信息", message: "登录失败", btnOk: "确定", width: 200, auto: true });
        }
    });
}