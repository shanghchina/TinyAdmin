$(function () {
    //alert("1");
    doIniApi("1", function (data) {
        //alert("3");
        doInvokeApiGrid(data);
    });
});


function doIniApi(sign, callBackFun) {
    //2 获取sign
    var appSign = GetCheckAppString();
    //console.log(appSign);

    if (callBackFun != undefined && typeof callBackFun == 'function') {
        callBackFun(appSign);
    } else {
        console.log("第二个参数不是函数");
    }
}

function doInvokeApiGrid(appSign) {
    //alert("4");
    //加载grid
    YSinitApiGrid(appSign)
}

/////////////////////////////////////
var getConditionJson = function (params) {
    var pagination = $('#gridTable').ysTable('getPagination', params);
    var divparam = $('#searchDiv').getWebControls(pagination);
    var paramjson = {
        pagination: pagination,
        param: divparam
        //DictType: $('#dictType').val(),
        //Remark: $('#remark').val()
    };
    //console.log(paramjson)
    return paramjson;
}

function YSinitApiGrid(appSign) {
    // var queryUrl = '@Url.Content("~/SystemManage/DataDict/GetPageListJson")';
    // url: configuration.tscweburl + '/Home/AjaxPostCorssDomain',//请求URL
    //var queryUrl = '@Url.Content("~/DemoApiRequest/DataDictManage/AjaxPostCorssDomain")';
    var queryUrl = configuration.tscapiurl + '/api/DataDictApi/GetPageListJson';
    // var appSign = "";

    $('#gridTable').ysTable({
        method: 'post', //请求方式
        contentType: 'application/json',
        dataType: 'json',
        url: queryUrl,
        sortName: 'DictSort',
        sortOrder: 'Asc',
        columns: [
            { checkbox: true, visible: true },
            { field: 'Id', title: 'Id', visible: false },
            { field: 'DictType', title: '字典类型' },
            { field: 'Remark', title: '字典描述' },
            { field: 'DictSort', title: '字典排序' },
            {
                field: 'BaseModifyTime', title: '创建时间', formatter: function (value, row, index) {
                    return ys.formatDate(value, "yyyy-MM-dd HH:mm:ss");
                }
            },
            {
                title: '操作',
                align: 'center',
                formatter: function (value, row, index) {
                    var actions = [];
                    actions.push('<a class="btn btn-info btn-xs" href="#" onclick="showDataDictDetailForm(\'' + row.DictType + '\')"><i class="fa fa-list-ul"></i>字典值</a>');
                    return actions.join('');
                }
            }
        ],
        //headers: { "Authorization": "Basic cGluZ2FuOjEyMzQ1Na==" },
        ajaxOptions: {
            headers: { "Sign": appSign }
        },
        queryParams: function (params) {
            //var pagination = $('#gridTable').ysTable('getPagination', params);
            //var queryString = $('#searchDiv').getWebControls(pagination);

            var _json = getConditionJson(params);
            var param = {
                //url: configuration.tscapiurl + '/api/DataDictApi/GetPageListJson',
                //json: JSON.stringify(_json),
                param: _json.param,
                pagination: _json.pagination
            };
            return param;
        }
        //responseHandler: function (data) {
        //    //debugger
        //    //var data_type = $.type(data);
        //    //if (data_type != "object") {
        //    //    data = $.parseJSON(data);
        //    //}
        //    //远程数据加载之前,处理程序响应数据格式,对象包含的参数: 我们可以对返回的数据格式进行处理
        //    //在ajax后我们可以在这里进行一些事件的处理
        //    //var _data = $.parseJSON(data);
        //}
    });
}

//function initApiGrid() {
//    //初始化数据表格
//    var queryUrl = '/DemoApiRequest/DataDictManage/AjaxPostCorssDomain';
//    $('#gridTable').bootstrapTable({
//        silent: true,
//        striped: true,                      //是否显示行间隔色
//        cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
//        pagination: true,                   //是否显示分页（*）
//        sortable: false,                    //是否启用排序
//        method: 'post', //请求方式
//        contentType: 'application/json',
//        dataType: 'json',
//        url: queryUrl,
//        sortName: 'DictSort',
//        sortOrder: 'Asc',
//        //toolbar: "#table_toolbar",
//        singleSelect: true,             //复选框单选
//        showColumns: true,          // 开启自定义列显示功能
//        showRefresh: false,           // 开启刷新功能
//        pageNumber: 1,               //初始化加载第一页，默认第一页
//        pageSize: 5,                      //每页的记录行数（*）
//        pageList: [10, 25, 50, 100],   //可供选择的每页的行数（*）
//        language: 'zh-CN',            //本地化
//        sidePagination: 'server',  //表示服务端请求
//        url: queryUrl,//请求URL configuration.tscweburl 
//        method: 'post',                     //请求方式
//        responseHandler: function (data) {
//            //远程数据加载之前,处理程序响应数据格式,对象包含的参数: 我们可以对返回的数据格式进行处理
//            //在ajax后我们可以在这里进行一些事件的处理
            
//            debugger
//            return data;
//            //var _data = data;
//            //var data_type = $.type(data);
//            //if (data_type != "object") {
//            //   _data = $.parseJSON(data);
//            //}
//            //if (_data.Code != "0") {
//            //    WaitMsg(_data.Message, "错误", true);
//            //    return { rows: [], total: 0 };
//            //}
//            //return { rows: _data.Data, total: _data.TotalCount };
//        },
//        columns: [
//            { checkbox: true, visible: true },
//            { field: 'Id', title: 'Id', visible: false },
//            { field: 'DictType', title: '字典类型' },
//            { field: 'Remark', title: '字典描述' },
//            { field: 'DictSort', title: '字典排序' },
//            {
//                field: 'BaseModifyTime', title: '创建时间', formatter: function (value, row, index) {
//                    return ys.formatDate(value, "yyyy-MM-dd HH:mm:ss");
//                }
//            },
//            {
//                title: '操作',
//                align: 'center',
//                formatter: function (value, row, index) {
//                    var actions = [];
//                    actions.push('<a class="btn btn-info btn-xs" href="#" onclick="showDataDictDetailForm(\'' + row.DictType + '\')"><i class="fa fa-list-ul"></i>字典值</a>');
//                    return actions.join('');
//                }
//            }
//        ],

//        //设置查询参数
//        queryParams: function (params) {
//            var _json = getConditionJson(params);
//            var param = {
//                url: configuration.tscapiurl + '/api/DataDictApi/GetPageListJson',
//                json: JSON.stringify(_json),
//                DictType: 'DictType111',
//                pagination: { PageSize: '11' }

//            };
//            return param;
//        }
//    });
//}
