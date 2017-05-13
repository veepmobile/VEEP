var MAX=1;
var img=new MakeArray(MAX);
opera	= (navigator.userAgent.indexOf('Opera') >= 0)? true : false;
ie		= (document.all && !opera)? true : false;
dom		= (document.getElementById && !ie && !opera)? true : false;
ie4		= (!dom && ie) ? true : false;
nn4		= (document.layers)? true : false;
function MakeArray(n){for(var i=0;i<n;i++){this['i'+i]=0;}this.maxlen=n;this.len=0;return this;}
function Images(org,swap){if(document.images){this.org=new Image();this.org.src=org;this.swap=new Image();this.swap.src=swap;}}
function Add(name,id,hint){	/*img[id]=new Images(name+"_1.gif",name+"_2.gif")*/ void(0);}
function Add_New(name,id,hint){	img[id]=new Images(name+"_1.gif",name+"_2.gif")}
function enter(id){if(document.images){document.images[id].src=img[id].swap.src;}}
function out(id){if (document.images){document.images[id].src=img[id].org.src;}}
function goDate(my,pth) {
if(!document.calfrm) {
}
else
{
	var y,m;
	var path=window.location.href;
	y=document.calfrm.syy.selectedIndex;
	m=document.calfrm.smm.selectedIndex+1;
	m="0"+m
	if (m.length>2){m=m.substr(1,2)}
	
		window.location.href="/" + pth + "/" + (my+y) + "/" + m + "/";
	
}
}
function findObj(theObj, theDoc){
  var p, i, foundObj;
  if(!theDoc) theDoc = document;
  if( (p = theObj.indexOf("?")) > 0 && parent.frames.length){
    theDoc = parent.frames[theObj.substring(p+1)].document;
    theObj = theObj.substring(0,p);
  }
  if(!(foundObj = theDoc[theObj]) && theDoc.all) foundObj = theDoc.all[theObj];
  for (i=0; !foundObj && i < theDoc.forms.length; i++) 
    foundObj = theDoc.forms[i][theObj];
  for(i=0; !foundObj && theDoc.layers && i < theDoc.layers.length; i++) 
    foundObj = findObj(theObj,theDoc.layers[i].document);
  if(!foundObj && document.getElementById) foundObj = document.getElementById(theObj);
  return foundObj;
}
function browseURL(url, money, balance){
	var width="270", height="120";
	var left = (screen.width/2) - width/2;
	var top = (screen.height/2) - height/2;
	var styleStr = 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbar=no,resizable=no,copyhistory=no,width='+width+',height='+height+',left='+left+',top='+top+',screenX='+left+',screenY='+top;
	var msgWindow = window.open("/login/show_docprice.asp?url=" + encodeURI(url) + "&price=" + money,"msgWindow", styleStr);
}

function openInWin(url){
	var width="400", height="500";
	var left = (screen.width/2) - width/2;
	var top = (screen.height/2) - height/2;
	var styleStr = 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,copyhistory=no,width='+width+',height='+height+',left='+left+',top='+top+',screenX='+left+',screenY='+top;
	var msgWindow = window.open(url, "WindowList", styleStr);
}
function openIssuer(ticker){
	var width="1024", height="900";
	var left = (screen.width/2) - width/2;
	var top = (screen.height/2) - height/2;
	var win_name=String(ticker).replace("%21","_").replace("!","_")
	var styleStr = 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no,width='+width+',height='+height+',left='+left+',top='+top+',screenX='+left+',screenY='+top;
	window.open('/issuers/' + ticker + '/', win_name, styleStr);
}
function openPIF(ticker){
	var width="1024", height="900";
	var left = (screen.width/2) - width/2;
	var top = (screen.height/2) - height/2;
	var styleStr = 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no,width='+width+',height='+height+',left='+left+',top='+top+',screenX='+left+',screenY='+top;
	window.open('/pifs/' + ticker + '/', ticker, styleStr);
}
function openPIFMenu(path,ticker){
	var width="1024", height="900";
	var left = (screen.width/2) - width/2;
	var top = (screen.height/2) - height/2;
	var styleStr = 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no,width='+width+',height='+height+',left='+left+',top='+top+',screenX='+left+',screenY='+top;
	window.open(path, ticker, styleStr);
}

