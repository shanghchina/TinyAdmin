//document.location.host   //表示当前域名 + 端口号
//document.location.hostname  //表示域名
//document.location.href   //表示完整的URL
//document.location.port   //表示端口号
//document.location.protocol   //表示当前的网络协议
var protocolStr = document.location.protocol; //当前的网络协议

var configuration = {
    tscweburl: protocolStr + '//tinyadmin.local.com',
    tscapiurl: 'http://tinyapi.local.com',
    appid: 'TINY',
}

//// 参考用
//var system = {
//    LoginUser: null,
//    PermissionPoint: {
//        // 教师基本信息 页面
//        TeacherManage_BaseInfo_Search: "MTeacher_TeacherManage_BaseInfo_Search",
//        TeacherManage_BaseInfo_Export: "MTeacher_TeacherManage_BaseInfo_Export",
//        TeacherManage_BaseInfo_Import: "MTeacher_TeacherManage_BaseInfo_Import",
//        TeacherManage_BaseInfo_Update: "MTeacher_TeacherManage_BaseInfo_Update",
//        TeacherManage_BaseInfo_ShowHistory: "MTeacher_TeacherManage_BaseInfo_ShowHistory",
//        TeacherManage_BaseInfo_HistoryDelete:"MTeacher_TeacherManage_BaseInfo_HistoryDelete",
        
//        //薪酬明细
//        SalaryDetail_Export: "MSalary_SalaryDetail_Export",
//        SalaryDetail_ExportEas:"MSalary_SalaryDetail_ExportEas",
//        TeacherSalary_Calculate_Export:"MSalary_TeacherSalary_Calculate_Export",//导出
//        TeacherSalary_Calculate_Import: "MSalary_TeacherSalary_Calculate_Import",//导入
//        TeacherSalary_Calculate_CheckCalcPay: "MSalary_TeacherSalary_Calculate_CheckCalcPay",//核算应发
//        TeacherSalary_Calculate_VerifyData: "MSalary_TeacherSalary_Calculate_VerifyData",//审核数据
//        TeacherSalary_Calculate_Checkout:"MSalary_TeacherSalary_Calculate_Checkout",//结账
//        TeacherSalary_Calculate_Cancel: "MSalary_TeacherSalary_Calculate_Cancel", //撤销

//        //上海少儿_计算薪酬用
//        SETeacherSalary_Calculate_Page: "MSalary_SETeacherSalary_Calculate_Page",//少儿查询页
//        Select_TeacherSalary_Calculate_Export: "MSalary_Select_TeacherSalary_Calculate_Export",//导出
//        Select_TeacherSalary_Calculate_Import: "MSalary_Select_TeacherSalary_Calculate_Import",//导入
//        Select_TeacherSalary_Calculate_CheckCalcPay: "MSalary_Select_TeacherSalary_Calculate_CheckCalcPay",//核算应发
//        Select_TeacherSalary_Calculate_VerifyData: "MSalary_Select_TeacherSalary_Calculate_VerifyData",//审核数据
//        Select_TeacherSalary_Calculate_Checkout: "MSalary_Select_TeacherSalary_Calculate_Checkout",//结账
//        Select_TeacherSalary_Calculate_Cancel: "MSalary_Select_TeacherSalary_Calculate_Cancel", //撤销

//        //生成课时数
//        ClassHours_Statistics_Create: "MCourse_ClassHours_Statistics_Create",
//        ClassHours_Statistics_SearchList: "MCourse_ClassHours_Statistics_SearchList",//查询
//        ClassHours_Statistics_Cancel: "MCourse_ClassHours_Statistics_Cancel",//撤销
//        ClassHours_Statistics_Verify:"MCourse_ClassHours_Statistics_Verify",//生成课时数审核
//        ClassHours_Statistics_ExportDetail: "MCourse_ClassHours_Statistics_ExportDetail",//生成课时数【导出明细】
//        ClassHours_Detail_ExportClassDeail: "MCourse_ClassHours_Detail_ExportClassDeail",//生成课时数明细页面【导出】
//        ClassHours_Detail_SearchList:"MCourse_ClassHours_Detail_SearchList",//课时数明细页面【查询】


//        //数据字典
//        Dictionary_Catagory_Search: "MSysConfig_Dictionary_Catagory_Search",
//        Dictionary_Catagory_Create: "MSysConfig_Dictionary_Catagory_Create",
//        Dictionary_Catagory_Update: "MSysConfig_Dictionary_Catagory_Update",
//        Dictionary_Item_Search: "MSysConfig_Dictionary_Item_Search",
//        Dictionary_Item_Create: "MSysConfig_Dictionary_Item_Create",
//        Dictionary_Item_Update: "MSysConfig_Dictionary_Item_Update",
//        Dictionary_Item_Enable: "MSysConfig_Dictionary_Item_Enable",
//        Dictionary_Item_Disable: "MSysConfig_Dictionary_Item_Disable",
//        //核算规则
//        AccountItem_Main_Create: "MSysConfig_AccountItem_Main_Create",
//        AccountItem_Main_Update: "MSysConfig_AccountItem_Main_Update",
//        AccountItem_Detail_Create: "MSysConfig_AccountItem_Detail_Create",
//        AccountItem_Detail_Update: "MSysConfig_AccountItem_Detail_Update",

//        //补贴页面
//        IncomeMain_Search:"MSysConfig_IncomeMain_Search",
//        IncomePerson_Search:"MSysConfig_IncomePerson_Search",
//        IncomeCount_Search:"MSysConfig_IncomeCount_Search",
//        IncomeMain_Edit:"MSysConfig_IncomeMain_Edit",
//        IncomePerson_Edit:"MSysConfig_IncomePerson_Edit",
//        IncomeCount_Edit: "MSysConfig_IncomeCount_Edit",
//        //IncomeCount_Update:"MSysConfig_IncomeCount_Edit",//批量修改

//        //补贴页面历史操作
//        IncomeMain_History: "MSysConfig_IncomeMain_History",
//        IncomePerson_History: "MSysConfig_IncomePerson_History",
//        IncomeCount_History: "MSysConfig_IncomeCount_History",
//        IncomeMain_Delete: "MSysConfig_IncomeMain_History_delete",
//        IncomePerson_Delete: "MSysConfig_IncomePerson_History_delete",
//        IncomeCount_Delete: "MSysConfig_IncomeCount_History_delete",
        

//    },
//    GlobalConstConfig: {
//        GUID_ORG_WAIYU: "E2165BA6-B521-4429-BCCF-DB0E0D753B39",
//        GUID_ORG_SHSE: "81BC7BD3-1A85-4BD0-AB79-41F7F00995A5"
//    },
//}