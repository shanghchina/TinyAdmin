var DateFormat = function (value, row, index) {
    if (value == "0001-01-01T00:00:00" || value == null) 
        return "-";
    return new Date(value).Format("yyyy-MM-dd");
}
var ActiveMenu = function(id) {
    $('.page-sidebar ul').children('li.active').removeClass('active');
    $('.page-sidebar ul').children('arrow.open').removeClass('open');
    $('#'+id).addClass('active').parents('li').addClass('active');
    $('#'+id).children('a > span.arrow').addClass('open');
}
var EffectMonthFormat = function (value, row, index) {
    if (value == "0001-01-01T00:00:00" || value == null)
        return "-";
    return new Date(value).Format("yyyy-MM");
}
var DateTimeFormat = function (value, row, index) {
    if (value == "0001-01-01T00:00:00" || value == null)
        return "-";
    return new Date(value).Format("yyyy-MM-dd hh:mm:ss");
}
 //获取当前的年月  
var GetCurrentMonth = function () {
    var _currentDate = new Date;
    var _currentYear = _currentDate.getFullYear();
    var _currentMonth = _currentDate.getMonth() + 1;
    if (parseInt(_currentMonth)<10) {
        _currentMonth = "0" + _currentMonth;
    }
    return  _currentYear + "-" + _currentMonth; 
}
//年月类型日期比较大小,相等返回0，大于返回1，(小于返回-1,不存在)
function CompareDate(d1, d2) {
    //return ((new Date(d1.replace(/-/g, "\/"))) > (new Date(d2.replace(/-/g, "\/"))));
    var effectYear = d1.split('-')[0];
    var effectMonth = d1.split('-')[1];
    var currentYear = d2.split('-')[0];
    var currentMonth = d2.split('-')[1];
    if (effectYear == currentYear) {
        if (effectMonth == currentMonth) {
            return 0;
        } else if (effectMonth > currentMonth) {
            return 1;
        } else {
            return -1;
        }
    } else {
        return 1;
    }
}
var NumberFormat = function (value, row, index) {
    return new Number(value).ToThousands();
}
var ColumnMerge = function (value, row, index) {
    var _cols = this.column.split(',');
    var _result = '';
    for (var i = 0; i < _cols.length; i++) {
        _result += ' / ' + new Number(row[_cols[i]]).ToThousands();
    }
    return _result.substr(3, _result.length);
}
var PostJson = function (url, json, loading, callback) {
    $.ajax({
        url: configuration.tscweburl + '/Home/AjaxPostCorssDomain',
        datatype: "json",
        type: 'post',
        cache: false,
        data: { url: url, json: json },
        success: function (data) {
            //成功后调用
            callback(data);
        },
        error: function (e) {
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
var WaitMsg = function (text, title, timmer) {
    $("#WaitForm").find(".modal-title").html(title);
    $("#WaitForm").find(".modal-body-content").html(text);
    $("#WaitForm").find(".btn-primary").click(function () {
        $("#WaitForm").modal('hide');
        fn && fn.call(this, false);
    });
    if (timmer) {
        var _seconds = 1;
        var _interval = setInterval(function () {
            if (_seconds == 0) {
                clearInterval(_interval);
                CloseWaitMsg();
                return
            }
            $("#WaitForm").find(".modal-title").html(title + "[" + _seconds + "秒后自动关闭]");
            _seconds--;
        }, 1000);
    }
    return $("#WaitForm").modal({ backdrop: 'static', keyboard: false });
};

var InitOneLevOrgMutiSelect = function (id) {
    $('#'+id).multiselect({
        includeSelectAllOption: true,
        selectAllText: '全选',
        enableFiltering: false,
        //maxHeight: 300,
        buttonWidth: '80%',
        onInitialized: function (select, container) {
            var _json = {};
            _json.PermissionsAttributes = "";//system.PermissionPoint.TeacherManage_BaseInfo_Search;
            _json.UserId = system.LoginUser.OnlyEmployeeID;
            PostJson(configuration.tscapiurl + '/Permission/GetRangeOneOrgs', JSON.stringify(_json), false, function (data) {
                var _date = $.parseJSON(data);
                if (_date.Code=="0") {
                    var options = [];
                    $.each(_date.Data, function (i, item) {
                        options.push({
                            label: item.UserOrgName, title: item.UserOrgName, value: item.UserOrgGuid
                        });
                    });
                    $('#' + id).multiselect('dataprovider', options);
                    CloseWaitMsg();
                }
                else
                    WaitMsg(_date.Message, "错误", true);
            });
        }
    });
}
var InitEffectMonth = function (id,y=2017,m=0,d=1) {
    $('#' + id).datetimepicker({
        format: 'yyyy-mm',
        weekStart: 1,
        startView: 3,
        language: 'zh-CN',
        minView: 3,
        autoclose: true,
        todayBtn: false,
        startDate: new Date(y, m, d),
    });
}

var CloseWaitMsg = function () {
    $("#WaitForm").modal('hide');
}
var ExportFile = function (url, json) {
    WaitMsg("正在导出", "提示", false);
    var _form = $("<form>");
    _form.attr('style', 'display:none');
    _form.attr('target', '');
    _form.attr('method', 'post');
    _form.attr('action', url);
    var _input = $('<input>');
    _input.attr('type', 'hidden');
    _input.attr('name', 'json');
    _input.attr('value', JSON.stringify(json));
    _form.append(_input);
    $('body').append(_form);
    _form.submit();
    _form.remove();
    CloseWaitMsg();
}
var ExportLargeFile = function (action, url, json) {
    var _form = $("<form>");
    _form.attr('style', 'display:none');
    _form.attr('target', '');
    _form.attr('method', 'post');
    _form.attr('action', action);
    var _input_url = $('<input>');
    _input_url.attr('type', 'hidden');
    _input_url.attr('name', 'url');
    _input_url.attr('value', url);
    _form.append(_input_url);
    var _input_json = $('<input>');
    _input_json.attr('type', 'hidden');
    _input_json.attr('name', 'json');
    _input_json.attr('value', JSON.stringify(json));
    _form.append(_input_json);
    $('body').append(_form);
    _form.submit();
    _form.remove();
}
var ExportLargeFileByOrg = function (action, url, json,org) {
    var _form = $("<form>");
    _form.attr('style', 'display:none');
    _form.attr('target', '');
    _form.attr('method', 'post');
    _form.attr('action', action);
    var _input_url = $('<input>');
    _input_url.attr('type', 'hidden');
    _input_url.attr('name', 'url');
    _input_url.attr('value', url);
    _form.append(_input_url);
    var _input_json = $('<input>');
    _input_json.attr('type', 'hidden');
    _input_json.attr('name', 'json');
    _input_json.attr('value', JSON.stringify(json));
    _form.append(_input_json);
    var _input_url = $('<input>');
    _input_url.attr('type', 'hidden');
    _input_url.attr('name', 'org');
    _input_url.attr('value', org);
    _form.append(_input_url);
    $('body').append(_form);
    _form.submit();
    _form.remove();
}
$.ajax({
    async: false, type: "POST", url: configuration.tscweburl + '/Home/GetLoginUser',
    success: function (data) {
        system.LoginUser = data;
    }
});
var GetButtonHtml = function (point, args) {
    var _result = "";
    $.ajax({
        async: false, type: "POST", url: configuration.tscweburl + '/Home/GetButtonHtml', data: { point: point, args: args },
        success: function (data) {
            _result = data;
        }
    });
    return _result;
}
var GetCookie = function (name) {
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) return unescape(arr[2]); return null;
}
var SetCookie = function (name, value) {
    var Days = 30;
    var exp = new Date();
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString() + ";path=/";
}

//初始化radio列表，根据登录用户id获取权限范围的组织列表
var InitOneOrgRadio = function (id) {
    $('#' + id).multiselect({
        placeholder: "请选择",
        disableIfEmpty: true,
        //buttonWidth: '80%',
        onInitialized: function (select, container) {
            var _json = {};
            _json.PermissionsAttributes = "";
            _json.UserId = system.LoginUser.OnlyEmployeeID;
            PostJson(configuration.tscapiurl + '/Permission/GetRangeOneOrgs', JSON.stringify(_json), false, function (data) {
                var _data = $.parseJSON(data);
                if (_data.Code=="0") {
                    var options = [];
                    $.each(_data.Data, function (i, item) {
                        options.push({
                            label: item.UserOrgName, title: item.UserOrgName, value: item.UserOrgGuid
                        });
                    });
                    $('#' + id).multiselect('dataprovider', options);
                    CloseWaitMsg();
                }
                else
                    WaitMsg(_data.Message, "错误", true);
            });
        }
    });
}

var GetQueryString=function (name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

////初始化fileinput
//var FileInput = function () {
//    var oFile = new Object();

//    //初始化fileinput控件（第一次初始化）
//    oFile.Init = function (ctrlName, uploadUrl) {
//        var control = $('#' + ctrlName);

//        //初始化上传控件的样式
//        control.fileinput({
//            language: 'zh', //设置语言
//            uploadUrl: uploadUrl, //上传的地址
//            allowedFileExtensions: ['xlsx', 'xls'],//接收的文件后缀
//            browseClass: "btn  btn-circle btn-primary", //按钮样式     
//        });

//        //导入文件上传完成之后的事件
//        control.on("fileuploaded", function (event, data, previewId, index) {
//            var result = data.response;
//            if (result.IsSuccess != true) {
//                WaitMsg(result.AlertMessage, "警告", false);
//                return;
//            } else {
//                WaitMsg(result.AlertMessage, "提示", true);
//                GetTableData();
//            }
//        });

//    }
//    return oFile;
//};