function openXLS(ticker,id,period,currency){
    var path="/issuers/XLS/SingleReportXLS.asp?iss=" + ticker + "&menu_id=" + id;
//    if (!isNaN(period)){
//        path += "&arc=" + period;
//    }
    if (period != '')
    {
        path += "&arc=" + period;
    }
    if (!isNaN(currency)){
        path += "&curr=" + currency
    }
	location.href=path;
	
}
function openPRN(path,ticker,period){
    path=(path.length==0)? "/":path;
    if (String(path).substr(String(path).length-2,2)=="//")
        path=String(path).substr(0,String(path).length-1)
       
    path = "/issuers/" + ticker + path + ((isNaN(period))? "prn/": period+"/prn/" ) ;
	var width="1024", height="768";
	var left = (screen.width/2) - width/2;
	var top = (screen.height/2) - height/2;
	var styleStr = 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no,width='+width+',height='+height+',left='+left+',top='+top+',screenX='+left+',screenY='+top;
	var wndprn=window.open(path, "prn_" + ticker, styleStr);
}
function closeShows()
	{
		var divColl = document.all.tags('DIV');
		for (var i = 0; i < divColl.length; i++)
		{
			if (divColl[i].id.substr(0,7) == 'extinfo' ) divColl[i].style.display = 'none';
		}
	}

	function showExtInfo(a)
	{
		closeShows();
		var aa = document.all(a);
		aa.style.display = 'block';
		aa.style.top = event.y;
		aa.style.left = event.x;
		event.cancelBubble = true;
	}
	function ShowFiles(reg,no){
		var wnd=window.open("/viewfiles/default.asp?reg=" + reg+'&no='+no,"wnd","width=640 height=480");
		wnd.focus();
}
function gotoToNeedDate(which,path){

    if(which.period.value != 0){
	    if(which.period.value == -1)
		    location.href = path
	    else
		    location.href = path + which.period.value + "/"
    }
}
function selectCurrency(which){
    writeCookie("curr_ind", which,"/");
    location.reload();
}
function Shownews_old(id,search_text,iss_code){

        var params = {id : id,   ss:search_text, iss:iss_code};
        var width="800", height="600";
    	var left = (screen.width/2) - width/2;
	    var top = (screen.height/2) - height/2;
       	var styleStr = 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no,width='+width+',height='+height+',left='+left+',top='+top+',screenX='+left+',screenY='+top;
       	
        var previewPopup = Form2Popup(params, "/issuers/news/selected.asp", iss_code+"_news", styleStr);
}
function showcalendar(dt){
    if (!document.getElementById("smm")){
        //Календаря еще нет на странице!
        dt="";
    }
    ClearSearchForm();
    $.post("/dbsearch/dbsearchru/news/modules/calendar.asp",{dt:dt},
            function(data){
                document.getElementById("calend").innerHTML=data;
                shownewslist(1);
            }
    )
    
    
}
function show_events_calendar(dt){
    if (!document.getElementById("smm") && dt==""){//Календаря еще нет на странице!
        dt="";
    }
    ClearSearchForm();
    $.post("/dbsearch/dbsearchru/news/events/calendar.asp",{dt:dt},
            function(data){
                document.getElementById("calend").innerHTML=data;
                document.getElementById("day").value=String(dt).substr(8,2);
                showeventslist(1);
            }
    )
    
    
    
}

function goDate(my, pt)
{
 if (typeof(pt) == 'undefined') pt='';
 var yy = $("#syy").val();
 var mm = $("#smm").val();
 
 dt = yy + '-' + mm + '-01'; // + dd;
 ClearSearchForm(document.getElementById("news_search"));
      $.post("/dbsearch/dbsearchru/news/modules/calendar.asp",{dt:dt},
            function(data){
                document.getElementById("calend").innerHTML=data;
                shownewslist(1);
            }
    )
}
function goEventsDate()
{
 var dt=document.getElementById("syy").value + "-" + document.getElementById("smm").value + "-" + document.getElementById("day").value;
 ClearSearchForm(document.getElementById("news_search"));
    $.post("/dbsearch/dbsearchru/news/events/calendar.asp",{dt:dt},
            function(data){
                document.getElementById("calend").innerHTML=data;
                showeventslist(1)
            }
    )
    

}
//***********************************

function showeventslist(pg){
    var params;
    var search=String(document.getElementById("search").value)
    
    var dt=document.getElementById("syy").value + "-" + document.getElementById("smm").value + "-" + document.getElementById("day").value;

    if (search=="1"){
        
        params={isSearch:1, DBeg:Date2Str(Str2Date(getValue("DBeg"),0,0,0),1), DEnd:Date2Str(Str2Date(getValue("DEnd"),0,0,0),1), pg:pg, KW:getValue("KW"), 
                 type_id:getValue("news_type")};
    }else{
        params={isSearch:0, DBeg:dt, DEnd:dt+" 23:59:59", PG:pg, isSearch:0 };
    }    
        showClock()
        $.post("/dbsearch/dbsearchru/news/events/events.asp",params,
            function(data){
                document.getElementById("content").innerHTML=data;
                hideClock();
            }
        )    
    

}

//***********************************




