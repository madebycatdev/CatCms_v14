/**
 * @license Copyright (c) 2003-2017, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */


   // The toolbar groups arrangement, optimized for two toolbar rows.
CKEDITOR.editorConfig = function( config ) {

	
	// %REMOVE_START%
	// The configuration options below are needed when running CKEditor from source files.
	config.plugins = 'dialogui,dialog,about,a11yhelp,dialogadvtab,basicstyles,bidi,blockquote,clipboard,button,panelbutton,panel,floatpanel,colorbutton,colordialog,templates,menu,contextmenu,copyformatting,div,resize,toolbar,elementspath,enterkey,entities,popup,filebrowser,find,fakeobjects,flash,floatingspace,listblock,richcombo,font,forms,format,horizontalrule,htmlwriter,iframe,wysiwygarea,image,indent,indentblock,indentlist,smiley,justify,menubutton,language,link,list,liststyle,magicline,maximize,newpage,pagebreak,pastetext,pastefromword,preview,print,removeformat,save,selectall,showblocks,showborders,sourcearea,specialchar,scayt,stylescombo,tab,table,tabletools,undo,wsc';
    config.extraPlugins = 'add-objects,edit-objects,combobox';
	config.skin = 'moono-lisa';
	
    

	
	config.entities = false;
	config.htmlEncodeOutput = false;
	config.basicEntities = false;


/*
	config.toolbar = [
		{ name: 'document', items: [ 'Source', '-', 'Save', 'NewPage', 'Preview', 'Print', '-', 'Templates' ] },
		{ name: 'clipboard', items: [ 'Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo' ] },
		{ name: 'editing', items: [ 'Find', 'Replace', '-', 'SelectAll', '-', 'Scayt' ] },
		{ name: 'forms', items: [ 'Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField' ] },
		'/',
		{ name: 'basicstyles', items: [ 'Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'CopyFormatting', 'RemoveFormat' ] },
		{ name: 'paragraph', items: [ 'NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl', 'Language' ] },
		{ name: 'links', items: [ 'Link', 'Unlink', 'Anchor' ] },
		{ name: 'insert', items: [ 'Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe' ] },
		'/',
		{ name: 'styles', items: [ 'Styles', 'Format', 'Font', 'FontSize' ] },
		{ name: 'colors', items: [ 'TextColor', 'BGColor' ] },
		{ name: 'tools', items: [ 'Maximize', 'ShowBlocks' ] },
		{ name: 'document', items: ['mode', 'document', 'doctools'] },
		{ name: 'objects', items: ['add-objects', 'edit-objects', 'cmsCustomVariables'] }
	];
*/
	config.toolbar = [
		{ name: 'document', items: [ 'Source'] },
		{ name: 'clipboard', items: [ 'Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo' ] },
		{ name: 'basicstyles', items: [ 'Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'CopyFormatting', 'RemoveFormat' ] },
		{ name: 'paragraph', items: [ 'NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock' ] },
		{ name: 'links', items: [ 'Link', 'Unlink', 'Anchor' ] },
		{ name: 'insert', items: [ 'Image', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe' ] },
		{ name: 'tools', items: [ 'Maximize', 'ShowBlocks' ] },
		{ name: 'document', items: ['mode', 'document', 'doctools'] },
		{ name: 'objects', items: ['add-objects', 'edit-objects', 'cmsCustomVariables', 'cmsSystemVariables', '-', 'Templates'] }
	];
	
	config.removeButtons = 'Font';
    config.enterMode = CKEDITOR.ENTER_BR;


	config.protectedSource.push( /<cms[\s\S]*?\/cms>/g );
	config.allowedContent = true;
	//config.protectedSource.push(/<[\s\S]*?>/g); 
    //config.allowedContent = 'span div p[id,class]; h1 a img hr table tr td ul ol li[*]{*}(*)';

		
	// %REMOVE_END%
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';
	
    //config.removeDialogTabs = 'image:advanced;link:advanced;image:Upload;link:Upload';
	//config.removePlugins = 'magicline';
};


