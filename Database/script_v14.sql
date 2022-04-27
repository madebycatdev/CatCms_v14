

--siteafterbody	
IF not exists(Select * from sys.columns where Name = 'afterbody' and object_id = OBJECT_ID('cms_sites'))
BEGIN
ALTER TABLE [dbo].[cms_sites]
   ADD afterbody nvarchar(max) 

END 
GO	

--sitesuffix	
IF not exists(Select * from sys.columns where Name = 'sitesuffix' and object_id = OBJECT_ID('cms_sites'))
BEGIN
ALTER TABLE [dbo].[cms_sites]
   ADD sitesuffix nvarchar(max) 

END 
GO	

--siteprefix
IF not exists(Select * from sys.columns where Name = 'siteprefix' and object_id = OBJECT_ID('cms_sites'))
BEGIN
ALTER TABLE [dbo].[cms_sites]
   ADD siteprefix nvarchar(max) 

END 
GO	

--zonegroup afterbody	
IF not exists(Select * from sys.columns where Name = 'afterbody' and object_id = OBJECT_ID('cms_zone_groups'))
BEGIN
ALTER TABLE [dbo].[cms_zone_groups]
   ADD afterbody nvarchar(max) 

END 
GO	


--zone afterbody	
IF not exists(Select * from sys.columns where Name = 'afterbody' and object_id = OBJECT_ID('cms_zones'))
BEGIN
ALTER TABLE [dbo].[cms_zones]
   ADD afterbody nvarchar(max) 

END 
GO	



--zonerevision afterbody	
IF not exists(Select * from sys.columns where Name = 'afterbody' and object_id = OBJECT_ID('cms_zone_revision'))
BEGIN
ALTER TABLE [dbo].[cms_zone_revision]
   ADD afterbody nvarchar(max) 

END 
GO	


--article afterbody	
IF not exists(Select * from sys.columns where Name = 'afterbody' and object_id = OBJECT_ID('cms_articles'))
BEGIN
ALTER TABLE [dbo].[cms_articles]
   ADD afterbody nvarchar(max) 

END 
GO	

--articlerevision afterbody	
IF not exists(Select * from sys.columns where Name = 'afterbody' and object_id = OBJECT_ID('cms_article_revision'))
BEGIN
ALTER TABLE [dbo].[cms_article_revision]
   ADD afterbody nvarchar(max) 

END 
GO	


--article hideprefix	
IF not exists(Select * from sys.columns where Name = 'hideprefix' and object_id = OBJECT_ID('cms_articles'))
BEGIN
ALTER TABLE [dbo].[cms_articles]
   ADD hideprefix bit DEFAULT 0 NOT NULL

END 
GO	

--articlerevision hideprefix	
IF not exists(Select * from sys.columns where Name = 'hideprefix' and object_id = OBJECT_ID('cms_article_revision'))
BEGIN
ALTER TABLE [dbo].[cms_article_revision]
   ADD hideprefix bit DEFAULT 0 NOT NULL

END 
GO	


--article hidesuffix	
IF not exists(Select * from sys.columns where Name = 'hidesuffix' and object_id = OBJECT_ID('cms_articles'))
BEGIN
ALTER TABLE [dbo].[cms_articles]
   ADD hidesuffix bit DEFAULT 0 NOT NULL

END 
GO	

--articlerevision hidesuffix	
IF not exists(Select * from sys.columns where Name = 'hidesuffix' and object_id = OBJECT_ID('cms_article_revision'))
BEGIN
ALTER TABLE [dbo].[cms_article_revision]
   ADD hidesuffix bit DEFAULT 0 NOT NULL

END 
GO	