function shownewslist(pg){
    var params;
    var search=String(document.getElementById("search").value)
    
    var dt=document.getElementById("syy").value + "-" + document.getElementById("smm").value + "-" + document.getElementById("day").value;

    if (search=="1"){
        
        params={isSearch:1, DBeg:Date2Str(Str2Date(getValue("DBeg"),0,0,0),1), DEnd:Date2Str(Str2Date(getValue("DEnd"),0,0,0),1), pg:pg, KW:getValue("KW"), RTS_CODE:getValue("news_code"),
        iss_name:getValue("news_issuer"), type_id:getValue("news_type"), list_id: ((document.getElementById("news_iss_list"))? getValue("news_iss_list") : 0)};
    }else{
        params={isSearch:0, DBeg:dt, DEnd:dt+" 23:59:59", PG:pg, isSearch:0 };
    }    
        showClock()
        $.post("/dbsearch/dbsearchru/news/modules/news.asp",params,
            function(data){
                document.getElementById("content").innerHTML=data;
                hideClock();
            }
        )    
    

}
function getObj(obj_id){
    return document.getElementById(obj_id);
}
function getValue(obj_id){
    return document.getElementById(obj_id).value;
}

function NewsSearch(){
    document.getElementById("search").value=1;
    shownewslist(1)
}
function EventsSearch(){
    document.getElementById("search").value=1;
    showeventslist(1)

}

function ClearEventsSearchForm(frm){
    //var frm = document.getElementById("news_search");
    if (typeof(frm) == 'undefined' ) frm = document.news_search;
    for (var i=0; i<frm.elements.length; i++){
        switch(frm.elements[i].tagName){
            case "INPUT" : {frm.elements[i].value="";
            break;
            }
            case "SELECT" : {frm.elements[i].selectedIndex=0;
            break;
            }
        }
        if (frm.elements[i].name=="DBeg" || frm.elements[i].name=="DEnd"){
            frm.elements[i].value=Date2Str(Str2Date("",0,0,0),0);
        }
        
    }
    
}



function ClearSearchForm(frm){
//    var frm=document.getElementById("news_search");
    if (typeof(frm) == 'undefined' ) frm = document.getElementById("news_search");
    for (var i=0; i<frm.elements.length; i++){
        switch(frm.elements[i].tagName){
            case "INPUT" : {frm.elements[i].value="";
            break;
            }
            case "SELECT" : {frm.elements[i].selectedIndex=0;
            break;
            }
        }
        if (frm.elements[i].name=="DBeg" || frm.elements[i].name=="DEnd"){
            frm.elements[i].value=Date2Str(Str2Date("",0,0,0),0);
        }
        
    }
    
}

function showClock(){
    if (document.getElementById("clock_div")){
        document.body.removeChild(document.getElementById("clock_div"));
    }
    var d=document.createElement("div");
    var sch = window.innerHeight ? window.innerHeight : document.documentElement.offsetHeight; 
    var scw = window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth; 
	var left = (scw/2) - 80;
    var top = (sch/2) - 80;
    d.id="clock_div";
    d.style.position="absolute";
    d.style.display="block";
    d.style.left=left + "px";
    d.style.top=top + "px";
    d.style.width="160px";
    d.style.height="160px";
    d.innerHTML='<table width="100%" height="100%"><tr valign="middle"><td align="center"><img src="/images/wait.gif" align="absmiddle"/></td></tr></table>';
    document.body.appendChild(d);
    
}
function hideClock(){
    if (document.getElementById("clock_div")){
        document.body.removeChild(document.getElementById("clock_div"));
    }
}
function Str2Date(cDate,shift_day,shift_month,shift_year){
  var aData=String(cDate).split("-");
  var retDate = new Date()
  if (aData.length<3) {
    //дата кривая, возьмем текущую
    var cDate=new Date();
    retDate.setMonth(cDate.getMonth() + shift_month)
    retDate.setFullYear(cDate.getFullYear() + shift_year)
    retDate.setDate(cDate.getDate() + shift_day)
    
  }else{
    retDate.setMonth(aData[1]*1 + shift_month-1)
    retDate.setFullYear(aData[2]*1 + shift_year)
    retDate.setDate(aData[0]*1 + shift_day)
  }
return retDate  
}
/*
Функция преобразует дату в строку в соответсвии с форматом:
0- dd-mm-yyyy
1- yyyy-mm-dd
*/
function Date2Str(Data,format){
    var retStr=""
    switch(format) {
        case 0 : {retStr=PadL(Data.getDate(),"0",2) + "-" + PadL(Data.getMonth()+1,"0",2) + "-" + Data.getFullYear();
                  break;
        }
        case 1 : {retStr=Data.getFullYear() + "-" + PadL(Data.getMonth()+1,"0",2) + "-" + PadL(Data.getDate(),"0",2);
                  break;
        }
    
    }
return retStr;
}
/*
функция добавляет слева символы ch к строке Str до общей длины nlen
*/
function PadL(Str,ch,nlen){
var addStr=""
for(var i=0; i<nlen; i++) {
    addStr +=String(ch);
}
addStr +=String(Str)
return addStr.substr(addStr.length-nlen,nlen);
}

