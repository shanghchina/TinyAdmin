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
    initApiGrid(appSign)
}

/////////////////////////////////////
var getConditionJson = function (params) {
    var pagination = $('#gridTable').ysTable('getPagination', params);
    var divparam = $('#searchDiv').getWebControls(pagination);
    var paramjson = {
        pagination: pagination,
        param: divparam
    };
    //console.log(paramjson)
    return paramjson;
}

function initApiGrid(appSign) {
    var queryUrl = configuration.tscapiurl + '/api/ApiDataDict/GetPageListJson';
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
                //url: configuration.tscapiurl + '/api/ApiDataDict/GetPageListJson',
                //json: JSON.stringify(_json),
                param: _json.param,
                pagination: _json.pagination
            };
            return param;
        }
    });
}

function searchGrid() {
    $('#gridTable').ysTable('search');
    resetToolbarStatus();
}

function showSaveForm(bAdd) {
    var id = 0;
    if (!bAdd) {
        var selectedRow = $("#gridTable").bootstrapTable("getSelections");
        if (!ys.checkRowEdit(selectedRow)) {
            return;
        }
        else {
            id = selectedRow[0].Id;
        }
    }
    ys.openDialog({
        title: id > 0 ? "编辑字典" : "添加字典",
        content: '/DemoApiRequest/DataDictManage/Edit' + '?id=' + id,
        width: "768px",
        height: "280px",
        callback: function (index, layero) {
            var iframeWin = window[layero.find('iframe')[0]['name']];
            iframeWin.saveForm(index);
        }
    });
}

function deleteForm() {
    var selectedRow = $("#gridTable").bootstrapTable("getSelections");
    if (ys.checkRowDelete(selectedRow)) {
        ys.confirm("确认要删除选中的" + selectedRow.length + "条数据吗？", function () {
            var ids = ys.getIds(selectedRow);
            ys.ajax({
                url: configuration.tscapiurl + '/api/ApiDataDict/DeleteFormJson' + '?ids=' + ids,
                type: "post",
                success: function (obj) {
                    if (obj.Tag == 1) {
                        ys.msgSuccess(obj.Message);
                        searchGrid();
                    }
                    else {
                        ys.msgError(obj.Message);
                    }
                }
            });
        });
    }
}

function showDataDictDetailForm(dictType) {
    var url = '/SystemManage/DataDictDetail/DataDictDetailIndex' + '?dictType=' + dictType;
    createMenuItem(url, "字典数据");
}
