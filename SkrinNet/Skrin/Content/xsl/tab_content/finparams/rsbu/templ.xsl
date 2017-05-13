<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">
	<xsl:import href="../../../../xsl/common.xsl"/>
	<msxsl:script language="JScript" implements-prefix="js">
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
    function FormatLineCode(nl,cnt){
    <!--var str=String(nl.nextNode().text);-->
    var str = nl;
		<!--var ntypes=String(cnt.nextNode().text);-->
    var ntypes = cnt;
		str=str.substr(0,str.length-1);
		var aCodes=str.split("/");
		var aCodesMod=new Array();
		var aTmp = new Array();
		var index=-1
		var ret_val="";
		for(var i=0; i &lt; aCodes.length; i++){
		aTmp=aCodes[i].split("_");
		index=aScan(aCodesMod,aTmp[0],0)
		if(index &lt; 0){
    aCodesMod[aCodesMod.length]=aTmp;
    }else{
    aCodesMod[index][1] = aCodesMod[index][1] + "," + aTmp[1];
    }
    }
    for(var i=0; i &lt; aCodesMod.length; i++){
    ret_val = ret_val +  ((i==0)?"":" / ") + aCodesMod[i][0]+((ntypes &gt; 1)? "&lt;span class=\"uin\"&gt;" + aCodesMod[i][1] + "&lt;/span&gt;":"");
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
			<xsl:if test="//cons/@val=1">

        <table cellpadding="4" style="margin-bottom:8px;">
          <tr>
            <xsl:if test="//ed/@val=0">
              <td class="content_menu" valign="top">
                неконсолидированная бухгалтерская отчетность
              </td>
              <td style="cursor:pointer;background-color:#EEE;" class="content_menu_sel" >
                <xsl:attribute name="onclick">
                  goform(1,'<xsl:value-of select="//@iss"/>')
                </xsl:attribute>
                консолидированной бухгалтерская отчетность
              </td>

            </xsl:if>
            <xsl:if test="//ed/@val=1">
              <td style="cursor:pointer;background-color:#EEE;" class="content_menu_sel" valign="top">
                <xsl:attribute name="onclick">
                  goform(0,'<xsl:value-of select="//@iss"/>')
                </xsl:attribute>
                неконсолидированная бухгалтерская отчетность
              </td>
              <td class="content_menu">
                консолидированной бухгалтерская отчетность
              </td>
            </xsl:if>
          </tr>
        </table>
		</xsl:if>
			
		<form id="frm">
			<table id="curr_selector" onclick="checkchecked(event);">
				<tr>
					<td  style="border:none;background-color:#f6f7fa ;">Период:</td>
					<td  style="border:none;background-color:#f6f7fa ;">
						<!--<input type="text" id="inpselector" readonly="readonly" class="system_form" onclick="showchbselector(event)" style="width:197px;"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:if test="@sel=1"><xsl:value-of select="@sname"/>,</xsl:if></xsl:for-each></xsl:attribute></input><img id="sel_div_btn" alt="" onclick="showchbselector(event)" src="/images/select_bottom_small2.gif" style="border:solid 1px #c0c0c0; padding:4px 3px 3px 3px; vertical-align:bottom; cursor:pointer;"/>-->
            <input type="text" id="inpselector" readonly="readonly" class="system_form dds" onclick="showchbselector(event)" style="width:300px;"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:if test="@sel=1"><xsl:value-of select="@sname"/>,</xsl:if></xsl:for-each></xsl:attribute></input>
				  	<input type="hidden" id="dtselector"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:if test="@sel=1"><xsl:value-of select="@yq"/>,</xsl:if></xsl:for-each></xsl:attribute></input>
						<img id="sel_div_btn" alt="" onclick="showchbselector(event)" src="/images/select_bottom_small2.gif" class="dds_butt"/>
          </td>

					<td  style="border:none;background-color:#f6f7fa ;">Валюта: </td>
					<xsl:for-each select="currency">
						<td  style="border:none;background-color:#f6f7fa ;">
							<input type="radio" name="currency"><xsl:attribute name="id">r<xsl:value-of select="@id"/></xsl:attribute><xsl:attribute name="value"><xsl:value-of select="@id"/></xsl:attribute><xsl:if test="@sel=1"><xsl:attribute name="checked">checked</xsl:attribute></xsl:if><xsl:attribute name="onclick">goform(<xsl:value-of select="//ed/@val"/>,'<xsl:value-of select="//@iss"/>');</xsl:attribute></input><xsl:value-of select="@name"/>
						</td>
					</xsl:for-each>
					<!--td>
						<input type="button" id="goform_btn" value="Показать" class="btns blue"><xsl:attribute name="onclick">goform(<xsl:value-of select="//ed/@val"/>,'<xsl:value-of select="//@iss"/>');</xsl:attribute></input>
					</td-->
				</tr>
			</table>
			<input type="hidden" id="ds_date"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:value-of select="@sname"/>,</xsl:for-each></xsl:attribute></input>
			<input type="hidden" id="ds_name"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:value-of select="@name"/>,</xsl:for-each></xsl:attribute></input>
			<input type="hidden" id="fn"><xsl:attribute name="value"><xsl:value-of select="//ed/@val"/></xsl:attribute></input>
			<input type="hidden" id="iss"><xsl:attribute name="value"><xsl:value-of select="//@iss"/></xsl:attribute></input>
			<input type="hidden" id="gks"><xsl:attribute name="value"><xsl:value-of select="//gks/@val"/></xsl:attribute></input>
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
					<xsl:value-of select="@name"/>,
				</xsl:if>
			</xsl:for-each><br/>
			Валюта:<xsl:for-each select="currency">
					<xsl:if test="@sel=1">
						<xsl:value-of select="@name"/>
					</xsl:if>
		</xsl:for-each><br/><br/>
		</xsl:if>
		<xsl:for-each select="f">
			<span class="subcaption">
				<xsl:value-of select="@form_header"/>
				
			</span>
			<table width="100%" cellpadding="0" cellspacing="0" border="0">
				<xsl:for-each select="headers/LA">
					<xsl:if test="position()=1">
						<tr>
							<td class="table_caption_left">Наименование показателя</td>
							<td class="table_caption_left">Код строки</td>
							<xsl:for-each select="dta">
								<td class="table_caption" align="center">
									<xsl:value-of select="@yn"/>
									
								</td>
							</xsl:for-each>
						</tr>
						
						<xsl:if test="//cur">
							<tr>
								<td>Курс <xsl:value-of select="//kurs_header/@val"/>
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

					</xsl:if>
					<xsl:choose>
						<xsl:when test="@id=6">
							<tr bgcolor="#EEEEEE">
								<td>
									<b>АКТИВ</b>
								</td>
								<td>
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
								<xsl:for-each select="dta">
									<td>
										<img src="/images/null.gif" width="1" height="1" border="0"/>
									</td>
								</xsl:for-each>
							</tr>
						</xsl:when>
						<xsl:when test="@id=20">
							<tr bgcolor="#EEEEEE">
								<td>
									<b>ПАССИВ</b>
								</td>
								<td>
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
								<xsl:for-each select="dta">
									<td>
										<img src="/images/null.gif" width="1" height="1" border="0"/>
									</td>
								</xsl:for-each>
							</tr>
						</xsl:when>
                        <xsl:when test="count(../@parent_name)&gt;0 and ../@parent_name != ''">
                            <tr bgcolor="#EEEEEE">
                                <td>
                                    <b>
                                        <xsl:value-of disable-output-escaping="yes" select="../@parent_name"/>
                                    </b>
                                </td>
                                <td>
                                    <img src="/images/null.gif" width="1" height="1" border="0"/>
                                </td>
                                <xsl:for-each select="dta">
                                    <td>
                                        <img src="/images/null.gif" width="1" height="1" border="0"/>
                                    </td>
                                </xsl:for-each>
                            </tr>
                        </xsl:when>
                    </xsl:choose>
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
									
									<xsl:if test="../../@form_no=1">
                                        <xsl:if test="count(../../@form_tp)=0 and (contains(@name,'Итого') or contains(@name,'ИТОГО'))">
										<span style="text-transform:uppercase">
									        <xsl:value-of disable-output-escaping ="yes" select="../@name"/>
										</span>
                                        </xsl:if>
                                        <xsl:if test="count(../../@form_tp)&gt;0">
                                            <xsl:value-of disable-output-escaping ="yes" select="../@name"/>
                                        </xsl:if>
                                    </xsl:if>
									<xsl:if test="../../@form_no &gt; 1">
										<xsl:value-of disable-output-escaping ="yes" select="@name"/>
									</xsl:if>
									<xsl:if test="not(contains(@name,'Итого') or contains(@name,'ИТОГО') ) and ../../@form_no=1 and count(../../@form_tp)=0">
										<xsl:value-of disable-output-escaping ="yes" select="@name"/>
									</xsl:if>

								</td>
								<td class="table_item" align="center" >
									<xsl:value-of disable-output-escaping ="yes" select="js:FormatLineCode(string(@line_agg),string(//ntypes/@val))"/>
								</td>

								<xsl:for-each select="dta">
                                    <td class="table_item" align="right">
										<xsl:choose>
											<xsl:when test ="js:GetMult(string(@val))=-0.001">-</xsl:when>
											<xsl:otherwise>
                                                <xsl:if test="count(@dv)&gt;0">
                                                    <xsl:value-of select="format-number(@val div @dv,'# ##0,00','buh')"/>
                                                </xsl:if>
                                                <xsl:if test="count(@dv)=0">                                                 
                                                    <!--<xsl:value-of select="format-number(@val div 1000,'# ##0,00','buh')"/>-->
                                                  <xsl:value-of select="format-number(js:GetMult(string(@val)) div 1000, '# ##0,00', 'buh')"/>
                                                </xsl:if>
											</xsl:otherwise>
										</xsl:choose>
									</td>
								</xsl:for-each>
							</tr>
						</xsl:for-each>

			</table>
		
		</xsl:for-each>
		<span class="data_comment">
			<xsl:for-each select="comments"	>
				<span class="uin">
					<xsl:value-of select="@idx"/>
				</span>
				<xsl:value-of select="tt/@comment"/>
				<br/>
			</xsl:for-each>
		</span>
		<xsl:if test="//gks/@val=0">
			<span class="data_comment limitation">ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и могут не отражать абсолютную актуальность и достоверность информации. Дополнительную информацию можно получить в разделах (<a href="#" onclick="setclass(38,10)">«Бухгалтерская отчетность по РСБУ»</a> или «Ежеквартальные отчеты»)</span>
		</xsl:if>
		<xsl:if test="//gks/@val=1">
			<span class="data_comment limitation">ВНИМАНИЕ: Данные ГМЦ Росстат. В связи с особенностями функционирования и обновления указанного источника информации АО "СКРИН" не может гарантировать абсолютную актуальность и достоверность данных.</span>
		</xsl:if>



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
            		
           
            iframepost({"id": $("#tab_id").val(), "curr":$("#curr_selector").find("input:radio:checked").val(), "ticker":$("#iss").val(),"per" :getYears(),"fn" : $("#fn").val(), "xls" : "1","cons" : $("#fn").val()}, "/Tab/", "reports");
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
			      d.style.height="314px";
			      document.body.appendChild(d);
            $(d).click(function(e){
              if (window.event){
	                e = window.event;
              }
             if (e.stopPropagation) {
               e.stopPropagation();
             }else{
              e.cancelBubble=true;
             }
             });
            $("body").click(function(){
              closeselector()
              });
			
        }else{
            closeselector()
            
        }
        if(getObj("ddselector")){
            var aData=$("#ds_date").val().split(",");
            Sel="," + $("#inpselector").val()+",";
			var aND=$("#ds_name").val().split(",");
			var d1=getObj("dds");
			
			buff="<div id=\"dds\" style=\"overflow:auto;background-color:#FFF; padding-left:6px; position:relative;height:269px; width:" + ($("#inpselector").width() + $("#sel_div_btn").width() + 16)  + "px;\">"
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
	function getYear(inp_str){
		var ret_val="";
		if(inp_str.length==4){
			ret_val=inp_str;
		}else{
			ret_val=inp_str.substr(inp_str.length-4,4);
		}
		return ret_val
	}
    function doSelect(){
        var buff="";
		var cantEuro=false;
        $("input:checkbox:checked").each(function(i){
            buff+=this.value + ",";
			if(getYear(this.value)<1999){
				cantEuro=true;
			}
        });
		getObj("r3").disabled=cantEuro;
        $("#inpselector").val(buff);
        document.body.removeChild(getObj("ddselector"));
		goform($("#fn").val(),$("#iss").val());
    }
    function getYears(){
      period=$("#inpselector").val();
       alert(period)
		  if(fn-$("#fn").val()!=0){
			  period="";
		  }
		  var aData=period.split(",");
		  period="";
		  for(var i=0; i < aData.length-1; i++){
			  if($("#gks").val()==0){
				  period+=aData[i].substr(aData[i].length-4,4) + ((aData[i].length==4)?"4":aData[i].substr(0,1)/3) + ","
			  }else{
				  period+=aData[i]+",";
			  }
		  }
		  period=(period==",")?"":period;
     
      return period;
    }
	function goform(fn,iss){
		period=$("#inpselector").val();
		if(fn-$("#fn").val()!=0){
			period="";
		}
		var aData=period.split(",");
		
		currency=$("#curr_selector").find("input:radio:checked").val(),
		showClock();
	      $.get("/tab/", { "id": 49, "ticker": ISS, "PG": 1,"per" : getYears(),"first" : -1, "curr" : currency,"fn":fn }, function (data) {
            hideClock();
            $("#tab_content").html(data);                           
        }, "html").fail(function (jqXHR, textStatus, errorThrown) {
            hideClock();
            $("#tab_content").html(textStatus);
        });
	}
	$(document).ready(function(){
		xls_params={"per": getYears(),"curr":$("#curr_selector").find("input:radio:checked").val(),"iss":$("#iss").val(),"fn":$("#fn").val(), "module" : "finparams/rsbu/","x":Math.random()}
		var cantEuro=false;
		aYears=$("#inpselector").val().split(",");
			for(var i=0; i < aYears.length; i++){
				if(getYear(aYears[i])<1999 && TrimStr(aYears[i]).length>0){
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
			if($("#gks").val()==0){
				years+=aYears[i].substr(aYears[i].length-4,4) + ((aYears[i].length==4)?"4":aYears[i].substr(0,1)/3) + ","
			}else{
				years+=aYears[i]+",";
			}
			
		}
		years=(years==",")?"":years;
		return years
	}
				]]>
</script>
</xsl:template>
</xsl:stylesheet>
