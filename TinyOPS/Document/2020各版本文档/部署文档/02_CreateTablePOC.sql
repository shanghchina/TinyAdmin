/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2005                    */
/* Created on:     2019/10/24 14:27:11                          */
/*==============================================================*/
use Tiny_OPS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_APP_Appid')
            and   type = 'U')
   drop table T_APP_Appid
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_APP_Authorize')
            and   type = 'U')
   drop table T_APP_Authorize
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_APP_SysLog')
            and   type = 'U')
   drop table T_APP_SysLog
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_APP_WhiteList')
            and   type = 'U')
   drop table T_APP_WhiteList
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_EHS_Org')
            and   type = 'U')
   drop table T_EHS_Org
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_EHS_Position')
            and   type = 'U')
   drop table T_EHS_Position
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_EHS_PositionTemplate')
            and   type = 'U')
   drop table T_EHS_PositionTemplate
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('T_EXT_BasicData')
            and   name  = 'Index_BasicDataGuid'
            and   indid > 0
            and   indid < 255)
   drop index T_EXT_BasicData.Index_BasicDataGuid
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_EXT_BasicData')
            and   type = 'U')
   drop table T_EXT_BasicData
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_EXT_Class')
            and   type = 'U')
   drop table T_EXT_Class
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_EXT_ClassTeachTime')
            and   type = 'U')
   drop table T_EXT_ClassTeachTime
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_EXT_Course')
            and   type = 'U')
   drop table T_EXT_Course
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_EXT_CourseRange')
            and   type = 'U')
   drop table T_EXT_CourseRange
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_EXT_ItemType')
            and   type = 'U')
   drop table T_EXT_ItemType
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_EXT_SyncHistory')
            and   type = 'U')
   drop table T_EXT_SyncHistory
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_EXT_ThresholdValue')
            and   type = 'U')
   drop table T_EXT_ThresholdValue
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('T_POC_BasicDataMap')
            and   name  = 'Index_BasicDataMappingGuid'
            and   indid > 0
            and   indid < 255)
   drop index T_POC_BasicDataMap.Index_BasicDataMappingGuid
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_POC_BasicDataMap')
            and   type = 'U')
   drop table T_POC_BasicDataMap
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('T_POC_Extractor')
            and   name  = 'Index_ExtractorGuid'
            and   indid > 0
            and   indid < 255)
   drop index T_POC_Extractor.Index_ExtractorGuid
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_POC_Extractor')
            and   type = 'U')
   drop table T_POC_Extractor
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('T_POC_ExtractorDetail')
            and   name  = 'Index_ExtractorDetailGuid'
            and   indid > 0
            and   indid < 255)
   drop index T_POC_ExtractorDetail.Index_ExtractorDetailGuid
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_POC_ExtractorDetail')
            and   type = 'U')
   drop table T_POC_ExtractorDetail
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('T_POC_ExtractorQuery')
            and   name  = 'Index_ExtractorQueryParam'
            and   indid > 0
            and   indid < 255)
   drop index T_POC_ExtractorQuery.Index_ExtractorQueryParam
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_POC_ExtractorQuery')
            and   type = 'U')
   drop table T_POC_ExtractorQuery
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('T_POC_Product')
            and   name  = 'Index_ProductGuid'
            and   indid > 0
            and   indid < 255)
   drop index T_POC_Product.Index_ProductGuid
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_POC_Product')
            and   type = 'U')
   drop table T_POC_Product
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('T_POC_ProductType')
            and   name  = 'Index_ProductTypeGuid'
            and   indid > 0
            and   indid < 255)
   drop index T_POC_ProductType.Index_ProductTypeGuid
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_POC_ProductType')
            and   type = 'U')
   drop table T_POC_ProductType
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('T_POC_ProductTypeMap')
            and   name  = 'Index_ProductTypeMappingGuid'
            and   indid > 0
            and   indid < 255)
   drop index T_POC_ProductTypeMap.Index_ProductTypeMappingGuid
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_POC_ProductTypeMap')
            and   type = 'U')
   drop table T_POC_ProductTypeMap
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_RLS_Permission')
            and   type = 'U')
   drop table T_RLS_Permission
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_RLS_RolePermission')
            and   type = 'U')
   drop table T_RLS_RolePermission
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_RLS_UserRole')
            and   type = 'U')
   drop table T_RLS_UserRole
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('T_RLS_UserRoleRange')
            and   name  = 'IDX_TRLSUserRoleRange_UserIDRoleIDRoleRangeValue'
            and   indid > 0
            and   indid < 255)
   drop index T_RLS_UserRoleRange.IDX_TRLSUserRoleRange_UserIDRoleIDRoleRangeValue
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_RLS_UserRoleRange')
            and   type = 'U')
   drop table T_RLS_UserRoleRange
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('T_SYS_Dictionary')
            and   name  = 'Index_DictGuid'
            and   indid > 0
            and   indid < 255)
   drop index T_SYS_Dictionary.Index_DictGuid
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_SYS_Dictionary')
            and   type = 'U')
   drop table T_SYS_Dictionary
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_SYS_InvokeLog')
            and   type = 'U')
   drop table T_SYS_InvokeLog
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('T_SYS_TaskPlan')
            and   name  = 'Index_TaskGuid'
            and   indid > 0
            and   indid < 255)
   drop index T_SYS_TaskPlan.Index_TaskGuid
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_SYS_TaskPlan')
            and   type = 'U')
   drop table T_SYS_TaskPlan
