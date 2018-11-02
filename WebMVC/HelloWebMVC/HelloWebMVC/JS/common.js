
//window.history.forward();
//window.onbeforeunload = function () {

//}

$(document).ready(function () {
    $(document).bind('click', function () {
        $(".sele").attr("style", "display:none;");
        $(".sele[imshow='1']").removeAttr("style");
        $(".sele[imshow='1']").removeAttr("imshow");

        $(".shuttlebg").hide();
    });
});

/*下拉框绑定相关*/
//绑定字典内容到指定的Select控件
function BindSelect(ctrlName, url, defaultValue) {
    var control = $('#' + ctrlName);
    //设置select2的处理
    control.select2({
        language: "zh-CN", //设置 提示语言
        //width: "100%", //设置下拉框的宽度
        placeholder: "请选择",
        //minimumInputLength: 10  //最小需要输入多少个字符才进行查询
        //maximumSelectionLength: 2  //设置最多可以选择多少项
        //templateResult: function (state) { //图文选项
        //    if (!state.id) {
        //        return state.text;
        //    }
        //    console.log(state.element.getAttribute("imgPath"));
        //    var $state = $('<span><img style="width: 30px;height: 20px;" src="' + state.element.getAttribute("imgPath") + '" class="img-flag" /> ' + state.text + '</span>');
        //    return $state;
        //},
        //templateSelection: function (state) {
        //    if (!state.id) return state.text; // optgroup
        //    var $state = $('<span><img style="width: 30px;height: 20px;" src="' + state.element.getAttribute("imgPath") + '" class="img-flag" /> ' + state.text + '</span>');
        //    return $state;
        //}
    });

    //绑定Ajax的内容
    $.getJSON(url, function (data) {
        control.empty();//清空下拉框
        if (defaultValue) {
            $.each(data, function (i, item) {
                if (item.Value == defaultValue) {
                    controlAppend(control, item);
                }
            });
            $.each(data, function (i, item) {
                if (item.Value != defaultValue) {
                    controlAppend(control, item);
                }
            });
        }
        else {
            $.each(data, function (i, item) {
                controlAppend(control, item);
            });
        }
    });
}

function controlAppend(control, item) {
    if (item.ParentValue) {
        control.append("<option value='" + item.Value + "' parent='" + item.ParentValue + "'>&nbsp;" + item.Text + "</option>");
    }
    else {
        control.append("<option value='" + item.Value + "'>&nbsp;" + item.Text + "</option>");
    }
}

function settextasPwd(id) {
    var pwdinput = $("#" + id);
    pwdinput.attr("ispassword", "1");
    if (pwdinput.val() != "") {
        pwdinput.attr("pwdval", pwdinput.val());
        var newval = "";
        for (var i = 0; i < pwdinput.attr("pwdval").length; i++) {
            newval += "•";
        }
        if (pwdinput.val() == newval) {
            pwdinput.val("");
        }
        else {
            pwdinput.val(newval);
        }
    }
    else {
        pwdinput.attr("pwdval", "");
    }
    var last;
    pwdinput.on('input', function (event) {
        if ($(this).prop('comStart')) {
            //
        }
        else {
            var thisval = this.value;
            last = event.timeStamp;
            setTimeout(function () {
                if (last - event.timeStamp == 0) {
                    if (event.keyCode != 13 && event.keyCode != 37 && event.keyCode != 39) {
                        if (thisval != "") {
                            var rg = new RegExp("^•*");
                            var pwdval = pwdinput.attr("pwdval"),
                                lastval = thisval.replace(rg, ""),
                                beginval = thisval.replace(lastval, "");
                            if (lastval.indexOf("•") < 0) {
                                pwdinput.attr("pwdval", pwdval.substr(0, beginval.length) + lastval);
                            }
                            else {
                                var rg = new RegExp("•");
                                var i = lastval.length;
                                while (rg.test(lastval.charAt(--i)));
                                var centerstr = lastval.slice(0, i + 1);
                                pwdinput.attr("pwdval", pwdval.substr(0, beginval.length) + centerstr + pwdval.substr(beginval.length, pwdval.length));
                            }
                            var newval = "";
                            for (var i = 0; i < pwdinput.attr("pwdval").length; i++) {
                                newval += "•";
                            }
                            pwdinput.val(newval);
                        }
                        else {
                            pwdinput.attr("pwdval", "");
                        }
                        pwdinput.attr("ispassword", "1");
                        $("#" + id + " ~ img:first").prop("src", "/Content/images/u37.png");
                    }
                }
            }, 100);
        }
    }).on('compositionstart', function () {
        $(this).prop('comStart', true);
    }).on('compositionend', function () {
        $(this).prop('comStart', false);
        $(this).trigger("input");
    });
}

