//mvc请求webapi通用函数
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