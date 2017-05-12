<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" >
  <xsl:import href="../../../xsl/common.xsl"/>
  <xsl:output method="html" version="4.0" encoding="utf-8"/>

  <xsl:template match="/">
    <xsl:apply-templates/>
  </xsl:template>
  <xsl:decimal-format name="buh" decimal-separator="," grouping-separator=" " NaN="-"/>
  <xsl:template match="iss_profile">

    <!-- content -->
    <span class="subcaption">
      Данные о численности, а также об изменении численности сотрудников (работников)
    </span>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
      <xsl:apply-templates select="profile">
      </xsl:apply-templates>
    </table>

    <span class="data_comment limitation">
      ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и могут не отражать абсолютную актуальность и достоверность информации. Дополнительную информацию можно получить в разделе <a onclick="gotomenu(48);" href="#">«Квартальные отчеты»</a>
    </span>

    <!-- end content -->
  </xsl:template>
  <xsl:template match="profile">
    <input type="hidden" id="iss">
      <xsl:attribute name="value">
        <xsl:value-of select="//@iss"/>
      </xsl:attribute>
    </input>
    <xsl:for-each select="a">
      <xsl:if test="position()=1">
        <tr>
          <td class="table_caption_left">Год</td>
          <xsl:for-each select="b">
            <td class="table_caption" align="center">
              <xsl:value-of select="@q"/>-й кв.
            </td>
          </xsl:for-each>
        </tr>
        <tr>
          <td class="table_shadow">
            <div style="width: 1px; height: 1px;">
              <spacer type="block" width="1px" height="1px" />
            </div>
          </td>
          <xsl:for-each select="b">
            <td class="table_shadow">
              <div style="width: 1px; height: 1px;">
                <spacer type="block" width="1px" height="1px" />
              </div>
            </td>
          </xsl:for-each>
        </tr>

      </xsl:if>
      <tr>
        <xsl:attribute name="bgcolor">
          <xsl:call-template name="set_bg">
            <xsl:with-param name="str_num" select="position()"/>
          </xsl:call-template>
        </xsl:attribute>
        <td class="table_item" align="center">
          <xsl:value-of select="@y"/> г.
        </td>
        <xsl:for-each select="b">
          <td class="table_item" align="right">
            <xsl:value-of select="@Emp"/>
          </td>
        </xsl:for-each>
      </tr>
    </xsl:for-each>
    <script language="javascript" type="text/javascript">
      <![CDATA[ 
			xls_params={"iss":$("#iss").val(),"module" : "work/colabs/","x":Math.random()};
			]]>
    </script>
  </xsl:template>
</xsl:stylesheet>
