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
      Период: <select name="period" id="period" class="system_form" onchange="goPeriod(this.value)">
        <xsl:for-each select="updt">
          <option>
            <xsl:attribute name="value">
              <xsl:value-of select="@update_date"/>
            </xsl:attribute>
            <xsl:if test="@cur=1">
              <xsl:attribute name="selected">selected</xsl:attribute>
            </xsl:if>
            <xsl:value-of select="@update_date_rus"/>
          </option>
        </xsl:for-each>

      </select>
    </xsl:if>
    <xsl:if test="//@PDF=0">
      Период:
      <xsl:for-each select="updt">
        <xsl:if test="@cur=1">
          <xsl:value-of select="@update_date_rus"/>
        </xsl:if>
      </xsl:for-each>
    </xsl:if>
    <br/>
    <br/>
    <xsl:if test="cons_amount[position()=1]">
      <span class="subcaption">
        Сведения об общем количестве акционеров(участников)
      </span>
      <br/>
      <table cellpadding="0" cellspacing="0">
        <tr>
          <td class="table_caption" style="width:350px;">Наименование показателя</td>
          <td class="table_caption" style="width:95px;">Значение</td>
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
        </tr>
        <tr>
          <td>Общее  количество акционеров(участников)</td>
          <td>
            <xsl:value-of select="cons_amount/@val"/>
          </td>
        </tr>
      </table>
      <br/>
    </xsl:if>
    <span class="subcaption">Сведения об учредителях/участниках</span>
    <br/>
    <input type="hidden" id="iss">
      <xsl:attribute name="value">
        <xsl:value-of select="//@iss"/>
      </xsl:attribute>
    </input>
    <input type="hidden" id="per">
      <xsl:attribute name="value">
        <xsl:for-each select="updt">
          <xsl:if test="@cur=1">
            <xsl:value-of select="@update_date"/>
          </xsl:if>
        </xsl:for-each>
      </xsl:attribute>
    </input>

    <table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <xsl:if test="//@PDF=-1">
          <td class="table_caption" rowspan="2" style="width:350px;">Наименование учредителя или участника</td>
          <td class="table_caption" rowspan="2" style="width:75px;">ИНН</td>
          <td class="table_caption" rowspan="2" style="width:350px;">Адрес места нахождения</td>
          <td class="table_caption" colspan="2" style="width:150px;">Доля в уставном капитале</td>
        </xsl:if>
        <xsl:if test="//@PDF=0">
          <td class="table_caption" rowspan="2" style="width:225px;">Наименование учредителя или участника</td>
          <td class="table_caption" rowspan="2" style="width:53px;">ИНН</td>
          <td class="table_caption" rowspan="2" style="width:225px;">Адрес места нахождения</td>
          <td class="table_caption" colspan="2" style="width:146px;">Доля в уставном капитале</td>
        </xsl:if>

      </tr>
      <tr>
        <td class="table_caption">%</td>
        <td class="table_caption">руб.</td>
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
        <td class="table_shadow">
          <div style="width: 1px; height: 1px;">
            <spacer type="block" width="1px" height="1px" />
          </div>
        </td>
      </tr>
      <xsl:for-each select="fndrs">
        <tr>
          <xsl:attribute name="bgcolor">
            <xsl:call-template name="set_bg">
              <xsl:with-param name="str_num" select="position()"/>
            </xsl:call-template>
          </xsl:attribute>
          <td>
            <xsl:choose>
              <xsl:when test="string-length(@ticker)>0">
                <a target="_blank">
                  <xsl:attribute name="href">
                    /issuers/<xsl:value-of select="@ticker"/>/
                  </xsl:attribute>
                  <xsl:value-of select="@name"/>
                </a>
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select="@name"/>
              </xsl:otherwise>
            </xsl:choose>
          </td>
          <td align="center" >
            <xsl:value-of select="@inn"/>
          </td>
          <td>
            <xsl:value-of select="@address"/>
          </td>
          <td align="right" width="53px">

            <xsl:value-of select="format-number(js:GetMult(string(@share)),'# ##0,#######','buh')"/>
            <!--<xsl:value-of select="format-number(@share,'# ##0,###########','buh')"/>-->
          </td>
          <td align="right" width="53px" nowrap="nowrap">
            <xsl:value-of select="format-number(@share_pecuniary,'# ##0,00','buh')"/>
          </td>
        </tr>
      </xsl:for-each>
    </table>
    <span class="data_comment limitation" >
      ВНИМАНИЕ: в связи с особенностями функционирования и обновления указанного источника информации АО "СКРИН" не может гарантировать абсолютную актуальность и достоверность данных.
    </span>
    <script type="text/javascript"  language="javascript" >
      <![CDATA[ 
				xls_params={"period": $("#per").val(),"iss":$("#iss").val(),"module" : "members/gmc/","x":Math.random()}
        
          function goPeriod(period){
        showClock();
        $("#tab_content").load("/Tab/Index/?id=24&ticker=]]><xsl:value-of select="//@iss"/><![CDATA[&src=1&period=" + period, 
            function(data){
                hideClock();
            }
        );
        }        
			]]>
    </script>
  </xsl:template>
</xsl:stylesheet>
