<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">
	<xsl:import href="../../../../xsl/common.xsl"/>
	<msxsl:script language="JScript" implements-prefix="js">
    function hasitem( oNodeVals, oNodeCol )
    {
    var oVals = oNodeVals.nextNode();
    var oCol = oNodeCol.nextNode().text;
    return oVals.getAttribute(oCol).text;
    }
    function GetMult(nl){
    var val= nl.toUpperCase(); <!--String(nl.nextNode().text);-->
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
	<xsl:decimal-format name="buh" decimal-separator="," grouping-separator=" " NaN="0"/>

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
		<xsl:if test="//@PDF=-1">
		<table cellpadding="4">
			<tr>
				<xsl:if test="//fn/@val=1">
					<td class="content_menu" valign="top">
						Оборотная ведомость по счетам бухгалтерского учета (Форма №101)
					</td>
          <td style="cursor:pointer;background-color:#EEE;" class="content_menu_sel" >
						<xsl:attribute name="onclick">goform(2,'<xsl:value-of select="//@iss"/>')</xsl:attribute>
						Отчет о прибылях и убытках (Форма №102)
					</td>

				</xsl:if>
				
				<xsl:if test="//fn/@val=2">
				<td  style="cursor:pointer;background-color:#EEE;" class="content_menu_sel" valign="top"><xsl:attribute name="onclick">goform(1,'<xsl:value-of select="//@iss"/>')</xsl:attribute>
					Оборотная ведомость по счетам бухгалтерского учета (Форма №101)
				</td>
				<td class="content_menu">
					Отчет о прибылях и убытках (Форма №102)
				</td>
				</xsl:if>
			</tr>
		</table>

      
		<br/>
			
		<form id="frm">
			<table id="curr_selector" onclick="checkchecked(event);">
				<tr>
					<td>Период:</td>
					<td>
            <select id="per_sel" class="system_form" onchange="doSelect()">
              <xsl:for-each select="per">
                <option>
                  <xsl:attribute name="value">
                    <xsl:value-of select="@yq"/>
                  </xsl:attribute>
                  <xsl:if test="@sel=1">
                    <xsl:attribute name="selected">selected</xsl:attribute>
                  </xsl:if>
                  <xsl:value-of select="@name"/>
                </option>
              </xsl:for-each>

            </select>






            <!--input type="text" id="inpselector" readonly="readonly" class="system_form" onclick="showchbselector(event)" style="width:197px;"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:if test="@sel=1"><xsl:value-of select="@sname"/></xsl:if></xsl:for-each></xsl:attribute></input>
						<img id="sel_div_btn" alt="" onclick="showchbselector(event)" src="/images/select_bottom_small2.gif" style="border:solid 1px #c0c0c0; padding:4px 3px 3px 3px; vertical-align:bottom; cursor:pointer;"/-->
					</td>
					<td><!--input type="button" id="goform_btn" value="Показать" class="btns blue"><xsl:attribute name="onclick">goform(<xsl:value-of select="//fn/@val"/>,'<xsl:value-of select="//@iss"/>')</xsl:attribute></input--> </td>
				</tr>
		</table>
			<input type="hidden" id="ds_date"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:value-of select="@sname"/>,</xsl:for-each></xsl:attribute></input>
			<input type="hidden" id="ds_name"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:value-of select="@name"/>,</xsl:for-each></xsl:attribute></input>
			<input type="hidden" id="fn"><xsl:attribute name="value"><xsl:value-of select="//fn/@val"/></xsl:attribute></input>
			<input type="hidden" id="iss"><xsl:attribute name="value"><xsl:value-of select="//@iss"/></xsl:attribute></input>
      <input type="hidden" id="tab_id">
        <xsl:attribute name="value">
          <xsl:value-of select="//@id"/>
        </xsl:attribute>
      </input>
		</form>
			</xsl:if>
		<xsl:if test="//@PDF=0">
			Период:<xsl:for-each select="per">
			<xsl:if test="@sel=1">
				<xsl:value-of select="@name"/>
			</xsl:if>
		</xsl:for-each>
		</xsl:if>
		<xsl:if test="count(//Per101)>0">
			<br/><span class="subcaption">Оборотная ведомость по счетам бухгалтерского учета (Форма №101)
			</span><br/>тыс. рублей
		<table width="100%" cellpadding="0" cellspacing="0">
			<xsl:if test="../@isAct=0">
				<xsl:attribute name="border">1</xsl:attribute>
			</xsl:if>
			<tr>
				<td class="table_caption_left" rowspan="3" colspan="2">Номер счета второго порядка</td>
				<td class="table_caption" align="center" rowspan="2" colspan="3">
					Входящие остатки
				</td>
				<td class="table_caption" align="center"  colspan="6">
					Обороты за отчетный период
				</td>
				<td class="table_caption" align="center" rowspan="2"  colspan="3">
					Исходящие остатки
				</td>
			</tr>
			<tr>
				<td class="table_caption" align="center" colspan="3">по дебету</td>
				<td class="table_caption" align="center" colspan="3">по кредиту</td>
			</tr>
			<tr>
				<td class="table_caption" align="center">В рублях</td>
				<td class="table_caption" align="center">Ин.вал., драг. металлы</td>
				<td class="table_caption" align="center">итого</td>
				<td class="table_caption" align="center">В рублях</td>
				<td class="table_caption" align="center">Ин.вал., драг. металлы</td>
				<td class="table_caption" align="center">итого</td>
				<td class="table_caption" align="center">В рублях</td>
				<td class="table_caption" align="center">Ин.вал., драг. металлы</td>
				<td class="table_caption" align="center">итого</td>
				<td class="table_caption" align="center">В рублях</td>
				<td class="table_caption" align="center">Ин.вал., драг. металлы</td>
				<td class="table_caption" align="center">итого</td>

			</tr>
			
			<xsl:for-each select="Per101">
				<tr style="background-color:#C0C0C0">
					<td colspan="14" style="background-color:#C0C0C0">
						<b>
							<xsl:value-of select="@sect"/>
						</b>
					</td>
				</tr>
				<xsl:for-each select="QIV">
					<tr>
						<td></td>
						<td>
							<b>
								<xsl:if test="@ap=1">Актив</xsl:if>
								<xsl:if test="@ap=2">Пассив</xsl:if>
							</b>
						</td>
						<td></td>
						<td></td>
						<td></td>
						<td></td>
						<td></td>
						<td></td>
						<td></td>
						<td></td>
						<td></td>
						<td></td>
						<td></td>
						<td></td>
					</tr>
					<xsl:for-each select="F101">
						<tr>
							<td valign="top">
								<b>
									<xsl:value-of select="@LN"/>
								</b>
							</td>
							<td>

								<xsl:value-of select="@NM"/>

							</td>
							<td></td>
							<td></td>
							<td></td>
							<td></td>
							<td></td>
							<td></td>
							<td></td>
							<td></td>
							<td></td>
							<td></td>
							<td></td>
							<td></td>
						</tr>
						<xsl:for-each select="b">
							<tr>
								<xsl:choose>
									<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">
										<xsl:attribute name="bgColor">#F0F0F0</xsl:attribute>
									</xsl:when>
									<xsl:otherwise>
										<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
									</xsl:otherwise>
								</xsl:choose>
								<td valign="top" >
									<xsl:if test="//@PDF=0"><xsl:attribute name="class">td_mini</xsl:attribute>
								</xsl:if>
									<xsl:value-of select="@line_code"/>
								</td>
								<td  style="border:none;">

									<table cellspacing="0" cellpadding="0" style="border:none;">
										<tr>
											<td style="border:none;">
												<img src="/images/null.gif" width="10" height="1"/>
											</td>
											<td style="border:none;"><xsl:if test="//@PDF=0"><xsl:attribute name="class">td_mini</xsl:attribute></xsl:if>
												<xsl:value-of select="@name"/>
											</td>
										</tr>
									</table>
								</td>
								<xsl:variable name ="line" select ="cols"/>
								
								<xsl:for-each select="//Cols101">
									
									<td style="text-align:right;" nowrap="yes" ><xsl:if test="//@PDF=0"><xsl:attribute name="class">td_mini</xsl:attribute></xsl:if>

										<xsl:variable name="code" select="@col_code"/>
										<xsl:for-each select ="$line">
											<xsl:if test="$code=@col_code">
												<xsl:value-of select="format-number(js:GetMult(string(@val)),'# ##0,#','buh')"/>
											</xsl:if>
										</xsl:for-each>
										<xsl:variable name ="ex">
											<xsl:call-template name="has_item">
												<xsl:with-param name="line" select="$line"/>
												<xsl:with-param name="col_code" select="@col_code"/>
											</xsl:call-template>
										</xsl:variable>
										<xsl:if test="string-length($ex) = 0">0</xsl:if>
										
									</td>

								</xsl:for-each>
							</tr>

						</xsl:for-each>
					</xsl:for-each>
				</xsl:for-each>
			</xsl:for-each>
		</table>
		</xsl:if>
		<xsl:if test="count(//F102)">
			<br/><span class="subcaption">
				Отчет о прибылях и убытках (Форма №102)
			</span><br/>тыс. рублей

		<table width="100%" cellpadding="0" cellspacing="0">
				<xsl:if test="../@isAct=0">
					<xsl:attribute name="border">1</xsl:attribute>
				</xsl:if>
				<tr>
					<td class="table_caption_left" rowspan="2">Номер п/п</td>
					<td class="table_caption" align="center" rowspan="2" >
						Наименование статей
					</td>
					<td class="table_caption" align="center"  rowspan="2">
						Символы
					</td>
					<td class="table_caption" align="center" colspan="2">
						Суммы в рублях от операций
					</td>
					<td class="table_caption" align="center" rowspan="2">
						Всего
					</td>

				</tr>
				<tr>
					<td class="table_caption" align="center" >В рублях</td>
					<td class="table_caption" align="center" >В ин. валюте и драг.металлах</td>
				</tr>
				
				<xsl:for-each select="F102">
					<tr>
						<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">
								<xsl:attribute name="style">background-color:#F0F0F0;</xsl:attribute>
							</xsl:when>
							<xsl:otherwise>
								<xsl:attribute name="style">background-color:#FFFFFF</xsl:attribute>
							</xsl:otherwise>
						</xsl:choose>
						<td style="text-align:right;">
							<xsl:value-of select="position()"/>
							<img src="/images/null.gif" width="10" height="1"/>
						</td>
						<td>
							<xsl:value-of select="@NM"/>
						</td>
						<td align="center">
							<xsl:value-of select="@LN"/>
						</td>
						<xsl:variable name ="line" select ="Per102"/>
						<xsl:for-each select="//Cols102">
							<td style="text-align:right;" nowrap="yes">
								<xsl:variable name="code" select="@col_code"/>
								<xsl:for-each select ="$line">
									<xsl:if test="$code=@col_code">
										<xsl:value-of select="format-number(js:GetMult(string(@val)),'# ##0,#','buh')"/>
									</xsl:if>
								</xsl:for-each>
							</td>

						</xsl:for-each>

					</tr>

				</xsl:for-each>
			</table>
		</xsl:if>
		
		<span class="data_comment limitation">ВНИМАНИЕ: в связи с особенностями функционирования и обновления указанного источника информации АО "СКРИН" не может гарантировать абсолютную актуальность и достоверность данных.</span>

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
            	
        iframepost({"id": $("#tab_id").val(),  "ticker":$("#iss").val(),"per" :$("#per_sel").val(),"fn" :$("#fn").val(), "xls" : "1"}, "/Tab/", "reports");
        });
    
			function showchbselector(e){
        if (window.event){
	        e = window.event;
        }
        if (e.stopPropagation) {
            e.stopPropagation();
         }else{
             e.cancelBubble=true;
        }    

        var bounds=getBounds(getObj("inpselector"))
        if(!getObj("ddselector")){
            var d=document.createElement("div");
            d.className="ddselector";
            d.id="ddselector";
            d.style.left=bounds.left + "px";
            d.style.top=bounds.top + 4 + $("#inpselector").height() + "px";
            d.style.width="220px";
            document.body.appendChild(d);
            $("html").click(closeselector);
        }else{
            closeselector()
            
        }
        if(getObj("ddselector")){
            var aData=$("#ds_date").val().split(",");
            Sel="," + $("#inpselector").val()+",";
            for(var i=0; i < aData.length; i++){
                if(aData[i].length>0){
                    d.innerHTML+="<input onclick=\"checkchecked(event);\" type=\"checkbox\" id=\"chb" + i + "\"" + ((Sel.indexOf("," + aData[i] + ",")>=0)?" checked" : "") + " value=\"" + TrimStr(aData[i]) + "\"/>" + TrimStr(aData[i]) + "<br/>";
                }    
            }
            d.innerHTML+="<div style=\"text-align:center\"><input type=\"button\" class=\"btns blue\" value=\"Выбрать\" onclick=\"doSelect()\"/></div>"
            checkchecked(null);
        }
        
    }
    function closeselector(){
        if(getObj("ddselector")){
            document.body.removeChild(getObj("ddselector"));
            $("html").unbind();
        }
    }
    function checkchecked(e){
        var cnt=0;
        var click_src;
        if (window.event){
	        e = window.event;
        }
		if(e){
			
			if (e.stopPropagation) {
				e.stopPropagation();
				click_src=e.target;
			 }else{
				 e.cancelBubble=true;
				 click_src=e.srcElement;
			}    
		
			if( click_src.id.indexOf("chb")==0){
				$("input:checkbox:checked").each(function(i){
					if(this.id!=click_src.id){
						this.checked=false;
					}
				})
			}
		}
    }
    function doSelect(){
		goform($("#fn").val(),$("#iss").val());
    }
	function goform(fn,iss){
		period=String($("#per_sel").val());
		

		if(fn-$("#fn").val()!=0){
			period="0";
		}
		
		showClock();
		$("#forms").load("/iss/content/rsbu/cb/code.asp",{"iss" : iss,"fn":fn,"per" : period,"first" : -1},function(d){hideClock();});
    
    
    $("#tab_content").load("/Tab/?id=" + $("#tab_id").val() + "&ticker=" + iss + "&fn=" + fn +  "&per=" + period ,
        function (data) {
                hideClock();
        });
    
    
	}
	$(document).ready(function(){
		period=String($("#inpselector").val());
		period='' + period.substr(2,4)+period.substr(0,1);
		xls_params={"per": period,"iss":$("#iss").val(),"fn":$("#fn").val(),"module" : "rsbu/cb/","x":Math.random()}
	});
	]]>
</script>
		
</xsl:template>
	<xsl:template name="has_item">
		<xsl:param name="line"/>
		<xsl:param name="col_code"/>
		<xsl:for-each select="$line">
			<xsl:if test="@col_code=$col_code">1</xsl:if>
		</xsl:for-each>
	</xsl:template>
</xsl:stylesheet>
