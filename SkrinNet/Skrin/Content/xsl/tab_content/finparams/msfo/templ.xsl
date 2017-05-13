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
		<form id="frm">
			<table id="curr_selector" onclick="checkchecked(event)">
				<tr>
					<td style="border:none;background-color:#f6f7fa ;">Период:</td>
          <td style="border:none;background-color:#f6f7fa ;">
            <!--<input type="text" id="inpselector" readonly="readonly" class="system_form" onclick="showchbselector(event)" style="width:197px;"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:if test="@sel=1"><xsl:value-of select="@sname"/>,</xsl:if></xsl:for-each></xsl:attribute></input><img id="sel_div_btn" alt="" onclick="showchbselector(event)" src="/images/select_bottom_small2.gif" style="border:solid 1px #c0c0c0; padding:4px 3px 3px 3px; vertical-align:bottom; cursor:pointer;"/>-->
            <select  name="per_sel" id="per_sel" class="system_form" onchange="doSelector(this.value);">
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
          </td>

					<td style="display:none">Валюта: </td>
					<xsl:for-each select="currency">
						<td  style="display:none">
							<input type="radio" name="currency"><xsl:attribute name="id">r<xsl:value-of select="@id"/></xsl:attribute><xsl:attribute name="value"><xsl:value-of select="@id"/></xsl:attribute><xsl:if test="@sel=1"><xsl:attribute name="checked">checked</xsl:attribute></xsl:if></input><xsl:value-of select="@name"/>
						</td>
					</xsl:for-each>
				
				</tr>
			</table>
			<input type="hidden" id="ds_date"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:value-of select="@sname"/>,</xsl:for-each></xsl:attribute></input>
			<input type="hidden" id="ds_name"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:value-of select="@name"/>,</xsl:for-each></xsl:attribute></input>
			<input type="hidden" id="fn"><xsl:attribute name="value"><xsl:value-of select="//ed/@val"/></xsl:attribute></input>
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
					<xsl:value-of select="@name"/>,
				</xsl:if>
			</xsl:for-each><br/><br/>
		</xsl:if>
		<xsl:for-each select="f">
			<span class="subcaption">
				<xsl:choose>
					<xsl:when test="@form_no=1">
						БУХГАЛТЕРСКИЙ БАЛАНС
					</xsl:when>
					<xsl:when test="@form_no=2">
						ОТЧЕТ О ПРИБЫЛЯХ И УБЫТКАХ
					</xsl:when>

				</xsl:choose>
			</span>
			<xsl:choose>
				<xsl:when test="@form_no=1">
					<!--font class="data_comment">* Для расчета используется значение курса ЦБ на последнюю дату отчетного периода</font-->
					<br/>
					<b>Consolidated balance sheet</b>

				</xsl:when>
				<xsl:when test="@form_no=2">
					<br/>
					<b>Consolidated Statements of Income</b>
				</xsl:when>

			</xsl:choose>
			<table width="100%" cellpadding="0" cellspacing="0" border="0">
				<xsl:for-each select="LA">
					<xsl:if test="position()=1">
						<tr>
							<td class="table_caption_left">Наименование показателя</td>
							
							<xsl:for-each select="dta">
								<td class="table_caption" align="center">
									<xsl:value-of select="@yn"/>
									
								</td>
							</xsl:for-each>
						</tr>
						<tr>
							<td class="table_shadow_left">
								<img src="/images/null.gif" width="1" height="1" border="0"/>
							</td>
							
							<xsl:for-each select="dta">
								<td class="table_shadow">
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
							</xsl:for-each>
						</tr>

					</xsl:if>
					<xsl:if test="//f/@form_no=1 and @line_code=30">
						<tr>
							<td>
								<b>ASSETS</b>
							</td>
							<xsl:for-each select="dta">
								<td>
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
							</xsl:for-each>
						</tr>
					</xsl:if>
					<xsl:if test="//f/@form_no=2 and @line_code=100">
						<tr>
							<td>
								<b>EQUITY &amp; LIABILITIES</b>
							</td>
							<xsl:for-each select="dta">
								<td>
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
							</xsl:for-each>
						</tr>
					</xsl:if>


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
								<xsl:for-each select="dta">
									<td align="right">                  
										<xsl:choose>
											<xsl:when test ="js:GetMult(string(@val))=-0.001">-</xsl:when>
											<xsl:otherwise>
												<xsl:value-of select="format-number(js:GetMult(string(@val)),'# ##0,00','buh')"/>
											</xsl:otherwise>
										</xsl:choose>
									</td>
								</xsl:for-each>
							</tr>
						</xsl:for-each>

			</table>
			
		</xsl:for-each>
			<span class="data_comment limitation">ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и могут не отражать абсолютную актуальность и достоверность информации. Дополнительную информацию можно получить в разделах (<a href="#" onclick="setclass(39,10)">«Финансовая отчетность по МСФО»</a> или «Ежеквартальные отчеты»)</span>
		
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
            iframepost({"id": $("#tab_id").val(), "ticker":$("#iss").val(), "per" :$("#per_sel").val(),"fn" : $("#fn").val(), "xls" : "1","cons" : $("#fn").val()}, "/Tab/", "reports");
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
			d.style.height="300px";
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
			
			buff="<div id=\"dds\" style=\"overflow:auto;background-color:#FFF; position:relative;height:269px; width:219px;\">"
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
		/*if($("input:checkbox:checked").length>5 && src.id.substr(0,3)=="chb"){
			showwin("critical","Можно выбрать не более 5 периодов.<br/><br/>",2000)
			getObj(src.id).checked=false;
		}*/
		if(src.id.indexOf("chb")==0){
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
				if(getYear(aYears[i])>1998){
					buff+=aYears[i]+",";
				}	
			}
			$("#inpselector").val(buff);
			
		}
        $("input:checkbox").each(function(i){
			if($("#curr_selector").find("input:radio:checked").val()-3==0){
				if(getYear(this.value)<1999){
					this.disabled=true;
					this.checked=false;
				}
			}
			if(this.checked){
				cnt++;
			}
			
        });
         $("input:checkbox").each(function(i){
                this.disabled=false;
				if($("#curr_selector").find("input:radio:checked").val()-3==0){
					if(getYear(this.value)<1999){
						this.disabled=true;
						this.checked=false;
					}
				}
        })

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
	function goform(fn,iss){
		
		period=(period==",")?"":period;
		currency=$("#curr_selector").find("input:radio:checked").val(),
		showClock();

           $.get("/tab/", { "id": 50, "ticker": ISS, "PG": 1,"per" : $("#per_sel").val(),"first" : -1, "curr" : currency,"fn":fn }, function (data) {
            hideClock();        
            $("#tab_content").html(data);                           
        }, "html").fail(function (jqXHR, textStatus, errorThrown) {
            hideClock();
            $("#tab_content").html(textStatus);
        });
	}
  
  function doSelector(per)
  {
  showClock();
  	currency=$("#curr_selector").find("input:radio:checked").val(),
           $.get("/tab/", { "id": 50, "ticker": ISS, "PG": 1,"per" : per,"first" : -1, "curr" : currency}, function (data) {
            hideClock();        
            $("#tab_content").html(data);                           
        }, "html").fail(function (jqXHR, textStatus, errorThrown) {
            hideClock();
            $("#tab_content").html(textStatus);
        });
  }
  
	$(document).ready(function(){
		var cantEuro=false;
		aYears=$("#inpselector").val().split(",");
			for(var i=0; i < aYears.length; i++){
				if(getYear(aYears[i])<1999 && TrimStr(aYears[i]).length>0){
					cantEuro=true;
				}	
			}
		getObj("r3").disabled=cantEuro;
		period="";
		for(var i=0; i < aYears.length; i++){
			if(aYears[i].length>0){
				period+=aYears[i].substr(aYears[i].length-4,4) + ((aYears[i].length==4)?"4":aYears[i].substr(0,1)/3) + ","
			}
		}
		period=(period==",")?"":period;
		xls_params={"per": period,"iss":$("#iss").val(),"module" : "finparams/msfo/","x":Math.random()};
	});

				]]>
</script>
</xsl:template>
</xsl:stylesheet>
