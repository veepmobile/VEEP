<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user">
  <xsl:import href="../../../xsl/common.xsl"/>
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

    <span class="subcaption">Сведения об участии юридического лица в уставном (складочном) капитале юридических лиц, паевых фондов</span>

    <table width="98%" cellpadding="2" cellspacing="0">
      <tr>
        <td class="table_caption" rowspan="2" style="width:250px;">Наименование</td>
        <td class="table_caption" rowspan="2" style="width:50px;">ИНН</td>
        <td class="table_caption" rowspan="2" style="width:250px;">Адрес места нахождения</td>
        <td class="table_caption" rowspan="2" style="width:250px;">Вид деятельности</td>
        <td class="table_caption" rowspan="2" style="width:75px;">Дата начала деятельности</td>
        <td class="table_caption" colspan="2" >Доля в уставном капитале</td>
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
      <xsl:for-each select="b">
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
          <td align="center" style="width:75px;">
            <xsl:value-of select="@inn"/>
          </td>
          <td>
            <xsl:value-of select="@address"/>
          </td>
          <td>
            <xsl:value-of select="a/@okved"/>
          </td>
          <td align="center">
            <xsl:value-of select="a/@rd"/>
          </td>
          <td align="right">

            <xsl:value-of select="format-number(a/@share,'# ##0,###########','buh')"/>
          </td>
          <td align="right" nowrap="nowrap">
            <xsl:value-of select="format-number(a/@share_pecuniary,'# ##0,00','buh')"/>
          </td>
        </tr>
      </xsl:for-each>
    </table>

    <span class="data_comment limitation">
      ВНИМАНИЕ: Сведения из раздела являются результатом обработки данных, предоставленных в ГМЦ РОССТАТа организациями, указанными в разделе. В связи с особенностями обработки информации АО«СКРИН» не может гарантировать актуальность и достоверность сведений на текущую дату.
    </span>
    <script type="text/javascript"  language="javascript" >
      <![CDATA[ 
				xls_params={"period": $("#per").val(),"iss":$("#iss").val(),"module" : "depend/gmc/","x":Math.random()}
      function goPeriod(period){
        showClock();
        $("#tab_content").load("/Tab/Index/?id=26&ticker=]]><xsl:value-of select="//@iss"/><![CDATA[&period=" + period, 
            function(data){
                hideClock();
            }
        );
      }             
			]]>
    </script>
  </xsl:template>
</xsl:stylesheet>