function Login(){
 if (document.getElementById("login_div")){
        document.body.removeChild(document.getElementById("login_div"));
    }
    var d=document.createElement("div");
    var sch = window.innerHeight ? window.innerHeight : document.documentElement.offsetHeight; 
    var scw = window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth; 
	var left = (scw/2) - 80;
    var top = (sch/2) - 80;
    d.id="login_div";
    d.style.position="absolute";
    d.style.display="block";
    d.style.background="#FFFFFF"
    d.style.border="solid 1px #003399"
    d.style.left=left + "px";
    d.style.top=top + "px";
    d.style.width="400px";
    d.style.height="260px";
    $.post("/cgi/issuer/login.asp",{ispost : 0},
            function(data){
               
               d.innerHTML=data;
            }
    )
    
    document.body.appendChild(d);
}
function loginClose(){
    if (document.getElementById("login_div")){
        document.body.removeChild(document.getElementById("login_div"));
    }
}

function doLoginEv(proc,action,iss){
    var d;
    var inp
    //0 - Первичный вызов, 1 - попытка логина.
    switch(action){
        case 0 :{
                    if (document.getElementById("login_div")){
                        document.body.removeChild(document.getElementById("login_div"));
                    }
                    d=document.createElement("div");
                    var sch = window.innerHeight ? window.innerHeight : document.documentElement.offsetHeight; 
                    var scw = window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth; 
	                var left = (scw/2) - 200;
                    var top = (sch/2) - 130;
                    d.id="login_div";
                    d.style.position="absolute";
                    d.style.display="block";
                    d.style.background="#FFFFFF"
                    d.style.border="solid 1px #003399"
                    d.style.left=left + "px";
                    d.style.top=top + "px";
                    d.style.width="400px";
                    d.style.height="260px";
                    
                    inp=document.createElement("input");
                    inp.id="proc";
                    inp.type="hidden";
                    inp.value=proc;
                    document.body.appendChild(inp);
                    $.post("/cgi/issuer/login.asp",{ispost : 0, iss : iss},
                            function(data){
                               
                               d.innerHTML=data;
                            }
                    )
                    
                    document.body.appendChild(d);
                    break;
        }
        case 1:{
            var frm;
            var arr;
            var params;
            if (document.loginform){
                var d=document.getElementById("login_div");
                frm=document.loginform;
                var info = new BrowserInfo();
                try{
		            frm.storedID.load('namoy');
		            frm.storedID.value =  document.loginform.storedID.getAttribute('p0');
		        }catch(e){
		            frm.storedID.value =  readCookie("storedID");
		        }
		        finally{}
                params={
                    ispost     :    1,
                    myNavigator:    info.name,
		            myCodeName:     info.codename,
		            myVersion:      info.version,
		            myPlatform:     info.platform,
		            myJavaEnabled:  ((info.javaEnabled)? 1:0),
		            myScreenWidth:  info.screenWidth,
		            myScreenHeight: info.screenHeight,
		            path:           frm.path.value,
		            workmode:       ((frm.workmode)? frm.workmode.value : 0),
		            storedID:       frm.storedID.value,
		            user:           frm.user.value,
		            psw:            frm.psw.value
                }
                $.post("/cgi/issuer/login.asp",params,
                        function(data){
                            arr=String(data).split("№");
                            d.innerHTML=arr[0];
                            if(String(arr[1])=="0"){
                                try{
		                            document.getElementById("storedID").setAttribute('p0', arr[2]);
		                            document.getElementById("storedID").save('namoy');
		                        }catch(e){}
		                        finally{}
                                eval(document.getElementById("proc").value);
                                if (document.getElementById("proc")){
                                    document.body.removeChild(document.getElementById("proc"))
                                }
                                
                                if (document.getElementById("login_div")){
                                    setTimeout('document.body.removeChild(document.getElementById("login_div"))',1);
                                }
                            } 
                        }
                        
                )
                
                
            }

        }
    }
}

function openIssuerMenu(path,ticker){
	var width="1024", height="900";
	var left = (screen.width/2) - width/2;
	var top = (screen.height/2) - height/2;
	var styleStr = 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no,width='+width+',height='+height+',left='+left+',top='+top+',screenX='+left+',screenY='+top;
	window.open(path, ticker, styleStr);
}

