<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">

	<xsl:import href="../../../../xsl/common.xsl"/>
	<msxsl:script language="JScript" implements-prefix="js">
		function GetMult(val){
		<!--var val=String(nl.nextNode().text);-->
    var retval="0"
    var mult,exp;
    val = val.toUpperCase();
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
		function formatnumber(str) {
		<!--var amount = new String(str.nextNode().text);-->
    var amount = new String(str);
    amount = amount.split("").reverse();

    var output = "";
    for ( var i = 0; i &lt; amount.length-1; i++ ){
		output = amount[i] + output;
		if ((i+1) % 3 == 0 &amp;&amp; (amount.length-1) !== i)output = ' ' + output;
		}
		return output;
		}
		function number_format(node, decimals, point, separator)
		{<!--var number=node.nextNode().text-->
    var number = node;
		if(!isNaN(number))
		{
		point = point ? point : '.';
		number = number.split('.');
		if(separator)
		{
		var tmp_number = new Array();
		for(var i = number[0].length, j = 0; i &gt; 0; i -= 3)
		{
		var pos = i &gt; 0 ? i - 3 : i;
		tmp_number[j++] = number[0].substring(i, pos);
		}
		number[0] = tmp_number.reverse().join(separator);
		}
		if(decimals &amp;&amp; number[1])
			                    number[1] = Math.round((parseFloat(number[1].substr(0, decimals) + '.' + number[1].substr(decimals, number[1].length)), 10));
			                return(number.join(point));
			            }
			            else return(null);
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
	<xsl:template match="profile">
		<!--<xsl:if test="Issuer_Capitals[position()=last()]">
			<span class="subcaption">Размер  уставного  капитала</span>
			
			<table cellpadding="0" cellspacing="0" width="550px">
				<tr>
					<td  class="table_caption" style="width:70px;" >Дата</td>
					<td  class="table_caption"  >Наименование показателя</td>
					<td  class="table_caption"  >Значение, руб.</td>
				</tr>
				<tr>
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
				<xsl:if test="//ic/@val=1 and //@PDF=-1">
					<tr>
						<td colspan="3" style="text-align:right; ">
							<span val="0" onclick="switch_arc(0);" style="cursor:pointer;color:#003399;" id="switcher0">
								Показать архив
							</span>
							<img src="/images/tra_e.png" alt="" style="padding-left:3px">
								<xsl:attribute name="id">img0</xsl:attribute>
							</img>
						</td>
					</tr>
				</xsl:if>
				<xsl:for-each select="Issuer_Capitals">
					<tr><xsl:attribute name="bgcolor"><xsl:call-template name="set_bg"><xsl:with-param name="str_num" select="position()+1"/></xsl:call-template></xsl:attribute><xsl:attribute name="class"><xsl:if test="@ia=0">c0 trclosed</xsl:if></xsl:attribute><xsl:attribute name="id">ic<xsl:value-of select="position()"/></xsl:attribute>
						<td>
							<xsl:value-of select="@dt"/>
						</td>
						<td align="left"  width="50%">Размер уставного капитала:</td>
						<td align="right">
							<xsl:value-of select="format-number(@val, '# ##0.################', 'buh')"/>
						</td>
					</tr>
				</xsl:for-each>
			</table>
		</xsl:if>-->
		<xsl:if test="IShares[position()=last()]">
			<xsl:if test="//@PDF=-1">
			<div style="height:1.7em;padding-top:.7em"> </div>
			</xsl:if>
      <input type="hidden" id="tab_id">
        <xsl:attribute name="value">
          <xsl:value-of select="//@id"/>
        </xsl:attribute>
      </input>

      <span class="subcaption">Размещенные акции</span>
			<table  width="550px" cellpadding="0" cellspacing="0">
				<tr>
					<td  class="table_caption" style="width:70px;">Дата</td>
					<td  class="table_caption"  style="width:40%">Вид акций</td>
					<td  class="table_caption">Номинал, руб</td>
					<td  class="table_caption">Количество, шт</td>

				</tr>
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
				<xsl:if test="//is/@val=1 and //@PDF=-1">
					<tr>
						<td colspan="4" style="text-align:right; ">
							<span val="0" onclick="switch_arc(1);" style="cursor:pointer;color:#003399;" id="switcher1">
								Показать архив
							</span>
							<img src="/images/tra_e.png" alt="" style="padding-left:3px">
								<xsl:attribute name="id">img1</xsl:attribute>
							</img>
						</td>
					</tr>
				</xsl:if>
				<xsl:for-each select="IShares">
					<tr><xsl:attribute name="bgcolor"><xsl:call-template name="set_bg"><xsl:with-param name="str_num" select="position()+1"/></xsl:call-template></xsl:attribute><xsl:attribute name="class"><xsl:if test="@ia=0">c1 trclosed</xsl:if></xsl:attribute><xsl:attribute name="id">is<xsl:value-of select="position()"/></xsl:attribute>
						<td>
							<xsl:value-of disable-output-escaping="yes" select="@dt"/>
						</td>
						<td>
							<xsl:value-of disable-output-escaping="yes" select="@st"/>
						</td>
						<td align="right">
							<!--<xsl:value-of disable-output-escaping="yes" select="format-number(js:GetMult(@face_value), '# ##0.00#', 'buh')"/>-->
              <xsl:value-of disable-output-escaping="yes" select="format-number(js:GetMult(string(@face_value)), '# ##0.00#', 'buh')"/>           
						</td>
						<td  class="numbers" align="right">
							<xsl:choose>
								<xsl:when test="contains(@sn,'/')">
									<table  cellpadding="0" cellspacing="0" border="0"  align="right">
										<tr>
											<td rowspan="2" class="numbers" style="padding-right:3px;">
												<xsl:value-of select="format-number(substring-before(@sn,' '),'# ##0','buh')"/>
											</td>
											<td class="numbers">
												<xsl:value-of select="format-number(substring-before(substring-after(@sn,' '),'/'),'# ##0','buh')"/>
											</td>
										</tr>
										<tr>
											<td style="border-top:solid 1px #000000" class="numbers">
												<xsl:value-of select="format-number(substring-after(@sn,'/'),'# ##0','buh')"/>
											</td>
										</tr>
									</table>
								</xsl:when>
								<xsl:otherwise>
									<xsl:choose>
										<xsl:when test="contains(@sn,'.')">
											<xsl:value-of select="format-number(substring-before(@sn,'.'),'# ##0','buh')"/>.<xsl:value-of select="substring-after(@sn,'.')"/>
										</xsl:when>
										<xsl:otherwise>
											<!--xsl:value-of select="format-number(@sn,'# ##0.################','buh')"/-->
											<xsl:value-of select="js:number_format(string(@sn),2,'.',' ')"/>
										</xsl:otherwise>
									</xsl:choose>

									
								</xsl:otherwise>
							</xsl:choose>
						</td>
					</tr>
				</xsl:for-each>
				
			</table>
		

		</xsl:if>
		<xsl:if test="Shares_Future[position()=last()]">
			<xsl:if test="//@PDF=-1">
				<div style="height:1.7em;padding-top:.7em"> </div>
			</xsl:if>
			<span class="subcaption">Объявленные акции</span>
			<table  width="550px" cellpadding="0" cellspacing="0">
				<tr>
					<td class="table_caption" style="width:50%">Вид акций</td>
					<td class="table_caption" style="width:25%">Номинал, руб</td>
					<td class="table_caption" style="width:25%">Количество, шт</td>
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
				</tr>
				<xsl:for-each select="Shares_Future">
					<tr>
						<xsl:attribute name="bgcolor">
							<xsl:call-template name="set_bg">
								<xsl:with-param name="str_num" select="position()"/>
							</xsl:call-template>
						</xsl:attribute>
						<td>
							<xsl:value-of disable-output-escaping="yes" select="@sectype"/>
						</td>
						<td align="right">
              <xsl:value-of disable-output-escaping="yes" select="format-number(js:GetMult(string(@face_value)), '# ##0.00##############', 'buh')"/>             
            </td>
						<td align="right">
							<xsl:value-of disable-output-escaping="yes" select="format-number(@shares, '# ##0', 'buh')"/>
						</td>

					</tr>
				</xsl:for-each>
			</table>
			
		</xsl:if>
		<xsl:if test="issues">
			<xsl:if test="//@PDF=-1">
				<div style="height:1.7em;padding-top:.7em"> </div>
			</xsl:if>
		<span class="subcaption">Выпуски акций</span>
		<table width="98%" cellpadding="0" cellspacing="0" border="0">
			<thead>
				<tr>
					<td class="table_caption" colspan="2">Дата государственной регистрации</td>
					<td class="table_caption">Государственный регистрационный номер</td>
					<td class="table_caption">Вид ценной бумаги</td>
					<td class="table_caption">Номинальная стоимость каждой ценной бумаги выпуска</td>
					<td class="table_caption">Количество ценных бумаг, подлежавших размещению, шт.</td>
					<td class="table_caption">Количество в обращении  ценных бумаг, шт.</td>
					<td class="table_caption">Способ размещения</td>
					<td class="table_caption">Текущее состояние ценных бумаг выпуска</td>

				</tr>
			</thead>
			<tbody>
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
				<xsl:if test="//showall/@val=1 and //@PDF=-1">
					<tr>
						<td colspan="9" style="text-align:right; ">
							<span val="0" onclick="switch_vis();" style="cursor:pointer;color:#003399;" id="switcher">
								Показать аннулированные
							</span>
							<img src="/images/tra_e.png" alt="" style="padding-left:3px">
								<xsl:attribute name="id">img</xsl:attribute>
							</img>
						</td>
					</tr>
				</xsl:if>

				<xsl:for-each select="issues">
					<tr><xsl:attribute name="bgcolor"><xsl:call-template name="set_bg"><xsl:with-param name="str_num" select="position()+1"/></xsl:call-template></xsl:attribute><xsl:attribute name="class"><xsl:if test="@ia=0">trclosed</xsl:if></xsl:attribute><xsl:attribute name="id">ac<xsl:value-of select="position()"/></xsl:attribute>
						<td valign="top" onclick="void(0);">
							<xsl:if test="@has_info=1">
								<input class="system_form" type="checkbox" onclick="ch_button()">
									<xsl:attribute name="id">ia<xsl:value-of select="position()"/></xsl:attribute>
									<xsl:attribute name="value"><xsl:value-of select="@id"/></xsl:attribute>
								</input>
							</xsl:if>
						</td>
						<td align="center" valign="top">
							<xsl:value-of select="@rd"/>
						</td>
						<td align="center" valign="top">
							<xsl:if test="@has_info=1">
								<xsl:attribute name="style">color:#003399;text-decoration:underline;cursor:pointer;</xsl:attribute>
								<xsl:attribute name="onclick">
									ShowAction('<xsl:value-of select="@id"/>','<xsl:value-of select="//@iss"/>',event)
								</xsl:attribute>
							</xsl:if>
							<xsl:value-of select="@reg_no"/>
						</td>
						<td align="center" valign="top">
							<xsl:value-of select="@action_type"/>
						</td>
						<td align="right" valign="top">
							<xsl:value-of select="format-number(js:GetMult(string(@face_value)),'# ##0.00##############','buh')"/> <!--руб.-->
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
											<td style="border-top:solid 1px #000000" class="numbers">
												<xsl:value-of select="format-number(substring-after(@shares_declared,'/'),'# ##0','buh')"/>
											</td>
										</tr>
									</table>
								</xsl:when>
								<xsl:otherwise>
									<xsl:choose>
										<xsl:when test="contains(@shares_declared,'.')">
											<xsl:value-of select="format-number(substring-before(@shares_declared,'.'),'# ##0','buh')"/>.<xsl:value-of select="substring-after(@shares_declared,'.')"/>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="format-number(@shares_declared,'# ##0.################','buh')"/>
										</xsl:otherwise>
									</xsl:choose>
									
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
									<xsl:choose>
										<xsl:when test="contains(@shares_declared,'.')">
											<xsl:value-of select="format-number(substring-before(@shares_rolling,'.'),'# ##0','buh')"/>.<xsl:value-of select="substring-after(@shares_rolling,'.')"/>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="format-number(@shares_rolling,'# ##0.################','buh')"/>
										</xsl:otherwise>
									</xsl:choose>
								</xsl:otherwise>
							</xsl:choose>
						</td>
						<td align="center" valign="top" style="width:150px;">
							<xsl:value-of select="@pt_name"/>
						</td>
						<td align="center" valign="top">
							<xsl:value-of select="@ps_name"/>
						</td>
				</tr>
				</xsl:for-each>
				
			</tbody>
		</table>
		<div class="spacer">
			<input type="button"  id="shbc" class="btns dis" style="width:250px;" value="Посмотреть выбранные выпуски">
				<xsl:attribute name="onclick">
					ShowAction(-1,"<xsl:value-of select="//@iss"/>",event)
				</xsl:attribute>
			</input>
		</div>
		</xsl:if>
		<input type="hidden" id="iss"><xsl:attribute name="value"><xsl:value-of select="//@iss"/></xsl:attribute></input>
		<span class="data_comment limitation">
			ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и могут не отражать абсолютную актуальность и достоверность информации. Дополнительную информацию можно получить в разделе
			<a onclick="gotomenu(48);" href="#">«Квартальные отчеты»</a> или <a onclick="gotomenu(32);" href="#">«Существенные факты»</a>
		</span>
		
		<script language="javascript" type="text/javascript">
			<![CDATA[ 
			xls_params={"iss":$("#iss").val(),"module" : "action/skrin/","x":Math.random()};
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
					if (this.id.substring(0,2)=="ac"){
						this.className=newClass;
					}
		        });
				$("#switcher").attr("val",Math.abs($("#switcher").attr("val")-1));
				$("#switcher").html((($("#switcher").attr("val")=="1")? "Скрыть аннулированные":"Показать аннулированные"))
				$("#img").attr("src",(($("#switcher").attr("val")=="1")? "/images/tra_w.png":"/images/tra_e.png"));
				
			}
			function switch_arc(sel){
				var newClass=trclasses[Math.abs($("#switcher" + sel).attr("val")-1)];
				var aids=new Array("ic","is");
				
				$("."+trclasses[$("#switcher" + sel).attr("val")]).each(function(i){
					if (this.id.substring(0,2)==aids[sel]){
						this.className=newClass;
					}	
		        });
				$("#switcher" + sel ).attr("val",Math.abs($("#switcher" + sel).attr("val")-1));
				$("#switcher" + sel).html((($("#switcher" + sel).attr("val")=="1")? "Скрыть архив":"Показать архив"));
				$("#img" + sel).attr("src",(($("#switcher" + sel).attr("val")=="1")? "/images/tra_w.png":"/images/tra_e.png"));
				
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
						$.post("/Message/ActionsSkrin/",{"type_id" : "1","ids" : ids,"iss" : iss}/*,"ww":(ww-10), "wh" : (wh-25)}*/,
						function(data){
            show_dialog({ "content": data, "is_print": true,"extra_style":"width:900px;" });
						hideClock();						
						}
						,"html");
					}	
			}
			]]>
		</script>
	</xsl:template>
</xsl:stylesheet>
