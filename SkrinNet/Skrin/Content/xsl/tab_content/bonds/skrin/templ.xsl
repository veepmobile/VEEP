<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">

	<xsl:import href="../../../../xsl/common.xsl"/>
	<msxsl:script language="JScript" implements-prefix="js">
		function GetMult(nl){
		<!--var val=String(nl.nextNode().text);-->
    var val=nl.toUpperCase();
    var retval="0"
    var mult,exp;
    if(val.indexOf("E") &lt; 0){
		retval=val
		}else{
		mult=val.substring(0,val.indexOf("E"));
		exp=val.substring(val.indexOf("E")+1,val.length);
		exp=Math.pow(10,exp);
		retval=mult*exp;
		}
		return retval;
		}
	</msxsl:script>
		<xsl:output method="html" version="4.0" encoding="utf-8"/>
	<xsl:decimal-format name="buh" decimal-separator="." grouping-separator=" " NaN="-"/>

	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="iss_profile">
		<!-- content -->

		<xsl:apply-templates select="profile">
		</xsl:apply-templates>


		<!-- end content -->
	</xsl:template>
	<xsl:template match="profile"><input type="hidden" id="iss"><xsl:attribute name="value"><xsl:value-of select="//@iss"/></xsl:attribute></input>
    <input type="hidden" id="tab_id"><xsl:attribute name="value"><xsl:value-of select="//@id"/></xsl:attribute></input>
		<br/>
		
		
		<table width="100%" cellpadding="0" cellspacing="0" border="0">
			<thead>
				<tr>
					<td class="table_caption" colspan="2">Дата государственной регистрации</td>
					<td class="table_caption">Государственный регистрационный номер</td>
					<td class="table_caption">Вид ценной бумаги</td>
					<td class="table_caption">Номинальная стоимость каждой ценной бумаги выпуска</td>
					<td class="table_caption">Количество ценных бумаг, подлежавших размещению, шт.</td>
					<td class="table_caption">Количество в обращении ценных бумаг, шт.</td>
					<td class="table_caption">Способ размещения</td>
					<td class="table_caption">Текущее состояние ценных бумаг выпуска</td>
					<td class="table_caption">Дата погашения</td>
					<td class="table_caption">Срок погашения</td>
					<td class="table_caption">Кол-во купонов</td>

				</tr>
			</thead>
			<tbody>
				<xsl:if test="//showall/@val=1">
					<tr>
						<td colspan="12" style="text-align:right;">
							<span val="0" onclick="switch_vis();" style="cursor:pointer;color:#003399;" id="switcher">
								Показать аннулированные
							</span>
							<img src="/images/tra_e.png" alt="" style="padding-left:3px" id="img"/>
						</td>
					</tr>
				</xsl:if>

				<xsl:for-each select="issues">
					<tr style="cursor:pointer;">
						<xsl:attribute name="bgcolor">
							<xsl:call-template name="set_bg">
								<xsl:with-param name="str_num" select="position()+1"/>
							</xsl:call-template>
						</xsl:attribute>
						<xsl:attribute name="class">
							<xsl:if test="@ia=0">trclosed</xsl:if>
						</xsl:attribute>
						<td valign="top" onclick="void(0);">
							<input class="system_form" type="checkbox" onclick="ch_button()">
								<xsl:attribute name="id">ia<xsl:value-of select="position()"/></xsl:attribute>
								<xsl:attribute name="value"><xsl:value-of select="@id"/></xsl:attribute>
							</input>
						</td>
						<td align="center" valign="top">
							<xsl:value-of select="@rd"/>
						</td>
						<td align="center" valign="top" class="ahref">
              <xsl:attribute name="style">color:#003399;text-decoration:underline;cursor:pointer;</xsl:attribute>
							<xsl:attribute name="onclick">
								ShowAction('<xsl:value-of select="@id"/>','<xsl:value-of select="//@iss"/>',event)
							</xsl:attribute>
							<xsl:value-of select="@reg_no"/>
						</td>
						<td align="center" valign="top">
							<xsl:value-of select="@stype"/>
						</td>
						<td align="right" valign="top">
							<xsl:value-of select="format-number(js:GetMult(string(@face_value)),'# ##0.###########################','buh')"/> <!--руб.-->
                            &#160;<xsl:value-of select="@curr_name"/>
						</td>
						<td valign="top"  class="numbers" align="right">
							<xsl:choose>
								<xsl:when test="contains(@shares_declared,'/')">
									<table  cellpadding="0" cellspacing="0" border="0"  align="right">
										<tr>
											<td rowspan="2" class="numbers" style="padding-right:3px;border:none;">
												<xsl:value-of select="format-number(substring-before(@shares_declared,' '),'# ##0','buh')"/>
											</td>
											<td class="numbers">
												<xsl:value-of select="format-number(substring-before(substring-after(@shares_declared,' '),'/'),'# ##0','buh')"/>
											</td>
										</tr>
										<tr>
											<td style="border-top:solid 1px #000000" class="numbers">
												<xsl:value-of select="format-number(substring-after(@shares_declared,'/'),'# ##0','buh')"/>
											</td>
										</tr>
									</table>
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="format-number(@shares_declared,'# ##0.################','buh')"/>
								</xsl:otherwise>
							</xsl:choose>
						</td>
						<td valign="top"  class="numbers" align="right">
							<xsl:choose>
								<xsl:when test="contains(@shares_rolling,'/')">
									<table cellpadding="0" cellspacing="0" border="0" align="right">
										<tr>
											<td rowspan="2" class="numbers" style="padding-right:3px;">
												<xsl:value-of select="format-number(substring-before(@shares_rolling,' '),'# ##0','buh')"/>
											</td>
											<td class="numbers">
												<xsl:value-of select="format-number(substring-before(substring-after(@shares_rolling,' '),'/'),'# ##0','buh')"/>
											</td>
										</tr>
										<tr>
											<td style="border-top:solid 1px #000000" class="numbers">
												<xsl:value-of select="format-number(substring-after(@shares_rolling,'/'),'# ##0','buh')"/>
											</td>
										</tr>
									</table>
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="format-number(@shares_rolling,'# ##0.################','buh')"/>
								</xsl:otherwise>
							</xsl:choose>
						</td>
						<td align="center" valign="top" >
							<xsl:value-of select="@pt_name"/>
						</td>
						<td align="center" valign="top">
							<xsl:value-of select="@ps_name"/>
						</td>
						<td align="center" valign="top">
							<xsl:value-of select="@red"/>
						</td>
						<td align="center" valign="top">
							<xsl:value-of select="@redemption_end_period"/>
						</td>
						<td align="center" valign="top">
							<xsl:choose>
								<xsl:when test="@coupons=0">
									-
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="@coupons"/>		
								</xsl:otherwise>
							</xsl:choose>
							
						</td>
				</tr>
				</xsl:for-each>
				
			</tbody>
		</table>
		<input type="button" id="shbc"  class="btns dis" style="width:250px;" value="Посмотреть выбранные выпуски"><xsl:attribute name="onclick">ShowAction(-1,"<xsl:value-of select="//@iss"/>",event)</xsl:attribute></input>

		<span class="data_comment limitation">
			ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и могут не отражать абсолютную актуальность и достоверность информации. Дополнительную информацию можно получить в разделе
			<a onclick="gotomenu(48);" href="#">«Квартальные отчеты»</a> или <a onclick="gotomenu(32);" href="#">«Существенные факты»</a>
		</span>
		

		<script language="javascript" type="text/javascript">
			<![CDATA[ 
			xls_params={"iss":$("#iss").val(),"module" : "bonds/skrin/","x":Math.random()};
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
			var trclasses=new Array("trclosed","trshow")
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
			function switch_vis(){
				var newClass=trclasses[Math.abs($("#switcher").attr("val")-1)];
				
				$("."+trclasses[$("#switcher").attr("val")]).each(function(i){
					this.className=newClass;
		        });
				$("#switcher").attr("val",Math.abs($("#switcher").attr("val")-1));
				$("#switcher").html((($("#switcher").attr("val")=="1")? "Скрыть аннулированные":"Показать аннулированные"))
				$("#img").attr("src",(($("#switcher").attr("val")=="1")? "/images/tra_w.png":"/images/tra_e.png"));

				
			}
			function ShowAction(id,iss,e){
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
         		$.post("/Message/ActionsSkrin/",{"type_id" : "2","ids" : ids,"iss" : iss}/*,"ww":(ww-10), "wh" : (wh-25)}*/,
						function(data){
            show_dialog({ "content": data, "is_print": true,"extra_style":"width:900px;" });
						hideClock();
						$( "#dialog_div" ).dialog( "open" );
						}
						,"html");
					}	
			}
			]]>
		</script>
	</xsl:template>
</xsl:stylesheet>