function try2show(path){
var ticker=document.getElementById("new_ticker").value;
 $.post("/cgi/issuer/checkIssuerExistence.asp",{iss:ticker},
            function(data){
                if (data=="1"){
                    openIssuerMenu("/issuers/" + ticker + path,ticker)
                }else{
                    alert("Некорректный код эмитента")
                }
            }
    )
}
function AccessDenied(e,iss){
    var txt;
    var hg=100;
    if (String(iss)!="undefined"){
        txt="<table cellpadding=\"3\"><tr><td><p align=\"justify\"><img src=\"/images/icon_error.gif\" border=\"0\"/>Для просмотра информации по данной компании необходим уровень доступа СКРИН \"Предприятия\".<br/>" +
            "По вопросам изменения уровня доступа Вы можете связаться с Отделом продаж и маркетинга по телефонам (495) 787-17-67 или " +
	        "<a href=\'/company/support/\'>оставив&nbsp;запрос</a><br/><br/>" +
		    "Бесплатно ознакомиться с Профилем данной компании Вы можете в <a href=\"http://shop.skrin.ru/issuers/" + iss + "/reports/\" target=\"_blank\">Интернет-магазине СКРИН</a></p></td></tr></table><br/><br/>";
		    hg=200

    }else{
        txt="<p align=\"center\"><img src=\"/images/icon_error.gif\" border=\"0\"/>Для просмотра информации в этом разделе вашего уровня доступа недостаточно. Обратитесь к вашему менеджеру.</p>"
    }
    txt +="<div align=\"center\"><img src=\"/images/btn_close.gif\" style=\"cursor:pointer\" onclick=\"document.body.removeChild(document.getElementById('info_div'))\"/></div>"
    if (document.getElementById("info_div")){
        document.body.removeChild(document.getElementById("info_div"));
    }
    d=document.createElement("div");
    var sch = window.innerHeight ? window.innerHeight : document.documentElement.offsetHeight; 
    var scw = window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth; 
    var left = (scw/2) - 200;
    var top = (sch/2) +100;
    d.id="info_div";
    d.style.position="absolute";
    d.style.display="block";
    d.style.background="#FFFFFF"
    d.style.border="solid 1px #003399"
    d.style.left=left + "px";
    d.style.top=top + "px";
    d.style.width="400px";
    d.style.height=hg + "px";
    d.innerHTML=txt;
    document.body.appendChild(d);
    //e.cancelBubble = true;
}
function OnEnterPress(e,proc){
    if (e.keyCode==13){
        eval(proc);
    }
}
/*********************************/
function openTown(ticker){
	var width=screen.width, height=screen.height;
	var left = (screen.width/2) - width/2;
	var top = (screen.height/2) - height/2;
	var win_name=String(ticker).replace("%21","_").replace("!","_")
	var styleStr = 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no,width='+width+',height='+height+',left='+left+',top='+top+',screenX='+left+',screenY='+top;
	window.open('/gmc/towns/' + ticker + '/' , win_name, styleStr);
}
/*********************************/
function openRegion(ticker){
	var width=screen.width, height=screen.height;
	var left = (screen.width/2) - width/2;
	var top = (screen.height/2) - height/2;
	var win_name=String(ticker).replace("%21","_").replace("!","_")
	var styleStr = 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no,width='+width+',height='+height+',left='+left+',top='+top+',screenX='+left+',screenY='+top;
	window.open('/gmc/regions/' + ticker + '/' , win_name, styleStr);
}
/**********************************/

//Функция возвращает ширину клиентской области окна
function getViewportWidth() {
    if (window.innerWidth) {//Все броузеры, кроме IE
        return window.innerWidth;
    }
    else if (document.documentElement && document.documentElement.clientWidth) {//для IE 6 и документов с объявлением DOCTYPE
        return document.documentElement.clientWidth;
    }
    else {
        return document.body.clientWidth;
    }
}

