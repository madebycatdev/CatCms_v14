CKEDITOR.plugins.add( 'inlinesave',
{
	init: function( editor )
	{
		editor.addCommand( 'inlinesave',
			{
				exec : function( editor )
				{

					addData();
					
					function addData() {
						var data = editor.getData();
						var dataID = editor.container.getAttribute("data-id");
						
						jQuery.ajax({

							type: "POST",

							//Specify the name of the file you wish to use to handle the data on your web page with this code:
							//<script>var dump_file="yourfile.php";</script>
							//(Replace "yourfile.php" with the relevant file you wish to use)
							//Data can be retrieved from the variable $_POST['editabledata']
							//The ID of the editor that the data came from can be retrieved from the variable $_POST['editorID']
							
							url: '//localhost/ckeditor/samples/sample_posteddata_2.php',		

							data: { editabledata: data, editorID: dataID }

						})

						.done(function (data, textStatus, jqXHR) {

							alert("Your content was successfully saved. [" + jqXHR.responseText + "]");

						})

						.fail(function (jqXHR, textStatus, errorThrown) {

							alert("Error saving content. [" + jqXHR.responseText + "]");

						});   
					} 

				}
			});
		editor.ui.addButton( 'Inlinesave',
		{
			label: 'Save',
			command: 'inlinesave',
			icon: this.path + 'images/inlinesave.png'
		} );
	}
} );