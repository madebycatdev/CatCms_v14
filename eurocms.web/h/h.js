var is = {
	ua: navigator.userAgent.toLowerCase(),
	tArray: [],
	fArray: [],
	
	browser: function (trueArray, falseArray){
		is.tArray.length=0;
		is.fArray.length=0;
		for (var i=0; i<trueArray.length; i++){
			is.ua.search(trueArray[i])!=-1 ? is.tArray.push(1) : is.tArray.push(0);
		}
		for (var j=0; j<falseArray.length; j++){
			is.ua.search(falseArray[j])==-1 ? is.fArray.push(0) : is.fArray.push(1);
		}
		return ((is.tArray.inArray(0) ? 0 : 1) && (is.fArray.inArray(0) ? 1 : 0));
	},
	
	debug:function(){
		return is.ua;
	}
};

Array.prototype.inArray = function(value){
	var i;
	for (i=0;i<this.length;i++) {
		if (this[i]===value) return true;
	}
	return false;
};

var cook = {
	set: function(name, value, expr){
		if(expr){
			var date = new Date();
			date.setTime(date.getTime() + (expr * 24 * 60 * 60 * 1000));
			var expires=";expires="+date.toGMTString();
		}
		
		else var expires="";
		document.cookie = name + "=" + value + expires + "; path=/";
	},

	get: function(name){
		var nameEQ = name + "=";
		var ca = document.cookie.split(';');
		for(var i=0; i<ca.length; i++){
			var c = ca[i];
			while(c.charAt(0)==' ') c = c.substring(1,c.length);
			if(c.indexOf(nameEQ)==0) return c.substring(nameEQ.length,c.length);
		};
		return null;
	},
	
	burn: function(name){
		(this.get(name)) ? (this.set(name,"",-1)) : void(0);
	}
};

var externalLinks = function(){
	if(!document.getElementsByTagName) return;
	var anchors=document.getElementsByTagName("a");
	for(var i=0;i<anchors.length;i++){
		var anchor=anchors[i];
		if (anchor.getAttribute("href") && anchor.getAttribute("rel")=="external"){
			anchor.target="_blank";
		}
	}
};

var winopen = function (obj){
	!obj.name ? obj.name = "newwindow" : null;
	if (obj.centerscreen == true) {
		obj.left = (screen.width - obj.width) / 2;
		obj.top = (screen.height - obj.height) / 2;
	}
	var newWindow = window.open(obj.url, obj.name, "width = " + obj.width + ", height = " + obj.height + ", left =" +  obj.left + ", top = " + obj.top + ", titlebar = " + obj.titlebar + ", menubar = " + obj.menubar + ", toolbar = " + obj.toolbar + ", location = " + obj.location + ", scrollbars = " + obj.scrollbars + ", status = " + obj.status + ", resizable = " + obj.resizable);
	obj.focus ? newWindow.focus() : void(0);
	return true;
};

var __constructTitles = function(){
	if(typeof sIFR == "function"){
		sIFR.replaceElement(named({sSelector:".block h2", sFlashSrc:"/i/Assets/addons/siemens.swf", sColor:"#666666", sBgColor:"#ffffff", sWmode:"transparent"}));
		sIFR.replaceElement(named({sSelector:".block h3", sFlashSrc:"/i/Assets/addons/siemens.swf", sColor:"#666666", sBgColor:"#ffffff", sWmode:"transparent"}));
		sIFR.replaceElement(named({sSelector:"#content-padding .portlet h3", sFlashSrc:"/i/Assets/addons/siemens.swf", sColor:"#333333", sBgColor:"#ffffff", sWmode:"transparent"}));
		//sIFR.replaceElement(named({sSelector:"#context-padding .portlet h3", sFlashSrc:"/i/Assets/addons/siemens.swf", sColor:"#333333", sBgColor:"#ffffff", sWmode:"transparent"}));
	}
};

