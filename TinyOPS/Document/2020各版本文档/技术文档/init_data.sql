--USE [eleadmin]
--GO
SET IDENTITY_INSERT [dbo].[t_sys_token] ON 

GO
INSERT [dbo].[t_sys_token] ([id], [tb_guid], [app_name], [app_id], [app_secret], [access_token], [begin_datetime], [expried_datetime], [time_stamp], [CreatedDate], [UpdateDate]) VALUES (3, N'2C4C71B3-709B-4B24-978B-0085253739EC', N'opsApi', N'opsapp', N'BB12E86E-D5C5-4D12-9844-8E868E4C1BB9', NULL, NULL, NULL, 60, CAST(N'2020-05-01 00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[t_sys_token] ([id], [tb_guid], [app_name], [app_id], [app_secret], [access_token], [begin_datetime], [expried_datetime], [time_stamp], [CreatedDate], [UpdateDate]) VALUES (4, N'2C4C71B3-709B-4B24-978B-0085253739E1', N'opsWeb', N'opsappweb', N'F6D08AA4-F4C8-4B43-928D-7E9AF6D51CB0', NULL, NULL, NULL, 10, CAST(N'2020-05-01 00:00:00.000' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[t_sys_token] OFF
GO
SET IDENTITY_INSERT [dbo].[t_sys_user] ON 

GO
INSERT [dbo].[t_sys_user] ([Id], [tb_guid], [avatar_id], [email], [is_admin], [is_enabled], [password], [username], [dept_id], [phone], [post_id], [last_password_reset_time], [nick_name], [sex], [CreatedDate], [UpdateDate]) VALUES (5, N'9BD32C5A-8613-45A1-A3C4-5BDD25A5E495', NULL, NULL, 1, 1, N'admin', N'admin', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-01-01 00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[t_sys_user] ([Id], [tb_guid], [avatar_id], [email], [is_admin], [is_enabled], [password], [username], [dept_id], [phone], [post_id], [last_password_reset_time], [nick_name], [sex], [CreatedDate], [UpdateDate]) VALUES (6, N'9BD32C5A-8613-45A1-A3C4-5BDD25A5E491', NULL, NULL, 0, 1, N'user', N'user', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-01-01 00:00:00.000' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[t_sys_user] OFF
GO
