--Create table cms_splashes
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'cms_splashes'))
BEGIN
CREATE TABLE [dbo].[cms_splashes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[ZoneID] [int] NOT NULL,
	[ArticleID] [int] NOT NULL,
	[Width] [nvarchar](250) NOT NULL,
	[Height] [nvarchar](250) NOT NULL,
	[OpenTime] [nvarchar](250) NOT NULL,
	[CloseTime] [nvarchar](250) NOT NULL,
	[IsModal] [bit] NOT NULL,
	[CloseButton] [bit] NOT NULL,
	[Cookie] [bit] NOT NULL,
	[CookieExpire] [nvarchar](250) NOT NULL,
	[StartDate] [nvarchar](250) NOT NULL,
	[EndDate] [nvarchar](250) NOT NULL,
	[Status] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_cms_splashes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[cms_splashes] ADD  CONSTRAINT [DF_cms_splashes_ZoneID]  DEFAULT ((0)) FOR [ZoneID]

ALTER TABLE [dbo].[cms_splashes] ADD  CONSTRAINT [DF_cms_splashes_Width]  DEFAULT ('720') FOR [Width]

ALTER TABLE [dbo].[cms_splashes] ADD  CONSTRAINT [DF_cms_splashes_Height]  DEFAULT ('500') FOR [Height]

ALTER TABLE [dbo].[cms_splashes] ADD  CONSTRAINT [DF_cms_splashes_OpenTime]  DEFAULT ('0') FOR [OpenTime]

ALTER TABLE [dbo].[cms_splashes] ADD  CONSTRAINT [DF_cms_splashes_CloseTime]  DEFAULT ('0') FOR [CloseTime]

ALTER TABLE [dbo].[cms_splashes] ADD  CONSTRAINT [DF_cms_splashes_IsModal]  DEFAULT ((0)) FOR [IsModal]

ALTER TABLE [dbo].[cms_splashes] ADD  CONSTRAINT [DF_cms_splashes_CloseButton]  DEFAULT ((1)) FOR [CloseButton]

ALTER TABLE [dbo].[cms_splashes] ADD  CONSTRAINT [DF_cms_splashes_Cookie]  DEFAULT ((0)) FOR [Cookie]

ALTER TABLE [dbo].[cms_splashes] ADD  CONSTRAINT [DF_cms_splashes_CookieExpire]  DEFAULT ('1') FOR [CookieExpire]

ALTER TABLE [dbo].[cms_splashes] ADD  CONSTRAINT [DF_cms_splashes_StartDate]  DEFAULT ('') FOR [StartDate]

ALTER TABLE [dbo].[cms_splashes] ADD  CONSTRAINT [DF_cms_splashes_EndDate]  DEFAULT ('') FOR [EndDate]

ALTER TABLE [dbo].[cms_splashes] ADD  CONSTRAINT [DF_cms_splashes_Status]  DEFAULT ((0)) FOR [Status]

ALTER TABLE [dbo].[cms_splashes] ADD  CONSTRAINT [DF_cms_splashes_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]

ALTER TABLE [dbo].[cms_splashes] ADD  CONSTRAINT [DF_cms_splashes_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]

END
GO

IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'cms_custom_values'))
BEGIN
CREATE TABLE [dbo].[cms_custom_values](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GroupID] [int] NULL,
	[GroupParentID] [int] NULL,
	[intCustom1] [int] NULL,
	[intCustom2] [int] NULL,
	[intCustom3] [int] NULL,
	[intCustom4] [int] NULL,
	[intCustom5] [int] NULL,
	[intCustom6] [int] NULL,
	[intCustom7] [int] NULL,
	[intCustom8] [int] NULL,
	[intCustom9] [int] NULL,
	[intCustom10] [int] NULL,
	[intCustom11] [int] NULL,
	[intCustom12] [int] NULL,
	[intCustom13] [int] NULL,
	[intCustom14] [int] NULL,
	[intCustom15] [int] NULL,
	[longCustom1] [bigint] NULL,
	[longCustom2] [bigint] NULL,
	[longCustom3] [bigint] NULL,
	[longCustom4] [bigint] NULL,
	[longCustom5] [bigint] NULL,
	[longCustom6] [bigint] NULL,
	[longCustom7] [bigint] NULL,
	[longCustom8] [bigint] NULL,
	[longCustom9] [bigint] NULL,
	[longCustom10] [bigint] NULL,
	[strCustom1] [nvarchar](max) NULL,
	[strCustom2] [nvarchar](max) NULL,
	[strCustom3] [nvarchar](max) NULL,
	[strCustom4] [nvarchar](max) NULL,
	[strCustom5] [nvarchar](max) NULL,
	[strCustom6] [nvarchar](max) NULL,
	[strCustom7] [nvarchar](max) NULL,
	[strCustom8] [nvarchar](max) NULL,
	[strCustom9] [nvarchar](max) NULL,
	[strCustom10] [nvarchar](max) NULL,
	[strCustom11] [nvarchar](max) NULL,
	[strCustom12] [nvarchar](max) NULL,
	[strCustom13] [nvarchar](max) NULL,
	[strCustom14] [nvarchar](max) NULL,
	[strCustom15] [nvarchar](max) NULL,
	[strCustom16] [nvarchar](max) NULL,
	[strCustom17] [nvarchar](max) NULL,
	[strCustom18] [nvarchar](max) NULL,
	[strCustom19] [nvarchar](max) NULL,
	[strCustom20] [nvarchar](max) NULL,
	[strCustom21] [nvarchar](max) NULL,
	[strCustom22] [nvarchar](max) NULL,
	[strCustom23] [nvarchar](max) NULL,
	[strCustom24] [nvarchar](max) NULL,
	[strCustom25] [nvarchar](max) NULL,
	[strCustom26] [nvarchar](max) NULL,
	[strCustom27] [nvarchar](max) NULL,
	[strCustom28] [nvarchar](max) NULL,
	[strCustom29] [nvarchar](max) NULL,
	[strCustom30] [nvarchar](max) NULL,
	[dtCustom1] [datetime] NULL,
	[dtCustom2] [datetime] NULL,
	[dtCustom3] [datetime] NULL,
	[dtCustom4] [datetime] NULL,
	[dtCustom5] [datetime] NULL,
	[dtCustom6] [datetime] NULL,
	[dtCustom7] [datetime] NULL,
	[dtCustom8] [datetime] NULL,
	[dtCustom9] [datetime] NULL,
	[dtCustom10] [datetime] NULL,
	[dtCustom11] [datetime] NULL,
	[dtCustom12] [datetime] NULL,
	[dtCustom13] [datetime] NULL,
	[dtCustom14] [datetime] NULL,
	[dtCustom15] [datetime] NULL,
	[bCustom1] [bit] NULL,
	[bCustom2] [bit] NULL,
	[bCustom3] [bit] NULL,
	[bCustom4] [bit] NULL,
	[bCustom5] [bit] NULL,
	[bCustom6] [bit] NULL,
	[bCustom7] [bit] NULL,
	[bCustom8] [bit] NULL,
	[bCustom9] [bit] NULL,
	[bCustom10] [bit] NULL,
	[dCustom1] [decimal](18, 0) NULL,
	[dCustom2] [decimal](18, 0) NULL,
	[dCustom3] [decimal](18, 0) NULL,
	[dCustom4] [decimal](18, 0) NULL,
	[dCustom5] [decimal](18, 0) NULL,
	[dCustom6] [decimal](18, 0) NULL,
	[dCustom7] [decimal](18, 0) NULL,
	[dCustom8] [decimal](18, 0) NULL,
	[dCustom9] [decimal](18, 0) NULL,
	[dCustom10] [decimal](18, 0) NULL,
	[status] [tinyint] NULL,
 CONSTRAINT [PK_cms_custom_values] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[cms_custom_values] ADD  CONSTRAINT [DF_cms_custom_values_status]  DEFAULT ((0)) FOR [status]