var txtIss;
function doEGRUL(iss, user_id, isFromStatus) {
    txtIss = iss;
    var d=document.createElement("div");
    var bounds;
    if (isFromStatus) {
        bounds = $("#egrstate_get").position();
        d.style.top = bounds.top + $("#egrstate_get").height()+ 4 + "px";
        //d.style.left = bounds.left + "px";
        d.style.right = (getViewportWidth() - bounds.left - $("#egrstate_get").outerWidth()) + "px";
        //d.style.border = "solid black 1px";
        d.style.backgroundImage = "url(/images/button_back.gif)";
        d.style.backgroundRepeat = "repeat-x";
        d.style.backgroundColor = "#DBD8D1";
    }
    else {
        bounds = $("#begrul").position();
        d.style.top = bounds.top + 10 + $("#begrul").height() + "px";
        d.style.left = bounds.left + "px";
        d.style.border = "1px solid #146EC7";
        d.style.backgroundColor = "#fff";
    }
    
    d.className="egrul_selector";
    d.id="degrul";
    document.body.appendChild(d);
    showClock();
    $.get("/iss/modules/getegrul.asp", { "iss": iss, "user_id": user_id, "id": 0 },
        function (data) {
            if (data[0].kod == 0) {
                d.innerHTML = data[0].txt;
                $("html").bind("click", function (e) {
                    if (getObj("degrul")) {
                        document.body.removeChild(getObj("degrul"));
                        $("html").unbind();
                    }
                });
            } else {
                if (getObj("degrul")) {
                    document.body.removeChild(getObj("degrul"));
                    $("html").unbind();
                }
                if (data[0].kod == -1) {
                    showwin("info", "<div style=\"text-align:center;\">Информация по данной компании отсутствует в ЕГРЮЛ.<br/>" +
                                   "Для уточнения запроса предлагаем связаться со специалистами Отдела продаж и маркетинга ЗАО \"СКРИН\"<br/>" +
                                   "телефоны: (495) 787-17-67, e-mail: sales@skrin.ru</div>", 0);
                } else {
                    showwin("info", "<div style=\"text-align:center;\">Сервис заказа Выписок из ЕГРЮЛ временно не доступен, попробуйте зайти позже.", 0);
                }
            }
            hideClock();
        }
    , "json");
    }


/*******************************************/
function showwin(type,text,timeout) {
        var sch = window.innerHeight ? window.innerHeight : document.documentElement.offsetHeight; 
        var scw = window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth; 
        var btn_class="blue"
        if (getObj("span_info")){
            document.body.removeChild(document.getElementById("span_info"));
        }
        if (type=="critical" || type=="warning"){
            btn_class="red";
        }
        
        var s=document.createElement("span");
        s.className="span_info "  + type;
        s.innerHTML="<img src=\"/images/" + type + ".gif\" class=\"img\" height=\"48\" width=\"48\" /><br/>" + text;
        if (timeout==0){
            s.innerHTML += "<br/><br/><div align=\"center\"><input type=\"button\" id=\"span_button\" value=\"Закрыть\" class=\"btns " + btn_class + "\" onclick=\"cancelReport()\" onkeydown=\"cancelReport()\"/></div>"
        } 
        s.style.background="#FFF";
        s.innerHTML +="<br/>";
        s.style.display="block";
        s.id = "span_info"
        s.style.zIndex = "5000";
        document.body.appendChild(s);
        if (timeout==0){
            getObj("span_button").focus();
        }
        var left = (scw/2) - s.offsetWidth/2;
        var top = (sch/2) - s.offsetHeight/2;
        s.style.left=left + "px";
        s.style.top=top + "px";
        if(timeout>0){
            window.setTimeout("document.body.removeChild(document.getElementById('span_info'))",timeout);
        }
        
        
    }
/************************************/    
function closeSpan(){
    if (getObj("span_info")){
        document.body.removeChild(document.getElementById("span_info"))
    }    
}
var stat = "-1";
var scanInt;
var txtOgrn;
var txtInn;
function getReport(ogrn,inn, rep_ready) {
    ogrn = ogrn == "null" ? "" : ogrn;
    if(rep_ready==1){
        //location.href = "/egrulgen/reportAll.asp?ogrn=" + ogrn + "&inn=" + inn;
        window.open("/egrulgen/reportAll.asp?ogrn=" + ogrn + "&inn=" + inn);
        location.href = "/issuers/"+txtIss+"/";

    }else{
        txtOgrn = ogrn;
        txtInn = inn;
        showwin('info', '<span id=\"lblError\" style=\"color:#000;\">Формируется Выписка из ЕГРЮЛ, пожалуйста, подождите</span><br /><div id=\"rotor\" style=\"text-align:center;padding-top:20px;\"><img src="/images/wait.gif" align="absmiddle"/></div>', 0);
        scanInt = setInterval(checkReport, 5000);
        checkReport();
    }    
}


