/*
	EuroCMS.NET
*/
/*$(".editButton").each(function(index,element) {
	$(this).css("top",$(this).index())*50
})*/
/*
window.onload = function(){
	var editButtonIndex = [].slice.call(document.getElementsByClassName("editButton"));
	for (var i = 0; i < editButtonIndex.length; i++) {
		editButtonIndex[i].style.top = (((i+1) * 32) +100) + "px";
		editButtonIndex[i].style.position = "fixed"

		//console.log(editButtonIndex)
	}	
}
*/
var ed_pop = false;
var ed_int = 0;
var xy;

if (!window.captureEvents) document.onmousemove = getMousePos
if (window.captureEvents) {
	window.captureEvents(Event.MOUSEMOVE)
	window.onmousemove = getMousePos

}


function getMousePos(e) { //you need the 'e', although it does NOT need to be defined
	//NS
	if (document.layers || document.getElementById && !document.all) {
		mousex = e.pageX;
		mousey = e.pageY;
	}
	//IE
	else if (document.all) {
		mousex = window.event.clientX;
		mousey = window.event.clientY;
	}
}

function showEditButtons(inID, inFlags, inSID, inGID, inZID, inAID, inRID, cmsPath) {
	if (!document.getElementById("editButtonz") && !ed_pop) {

		ed_pop = true;

		//var but = document.getElementById('editButton' + inAID);
		//console.log(inID);
		var but = document.querySelectorAll('a[onmouseout="hideEditButtons(' + inID + ');"]')
		//console.log('but ' , but);
		var tempVar = but[0];
		var butTop = tempVar.offsetTop;
		var butLeft = tempVar.offsetLeft + 27;
		console.log(butTop + " / " + butLeft);
		var obj = {
			left: mousex,
			top: mousey,
			width: '',
			height: 'auto'
		};

		var position = obj;

		var nh = document.createElement('ul');
		nh.id = "editButtonz";
		nh.className = "editButtonz";
		nh.style.width = position.width + "px";
		nh.style.height = position.height;
		nh.style.top = (position.top) + "px";
		nh.style.left = position.left + "px";
		nh.onmouseover = function() {
			ed_pop = true;
		};
		nh.onmouseout = function() {
			ed_pop = false;
		};
		document.body.appendChild(nh);

		var ed_int = window.setInterval('hideEditButtons(' + inID + ')', 100);

		var eb = document.getElementById("editButtonz");

		if (inRID > 0) {
			var a_link = '<li><a onmouseover="ed_pop=true;" href="' + cmsPath + '/Article/Edit/' + inAID + '?RevisionId=' + inRID + '#tab_2_' + inID + '" target="editor" >Revision ##data##</a></li>';
			var af_link = '';
		} else {
			var a_link = '<li><a onmouseover="ed_pop=true;" href="' + cmsPath + '/Article/Edit/' + inAID + '#tab_2_' + inID + '" target="editor" >Article ##data##</a></li>';
			var a_link = a_link + '<li><a onmouseover="ed_pop=true;" href="' + cmsPath + '/Article/Edit/' + inAID + '#tab_1_2" target="editor" >Article Advanced</a></li>';
			var af_link = '<li><a onmouseover="ed_pop=true;" href="' + cmsPath + '/ArticleFile?ArticleId=' + inAID + '&RevisionId=' + inRID + '" target="editor" >Article Files </a></li>';
		}
		var z_link = '<li><a onmouseover="ed_pop=true;" href="' + cmsPath + '/Zone/Edit/' + inZID + '#tab_2_' + inID + '" target="editor" >Zone ##data##</a></li>';
		var g_link = '<li><a onmouseover="ed_pop=true;" href="' + cmsPath + '/ZoneGroup/Edit/' + inGID + '#tab_2_' + inID + '" target="editor" >Zone Group ##data##</a></li>';
		var s_link = '<li><a onmouseover="ed_pop=true;" href="' + cmsPath + '/Site/Edit/' + inSID + '#tab_2_' + inID + '" target="editor" >Site ##data##</a></li>';

		if (inFlags.indexOf('A') != -1) {
			a_link = a_link.replace('##data##', '*');
		} else {
			a_link = a_link.replace('##data##', '');
		}
		if (inFlags.indexOf('Z') != -1) {
			z_link = z_link.replace('##data##', '*');
		} else {
			z_link = z_link.replace('##data##', '');
		}
		if (inFlags.indexOf('G') != -1) {
			g_link = g_link.replace('##data##', '*');
		} else {
			g_link = g_link.replace('##data##', '');
		}
		if (inFlags.indexOf('S') != -1) {
			s_link = s_link.replace('##data##', '*');
		} else {
			s_link = s_link.replace('##data##', '');
		}

		eb.innerHTML = a_link + z_link + g_link + s_link + af_link;

	}
}

function hideEditButtons(inID) {
	if (document.getElementById("editButtonz") && !ed_pop) {
		document.body.removeChild(document.getElementById("editButtonz"));
		window.clearInterval(ed_int);
	}
}

/*function thisclicked(e, element)
{
	e = e || event;
	var target = e.target || e.srcElement;
	if(target.id==element.id)
		return true;
	else
		return false;
}

function mainClick(event, element)
{
	if(thisclicked(event, element))
	{
		Aloha.ready( function() {
			Aloha.jQuery("#article_1").mahalo();
			Aloha.jQuery("#article_2").mahalo();
			Aloha.jQuery("#article_3").mahalo();
			Aloha.jQuery("#article_4").mahalo();
			Aloha.jQuery("#article_5").mahalo();
			Aloha.jQuery("#template").aloha();
		})
		
	}
}
function inClick(event, element)
{
	if(thisclicked(event, element))
	{
		Aloha.ready( function() {
			Aloha.jQuery("#template").mahalo();
			Aloha.jQuery("#"+element.getAttribute("id")).aloha();
		})
	}
}

Aloha.ready( function() {
	Aloha.jQuery("#template").aloha();
	Aloha.jQuery("#article_1").aloha();
	Aloha.jQuery("#article_2").aloha();
	Aloha.jQuery("#article_3").aloha();
	Aloha.jQuery("#article_4").aloha();
	Aloha.jQuery("#article_5").aloha();	
})*/
