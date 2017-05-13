<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">
	<xsl:import href="../../../xsl/common.xsl"/>
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


		function aScan(arr,val){
		var ret_val=-1;
		for(var i=0; i &lt; arr.length; i++){
    if(arr[i][0]==val){
    ret_val=i;
    i=arr.length+1
    }

    }
    return ret_val
    }
    function GetMult(nl){
    var val=0
    var retval="0"
    var mult,exp;
    if(nl.length){
    val=nl.toUpperCase();
    if(val.indexOf("E") &lt; 0){
    retval=val
    }else{
    mult=val.substring(0,val.indexOf("E"));
    exp=val.substring(val.indexOf("E")+1,val.length);
    exp=Math.pow(10,exp);
    retval=mult*exp;
    }
    }
    return retval;
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
			<xsl:if test="//cons/@val=1">
						<xsl:if test="//fn/@val=0">
							<div class="content_menu">
								неконсолидированная бухгалтерская отчетность
							</div>
							<div style="cursor:pointer;" class="content_menu_sel" >
								<xsl:attribute name="onclick">
									goform(1,'<xsl:value-of select="//@iss"/>')
								</xsl:attribute>
								консолидированная бухгалтерская отчетность
							</div>

						</xsl:if>
						<xsl:if test="//fn/@val=1">
							<div style="cursor:pointer;" class="content_menu_sel" >
								<xsl:attribute name="onclick">
									goform(0,'<xsl:value-of select="//@iss"/>')
								</xsl:attribute>
								неконсолидированная бухгалтерская отчетность
							</div>
							<div class="content_menu">
								консолидированная бухгалтерская отчетность
							</div>
						</xsl:if>
					
			</xsl:if>
		</xsl:if>
		<xsl:if test="//@PDF=0">
			неконсолидированная бухгалтерская отчетность
		</xsl:if>
		<form id="frm">
			<xsl:if test="//@PDF=-1">
			<table id="curr_selector" onclick="checkchecked(event);" style="border:none;background-color:#f6f7fa ;">
				<tr>
					<td style="border:none;background-color:#f6f7fa ;">Период:</td>
					<td style="border:none;background-color:#f6f7fa ;"><input type="text" id="inpselector" readonly="readonly" class="system_form" onclick="showchbselector(event)" style="width:300px;"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:if test="@sel=1"><xsl:value-of select="@sname"/>,</xsl:if></xsl:for-each></xsl:attribute></input>
					<input type="hidden" id="dtselector"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:if test="@sel=1"><xsl:value-of select="@yq"/>,</xsl:if></xsl:for-each></xsl:attribute></input>
						<img id="sel_div_btn" alt="" onclick="showchbselector(event)" src="/images/select_bottom_small2.gif" style="border:solid 1px #c0c0c0; padding:4px 5px 7px; vertical-align:bottom; cursor:pointer;"/>

					</td>
			
					<td style="border:none;background-color:#f6f7fa ;">Валюта: </td><xsl:for-each select="currency">
					<td style="border:none;background-color:#f6f7fa ;">
				<input type="radio" name="currency"><xsl:attribute name="id">r<xsl:value-of select="@id"/></xsl:attribute><xsl:attribute name="value"><xsl:value-of select="@id"/></xsl:attribute><xsl:attribute name="onclick">goform(<xsl:value-of select="//fn/@val"/>,'<xsl:value-of select="//@iss"/>')</xsl:attribute><xsl:if test="@sel=1"><xsl:attribute name="checked">checked</xsl:attribute></xsl:if><xsl:value-of select="@name"/></input>
						</td>
			</xsl:for-each>
				</tr>
		</table>
			</xsl:if>
			<xsl:if test="//@PDF=0">
				Период: <xsl:for-each select="per">
				<xsl:if test="@sel=1">
					<xsl:value-of select="@sname"/>
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
			<input type="hidden" id="ds_date"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:value-of select="@sname"/>,</xsl:for-each></xsl:attribute></input>
			<input type="hidden" id="ds_name"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:value-of select="@name"/>,</xsl:for-each></xsl:attribute></input>
			<input type="hidden" id="fn"><xsl:attribute name="value"><xsl:value-of select="//fn/@val"/></xsl:attribute></input>
			<input type="hidden" id="iss"><xsl:attribute name="value"><xsl:value-of select="//@iss"/></xsl:attribute></input>
		</form>
		<xsl:if test="f">
		<xsl:for-each select="f">
			<xsl:for-each select="s">
				<span class="subcaption" align="left" style="text-transform:uppercase;">
							<xsl:value-of select="@name"/>
					<xsl:if test="position()=1">
						(ФОРМА № <xsl:value-of select="../@form_no"/>)
					</xsl:if>
				</span>
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
									<xsl:for-each select="c">
										<td>
											<img src="/images/null.gif" width="1px" height="1px"/>
										</td>
									</xsl:for-each>
								</tr>
							</xsl:if>
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
						<xsl:for-each select="Form">

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
                    <!--xsl:value-of select="format-number(v/@val div 1000, '# ##0,00','buh')"/-->
                    <xsl:value-of select="format-number(js:GetMult(string(v/@val)) div 1000, '# ##0,00','buh')"/>
									</td>
								</xsl:for-each>
							</tr>
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
			<br/>
			<br/>
			<span class="data_comment">
				ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и могут не отражать абсолютную актуальность и достоверность информации. Дополнительную информацию можно получить в разделах (<a href="#" onclick="setclass(38,10)">«Бухгалтерская отчетность»</a> или <a href="#" onclick="setclass(42,11)">«Ежеквартальные отчеты»</a>)
			</span>
		</xsl:if>
		<xsl:if test="headers[position()=last()]">
			<span class="data_comment">по данным неконсолидированной бухгалтерской отчетности эмитента</span>
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
										<xsl:value-of select="@yn"/>
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
									<span style="font-weight:bold;">
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
									<td style="text-align:right;">
										<xsl:choose>
											<xsl:when test ="@val=-0.001">-</xsl:when>
											<xsl:otherwise>
                        
                        <xsl:value-of select="format-number(js:GetMult(string(@val)) div 1000, '# ##0,00','buh')"/>
											</xsl:otherwise>
										</xsl:choose>
									</td>
								</xsl:for-each>
							</tr>
						</xsl:for-each>


					</xsl:if>
				</xsl:for-each>
			</table>
			
			<br/>
			<span class="data_comment">
				<xsl:for-each select="comments"	>
					<span class="uin">
						<xsl:value-of select="@idx"/>
					</span>
					<xsl:value-of select="tt/@comment"/>
					<br/>
				</xsl:for-each>
			</span>
			<span class="data_comment limitation">
				ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и могут не отражать абсолютную актуальность и достоверность информации. Дополнительную информацию можно получить в разделах (<a href="#" onclick="setclass(38,10)">«Бухгалтерская отчетность»</a> или <a href="#" onclick="setclass(42,11)">«Ежеквартальные отчеты»</a>)
			</span>


		</xsl:if>
		<script language="javascript" type="text/javascript">
		<![CDATA[ 
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
            d.style.width="323px";
			d.style.height="314px";
			document.body.appendChild(d);
            $("html").click(closeselector);
			
        }else{
            closeselector()
            
        }
        if(getObj("ddselector")){
            var aData=$("#ds_date").val().split(",");
            Sel="," + $("#inpselector").val()+",";
			var aND=$("#ds_name").val().split(",");
			var d1=getObj("dds");
			
			buff="<div id=\"dds\" style=\"overflow:auto;background-color:#FFF; position:relative;height:280px; width:322px;\">"
            for(var i=0; i < aData.length; i++){
                if(aData[i].length>0){
                    buff+="<input onclick=\"checkchecked(event);\" type=\"checkbox\" id=\"chb" + i + "\"" + ((Sel.indexOf("," + aData[i] + ",")>=0)?" checked" : "") + " value=\"" + TrimStr(aData[i]) + "\"/>" + TrimStr(aND[i]) + "<br/>";
                }    
            }
            buff+="</div><div style=\"text-align:center\"><input type=\"button\" class=\"btns blue\" value=\"Выбрать\" onclick=\"doSelect()\"/></div>";
			d.innerHTML=buff
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
				if(aYears[i].substr(aYears[i].length-4,4)>1998){
					buff+=aYears[i]+",";
				}	
			}
			$("#inpselector").val(buff);
			
		}
        $("input:checkbox").each(function(i){
			if($("#curr_selector").find("input:radio:checked").val()-3==0){
				if(this.value.substr(this.value.length-4,4)<1999){
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
						if(this.value.substr(this.value.length-4,4)<1999){
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
			if(this.value.substr(this.value.length-4,4)<1999){
				cantEuro=true;
				
			}
        });
		getObj("r3").disabled=cantEuro;
        $("#inpselector").val(buff);
        document.body.removeChild(getObj("ddselector"));
		goform($("#fn").val(),$("#iss").val());
    }
	function goform(fn,iss){
		period=$("#inpselector").val();
		if(fn-$("#fn").val()!=0){
			period="";
		}
		var aData=period.split(",");
		period="";
		for(var i=0; i < aData.length-1; i++){
			if(aData.length>0){
				period+=aData[i].substr(aData[i].length-4,4) + ((aData[i].length==4)?"4":aData[i].substr(0,1)/3) + ","
			}	
			
		}
		
		period=(period==",")?"":period;
		currency=$("#curr_selector").find("input:radio:checked").val(),
		showClock();
    
    $("#tab_content").load("/Tab/?id=53&ticker=" + $("#iss").val() + "&per=" + period + "&curr=" + currency + "&cons=" + fn,
        function (data) {
                hideClock();
        });
	}
	$(document).ready(function(){
		
		xls_params={"per": getYears(),"curr":$("#curr_selector").find("input:radio:checked").val(),"iss":$("#iss").val(),"fn":$("#fn").val(),"module" : "rsbu/skrin/","x":Math.random()}
		var cantEuro=false;
		aYears=$("#inpselector").val().split(",");
			for(var i=0; i < aYears.length; i++){
				if(aYears[i].substr(aYears[i].length-4,4)<1999 && TrimStr(aYears[i]).length>0){
					cantEuro=true;
				}	
			}
		getObj("r3").disabled=cantEuro;
	});
	function getYears(){
		var years=$("#inpselector").val();
		var aYears=years.split(",");
		years="";
		for(var i=0; i < aYears.length-1; i++){
			if(aYears.length>0){
				years+=aYears[i].substr(aYears[i].length-4,4) + ((aYears[i].length==4)?"4":aYears[i].substr(0,1)/3) + ","
			}	
			
		}
		years=(years==",")?"":years;
		return years
	}

				]]>
</script>
</xsl:template>
</xsl:stylesheet>