var animateVisualHeader = function(){
	if($('header-visual') && $('header-visual-text') && $('header-visual-img')){
		var htext = $('header-visual-text').childNodes[0].nodeValue.replace('&','##and##');
		var himg = $('header-visual-img');

		var swfFile;
		var htext2 = $('header-visual-text-2') ? $('header-visual-text-2').childNodes[0].nodeValue.replace('&','##and##') : "";
		var htext3 = $('header-visual-text-3') ? $('header-visual-text-3').childNodes[0].nodeValue.replace('&','##and##') : "";
		
		var flashParams = "imgPath=" + himg.src + "&pageName=" + htext + "&header=" + htext2; // + "&txt=" + htext3;
		
		if(himg.width==612){
			if(himg.height==324){
				swfFile ="/i/Assets/addons/headline-612x324.swf";
				flashParams = flashParams + "&txt=" + htext3;
			}
			if(himg.height==90){
				swfFile ="/i/Assets/addons/headline-612x90.swf";
			}
		} else if(himg.width==848){
			swfFile ="/i/Assets/addons/headline-848x90.swf" ;
		}
		var FO = {
					movie: swfFile + "?" + flashParams, 
					width: himg.width, 
					height: himg.height, 
					majorversion:"7", 
					build:"0", 
					xi:"true", 
					menu:"false", 
					quality:"best", 
					flashvars: flashParams, 
					wmode:"transparent", 
					setcontainercss:"true"
				};
		UFO.create(FO, "header-visual");
	}
};

var getURL = function (o){
	var selectedIndexValue = o.options[o.selectedIndex].getAttribute('value');
	if(selectedIndexValue && selectedIndexValue.length>0){
		(selectedIndexValue.indexOf('http://')>0 || selectedIndexValue.indexOf('https://')>0) ? window.open(selectedIndexValue, _blank) : window.location.href = selectedIndexValue;
		return true;
	}
	return false;
};

var reposFooter = function(){
	if($('footer-padding')){
		var wh = window.getHeight();
		var wsh = window.getScrollHeight();
		var wsx = window.getScrollWidth();
		
		is.browser(['msie', '6.0'], ['linux']) ? wh=wh+4 : void(0);
		
		if(wsh<=wh){
			if(is.browser(['msie'], ['linux'])){
				$('content-padding') ? $('content-padding').style.height = (wh - 144) + "px" : void(0);
			}else{
				if($('footer-padding')){
					$('footer-padding').setStyles({
						'position': 'absolute',
						'width': 594 + "px",
						'top': (wh -236) + "px"
					});
				}
			}
		} else {
			if($('footer-padding')){
				$('footer-padding').setStyles({
					'position': 'relative',
					'width': 594 + "px",
					'top': 0 + "px"
				});
			}
		}
	}
};

var colorizeTable = function(){
	var tableArray = $ES('.table');
	for (var i=0; i < tableArray.length; i++) {
		var thArray = $ES('thead tr th', tableArray[i]);
		var eachWidth = 576 / (thArray.length);
		for (var j=0; j < thArray.length; j++) {
			thArray[j].style.width = eachWidth + "px";
		};
	};
	return true;
};

var getBack = function(){
	var backLink = $$('.back');
	(backLink.length==1 && (!backLink[0].href || !backLink[0].href.length>0)) ? backLink[0].href="javascript: history.go(-1);" : void(0);
};

var setContent = function (aid){
	var containerdiv = $('org-active-content').setStyles({
		display:'block',
		opacity: 0
	});
	var FXOut = new Fx.Style(
			containerdiv, 
			'opacity', {
				duration:1000, 
				onComplete:function(){
					$('org-active-content').innerHTML = $('cont-' + aid).innerHTML;
					var FXIn = new Fx.Style(
						containerdiv, 
						'opacity', {
							duration: 1500, 
							onComplete:reposFooter()
						}
					).start(0, 1);
				}
			}
		).start(1, 0);
}

function ShowSignUp(){
	winopen({url:'/i/Assets/signup.html', centerscreen:true, focus:true, name:'winstf', width:780, height:450, toolbar:false, location:true, menubar:false, scrollbars:true, resizable:true, status:true, titlebar:false, left:0, top:0});
};

window.onload = function(){
	animateVisualHeader();
	__constructTitles();
	reposFooter();
	externalLinks();
	colorizeTable();
	getBack();
};

window.onResize = function(){
	reposFooter();
};