-- alter view
/****** Object:  View [dbo].[vArticlesZonesFull]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Add before_head,before_body to cms_zonegroups
ALTER VIEW [dbo].[vArticlesZonesFull]
AS
SELECT        TOP (100) PERCENT az.az_order,
                             (SELECT        lang_alias
                               FROM            dbo.cms_languages AS ln
                               WHERE        (lang_id = a.lang_id)) AS lang_alias, z.zone_alias, z.zone_type_id, a.article_id, a.clsf_id, a.status, a.created, a.updated, a.startdate, a.enddate, a.publisher_id, a.clicks, a.orderno, a.lang_id, 
                         a.navigation_display, a.navigation_zone_id, a.menu_text, a.headline, a.summary, a.keywords, a.article_type, a.article_type_detail, a.article_1, a.article_2, a.article_3, a.article_4, a.article_5, a.custom_1, 
                         a.custom_2, a.custom_3, a.custom_4, a.custom_5, a.custom_6, a.custom_7, a.custom_8, a.custom_9, a.custom_10, a.custom_11, a.custom_12, a.custom_13, a.custom_14, a.custom_15, a.custom_16, 
                         a.custom_17, a.custom_18, a.custom_19, a.custom_20, a.flag_1, a.flag_2, a.flag_3, a.flag_4, a.flag_5, a.date_1, a.date_2, a.date_3, a.date_4, a.date_5, a.cl_1, a.cl_2, a.cl_3, a.cl_4, a.cl_5, 
                         a.custom_body AS a_custom_body, a.before_head AS a_before_head, a.before_body AS a_before_body, a.no_index_no_follow, a.custom_html_attr, a.meta_title, a.canonical_url, z.zone_id, z.zone_group_id, 
                         z.zone_status, z.zone_name, z.zone_desc, z.css_merge AS zone_css_merge, z.css_id AS zone_css_id, z.css_id_mobile AS zone_css_id_mobile, z.css_id_print AS zone_css_id_print, 
                         z.template_id AS zone_template_id, z.template_id_mobile AS zone_template_id_mobile, z.zone_keywords, z.article_1 AS zone_article_1, z.article_2 AS zone_article_2, z.article_3 AS zone_article_3, 
                         z.article_4 AS zone_article_4, z.article_5 AS zone_article_5, z.append_1, z.append_2, z.append_3, z.append_4, z.append_5, z.publisher_id AS zone_publisher_id, z.created AS zone_created, 
                         z.updated AS zone_updated, z.custom_body AS zone_custom_body, z.before_head AS zone_before_head, z.before_body AS zone_before_body, zg.zone_group_name, zg.zone_group_keywords, zg.site_id, 
                         zg.css_merge AS zg_css_merge, zg.css_id AS zg_css_id, zg.css_id_mobile AS zg_css_id_mobile, zg.css_id_print AS zg_css_id_print, zg.template_id AS zg_template_id, 
                         zg.template_id_mobile AS zg_template_id_mobile, zg.publisher_id AS zg_publisher_id, zg.created AS zg_created, zg.updated AS zg_updated, zg.article_1 AS zg_article_1, zg.article_2 AS zg_article_2, 
                         zg.article_3 AS zg_article_3, zg.article_4 AS zg_article_4, zg.article_5 AS zg_article_5, zg.append_1 AS zg_append_1, zg.append_2 AS zg_append_2, zg.append_3 AS zg_append_3, 
                         zg.append_4 AS zg_append_4, zg.append_5 AS zg_append_5, zg.custom_body AS zg_custom_body, zg.before_head AS zg_before_head, zg.before_body AS zg_before_body, s.site_name, 
                         s.css_id AS site_css_id, s.css_id_mobile AS site_css_id_mobile, s.css_id_print AS site_css_id_print, s.template_id AS site_template_id, s.template_id_mobile AS site_template_id_mobile, 
                         s.publisher_id AS site_publisher_id, s.site_keywords, s.site_header, s.site_js, s.site_icon, s.created AS site_created, s.updated AS site_updated, s.article_1 AS s_article_1, s.article_2 AS s_article_2, 
                         s.article_3 AS s_article_3, s.article_4 AS s_article_4, s.article_5 AS s_article_5, s.custom_body AS s_custom_body, s.analytics AS site_analytics, zg.analytics AS zg_analytics, z.analytics AS zone_analytics, 
                         a.rating, a.ratingcount, a.meta_description, z.meta_description AS zone_meta_description, zg.meta_description AS zone_group_meta_description, s.meta_description AS site_meta_description, 
                         zg.zone_group_name_display, z.zone_name_display, az.az_alias, a.omniture_code AS article_omniture_code, z.omniture_code AS zone_omniture_code, zg.omniture_code AS zone_group_omniture_code, 
                         s.omniture_code AS site_omniture_code, s.default_article AS site_default_article, zg.default_article AS zone_group_default_article, z.default_article AS zone_default_article, a.custom_setting, s.site_alias, 
                         zg.zg_alias, az.is_alias_protected, az.is_page, CASE WHEN charindex(char(10), d .domain_names) > 0 THEN substring(d .domain_names, 1, charindex(char(10), d .domain_names)) 
                         ELSE d .domain_names END AS domain_name, a.tag_ids, a.tag_contents, a.afterbody AS a_afterbody, a.hideprefix AS a_hideprefix, a.hidesuffix AS a_hidesuffix, z.afterbody AS z_afterbody, 
                         zg.afterbody AS zg_afterbody, s.afterbody AS site_afterbody, s.siteprefix AS site_prefix, s.sitesuffix AS site_suffix
FROM            dbo.cms_zones AS z WITH (nolock) INNER JOIN
                         dbo.cms_articles AS a WITH (nolock) INNER JOIN
                         dbo.cms_article_zones AS az WITH (nolock) ON a.article_id = az.article_id ON z.zone_id = az.zone_id LEFT OUTER JOIN
                         dbo.cms_zone_groups AS zg WITH (nolock) ON zg.zone_group_id = z.zone_group_id LEFT OUTER JOIN
                         dbo.cms_sites AS s WITH (nolock) ON s.site_id = zg.site_id LEFT OUTER JOIN
                         dbo.cms_domains AS d WITH (nolock) ON s.domain_id = d.domain_id

GO



--alter sp


GO
/****** Object:  StoredProcedure [dbo].[cms_asp_approval_approve_zone_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[cms_asp_approval_approve_zone_revision]
	@rev_id bigint,
	@approve_level int,
	@publisher_id uniqueidentifier,
	@publisher_level int,
	@cio char(1) --Zone Check-In Check-Out parameter
as

set nocount on
if not exists (select * from dbo.cms_zone_revision with (nolock) where rev_id = @rev_id and revision_status  in ('N','A','W') )
begin
	-- revision not available for approval
	select 'NOT_AVAILABLE' as aStat, '' as found_name, @rev_id as rev_id
	return
end


declare @zone_status as char(1)
declare @zone_id as int
declare @locked datetime
declare @locked_by nvarchar(100)

select @zone_status = zone_status, @zone_id = zone_id from dbo.cms_zone_revision with (nolock) where rev_id = @rev_id

--Check for zone lock
if not exists(select * from dbo.cms_zones with (nolock) where zone_id = @zone_id and (locked_by = @publisher_id Or locked_by is null)) AND @cio = '1'
begin
	select @locked = a.locked, @locked_by = p.UserName from dbo.cms_zones as a with (nolock) left outer join dbo.vw_aspnet_MembershipUsers as p  with (nolock) on a.locked_by = p.UserId where zone_id = @zone_id
	select 'LOCKED' as aStat, @locked as locked, @locked_by as locked_by, '' as found_name
	return
end

if (@zone_status = 'D')
begin

	-- this is delete approval.. so check related articles first..
	if exists(select * from dbo.cms_article_zones caz with (nolock) LEFT JOIN dbo.cms_articles ca with (nolock) ON ca.article_id = caz.article_id where caz.zone_id = @zone_id and ca.status <> 2)
	begin
		select 'ARTICLE_EXIST' as aStat, '(' + ca.article_id + ') ' + ca.headline as found_name, @rev_id as rev_id
		from dbo.cms_article_zones caz with (nolock)
			LEFT JOIN dbo.cms_articles ca with (nolock)
			ON ca.article_id = caz.article_id and ca.status <> 2
		where caz.zone_id = @zone_id
		order by ca.headline

		return
	end

	-- also check for menu structure relations
	if exists(select * from dbo.cms_articles with (nolock) where navigation_zone_id = @zone_id and navigation_display in (2,3) and status <> 2 )
	begin
		select 'MENU_EXIST' as aStat, '(' + ca.article_id + ') ' + ca.headline as found_name, @rev_id as rev_id
		from dbo.cms_articles ca with (nolock)
		where ca.navigation_zone_id = @zone_id and ca.navigation_display in (2,3) and ca.status <> 2
		order by ca.headline

		return
	end

	--check for domain homepage article
	if exists(select * from dbo.cms_domains with (nolock) where home_page_article like CAST(@zone_id AS VARCHAR(20)) + '-%')
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_DOMAIN_HOME_PAGE' as rStat, domain_names as tStat from dbo.cms_domains with (nolock) where home_page_article like CAST(@zone_id AS VARCHAR(20)) + '-%'
		return
	end

	--check for domain 404 article
	if exists(select * from dbo.cms_domains with (nolock) where error_page_article like CAST(@zone_id AS VARCHAR(20)) + '-%')
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_DOMAIN_404_PAGE' as rStat, domain_names as tStat from dbo.cms_domains with (nolock) where home_page_article like CAST(@zone_id AS VARCHAR(20)) + '-%'
		return
	end

	--check for alias redirections
	if exists(select * from dbo.cms_redirects with (nolock) where zone_id = @zone_id)
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_ALIAS_REDIRECTION' as rStat, redirect_alias as tStat from dbo.cms_redirects with (nolock) where zone_id = @zone_id
		return
	end

	--check for site default article
	if exists(select * from dbo.cms_sites with (nolock) where default_article like CAST(@zone_id AS VARCHAR(20)) + '-%')
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_SITE_DEFAULT_ARTICLE' as rStat, site_name as tStat from dbo.cms_sites with (nolock) where default_article like CAST(@zone_id AS VARCHAR(20)) + '-%'
		return
	end

	--check for site tag detail article
	if exists(select * from dbo.cms_sites with (nolock) where tag_detail_article like '%-' + CAST(@zone_id AS VARCHAR(20)))
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_SITE_TAG_DETAIL_ARTICLE' as rStat, site_name as tStat from dbo.cms_sites with (nolock) where tag_detail_article like '%-' + CAST(@zone_id AS VARCHAR(20))
		return
	end

	--check for zone group default article
	if exists(select * from dbo.cms_zone_groups with (nolock) where default_article like CAST(@zone_id AS VARCHAR(20)) + '-%')
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_ZONE_GROUP_DEFAULT_ARTICLE' as rStat, zone_group_name as tStat from dbo.cms_zone_groups with (nolock) where default_article like CAST(@zone_id AS VARCHAR(20)) + '-%'
		return
	end

	--check for zone group tag detail article
	if exists(select * from dbo.cms_zone_groups with (nolock) where tag_detail_article like CAST(@zone_id AS VARCHAR(20)) + '-%')
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_ZONE_GROUP_TAG_DETAIL_ARTICLE' as rStat, zone_group_name as tStat from dbo.cms_zone_groups with (nolock) where tag_detail_article like CAST(@zone_id AS VARCHAR(20)) + '-%'
		return
	end

	--check for zone default article
	if exists(select * from dbo.cms_zones with (nolock) where default_article like CAST(@zone_id AS VARCHAR(20)) + '-%')
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_ZONE_DEFAULT_ARTICLE' as rStat, zone_name as tStat from dbo.cms_zones with (nolock) where default_article like CAST(@zone_id AS VARCHAR(20)) + '-%'
		return
	end

	--check for article language relation reverse
	--if exists(select * from dbo.cms_article_language_relation with (nolock) where related_zone_id = @zone_id)
	--begin
	--	select 'CANT_DELETE' as aStat, 'USED_IN_LANGUAGE_RELATION' as rStat, CAST(article_id AS VARCHAR(20)) + '-' + (select headline from dbo.cms_articles with (nolock) where article_id = alr.article_id) as tStat from dbo.cms_article_language_relation alr with (nolock) where related_zone_id = @zone_id
	--	return
	--end

	--check for internal article redirection
	if exists(select * from dbo.cms_articles with (nolock) where article_type_detail like CAST(@zone_id AS VARCHAR(20)) + '-%' AND article_type = 2 AND status=1)
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_INTERNAL_ARTICLE_REDIRECTION' as rStat, CAST(article_id AS VARCHAR(20)) + '-' + headline as tStat from dbo.cms_articles with (nolock) where article_type_detail like CAST(@zone_id AS VARCHAR(20)) + '-%' AND article_type = 2 AND status=1
		return
	end

	--check for mapped article
	if exists(select * from dbo.cms_articles with (nolock) where article_type_detail like CAST(@zone_id AS VARCHAR(20)) + '-%' AND article_type = 9 AND status=1)
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_MAPPED_ARTICLE' as rStat, CAST(article_id AS VARCHAR(20)) + '-' + headline as tStat from dbo.cms_articles with (nolock) where article_type_detail like CAST(@zone_id AS VARCHAR(20)) + '-%' AND article_type = 9 AND status=1
		return
	end
end



-- update only status if not an admin
if @publisher_level < 100 and @approve_level = 3
begin
	update dbo.cms_zone_revision
	set
		revision_status = 'A',
		approval_date = getDate(),
		approval_id = @publisher_id
	where
		rev_id = @rev_id

	select 'OKA' as aStat, '' as found_name, @rev_id as rev_id
	return
end






begin transaction

-- can be approved..
-- update original table
update dbo.cms_zones
set
	zone_group_id = zr.zone_group_id,
	zone_type_id = zr.zone_type_id, 
	zone_status = zr.zone_status, 
	zone_name = zr.zone_name, 
	zone_desc = zr.zone_desc, 
	css_merge = zr.css_merge, 
	css_id = zr.css_id, 
	css_id_mobile = zr.css_id_mobile, 
	css_id_print = zr.css_id_print, 
	template_id = zr.template_id, 
	template_id_mobile = zr.template_id_mobile, 
	custom_body = zr.custom_body,
	zone_keywords = zr.zone_keywords,
	article_1 = zr.article_1, 
	article_2 = zr.article_2, 
	article_3 = zr.article_3, 
	article_4 = zr.article_4, 
	article_5 = zr.article_5, 
	append_1 = zr.append_1, 
	append_2 = zr.append_2, 
	append_3 = zr.append_3, 
	append_4 = zr.append_4, 
	append_5 = zr.append_5, before_head = zr.before_head, before_body = zr.before_body,
	analytics = zr.analytics,zone_alias = zr.zone_alias, 
	updated = zr.rev_date,
	meta_description = zr.meta_description,
	zone_name_display = zr.zone_name_display,
	default_article = zr.default_article,
	omniture_code = zr.omniture_code,
	lang_id = zr.lang_id,
	afterbody = zr.afterbody
from dbo.cms_zone_revision zr
where zr.rev_id = @rev_id and dbo.cms_zones.zone_id = zr.zone_id

if(@@error <> 0) goto RollbackAndReturn

-- mark other old approved revisions as ex-approved
update dbo.cms_zone_revision
set
	revision_status = 'E'
where
	zone_id = @zone_id and revision_status = 'L'
if(@@error <> 0) goto RollbackAndReturn

-- mark revision as approved
update dbo.cms_zone_revision
set
	revision_status = 'L',
	approval_date = getDate(),
	approval_id = @publisher_id
where
	rev_id = @rev_id
if(@@error <> 0) goto RollbackAndReturn



-- mark revision requests as processed & deleted
update dbo.cms_instant_messaging
set
	processed = getDate(),
	deleted = getDate()
where
	related_id = @rev_id and ims_type = 'ZA'
if(@@error <> 0) goto RollbackAndReturn


-- update zone language relations for this revision
--delete from dbo.cms_zone_language_relation
--where zone_id = @zone_id

--if(@@error <> 0) goto RollbackAndReturn

--insert into dbo.cms_zone_language_relation
--(zone_id, related_zone_id)
--select zone_id, related_zone_id
--from dbo.cms_zone_language_relation_revision
--where rev_id = @rev_id

--if(@@error <> 0) goto RollbackAndReturn

set nocount off

commit transaction
if (@zone_status = 'D')
begin
	select 'DELETED' as aStat, '' as found_name, @rev_id as rev_id
end
else
begin
	select 'OK' as aStat, '' as found_name, @rev_id as rev_id
end
return

RollbackAndReturn:
rollback transaction
select @@ERROR as aStat, '' as found_name, @rev_id as rev_id
return




--alter procedure



GO
/****** Object:  StoredProcedure [dbo].[cms_asp_approval_approve_article_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[cms_asp_approval_approve_article_revision]
	@rev_id bigint,
	@approve_level int,
	@publisher_id uniqueidentifier,
	@publisher_level int,
	@cio char(1) --Article Check-In Check-Out parameter
as

declare @article_id int
--declare @target_status int
declare @locked datetime
declare @locked_by nvarchar(100)
declare @revision_status char(1)
declare @article_status INT
declare @navigation_zone_id INT
DECLARE @article_revision_article_type_detail NVARCHAR(500)
DECLARE @article_revision_article_type TINYINT


set nocount on

if not exists (select * from dbo.cms_article_revision with (nolock) where rev_id = @rev_id and revision_status in ('N','A','W') )
begin
	-- revision not available for approval
	select 'NOT_AVAILABLE' as aStat
	return
end

-- read article id from revision
select @article_id = article_id, @revision_status = revision_status, @article_status = status, @article_revision_article_type_detail = article_type_detail, @article_revision_article_type = article_type, @navigation_zone_id = navigation_zone_id from dbo.cms_article_revision with (nolock) where rev_id = @rev_id

-- check zone existance for this revision
if not exists(select * from dbo.cms_article_zones_revision cazr with (nolock) left join dbo.cms_zones cz with (nolock) on cz.zone_id = cazr.zone_id where cazr.rev_id = @rev_id and cz.zone_status <> 'D')
begin
	-- revision not available for approval because zone not found
	select 'ZONE_NOT_FOUND' as aStat
	return
end


--Check for article lock
if not exists(select * from dbo.cms_articles with (nolock) where article_id = @article_id and (locked_by = @publisher_id Or locked_by is null)) AND @cio = '1'
begin
	select @locked = a.locked, @locked_by = p.UserName from dbo.cms_articles as a with (nolock) left outer join dbo.vw_aspnet_MembershipUsers as p  with (nolock) on a.locked_by = p.UserId where article_id = @article_id
	select 'LOCKED' as aStat, @locked as locked, @locked_by as locked_by
	return
end

--check for delete approval
if @article_status = 2 
begin
	--check for domain homepage article
	if exists(select * from dbo.cms_domains with (nolock) where home_page_article like '%-' + CAST(@article_id AS VARCHAR(20)))
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_DOMAIN_HOME_PAGE' as rStat, domain_names as tStat from dbo.cms_domains with (nolock) where home_page_article like '%-' + CAST(@article_id AS VARCHAR(20))
		return
	end

	--check for domain 404 article
	if exists(select * from dbo.cms_domains with (nolock) where error_page_article like '%-' + CAST(@article_id AS VARCHAR(20)))
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_DOMAIN_404_PAGE' as rStat, domain_names as tStat from dbo.cms_domains with (nolock) where home_page_article like '%-' + CAST(@article_id AS VARCHAR(20))
		return
	end

	--check for alias redirections
	if exists(select * from dbo.cms_redirects with (nolock) where article_id = @article_id)
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_ALIAS_REDIRECTION' as rStat, redirect_alias as tStat from dbo.cms_redirects with (nolock) where article_id = @article_id
		return
	end

	--check for site default article
	if exists(select * from dbo.cms_sites with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20)))
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_SITE_DEFAULT_ARTICLE' as rStat, site_name as tStat from dbo.cms_sites with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20))
		return
	end

	--check for site tag detail article
	if exists(select * from dbo.cms_sites with (nolock) where tag_detail_article like '%-' + CAST(@article_id AS VARCHAR(20)))
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_SITE_TAG_DETAIL_ARTICLE' as rStat, site_name as tStat from dbo.cms_sites with (nolock) where tag_detail_article like '%-' + CAST(@article_id AS VARCHAR(20))
		return
	end

	--check for zone group default article
	if exists(select * from dbo.cms_zone_groups with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20)))
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_ZONE_GROUP_DEFAULT_ARTICLE' as rStat, zone_group_name as tStat from dbo.cms_zone_groups with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20))
		return
	end

	--check for zone group tag detail article
	if exists(select * from dbo.cms_zone_groups with (nolock) where tag_detail_article like '%-' + CAST(@article_id AS VARCHAR(20)))
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_ZONE_GROUP_TAG_DETAIL_ARTICLE' as rStat, zone_group_name as tStat from dbo.cms_zone_groups with (nolock) where tag_detail_article like '%-' + CAST(@article_id AS VARCHAR(20))
		return
	end

	--check for zone default article
	if exists(select * from dbo.cms_zones with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20)))
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_ZONE_DEFAULT_ARTICLE' as rStat, zone_name as tStat from dbo.cms_zones with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20))
		return
	end

	--check for article language relation
	
	if exists(select * from dbo.cms_language_relations with (nolock) where article_id = @article_id)
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_LANGUAGE_RELATION' as rStat, CAST(article_id AS VARCHAR(20)) + '-' + (select headline from dbo.cms_articles with (nolock) where article_id = alr.article_id) as tStat from dbo.cms_language_relations alr with (nolock) where article_id = @article_id
		return
	end

	--check for article language relation reverse
	--if exists(select * from dbo.cms_language_relations with (nolock) where related_article_id = @article_id)
	--begin
	--	select 'CANT_DELETE' as aStat, 'USED_IN_LANGUAGE_RELATION' as rStat, CAST(article_id AS VARCHAR(20)) + '-' + (select headline from dbo.cms_articles with (nolock) where article_id = alr.article_id) as tStat from dbo.cms_language_relations alr with (nolock) where related_article_id = @article_id
	--	return
	--end

	--check for internal article redirection
	if exists(select * from dbo.cms_articles with (nolock) where article_type_detail like '%-' + CAST(@article_id AS VARCHAR(20)) AND article_type = 2 AND status=1)
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_INTERNAL_ARTICLE_REDIRECTION' as rStat, CAST(article_id AS VARCHAR(20)) + '-' + headline as tStat from dbo.cms_articles with (nolock) where article_type_detail like '%-' + CAST(@article_id AS VARCHAR(20)) AND article_type = 2 AND status=1
		return
	end

	--check for mapped article
	if exists(select * from dbo.cms_articles with (nolock) where article_type_detail like '%-' + CAST(@article_id AS VARCHAR(20)) AND article_type = 9 AND status=1)
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_MAPPED_ARTICLE' as rStat, CAST(article_id AS VARCHAR(20)) + '-' + headline as tStat from dbo.cms_articles with (nolock) where article_type_detail like '%-' + CAST(@article_id AS VARCHAR(20)) AND article_type = 9 AND status=1
		return
	end

end

--check relations for approval
if @article_status = 1
begin
	--check for language relation article is exist
	
	IF EXISTS(select article_id from dbo.cms_language_relations_revision with (nolock) where rev_id = @rev_id AND article_id NOT IN (SELECT article_id FROM dbo.cms_articles WITH (NOLOCK) WHERE status <> 2))
	begin
		select 'CANT_APPROVE' as aStat, 'LANGUAGE_RELATIONED_ARTICLE_NOT_FOUND' as rStat, a.headline AS tStat from dbo.cms_language_relations_revision alrr with (nolock) left join dbo.cms_articles a (nolock) on  a.article_id = alrr.article_id where rev_id = @rev_id AND alrr.article_id NOT IN (SELECT article_id FROM dbo.cms_articles WITH (NOLOCK) WHERE status <> 2)
		return
	end
	--check for related articles are exist
	
	IF EXISTS(select article_id from dbo.cms_article_relation_revision with (nolock) where rev_id = @rev_id AND related_article_id NOT IN (SELECT article_id FROM dbo.cms_articles WITH (NOLOCK) WHERE status <> 2))
	begin
		select 'CANT_APPROVE' as aStat, 'RELATIONED_ARTICLE_NOT_FOUND' as rStat, a.headline AS tStat from dbo.cms_article_relation_revision arr with (nolock) left join dbo.cms_articles a (nolock) on  a.article_id = arr.related_article_id where rev_id = @rev_id AND related_article_id NOT IN (SELECT article_id FROM dbo.cms_articles WITH (NOLOCK) WHERE status <> 2)
		return
	END

	--check for internal redirection article is exist
	IF @article_revision_article_type = 2 AND @article_revision_article_type_detail <> '' AND NOT exists(select * from dbo.cms_article_zones with (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) = @article_revision_article_type_detail)
	begin
		select 'CANT_APPROVE' as aStat, 'INTERNAL_REDIRECTED_ARTICALE_NOT_FOUND' as rStat, '' AS tStat
		return
	end

	--check for mapped article is exist
	IF @article_revision_article_type = 9 AND @article_revision_article_type_detail <> '' AND NOT exists(select * from dbo.cms_article_zones with (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) = @article_revision_article_type_detail)
	begin
		select 'CANT_APPROVE' as aStat, 'MAPPED_ARTICALE_NOT_FOUND' as rStat, '' AS tStat
		return
	end

	--check for navigation zone id count
	If @navigation_zone_id <> 0 AND exists(SELECT article_id FROM dbo.cms_articles WHERE navigation_zone_id = @navigation_zone_id AND status <> 2 AND article_id <> @article_id)
	begin
		select 'CANT_APPROVE' as aStat, 'NAVIGATION_ZONE_ID_USED' as rStat, (SELECT CONVERT(nvarchar(max), article_id) FROM dbo.cms_articles WHERE navigation_zone_id = @navigation_zone_id AND status <> 2 AND article_id <> @article_id) AS tStat
		return
	end
	--check for domain homepage article
	if exists(select * from dbo.cms_domains with (nolock) where home_page_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND not exists(select * from dbo.cms_article_zones_revision (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) in (select home_page_article from dbo.cms_domains with (nolock) where home_page_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND rev_id = @rev_id)
	begin
		select 'CANT_APPROVE' as aStat, 'USED_IN_DOMAIN_HOME_PAGE' as rStat, mt.domain_names as tStat, mt.home_page_article, s.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name as z_name
			from dbo.cms_domains mt (nolock) 
				left join dbo.cms_zones z (nolock) on mt.home_page_article like cast(z.zone_id as varchar(10)) + '-%'
				left join dbo.cms_zone_groups zg (nolock) on z.zone_group_id = zg.zone_group_id
				left join dbo.cms_sites s (nolock) on s.site_id = zg.site_id
			where
				mt.home_page_article like '%-' + CAST(@article_id AS VARCHAR(20))
				AND mt.home_page_article NOT IN (Select (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10))) from dbo.cms_article_zones_revision (nolock) where rev_id = @rev_id)
		return
	end
	
	--check for domain 404 article
	if exists(select * from dbo.cms_domains with (nolock) where error_page_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND not exists(select * from dbo.cms_article_zones_revision (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) in (select error_page_article from dbo.cms_domains with (nolock) where error_page_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND rev_id = @rev_id)
	begin
		select 'CANT_APPROVE' as aStat, 'USED_IN_DOMAIN_404_PAGE' as rStat, mt.domain_names as tStat, mt.error_page_article, s.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name as z_name
			from dbo.cms_domains mt (nolock) 
				left join dbo.cms_zones z (nolock) on mt.error_page_article like cast(z.zone_id as varchar(10)) + '-%'
				left join dbo.cms_zone_groups zg (nolock) on z.zone_group_id = zg.zone_group_id
				left join dbo.cms_sites s (nolock) on s.site_id = zg.site_id
			where
				mt.error_page_article like '%-' + CAST(@article_id AS VARCHAR(20))
				AND mt.error_page_article NOT IN (Select (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10))) from dbo.cms_article_zones_revision (nolock) where rev_id = @rev_id)
		return
	end

	--check for alias redirections
	if exists(select * from dbo.cms_redirects with (nolock) where article_id = @article_id) AND not exists(select * from dbo.cms_article_zones_revision (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) in (select CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) from dbo.cms_redirects with (nolock) where article_id = @article_id) AND rev_id = @rev_id)
	begin
		select 'CANT_APPROVE' as aStat, 'USED_IN_ALIAS_REDIRECTION' as rStat, mt.redirect_alias as tStat, s.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name as z_name
			from dbo.cms_redirects mt (nolock) 
				left join dbo.cms_zones z (nolock) on mt.zone_id = z.zone_id
				left join dbo.cms_zone_groups zg (nolock) on z.zone_group_id = zg.zone_group_id
				left join dbo.cms_sites s (nolock) on s.site_id = zg.site_id
			where
				mt.article_id = @article_id
				AND CAST(mt.zone_id AS VARCHAR(20)) + '-' + CAST(mt.article_id AS VARCHAR(20)) NOT IN (Select (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10))) from dbo.cms_article_zones_revision (nolock) where rev_id = @rev_id)
		return
	end

	--check for site default article
	if exists(select * from dbo.cms_sites with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND not exists(select * from dbo.cms_article_zones_revision (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) in (select default_article from dbo.cms_sites with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND rev_id = @rev_id)
	begin
		select 'CANT_APPROVE' as aStat, 'USED_IN_SITE_DEFAULT_ARTICLE' as rStat, mt.site_name as tStat, mt.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name as z_name
			from dbo.cms_sites mt (nolock) 
				left join dbo.cms_zones z (nolock) on mt.default_article like cast(z.zone_id as varchar(10)) + '-%'
				left join dbo.cms_zone_groups zg (nolock) on z.zone_group_id = zg.zone_group_id
			where
				mt.default_article like '%-' + CAST(@article_id AS VARCHAR(20))
				AND mt.default_article NOT IN (Select (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10))) from dbo.cms_article_zones_revision (nolock) where rev_id = @rev_id)
		return
	end

	--check for site tag detail article
	if exists(select * from dbo.cms_sites with (nolock) where tag_detail_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND not exists(select * from dbo.cms_article_zones_revision (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) in (select tag_detail_article from dbo.cms_sites with (nolock) where tag_detail_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND rev_id = @rev_id)
	begin
		select 'CANT_APPROVE' as aStat, 'USED_IN_SITE_TAG_DETAIL_ARTICLE' as rStat, mt.site_name as tStat, mt.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name as z_name
			from dbo.cms_sites mt (nolock) 
				left join dbo.cms_zones z (nolock) on mt.tag_detail_article like cast(z.zone_id as varchar(10)) + '-%'
				left join dbo.cms_zone_groups zg (nolock) on z.zone_group_id = zg.zone_group_id
			where
				mt.tag_detail_article like '%-' + CAST(@article_id AS VARCHAR(20))
				AND mt.tag_detail_article NOT IN (Select (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10))) from dbo.cms_article_zones_revision (nolock) where rev_id = @rev_id)
		return
	end

	--check for zone group default article
	if exists(select * from dbo.cms_zone_groups with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND not exists(select * from dbo.cms_article_zones_revision (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) in (select default_article from dbo.cms_zone_groups with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND rev_id = @rev_id)
	begin
		select 'CANT_APPROVE' as aStat, 'USED_IN_ZONE_GROUP_DEFAULT_ARTICLE' as rStat, mt.zone_group_name as tStat, s.site_name + ' / ' + mt.zone_group_name + ' / ' + z.zone_name as z_name
			from dbo.cms_zone_groups mt (nolock) 
				left join dbo.cms_zones z (nolock) on mt.default_article like cast(z.zone_id as varchar(10)) + '-%'
				left join dbo.cms_sites s (nolock) on s.site_id = mt.site_id
			where
				mt.default_article like '%-' + CAST(@article_id AS VARCHAR(20))
				AND mt.default_article NOT IN (Select (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10))) from dbo.cms_article_zones_revision (nolock) where rev_id = @rev_id)
		return
	end

	--check for zone group tag detail article
	if exists(select * from dbo.cms_zone_groups with (nolock) where tag_detail_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND not exists(select * from dbo.cms_article_zones_revision (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) in (select tag_detail_article from dbo.cms_sites with (nolock) where tag_detail_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND rev_id = @rev_id)
	begin
		select 'CANT_APPROVE' as aStat, 'USED_IN_ZONE_GROUP_TAG_DETAIL_ARTICLE' as rStat, mt.zone_group_name as tStat, s.site_name + ' / ' + mt.zone_group_name + ' / ' + z.zone_name as z_name
			from dbo.cms_zone_groups mt (nolock) 
				left join dbo.cms_zones z (nolock) on mt.tag_detail_article like cast(z.zone_id as varchar(10)) + '-%'
				left join dbo.cms_sites s (nolock) on s.site_id = mt.site_id
			where
				mt.tag_detail_article like '%-' + CAST(@article_id AS VARCHAR(20))
				AND mt.tag_detail_article NOT IN (Select (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10))) from dbo.cms_article_zones_revision (nolock) where rev_id = @rev_id)
		return
	end

	--check for zone default article
	if exists(select * from dbo.cms_zones with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND not exists(select * from dbo.cms_article_zones_revision (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) in (select default_article from dbo.cms_zones with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND rev_id = @rev_id)
	begin
		select 'CANT_APPROVE' as aStat, 'USED_IN_ZONE_DEFAULT_ARTICLE' as rStat, mt.zone_name as tStat, s.site_name + ' / ' + zg.zone_group_name + ' / ' + mt.zone_name as z_name
			from dbo.cms_zones mt (nolock) 
				left join dbo.cms_zone_groups zg (nolock) on mt.zone_group_id = zg.zone_group_id
				left join dbo.cms_sites s (nolock) on s.site_id = zg.site_id
			where
				mt.default_article like '%-' + CAST(@article_id AS VARCHAR(20))
				AND mt.default_article NOT IN (Select (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10))) from dbo.cms_article_zones_revision (nolock) where rev_id = @rev_id)
		return
	end

	----check for article language relation reverse
	--if exists(select * from dbo.cms_article_language_relation with (nolock) where related_article_id = @article_id) AND not exists(select * from dbo.cms_article_zones_revision (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) in (select CAST(related_zone_id AS VARCHAR(20)) + '-' + CAST(related_article_id AS VARCHAR(20)) from dbo.cms_article_language_relation with (nolock) where related_article_id = @article_id) AND rev_id = @rev_id)
	--begin
	--	select 'CANT_APPROVE' as aStat, 'USED_IN_LANGUAGE_RELATION' as rStat, CAST(mt.article_id AS VARCHAR(20)) + '-' + (select headline from dbo.cms_articles with (nolock) where article_id = mt.article_id) as tStat, s.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name as z_name
	--		from dbo.cms_article_language_relation mt (nolock) 
	--			left join dbo.cms_zones z (nolock) on mt.related_zone_id = z.zone_id
	--			left join dbo.cms_zone_groups zg (nolock) on z.zone_group_id = zg.zone_group_id
	--			left join dbo.cms_sites s (nolock) on s.site_id = zg.site_id
	--		where
	--			mt.related_article_id = @article_id
	--			AND CAST(mt.related_zone_id AS VARCHAR(20)) + '-' + CAST(mt.related_article_id AS VARCHAR(20)) NOT IN (Select (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10))) from dbo.cms_article_zones_revision (nolock) where rev_id = @rev_id)
	--	return
	--end

	--check for internal article redirection
	if exists(select * from dbo.cms_articles with (nolock) where article_type_detail like '%-' + CAST(@article_id AS VARCHAR(20)) AND article_type = 2 AND status=1) AND not exists(select * from dbo.cms_article_zones_revision (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) in (select article_type_detail from dbo.cms_articles with (nolock) where article_type_detail like '%-' + CAST(@article_id AS VARCHAR(20)) AND article_type = 2 AND status=1) AND rev_id = @rev_id)
	begin
		select 'CANT_APPROVE' as aStat, 'USED_IN_INTERNAL_ARTICLE_REDIRECTION' as rStat, CAST(mt.article_id AS VARCHAR(20)) + '-' + mt.headline as tStat, s.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name as z_name
			from dbo.cms_articles mt (nolock)
				left join dbo.cms_zones z (nolock) on mt.article_type_detail like cast(z.zone_id as varchar(10)) + '-%'
				left join dbo.cms_zone_groups zg (nolock) on z.zone_group_id = zg.zone_group_id
				left join dbo.cms_sites s (nolock) on s.site_id = zg.site_id
			where
				mt.article_type_detail like '%-' + CAST(@article_id AS VARCHAR(20))
				AND mt.article_type_detail NOT IN (Select (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10))) from dbo.cms_article_zones_revision (nolock) where rev_id = @rev_id)
				AND mt.article_type = 2
				AND mt.status=1
		return
	end

	--check for mapped article
	if exists(select * from dbo.cms_articles with (nolock) where article_type_detail like '%-' + CAST(@article_id AS VARCHAR(20)) AND article_type = 9 AND status=1) AND not exists(select * from dbo.cms_article_zones_revision (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) in (select article_type_detail from dbo.cms_articles with (nolock) where article_type_detail like '%-' + CAST(@article_id AS VARCHAR(20)) AND article_type = 9 AND status=1) AND rev_id = @rev_id)
	begin
		select 'CANT_APPROVE' as aStat, 'USED_IN_MAPPED_ARTICLE' as rStat, CAST(mt.article_id AS VARCHAR(20)) + '-' + mt.headline as tStat, s.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name as z_name
			from dbo.cms_articles mt (nolock)
				left join dbo.cms_zones z (nolock) on mt.article_type_detail like cast(z.zone_id as varchar(10)) + '-%'
				left join dbo.cms_zone_groups zg (nolock) on z.zone_group_id = zg.zone_group_id
				left join dbo.cms_sites s (nolock) on s.site_id = zg.site_id
			where
				mt.article_type_detail like '%-' + CAST(@article_id AS VARCHAR(20))
				AND mt.article_type_detail NOT IN (Select (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10))) from dbo.cms_article_zones_revision (nolock) where rev_id = @rev_id)
				AND mt.article_type = 9
				AND mt.status=1
		return
	end
end

-- update only status if not an admin
if @publisher_level < 100 and @approve_level = 3
begin
	update dbo.cms_article_revision
	set
		revision_status = 'A',
		approval_date = getDate(),
		approval_id = @publisher_id
	where
		rev_id = @rev_id

	select 'OKA' as aStat
	return
end

-- read target status for articles zone update
--select @target_status = status from dbo.cms_article_revision with (nolock) where rev_id = @rev_id




begin transaction

-- can be approved..
-- update original table
update dbo.cms_articles
set
	clsf_id = ar.clsf_id,
	status = ar.status, 
	updated = ar.rev_date,
	startdate = ar.startdate, 
	enddate = ar.enddate, 
	orderno = ar.orderno, 
	lang_id = ar.lang_id,
	menu_text = ar.menu_text,
	navigation_display = ar.navigation_display,
	navigation_zone_id = ar.navigation_zone_id,
	headline = ar.headline, 
	summary = ar.summary, 
	keywords = ar.keywords, 
	article_type = ar.article_type, 
	article_type_detail = ar.article_type_detail, 
	article_1 = ar.article_1, 
	article_2 = ar.article_2, 
	article_3 = ar.article_3, 
	article_4 = ar.article_4, 
	article_5 = ar.article_5, 
	custom_1 = ar.custom_1, 
	custom_2 = ar.custom_2, 
	custom_3 = ar.custom_3, 
	custom_4 = ar.custom_4, 
	custom_5 = ar.custom_5, 
	custom_6 = ar.custom_6, 
	custom_7 = ar.custom_7, 
	custom_8 = ar.custom_8, 
	custom_9 = ar.custom_9, 
	custom_10 = ar.custom_10, 
	custom_11 = ar.custom_11, 
	custom_12 = ar.custom_12, 
	custom_13 = ar.custom_13, 
	custom_14 = ar.custom_14, 
	custom_15 = ar.custom_15, 
	custom_16 = ar.custom_16, 
	custom_17 = ar.custom_17, 
	custom_18 = ar.custom_18, 
	custom_19 = ar.custom_19, 
	custom_20 = ar.custom_20, 
	flag_1 = ar.flag_1, 
	flag_2 = ar.flag_2, 
	flag_3 = ar.flag_3, 
	flag_4 = ar.flag_4, 
	flag_5 = ar.flag_5, 
	date_1 = ar.date_1, 
	date_2 = ar.date_2,
	date_3 = ar.date_3, 
	date_4 = ar.date_4,
	date_5 = ar.date_5,
	cl_1 = ar.cl_1, 
	cl_2 = ar.cl_2, 
	cl_3 = ar.cl_3, 
	cl_4 = ar.cl_4, 
	cl_5 = ar.cl_5, before_head = ar.before_head, before_body = ar.before_body,no_index_no_follow = ar.no_index_no_follow,custom_html_attr = ar.custom_html_attr,meta_title = ar.meta_title,canonical_url = ar.canonical_url,
	custom_body = ar.custom_body,
	meta_description = ar.meta_description,
	omniture_code = ar.omniture_code,
	custom_setting = ar.custom_setting,
	afterbody = ar.afterbody,
	hideprefix = ar.hideprefix,
	hidesuffix = ar.hidesuffix
from dbo.cms_article_revision ar
where ar.rev_id = @rev_id and dbo.cms_articles.article_id = ar.article_id

if(@@error <> 0) goto RollbackAndReturn


-- mark other old approved revisions as ex-approved
update dbo.cms_article_revision
set
	revision_status = 'E'
where
	article_id = @article_id and revision_status = 'L'
if(@@error <> 0) goto RollbackAndReturn


-- mark this revision as approved
update dbo.cms_article_revision
set
	revision_status = 'L',
	approval_date = getDate(),
	approval_id = @publisher_id
where
	rev_id = @rev_id
if(@@error <> 0) goto RollbackAndReturn


-- mark revision requests as processed & deleted
update dbo.cms_instant_messaging
set
	processed = getDate(),
	deleted = getDate()
where
	related_id = @rev_id and ims_type = 'AA'
if(@@error <> 0) goto RollbackAndReturn





-- update related articles for this revision
delete from dbo.cms_article_relation
where article_id = @article_id

if(@@error <> 0) goto RollbackAndReturn

insert into dbo.cms_article_relation
(article_id, related_zone_id, related_article_id)
select article_id, related_zone_id, related_article_id
from dbo.cms_article_relation_revision
where rev_id = @rev_id

if(@@error <> 0) goto RollbackAndReturn






-- update article zones for this revision
delete from dbo.cms_article_zones
where article_id = @article_id

if(@@error <> 0) goto RollbackAndReturn

if @article_status <> 2
begin
	--if its not a delete request then save new article zones
	insert into dbo.cms_article_zones
	(article_id, zone_id, az_order, az_alias,is_alias_protected,is_page)
	select article_id, zone_id, az_order, az_alias,is_alias_protected,is_page
	from dbo.cms_article_zones_revision
	where rev_id = @rev_id
end

if(@@error <> 0) goto RollbackAndReturn




-- update article language relations for this revision

--declare @lr_id bigint
--select @lr_id = lr.lr_id from dbo.cms_language_relations_revision lr with(nolock) where lr.rev_id = @rev_id

--delete from dbo.cms_language_relations
--where lr_id in (select distinct lr_id from dbo.cms_language_relations  where zone_id in(
--	select distinct zone_id from  dbo.cms_article_zones where article_id = @article_id
--))

delete from dbo.cms_language_relations where lr_id in(
select lr.lr_id from dbo.cms_language_relations_revision lr with(nolock) where article_id = @article_id
)

 
if(@@error <> 0) goto RollbackAndReturn

insert into dbo.cms_language_relations
(lr_id, zone_id, article_id)
select lr_id, zone_id, article_id
from dbo.cms_language_relations_revision
where lr_id in ( select lr.lr_id from dbo.cms_language_relations_revision lr with(nolock) where article_id = @article_id )

if(@@error <> 0) goto RollbackAndReturn




set nocount off

commit transaction

if @article_status = 2
begin
	select 'DELETED' as aStat
end
else
begin
	select 'OK' as aStat, @article_id AS article_id
end
return

RollbackAndReturn:
rollback transaction
select @@ERROR as aStat
return


/* article language revision */

GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_insert_article_language_relations_with_revision]    Script Date: 31.07.2018 11:18:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[cms_asp_admin_insert_article_language_relations_with_revision]
    @rev_id bigint, 
    @zone_id int,
    @article_id int, 
    @related_zone_id int,
    @related_article_id int,
    @pool_id bigint = null
as
declare @lr_id bigint

select @lr_id = isnull(max(lr.lr_id), 0) + 1 from dbo.cms_language_relations_revision lr with(nolock) -- default

declare @rev_id_reverse bigint
 
EXEC @rev_id_reverse = [dbo].[cms_asp_admin_select_article_last_revision] @article_id = @related_article_id, @return_rev_id =  1

if(@pool_id is null)
begin

if (exists(select * from dbo.cms_language_relations_revision lr with(nolock) where lr.article_id = @article_id and lr.zone_id = @zone_id  and rev_id = @rev_id)) 
	begin
		select @lr_id = lr.lr_id from dbo.cms_language_relations_revision lr with(nolock) where lr.article_id = @article_id and lr.zone_id = @zone_id  and rev_id = @rev_id 
		goto addToLanguagePool;
	end

if (exists(select * from dbo.cms_language_relations_revision lr with(nolock) where lr.article_id = @related_article_id and lr.zone_id = @related_zone_id  and rev_id = @rev_id))
	begin
		select @lr_id = lr.lr_id from dbo.cms_language_relations_revision lr with(nolock) where  lr.article_id = @related_article_id and lr.zone_id = @related_zone_id  and rev_id = @rev_id
		goto addToLanguagePool;
	end