go

/*==============================================================*/
/* Table: T_APP_Appid                                           */
/*==============================================================*/
create table T_APP_Appid (
   Id                   int                  identity(1, 1),
   AppWhiteListId       int                  null,
   Appid                nvarchar(50)         collate Chinese_PRC_CI_AS null,
   SecretKey            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   FromSystem           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   FromSystemName       nvarchar(500)        collate Chinese_PRC_CI_AS null,
   IsEnabled            bit                  not null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             null,
   constraint PK_APP_Appid primary key (Id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: T_APP_Authorize                                       */
/*==============================================================*/
create table T_APP_Authorize (
   Id                   int                  identity(1, 1),
   AppidId              int                  null,
   Controller           nvarchar(100)        collate Chinese_PRC_CI_AS null,
   Action               nvarchar(100)        collate Chinese_PRC_CI_AS null,
   IsMd5                bit                  not null,
   IsEnabled            bit                  not null,
   IsInvokeLog          bit                  null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             null,
   constraint PK_APP_Md5Validate primary key (Id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: T_APP_SysLog                                          */
/*==============================================================*/
create table T_APP_SysLog (
   Id                   bigint               identity(1, 1),
   LogType              nvarchar(50)         collate Chinese_PRC_CI_AS null,
   LogDesc              nvarchar(500)        collate Chinese_PRC_CI_AS null,
   LogParam             nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   LogInfo              nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   UpdaterUserId        nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   UpdaterUserName      nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             null,
   constraint PK_APP_SYSLOG primary key (Id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: T_APP_WhiteList                                       */
/*==============================================================*/
create table T_APP_WhiteList (
   Id                   bigint               identity(1, 1),
   Ip                   nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   FromSystemName       nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   IsEnabled            bit                  not null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             null,
   constraint PK_APP_WhiteList primary key (Id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: T_EHS_Org                                             */
/*==============================================================*/
create table T_EHS_Org (
   ID                   bigint               identity(1, 1),
   UserOrgGuid          uniqueidentifier     not null,
   UserOrgCode          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   UserOrgName          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   UserOrgFullName      nvarchar(500)        collate Chinese_PRC_CI_AS null,
   UserOrgStatus        bit                  not null,
   ParentID             uniqueidentifier     null,
   IsLevelOne           bit                  not null,
   CreatedDate          datetime             null,
   UpdateDate           datetime             null,
   AppID                nvarchar(1000)       collate Chinese_PRC_CI_AS null,
   constraint CT_Key_EHS_Org primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: T_EHS_Position                                        */
/*==============================================================*/
create table T_EHS_Position (
   ID                   bigint               identity(1, 1),
   PositionGuid         uniqueidentifier     not null,
   PositionCode         nvarchar(50)         collate Chinese_PRC_CI_AS null,
   PositionDescription  nvarchar(500)        collate Chinese_PRC_CI_AS null,
   IsCharge             bit                  not null,
   PositionStatus       bit                  not null,
   PositionName         nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ParentID             uniqueidentifier     null,
   UserOrgID            uniqueidentifier     not null,
   PositionTemplateID   uniqueidentifier     not null,
   CreatedDate          datetime             null,
   UpdateDate           datetime             null,
   constraint CT_Key_EHS_Position primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: T_EHS_PositionTemplate                                */
/*==============================================================*/
create table T_EHS_PositionTemplate (
   Id                   bigint               identity(1, 1),
   PositionTemplateId   uniqueidentifier     not null,
   Code                 nvarchar(100)        collate Chinese_PRC_CI_AS null,
   Name                 nvarchar(100)        collate Chinese_PRC_CI_AS null,
   Description          nvarchar(500)        collate Chinese_PRC_CI_AS null,
   PositionTemplateStatus int                  null,
   EnableDateTime       datetime             null,
   DisableDateTime      datetime             null,
   JobGradeId           uniqueidentifier     not null,
   OrgId                uniqueidentifier     not null,
   JobId                uniqueidentifier     not null,
   ParentId             uniqueidentifier     not null,
   PositionTypeId       uniqueidentifier     not null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             not null,
   constraint CT_Key_EHS_PositionTemplate primary key (Id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: T_EXT_BasicData                                       */
/*==============================================================*/
create table T_EXT_BasicData (
   Id                   bigint               identity(1, 1),
   BasicDataGuid        uniqueidentifier     not null,
   FromSystem           int                  not null,
   OneOrgId             nvarchar(50)         null,
   OneOrgName           nvarchar(50)         null,
   DictTypeCode         nvarchar(50)         null,
   DictTypeName         nvarchar(100)        null,
   DictSort             int                  not null,
   DictId               nvarchar(50)         null,
   DictValue            nvarchar(50)         null,
   DictCreatetime       datetime             null,
   DictUpdatetime       datetime             null,
   DictCode             nvarchar(50)         null,
   DictIsSys            int                  null,
   DictStatus           int                  null,
   DictDescribe         nvarchar(50)         null,
   UpdaterUserId        nvarchar(50)         not null,
   UpdaterUserName      nvarchar(50)         not null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             null,
   POCSource            int                  not null,
   constraint CT_Key_BasicData primary key (Id)
)
go
/*==============================================================*/
/* Index: Index_BasicDataGuid                                   */
/*==============================================================*/
create unique index Index_BasicDataGuid on T_EXT_BasicData (
BasicDataGuid ASC
)
go

/*==============================================================*/
/* Table: T_EXT_Class                                           */
/*==============================================================*/
create table T_EXT_Class (
   Id                   bigint               identity(1, 1),
   FromSystem           int                  not null,
   TeachLevelOneOrgID   uniqueidentifier     not null,
   TeachLevelOneOrgName nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   CampusID             nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   TeachNetOrgID        uniqueidentifier     not null,
   TeachNetOrgName      nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   TeachOrgFinaUnitEOSID int                  not null,
   ClassID              nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   ClassName            nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   CourseID             nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   ClassYear                 int                  not null,
   TermID               nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   TermName             nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   OpenDate             datetime             null,
   CloseDate            datetime             null,
   MinStudentAmoun      int                  not null,
   MaxStudentAmoun      int                  not null,
   TotalClassHour       decimal(18,2)        not null,
   TotalClassHourName   nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   ClassMasterUserID    nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   ClassMasterUserName  nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   ClassStatus          int                  not null,
   ProductCreatedDate   datetime             not null,
   ProductUpdateDate    datetime             not null,
   Describe             nvarchar(1000)       collate Chinese_PRC_CI_AS not null,
   SyncHistoryID        bigint               not null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             null,
   VersionNum           bigint               not null,
   CourseStartTime      datetime             null,
   CourseEndTime        datetime             null,
   POCSource            int                  not null,
   constraint CT_PK_ProductClass primary key (Id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: T_EXT_ClassTeachTime                                  */
/*==============================================================*/
create table T_EXT_ClassTeachTime (
   Id                   bigint               identity(1, 1),
   ProductClassID       bigint               not null,
   ClassID              nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   WeekDay              int                  not null,
   StartTime            nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   EndTime              nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   TeacherUserID        uniqueidentifier     not null,
   TeacherUserName      nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   ClassroomID          nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   ClassroomName        nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   SyncHistoryID        bigint               not null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             null,
   VersionNum           bigint               not null,
   POCSource            int                  not null,
   constraint CT_PK_ProductClass_TeachTime primary key (Id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: T_EXT_Course                                          */
/*==============================================================*/
create table T_EXT_Course (
   Id                   bigint               identity(1, 1),
   FromSystem           int                  not null,
   TeachLevelOneOrgID   uniqueidentifier     not null,
   TeachLevelOneOrgName nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   CourseID             nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   CourseName           nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   ProductTypeOneID     nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   ProductTypeTwoID     nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   CourseYear           int                  not null,
   TermID               nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   TermName             nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   GradeID              nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   GradeName            nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   ClassTypeID          nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   ClassTypeName        nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   FlagID               nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   FlagName             nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   SubjectID            nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   SubjectName          nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   CategoryID           nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   CategoryName         nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   TotalClassHour       decimal(18,2)        not null,
   TotalClassHourName   nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   FeeUnitPrice         decimal(18,10)       not null,
   FeeUnitPriceName     nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   CourseStatus         int                  not null,
   ProductCreatedDate   datetime             not null,
   ProductUpdateDate    datetime             not null,
   Describe             nvarchar(1000)       collate Chinese_PRC_CI_AS not null,
   SyncHistoryID        bigint               not null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             null,
   VersionNum           bigint               not null,
   ExtractStatus        int                  not null,
   FKProductCourseGuid  uniqueidentifier     null,
   POCSource            int                  not null,
   constraint CT_PK_ProductCourse primary key (Id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: T_EXT_CourseRange                                     */
/*==============================================================*/
create table T_EXT_CourseRange (
   Id                   bigint               identity(1, 1),
   ProductCourseID      bigint               not null,
   CourseID             nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   CampusID             nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   TeachNetOrgID        uniqueidentifier     not null,
   TeachNetOrgName      nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   TeachOrgFinaUnitEOSID int                  not null,
   FeeUnitPrice         decimal(18,10)       not null,
   FeePriceName         nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   SyncHistoryID        bigint               not null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             null,
   VersionNum           bigint               not null,
   POCSource            int                  not null,
   constraint CT_PK_ProductCourse_CampusRange primary key (Id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: T_EXT_ItemType                                        */
/*==============================================================*/
create table T_EXT_ItemType (
   Id                   bigint               identity(1, 1),
   FromSystem           int                  not null,
   TeachLevelOneOrgID   uniqueidentifier     not null,
   TeachLevelOneOrgName nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   ProductTypeID        nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   ProductTypeName      nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   NodeFlag             int                  not null,
   ParentProductTypeID  nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   ItemTypeStatus       int                  not null,
   ProductCreatedDate   datetime             not null,
   ProductUpdateDate    datetime             not null,
   Describe             nvarchar(1000)       collate Chinese_PRC_CI_AS not null,
   SyncHistoryID        bigint               not null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             null,
   VersionNum           bigint               not null,
   POCSource            int                  not null,
   constraint CT_PK_ProductItemType primary key (Id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: T_EXT_SyncHistory                                     */
/*==============================================================*/
create table T_EXT_SyncHistory (
   Id                   bigint               identity(1, 1),
   FromSystem           int                  not null,
   TeachLevelOneOrgID   uniqueidentifier     not null,
   TeachLevelOneOrgName nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   SynchroDate          datetime             not null,
   DataType             int                  not null,
   DataAPIPath          nvarchar(200)        collate Chinese_PRC_CI_AS not null,
   DataJson             varchar(Max)         collate Chinese_PRC_CI_AS not null,
   SyncStatus           int                  not null,
   ErrorMessage         varchar(Max)         collate Chinese_PRC_CI_AS not null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             null,
   VersionNum           bigint               not null,
   POCSource            int                  not null,
   constraint CT_PK_ProductSynchroHistory primary key (Id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: T_EXT_ThresholdValue                                  */
/*==============================================================*/
create table T_EXT_ThresholdValue (
   Id                   bigint               identity(1, 1),
   FromSystem           int                  not null,
   ChargeLevelOneOrgID  uniqueidentifier     not null,
   ProductID            nvarchar(50)         not null,
   LastSaleDate         datetime             not null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             null,
   VersionNum           bigint               not null,
   constraint CT_PK_Product_ThresholdValue primary key (Id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: T_POC_BasicDataMap                                    */
/*==============================================================*/
create table T_POC_BasicDataMap (
   Id                   bigint               identity(1, 1),
   BasicDataMapGuid     uniqueidentifier     not null,
   FKBasicDataGuid      uniqueidentifier     not null,
   FKDictGuid           uniqueidentifier     not null,
   SysIsCatalog         bit                  null,
   SysCatalogTitle      nvarchar(100)        null,
   UpdaterUserId        nvarchar(50)         not null,
   UpdaterUserName      nvarchar(50)         not null,
   UpdateDate           datetime             null,
   CreatedDate          datetime             not null,
   constraint CT_Key_BasicDataMapping primary key (Id)
)
go

/*==============================================================*/
/* Index: Index_BasicDataMappingGuid                            */
/*==============================================================*/
create unique index Index_BasicDataMappingGuid on T_POC_BasicDataMap (
BasicDataMapGuid ASC
)
go

/*==============================================================*/
/* Table: T_POC_Extractor                                       */
/*==============================================================*/
create table T_POC_Extractor (
   Id                   bigint               identity(1, 1),
   ExtractorGuid        uniqueidentifier     not null,
   UpdaterUserId        nvarchar(50)         not null,
   UpdaterUserName      nvarchar(50)         not null,
   UpdateDate           datetime             not null,
   CreatedDate          datetime             not null,
   constraint CT_Key_Extractor primary key (Id)
)
go

/*==============================================================*/
/* Index: Index_ExtractorGuid                                   */
/*==============================================================*/
create unique index Index_ExtractorGuid on T_POC_Extractor (
ExtractorGuid ASC
)
go


/*==============================================================*/
/* Table: T_POC_ExtractorDetail                                 */
/*==============================================================*/
create table T_POC_ExtractorDetail (
   Id                   bigint               identity(1, 1),
   FKExtractorGuid      uniqueidentifier     not null,
   ExtractorDetailGuid  uniqueidentifier     not null,
   ExtractorStatus      int                  not null,
   ExtractorNo          nvarchar(50)         not null,
   ProductCount         int                  null,
   ExtractCount         int                  null,
   AssociatedCount      int                  null,
   NotExtractCount      int                  null,
   Year                 int                  not null,
   GradeID              nvarchar(200)        collate Chinese_PRC_CI_AS not null,
   GradeName            nvarchar(200)        collate Chinese_PRC_CI_AS not null,
   FlagID               nvarchar(200)        collate Chinese_PRC_CI_AS not null,
   FlagName             nvarchar(200)        collate Chinese_PRC_CI_AS not null,
   SubjectID            nvarchar(200)        collate Chinese_PRC_CI_AS not null,
   SubjectName          nvarchar(200)        collate Chinese_PRC_CI_AS not null,
   CategoryID           nvarchar(200)        collate Chinese_PRC_CI_AS not null,
   CategoryName         nvarchar(200)        collate Chinese_PRC_CI_AS not null,
   TermID               nvarchar(200)        collate Chinese_PRC_CI_AS not null,
   TermName             nvarchar(200)        collate Chinese_PRC_CI_AS not null,
   ClassTypeID          nvarchar(200)        collate Chinese_PRC_CI_AS not null,
   ClassTypeName        nvarchar(200)        collate Chinese_PRC_CI_AS not null,
   UpdaterUserId        nvarchar(50)         not null,
   UpdaterUserName      nvarchar(50)         not null,
   UpdateDate           datetime             not null,
   CreatedDate          datetime             not null,
   constraint CT_Key_ExtractorDetail primary key (Id)
)
go

/*==============================================================*/
/* Index: Index_ExtractorDetailGuid                             */
/*==============================================================*/
create unique index Index_ExtractorDetailGuid on T_POC_ExtractorDetail (
ExtractorDetailGuid ASC
)
go

/*==============================================================*/
/* Table: T_POC_ExtractorQuery                                  */
/*==============================================================*/
create table T_POC_ExtractorQuery (
   Id                   bigint               identity(1, 1),
   FKExtractorGuid      uniqueidentifier     not null,
   ExtractorQueryGuid   uniqueidentifier     not null,
   SelectFieldTypeCode  nvarchar(50)         not null,
   SelectFieldTypeId    nvarchar(50)         not null,
   SelectFieldTypeName  nvarchar(200)        not null,
   UpdaterUserId        nvarchar(50)         not null,
   UpdaterUserName      nvarchar(50)         not null,
   UpdateDate           datetime             not null,
   CreatedDate          datetime             not null,
   constraint CT_Key_ExtractorQueryParam primary key (Id)
)
go

/*==============================================================*/
/* Index: Index_ExtractorQueryParam                             */
/*==============================================================*/
create unique index Index_ExtractorQueryParam on T_POC_ExtractorQuery (
ExtractorQueryGuid ASC
)
go

/*==============================================================*/
/* Table: T_POC_Product                                         */
/*==============================================================*/
create table T_POC_Product (
   Id                   bigint               identity(1, 1),
   FKDetailGuid         uniqueidentifier     not null,
   ProductGuid          uniqueidentifier     not null,
   ProductNo            nvarchar(50)         null,
   ProductTypeGuid      uniqueidentifier     not null,
   ProductTypeName      nvarchar(100)        null,
   ProductTypeLable     nvarchar(100)        null,
   FromSystem           int                  not null,
   TeachLevelOneOrgID   uniqueidentifier     not null,
   TeachLevelOneOrgName nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   CourseID             nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   CourseName           nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   ProductTypeOneID     nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   ProductTypeTwoID     nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   CourseYear           int                  not null,
   TermID               nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   TermName             nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   GradeID              nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   GradeName            nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   ClassTypeID          nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   ClassTypeName        nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   FlagID               nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   FlagName             nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   SubjectID            nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   SubjectName          nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   CategoryID           nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   CategoryName         nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   TotalClassHour       decimal(18,2)        not null,
   TotalClassHourName   nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   FeeUnitPrice         decimal(18,10)       not null,
   FeeUnitPriceName     nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   CourseStatus         int                  not null,
   ProductCreatedDate   datetime             not null,
   ProductUpdateDate    datetime             not null,
   GradeDictGuid        uniqueidentifier     not null,
   GradeDictName        nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   CategoryDictGuid     uniqueidentifier     not null,
   CategoryDictName     nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   SubjectDictGuid      uniqueidentifier     not null,
   SubjectDictName      nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   TermDictGuid         uniqueidentifier     not null,
   TermDictName         nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   ClassTypeDictGuid    uniqueidentifier     not null,
   ClassTypeDictName    nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   Describe             nvarchar(1000)       collate Chinese_PRC_CI_AS not null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             null,
   CampusCountName      nvarchar(20)         null,
   constraint CT_Key_POC_ProductCourse primary key (Id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: Index_ProductGuid                                     */
/*==============================================================*/
create unique index Index_ProductGuid on T_POC_Product (
ProductGuid ASC
)
go


/*==============================================================*/
/* Table: T_POC_ProductType                                     */
/*==============================================================*/
create table T_POC_ProductType (
   Id                   bigint               identity(1, 1),
   ProductTypeGuid      uniqueidentifier     not null,
   ProductTypeName      nvarchar(50)         not null,
   ProductTypeLevelNo   int                  not null,
   ProductTypeLevelName nvarchar(50)         not null,
   ProductCount         int                  null,
   ParentGuid           uniqueidentifier     not null,
   IsEnabled            bit                  not null,
   Remark               nvarchar(500)        null,
   UpdaterUserId        nvarchar(50)         not null,
   UpdaterUserName      nvarchar(50)         not null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             not null,
   constraint CT_Key_ProductType primary key (Id)
)
go

/*==============================================================*/
/* Index: Index_ProductTypeGuid                                 */
/*==============================================================*/
create unique index Index_ProductTypeGuid on T_POC_ProductType (
ProductTypeGuid ASC
)
go

/*==============================================================*/
/* Table: T_POC_ProductTypeMap                                  */
/*==============================================================*/
create table T_POC_ProductTypeMap (
   Id                   bigint               identity(1, 1),
   ProductTypeMapGuid   uniqueidentifier     not null,
   FKItemTypeId         bigint               not null,
   FKProductTypeGuid    uniqueidentifier     not null,
   ProductTypeLevelNo   int                  null,
   ProductTypeID        nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   ProductTypeTitle     nvarchar(200)        collate Chinese_PRC_CI_AS null,
   Remark               nvarchar(500)        null,
   UpdaterUserId        nvarchar(50)         not null,
   UpdaterUserName      nvarchar(50)         not null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             not null,
   constraint CT_Key_ProductTypeMappingGuid primary key (Id)
)
go

/*==============================================================*/
/* Index: Index_ProductTypeMappingGuid                          */
/*==============================================================*/
create unique index Index_ProductTypeMappingGuid on T_POC_ProductTypeMap (
ProductTypeMapGuid ASC
)
go

/*==============================================================*/
/* Table: T_RLS_Permission                                      */
/*==============================================================*/
create table T_RLS_Permission (
   ID                   bigint               identity(1, 1),
   PermissionsID        bigint               not null,
   DomainTypeID         int                  not null,
   PermissionsName      nvarchar(200)        not null,
   PermissionsParentID  bigint               not null,
   RowStateID           int                  not null,
   PermissionsAttributes nvarchar(200)        not null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             not null,
   constraint CT_Key_RLS_Permission primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: T_RLS_RolePermission                                  */
/*==============================================================*/
create table T_RLS_RolePermission (
   ID                   bigint               identity(1, 1),
   RoleID               bigint               not null,
   PermissionsID        bigint               not null,
   AuthorizationStateID int                  not null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             not null,
   constraint CT_Key_RLS_RolePermission primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: T_RLS_UserRole                                        */
/*==============================================================*/
create table T_RLS_UserRole (
   ID                   bigint               identity(1, 1),
   UserID               int                  not null,
   UserGuid             uniqueidentifier     not null,
   RoleSourceID         int                  not null,
   AuthorizationStateID int                  not null,
   PositionTemplateID   uniqueidentifier     not null,
   UserPositionID       uniqueidentifier     not null,
   RoleID               bigint               not null,
   DomainTypeID         int                  not null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             not null,
   constraint CT_Key_RLS_UserRole primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: T_RLS_UserRoleRange                                   */
/*==============================================================*/
create table T_RLS_UserRoleRange (
   ID                   bigint               identity(1, 1),
   UserID               int                  not null,
   RoleSourceID         int                  not null,
   RoleID               bigint               not null,
   DomainTypeID         int                  not null,
   AuthorizationStateID int                  not null,
   RoleRangeID          bigint               not null,
   RoleRangeTypeID      int                  not null,
   RoleRangeValue       nvarchar(200)        not null,
   ParentRoleRangeValue nvarchar(200)        not null,
   ParentRoleRangeID    bigint               not null,
   RowStateID           int                  not null,
   OneOrgId             uniqueidentifier     null,
   OneOrgCode           nvarchar(50)         null,
   OneOrgName           nvarchar(50)         null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             not null,
   constraint CT_Key_RLS_UserRoleRange primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: IDX_TRLSUserRoleRange_UserIDRoleIDRoleRangeValue      */
/*==============================================================*/
create index IDX_TRLSUserRoleRange_UserIDRoleIDRoleRangeValue on T_RLS_UserRoleRange (
UserID ASC,
RoleID ASC,
RoleRangeValue ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: T_SYS_Dictionary                                      */
/*==============================================================*/
create table T_SYS_Dictionary (
   Id                   bigint               identity(1, 1),
   FromSystem           int                  null,
   OneOrgId             uniqueidentifier     not null,
   OneOrgName           nvarchar(50)         null,
   DictGuid             uniqueidentifier     not null,
   ParentGuid           uniqueidentifier     not null,
   SysDictKey           nvarchar(100)        null,
   SysDictValue         nvarchar(100)        null,
   SysDictDesc          nvarchar(100)        null,
   SysDictSort          int                  not null,
   SysCatalogCode       nvarchar(100)        null,
   SysCatalogName       nvarchar(100)        null,
   SysIsCatalog         bit                  not null,
   SysIsEnabled         bit                  not null,
   SysIsEdit            bit                  not null,
   UpdaterUserId        nvarchar(50)         not null,
   UpdaterUserName      nvarchar(50)         not null,
   UpdateDate           datetime             not null,
   CreatedDate          datetime             not null,
   constraint CT_Key_SYS_Dictionary primary key (Id)
)
go

/*==============================================================*/
/* Index: Index_DictGuid                                        */
/*==============================================================*/
create unique index Index_DictGuid on T_SYS_Dictionary (
DictGuid ASC
)
go

/*==============================================================*/
/* Table: T_SYS_InvokeLog                                       */
/*==============================================================*/
create table T_SYS_InvokeLog (
   Id                   bigint               identity(1, 1),
   InvokeIP             nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   InvokeURL            nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   InvokeFunction       nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   InvokeJson           nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   ReturnJson           nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   UseTime              numeric(18,2)        not null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             null,
   constraint PK_InvokeLog primary key (Id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: T_SYS_TaskPlan                                        */
/*==============================================================*/
create table T_SYS_TaskPlan (
   Id                   bigint               identity(1, 1),
   TaskGuid             uniqueidentifier     not null,
   TaskParam            nvarchar(max)        null,
   TaskType             nvarchar(100)        null,
   TaskName             nvarchar(100)        null,
   TaskStartTime        datetime             null,
   TaskEndTime          datetime             null,
   TaskStatus           int                  null,
   TaskJobId            nvarchar(50)         null,
   Remark               nvarchar(max)        null,
   UpdaterUserId        nvarchar(50)         not null,
   UpdaterUserName      nvarchar(50)         not null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             null,
   constraint CT_Key_TaskGuid primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Index: Index_TaskGuid                                        */
/*==============================================================*/
create unique clustered index Index_TaskGuid on T_SYS_TaskPlan (
TaskGuid ASC
)
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_SYS_UrlConfigure')
            and   type = 'U')
   drop table T_SYS_UrlConfigure
go

/*==============================================================*/
/* Table: T_SYS_UrlConfigure                                    */
/*==============================================================*/
create table T_SYS_UrlConfigure (
   Id                   bigint               identity(1, 1),
   Category             varchar(20)          collate Chinese_PRC_CI_AS not null,
   FromSystem           int                  not null,
   ChargeLevelOneOrgId  varchar(50)          collate Chinese_PRC_CI_AS not null,
   ChargeLevelOneOrgName varchar(50)          collate Chinese_PRC_CI_AS not null,
   TokenUrl             varchar(200)         collate Chinese_PRC_CI_AS not null,
   ErrorUrl             varchar(200)         collate Chinese_PRC_CI_AS null,
   AppId                varchar(100)         collate Chinese_PRC_CI_AS not null,
   AppSecret            varchar(100)         collate Chinese_PRC_CI_AS not null,
   EnableTokenCache     bit                  not null,
   CreatedUserId        varchar(100)         collate Chinese_PRC_CI_AS null,
   UrlConfigureStatus   int                  not null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             null,
   VersionNum           bigint               null,
   ConfigureFunction    varchar(100)         collate Chinese_PRC_CI_AS null,
   POCSource            int                  not null,
   constraint PK_Key_UrlConfigure primary key (Id)
)
on "PRIMARY"
go

create FUNCTION [dbo].[f_split]
 (
     @strText varchar(3000),--待分离的原字符串
     @strSplit varchar(100)--分隔符
 )
 RETURNS @temp Table
 (
     ID int IDENTITY PRIMARY KEY,
     Code varchar(1000)
 )
 AS
 BEGIN
     Declare @intLen int --用来存储待分离原字符串长度
     Declare @intSplitLen int --分隔符长度
     Declare @intIndex int --用来存储分离字符串在原字符串的位置
    Declare @strVal varchar(1000)--用来存储分离出来后的字符串
     --获取原字符串的长度
    Set @intLen = LEN(RTRIM(LTRIM(@strText)))
     Set @intSplitLen = LEN(RTRIM(LTRIM(@strSplit)))
     --原字符串不为空，才继续分离
     If(@intLen > 0)
     Begin
         --循环原字符串，直至原字符串被分离完毕
         While CHARINDEX(@strSplit,@strText)>0
         Begin
             --获取分离字符串在原字符串的位置
             Set @intIndex = CHARINDEX(@strSplit,@strText)
             --获取分离出的字符串，并插入表中
             Set @strVal = RTRIM(LTRIM(LEFT(@strText,@intIndex-1)))
             if(LEN(@strVal)>0)
             Begin
                 Insert Into @temp (Code) values(@strVal)
             End
             --分离后，将分离出的字符串（包括分隔符）从原字符串中删除
             Set @strText = Substring(@strText,@intIndex+@intSplitLen,@intLen-@intIndex)
             --重新设置原字符串的长度
             Set @intLen = LEN(@strText)
         End
         --如果分离后的原字符串依然不为空，则也应该插入表中
         if(LEN(RTRIM(LTRIM(@strText)))>0)
         Begin
             Insert Into @temp (Code) values(@strText)
         End
     End
     return
end
GO