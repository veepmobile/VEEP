<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">

	<xsl:import href="../../../xsl/common.xsl"/>
	<msxsl:script language="JScript" implements-prefix="js">
		function GetMult(nl){
		<!--var val=String(nl.nextNode().text);-->
    var val = nl.toUpperCase();
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

		<span class="subcaption">Выпуски облигаций</span>
		<br/>
		
		
		<table cellpadding="0" cellspacing="0" border="0">
			<thead>
				<tr>
					<td class="table_caption" rowspan="3">Дата государ-<br/>ственной регист-<br/>рации</td>
					<td class="table_caption" rowspan="3">Государственный регистрационный номер</td>
					<td class="table_caption" colspan="12">Дата предстоящего события</td>
				</tr>
				<tr>
					<td class="table_caption" colspan="4">Выплаты по Купонам</td>
					<td class="table_caption" colspan="4">Погашение номинальной стоимости (амортизация)</td>
					<td class="table_caption" colspan="4">Предложение о выкупе (Оферта)</td>
				</tr>
				<tr>

								<td class="table_caption" style="width:22px;">№</td>
									<td class="table_caption" style="width:76px;">Расчетная дата /срок окончания купонного периода</td>
									<td class="table_caption" style="width:53px;">Ставка купона в % годо-<br/>вых</td>
									<td class="table_caption" style="width:78px;">Размер выплаты купонного дохода на 1 Ц.Б. (в валюте платежа)</td>
								<td class="table_caption" style="width:22px;">№</td>
								<td class="table_caption" style="width:75px;" >Расчетная дата /срок погашения</td>
								<td class="table_caption" style="width:90px;">Доля в % погашения части номинальной стоимости</td>
								<td class="table_caption" style="width:83px;">Размер погашаемой части</td>
								<td class="table_caption" style="width:22px;">№</td>
								<td class="table_caption" style="width:100px;">Вид обьязательства</td>
					<td class="table_caption" style="width:74px;">
						Дата<br/>наступ-<br/>ления<br/>обязатель-<br/>ства
					</td>
					<td class="table_caption" style="width:67px;">
						Цена приобре-<br/>тения %</td>
				</tr>
			</thead>
			<tbody>
				

				<!--xsl:if test="//showall/@val=1"-->
					<tr>
						<td colspan="14" style="text-align:right;">
							<span val="0" onclick="switch_vis();" style="cursor:pointer;color:#003399;" id="switcher">
								Показать аннулированные
							</span>
							<img src="/images/tra_e.png" alt="" style="padding-left:3px" id="img"/>
						</td>
					</tr>
				<!--/xsl:if-->

				<xsl:for-each select="issues">
					<tr>
						<xsl:attribute name="bgcolor">
							<xsl:call-template name="set_bg">
								<xsl:with-param name="str_num" select="position()+1"/>
							</xsl:call-template>
						</xsl:attribute>
						<xsl:attribute name="class">
							<xsl:if test="PS/@act=0">trclosed</xsl:if>
						</xsl:attribute>
						<xsl:attribute name="id">tr<xsl:value-of select="position()"/></xsl:attribute>
						<td align="center" valign="top">
							<xsl:value-of select="@rd"/>
						</td>
						<td align="center" valign="top" nowrap="nowrap" style="border-left:solid 1px #FFFFFF;">
							<div val="0" style="cursor:pointer;color:#003399;padding-bottom:7px;">
								<xsl:attribute name="onclick">
									ShowAction('<xsl:value-of select="@id"/>','<xsl:value-of select="//@iss"/>',event)
								</xsl:attribute>
								<xsl:value-of select="@reg_no"/>
							</div>
							<xsl:if test="issue_coupons[position()=last()] or issue_repayments[position()=last()] or Issue_Purchases[position()=last()]">
								<span val="0" style="cursor:pointer;color:#003399">
									<xsl:attribute name="id">sw<xsl:value-of select="position()"/></xsl:attribute>
									<xsl:attribute name="onclick">switch_vis_det(<xsl:value-of select="position()"/>)</xsl:attribute>
									Календарь выплат
								</span>
								<img src="/images/tra_e.png" alt="" style="padding-left:3px" >
									<xsl:attribute name="id">img<xsl:value-of select="position()"/></xsl:attribute>
								</img>
								
								
								
						</xsl:if>
						</td>
						<td colspan="4" valign="top" style="border-left:solid 1px #FFFFFF;">
							<table width="100%" cellspacing="0" cellpadding="0" border="0">
								<xsl:attribute name="id">tab_1_<xsl:value-of select="position()"/></xsl:attribute>
								<xsl:choose>
									<xsl:when test="issue_coupons">
										<xsl:for-each select="issue_coupons">
											<tr>
												<xsl:attribute name="class">
													<xsl:if test="@dd=0">trclosed</xsl:if>
												</xsl:attribute>
												<td style="width:27px;text-align:center;border:none;">
													<xsl:value-of select="@no"/>
												</td>
												<td style="width:76px;text-align:center;border:none; " align="center">
													<table align="center" style="border:none;">
														<tr>
															<td style="border-bottom:solid 1px #000000;border-top:none;border-left:none; border-right:none;width:55px;">
																<xsl:value-of select="@d_date"/>
															</td>
														</tr>
														<tr>
															<td style="border:none;">
																<xsl:value-of select="@d_period"/>
															</td>
														</tr>
													</table>
																										</td>
												<td style="width:53px;text-align:right;border:none; ">
													<xsl:value-of select="format-number(@rate,'# ##0.###########################','buh')"/>
												</td>
												<td style="text-align:right; width:78px;border:none;">
													<xsl:value-of select="format-number(@sum_value,'# ##0.###########################','buh')"/>
												</td>
											</tr>
										</xsl:for-each>

									</xsl:when>
									<xsl:otherwise>
										<tr>
											<td colspan="4" align="center" style="border:none;" >Нет данных</td>
										</tr>
									</xsl:otherwise>
								</xsl:choose>
							</table>
						</td>
						<td colspan="4" valign="top" style="border-left:solid 1px #FFFFFF;">

							<table width="100%" cellspacing="0" cellpadding="0" border="0">
								<xsl:attribute name="id">tab_2_<xsl:value-of select="position()"/></xsl:attribute>
								<xsl:choose>
									<xsl:when test="issue_repayments">
										<xsl:for-each select="issue_repayments">
											<tr>
												<xsl:attribute name="class">
													<xsl:if test="@dd=0">trclosed</xsl:if>
												</xsl:attribute>
												<td style="width:22px;text-align:center;border:none;">
													<xsl:value-of select="@no"/>
												</td>
												<td style="width:75px;text-align:center;border:none;" align="center">
													<table align="center">
														<tr>
															<td style="border-bottom:solid 1px #000000;width:55px;border-top:none;border-left:none; border-right:none;">
																<xsl:value-of select="@d_date"/>
															</td>
														</tr>
														<tr>
															<td style="border:none;">
																<xsl:value-of select="@d_period"/>
															</td>		
														</tr>
													</table>
												</td>
												<td style="width:90px;text-align:right;border:none;">
													<xsl:choose>
														<xsl:when test="string(number(@percent)) = 'NaN'">
															<xsl:value-of select="@percent"/>
														</xsl:when>
														<xsl:otherwise>
															<xsl:value-of select="format-number(@percent,'# ##0.###########################','buh')"/>
														</xsl:otherwise>
													</xsl:choose>
												</td>
												<td style="width:83px;text-align:right;border:none;">
													<xsl:value-of select="format-number(@sum_value,'# ##0.###########################','buh')"/>
												</td>
											</tr>
										</xsl:for-each>

									</xsl:when>
									<xsl:otherwise>
										<tr>
											<td colspan="4" align="center" style="border:none;">Нет данных</td>
										</tr>
									</xsl:otherwise>
								</xsl:choose>
							</table>
						</td>
						<td colspan="4" valign="top" style="border-left:solid 1px #FFFFFF;">

							<table cellspacing="0" cellpadding="0" border="0"  width="100%">
								<xsl:attribute name="id">tab_3_<xsl:value-of select="position()"/></xsl:attribute>
								<xsl:choose>
									<xsl:when test="Issue_Purchases">
										<xsl:for-each select="Issue_Purchases">
											<tr>
												<xsl:attribute name="class">
													<xsl:if test="@dd=0">trclosed</xsl:if>
												</xsl:attribute>
												<td style="width:27px;text-align:center;border:none;">
													<xsl:value-of select="@no"/>
												</td>
												<td style="width:100px;border:none;">
													<xsl:value-of select="@pname"/>
												</td>
												<td style="width:74px;text-align:center;border:none" align="center">
													<table align="center" style="border:none;">
														<tr>
															<td style="border-bottom:solid 1px #000000;width:55px;border-top:none;border-left:none;border-right:none;">
																<xsl:value-of select="@d_date"/>
															</td>
														</tr>
														<tr>
															<td style="border:none">
																<xsl:value-of select="@d_period"/>
															</td>
														</tr>
													</table>
												</td>
												<td style="text-align:right; width:67px;border:none;">
													<xsl:choose>
														<xsl:when test="string(number(@rate)) = 'NaN' and string-length(@rate) &gt; 0">
															<xsl:value-of select="@rate"/>
														</xsl:when>
														<xsl:otherwise>
															<xsl:value-of select="format-number(@rate,'# ##0.###########################','buh')"/>
														</xsl:otherwise>
													</xsl:choose>
													
												</td>
											</tr>
										</xsl:for-each>

									</xsl:when>
									<xsl:otherwise>
										<tr>
											<td colspan="4" align="center" style="border:none;">Нет данных</td>
										</tr>
									</xsl:otherwise>
								</xsl:choose>
							</table>
						</td>
						<!--td align="center" valign="top" class="ahref">
							
						</td>
						<td align="center" valign="top">
							<xsl:value-of select="@stype"/>
						</td>
						<td align="right" valign="top">
							<xsl:value-of select="format-number(js:GetMult(@face_value),'# ##0.###########################','buh')"/> 
                            &#160;<xsl:value-of select="@curr_name"/>
						</td>
						<td valign="top"  class="numbers" align="right">
							<xsl:choose>
								<xsl:when test="contains(@shares_declared,'/')">
									<table  cellpadding="0" cellspacing="0" border="0"  align="right">
										<tr>
											<td rowspan="2" class="numbers" style="padding-right:3px;">
												<xsl:value-of select="format-number(substring-before(@shares_declared,' '),'# ##0','buh')"/>
											</td>
											<td class="numbers">
												<xsl:value-of select="format-number(substring-before(substring-after(@shares_declared,' '),'/'),'# ##0','buh')"/>
											</td>
										</tr>
										<tr>
											<td style="border-top:solid 1px #FFFFFF" class="numbers">
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
											<td style="border-top:solid 1px #FFFFFF" class="numbers">
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
							
						</td-->
				</tr>
				</xsl:for-each>
				
			</tbody>
		</table>
		<span class="data_comment limitation">
			ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и могут не отражать абсолютную актуальность и достоверность информации. Дополнительную информацию можно получить в разделе
			<a onclick="gotomenu(48);" href="#">«Квартальные отчеты»</a> или <a onclick="gotomenu(32);" href="#">«Существенные факты»</a>
		</span>
		

		<script language="javascript" type="text/javascript">
			<![CDATA[ 
			xls_params={"iss":$("#iss").val(),"module" : "bonds/skrin/","x":Math.random()};
			var trclasses=new Array("trclosed","trshow")
			$(function() {
					var sch = window.innerHeight ? window.innerHeight : document.documentElement.offsetHeight; 
					var scw = window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth; 
					var	ww,wh,top,left;
					ww=(scw<1000)?scw-44:1000;
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
			function switch_vis_det(tid){
			        showClock();
					var newClass=trclasses[Math.abs($("#sw" + tid).attr("val")-1)];
					var coll
					for(var i=1; i<4; i++){
					   $(document).find("#tab_" + i + "_" + tid).find("." + trclasses[$("#sw" + tid).attr("val")]).each(function(i){
								this.className=newClass;
							});
					}
					$("#sw" + tid).attr("val",Math.abs($("#sw" + tid).attr("val")-1));
							$("#sw" + tid).html((($("#sw" + tid).attr("val")=="1")? "Скрыть календарь":"Календарь выплат"))
							$("#img" + tid).attr("src",(($("#sw" + tid).attr("val")=="1")? "/images/tra_w.png":"/images/tra_e.png"));
					hideClock();		

					
			}
			function switch_vis(){
				var trclasses=new Array("trclosed","trshow")
				var newClass=trclasses[Math.abs($("#switcher").attr("val")-1)];
				
				$("."+trclasses[$("#switcher").attr("val")]).each(function(i){
					if(this.parentElement.parentElement.id.indexOf("tab_")<0){
						this.className=newClass;
					}	
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
						$.post("/Message/ActionsSkrin/",{"type_id" : "6","ids" : ids,"iss" : iss}/*,"ww":(ww-10), "wh" : (wh-25)}*/,
						function(data){
            show_dialog({ "content": data, "is_print": true,"extra_style":"width:1200px;" });
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
