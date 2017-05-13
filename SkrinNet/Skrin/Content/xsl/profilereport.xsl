<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="urn:deitel:user">
  <xsl:output method="html" version="4.0" encoding="utf-8"/>
  <xsl:template match="/">
    <xsl:apply-templates/>
  </xsl:template>
  <xsl:decimal-format name="buh" decimal-separator="." grouping-separator=" " NaN="-"/>

  <xsl:template match="iss_profile">
    <!-- content -->
    <xsl:apply-templates select="profile">
    </xsl:apply-templates>
    <!-- end content -->
  </xsl:template>
  <xsl:template match="profile">
    <xsl:for-each select="//URep">
      <tr class="rep_arc">
        <xsl:if test="position() &gt; 1">
          <xsl:attribute name="style">display:none;</xsl:attribute>
          <xsl:attribute name="val">1</xsl:attribute>
        </xsl:if>
        <xsl:attribute name="id">Z<xsl:value-of select="@id"/></xsl:attribute>
        <td class="repitem"  style="height:38px">
          <div>
            <div class="icon_block">
              <div>
                <xsl:attribute name="onclick">
                  LoadReport('<xsl:value-of select="@id"/>');stopEvent();
                </xsl:attribute>
                <span class="icon-file-pdf pdf ico"></span>
                <span>
                  Отчет от <xsl:value-of select="@dt"/>
                </span>
              </div>
              <span class="ddel">
                <xsl:attribute name="onclick">
                  doDelReport('<xsl:value-of select="@id"/>');
                </xsl:attribute>
                <span class="ddel_1">
                  <b>x</b>
                  Удалить
                </span>
              </span>
            </div>
          </div>
         </td>
      </tr>
      <xsl:if test="position() &gt; 1 and position()=last()">
        <tr class="rep_arc1">
          <td>
            <span class="icon-angle-down more_ico" id="report_togler" onclick="showReps()">Архив отчетов</span>
          </td>
        </tr>
      </xsl:if>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>