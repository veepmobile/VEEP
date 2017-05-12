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
		<table width="100%" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td width="45%" class="table_caption_left">Название раздела</td>
				<td width="10%" class="table_caption_left">Дата</td>
				<td width="45%" class="table_caption_left">Вид документа (информации)</td>
			</tr>
			

		<xsl:apply-templates select="profile">
			</xsl:apply-templates>
		</table>
		<!-- end content -->
	</xsl:template>
	<xsl:template match="profile">

		<xsl:for-each select="a">
			<tr><xsl:attribute name="bgcolor"><xsl:call-template name="set_bg"><xsl:with-param name="str_num" select="position()"/></xsl:call-template></xsl:attribute>
				<td style="width:45%;" class="table_item_left">
					<xsl:value-of select="@mn"/>
				</td>
				<td style="width:10%; taxt-align:center" class="table_item">
					<xsl:value-of select="@ld"/>
				</td>
				<td style="width:45%; text-align:left" class="table_item_left">
					<xsl:if test ="@ld">
						<a href="#"><xsl:attribute name="onclick">gotomenu(<xsl:value-of select="@id"/>)</xsl:attribute><xsl:value-of select="@sm"/></a><!--,<xsl:value-of select="@tab_id"/>-->
					</xsl:if>	
					
				</td>
			</tr>
		</xsl:for-each>
    <xsl:for-each select="updt">
      <tr>
        <td>
          <xsl:value-of select="@nm"/>
        </td>
        <td>
          <xsl:call-template name="format-date">
            <xsl:with-param name="date" select="@ld"/>
        </xsl:call-template>
        </td>
        <td style="width:45%; text-align:left" class="table_item_left">
          <xsl:if test ="@ld">
            <a href="#">
              <xsl:attribute name="onclick">
                gotomenu(<xsl:value-of select="@mid"/>,<xsl:value-of select="@tid"/>)
              </xsl:attribute>
              <xsl:value-of select="@name"/>
            </a>
          </xsl:if>

        </td>
      </tr>
    </xsl:for-each>
</xsl:template>
  <xsl:template name="format-date">
    <xsl:param name="date"/>
    <xsl:value-of select="concat(substring($date, 9, 2), '.', substring($date, 6, 2), '.', substring($date, 1, 4))"/>
  </xsl:template>
</xsl:stylesheet>
