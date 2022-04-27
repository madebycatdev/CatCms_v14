/**
 * Basic sample plugin inserting current date and time into CKEditor editing area.
 *
 * Created out of the CKEditor Plugin SDK:
 * http://docs.ckeditor.com/#!/guide/plugin_sdk_intro
 */

var editorName;

function modelessDialogShow(url,width,height){
	var left = screen.availWidth/2 - width/2;
	var top = screen.availHeight/2 - height/2;
	window.open(url, "", "dependent=yes,width="+width+"px,height="+height+",left="+left+",top="+top);
}

/*-======== Portlet Properties Function Start ========-*/

function applyPortlet(e) {

	var selectObj = CKEDITOR.instances[editorName].getSelection().getSelectedElement();
	var parent = selectObj.getParent();
	var parentObj = parent.getParent();
	var s_Element = $(parentObj).attr("alt");
	
    var includeArr = [];
    var excludeArr = [];

    $("#include_articles option").each(function () {
        includeArr.push($(this).val());
    });

    $("#exclude_articles option").each(function () {
        var excludeSplit = $(this).val().split("-");
        excludeArr.push(excludeSplit[1]);

    });

    p_IncludeArr = includeArr.join(",");
    $("#inpInclude").val(p_IncludeArr);
    p_ExcludeeArr = excludeArr.join(",");
    $("#inpExclude").val(p_ExcludeeArr);

    var p_ZoneId = $("#zone_id option:selected").val();
    var p_ItemCount = $("#inpItemCount option:selected").val();
    var p_ItemOrder = $("#inpOrderID option:selected").val();
    var p_MenuDepth = $("#menu_depth option:selected").val();
    var p_SitemapType = $("#SitemapType option:selected").val();
    var p_Header = $("#inpHeader").val();
    var p_ClassName = $("#inpClass").val();
    var p_ContainerTag = $("#inpContainer").val();
    var p_Include = $("#inpInclude").val();
    var p_Exclude = $("#inpExclude").val();
    var p_Seperator = $("#inpSeperator").val();
    var p_PrevNextCaption = $("#inpPrevNext").val();
    var p_PagerHeader = $("#inpPagerHeader").val();
    var p_PagerCount = $("#inpPagerCount option:selected").val();
    var p_PagerLocation = $("#inpPagerLocation option:selected").val();
    var p_ClassPager = $("#inpPagerClass").val();

    var p_MenuNotSelectedCls = $("#menu_not_selected_item_class").val();
    var p_MenuSelectedCls = $("#menu_selected_item_class").val();
    var p_MenuEliminateSingle = $("#eliminate_single_items").is(":checked") == true ? "true" : "";
    var p_MenuRemoveOnclick = $("#remove_onlick_function").is(":checked") == true ? "true" : "";
    var p_MenuContainerTagId = $("#menu_container_tag_id").val();
    var p_MenuPosition = $("#inpVH option:selected").val();

    if (s_Element.substr(0, 10) == "##portlet_") {
        var p_Ids = p_ZoneId + "_" + p_ItemCount + "_" + p_ItemOrder;
        var p_ExcludeSelf = $("#inpExSelf").is(":checked") == true ? "true" : "";
		
		$(parentObj).attr("cid", p_Ids);
        $(parentObj).attr("hspace", p_ExcludeSelf);
		$(parentObj).attr("PortletID", s_Element.substr(10, 10).replace("#","").replace("#",""));
		$(parentObj).attr("Zone", p_ZoneId);
		$(parentObj).attr("ItemCount", p_ItemCount);
		$(parentObj).attr("ItemOrdering", p_ItemOrder);
		$(parentObj).attr("Header", p_Header);
		$(parentObj).attr("ClassName", p_ClassName);
		$(parentObj).attr("ContainerTag", p_ContainerTag);
		$(parentObj).attr("DisplayArticles", p_Include);
		$(parentObj).attr("ExternalArticles", p_Exclude);
		$(parentObj).attr("PagerClassName", p_ClassPager);
		$(parentObj).attr("PagerCount", p_PagerCount);
		$(parentObj).attr("PagerPosition", p_PagerLocation);
		$(parentObj).attr("PagerHeader", p_PagerHeader);
		$(parentObj).attr("PagerCaptions", p_PrevNextCaption);
		$(parentObj).attr("PagerSeperator", p_Seperator);
		
    } else if (s_Element.substr(0, 10) == "##sitemap_") {
        var p_Ids = p_ZoneId + "_" + p_MenuDepth + "_" + p_ItemOrder + "_" + p_SitemapType;
		
		$(parentObj).attr("cid", p_Ids);
		$(parentObj).attr("SitemapType", "sitemap");
		$(parentObj).attr("Zone", p_ZoneId);
		$(parentObj).attr("MenuDepth", p_MenuDepth);
		$(parentObj).attr("ItemOrdering", p_ItemOrder);
		$(parentObj).attr("ClassName", p_ClassName);
		$(parentObj).attr("ContainerTag", p_ContainerTag);
		
    } else if (s_Element.substr(0, 7) == "##menu_") {
        var p_Ids = p_ZoneId + "_" + p_MenuDepth + "_" + p_ItemOrder + "_" + p_MenuPosition;
		
		$(parentObj).attr("cid", p_Ids);
		$(parentObj).attr("MenuType", "menu");
		$(parentObj).attr("Zone", p_ZoneId);
		$(parentObj).attr("MenuDepth", p_MenuDepth);
		$(parentObj).attr("ItemOrdering", p_ItemOrder);
		$(parentObj).attr("ClassName", p_ClassName);
		$(parentObj).attr("ContainerTag", p_ContainerTag);
		$(parentObj).attr("DisplayArticles", p_Include);
		$(parentObj).attr("ExternalArticles", p_Exclude);
		$(parentObj).attr("Position", p_MenuPosition);
		$(parentObj).attr("ContainerTagId", p_MenuContainerTagId);
		$(parentObj).attr("EliminateSignleItems", p_MenuEliminateSingle);
		$(parentObj).attr("EliminateOnclikFunction", p_MenuRemoveOnclick);
		$(parentObj).attr("SelectedItemClass", p_MenuSelectedCls);
		$(parentObj).attr("NotSelectedItemClass", p_MenuNotSelectedCls);
    }
    $(e).parents("div").find(".close").trigger("click");

    return false;
}

