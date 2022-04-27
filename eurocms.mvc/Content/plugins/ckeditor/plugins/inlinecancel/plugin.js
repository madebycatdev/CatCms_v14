var countArr = [];

CKEDITOR.plugins.add( 'inlinecancel',
{
	init: function( editor )
	{		
		editorName = editor.name
		countArr.push([editorName,1]);
		
		editor.on('focus', function( e ) {
			var eName = e.editor["name"];
			
			for(var i=0;i<countArr.length;i++)
			{
				if(countArr[i][1] == 1 && countArr[i][0] == e.editor["name"])
				{
					CKEDITOR.instances[eName].firstSnapshot = CKEDITOR.instances[eName].getData();
					countArr[i][1] = 2
				}	
			}
		} );
		
		/*editor.on( 'blur', function( e ) {
			if(confirm("Degişiklikleri iptal mi etmek istiyorsunuz?"))
			{
				editor.setData(CKEDITOR.editor.firstSnapshot);
				$('#cke_'+editor.name).show();
				$('.cke_editable_inline').addClass('cke_focus');
				$('.cke_editable_inline').blur();
			}
			//$('.cke_editable_inline[data-name='+ editor.container.getAttribute("data-name") +']').addClass('cke_focus')
			//editor.focusManage.focus( editor.editable() );
		} );*/
		
		editor.addCommand( 'inlinecancel',
			{
				exec : function( editor )
				{    
					if(confirm("Degişiklikleri iptal mi etmek istiyorsunuz?"))
					{
						var eeName = editor.name;
						CKEDITOR.instances[eeName].setData(CKEDITOR.instances[eeName].firstSnapshot);
						$('#cke_'+editor.name).hide();
						$('.cke_editable_inline').removeClass('cke_focus');
						$('.cke_editable_inline').blur();
								
						for(k in CKEDITOR.instances){
							var instance = CKEDITOR.instances[k];
							instance.destroy();
							
							var names = $(instance).attr("name");
							//console.log($("#"+names));
							$("#"+names).attr("contenteditable",false);
						}
								
					}
				}
			});
			
		editor.ui.addButton( 'Inlinecancel',
		{
			label: 'Cancel',
			command: 'inlinecancel',
			icon: this.path + 'images/inlinecancel.png'
		} );
	}
} );