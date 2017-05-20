<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">

	<xsl:import href="../../../xsl/common.xsl"/>
	<msxsl:script language="JScript" implements-prefix="js">
		function GetMult(nl){
		var val=String(nl.nextNode().text);
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
		<div class="data_comment" xmlns:user="urn:deitel:user" xmlns:msxsl="urn:schemas-microsoft-com:xslt">
			Данные от <xsl:value-of select="//ld/@val"/>
		</div>

		<span class="subcaption">Выпуски облигаций</span>
		<br/>

		<table width="100%" cellpadding="0" cellspacing="0" border="0">
			<thead>
				<tr>
					<td class="table_caption" colspan="2">Дата государственной регистрации</td>
					<td class="table_caption">Государственный регистрационный номер</td>
					<td class="table_caption">Вид ценной бумаги</td>
					<td class="table_caption">Номинальная стоимость каждой ценной бумаги выпуска</td>
					<td class="table_caption">Количество ценных бумаг, подлежавших размещению, шт.</td>
					<td class="table_caption">Количество размещенных ценных бумаг, шт.</td>
					<td class="table_caption">Способ размещения</td>
					<td class="table_caption">Состояние ценных бумаг выпуска</td>
					<td class="table_caption">Расчетная дата  погашения </td>
					<td class="table_caption">Период обращения</td>
					<td class="table_caption">Кол-во купонов</td>

				</tr>
			</thead>
			<tbody>
				<tr>
					<td class="table_shadow" colspan="2">
						<div style="width: 1px; height: 1px;">
							<spacer type="block" width="1px" height="1px" />
						</div>
					</td>
					<td class="table_shadow" colspan="2">
						<div style="width: 1px; height: 1px;">
							<spacer type="block" width="1px" height="1px" />
						</div>
					</td>
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
					<td class="table_shadow">
						<div style="width: 1px; height: 1px;">
							<spacer type="block" width="1px" height="1px" />
						</div>
					</td>
				</tr>

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
				
				<xsl:for-each select="bonds">
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
								<xsl:attribute name="value"><xsl:value-of select="@code_nsd"/></xsl:attribute>
							</input>
						</td>
						<td align="center" valign="top">
							<xsl:value-of select="@rd"/>
						</td>
						<td align="center" valign="top" class="ahref">
							<xsl:attribute name="onclick">
								ShowAction('<xsl:value-of select="@code_nsd"/>','<xsl:value-of select="//@iss"/>',event)
							</xsl:attribute>
							<xsl:value-of select="@state_reg_number"/>
						</td>
						<td align="center" valign="top">
							<xsl:value-of select="@stype"/>
						</td>
						<td align="right" valign="top">
							<xsl:value-of select="format-number(js:GetMult(@face_value),'# ##0.###########################','buh')"/> <!--руб.-->
							&#160;<xsl:value-of select="@currency_code"/>
						</td>
						<td valign="top"  class="numbers" align="right">
									<xsl:value-of select="format-number(@issue_size_planned,'# ##0.################','buh')"/>
						</td>
						<td valign="top"  class="numbers" align="right">
									<xsl:value-of select="format-number(@issued_size,'# ##0.################','buh')"/>
						</td>
						<td align="center" valign="top" >
							<xsl:value-of select="@ptname"/>
						</td>
						<td align="center" valign="top">
							<xsl:value-of select="states/@sec_state_name"/>
						</td>
						<td align="center" valign="top">
							<xsl:value-of select="@edp"/>
						</td>
						<td align="center" valign="top">
							<xsl:value-of select="@live_period"/>
						</td>
						<td align="center" valign="top">
							<xsl:choose>
								<xsl:when test="@coupons_number=0">
									-
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="@coupons_number"/>		
								</xsl:otherwise>
							</xsl:choose>
							
						</td>
				</tr>
				</xsl:for-each>
				
			</tbody>
		</table>
		<input type="button" id="shbc"  class="btns dis" style="width:250px;" value="Посмотреть выбранные выпуски"><xsl:attribute name="onclick">ShowAction(-1,"<xsl:value-of select="//@iss"/>",event)</xsl:attribute></input>

		<span class="data_comment limitation">
			ВНИМАНИЕ: в связи с особенностями функционирования и обновления указанного источника информации АО "СКРИН" не может гарантировать абсолютную актуальность и достоверность данных.
		</span>


		<script language="javascript" type="text/javascript">
			<![CDATA[ 
			xls_params={"iss":$("#iss").val(),"module" : "bonds/skrin/","x":Math.random()};
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
					
					if(ids.length==0){
						showwin("warning","Нет ни одного выбранного выпуска.",2000);
					}else{
						showClock();
						$.post("/iss/modules/papers_selected.asp",{"type_id" : "8","ids" : ids,"iss" : iss}/*,"ww":(ww-10), "wh" : (wh-25)}*/,
						function(data){
						$("#dcontent").html(data);
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
