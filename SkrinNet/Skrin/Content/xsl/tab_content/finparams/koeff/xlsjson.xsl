<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">
	<msxsl:script language="JScript" implements-prefix="js">
	
	</msxsl:script>
	<xsl:output method="text" version="4.0" encoding="utf-8"/>
	<xsl:decimal-format name="buh" NaN="" decimal-separator="." grouping-separator=" "/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="iss_profile">
		{WorkBook : [<xsl:apply-templates select="profile"></xsl:apply-templates>]}
	</xsl:template>
		<xsl:template match="profile">
			{WorkSheet:"Коэффициенты",
			"Cols":[300,<xsl:for-each select="section[1]/lines[1]/dta">120<xsl:if test="position()!=last()">,</xsl:if></xsl:for-each>],
			"IssName": "<xsl:value-of disable-output-escaping="yes" select="//name/@val"/>", "Style": "Header", "Header" : "Основные финансовые показатели. Коэффициенты.",
			"SheetData" : [
			
				
					<xsl:for-each select="section">
						<xsl:if test="position()=1">
							{Row :[{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Наименование показателя"},
								<xsl:for-each select="lines[1]/dta">
									{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "<xsl:value-of select="@yn"/>"},
								</xsl:for-each>]},
						</xsl:if>
						{Row :[{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : "<xsl:value-of select="@name"/>"},
							<xsl:for-each select="lines[1]/dta">
								{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
							</xsl:for-each>]},
							<xsl:for-each select="lines">
								{Row :[{"Style" : "<xsl:choose><xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when><xsl:otherwise>even</xsl:otherwise></xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of disable-output-escaping ="yes" select="@name"/>"},
								<xsl:call-template name="num_nodes">
									<xsl:with-param name="odd">
										<xsl:value-of select="ceiling(position() div 2)- position() div 2"/>
									</xsl:with-param>
									<xsl:with-param name="nodes" select="dta"/>
									<xsl:with-param name="prec" select="@num_prec"/>
								</xsl:call-template>
							]},
						</xsl:for-each>
					</xsl:for-each>
			]},
		</xsl:template>
		<xsl:template name="num_nodes">
			<xsl:param name="odd"/>
			<xsl:param name="nodes"/>
			<xsl:param name="prec"/>
			<xsl:for-each select="$nodes">

				{"Style" : "<xsl:choose>
					<xsl:when test="$odd = 0 and $prec = 0">odd_right_int</xsl:when>
					<xsl:when test="$odd != 0 and $prec = 0">even_right_int</xsl:when>
					<xsl:when test="$odd = 0 and $prec != 0">odd_right</xsl:when>
					<xsl:when test="$odd != 0 and $prec != 0">even_right</xsl:when>
				</xsl:choose>","colspan":1, "rowspan":1, Type:"S", "Data" : "<xsl:choose>
				<xsl:when test ="@val=-0.001">-</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="format-number(@val,'###0.00','buh')"/>
				</xsl:otherwise>
			</xsl:choose>"},
			</xsl:for-each>
		</xsl:template>
	</xsl:stylesheet>