end
else if (@pool_id > 0)
begin
	set @lr_id = @pool_id
end


addToLanguagePool:
if not exists(select * from dbo.cms_language_relations_revision where article_id = @article_id and zone_id = @zone_id and lr_id = @lr_id  and rev_id = @rev_id )
begin
	insert into dbo.cms_language_relations_revision
	(lr_id, rev_id, zone_id, article_id)
	values
	(@lr_id, @rev_id, @zone_id, @article_id)
end

 
if not exists(select * from dbo.cms_language_relations_revision where article_id = @related_article_id and zone_id = @related_zone_id and lr_id = @lr_id and rev_id = @rev_id)
begin
  
	insert into dbo.cms_language_relations_revision
	(lr_id, rev_id, zone_id, article_id)
	values
	(@lr_id, @rev_id_reverse, @related_zone_id, @related_article_id)
end




GO
/****** Object:  StoredProcedure [dbo].[cms_WebEvent_GetLogEvent]    Script Date: 8.10.2018 14:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[cms_WebEvent_GetLogEvent]
@WebEventType		   varchar(50),
@BeginDate			   datetime,
@EndDate			   datetime,
@PageIndex             int,
@PageSize              int,
@TotalRecords          int OUTPUT 
AS
	-- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForWebEvents 
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        EventId char(32)
    )
 
	IF (@WebEventType <> '')
	BEGIN

		-- Insert into our temp table
		INSERT INTO #PageIndexForWebEvents (EventId) 
		SELECT e.EventId   
		FROM   dbo.aspnet_WebEvent_Events e  
		WHERE e.Message = @WebEventType AND 
			e.EventTime >= @BeginDate and e.EventTime <=  @EndDate 
		ORDER BY e.EventTime   DESC 

		SELECT @TotalRecords = @@ROWCOUNT

		SELECT e.* 
		FROM   dbo.aspnet_WebEvent_Events e, #PageIndexForWebEvents p
		WHERE  e.EventId COLLATE TURKISH_CI_AS = p.EventId COLLATE TURKISH_CI_AS  AND e.Message COLLATE TURKISH_CI_AS = @WebEventType COLLATE TURKISH_CI_AS AND
			   p.IndexId  COLLATE TURKISH_CI_AS >= @PageLowerBound  COLLATE TURKISH_CI_AS AND p.IndexId <= @PageUpperBound AND
			   e.EventTime  COLLATE TURKISH_CI_AS >= @BeginDate  COLLATE TURKISH_CI_AS and e.EventTime  COLLATE TURKISH_CI_AS <= @EndDate collate Turkish_CI_AS
		ORDER BY e.EventTime DESC
	END
	ELSE
	BEGIN

		-- Insert into our temp table
		INSERT INTO #PageIndexForWebEvents (EventId)
		SELECT e.EventId 
		FROM   dbo.aspnet_WebEvent_Events e 
		WHERE e.EventTime >= @BeginDate and e.EventTime <= @EndDate 
		ORDER BY e.EventTime DESC 

		SELECT @TotalRecords = @@ROWCOUNT

		SELECT e.*
		FROM   dbo.aspnet_WebEvent_Events e, #PageIndexForWebEvents p
		WHERE  e.EventId collate Turkish_CI_AS = p.EventId collate Turkish_CI_AS AND
			   p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound AND
			   e.EventTime >= @BeginDate and e.EventTime <= @EndDate 
		ORDER BY e.EventTime DESC 
	END

   -- RETURN @TotalRecords





