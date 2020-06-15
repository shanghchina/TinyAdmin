$(function () {
  

}); 

//mvc请求webapi通用函数
var GetCheckAppString = function () {
    var result = "";
    var postdata = { ajaxAppId: configuration.appid };
    $.ajax({
        async: false,
        url: '/ApiBase/AjaxGetEncryptStr',
        contentType: "application/json",
        datatype: "text",
        type: 'post',
        cache: false,
        data: JSON.stringify(postdata),
        success: function (data) {
            console.log(data);
            result = data;
            //成功后调用
        },
        error: function (e) {
            //失败后回调
            console.log(e);
        },
        beforeSend: function () {
            //发送请求前调用，可以放一些"正在加载"之类额话
        }
    })
    return result;
}

var RequestApiPostJson = function (apiurl, requestjson, loading, callback) {
    $.ajax({
        url: configuration.tscweburl + '/Home/AjaxPostCorssDomain',
        datatype: "json",
        type: 'post',
        cache: false,
        data: { url: apiurl, json: requestjson },
        success: function (data) {
            console.log(data);
            //成功后调用
            callback(data);
        },
        error: function (e) {
            console.log(e);
            //失败后回调
            WaitMsg(JSON.stringify(e), "错误", true);
        },
        beforeSend: function () {
            //发送请求前调用，可以放一些"正在加载"之类额话
            if (loading)
                WaitMsg("正在加载", "提示", false);
        }
    })
}
//post添加header请求
var RequestApiPostHeader = function (apiurl, requestjson, loading, callback) {
    $.ajax({
        //headers: {
        //    'token': "U2PGBjguSuJg",
        //    'version': "1.0",
        //    'from': "test",
        //},
        beforeSend: function (xhr) {
            //xhr.setRequestHeader("Authorization", "Basic " + "cGluZ2FuOjEyMzQ1Na==");
        },
        //headers: {"Authorization","Basic "+"cGluZ2FuOjEyMzQ1Na=="}
        url: configuration.tscweburl + apiurl,
        datatype: "json",
        type: 'post',
        cache: false,
        data: { url: apiurl, json: requestjson },
        success: function (data) {
            console.log(data);
            //成功后调用
            callback(data);
        },
        error: function (e) {
            console.log(e);
            //失败后回调
            WaitMsg(JSON.stringify(e), "错误", true);
        },
        beforeSend: function () {
            //发送请求前调用，可以放一些"正在加载"之类额话
            if (loading)
                WaitMsg("正在加载", "提示", false);
        }
    })
}