function checkReport() {
    if(getObj("span_button")){        
        getObj("span_button").value="Отмена";
     }   
    $.get("/egrulgen/checkreportAll.asp", { "ogrn": txtOgrn,"inn":txtInn },
    function (data) {
        stat = data[0].status;
        switch (data[0].status) {
             case "4":
                {
                    if(scanInt)
                        clearInterval(scanInt);
                    errorMessage("Выписка из ЕГРЮЛ по данной компании временно не доступна, попробуйте зайти позже.");
                    getObj("rotor").innerHTML=""
                    getObj("span_button").value="Закрыть"
                    break;
                }
            case "3":
                {
                    if(scanInt)
                        clearInterval(scanInt);
                    errorMessage("Сервис заказа Выписок из ЕГРЮЛ временно не доступен, попробуйте зайти позже.");
                    getObj("rotor").innerHTML=""
                    getObj("span_button").value="Закрыть"
                    break;
                }

            case "2":
                {
                    if(scanInt)
                        clearInterval(scanInt);
                    errorMessage("Сведения о юридическом лице в базе ЕГРЮЛ не найдены.");
                    getObj("rotor").innerHTML=""
                    getObj("span_button").value="Закрыть"
                    break;
                }
            case "5":
                {
                    if (scanInt)
                        clearInterval(scanInt);
                    errorMessage("Сведения о юридическом лице в базе ЕГРЮЛ не найдены.");
                    getObj("rotor").innerHTML = ""
                    getObj("span_button").value = "Закрыть"
                    break;
                }
            case "1":
                {
                    errorMessage("Формируется Выписка из ЕГРЮЛ, пожалуйста, подождите");
                    break;
                }
            case "0":
                {
                   
                    closeSpan();
                    if (scanInt)
                        clearInterval(scanInt);
                    //location.href="/egrulgen/reportAll.asp?ogrn=" + txtOgrn+"&inn="+txtInn;
                    window.open("/egrulgen/reportAll.asp?ogrn=" + txtOgrn + "&inn=" + txtInn);
                    location.href = "/issuers/" + txtIss + "/";
                }
        }
    }, "json");
}

function errorMessage(message) {
    $("#lblError").text(message);
}

function errorMessageHtml(message) {
    $("#lblError").html(message);
}

function cancelReport() {
    if (scanInt)
        clearInterval(scanInt);
    if (massregInt)
        clearInterval(massregInt);
    closeSpan();
}
function showFNDRS(iss){
        var params = {id : iss};
        var width="1024", height="768";
    	var left = (screen.width/2) - width/2;
	    var top = (screen.height/2) - height/2;
       	var styleStr = 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no,width='+width+',height='+height+',left='+left+',top='+top+',screenX='+left+',screenY='+top;
        var FNDRSPopup = Form2Popup(params, "/issuers/all_data/founders/visio.asp", iss+"_fndrs", styleStr);
}

/****/

var txt_ticker;
var q_stat = -1;
var massregInt;

function searchMassReg(iss, user_id) {
    txt_ticker = iss;
    waitMassReg();
    massregInt = setInterval(checkMassReg, 5000);
    checkMassReg();
    
     
}

function waitMassReg() {
    showwin('info', '<span id=\"lblError\" style=\"color:#000;\">Осуществляется поиск адресов массовой регистрации, пожалуйста, подождите</span><br /><div id=\"rotor\" style=\"text-align:center;padding-top:20px;\"><img src="/images/wait.gif" align="absmiddle"/></div>', 0);
}

function checkMassReg() {
    var req = '/egrulgen/proxy_massreg.asp';
    $.get(req, { "iss": txt_ticker, "rnd": Math.random()  }, function (data) {
        q_stat = data.QStatus;
        switch (q_stat) {
            case 0:
                {
                    if (massregInt)
                        clearInterval(massregInt);
                    var searchResult = data.SearchResult;
                    var origAddress = data.OriginalAddress;
                    var message_text = "";
                    closeSpan();
                    if (searchResult == "1") {
                        var street = data.Street;
                        var address_row = "";
                        var i_count = 0;
                        $.each(data.Items, function (entryIndex, entry) {
                            var address = "";
                            var b_color = "#f0f0f0";
                            if (i_count % 2 == 0)
                                b_color = "#ffffff";
                            var house = entry['House'];
                            if (house != null) {
                                house = "дом " + house;
                                address += house;
                            }
                            var corp = entry['Korpus'];
                            if (corp != null) {
                                corp = " корпус " + corp;
                                address += corp;
                            }
                            var flat = entry['Flat'];
                            if (flat != null) {
                                flat = " кв. " + flat;
                                address += flat;
                            }
                            var rcount = entry['RegCount'];
                            i_count++;
                            address_row += '<tr bgcolor="' + b_color + '"><td style="padding-left:5px">' + street + ' ' + address + '</td><td align="center">' + rcount + '</td></tr>';
                        });
                        message_text = '<p><span class="bluecaption">Адрес по которому искали: ' + origAddress + '</span></p>';
                        message_text += '<span class="caption">Адреса массовой регистрации</span><hr>';
                        message_text += '<table width="100%" cellspacing="0" cellpadding="0"><tr>';
                        message_text += '<tr><td class="table_caption" style="text-align:center;">Адрес</td><td class="table_caption" style="text-align:center;">Кол-во</td></tr>';
                        message_text += '<tr><td class="table_shadow"><div style="width: 1px; height: 1px;"><spacer height="1px" width="1px" type="block"></spacer></div></td>';
                        message_text += '<td class="table_shadow"><div style="width: 1px; height: 1px;"><spacer height="1px" width="1px" type="block"></spacer></div></td></tr>';
                        message_text += address_row;
                        message_text += '</table>';
                        ShowTable(message_text);
                    }
                    if (searchResult == "0") {
                        message_text = '<p><span class="bluecaption">Адрес по которому искали: ' + origAddress + '</span></p>';
                        message_text += '<span class="caption">Адреса массовой регистрации</span><hr>';
                        message_text += "По адресу не  найдены  записи массовых регистраций";
                        ShowTable(message_text);
                    }
                    
                    break;
                }
            case 1:
                {
                    errorMessage("Осуществляется поиск адресов массовой регистрации, пожалуйста, подождите");
                    break;
                }
            case 2:
                {
                    if (massregInt)
                        clearInterval(massregInt);
                    errorMessage("Недостаточно информации.");
                    getObj("rotor").innerHTML = ""
                    getObj("span_button").value = "Закрыть"
                    break;
                }
            case 3:
                {
                    if (massregInt)
                        clearInterval(massregInt);
                    errorMessage("Сервис временно не доступен, попробуйте зайти позже.");
                    getObj("rotor").innerHTML = ""
                    getObj("span_button").value = "Закрыть"
                    break;
                }
            case 4:
                {
                    if (massregInt)
                        clearInterval(massregInt);
                    errorMessage("Недостаточно информации.");
                    getObj("rotor").innerHTML = ""
                    getObj("span_button").value = "Закрыть"
                    break;
                }
            case 5:
                {
                    if (massregInt)
                        clearInterval(massregInt);
                    errorMessage("Недостаточно информации.");
                    getObj("rotor").innerHTML = ""
                    getObj("span_button").value = "Закрыть"
                    break;
                }
            case 6:
                {
                    if (massregInt)
                        clearInterval(massregInt);
                    errorMessage("Сервис временно не доступен, попробуйте зайти позже.");
                    getObj("rotor").innerHTML = ""
                    getObj("span_button").value = "Закрыть"
                    break;
                }
            case 7:
                {
                    if (massregInt)
                        clearInterval(massregInt);
                    errorMessage("Недостаточно информации.");
                    getObj("rotor").innerHTML = ""
                    getObj("span_button").value = "Закрыть"
                    break;
                }
        }
    }, "json");
}