END
GO

--Create table cms_tags
IF(NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'cms_tags'))
BEGIN
CREATE TABLE [dbo].[cms_tags](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[Counter] [int] NOT NULL,
	[SiteID] [int] NOT NULL,
	[AddedDate] [datetime] NOT NULL,
	[PublisherID] [uniqueidentifier] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_cms_tags] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
  
ALTER TABLE [dbo].[cms_tags] ADD  CONSTRAINT [DF_cms_tags_AddedDate]  DEFAULT (getdate()) FOR [AddedDate] 

ALTER TABLE [dbo].[cms_tags] ADD  CONSTRAINT [DF_cms_tags_IsActive]  DEFAULT ((1)) FOR [IsActive] 
END
GO

--Add Column tag_ids cms_articles
IF not exists(Select * from sys.columns where Name = 'tag_ids' and object_id = OBJECT_ID('cms_articles'))
BEGIN
ALTER TABLE cms_articles 
ADD tag_ids [nvarchar](max) NOT NULL  DEFAULT ('')  
END 
GO

--Add Column tag_ids cms_article_revision
IF not exists(Select * from sys.columns where Name = 'tag_ids' and object_id = OBJECT_ID('cms_article_revision'))
BEGIN
ALTER TABLE cms_article_revision 
ADD tag_ids [nvarchar](max) NOT NULL  DEFAULT ('')  
END 
GO

