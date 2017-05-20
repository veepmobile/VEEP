<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                              xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">
	<xsl:import href="../../../../xsl/common.xsl"/>

  <msxsl:script language="JScript" implements-prefix="js">
  function calc_cols(lst) {
    var cnt = 0;
    for (var i=0; i&lt;lst.length; i++) {
        var n = lst.nextNode();
       cnt += (n.getAttribute("colspan") == "0" ? 1:n.getAttribute("colspan")*1);
    }
    return(cnt);
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
            <table cellpadding="4" style="border:none;">
                <tr>
                    <xsl:for-each select="fg">
                        <td valign="top" style="border:none;">
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
		        <table cellpadding="4" style="border:none;">
			        <tr>
				        <xsl:if test="//fn/@val=1">
					        <td class="content_menu" valign="top" style="border:none;">
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
					        <td style="cursor:pointer;border:none;" class="content_menu_sel" valign="top">
						        <xsl:attribute name="onclick">
							        goform(1,'<xsl:value-of select="//@iss"/>')
						        </xsl:attribute>
						        неконсолидированная бухгалтерская отчетность
					        </td>
					        <td class="content_menu" style="border:none;">
                      консолидированная бухгалтерская отчетность
                  </td>
				        </xsl:if>
			        </tr>
		        </table>
		    </xsl:if>
			
		<form id="frm" style="margin-top:4px;">
			<table id="curr_selector" onclick="checkchecked(event);"  style="border:none;">
				<tr>
					<td  style="border:none;">Период:</td>
					<td  style="border:none;">
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
            <xsl:if test="//fn/@val!=2">
                <td  style="border:none;">
                    Валюта:
                </td>
                <xsl:for-each select="currency">
                      <td  style="border:none;">
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
            </xsl:if>
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
					<xsl:value-of select="@name"/>,
				</xsl:if>
			</xsl:for-each>
		</xsl:if>

		<xsl:for-each select="//forms">
      <xsl:variable name="f_id" select="@id" />
      <xsl:variable name="sc_cnt" select="count(//sections[@form_id = $f_id])" />
      <table width="100%" cellpadding="1" cellspacing="0"  style="border:none;background-color: #f9fafc;"><tr>
			    <td class="subcaption" style="border:none;background-color: #f9fafc;">
			  	    <xsl:value-of select="@name"/>
              <xsl:if test="@no_name != ''" >
			            <xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>(<xsl:value-of select="@no_name"/>)
              </xsl:if>
          </td>
      </tr></table>
      <xsl:for-each select="//sections[@form_id = $f_id]">
        <xsl:variable name="s_no" select="@no" />
        <!--xsl:variable name="col_cnt" select="count(//cols[@form_id = $f_id and @section_no = $s_no]) + 2" /-->
        <xsl:variable name="col_lst" select="//cols[@form_id = $f_id and @section_no = $s_no]" />
        <!--xsl:variable name="col_cnt" select="js:calc_cols($col_lst)"/-->
        <xsl:variable name="col_cnt" select="count(//cols[@form_id = $f_id and @section_no = $s_no])"/>

        <table width="100%" cellpadding="1" cellspacing="0"  style="border:none;background-color: #f9fafc;">
            <xsl:if test="$sc_cnt > 1" >
              <xsl:if test="@name != ''">
			          <tr><td class="subcaption"  style="border:none;background-color: #f9fafc;margin:0">
				        <xsl:value-of select="@name"/>
                </td></tr>
              </xsl:if> 
              <xsl:if test="@no_name != ''" >
			          <tr><td class="subcaption"  style="border:none;background-color: #f9fafc;">
			            <xsl:value-of select="@no_name"/>
                </td></tr>
              </xsl:if>
            </xsl:if>
            <xsl:if test="@dimension != ''" >
            <tr>
              <td align="left"  style="border:none;background-color: #f9fafc;">
                <xsl:value-of select="@dimension"/>
              </td>
            </tr>
            </xsl:if> 
        </table>
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
          <xsl:for-each select="//header_rows[@form_id = $f_id and @section_no = $s_no]">
            <xsl:variable name="hr_no" select="@row_no" />
            <tr>
            <xsl:for-each select="//headers[@form_id = $f_id and @section_no = $s_no and @row_no = $hr_no]">
              <td class="table_caption_left" style="border:none;">
                  <xsl:if test="@rowspan != 0">
                    <xsl:attribute name="rowspan"><xsl:value-of select="@rowspan"/></xsl:attribute>
                  </xsl:if>    
                  <xsl:if test="@colspan != 0">
                    <xsl:attribute name="colspan"><xsl:value-of select="@colspan"/></xsl:attribute>
                  </xsl:if>    
                  <xsl:value-of select="@title"/>
              </td>
            </xsl:for-each>
            </tr>
          </xsl:for-each>
          <tr>
            <xsl:for-each select="//cols[@form_id = $f_id and @section_no=$s_no]">
              <td class="table_shadow"  style="border:none;">
                <xsl:if test="@colspan != 0">
                  <xsl:attribute name="colspan"><xsl:value-of select="@colspan"/></xsl:attribute>
                </xsl:if>    
                <img src="/images/null.gif" width="1" height="1" border="0"/>
              </td>
            </xsl:for-each>
          </tr>
          <xsl:if test="//fn/@val != 2 and //currency[@id = 1]/@sel = 0">
              <tr>
                  <xsl:for-each select="//cols[@form_id = $f_id and @section_no = $s_no]">
                      <xsl:choose>
                        <xsl:when test="position() = 1">
                          <td>
                              <xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text> 
                          </td>
                        </xsl:when>
                        <xsl:when test="position() = 2">
                          <td>
                              Курс <xsl:value-of select="//kurs_header/@val"/>
                          </td>
                        </xsl:when>
                        <xsl:otherwise>
                          <td class="table_item" align="right">
                              <xsl:value-of select="@kurs"/>
                          </td>
                        </xsl:otherwise>
                      </xsl:choose> 
                  </xsl:for-each>
              </tr>
              <tr>
                  <xsl:for-each select="//cols[@form_id = $f_id and @section_no = $s_no]">
                      <xsl:choose>
                        <xsl:when test="position() = 1">
                          <td>
                              <xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text> 
                          </td>
                        </xsl:when>
                        <xsl:when test="position() = 2">
                          <td>
                              Курс на дату
                          </td>
                        </xsl:when>
                        <xsl:otherwise>
                          <td class="table_item" align="right">
                              <xsl:value-of select="@ld"/>
                          </td>
                        </xsl:otherwise>
                      </xsl:choose> 
                  </xsl:for-each>
              </tr>
          </xsl:if>
            
          <xsl:for-each select="//rgrp[@form_id = $f_id and @section_no=$s_no]">
            <xsl:variable name="g_no" select="@no" />
            <xsl:if test="@name!=''">
              <tr>
                <td style="text-align:center;background-color:#EEEEEE">
                    <xsl:attribute name="colspan"><xsl:value-of select="$col_cnt"/></xsl:attribute>
                    <b><xsl:value-of select="@name"/></b>
                </td>
              </tr>  
            </xsl:if>
            <xsl:for-each select="//trs[@form_id = $f_id and @section_no=$s_no and @group_no = $g_no]">
              <xsl:variable name="r_no" select="@row_no" />
              <tr>
                <xsl:choose>
                  <xsl:when test="ceiling(position() div 2)- position() div 2 = 0">
                    <xsl:attribute name="bgColor">#F0F0F0</xsl:attribute>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
                  </xsl:otherwise>
                </xsl:choose>
                <xsl:for-each select="//tds[@form_id = $f_id and @section_no=$s_no and @group_no = $g_no and @row_no = $r_no]">
                  <xsl:variable name="TYP">
                      <xsl:choose>
                         <xsl:when test="@val_type = 0 and @value != ''">N</xsl:when>
                         <xsl:otherwise>S</xsl:otherwise>
                      </xsl:choose>
                  </xsl:variable>
                  <xsl:variable name="VAL">
                    <xsl:choose>
                      <xsl:when test="@value = '' and @val_type = 0">-</xsl:when>
                      <xsl:when test="$TYP = 'N'"><xsl:value-of select="format-number(@value,'# ##0,00','buh')"/></xsl:when>
                      <xsl:otherwise><xsl:value-of select="@value"/></xsl:otherwise>
                    </xsl:choose>
                  </xsl:variable>
                                       
                  <td class="table_item" align="center">
                    <xsl:if test="@col_no=1">
                      <xsl:attribute name="class">table_item</xsl:attribute>
                      <xsl:attribute name="align">center</xsl:attribute>
                    </xsl:if>
                    <xsl:if test="@col_no=2">
                      <xsl:attribute name="class">table_item_left</xsl:attribute>
                      <xsl:attribute name="align">left</xsl:attribute>
                      <xsl:attribute name="width">300</xsl:attribute>
                    </xsl:if>
                    <xsl:if test="@col_no > 2">
                      <xsl:attribute name="class">table_item</xsl:attribute>
                      <xsl:if test="$TYP = 'N'" ><xsl:attribute name="style">text-align:right;</xsl:attribute></xsl:if>
                      <xsl:if test="$TYP = 'S'" ><xsl:attribute name="align">center</xsl:attribute></xsl:if>
                    </xsl:if>
                    <xsl:if test="@rowspan != 0">
                    <xsl:attribute name="rowspan"><xsl:value-of select="@rowspan"/></xsl:attribute>
                    </xsl:if>    
                    <xsl:if test="@colspan != 0">
                      <xsl:attribute name="colspan"><xsl:value-of select="@colspan"/></xsl:attribute>
                    </xsl:if>
                  
                    <xsl:value-of select="$VAL"/>
                  </td> 
                </xsl:for-each>
              </tr>          
            </xsl:for-each>
          </xsl:for-each>
        </table>
    </xsl:for-each>
  	</xsl:for-each>
		<span class="data_comment limitation">ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и могут не отражать абсолютную актуальность и достоверность информации.</span>

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
		//period='' + period.substr(2,4)+period.substr(0,1);
		//if(fn-$("#fn").val()!=0){
		//	period="0";
		//}
        var currency=1;
    	if(fn != 2){
			currency=$("#curr_selector").find("input:radio:checked").val();
		}
    if(String(currency)=="undefined"){
      currency=1;
    }

		showClock();
		$("#tab_content").load("/Tab/?id=" + $("#tab_id").val() + "&ticker=" + iss + "&fn=" + fn +  "&per=" + period + "&curr=" + currency ,
              function (data) {
                hideClock();
        });
     
	}
	$(document).ready(function(){
		period=String($("#inpselector").val());
		//period='' + period.substr(2,4)+period.substr(0,1);
		xls_params={"per": period,"curr":$("#curr_selector").find("input:radio:checked").val(),"iss":$("#iss").val(),"fn":$("#fn").val(),"module" : "rsbu/cby/","x":Math.random()}
	});
	]]>
        </script>
    </xsl:template>
</xsl:stylesheet>