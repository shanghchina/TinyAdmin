//document.location.host   //表示当前域名 + 端口号
//document.location.hostname  //表示域名
//document.location.href   //表示完整的URL
//document.location.port   //表示端口号
//document.location.protocol   //表示当前的网络协议
var protocolStr = document.location.protocol; //当前的网络协议

var configuration = {
    tscweburl: protocolStr + '//tsc.local.com',
    tscapiurl: 'http://tscapi.local.com',
    appid: 'TSC',
}
// 参考用
var system = {
    LoginUser: null,
    PermissionPoint: {
        // 页面
        TeacherManage_BaseInfo_Search: "MTeacher_TeacherManage_BaseInfo_Search",
        TeacherManage_BaseInfo_Export: "MTeacher_TeacherManage_BaseInfo_Export",
        TeacherManage_BaseInfo_Import: "MTeacher_TeacherManage_BaseInfo_Import",
        TeacherManage_BaseInfo_Update: "MTeacher_TeacherManage_BaseInfo_Update",
        TeacherManage_BaseInfo_ShowHistory: "MTeacher_TeacherManage_BaseInfo_ShowHistory",
        TeacherManage_BaseInfo_HistoryDelete:"MTeacher_TeacherManage_BaseInfo_HistoryDelete"

    }
}