--Add Column tag_contents cms_articles
IF not exists(Select * from sys.columns where Name = 'tag_contents' and object_id = OBJECT_ID('cms_articles'))
BEGIN
ALTER TABLE cms_articles 
ADD tag_contents [nvarchar](max) NOT NULL  DEFAULT ('')  
END 
GO

--Add Column tag_contents cms_article_revision
IF not exists(Select * from sys.columns where Name = 'tag_contents' and object_id = OBJECT_ID('cms_article_revision'))
BEGIN
ALTER TABLE cms_article_revision 
ADD tag_contents [nvarchar](max) NOT NULL  DEFAULT ('')  
END 
GO

--Add tag_ids to cms_asp_approval_approve_article_revision
Declare @prDefinition nvarchar(MAX); 
SET @prDefinition = (select  object_definition(object_id) proceduredefinition from sys.procedures where name = 'cms_asp_approval_approve_article_revision')
IF CHARINDEX('before_body = ar.before_body,tag_ids = ar.tag_ids,',@prDefinition) <= 0
BEGIN
SET @prDefinition = REPLACE(@prDefinition,'before_body = ar.before_body,','before_body = ar.before_body,tag_ids = ar.tag_ids,')
SET @prDefinition = REPLACE(@prDefinition,'CREATE procedure [dbo].[cms_asp_approval_approve_article_revision]','ALTER procedure [dbo].[cms_asp_approval_approve_article_revision]')
EXEC (@prDefinition)
END
GO

--Add tag_contents to cms_asp_approval_approve_article_revision
Declare @prDefinition nvarchar(MAX); 
SET @prDefinition = (select  object_definition(object_id) proceduredefinition from sys.procedures where name = 'cms_asp_approval_approve_article_revision')
IF CHARINDEX('tag_ids = ar.tag_ids,tag_contents = ar.tag_contents,',@prDefinition) <= 0
BEGIN
SET @prDefinition = REPLACE(@prDefinition,'tag_ids = ar.tag_ids,','tag_ids = ar.tag_ids,tag_contents = ar.tag_contents,')
SET @prDefinition = REPLACE(@prDefinition,'CREATE procedure [dbo].[cms_asp_approval_approve_article_revision]','ALTER procedure [dbo].[cms_asp_approval_approve_article_revision]')
EXEC (@prDefinition)
END
GO

--Add is_page to cms_article_zones
IF not exists(Select * from sys.columns where Name = 'is_page' and object_id = OBJECT_ID('cms_article_zones'))
BEGIN
ALTER TABLE cms_article_zones 
ADD is_page bit NOT NULL  DEFAULT ('1')  
END 
GO

--Add is_page to cms_article_zones_revision
IF not exists(Select * from sys.columns where Name = 'is_page' and object_id = OBJECT_ID('cms_article_zones_revision'))
BEGIN
ALTER TABLE cms_article_zones_revision 
ADD is_page bit NOT NULL  DEFAULT ('1')  
END 
GO

