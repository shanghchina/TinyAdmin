$(document).ready(function () {

});

$(function () {
    //if ($.cookie('RememberMe') == 1) {
    //    $("#rememberMe").prop("checked", true);
    //    if ("@GlobalContext.SystemConfig.LoginProvider" == "Cookie") {
    //        if (!ys.isNullOrEmpty($.cookie('UserToken'))) {
    //            redirectToIndex();
    //        }
    //    }
    //    else {
    //        $.removeCookie("UserToken", { path: '/' });
    //    }
    //}
    //$("#rememberMe").click(function () {
    //    if ($(this).prop("checked")) {
    //        $.cookie('RememberMe', 1, { expires: 30, path: '/' });
    //    }
    //});

    //$("#imgCaptchaCode").click(function () {
    //    $(this).attr("src", '@Url.Content("~/Home/GetCaptchaImage")' + '?t=' + new Date().getTime());
    //});

    $("#form").validate({
        rules: {
            userName: { required: true },
            password: { required: true }
        }
    });

    $.validator.setDefaults({
        submitHandler: function () {
            loginForm();
        }
    });
});

function loginForm() {
    console.log("loginForm");
    if ($("#form").validate().form()) {
        var postData = $("#form").getWebControls();
        ys.ajax({
            url: '@Url.Content("~/Account/CheckLogin")',
            type: "post",
            data: postData,
            success: function (obj) {
                if (obj.Tag == 1) {
                    ys.msgSuccess(obj.Message);
                    redirectToIndex();
                }
                else {
                    ys.msgError(obj.Message);
                }
            }
        });
    }
}

function redirectToIndex() {
    // location.href = '@Url.Content("~/Home/Index")';
}

//调用webapi校验用户
function checkLoginUser() {
    var _postjson = {};
    _postjson.userName = $("#userName").val();
    _postjson.password = $("#password").val();
    PostJson(configuration.tscapiurl + '/Login/CheckUserAccount', JSON.stringify(_json), false, function (data) {
        var _data = $.parseJSON(data);
        if (_data.Code == "0") {
            var options = [];
            $.each(_data.Data, function (i, item) {
                item.DictGuid;
                options.push({
                    label: item.DictName, title: item.DictName, value: item.DictGuid
                });
            });
            
            CloseWaitMsg();
        }
        else
            WaitMsg(_data.Message, "错误", true);
    });
}

//调用webapi校验用户
function checkLoginUserdemo() {
    var _postjson = {};
    _postjson.LevelOneOrgId = $("#UserOrgID").val();
    _postjson.ParentGuid = "EB115532-38B4-493E-961F-360AB556F066";
    _postjson.IsEnabled = true;
    PostJson(configuration.tscapiurl + '/Dictionary/CheckUserAccount', JSON.stringify(_json), false, function (data) {
        var _data = $.parseJSON(data);
        if (_data.Code == "0") {
            var options = [];
            $.each(_data.Data, function (i, item) {
                item.DictGuid;
                options.push({
                    label: item.DictName, title: item.DictName, value: item.DictGuid
                });
            });

            CloseWaitMsg();
        }
        else
            WaitMsg(_data.Message, "错误", true);
    });
}