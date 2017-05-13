<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user">
	<xsl:import href="../../../xsl/common.xsl"/>
	<xsl:output method="html" version="4.0" encoding="utf-8"/>
	<xsl:decimal-format name="buh" decimal-separator="." grouping-separator=" " NaN="#"/>

	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="iss_profile">
		<!-- content -->

		<xsl:apply-templates select="profile">
		</xsl:apply-templates>


		<!-- end content -->
	</xsl:template>
	<xsl:template match="profile">
    <input type="hidden" id="iss">
      <xsl:attribute name="value">
        <xsl:value-of select="//@iss"/>
      </xsl:attribute>
    </input>
    <input type="hidden" id="tab_id">
      <xsl:attribute name="value">
        <xsl:value-of select="//@id"/>
      </xsl:attribute>
    </input>
		<span class="subcaption">Депозитарные расписки</span>
		<br/>
		<table width="98%" cellpadding="0" cellspacing="0">
			<tr>
				<td class="table_caption" colspan="2">Дата допуска к торгам</td>
				<td class="table_caption">Вид ценной бумаги</td>
				<td class="table_caption" >Вид  депонированных ценных бумаг</td>
				<td class="table_caption">Отношение ДР к акциям</td>
				
			</tr>
			<tr>
				<td class="table_shadow" colspan="2">
					<div style="width: 1px; height: 1px;">
						<spacer type="block" width="1px" height="1px" />
					</div>
				</td>
				<td class="table_shadow">
					<div style="width: 1px; height: 1px;">
						<spacer type="block" width="1px" height="1px" />
					</div>
				</td>
				<td class="table_shadow">
					<div style="width: 1px; height: 1px;">
						<spacer type="block" width="1px" height="1px" />
					</div>
				</td>
				<td class="table_shadow">
					<div style="width: 1px; height: 1px;">
						<spacer type="block" width="1px" height="1px" />
					</div>
				</td>
			</tr>
	
			<xsl:for-each select="DR">
				<tr style="cursor:pointer"><xsl:attribute name="bgcolor"><xsl:call-template name="set_bg"><xsl:with-param name="str_num" select="position()"/></xsl:call-template></xsl:attribute>
				<td valign="top" onclick="void(0);">
					<input class="system_form" type="checkbox" onclick="ch_button()">
						<xsl:attribute name="id">ia<xsl:value-of select="position()"/></xsl:attribute>
						<xsl:attribute name="value"><xsl:value-of select="@id"/></xsl:attribute>
					</input>
				</td>
				<td align="center" valign="top">
					<xsl:value-of select="@dt"/>
				</td>
				<td align="center" style="color:#003399;text-decoration:underline;cursor:pointer;" valign="top" class="ahref"><xsl:attribute name="onclick">ShowADR('<xsl:value-of select="@id"/>','<xsl:value-of select="//@iss"/>',event)	</xsl:attribute>
					<xsl:value-of select="DRT/@name"/>
				</td>
				<td align="center" valign="top">
					<xsl:value-of select="DRT/@DR_Depon"/>
				</td>
				<td align="center">
					<xsl:value-of select="DRT/@Otnosh"/>
				</td>
			</tr>
		</xsl:for-each>
	</table>
	<input type="button" id="shbc" onclick="ShowADR(-1,event)" class="btns dis" style="width:250px;" value="Посмотреть выбранные выпуски">
		<xsl:attribute name="onclick">
			ShowADR(-1,'<xsl:value-of select="//@iss"/>',  event)
		</xsl:attribute>
		</input>

    <script language="javascript" type="text/javascript">
      <![CDATA[ 
			 $('#xls_btn').unbind('click').addClass('disabled');
        set_xls_function(function () {
           if (!getObj("reports")) {
               
                ifr = document.createElement("iframe");
                ifr.className = "service_frame"
                ifr.name = "reports"
                ifr.id = "reports"
                ifr.src = "about:blank";
                document.body.appendChild(ifr);
            } else {
                ifr = getObj("reports")
            }
            		
        
            iframepost({"id": $("#tab_id").val(), "ticker":$("#iss").val(), "xls" : "1"}, "/Tab/", "reports");
            });
			$(function() {
					var sch = window.innerHeight ? window.innerHeight : document.documentElement.offsetHeight; 
					var scw = window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth; 
					var	ww,wh,top,left;
					ww=(scw<840)?scw-44:840;
					wh=(sch<640)?sch-30:640
					$( "#dialog_div" ).dialog({
						autoOpen: false,
						height: wh,
						width: ww,
						modal: false,
						resizable: true,
						draggable: true,
						closeOnEscape: true,
						close: function() {
							void(0);
						}
					});
					$("#prnb").unbind()
					$("#prnb").click(function(){
					   doPrint("dcontent");
					})
				})
			function ch_button(){
				if($("input:checkbox:checked").length>0){
					$("#shbc").removeClass("dis");
					$("#shbc").addClass("blue");
				}else{
					$("#shbc").removeClass("blue");
					$("#shbc").addClass("dis");
				}
			}
			function ShowADR(id,iss,e){
			var ids="";
			if(window.event){
					e=window.event;
					e.cancelBubble=true;
			}else{
					e.stopPropagation();
				
			}
			if(String(id)=="-1"){
					$("input:checkbox:checked").each(function(i){
						if(this.id.substring(0,2)=="ia"){
								ids+=this.value + ",";
							}
						})
					}else{
						ids=id;
					}
					if(ids.length<32){
						showwin("warning","Нет ни одного выбранного выпуска.",2000);
					}else{
						showClock();
            $.post("/Message/ActionsSkrin/",{"type_id" : "3","ids" : ids,"iss" : iss},
						function(data){
					  show_dialog({ "content": data, "is_print": true,"extra_style":"width:900px;" });
						hideClock();
						$( "#dialog_div" ).dialog( "open" );
						}
						,"html");
					}	
			}
			/*function ShowADR(id,e){
			
			if (getObj("pap_div")){
					document.body.removeChild(getObj("pap_div"));
				}
				var ids="";
				var top;
				var left;
				var caller;
				if(window.event){
					e=window.event;
					caller=e.srcElement;
				}else{
					caller=e.target;
				}
				
				if(caller.id.substring(0,2)=="ia"){
					if(window.event){
						e.cancelBubble=true;
					}else{
						e.stopPropagation();
				
					}
				}else{
					
					var sch = window.innerHeight ? window.innerHeight : document.documentElement.offsetHeight; 
					var scw = window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth; 
					var ww,wh;
					ww=(scw<640)?scw-44:640;
					wh=(sch<380)?sch-30:380
					top=sch/2-wh/2;
					left=scw/2-ww/2;
					if(String(id)=="-1"){
						$("input:checkbox:checked").each(function(i){
							if(this.id.substring(0,2)=="ia"){
								ids+=this.value + ",";
							}
						})
					}else{
						ids=id;
					}
					if(ids.length<32){
						showwin("warning","Нет ни одного выбранного выпуска.",2000);
					}else{
						showClock();					
         		$.post("/Message/ActionsSkrin/",{"type_id" : "3","ids" : ids,"iss" : iss},
						function(data){
               show_dialog({ "content": data, "is_print": true });
							if (!getObj("pap_div")){
								d=document.createElement("div");
								d.id="pap_div";
								d.style.zIndex="600";
								d.style.display="block";
								d.style.position="absolute";
								d.style.background="#FFFFFF"
								d.style.left=left + "px";
								d.style.top=top + "px";
								d.style.height=wh +"px";
								d.style.width=ww + "px";
								d.style.border="solid 1px #003399";
								document.body.appendChild(d);
								$('#pap_div').html(data);
								$('#pap_div').draggable({handle:	"th", addClasses:false });
							$("#pap_div").click(function(e){
									if (e.stopPropagation) {
										e.stopPropagation();
									}else{
										e.cancelBubble=true;
									}    
								});
								$("html").click(function(e){
									if(getObj("pap_div")){
										document.body.removeChild(getObj("pap_div"));
									}
									$("html").unbind();
								})
							}
							hideClock();
						},
						"html"
						)
				}
			}
			}*/
			]]>
    </script>
	</xsl:template>
</xsl:stylesheet>