/*-======== Portlet Properties Function End ========-*/

CKEDITOR.plugins.add( 'edit-objects', {
    icons: 'EditObjects',
    init: function( editor ) {
        editor.addCommand( 'editObject', {
            exec: function (editor) {
                
                editorName = editor.name;

                var sel = editor.getSelection().getSelectedElement();
				if(sel != null)
				{
				    var selectObj = CKEDITOR.instances[editorName].getSelection().getSelectedElement();
					var parent = selectObj.getParent();
					var parentObj = parent.getParent();
				    var s_Element = $(parentObj).attr("alt");
					
				    if (s_Element.substr(0, 10) == "##portlet_") {
				        var s_Id = $(parentObj).attr("cid");
				        if (s_Id != undefined) {
				            var s_Split = s_Id.split("_");
				            var s_ZoneId = s_Split[0];
				            var s_ItemCount = s_Split[1];
				            var s_ItemOrder = s_Split[2];
				        } else {
				            var s_ZoneId = 0
				            var s_ItemCount = 0
				            var s_ItemOrder = 0
				        }		

				        var s_Class = $(parentObj).attr("ClassName") != undefined ? $(parentObj).attr("ClassName") : "";
				        var s_Lang = $(parentObj).attr("ContainerTag") != undefined ? $(parentObj).attr("ContainerTag") : "";
				        var s_Include = $(parentObj).attr("DisplayArticles") != undefined ? $(parentObj).attr("DisplayArticles") : "";
				        var s_Exclude = $(parentObj).attr("ExternalArticles") != undefined ? $(parentObj).attr("ExternalArticles") : "";
						
				        var s_Header = $(parentObj).attr("Header") != undefined ? $(parentObj).attr("Header") : "";
				        var s_Seperator = $(parentObj).attr("PagerSeperator") != undefined ? $(parentObj).attr("PagerSeperator") : "";
				        var s_PrevNextCaption = $(parentObj).attr("PagerCaptions") != undefined ? $(parentObj).attr("PagerCaptions") : "";
				        var s_PagerHeader = $(parentObj).attr("PagerHeader") != undefined ? $(parentObj).attr("PagerHeader") : "";
				        var s_PagerCount = $(parentObj).attr("PagerCount") != undefined ? $(parentObj).attr("PagerCount") : "";
				        var s_PagerLocation = $(parentObj).attr("PagerPosition") != undefined ? $(parentObj).attr("PagerPosition") : "";
				        var s_ClassPager = $(parentObj).attr("PagerClassName") != undefined ? $(parentObj).attr("PagerClassName") : "";
				        var s_ExcludeSelf = $(parentObj).attr("hspace") != undefined ? $(parentObj).attr("hspace") : "";

				        $('body').modalmanager('loading');
				        $.ajax({
				            "url": "/cms/Portlet/Properties",
				            "type": "POST",
				            "data": "zone_id=" + s_ZoneId + "&item_count=" + s_ItemCount + "&item_ordering=" + s_ItemOrder + "&portlet_header=" + s_Header + "&container_tag=" + s_Lang + "&include_articles=" + s_Include + "&exclude_articles=" + s_Exclude + "&pager_class=" + s_ClassPager + "&exclude_self=" + s_ExcludeSelf + "&pager_count=" + s_PagerCount + "&pager_position=" + s_PagerLocation + "&pager_header=" + s_PagerHeader + "&prev_next_caption=" + s_PrevNextCaption + "&item_seperator=" + s_Seperator + "&class_name=" + s_Class  + "",
				            "success": function (res) {
				                $("#ajax-modal").html(res);
				                $("#ajax-modal").modal().on("hidden", function () {
				                    $("#ajax-modal").empty();
				                });
				            }
				        });
				    } else if (s_Element.substr(0, 10) == "##sitemap_") {
				        var s_Id = $(parentObj).attr("cid");
				        if (s_Id != undefined) {
				            var s_Split = s_Id.split("_");
				            var s_ZoneId = s_Split[0];
				            var s_MenuDepth = s_Split[1];
				            var s_ItemOrder = s_Split[2];
				            var s_SiteMapType = s_Split[3];
				        } else {
				            var s_ZoneId = 0
				            var s_MenuDepth = 0
				            var s_ItemOrder = 0
				            var s_SiteMapType = 0
				        }
						
				        var s_Class = $(parentObj).attr("ClassName") != undefined ? $(parentObj).attr("ClassName") : "";
				        var s_Lang = $(parentObj).attr("ContainerTag") != undefined ? $(parentObj).attr("ContainerTag") : "";
						
				        $('body').modalmanager('loading');
				        $.ajax({
				            "url": "/cms/Sitemap/Properties",
				            "type": "POST",
				            "data": "zone_id=" + s_ZoneId + "&menu_depth=" + s_MenuDepth + "&item_ordering=" + s_ItemOrder + "&class_name=" + s_Class + "&container_tag=" + s_Lang  + "&sitemap_type=" + s_SiteMapType + "",
				            "success": function (res) {
				                $("#ajax-modal").html(res);
				                $("#ajax-modal").modal().on("hidden", function () {
				                    $("#ajax-modal").empty();
				                });
				            }
				        });
				    } else if (s_Element.substr(0, 7) == "##menu_") {
				        var s_Id = $(parentObj).attr("cid");
				        if (s_Id != undefined) {
				            var s_Split = s_Id.split("_");
				            var s_ZoneId = s_Split[0];
				            var s_MenuDepth = s_Split[1];
				            var s_ItemOrder = s_Split[2];
				            var s_Position = s_Split[3];
				        } else {
				            var s_ZoneId = 0
				            var s_MenuDepth = 0
				            var s_ItemOrder = 0
				            var s_Position = "v"
				        }

				        var s_Class = $(parentObj).attr("ClassName") != undefined ? $(parentObj).attr("ClassName") : "";
				        var s_Lang = $(parentObj).attr("ContainerTag") != undefined ? $(parentObj).attr("ContainerTag") : "";
				        var s_Include = $(parentObj).attr("DisplayArticles") != undefined ? $(parentObj).attr("DisplayArticles") : "";
				        var s_Exclude = $(parentObj).attr("ExternalArticles") != undefined ? $(parentObj).attr("ExternalArticles") : "";
				        var s_MenuNotSelectedCls = $(parentObj).attr("NotSelectedItemClass") != undefined ? $(parentObj).attr("NotSelectedItemClass") : "";
				        var s_MenuSelectedCls = $(parentObj).attr("SelectedItemClass") != undefined ? $(parentObj).attr("SelectedItemClass") : "";
				        var s_MenuOnClickFunction = $(parentObj).attr("EliminateOnclikFunction") != undefined ? $(parentObj).attr("EliminateOnclikFunction") : "";
				        var s_MenuSingleItem = $(parentObj).attr("EliminateSignleItems") != undefined ? $(parentObj).attr("EliminateSignleItems") : "";
				        var s_MenuContainerTagId = $(parentObj).attr("ContainerTagId") != undefined ? $(parentObj).attr("ContainerTagId") : "";
				        
				        $('body').modalmanager('loading');
				        $.ajax({
				            "url": "/cms/Menu/Properties",
				            "type": "POST",
				            "data": "zone_id=" + s_ZoneId + "&menu_depth=" + s_MenuDepth + "&item_ordering=" + s_ItemOrder + "&class_name=" + s_Class + "&container_tag=" + s_Lang + "&include_articles=" + s_Include + "&exclude_articles=" + s_Exclude + "&position=" + s_Position + "&container_tag_id=" + s_MenuContainerTagId + "&eliminate_single=" + s_MenuSingleItem + "&remove_onclick_function=" + s_MenuOnClickFunction + "&selected_item_class=" + s_MenuSelectedCls + "&not_selected_item_class=" + s_MenuNotSelectedCls + "",
				            "success": function (res) {
				                $("#ajax-modal").html(res);
				                $("#ajax-modal").modal().on("hidden", function () {
				                    $("#ajax-modal").empty();
				                });
				            }
				        });
				    }
					else{
						alert("This is not a portlet, menu, tag, sitemap or plugin item.");
					}
				}
            }
        });
				
        editor.ui.addButton( 'edit-objects', {
            label: 'Edit Object',
            command: 'editObject',
            toolbar: 'objects',
			icon: CKEDITOR.plugins.getPath('edit-objects') + 'icons/edit-ico.png'
        });
		
		//editor.addCommand( 'insertObject', new CKEDITOR.dialogCommand( 'insertObject' ) );
    }
});


CKEDITOR.dialog.add( 'editObject', function ( editor ) {
    return {
        title: 'Edit Object',
        minWidth: 400,
        contents: [
			{
				id: 'tab-custom',
				label: 'Custom Contents',
				elements: [
					{
						type: 'html',
						html:"",
					}
				]
			},
			{
				id: 'tab-portlet',
				label: 'Portlet',
				elements: [
					{
						type: 'text',
						id: 'id',
						label: 'Id'
					}
				]
			},
			{
				id: 'tab-menu',
				label: 'Menu',
				elements: [
					{
						type: 'text',
						id: 'id',
						label: 'Id'
					}
				]
			},
			{
				id: 'tab-plugins',
				label: 'Plugins',
				elements: [
					{
						type: 'text',
						id: 'id',
						label: 'Id'
					}
				]
			},
			{
				id: 'tab-breadcrumbs',
				label: 'BreadCrumbs',
				elements: [
					{
						type: 'text',
						id: 'id',
						label: 'Id'
					}
				]
			}
		]
    };
});