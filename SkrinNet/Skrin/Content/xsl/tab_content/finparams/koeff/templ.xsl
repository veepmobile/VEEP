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
			<table id="curr_selector" onclick="checkchecked(event);">
				<tr>
					<td style="border:none;background-color:#f6f7fa ;">Период:</td>
					<td style="border:none;background-color:#f6f7fa ;">
						<!--<input type="text" id="inpselector" readonly="readonly" class="system_form" onclick="showchbselector(event)" style="width:197px;"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:if test="@sel=1"><xsl:value-of select="@sname"/>,</xsl:if></xsl:for-each></xsl:attribute></input><img id="sel_div_btn" alt="" onclick="showchbselector(event)" src="/images/select_bottom_small2.gif" style="border:solid 1px #c0c0c0; padding:4px 3px 3px 3px; vertical-align:bottom; cursor:pointer;"/>-->
             <input type="text" id="inpselector" readonly="readonly" class="system_form dds" onclick="showchbselector(event)" style="width:300px;"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:if test="@sel=1"><xsl:value-of select="@sname"/>,</xsl:if></xsl:for-each></xsl:attribute></input>
				  	<input type="hidden" id="dtselector"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:if test="@sel=1"><xsl:value-of select="@yq"/>,</xsl:if></xsl:for-each></xsl:attribute></input>
						<img id="sel_div_btn" alt="" onclick="showchbselector(event)" src="/images/select_bottom_small2.gif" class="dds_butt"/>
       	</td>

					
				</tr>
			</table>
			<input type="hidden" id="ds_date"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:value-of select="@sname"/>,</xsl:for-each></xsl:attribute></input>
			<input type="hidden" id="ds_name"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:value-of select="@name"/>,</xsl:for-each></xsl:attribute></input>
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
		<table width="100%" cellpadding="0" cellspacing="0" border="0">
			<xsl:for-each select="section">
					<xsl:if test="position()=1">
						<tr>
							<td class="table_caption_left">Наименование показателя</td>
							<xsl:for-each select="lines[1]/dta">
								<td class="table_caption" align="center">
									<xsl:value-of select="@yn"/>
									
								</td>
							</xsl:for-each>
						</tr>
						<tr>
							<td class="table_shadow_left">
								<img src="/images/null.gif" width="1" height="1" border="0"/>
							</td>
							<xsl:for-each select="lines[1]/dta">
								<td class="table_shadow">
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
							</xsl:for-each>
						</tr>

					</xsl:if>
				<tr>
					<td style="background-color:#EEE">
						<b>
							<xsl:value-of select="@name"/>
						</b>
					</td>
					<xsl:for-each select="lines[1]/dta">
						<td style="background-color:#EEE">
							<img src="/images/null.gif" width="1" height="1" border="0"/>
						</td>
					</xsl:for-each>
				</tr>
				<xsl:for-each select="lines">

					<tr>
						<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">
								<xsl:attribute name="bgColor">#F0F0F0</xsl:attribute>
							</xsl:when>
							<xsl:otherwise>
								<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
							</xsl:otherwise>
						</xsl:choose>
						<td class="table_item_left" width="330">
							
								<xsl:value-of disable-output-escaping ="yes" select="@name"/>
							
						</td>
						<xsl:for-each select="dta">
							<td align="right">
								<xsl:choose>
									<xsl:when test ="@val=-0.001">-</xsl:when>
									<xsl:otherwise>
										<xsl:choose>
											<xsl:when test="../@num_prec=0">
												<xsl:value-of select="format-number(@val,'# ##0','buh')"/>
											</xsl:when>
											<xsl:when test="../@num_prec=2">
												<xsl:value-of select="format-number(@val,'# ##0,00','buh')"/>
											</xsl:when>
											<xsl:otherwise>
												<xsl:value-of select="@val"/>
											</xsl:otherwise>
										</xsl:choose>
										
									</xsl:otherwise>
								</xsl:choose>
							</td>
						</xsl:for-each>
					</tr>
				</xsl:for-each>
			</xsl:for-each>

		</table>
		
		
		<span class="data_comment limitation">
			<xsl:choose>
				<xsl:when test="//GKS/@val*1=0">
					ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и могут не отражать абсолютную актуальность и достоверность информации. Дополнительную информацию можно получить в разделах (<a href="#" onclick="setclass(38,10)">«Бухгалтерская отчетность»</a> или <a href="#" onclick="setclass(42,11)">«Ежеквартальные отчеты»</a>)<br/>
					<span class="ahref" onclick="showtxt('','','','/iss/modules/metod.htm')">Методика рассчета коэффициентов</span>
				</xsl:when>
				<xsl:otherwise>
					ВНИМАНИЕ: При расчете коэффициентов использовались данные ГМЦ Росстат. В связи с особенностями функционирования и обновления указанного источника информации АО "СКРИН" не может гарантировать абсолютную актуальность и достоверность данных. <span class="ahref" onclick="showtxt('','','','/iss/modules/metod.htm')">Методика рассчета коэффициентов</span>
				</xsl:otherwise>
			</xsl:choose>
		</span>
		
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
			$(function() {
					var sch = window.innerHeight ? window.innerHeight : document.documentElement.offsetHeight; 
					var scw = window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth; 
					var	ww,wh,top,left;
					ww=(scw<640)?scw-44:640;
					wh=(sch<480)?sch-30:480
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
			
			buff="<div id=\"dds\" style=\"overflow:auto;background-color:#FFF;padding-left:6px; position:relative;height:269px; width:" + ($("#inpselector").width() + $("#sel_div_btn").width() + 16)  + "px;\">"
            for(var i=0; i < aData.length; i++){
                if(aData[i].length>0){
                    buff+="<input onclick=\"checkchecked(event);\" type=\"checkbox\" id=\"chb" + i + "\"" + ((Sel.indexOf("," + aData[i] + ",")>=0)?" checked" : "") + " value=\"" + TrimStr(aData[i]) + "\"/>" + TrimStr(aND[i]) + "<br/>";
                }    
            }
            buff+="</div><div style=\"text-align:center\"><input type=\"button\" class=\"btns blue\" style=\"margin-bottom:4px\" value=\"Выбрать\" onclick=\"doSelect()\"/></div>";
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
        });
        $("#inpselector").val(buff);
        document.body.removeChild(getObj("ddselector"));
		goform(1,$("#iss").val());
    }
	function goform(fn,iss){
		var period=$("#inpselector").val();
		var aData=period.split(",");
		period="";
		for(var i=0; i < aData.length; i++){
			if(aData[i].length>0){
				period+=aData[i].substr(aData[i].length-4,4) + ((aData[i].length==4)?"4":aData[i].substr(0,1)/3) + ","

			}
		}
		
		period=(period==",")?"":period;
		currency=$("#curr_selector").find("input:radio:checked").val(),
		showClock();
	
           $.get("/tab/", { "id": 51, "ticker": ISS, "PG": 1,"per" : period,"first" : -1, "curr" : currency,"fn":fn }, function (data) {
            hideClock();           
            $("#tab_content").html(data);                           
        }, "html").fail(function (jqXHR, textStatus, errorThrown) {
            hideClock();
            $("#tab_content").html(textStatus);
        });
	}
	$(document).ready(function(){
		xls_params={"per": getYears(),"iss":$("#iss").val(),"module" : "finparams/koeff/","x":Math.random()}
	});
function getYears(){
		var years=$("#inpselector").val();
		var aYears=years.split(",");
		years="";
		for(var i=0; i < aYears.length-1; i++){
			if(aYears[i].length>0){
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
