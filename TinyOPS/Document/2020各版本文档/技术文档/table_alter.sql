if exists (select 1
            from  sysindexes
           where  id    = object_id('t_sys_user')
            and   name  = 'Index_Guid'
            and   indid > 0
            and   indid < 255)
   drop index t_sys_user.Index_Guid
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_sys_user')
            and   type = 'U')
   drop table t_sys_user
go

/*==============================================================*/
/* Table: t_sys_user                                            */
/*==============================================================*/
create table t_sys_user (
   Id                   bigint               identity,
   tb_guid              varchar(40)          not null,
   avatar_id            bigint               null,
   email                varchar(100)         null,
   is_admin             bit                  not null,
   is_enabled           int                  not null,
   password             varchar(255)         null,
   username             varchar(255)         null,
   dept_id              bigint               null,
   phone                varchar(50)          null,
   post_id              bigint               null,
   last_password_reset_time datetime             null,
   nick_name            varchar(200)         null,
   sex                  varchar(20)          null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             null,
   constraint PK_T_SYS_USER primary key (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('t_sys_user') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 't_sys_user' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '系统用户', 
   'user', @CurrentUser, 'table', 't_sys_user'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_sys_user')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_sys_user')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'avatar_id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'avatar_id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '头像',
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'avatar_id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_sys_user')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'email')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'email'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '邮箱',
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'email'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_sys_user')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'is_enabled')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'is_enabled'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '状态：1启用、0禁用',
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'is_enabled'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_sys_user')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'password')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'password'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '密码',
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'password'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_sys_user')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'username')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'username'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户名',
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'username'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_sys_user')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'dept_id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'dept_id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '部门名称id',
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'dept_id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_sys_user')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'phone')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'phone'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '手机号码',
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'phone'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_sys_user')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'post_id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'post_id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '岗位名称',
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'post_id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_sys_user')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'last_password_reset_time')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'last_password_reset_time'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改密码的日期',
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'last_password_reset_time'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_sys_user')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'nick_name')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'nick_name'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '昵称',
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'nick_name'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_sys_user')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'sex')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'sex'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '性别',
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'sex'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_sys_user')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatedDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'CreatedDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建日期',
   'user', @CurrentUser, 'table', 't_sys_user', 'column', 'CreatedDate'
go

/*==============================================================*/
/* Index: Index_Guid                                            */
/*==============================================================*/
create unique index Index_Guid on t_sys_user (
tb_guid ASC
)
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('t_sys_token')
            and   name  = 'Index_Guid'
            and   indid > 0
            and   indid < 255)
   drop index t_sys_token.Index_Guid
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_sys_token')
            and   type = 'U')
   drop table t_sys_token
go

/*==============================================================*/
/* Table: t_sys_token                                           */
/*==============================================================*/
create table t_sys_token (
   id                   bigint               identity,
   tb_guid              varchar(40)          not null,
   app_name             varchar(100)         not null,
   app_id               varchar(100)         not null,
   app_secret           varchar(200)         not null,
   access_token         varchar(200)         null,
   begin_datetime       datetime             null,
   expried_datetime     datetime             null,
   time_stamp           int                  not null,
   CreatedDate          datetime             not null,
   UpdateDate           datetime             null,
   constraint PK_T_SYS_TOKEN primary key (id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('t_sys_token') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 't_sys_token' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '系统令牌表', 
   'user', @CurrentUser, 'table', 't_sys_token'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_sys_token')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_sys_token', 'column', 'id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', @CurrentUser, 'table', 't_sys_token', 'column', 'id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_sys_token')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'app_name')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_sys_token', 'column', 'app_name'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '头像',
   'user', @CurrentUser, 'table', 't_sys_token', 'column', 'app_name'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_sys_token')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'app_id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_sys_token', 'column', 'app_id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '头像',
   'user', @CurrentUser, 'table', 't_sys_token', 'column', 'app_id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_sys_token')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'app_secret')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_sys_token', 'column', 'app_secret'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '邮箱',
   'user', @CurrentUser, 'table', 't_sys_token', 'column', 'app_secret'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_sys_token')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'access_token')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_sys_token', 'column', 'access_token'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '密码',
   'user', @CurrentUser, 'table', 't_sys_token', 'column', 'access_token'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_sys_token')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'begin_datetime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_sys_token', 'column', 'begin_datetime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户名',
   'user', @CurrentUser, 'table', 't_sys_token', 'column', 'begin_datetime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_sys_token')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'expried_datetime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_sys_token', 'column', 'expried_datetime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '部门名称id',
   'user', @CurrentUser, 'table', 't_sys_token', 'column', 'expried_datetime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('t_sys_token')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatedDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 't_sys_token', 'column', 'CreatedDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建日期',
   'user', @CurrentUser, 'table', 't_sys_token', 'column', 'CreatedDate'
go

/*==============================================================*/
/* Index: Index_Guid                                            */
/*==============================================================*/
create unique index Index_Guid on t_sys_token (
tb_guid ASC
)
go