//密码显/隐
function switchPwd2(id) {
    var showPwd = $("#" + id);
    if (showPwd.val()) {
        var passwordeye = $("#" + id + " ~ img:first");
        if (showPwd.attr("ispassword") == "1") {
            passwordeye.prop("src", "/Content/images/u63.png");
            showPwd.val(showPwd.attr("pwdval"));
            showPwd.attr("ispassword", "0");
        } else {
            passwordeye.prop("src", "/Content/images/u37.png");
            var newval = "";
            for (var i = 0; i < showPwd.attr("pwdval").length; i++) {
                newval += "•";
            }
            showPwd.val(newval);
            showPwd.attr("ispassword", "1");
        };
    }
    //获取焦点
    showPwd.focus();
}

//密码显/隐
function switchPwd(id) {
    var showPwd = $("#" + id);
    if (showPwd.val()) {
        var passwordeye = $("#" + id + " ~ img:first");
        if (showPwd.prop("type") == "password") {
            passwordeye.prop("src", "/Content/images/u63.png");
            showPwd.prop('type', 'text');
        } else {
            passwordeye.prop("src", "/Content/images/u37.png");
            showPwd.prop('type', 'password');
        };
    }
    //获取焦点
    showPwd.focus();
}
//密码隐
function HidePwd(id) {
    var showPwd = $("#" + id);
    var passwordeye = $("#" + id + " + img");
    if (showPwd.prop("type") == "text") {
        passwordeye.prop("src", "/Content/images/u37.png");
        showPwd.prop('type', 'password');
    }
}
//popover
function creatpopover(control) {
    control.focus(function () {
        if (!control.attr("isfirstfocus")) {
            control.attr("isfirstfocus", "1");
            control.popover({
                trigger: 'manual',
                placement: 'right', //top, bottom, left or right
                html: 'true',
                content: ContentMethod(control.val().length),
            });
            control.popover("show");
        }
        else {
            control.popover("show");
            control.next().children().last().html(ContentMethod(control.val().length));
        }
    });
    control.blur(function () {
        control.popover("hide");
    });
    control.keyup(function () {
        control.next().children().last().html(ContentMethod(control.val().length));
    });
}

function ContentMethod(pwdlen) {
    var pwdStr, colorStr, preStr, preStr2, resStr;
    if (pwdlen < 6) {
        pwdStr = "太短";
        colorStr = "red";
        preStr = pwdlen * 100 / 6;
        preStr2 = (100 - preStr) + "%";
        preStr += "%";
    }
    else {
        pwdStr = "安全";
        colorStr = "green";
        preStr = "100%";
        preStr2 = "0%";
    }
    resStr = "<h4 style='color:" + colorStr + "'>密码强度：" + pwdStr + "</h4>";
    if (preStr != "0%") {
        resStr += "<div style='float:left;width:" + preStr + ";border:3px solid " + colorStr + ";border-radius:6px'></div>";
    }
    if (preStr2 != "0%") {
        resStr += "<div style='float:left;width:" + preStr2 + ";border:3px solid lavender;border-top-right-radius:6px;border-bottom-right-radius:6px;'></div>";
    }
    resStr += "<br/><p>请输入 1~15 个字符。请不要使用容易被猜到的密码。</p>";
    return resStr;
}


