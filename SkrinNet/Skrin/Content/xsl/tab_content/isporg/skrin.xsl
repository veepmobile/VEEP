<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">
  <xsl:import href="../../../xsl/common.xsl"/>
  <msxsl:script language="JScript" implements-prefix="js">
    function GetMult(nl){
    var val=nl;
    var retval="0";
    var mult,exp;
    if(val.indexOf("e") &lt; 0){
    retval=val;
    }else{
    mult=val.substring(0,val.indexOf("e"));
    exp=val.substring(val.indexOf("e")+1,val.length);
    exp=Math.pow(10,exp);
    retval=mult*exp;
    }
    return retval;
    }
  </msxsl:script>

  <xsl:output method="html" version="4.0" encoding="utf-8"/>
  <xsl:decimal-format name="buh" decimal-separator="." grouping-separator=" " NaN="#"/>

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
      Период: <select name="period" id="period" class="system_form" onchange="goPeriod(this.value)">
        <xsl:for-each select="periods">
          <option>
            <xsl:attribute name="value">
              <xsl:value-of select="@yq"/>
            </xsl:attribute>
            <xsl:if test="@curr=1">
              <xsl:attribute name="selected">selected</xsl:attribute>
            </xsl:if>
            <xsl:value-of select="@quarter"/>-й кв. <xsl:value-of select="@year"/> г.
          </option>
        </xsl:for-each>
      </select><br/><br/><!--input type="button" class="btns green" id="bexpand" value="Показать должности" style="width:150px" onclick="ec()"/><br/><br/-->
    </xsl:if>
    <xsl:if test="//@PDF=0">
      Период: <xsl:for-each select="periods">
        <xsl:if test="@curr=1">
          <xsl:value-of select="@quarter"/>-й кв. <xsl:value-of select="@year"/> г.<br/><br/>
        </xsl:if>
      </xsl:for-each>
    </xsl:if>
    <input type="hidden" id="iss">
      <xsl:attribute name="value">
        <xsl:value-of select="//@iss"/>
      </xsl:attribute>
    </input>
    <input type="hidden" id="per">
      <xsl:attribute name="value">
        <xsl:for-each select="periods">
          <xsl:if test="@curr=1">
            <xsl:value-of select="@yq"/>
          </xsl:if>
        </xsl:for-each>
      </xsl:attribute>
    </input>

    <xsl:if test="edin[position()=last()]">
      <span class="subcaption">Лицо, исполняющее функции единоличного исполнительного органа</span>
      <table width="98%" cellpadding="0" cellspacing="0" >
        <tr>
          <td class="table_caption" style="width:250px;">Ф.И.О</td>
          <td class="table_caption" style="width:75px;">
            Год рождения
          </td>
          <td class="table_caption" style="width:150px;">Доля в УК%</td>
          <td class="table_caption">Должности занимаемые лицом за последние 5 лет</td>

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
          <td class="table_shadow">
            <div style="width: 1px; height: 1px;">
              <spacer type="block" width="1px" height="1px" />
            </div>
          </td>
        </tr>
        <xsl:for-each select="edin">
          <tr>
            <xsl:attribute name="bgcolor">
              <xsl:call-template name="set_bg">
                <xsl:with-param name="str_num" select="position()"/>
              </xsl:call-template>
            </xsl:attribute>
            <td valign="top">
              <a>
                <xsl:attribute name="href">
                    javascript:openProfileFL('<xsl:value-of select="@fio"/>','')
                </xsl:attribute>
                <xsl:value-of select="@fio"/>
              </a>
            </td>
            <td align="center" style="width:75px;" valign="top">
              <xsl:value-of select="@bd"/>
            </td>
            <td valign="top">
              <xsl:value-of select="format-number(js:GetMult(string(@StockPile)),'# ##0.#######','buh')"/>
            </td>
            <td>
              <div onclick="ec(this)">
                <xsl:attribute name="style">
                  <xsl:if test="//@PDF=0">display:none</xsl:if>
                </xsl:attribute>

                <xsl:attribute name="id">col99<xsl:value-of select="position()"/></xsl:attribute>
                <xsl:value-of disable-output-escaping="yes" select="substring(@bio,0,20)"/>...<span style="cursor:pointer;color:#003399;">Подробнее</span><img src="/images/tra_e.png" alt="" style="padding-left:3px"/>
              </div>
              <div onclick="ec(this)">
                <xsl:attribute name="id">exp99<xsl:value-of select="position()"/></xsl:attribute>
                <xsl:attribute name="style">
                  <xsl:if test="//@PDF=0">display:block</xsl:if>
                  <xsl:if test="//@PDF=-1">display:none</xsl:if>
                </xsl:attribute>

                <span style="cursor:pointer;color:#003399;">Свернуть</span>
                <img src="/images/tra_w.png" alt="" style="padding-left:3px"/>
                <br/>
                <xsl:value-of disable-output-escaping="yes" select="@bio"/>
              </div>
            </td>
          </tr>
        </xsl:for-each>
      </table>
      <br/><br/>
      <span class="data_comment limitation">
        ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и могут не отражать абсолютную актуальность и достоверность информации. Дополнительную информацию можно получить в разделе
        <a onclick="gotomenu(48);" href="#">«Квартальные отчеты»</a> или <a onclick="gotomenu(32);" href="#">«Существенные факты»</a>,  <a onclick="gotomenu(28);" href="#">«Списки аффилированных лиц»</a>
      </span>
    </xsl:if>

    <script language="javascript" type="text/javascript">
      <![CDATA[ 
			xls_params={"period": $("#per").val(),"iss":$("#iss").val(),"module" : "isporg/skrin/","x":Math.random()}

			function ec(obj){
			var id;
			if(obj){
				id=String(obj.id).substring(3,8);
				if(getObj("exp"+id).style.display=="block"){
					getObj("exp"+id).style.display="none";
					getObj("col"+id).style.display="block";
				}else{
					getObj("col"+id).style.display="none";
					getObj("exp"+id).style.display="block";

				}
			}else{
					
				$("div").each(function(i){
					
					if(this.id.substring(3,8)<990){
						if(this.id.substring(0,3)=="exp"){
							this.style.display=($("#bexpand").attr("val")==0)?"block":"none";
							getObj("col" + this.id.substring(3,8)).style.display=($("#bexpand").attr("val")=="0")?"none":"block";
						}
					}	
				})	
				$("#bexpand").attr("val",Math.abs($("#bexpand").attr("val")-1));
				$("#bexpand").html((($("#bexpand").attr("val")=="1")? "Скрыть должности":"Показать должности"))
				$("#img").attr("src",(($("#bexpand").attr("val")=="1")? "/images/tra_w.png":"/images/tra_e.png"));
			}
			}
      
      function goPeriod(period){
        showClock();
        $("#tab_content").load("/Tab/Index/?id=17&ticker=]]><xsl:value-of select="//@iss"/><![CDATA[&period=" + period, 
            function(data){
                hideClock();
            }
        );
        
}
			]]>
    </script>

    <input id="tabId" type="hidden" value="17" />
  </xsl:template>
</xsl:stylesheet>
