<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:import href="common.xsl"/>
	<xsl:output method="html" version="4.0" encoding="utf-8"/>
	<xsl:template match="/">
	<xsl:call-template name="set_br">
		<xsl:with-param name="text" select="main/планы_будущ_деятельности/описание"/>
	</xsl:call-template>
	</xsl:template>
</xsl:stylesheet>
