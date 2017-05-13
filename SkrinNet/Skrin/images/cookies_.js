nn4	= (document.layers)? true : false;
opera	= (navigator.userAgent.indexOf('Opera') >= 0)? true : false;
ie		= (document.all && !opera)? true : false;
dom		= (document.getElementById && !ie && !opera)? true : false;
	
function platformDetect(){
  if(navigator.appVersion.indexOf("Win") != -1){
    return "Windows";
  }
  else if(navigator.appVersion.indexOf("Mac") != -1){
    return "Macintosh";
  }
  else return "Other";
}
	
function BrowserInfo(){
  this.name = navigator.appName;
  this.codename = navigator.appCodeName;
  this.version = navigator.appVersion.substring(0,4);
  this.platform = navigator.platform;
  if(String(this.platform) == "null"){
  	this.platform = platformDetect();
  }
  this.javaEnabled = navigator.javaEnabled();
  this.screenWidth = screen.width;
  this.screenHeight = screen.height;
}

// Читаем куки
function readCookie(name){
	var cookieValue = "";
	var search = name + "=";
	if(window.document.cookie.length > 0){ 
		offset = window.document.cookie.indexOf(search);
		if (offset != -1){ 
			offset += search.length;
			end = window.document.cookie.indexOf(";", offset);
			if (end == -1) end = window.document.cookie.length;
			cookieValue = unescape(window.document.cookie.substring(offset, end))
		}
	}
	return cookieValue;
}

// записываем куки
function writeCookie(name, value, path, hours){
	var expire = "";
	if(path != null){
		path = "; path=" + path;
	}
	if(hours != null){
		expire = new Date((new Date()).getTime() + hours * 3600000);
		expire = "; expires=" + expire.toGMTString();
	}
	window.document.cookie = name + "=" + escape(value) + path + expire;
}

function deleteCookie(name, path){
	if(path != null){
		path = "; path=" + path;
	}
	window.document.cookie = name + "=" + path + "; expires=Thu, 01-Jan-70 00:00:01 GMT;";
}
