// This code is generally not necessary, but it is here to demonstrate
// how to customize specific editor instances on the fly. This fits well
// this demo because we have editable elements (like headers) that
// require less features.

// The "instanceCreated" event is fired for every editor instance created.
//CKEDITOR.disableAutoInline = true;
CKEDITOR.on( 'instanceCreated', function( event ) {
	var editor = event.editor,
		element = editor.element;
		
		

	// Customize editors for headers and tag list.
	// These editors don't need features like smileys, templates, iframes etc.
	//if ( element.is( 'h1', 'h2', 'h3' ) || element.getAttribute( 'id' ) == 'taglist' ) {
		// Customize the editor configurations on "configLoaded" event,
		// which is fired after the configuration file loading and
		// execution. This makes it possible to change the
		// configurations before the editor initialization takes place.
		editor.on( 'configLoaded', function() {
			
			editor.config.extraPlugins = 'specialchar,inlinesave,inlinecancel';
			
			// Remove unnecessary plugins to make the editor simpler.
			editor.config.removePlugins = 'colorbutton,find,flash,font,' +
				'forms,iframe,newpage,removeformat,' +
				'smiley,stylescombo,templates,magicline,table,tabletools';
				
			editor.config.allowedContent =
				'h1 h2 h3 p pre[align]; ' +
				'blockquote code kbd samp var del ins cite q b i u strike ul ol li hr table tbody tr td th caption script; ' +
				'img[!src,alt,align,width,height]; font[!face]; font[!family]; font[!color]; font[!size]; font{!background-color}; a[!href]; a[!name]';
				
				


			// Rearrange the layout of the toolbar.
			editor.config.toolbarGroups = [
				{ name: 'editing',		groups: [ 'basicstyles', 'links' ] },
				{ name: 'undo' },
				{ name: 'clipboard',	groups: [ 'selection', 'clipboard' ] },
				{ name: 'insert' },
				{ name: 'others', groups: [ 'inlinecancel', 'inlinesave' ] }
			];
			
			// Make dialogs simpler.
    		editor.config.removeDialogTabs = 'image:advanced;link:advanced;image:info;link:info';
			
		});
	//}
});