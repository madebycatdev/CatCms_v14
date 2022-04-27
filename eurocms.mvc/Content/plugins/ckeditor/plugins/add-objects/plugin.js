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

function getObject(url){
	var r_Result = "";
	$.ajax({
		type:"GET",
		url:""+ url +"",
		async:false,
		success:function(response){
			r_Result = response;
		}
	});
	return r_Result
};

function menuRender(){
	
	var r_Result = "<div>"+
		"<ul>"+
			"<li><a onclick=\"return iM('a','Menu A')\" href=\"javascript:void(0);\"><img hspace=\"4\" src=\"/cms/Content/img/icon_menu.png\"/>Menu A</a></li>" +
			"<li><a onclick=\"return iSM('a','Sitemap'),false;\" href=\"javascript:void(0);\"><img hspace=\"4\" src=\"/cms/Content/img/icon_sitemap.png\">Sitemap</a></li>" +
			"<li><a onclick=\"return iTag('a','Tag Listing'),false;\" href=\"javascript:void(0);\" ><img hspace=\"4\" src=\"/cms/Content/img/icon_tag.png\">Tag Listing</a></li>" +
		"</ul>"+
	"</div>";
	
	return r_Result;
}

function iP(id, text) {
    var element = CKEDITOR.dom.element.createFromHtml("<cms:portlet src='/cms/Content/img/icon_portlet.png' title='" + text + "' alt='##portlet_" + id + "##' runat='server'><content><img class='' src='/cms/Content/img/icon_portlet.png' /></content></cms:portlet>");
	CKEDITOR.instances[editorName].insertElement(element);
	CKEDITOR.dialog.getCurrent().hide();
	return true;
}

function iM(id,text){
    var element = CKEDITOR.dom.element.createFromHtml("<cms:menu title='" + text + "' alt='##menu_" + id + "##' runat='server'><content><img class='' src='/cms/Content/img/icon_menu.png' /></content></cms:menu>");
	CKEDITOR.instances[editorName].insertElement(element);
	CKEDITOR.dialog.getCurrent().hide();
	return true;
}

function iPl(id,text){
    var element = CKEDITOR.dom.element.createFromHtml("<cms:plugin title='" + text + "' alt='##plugin_" + id + "##' runat='server'><content><img class='' src='/cms/Content/img/icon_plugin.png' /></content></cms:plugin>");
	CKEDITOR.instances[editorName].insertElement(element);
	CKEDITOR.dialog.getCurrent().hide();
	return true;
}

function iSM(id,text){
    var element = CKEDITOR.dom.element.createFromHtml("<cms:sitemap title='" + text + "' alt='##sitemap_" + id + "##' runat='server'><content><img class='' src='/cms/Content/img/icon_sitemap.png' /></content></cms:sitemap>");
	CKEDITOR.instances[editorName].insertElement(element);
	CKEDITOR.dialog.getCurrent().hide();
	return true;
}

function iTag(id,text){
    var element = CKEDITOR.dom.element.createFromHtml("<cms:tag title='" + text + "' alt='##tag_" + id + "##' runat='server'><content><img class='' src='/cms/Content/img/icon_tag.png' /></content></cms:tag>");
	CKEDITOR.instances[editorName].insertElement(element);
	CKEDITOR.dialog.getCurrent().hide();
	return true;
}

function iB(id,text){
    var element = CKEDITOR.dom.element.createFromHtml("<cms:breadcrumb title='" + text + "' alt='##breadcrumb_" + id + "##' runat='server'><content><img class='' src='/cms/Content/img/icon_breadcrumb.png' /></content></cms:breadcrumb>");
	CKEDITOR.instances[editorName].insertElement(element);
	CKEDITOR.dialog.getCurrent().hide();
	return true;
}

CKEDITOR.plugins.add( 'add-objects', {
    icons: 'AddObjects',
    init: function( editor ) {
		
        editor.ui.addButton( 'add-objects', {
            label: 'Insert Object',
            command: 'insertObject',
            toolbar: 'objects',
			icon: CKEDITOR.plugins.getPath('add-objects') + 'icons/add-ico.png'
        });

		editor.addCommand('insertObject', new CKEDITOR.dialogCommand('insertObject'));
        editorName = editor.name;
		
    }
});



CKEDITOR.dialog.add( 'insertObject', function ( editor ) {
    return {
        title: 'Insert Object',
        minWidth: 400,
        contents: [
			{
				id: 'tab-portlet',
				label: 'Portlet',
				elements: [
					{
						type: 'html',
						html:getObject(portletList_var)
					}
				]
			},
			{
				id: 'tab-menu',
				label: 'Menu',
				elements: [
					{
						type: 'html',
						html:menuRender()
					}
				]
			},/*
			{
				id: 'tab-plugins',
				label: 'Plugins',
				elements: [
					{
						type: 'html',
						html:getObject(pluginList_var)
					}
				]
			},*/
			{
				id: 'tab-breadcrumbs',
				label: 'BreadCrumbs',
				elements: [
					{
						type: 'html',
						html:getObject(breadcrumbList_var)
					}
				]
			}/*,
            {
                id: 'tab-custom',
                label: 'Custom Contents',
                elements: [
					{
					    type: 'text'
					}
                ]
            }*/
		]
    };
});