--Add is_page to cms_asp_approval_approve_article_revision
Declare @prDefinition nvarchar(MAX); 
SET @prDefinition = (select  object_definition(object_id) proceduredefinition from sys.procedures where name = 'cms_asp_approval_approve_article_revision')
IF CHARINDEX('insert into dbo.cms_article_zones
	(article_id, zone_id, az_order, az_alias,is_alias_protected,is_page)
	select article_id, zone_id, az_order, az_alias,is_alias_protected,is_page
	from dbo.cms_article_zones_revision
	where rev_id = @rev_id',@prDefinition) <= 0
BEGIN
SET @prDefinition = REPLACE(@prDefinition,'insert into dbo.cms_article_zones
	(article_id, zone_id, az_order, az_alias,is_alias_protected)
	select article_id, zone_id, az_order, az_alias,is_alias_protected
	from dbo.cms_article_zones_revision
	where rev_id = @rev_id','insert into dbo.cms_article_zones
	(article_id, zone_id, az_order, az_alias,is_alias_protected,is_page)
	select article_id, zone_id, az_order, az_alias,is_alias_protected,is_page
	from dbo.cms_article_zones_revision
	where rev_id = @rev_id')
SET @prDefinition = REPLACE(@prDefinition,'CREATE procedure [dbo].[cms_asp_approval_approve_article_revision]','ALTER procedure [dbo].[cms_asp_approval_approve_article_revision]')
EXEC (@prDefinition)
END
GO


--Add tags, is_page to [vArticlesZonesFull]
ALTER VIEW [dbo].[vArticlesZonesFull]
AS
SELECT     TOP (100) PERCENT az.az_order,
                          (SELECT     lang_alias
                            FROM          dbo.cms_languages AS ln
                            WHERE      (lang_id = a.lang_id)) AS lang_alias, z.zone_alias, z.zone_type_id, a.article_id, a.clsf_id, a.status, a.created, a.updated, a.startdate, a.enddate, 
                      a.publisher_id, a.clicks, a.orderno, a.lang_id, a.navigation_display, a.navigation_zone_id, a.menu_text, a.headline, a.summary, a.keywords, a.article_type, 
                      a.article_type_detail, a.article_1, a.article_2, a.article_3, a.article_4, a.article_5, a.custom_1, a.custom_2, a.custom_3, a.custom_4, a.custom_5, a.custom_6, 
                      a.custom_7, a.custom_8, a.custom_9, a.custom_10, a.custom_11, a.custom_12, a.custom_13, a.custom_14, a.custom_15, a.custom_16, a.custom_17, a.custom_18, 
                      a.custom_19, a.custom_20, a.flag_1, a.flag_2, a.flag_3, a.flag_4, a.flag_5, a.date_1, a.date_2, a.date_3, a.date_4, a.date_5, a.cl_1, a.cl_2, a.cl_3, a.cl_4, a.cl_5, 
                      a.custom_body AS a_custom_body, a.before_head AS a_before_head, a.before_body AS a_before_body, a.no_index_no_follow, a.custom_html_attr, a.meta_title, 
                      a.canonical_url, z.zone_id, z.zone_group_id, z.zone_status, z.zone_name, z.zone_desc, z.css_merge AS zone_css_merge, z.css_id AS zone_css_id, 
                      z.css_id_mobile AS zone_css_id_mobile, z.css_id_print AS zone_css_id_print, z.template_id AS zone_template_id, 
                      z.template_id_mobile AS zone_template_id_mobile, z.zone_keywords, z.article_1 AS zone_article_1, z.article_2 AS zone_article_2, z.article_3 AS zone_article_3, 
                      z.article_4 AS zone_article_4, z.article_5 AS zone_article_5, z.append_1, z.append_2, z.append_3, z.append_4, z.append_5, z.publisher_id AS zone_publisher_id, 
                      z.created AS zone_created, z.updated AS zone_updated, z.custom_body AS zone_custom_body, z.before_head AS zone_before_head, 
                      z.before_body AS zone_before_body, zg.zone_group_name, zg.zone_group_keywords, zg.site_id, zg.css_merge AS zg_css_merge, zg.css_id AS zg_css_id, 
                      zg.css_id_mobile AS zg_css_id_mobile, zg.css_id_print AS zg_css_id_print, zg.template_id AS zg_template_id, zg.template_id_mobile AS zg_template_id_mobile, 
                      zg.publisher_id AS zg_publisher_id, zg.created AS zg_created, zg.updated AS zg_updated, zg.article_1 AS zg_article_1, zg.article_2 AS zg_article_2, 
                      zg.article_3 AS zg_article_3, zg.article_4 AS zg_article_4, zg.article_5 AS zg_article_5, zg.append_1 AS zg_append_1, zg.append_2 AS zg_append_2, 
                      zg.append_3 AS zg_append_3, zg.append_4 AS zg_append_4, zg.append_5 AS zg_append_5, zg.custom_body AS zg_custom_body, 
                      zg.before_head AS zg_before_head, zg.before_body AS zg_before_body, s.site_name, s.css_id AS site_css_id, s.css_id_mobile AS site_css_id_mobile, 
                      s.css_id_print AS site_css_id_print, s.template_id AS site_template_id, s.template_id_mobile AS site_template_id_mobile, s.publisher_id AS site_publisher_id, 
                      s.site_keywords, s.site_header, s.site_js, s.site_icon, s.created AS site_created, s.updated AS site_updated, s.article_1 AS s_article_1, s.article_2 AS s_article_2, 
                      s.article_3 AS s_article_3, s.article_4 AS s_article_4, s.article_5 AS s_article_5, s.custom_body AS s_custom_body, s.analytics AS site_analytics, 
                      zg.analytics AS zg_analytics, z.analytics AS zone_analytics, a.rating, a.ratingcount, a.meta_description, z.meta_description AS zone_meta_description, 
                      zg.meta_description AS zone_group_meta_description, s.meta_description AS site_meta_description, zg.zone_group_name_display, z.zone_name_display, 
                      az.az_alias, a.omniture_code AS article_omniture_code, z.omniture_code AS zone_omniture_code, zg.omniture_code AS zone_group_omniture_code, 
                      s.omniture_code AS site_omniture_code, s.default_article AS site_default_article, zg.default_article AS zone_group_default_article, 
                      z.default_article AS zone_default_article, a.custom_setting, s.site_alias, zg.zg_alias, az.is_alias_protected, az.is_page, CASE WHEN charindex(char(10), d .domain_names) 
                      > 0 THEN substring(d .domain_names, 1, charindex(char(10), d .domain_names)) ELSE d .domain_names END AS domain_name,a.tag_ids,a.tag_contents
FROM         dbo.cms_zones AS z WITH (nolock) INNER JOIN
                      dbo.cms_articles AS a WITH (nolock) INNER JOIN
                      dbo.cms_article_zones AS az WITH (nolock) ON a.article_id = az.article_id ON z.zone_id = az.zone_id LEFT OUTER JOIN
                      dbo.cms_zone_groups AS zg WITH (nolock) ON zg.zone_group_id = z.zone_group_id LEFT OUTER JOIN
                      dbo.cms_sites AS s WITH (nolock) ON s.site_id = zg.site_id LEFT OUTER JOIN
                      dbo.cms_domains AS d WITH (nolock) ON s.domain_id = d.domain_id 

GO
 

--Add Alias to cms_tags
IF not exists(Select * from sys.columns where Name = 'Alias' and object_id = OBJECT_ID('cms_tags'))
BEGIN
ALTER TABLE cms_tags 
ADD Alias Nvarchar(MAX) DEFAULT ''
END 
GO

--Add Column RedirectType cms_page_redirection
IF not exists(Select * from sys.columns where Name = 'RedirectType' and object_id = OBJECT_ID('cms_page_redirection'))
BEGIN
ALTER TABLE cms_page_redirection 
ADD RedirectType [nvarchar](max) NOT NULL  DEFAULT ('301')
IF((select count(*) from cms_config where config_name = 'FORCE_HTTPS' and (config_value_local like '%Y%' or config_value_remote like '%Y%')) >= 1)
BEGIN
update cms_page_redirection set RedirectTo = 'https://' + RedirectTo where RedirectTo not like '%https://%'
END
END 
GO




--language relations Id alanı eklendi
IF not exists(Select * from sys.columns where Name = 'Id' and object_id = OBJECT_ID('cms_language_relations'))
BEGIN
ALTER TABLE [dbo].[cms_language_relations]
   ADD Id INT IDENTITY

ALTER TABLE dbo.cms_language_relations
   ADD CONSTRAINT PK_cms_language_relations
   PRIMARY KEY(Id)
END 
GO

IF not exists(Select * from sys.columns where Name = 'Id' and object_id = OBJECT_ID('cms_language_relations_revision'))
BEGIN
ALTER TABLE [dbo].[cms_language_relations_revision]
   ADD Id INT IDENTITY

ALTER TABLE dbo.cms_language_relations_revision
   ADD CONSTRAINT PK_cms_language_relations_revision
   PRIMARY KEY(Id)
END 
GO



--HTML Minify  CONFIG TABLE Update - 22.05.2017 - KD
IF NOT EXISTS (SELECT * FROM cms_config WHERE config_name = 'HTML_MINIFY')
	INSERT INTO cms_config (config_name, config_value_local, config_value_remote, isDefault, publisher_id, updated) VALUES ('HTML_MINIFY', 'Y', 'Y', 'Y', '3B303D4E-4639-42F1-B382-A8C71F640443', GETDATE())

	
	
	
--SiteFilePath	
IF not exists(Select * from sys.columns where Name = 'file_path' and object_id = OBJECT_ID('cms_sites'))
BEGIN
ALTER TABLE [dbo].[cms_sites]
   ADD file_path nvarchar(250) 

END 
GO	
	