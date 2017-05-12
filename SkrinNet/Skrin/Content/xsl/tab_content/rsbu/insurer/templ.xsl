<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                              xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">
    
	<xsl:import href="../../../../xsl/common.xsl"/>
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
                    <xsl:for-each select="fg">
                        <td valign="top">
                            <xsl:if test="@g!=@sg">
                                <xsl:attribute name="class">content_menu_sel</xsl:attribute>
                                <xsl:attribute name="style">cursor:pointer;background-color:#EEE;</xsl:attribute>
                                <xsl:attribute name="onclick">
                                    goform('<xsl:value-of select="@g"/>','<xsl:value-of select="//@iss"/>')
                                </xsl:attribute>
                            </xsl:if>
                            <xsl:if test="@g=@sg">
                                <xsl:attribute name="class">content_menu</xsl:attribute>
                            </xsl:if>
                            <xsl:for-each select="fn">
                                <xsl:value-of disable-output-escaping="yes" select="@form_nm"/><br/>
                            </xsl:for-each>
                        </td>
                    </xsl:for-each>
                </tr>
            </table>

            <xsl:if test="//cons/@val=1">
		        <table cellpadding="4">
			        <tr>
				        <xsl:if test="//fn/@val=1">
					        <td class="content_menu" valign="top">
						        неконсолидированная бухгалтерская отчетность
					        </td>
					        <td style="cursor:pointer;" class="content_menu_sel" >
						        <xsl:attribute name="onclick">
							        goform(3,'<xsl:value-of select="//@iss"/>')
						        </xsl:attribute>
                                консолидированная бухгалтерская отчетность
                            </td>

				        </xsl:if>
				        <xsl:if test="//fn/@val=3">
					        <td style="cursor:pointer;" class="content_menu_sel" valign="top">
						        <xsl:attribute name="onclick">
							        goform(1,'<xsl:value-of select="//@iss"/>')
						        </xsl:attribute>
						        неконсолидированная бухгалтерская отчетность
					        </td>
					        <td class="content_menu">
                                консолидированная бухгалтерская отчетность
                            </td>
				        </xsl:if>
			        </tr>
		        </table>
		    </xsl:if>
		
		<form id="frm">
			<table id="curr_selector" onclick="checkchecked(event);">
				<tr>
					<td>Период:</td>
					<td>
            <select id="per_sel" class="system_form" onchange="doSelect()">
              <xsl:for-each select="per">
                <option>
                  <xsl:attribute name="value">
                    <xsl:value-of select="@y"/>
                  </xsl:attribute>
                  <xsl:if test="@sel=1">
                    <xsl:attribute name="selected">selected</xsl:attribute>
                  </xsl:if>
                  <xsl:value-of select="@name"/>
                </option>
              </xsl:for-each>

            </select>
					</td>
                        <td>
                            Валюта:
                        </td>
                        <xsl:for-each select="currency">
                             <td>
                                <input type="radio" name="currency">
                                    <xsl:attribute name="id">
                                        <xsl:value-of select="@id"/>
                                    </xsl:attribute>
                                    <xsl:attribute name="value">
                                        <xsl:value-of select="@id"/>
                                    </xsl:attribute>
                                    <xsl:if test="@sel=1">
                                        <xsl:attribute name="checked">checked</xsl:attribute>
                                    </xsl:if>
                                    <xsl:attribute name="onclick">
                                        goform(<xsl:value-of select="//fn/@val"/>,'<xsl:value-of select="//@iss"/>');
                                    </xsl:attribute>
                                    <xsl:value-of select="@name"/>
                                </input>
                            </td>
                        </xsl:for-each>
<!--                    
                    <xsl:if test="//fn/@val!=2">
                    </xsl:if>
