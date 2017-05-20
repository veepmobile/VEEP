<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user">
	<xsl:output method="html" version="4.0" encoding="utf-8"/>
	<xsl:decimal-format name="buh" decimal-separator="." grouping-separator=" " NaN="-"/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="iss_profile">
		<xsl:apply-templates select="profile">
		</xsl:apply-templates>
	</xsl:template>
	<xsl:template match="profile">
			[<xsl:for-each select="fndrs">
				{id:"<xsl:value-of select="@ticker"/>_<xsl:value-of select="@type"/>_<xsl:value-of select="@nid"/>", name:"<xsl:value-of select="@name"/>",data:{"ticker": "<xsl:value-of select="@ticker"/>", "share" : "<xsl:value-of select="format-number(@share,'# ##0.00','buh')"/>","ed":"<xsl:value-of select="@ei"/>","ogrn":"<xsl:value-of select="@ogrn"/>", "type":"<xsl:value-of select="@type"/>","ex" : "<xsl:value-of select="@ex"/>", "parent" : "<xsl:value-of select="@parent"/>", "ptype" :  "<xsl:value-of select="@ptype"/>"}<!--,children:[]-->}<xsl:if test="position()!=count(//fndrs)">,</xsl:if>
			</xsl:for-each>]
	</xsl:template>
</xsl:stylesheet>