function ShowTable(tabletext) {
    var sch = window.innerHeight ? window.innerHeight : document.documentElement.offsetHeight;
    var scw = window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth;

    var current_date = new Date();
    var month = ["01","02","03","04","05","06","07","08","09","10","11","12"];
    var today_date = current_date.getDate() + "." + month[current_date.getMonth()] + "." + current_date.getFullYear() 

    var inner_html = '<table id="dtable" width="100%" cellspacing="0" cellpadding="0" class="dragg_div_top" style="-moz-user-select: none;"><tbody><tr><td><img onclick="doPrint(&quot;news_text&quot;,&quot;Адреса массовой регистрации&quot;);" alt="Печать" class="prn_img" src="/images/printer.gif"></td><td align="right" class="dragg_div_top" style="-moz-user-select: none;"><img style="cursor:default;" alt="Закрыть" onclick="closetable();" src="/images/close_div.gif"></td></tr></tbody></table>'
    inner_html += '<div id="news_text" class="news_text">' + tabletext + '<hr><br>';
    inner_html += '<table width="100%" cellspacing="0" cellpadding="4" border="0"><tbody><tr><td width="16" bgcolor="#FFFFFF"><img width="16" border="0" height="16" src="/images/icon_information.gif"></td><td width="50%">Источник данных: ЕГРЮЛ.</td><td width="50%" align="right">Данные на ' + today_date + '</td></tr></tbody></table></div>';

    if (!getObj("news_div")) {
        d = document.createElement("div");
        d.id = "news_div";
        d.style.zIndex = "600";
        d.style.display = "block";
        d.style.position = "absolute";
        d.style.background = "#FFFFFF"
        //d.style.height = "380px";
        d.style.width = "640px";
        d.style.border = "solid 1px #003399";
        document.body.appendChild(d);
        $('#news_div').html(inner_html);
        var left = (scw / 2) - d.offsetWidth / 2;
        var top = (sch / 2) - d.offsetHeight / 2;
        d.style.left = left + "px";
        d.style.top = top + "px";
        $('#news_div').Draggable({ handle: "#dtable", addClasses: false });
    } else {
        d = getObj("news_div")
        //$('#news_div').html(data[0].text);
    }
}

function closetable() {
    var d = document.getElementById("news_div");
    if (d) {
        document.body.removeChild(d);
    }
}
function doPrint(id,title) {
    $("#" + id).printElement({ pageTitle: title + ".html", leaveOpen: false,
        printMode: 'popup'
    });
}

function remInputs(html) {
    var re = new RegExp("<input\/?[^>]+(>|$)", "ig")
    return html.replace(re, "");
} 