-->                    
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
<!--//-->        
		</xsl:if>
        
		<xsl:if test="//@PDF=0">
			Период:<xsl:for-each select="per">
				<xsl:if test="@sel=1">
					<xsl:value-of select="@name"/>,
				</xsl:if>
			</xsl:for-each>
		</xsl:if>
    
		<xsl:for-each select="forms">
            <xsl:variable name="f_no" select="@form_no" />

            <table width="100%" cellpadding="1" cellspacing="0" >
                <tr><td><img src="/images/null.gif" width="1" height="5" border="0"/></td></tr>
                <tr><td class="subcaption"><xsl:value-of select="@form_header"/></td></tr>
            </table>

            <xsl:for-each select="//secs[@form_no=$f_no]">
                <xsl:variable name="s_no" select="@id" />
                <xsl:variable name="h_cnt" select="//cols[@form_no=$f_no and @section_no=$s_no]/@row_cnt" />
                <table width="100%" cellpadding="1" cellspacing="0" >
                    <xsl:if test="@header !=''">
                        <tr><td><img src="/images/null.gif" width="1" height="3" border="0"/></td></tr>
                        <tr><td class="subcaption_prim"><xsl:value-of select="@header"/></td></tr>
                    </xsl:if>
                    <tr><td align="left"><xsl:value-of select="@dim"/></td></tr>
                </table>
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td class="table_caption_left">
                            <xsl:attribute name="rowspan">
                                <xsl:value-of select="$h_cnt"/>
                            </xsl:attribute>
                            Наименование показателя
                        </td>
                        <td class="table_caption_left">
                            <xsl:attribute name="rowspan">
                                <xsl:value-of select="$h_cnt"/>
                            </xsl:attribute>
                            Код строки
                        </td>
                        <xsl:for-each select="//cols[@form_no=$f_no and @section_no=$s_no and @hrow_no=1]">
                            <td class="table_caption" align="center">
                                <xsl:attribute name="rowspan">
                                    <xsl:value-of select="@row_span"/>
                                </xsl:attribute>
                                <xsl:attribute name="colspan">
                                    <xsl:value-of select="@col_span"/>
                                </xsl:attribute>
                                <xsl:value-of disable-output-escaping="yes" select="@colname"/>
                            </td>
                        </xsl:for-each>
                    </tr>
                    <xsl:if test="$h_cnt&gt;1">
                        <tr>
                            <xsl:for-each select="//cols[@form_no=$f_no and @section_no=$s_no and @hrow_no=2]">
                                <td class="table_caption" align="center">
                                    <xsl:value-of disable-output-escaping="yes" select="@colname"/>
                                </td>
                            </xsl:for-each>
                        </tr>
                    </xsl:if>
                  <!--tr>
                        <td class="table_shadow_left">
                            <img src="/images/null.gif" width="1" height="1" border="0"/>
                        </td>
                        <td class="table_shadow_left">
                            <img src="/images/null.gif" width="1" height="1" border="0"/>
                        </td>
                        <xsl:for-each select="//cols[@form_no=$f_no and @section_no=$s_no and count(@col_span)=0]">
                            <td class="table_shadow">
                                <img src="/images/null.gif" width="1" height="1" border="0"/>
                            </td>
                        </xsl:for-each>
                    </tr-->

<!--                    <xsl:if test="//fn/@val != 2 and //currency[@id = 1]/@sel = 0"> -->
                    <xsl:if test="//currency[@id = 1]/@sel = 0">
                        <tr>
                            <td class="table_item_left">
                                Курс <xsl:value-of select="//kurs_header/@val"/>
                            </td>
                            <td>
                                <xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
                            </td>
                            <xsl:for-each select="//cols[@form_no=$f_no and @section_no=$s_no and count(@col_span)=0]">
                                <td class="table_item" align="right">
                                    <xsl:value-of select="@kurs"/>
                                </td>
                            </xsl:for-each>
                        </tr>
                        <tr>
                            <td class="table_item_left">
                                Курс на дату
                            </td>
                            <td>
                                <xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
                            </td>
                            <xsl:for-each select="//cols[@form_no=$f_no and @section_no=$s_no and count(@col_span)=0]">
                                <td class="table_item" align="right">
                                    <xsl:value-of select="@ld"/>
                                </td>
                            </xsl:for-each>
                        </tr>
                    </xsl:if>
                    <xsl:call-template name="tbl_blok">
                        <xsl:with-param name="f_no" select="$f_no"/>
                        <xsl:with-param name="s_no" select="$s_no"/>
                        <xsl:with-param name="p_no" select="0"/>
                    </xsl:call-template>
                </table>
                <xsl:if test="count(//c[@form_no=$f_no and @section_no=$s_no]) != 0">
                    <div class="data_comment">
                        <xsl:for-each select="//c[@form_no=$f_no and @section_no=$s_no]">
                            <span class="uin"><xsl:value-of select="@no"/></span><xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text><xsl:value-of disable-output-escaping="yes" select="@text"/><br/>
                        </xsl:for-each>
                    </div>
                </xsl:if>
            </xsl:for-each>
        </xsl:for-each>
        
   	    <div class="data_comment limitation">ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и могут не отражать абсолютную актуальность и достоверность информации. Дополнительную информацию можно получить в разделах (<a href="#" onclick="setclass(38,10)">«Бухгалтерская отчетность по РСБУ»</a> или «Ежеквартальные отчеты»)</div>

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
            		
           
            iframepost({"id": $("#tab_id").val(), "curr":getCurr(), "ticker":$("#iss").val(),"per" :$("#per_sel").val(),"fn" : $("#fn").val(), "xls" : "1"}, "/Tab/", "reports");
        });
        function getCurr(){
          var currency=1;
    	    if(fn != 2){
			    currency=$("#curr_selector").find("input:radio:checked").val();
		      }
          if(String(currency)=="undefined"){
            currency=1;
          }
        return currency;
        }
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
            var aNm=$("#ds_name").val().split(",");
            Sel="," + $("#inpselector").val()+",";
            for(var i=0; i < aData.length; i++){
                if(aData[i].length>0){
                    d.innerHTML+="<input onclick=\"checkchecked(event);\" type=\"checkbox\" id=\"chb" + i + "\"" + ((Sel.indexOf("," + aData[i] + ",")>=0)?" checked" : "") + " value=\"" + TrimStr(aData[i]) + "\"/>" + TrimStr(aNm[i]) + "<br/>";
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
        var buff="";
		goform($("#fn").val(),$("#iss").val());
    }
	function goform(fn,iss){
		period=$("#per_sel").val();
		
        var currency=1;
    	currency=$("#curr_selector").find("input:radio:checked").val();
		
		showClock();
		$("#tab_content").load("/Tab/?id=" + $("#tab_id").val() + "&ticker=" + iss + "&fn=" + fn +  "&per=" + period + "&curr=" + currency ,
     function (data) {
                hideClock();
        });
   
	}
	$(document).ready(function(){
		period=String($("#inpselector").val());
		//period='' + period.substr(2,4)+period.substr(0,1);
		xls_params={"per": period,"curr":$("#curr_selector").find("input:radio:checked").val(),"iss":$("#iss").val(),"fn":$("#fn").val(),"module" : "rsbu/insurer/","x":Math.random()}
	});
	]]>
        </script>
    </xsl:template>