/*下拉框绑定相关*/
//绑定字典内容到指定的Select控件
function BindmySelectcontrol(ctrlid, data, emptytext, defaultValue, myevent) {
    var control = $('#' + ctrlid), mycontent = "", spancontent = "";
    mycontent += "<div class='sele' style='display:none;'>";
    //绑定Ajax的内容
    if (data && data != "") {
        $.each(JSON.parse(data), function (i, item) {
            var isdefault = false;
            if (defaultValue && defaultValue != "" && ("," + defaultValue + ",").indexOf("," + item.Value + ",") >= 0) {
                isdefault = true;
            }
            if (isdefault) {
                mycontent += "<p selectvalue='" + item.Value + "' isselected='1'>" + item.Text + "</p>";
                spancontent += "<span selectvalue='" + item.Value + "'><span class='clos'>×</span>" + item.Text + "</span>";
            }
            else {
                mycontent += "<p selectvalue='" + item.Value + "'>" + item.Text + "</p>";
            }
        });
    }
    mycontent += "</div><div emptytext='" + emptytext + "'>";
    if (spancontent && spancontent != "") {
        mycontent += spancontent;
    }
    else {
        mycontent += "<span style='background-color:#fff;border:0;font-size:16px;color:#c6d1e0;'>" + emptytext + "</span>";
    }
    mycontent += "</div><div class='clic'></div>";
    control.empty();//清空
    control.removeClass();//移除样式
    control.html(mycontent);
    //绑定点击事件
    control.delegate("p", "click", function () {
        if ($(this).attr("isselected") == "1") {
            $(this).removeAttr("isselected");
        }
        else {
            $(this).attr("isselected", "1");
        }
        //绑定已选项
        var selecteddiv = $(this).parent().next(), con = "";
        $(this).parent().children().each(function () {
            if ($(this).attr("isselected") == "1") {
                con += "<span selectvalue='" + $(this).attr("selectvalue") + "'><span class='clos'>×</span>" + $(this).text() + "</span>";
            }
        });
        if (con == "") {
            con += "<span style='background-color:#fff;border:0;font-size:16px;color:#c6d1e0;'>" + emptytext + "</span>";
        }
        selecteddiv.empty();
        selecteddiv.html(con);
        if (myevent && typeof (myevent) == "function") {
            myevent();
        }
    });
    control.delegate("div", "click", function (e) {
        if ($(this).attr("class") == "sele") {
            stopPropagation(e);
        }
        else if ($(this).attr("class") != "clic") {
            var selectdiv = $(this).prev();
            selectdiv.attr("imshow", "1");
        }
    });
    control.delegate(".clic", "click", function (e) {
        var selectdiv = $(this).prev().prev();
        if (!selectdiv.attr("imshow")) {
            selectdiv.attr("imshow", "1");
        }
    });
    control.delegate(".clos", "click", function (e) {
        var sp = $(this).parent(),
            spparent = sp.parent(),
            tex = sp.text().replace("×", "");
        sp.remove();
        if (spparent.children().length == 0) {
            spparent.html("<span style='background-color:#fff;border:0;font-size:16px;color:#c6d1e0;'>" + emptytext + "</span>");
        }
        spparent.prev().children().each(function () {
            if ($(this).text() == tex) {
                $(this).removeAttr("isselected");
            }
        });
        if (myevent && typeof (myevent) == "function") {
            myevent();
        }
        stopPropagation(e);
    });

    control.addClass("myselectstyle");
    if (defaultValue && defaultValue != "") {
        setconditiondiv();
    }
}

function stopPropagation(e) {
    if (e.stopPropagation)
        e.stopPropagation();
    else
        e.cancelBubble = true;
}
/*下拉框绑定相关-结束*/

$.ajaxSetup({
    dataFilter: function (data, type) {
        //返回处理后的数据
        if (data.indexOf("!DOCTYPE html") > 0) {
            location.href = "/Account/Index";
            return;
        } else {
            return data;
        }
    }
});