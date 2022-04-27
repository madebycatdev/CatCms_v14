var UIModals = function () {

    
    var initModals = function () {
       	$.fn.modalmanager.defaults.resize = true;
		$.fn.modalmanager.defaults.spinner = '<div class="loading-spinner fade" style="width: 200px; margin-left: -100px;"><img src="/cms/Content/img/ajax-modal-loading.gif" align="middle">&nbsp;<span style="font-weight:300; color: #eee; font-size: 18px; font-family:Open Sans;">&nbsp;Loading...</span></div>';


       	var $modal = $('#ajax-modal');
 
       	$("#quickStructure li a, .editObject, .openModal").not("[data-toggle=dropdown], .delete").on('click', function () {
       	    var thisLink = $(this).attr("href");
		  // create the backdrop and wait for next modal to be triggered
		  $('body').modalmanager('loading');

		      $modal.load(thisLink, '', function (responseText, textStatus, XMLHttpRequest) {
		          $modal.modal().on("hidden", function() {
		             $modal.empty();
		          });
		          if (textStatus == "error")
		          {
		              alert(XMLHttpRequest.status + ": " + XMLHttpRequest.statusText);
		              $('#ajax-modal').modal('hide');
		              $(".modal-scrollable, .modal-backdrop").remove();
		          }
		      });
		  
		  return false;
		}); 
		 
		$modal.on('click', '.update', function(){
		  $modal.modal('loading');
		  setTimeout(function(){
		    $modal
		      .modal('loading')
		      .find('.modal-body')
		        .prepend('<div class="alert alert-info fade in">' +
		          'Updated!<button type="button" class="close" data-dismiss="alert"></button>' +
		        '</div>');
		  }, 0);
		}); 
       
    }

    return {
        //main function to initiate the module
        init: function () {
            initModals();
        }

    };

}();