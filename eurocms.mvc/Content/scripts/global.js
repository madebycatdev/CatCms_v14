/*
	EuroCMS.NET
*/

var excludeSite = [];
var excludeZGroup = [];
var excludeZone = [];

(function (jQuery) {
	// set the x-csrf-token for each ajax operation
	// rack / rack_csrf handle the rest
	$.ajaxSetup({
		beforeSend: function (xhr) {
			var token = $('[name=__RequestVerificationToken]').val();
			xhr.setRequestHeader('__RequestVerificationToken', token);
		}
	});
}(jQuery));

function initSwitch(reinitElement) {
    var elementToInit = $('.switch');
    if (reinitElement != undefined) { elementToInit = $(reinitElement + " .switch") };
    elementToInit.each(function () {
        $(this).on('change', function (event) {
            var input = $(this).find('input'),
            a = input.data('act'),
            p = input.data('psf');
            input.prop('checked') === true ?
                input.val(a) : input.val(p);
        });
    });
}
/*-======== Site Load Start ========-*/

$(document).ready(function () {

	$(".changeButton").live("click", function () {
		getZoneGroupList($(this).attr("href"));
	});

	$(".chosen-select").chosen({
		width: "100%",
		allow_single_deselect: true
	}).change(function (event) {
		openZoneDetails();
	})


	$("#addDomain").one("click", function () {
		TableEditable.init(); //Inline table edit
	});

	/*-= Modal Close / CKEditor Destroy Start =-*/

	$('#editContent').on('hidden', function () {
		CKEDITOR.instances.editor_html.destroy();
	})

	/*-= Modal Close / CKEditor Destroy End =-*/

	/*-= Date Picker Start / Article Index Page  =-*/

	$('#startdate_hour, #enddate_hour').datetimepicker({
		onClose: function (selectedDate) {
			$("#enddate_hour").datepicker("option", "minDate", selectedDate);
		}
	});

	//$('#enddate_hour').datetimepicker({
	//	onClose: function (selectedDate) {
	//		$("#startdate_hour").datepicker("option", "maxDate", selectedDate);
	//	}
	//});

	//$('.form_datetime').datepicker({
	//	dateFormat: 'dd/mm/yy'
	//});



	$( "#splash_startdate" ).datepicker({
		dateFormat: 'dd/mm/yy',
		//defaultDate: "+1w",
		changeMonth: true,
		numberOfMonths: 2,
		onClose: function( selectedDate ) {
			$( "#splash_enddate" ).datepicker( "option", "minDate", selectedDate );
		}
	});
	$( "#splash_enddate" ).datepicker({
		dateFormat: 'dd/mm/yy',
		//defaultDate: "+1w",
		changeMonth: true,
		numberOfMonths: 2,
		onClose: function( selectedDate ) {
			$( "#splash_startdate" ).datepicker( "option", "maxDate", selectedDate );
		}
	});

	$('#date_1').each(function () {
		$(this).datetimepicker({});
	});

	$('#date_2').each(function () {
		$(this).datetimepicker({});
	});

	$('#date_3').each(function () {
		$(this).datetimepicker({});
	});

	$('#date_4').each(function () {
		$(this).datetimepicker({});
	});

	$('#date_5').each(function () {
		$(this).datetimepicker({});
	});

	$(".form_datetime .add-on").click(function () {
		$(this).parent(".form_datetime").find("input.hasDatepicker").trigger("focus");
	});

	/*-= Menu Function Start =-*/

	$(".sidebar-toggler").on("click", function () {
		if ($("body").hasClass("page-sidebar-closed")) {
			$.cookie('menuCookie', 'false');
		}
		else {
			$.cookie('menuCookie', 'true');
		}
	});

	/*-= Menu Function End =-*/

	/* Remove Selected Zone Start */

	$('#selectZone .zone option').live('click', function (event) {
		var count = $('#selectZone .zone').find('option:selected').length;
		if (event.ctrlKey) {
			if (count > 1) {
				$(this).attr('selected', false);
			}
		} else if (event.shiftKey) {
			if (count > 1) {
				$('#selectZone .zone').find('option:selected').attr('selected', false);
				$(this).attr('selected', true);
			}
		}
	});

	/* Remove Selected Zone End */

	/* Tab Function Start */

	$(".new-tabs li a").click(function () {
		var index = $(this).attr("href");
		$(".new-tabs li").removeClass("active");
		$(this).parent("li").addClass("active");
		$(".new-tab-content > .tab-pane, .new-tab-content form > .tab-pane").hide();
		$(".new-tab-content > .tab-pane" + index + ", .new-tab-content form > .tab-pane" + index + "").show();
		return false;
	});

	/* Tab Function End */


	// switch button init
	$('.switch').bootstrapSwitch();

	// article status set
	//$('#statusSwitch').on('change', function () {
	//    $(this).prop('checked') === true ?
	//        $('#statusInput').val('1') : $('#statusInput').val('0');
	//});
	$('.newstatus span').each(function () {
		if ($(this).attr('data-val') === $('#statusInput').val()) $(this).addClass('active');
	});

	$('.newstatus span').click(function () {
		$('.newstatus span').removeClass('active');
		$(this).addClass('active');
		$('#statusInput').val($(this).attr('data-val'));
	});

	//$('.switchBt').each(function () {
	//	var a = $(this).attr('id').split("_")[0],
	//	y = $(this).data('act'),
	//	n = $(this).data('psf');
	//	//console.log(a, y, n)
	//	$('#' + a + '_StatusSwitch').on('change', function () {
	//		$('#' + a + '_StatusSwitch').prop('checked') === true ?
	//			$('#' + a + '_status').val(y) : $('#' + a + '_status').val(n);
	//	});
	//});

	initSwitch();

	//Article File Index Image Popover
	$('td[data-title="File(s)"] .popovers').each(function (index, el) {
		var href = $(this).attr('href'),
		ext = href.split(/[. ]+/).pop();

		switch (ext) {
			case "jpg":
			case "jpeg":
			case "png":
			case "gif":
				$(this).popover({ placement: 'right', content: '<img src="' + href + '">', html: true, trigger: 'hover' });
		}
	});

	//Article Files Edir

	$('.afiles .afilePre').each(function (index, el) {
		var href = $('a', this).attr('href'),
		ext = href.split(/[. ]+/).pop();

		$(this).addClass('nonImg');
		if (ext != "jpg" && ext != "jpeg" && ext != "png" && ext != "gif")
			$('a', this).html('<i class="icon-file"></i>');

	});
	//var i = 1;
	$(document).on('change','#redirectRequestType, #editRedirectRequestType', function(event) {
		event.preventDefault();
		//console.log(i);
		//i = i+1;
		$('#redirectRequestUrl, #redirectTargetUrl, #editRedirectRequestUrl, #editRedirectTargetUrl').removeAttr('required').removeAttr('readonly').val("");
		if($('#redirectRequestType').find(':selected').text() == "Domain with Mirroring" )
			$('#redirectRequestUrl').val("*");
		if($('#editRedirectRequestType').find(':selected').text() == "Domain with Mirroring" )
			$('#editRedirectRequestUrl').val("*");
		$('#redirectRequestUrl, #redirectTargetUrl, #editRedirectRequestUrl, #editRedirectTargetUrl').attr($(this).find(':selected').data('attr'),"");	


		
	});

	// Form Validation
	FormValidation();

}); // __end__ document.ready

//chosen
function openZoneDetails() {
	$('#newZones .chosen-container li').each(function () {
		$('span', this).live('click', function () {
			$('.chosen-container').addClass('chosen-with-drop-disable');
			var selectedIndex = $(this).next('a').attr('data-option-array-index');
			//console.log($('.chosen-select').find('*:eq(' + selectedIndex + ')').val());
			var selectedZone = $('.chosen-select').find('*:eq(' + selectedIndex + ')');
			onZoneChanged(selectedZone);

			$('.chosen-container li').removeClass('active');
			$(this).parent().addClass('active');
			$('.notuniform').parent().parent('.checker').removeClass();
			$("input[type=checkbox]:not(.toggle), input[type=radio]:not(.toggle, .star)").uniform();
		});
	})
}


