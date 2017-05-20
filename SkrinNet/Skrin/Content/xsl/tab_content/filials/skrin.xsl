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
      </select>
    </xsl:if>
    <xsl:if test="//@PDF=0">
      Период:
      <xsl:for-each select="periods">

        <xsl:if test="@curr=1">
          <xsl:value-of select="@quarter"/>-й кв. <xsl:value-of select="@year"/> г.
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
        <xsl:for-each select="periods">
          <xsl:if test="@curr=1">
            <xsl:value-of select="@yq"/>
          </xsl:if>
        </xsl:for-each>
      </xsl:attribute>
    </input>
    <span class="subcaption">Филиалы и представительства</span>
    <xsl:if test="us/@name!='Нет данных'">
      <table width="98%" cellpadding="2px" cellspacing="0">
        <tr>
          <td class="table_caption" style="width:40%;">Наименование </td>
          <td class="table_caption" style="width:40%;">Адрес места нахождения</td>
          <td class="table_caption" style="width:20%;">Руководитель</td>
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

        </tr>
        <xsl:for-each select="us">
          <tr>
            <xsl:attribute name="bgcolor">
              <xsl:call-template name="set_bg">
                <xsl:with-param name="str_num" select="position()"/>
              </xsl:call-template>
            </xsl:attribute>
            <td>
              <xsl:value-of select="@name"/>
            </td>
            <td>
              <xsl:value-of select="@address"/>
            </td>
            <td>
              <xsl:value-of select="@manager"/>
            </td>
          </tr>
        </xsl:for-each>
      </table>
      <br/>
    </xsl:if>
    <xsl:if test="us/@name='Нет данных'">
      <br/><br/>

      Филиалов нет.

    </xsl:if>
    <span class="data_comment limitation">
      ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и могут не отражать абсолютную актуальность и достоверность информации. Дополнительную информацию можно получить в разделе
      <a onclick="gotomenu(48);" href="#">«Квартальные отчеты»</a> или <a onclick="gotomenu(32);" href="#">«Существенные факты»</a>
    </span>
    <script type="text/javascript"  language="javascript" >
      <![CDATA[ 
				xls_params={"period": $("#per").val(),"iss":$("#iss").val(),"module" : "filials/skrin/","x":Math.random()}
        
      function goPeriod(period){
        showClock();
        $("#tab_content").load("/Tab/Index/?id=28&ticker=]]><xsl:value-of select="//@iss"/><![CDATA[&period=" + period, 
            function(data){
                hideClock();
            }
        );
      }                
			]]>
    </script>
  </xsl:template>
</xsl:stylesheet>
