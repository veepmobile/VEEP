<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >
	<xsl:decimal-format name="dec" decimal-separator="." grouping-separator=" " NaN="#"/>
	<xsl:variable name="linefeed">
		<xsl:text>&#x0d;&#x0a;</xsl:text>
	</xsl:variable>
	<xsl:variable name="ws">
		<xsl:text>&#x20;</xsl:text>
	</xsl:variable>
	<xsl:template name="set_bg">
		<xsl:param name="str_num" select="."/>
		<xsl:choose>
			<xsl:when test="ceiling($str_num div 2)- $str_num div 2 = 0">#F0F0F0</xsl:when>
			<xsl:otherwise>#FFFFFF</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="img_by_ext">
		<xsl:param name="ext" select="."/>
		<xsl:choose>
			<xsl:when test="$ext='doc'">
				<img src="/images/icon_docword_16.gif" width="16" height="16" border="0" alt="Документ Word" align="absmiddle"/>
			</xsl:when>
			<xsl:when test="$ext='rtf'">
				<img src="/images/icon_docword_16.gif" width="16" height="16" border="0" alt="Документ Word" align="absmiddle"/>
			</xsl:when>
			<xsl:when test="$ext='pdf'">
				<img src="/images/icon_docpdf_16.gif" width="16" height="16" border="0" alt="Документ PDF" align="absmiddle"/>
			</xsl:when>
			<xsl:when test="$ext='htm' or $ext='html'">
				<img src="/images/icon_dochtm_16.gif" width="16" height="16" border="0" alt="Документ HTML" align="absmiddle"/>
			</xsl:when>
			<xsl:when test="$ext='txt'">
				<img src="/images/icon_doctxt_16.gif" width="16" height="16" border="0" alt="Текстовый документ" align="absmiddle"/>
			</xsl:when>
			<xsl:when test="$ext='jpg'">
				<img src="/images/icon_docpict_16.gif" width="16" height="16" border="0" alt="Отсканированная копия (изображение)" align="absmiddle"/>
			</xsl:when>
			<xsl:when test="$ext='zip'">
				<img src="/images/icon_doczip_16.gif" width="16" height="16" border="0" alt="Документ ZIP" align="absmiddle"/>
			</xsl:when>
			<xsl:when test="$ext='smml' or $ext='smm'">
				<img src="/images/icon_docsmml_16.gif" width="16" height="16" border="0" alt="Документ SMML" align="absmiddle"/>
			</xsl:when>
			<xsl:otherwise>
				<img src="/images/icon_docunknown_16.gif" width="16" height="16" border="0" alt="Документ" align="absmiddle"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="set_br">
		<xsl:param name="text" select="."/>
		<xsl:choose>
			<xsl:when test="contains($text, $linefeed)">
				<xsl:value-of select="substring-before($text, $linefeed)"/>
				<br/>
				<xsl:call-template name="set_br">
					<xsl:with-param name="text" select="substring-after($text,$linefeed)"/>
				</xsl:call-template>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$text"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
</xsl:stylesheet>

