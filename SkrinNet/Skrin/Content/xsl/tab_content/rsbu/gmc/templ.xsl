<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">
	<xsl:import href="../../../../xsl/common.xsl"/>
	<msxsl:script language="JScript" implements-prefix="js">
    function FormatLineCode(str,ntypes){
    str=str.substr(0,str.length-1);
    var aCodes=str.split("/");
    var aCodesMod=new Array();
    var aTmp = new Array();
    var index=-1
    var ret_val="";
    for(var i=0; i &lt; aCodes.length; i++){
    aTmp=aCodes[i].split("_");
    index=aScan(aCodesMod,aTmp[0])
    if(index &lt; 0){
    aCodesMod[aCodesMod.length]=aTmp;
    }else{
    aCodesMod[index][1] +="," + aTmp[1];
    }
    }
    for(var i=0; i &lt; aCodesMod.length; i++){
    ret_val = ret_val + ((i==0)?"":" / ") + aCodesMod[i][0]+((ntypes &gt; 1)? "&lt;span class=\"uin\"&gt;" + aCodesMod[i][1] + "&lt;/span&gt;":"");
    }
    return ret_val;
    }


    function aScan(arr,val,ind){
    var ret_val=-1;
    for(var i=0; i &lt; arr.length; i++){
			if(arr[i][0]==val){
				ret_val=i;
				i=arr.length+1
			}
		
		}
		return ret_val
		}
	</msxsl:script>
	<xsl:output method="html" version="4.0" encoding="utf-8"/>
	<xsl:decimal-format name="buh" decimal-separator="," grouping-separator=" " NaN="-"/>

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
      <table cellpadding="4" style="margin-bottom:8px;">
        <tr>
          <xsl:if test="//fn/@val=1">
            <td class="content_menu" valign="top">
              Бухгалтерский баланс (Форма № 1)<br/>Отчет о прибылях и убытках (Форма № 2)
            </td>

            <td style="cursor:pointer;background-color:#EEE;" class="content_menu_sel" >
              <xsl:attribute name="onclick">
                goform(2,'<xsl:value-of select="//@iss"/>')
              </xsl:attribute>
              Отчет об изменениях капитала (Форма № 3)<br/>Отчет  о движении денежных средств (Форма №4)<br/>Приложение к бухгалтерскому балансу (Форма № 5)<xsl:if test="//type_id/@val=7">
                <br/>Отчет о целевом использовании средств(Форма №6)
              </xsl:if>
            </td>

          </xsl:if>
          <xsl:if test="//fn/@val=2">
            <td style="cursor:pointer;background-color:#EEE;" class="content_menu_sel" >
              <xsl:attribute name="onclick">
                goform(1,'<xsl:value-of select="//@iss"/>')
              </xsl:attribute>
              Бухгалтерский баланс (Форма № 1)<br/>Отчет о прибылях и убытках (Форма № 2)
            </td>
            <td class="content_menu" valign="top">
              Отчет об изменениях капитала (Форма № 3)<br/>Отчет  о движении денежных средств (Форма №4)<br/>Приложение к бухгалтерскому балансу (Форма № 5)<xsl:if test="//type_id/@val=7">
                <br/>Отчет о целевом использовании средств(Форма №6)
              </xsl:if>
            </td>

          </xsl:if>
        </tr>
      </table>
		<form id="frm">
			<table id="curr_selector" onclick="checkchecked(event);" style="border:none;background-color:#f6f7fa ;">
				<tr>
					<td style="border:none;background-color:#f6f7fa ;">Период:</td>
					<td style="border:none;background-color:#f6f7fa ;"><input type="text" id="inpselector" readonly="readonly" class="system_form dds" onclick="showchbselector(event)" style="width:180px;"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:if test="@sel=1"><xsl:value-of select="@yq"/>,</xsl:if></xsl:for-each></xsl:attribute></input>
					<img id="sel_div_btn" alt="" onclick="showchbselector(event)" src="/images/select_bottom_small2.gif" class="dds_butt"/>

					</td>
					<xsl:if test="//fn/@val=1">
						<td style="border:none;background-color:#f6f7fa ;">
							<!--xsl:if test="//fn/@val=2"><xsl:attribute name="style">
								display:none;
							</xsl:attribute>
						</xsl:if--> Валюта: </td>
						<xsl:for-each select="currency">
							<td style="border:none;background-color:#f6f7fa ;"><!--xsl:if test="//fn/@val=2"><xsl:attribute name="style">
								display:none;
							</xsl:attribute>
						</xsl:if-->
								<input type="radio" name="currency"><xsl:attribute name="id">r<xsl:value-of select="@id"/></xsl:attribute><xsl:attribute name="value"><xsl:value-of select="@id"/></xsl:attribute><xsl:if test="@sel=1"><xsl:attribute name="checked">checked</xsl:attribute></xsl:if><xsl:attribute name="onclick">goform(<xsl:value-of select="//fn/@val"/>,'<xsl:value-of select="//@iss"/>');</xsl:attribute><xsl:value-of select="@name"/></input>
							</td>
						</xsl:for-each>
						</xsl:if>
					<xsl:if test="//fn/@val=2">
						<td style="border:none;background-color:#f6f7fa ;">
							Валюта:
						</td><td>
							<input type="radio" name="currency" id="r1" value="1" checked="checked"/>тыс.рублей.
						</td>
					</xsl:if>
				</tr>
		</table>
			<input type="hidden" id="ds_date"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:value-of select="@yq"/>,</xsl:for-each></xsl:attribute></input>
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
			Период: <xsl:for-each select="per">
				<xsl:if test="@sel=1">
					<xsl:value-of select="@yq"/>
					<br/>
				</xsl:if>
			</xsl:for-each>
			Валюта: <xsl:for-each select="currency">
				<xsl:if test="@sel=1">
					<xsl:value-of select="@name"/>
					<br/>
					<br/>
				</xsl:if>
			</xsl:for-each>
		</xsl:if>
		<xsl:for-each select="f">
			<xsl:for-each select="s">
				<span class="subcaption" align="left"><xsl:if test="position()>1"><xsl:attribute name="style">text-transform:none;</xsl:attribute></xsl:if>
					<xsl:choose>
						<xsl:when test="position()>1">
							<xsl:value-of select="substring(@name,1,1)"/><span style="text-transform:lowercase;"><xsl:value-of select="substring(@name,2,512)"/></span>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="@name"/>
						</xsl:otherwise>
					</xsl:choose><xsl:if test="position()=1">(ФОРМА № <xsl:value-of select="../@form_no"/>)</xsl:if>
				</span>
				<br/><xsl:value-of select="//type_id/@name"/>
				<table width="100%" cellpadding="0" cellspacing="0">
					<xsl:for-each select="fa">
				
					<xsl:if test="position()=1">
						<tr>
							<td class="table_caption_left" style="text-align:center;">Наименование показателя</td>
							<td class="table_caption" style="text-align:center;" width="20px">Код строки</td>
							<xsl:for-each select="Form[1]/c">
								<td class="table_caption" style="text-align:center;" width="80px">
									<xsl:value-of select="@col"/>
								</td>
							</xsl:for-each>
						</tr>
						
						
						<xsl:if test="//cs/@val &gt; 1 and position()=1">
							<tr>
								<td>
									Курс <xsl:value-of select="//kurs_header/@val"/>
								</td>
								<td></td>
								<xsl:for-each select="Form[1]/c">
									<td style="text-align:right">
										<xsl:value-of select="v/@kurs"/>
									</td>
								</xsl:for-each>
							</tr>
							<tr>
								<td>
									Курс на дату
								</td>
								<td></td>
								<xsl:for-each select="Form[1]/c">
									<td style="text-align:right">
										<xsl:value-of select="v/@cd"/>
									</td>
								</xsl:for-each>
							</tr>
						</xsl:if>

					</xsl:if>
						<xsl:if test="string-length(@name) &gt; 0">
							<xsl:if test="(../../@form_no=1 and position()=1)">
								<tr>
									<td>
										<b>АКТИВ</b>
									</td>
									<td></td>
									<xsl:for-each select="Form[1]/c">
										<td>
											<img src="/images/null.gif" width="1px" height="1px"/>
										</td>
									</xsl:for-each>
								</tr>
							</xsl:if>
							
							<xsl:if test="//type_id/@sub_head=1">
								<tr>
									<td style="text-transform:uppercase;">
										<b>
											<xsl:value-of select="@name"/>
										</b>
									</td>
									<td style="text-align:center;" width="20px"></td>
									<xsl:for-each select="Form[1]/c">
										<td style="text-align:center;" width="80px"></td>
									</xsl:for-each>
								</tr>
							</xsl:if>
					</xsl:if>
						<xsl:for-each select="Form">
							<xsl:if test="//f/@form_no=6">
								<xsl:if test="@line_code=6220">
									<tr>
										<td>
											<b>Поступило средств</b>
										</td>
										<td></td>
										<xsl:for-each select="c">
											<td>
												<img src="/images/null.gif" width="1px" height="1px"/>
											</td>
										</xsl:for-each>
									</tr>
								</xsl:if>
								<xsl:if test="@line_code=6310">
									<tr>
										<td>
											<b>Использовано средств</b>
										</td>
										<td></td>
										<xsl:for-each select="c">
											<td>
												<img src="/images/null.gif" width="1px" height="1px"/>
											</td>
										</xsl:for-each>
									</tr>
								</xsl:if>
							</xsl:if>
							<xsl:if test="@rowspan &gt;0">
								<tr>
									<xsl:choose>
										<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">
											<xsl:attribute name="bgColor">#F0F0F0</xsl:attribute>
										</xsl:when>
										<xsl:otherwise>
											<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
										</xsl:otherwise>
									</xsl:choose>
								<td>
									<xsl:value-of select="@name"/>
								</td>
								<td style="text-align:center;">
									<xsl:value-of select="@line_code"/>
								</td>
								<xsl:for-each select="c">
									<td style="text-align:right;line-height: 2em;white-space:nowrap;">
										<xsl:value-of select="format-number(v/@val div 1000, '# ##0,00','buh')"/>
									</td>
								</xsl:for-each>
							</tr>
						</xsl:if>
								<xsl:if test="//f/@form_no=1">
						<xsl:if test="@line_code=300 or @line_code=1600">
							<tr>
								<td>
									<b>ПАССИВ</b>
								</td>
								<td></td>
								<xsl:for-each select="c">
									<td>
										<img src="/images/null.gif" width="1px" height="1px"/>
									</td>
								</xsl:for-each>
							</tr>
						</xsl:if>
					</xsl:if>
							
						</xsl:for-each>
				</xsl:for-each>
				</table>
				<br/>
				<br/>
			</xsl:for-each>
		</xsl:for-each>
		<xsl:if test="headers[position()=last()]">
			<font class="data_comment">по данным неконсолидированной бухгалтерской отчетности эмитента</font>
			<table width="100%" cellpadding="0" cellspacing="0">
				<xsl:for-each select="headers">
					<xsl:choose>
						<xsl:when test="@parent_id=0">
							<tr>
								<td>
									<xsl:attribute name="colspan">
										<xsl:value-of select ="count(./LA/dta)+2"/>
									</xsl:attribute>
									<span class="subcaption">
										<xsl:value-of  disable-output-escaping="yes" select ="@name"/>
									</span>
								</td>
							</tr>
							
								<tr>
									<td class="table_caption_left">Наименование показателя</td>
									<td class="table_caption_left">Код строки</td>
									<xsl:for-each select="LA[1]/dta">
										<td class="table_caption" align="center">
											<xsl:value-of select="concat(@y,'г.')"/>
										</td>
									</xsl:for-each>
								</tr>
								
								<xsl:if test="//cur">
									<tr>
										<td>
											Курс <xsl:value-of select="//kurs_header/@val"/>
										</td>
										<td></td>
										<xsl:for-each select="//cur">
											<td style="text-align:right">
												<xsl:value-of select="@kurs"/>
											</td>
										</xsl:for-each>
									</tr>

									<tr>
										<td>
											Курс на дату
										</td>
										<td></td>
										<xsl:for-each select="//cur">
											<td style="text-align:right">
												<xsl:value-of select="@cd"/>
											</td>
										</xsl:for-each>
									</tr>
								</xsl:if>
							
						</xsl:when>
						<xsl:otherwise>
							<tr>
								<td>
									<xsl:attribute name="colspan">
										<xsl:value-of select ="count(./LA[1]/dta)+2"/>
									</xsl:attribute>
									<span style="font-weight:bold;text-transform:uppercase;">
										<xsl:value-of  disable-output-escaping="yes" select ="@name"/>
									</span>
								</td>
							</tr>
						</xsl:otherwise>
					</xsl:choose>

					
					
					<xsl:if test="string-length(LA/@line_agg)>0">

						<xsl:for-each select ="LA">
							
								<tr>
									<xsl:choose>
										<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">
											<xsl:attribute name="bgColor">#F0F0F0</xsl:attribute>
										</xsl:when>
										<xsl:otherwise>
											<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
										</xsl:otherwise>
									</xsl:choose>
									<td class="table_item_left" width="300">
										<xsl:value-of disable-output-escaping ="yes" select="@name"/>
									</td>
									<td class="table_item" align="center" >
                    <xsl:value-of disable-output-escaping ="yes" select="js:FormatLineCode(string(@line_agg),string(//ntypes/@val))"/>
									</td>

									<xsl:for-each select="dta">
										<td style="text-align:right;white-space:nowrap;">
											<xsl:choose>
												<xsl:when test ="@val=-0.001">-</xsl:when>
												<xsl:otherwise>
													<xsl:value-of select="format-number(@val div 1000,'# ##0,00','buh')"/>
												</xsl:otherwise>
											</xsl:choose>
										</td>
									</xsl:for-each>
								</tr>
							
						</xsl:for-each>


					</xsl:if>
				</xsl:for-each>
			</table>
			<span class="data_comment limitation">Сведения предоставлены Росстатом</span>
			<br/>
			<br/>
			<span class="data_comment">
				<xsl:for-each select="comments"	>
					<span class="uin"><xsl:value-of select="@idx"/></span>
					<xsl:value-of select="tt/@comment"/><br/>
				</xsl:for-each>
			</span>
			

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
            		

            iframepost({"id": $("#tab_id").val(), "curr":$("#curr_selector").find("input:radio:checked").val(), "ticker":$("#iss").val(),"per" :getYears(),"fn" :($("#fn").val()==1)?"1,2":"3,4,5", "xls" : "1"}, "/Tab/", "reports");
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
            d.style.width=($("#inpselector").width() + $("#sel_div_btn").width() + 23) + "px";
            document.body.appendChild(d);
            $("body").click(closeselector);
            $(d).click(function(e){
              if (window.event){
	                e = window.event;
              }
             if (e.stopPropagation) {
               e.stopPropagation();
             }else{
              e.cancelBubble=true;
            }
      })
        }else{
            closeselector()
            
        }
        if(getObj("ddselector")){
            var aData=$("#ds_date").val().split(",");
            Sel="," + $("#inpselector").val()+",";
            for(var i=0; i < aData.length; i++){
                if(aData[i].length>0){
                    d.innerHTML+="<input style=\"margin-left:6px;\" onclick=\"checkchecked(event);\" type=\"checkbox\" id=\"chb" + i + "\"" + ((Sel.indexOf("," + aData[i] + ",")>=0)?" checked" : "") + " value=\"" + TrimStr(aData[i]) + "\"/>" + TrimStr(aData[i]) + "<br/>";
                }    
            }
            d.innerHTML+="<div style=\"text-align:center\"><input type=\"button\" class=\"btns blue\" style=\"margin-bottom:6px;\" value=\"Выбрать\" onclick=\"doSelect()\"/></div>"
            checkchecked(null);
        }
        
    }
    function closeselector(){
        if(getObj("ddselector")){
            document.body.removeChild(getObj("ddselector"));
            $("body").unbind();
        }
    }
    function checkchecked(e){
        var cnt=0;
        var src;
        if (window.event){
	        e = window.event;
        }
		if(e){
			
			if (e.stopPropagation) {
				e.stopPropagation();
				src=e.target;
			 }else{
				 e.cancelBubble=true;
				 src=e.srcElement;
			}    
		}
		if($("#fn").val()-2==0 && src.id.indexOf("chb")==0){
			$("input:checkbox:checked").each(function(i){
				if(this.id!=src.id){
					this.checked=false;
				}
			})
		}

		var buff="";
		if($("#curr_selector").find("input:radio:checked").val()-3==0){
			aYears=$("#inpselector").val().split(",");
			for(var i=0; i < aYears.length; i++){
				if(aYears[i]>1998){
					buff+=aYears[i]+",";
				}	
			}
			$("#inpselector").val(buff);
			
		}
        $("input:checkbox").each(function(i){
			if($("#curr_selector").find("input:radio:checked").val()-3==0){
				if(this.value<1999){
					this.disabled=true;
					this.checked=false;
				}
			}
			if(this.checked){
				cnt++;
			}
			
        });
        if(cnt>=5){
            $("input:checkbox").each(function(i){
                if(!this.checked){
                    this.disabled=true;
                }
				
				
				
            })
        
        }else{
             $("input:checkbox").each(function(i){
                    this.disabled=false;
					if($("#curr_selector").find("input:radio:checked").val()-3==0){
						if(this.value<1999){
							this.disabled=true;
							this.checked=false;
						}
					}
            })
        }
    }
    function doSelect(){
        var buff="";
		var cantEuro=false;
        $("input:checkbox:checked").each(function(i){
            buff+=this.value + ",";
			if(this.value<1999){
				cantEuro=true;
			}
        });
		if(getObj("r3")){
			getObj("r3").disabled=cantEuro;
		}	
        $("#inpselector").val(buff);
        document.body.removeChild(getObj("ddselector"));
		goform($("#fn").val(),$("#iss").val());
    }
	function goform(fn,iss){
		period=$("#inpselector").val();
		if(fn-$("#fn").val()!=0){
			period="";
		}
		if(fn-1==0){
			currency=$("#curr_selector").find("input:radio:checked").val()
		}else{
			currency="1"
		}
		
			
		
		showClock();
        
        $("#tab_content").load("/Tab/?id=54&ticker=" + iss + "&fn=" + ((fn==1)?"1,2":"3,4,5,6") +  "&per=" + period + "&curr=" + currency ,
        function (data) {
                hideClock();
        });
	}
	function getYears(){
		var years=$("#inpselector").val();
		var aYears=years.split(",");
		years="";
		for(var i=0; i < aYears.length-1; i++){
			if(aYears.length>0){
				years+=aYears[i] + ","
			}	
			
		}
		years=(years==",")?"":years;
		return years
	}
	$(document).ready(function(){
		xls_params={"per": getYears(),"curr":$("#curr_selector").find("input:radio:checked").val(),"iss":$("#iss").val(),"fn":($("#fn").val()==1)?"1,2":"3,4,5","module" : "rsbu/gmc/","x":Math.random()}
		var cantEuro=false;
		aYears=$("#inpselector").val().split(",");
			for(var i=0; i < aYears.length; i++){
				if(aYears[i]<1999 && TrimStr(aYears[i]).length>0){
					cantEuro=true;
				}	
			}
			if(getObj("r3")){
				getObj("r3").disabled=cantEuro;
			}	
	});

				]]>
</script>
</xsl:template>
</xsl:stylesheet>