<!--//-->
    <xsl:template name="tbl_blok">
        <xsl:param name="f_no"/>
        <xsl:param name="s_no"/>
        <xsl:param name="p_no"/>
        <xsl:for-each select="//rows[@form_no=$f_no and @section_no=$s_no and @parent_no=$p_no]">
            <xsl:variable name="r_no" select="@row_no"/>
            <xsl:if test="@parent_name!=''">
              <tr bgcolor="#EEEEEE">
                    <td>
                        <b>
                            <xsl:value-of disable-output-escaping="yes" select="@parent_name"/>11
                        </b>
                    </td>
                    <td>
                        <img src="/images/null.gif" width="1" height="1" border="0"/>
                    </td>
                    <xsl:for-each select="//cols[@form_no=$f_no and @section_no=$s_no and count(@col_span)=0]">
                        <td>
                            <img src="/images/null.gif" width="1" height="1" border="0"/>
                        </td>
                    </xsl:for-each>
                </tr>
            </xsl:if>
            
            <xsl:if test="count(//rows[@form_no=$f_no and @section_no=$s_no and @parent_no=$r_no]) != 0">
                <tr bgcolor="#EEEEEE">
                    <td>
                        <b>
                            <xsl:value-of disable-output-escaping="yes" select="@row_name"/><xsl:if test="@comment_no!=0"><span class="uin"><xsl:value-of select="@comment_no"/></span></xsl:if>
                        </b>
                    </td>
                    <td>
                        <img src="/images/null.gif" width="1" height="1" border="0"/>
                    </td>
                    <xsl:for-each select="//cols[@form_no=$f_no and @section_no=$s_no and count(@col_span)=0]">
                        <td>
                            <img src="/images/null.gif" width="1" height="1" border="0"/>
                        </td>
                    </xsl:for-each>
                </tr>
                <xsl:call-template name="tbl_blok">
                    <xsl:with-param name="f_no" select="$f_no"/>
                    <xsl:with-param name="s_no" select="$s_no"/>
                    <xsl:with-param name="p_no" select="$r_no"/>
                </xsl:call-template>
            </xsl:if>
          
            <xsl:if test="count(//rows[@form_no=$f_no and @section_no=$s_no and @parent_no=$r_no]) = 0">
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
                        <xsl:value-of disable-output-escaping ="yes" select="@row_name"/><xsl:if test="@comment_no!=0"><span class="uin"><xsl:value-of select="@comment_no"/></span></xsl:if>
                    </td>
                    <td class="table_item" style="text-align:center" >
                        <xsl:value-of disable-output-escaping ="yes" select="@line_code"/>
                    </td>
                    <xsl:variable name="rw" select="."/>

                    <xsl:for-each select="//cols[@form_no=$f_no and @section_no=$s_no and (count(@col_span)=0 or @col_span='')]">
                        <xsl:variable name="c_no" select="@col_no" />
                        <td style="text-align:right" class="table_item" nowrap="nowrap">
                            <xsl:choose>
                                <xsl:when test ="count(//vals[@form_no=$f_no and @section_no=$s_no]/r[@line_code=$r_no]/c[@col_no=$c_no])=0">-</xsl:when>
                                <xsl:when test ="//vals[@form_no=$f_no and @section_no=$s_no]/r[@line_code=$r_no]/c[@col_no=$c_no]/@val=-0.001">-</xsl:when>
                                <xsl:otherwise>
                                    <xsl:value-of select="format-number(//vals[@form_no=$f_no and @section_no=$s_no]/r[@line_code=$r_no]/c[@col_no=$c_no]/@val,'# ##0,00','buh')"/>
                                </xsl:otherwise>
                            </xsl:choose>
                        </td>
                    </xsl:for-each>
                </tr>
            </xsl:if>
        </xsl:for-each>
    </xsl:template>

</xsl:stylesheet>