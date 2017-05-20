need_open = readCookie("1stopen");
need_open = ((String(need_open) == "") || ((String(need_open) == "undefined"))) ? 0 : 1;
if(need_open != 1){
	var styleStr = 'toolbar=yes,location=yes,directories=no,status=yes,menubar=yes,scrollbars=yes,resizable=yes,copyhistory=yes,width=400,height=300,left=100,top=100,screenX=100,screenY=100';
	/*var msgWindow = window.open("http://www.skrin.ru","skrinWindow", styleStr);
	window.focus();*/
	writeCookie("1stopen", "1", "/", 12);
}
