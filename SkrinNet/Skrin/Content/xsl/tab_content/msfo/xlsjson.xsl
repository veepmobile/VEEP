<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user">
	<xsl:output method="html" version="4.0" encoding="utf-8"/>
	<xsl:decimal-format name="buh" NaN="-" decimal-separator="." grouping-separator=" "/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="iss_profile">
		{WorkBook : [<xsl:apply-templates select="profile"></xsl:apply-templates>]}
	</xsl:template>
	<xsl:template match="profile">
		{WorkSheet:"ФО МСФО",
		"Cols":[310,120,120],"IssName": "<xsl:value-of disable-output-escaping="yes" select="//name/@val"/>", "Style": "Header", "Header" : "Финансовая отчетность по МСФО за <xsl:for-each select="per"><xsl:if test="@sel=1"><xsl:value-of select="@name"/></xsl:if></xsl:for-each>",
		"SheetData" : [
		<xsl:for-each select="gaap">
			
			{Row :[{"Style" : "subcaption","colspan":3, "rowspan":1, Type:"S", "Data" : "<xsl:choose>
				<xsl:when test="@form_no=1">БУХГАЛТЕРСКИЙ БАЛАНС</xsl:when>
				<xsl:when test="@form_no=2">ОТЧЕТ О ПРИБЫЛЯХ И УБЫТКАХ</xsl:when>
				<xsl:when test="@form_no=3">ОТЧЕТ О ДВИЖЕНИИ КАПИТАЛА</xsl:when>
			</xsl:choose>"},]},
			{Row :[{"Style" : "razdel","colspan":3, "rowspan":1, Type:"S", "Data" : "<xsl:choose>
				<xsl:when test="@form_no=1">Consolidated balance sheet</xsl:when>
				<xsl:when test="@form_no=2">Consolidated Statements of Income</xsl:when>
				<xsl:when test="@form_no=3">Cash and cash equivalents</xsl:when>
			</xsl:choose>"},]},
			<xsl:for-each select="LA">
				<xsl:if test="position()=1">
					{Row :[{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Наименование показателя"},
					<xsl:for-each select="cols">
						{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "<xsl:value-of select="@dname"/>"},
					</xsl:for-each>
					]},
				</xsl:if>
				<xsl:if test="../@form_no=1 and @line_code=10">
					{Row :[{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : "ASSETS"},
					<xsl:for-each select="cols">
						{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
					</xsl:for-each>]},
					{Row :[{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : "NON-CURRENT ASSETS"},
					<xsl:for-each select="cols">
						{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
					</xsl:for-each>]},
				</xsl:if>
				<xsl:if test="../@form_no=1 and @line_code=80">
					{Row :[{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : "EQUITY &amp; LIABILITIES"},
					<xsl:for-each select="cols">
						{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
					</xsl:for-each>]},
                    {Row :[{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : "EQUITY"},
                    <xsl:for-each select="cols">
						{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
					</xsl:for-each>]},
				</xsl:if>

				<xsl:if test="../@form_no=1 and @line_code=40">
					{Row :[{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : "CURRENT ASSETS"},
					<xsl:for-each select="cols">
						{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
					</xsl:for-each>]},
					{Row :[{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : "EQUITY"},
					<xsl:for-each select="cols">
						{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
					</xsl:for-each>]},
				</xsl:if>
				<xsl:if test="../@form_no=1 and @line_code=110">
                    {Row :[{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : "LIABILITIES"},
                    <xsl:for-each select="cols">
						{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
					</xsl:for-each>]},

				</xsl:if>
				
				{Row :[{"Style" : "<xsl:choose>
					<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
					<xsl:otherwise>even</xsl:otherwise>
				</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of disable-output-escaping ="yes" select="@name"/>"},
				<xsl:call-template name="num_nodes">
					<xsl:with-param name="odd">
						<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd_right</xsl:when>
							<xsl:otherwise>even_right</xsl:otherwise>
						</xsl:choose>
					</xsl:with-param>
					<xsl:with-param name="nodes" select="cols"/>
				</xsl:call-template>
					]},
				
			</xsl:for-each>
		</xsl:for-each>
		]},

	</xsl:template>
	<xsl:template name="num_nodes">
		<xsl:param name="odd"/>
		<xsl:param name="nodes"/>
		<xsl:for-each select="$nodes">
			<xsl:choose>
				<xsl:when test="@val=-0.001 or string-length(@val)=0">
					{"Style" : "<xsl:value-of select="$odd"/>","colspan":1, "rowspan":1, "Type" : "N", "Data" : "-"},
				</xsl:when>
				<xsl:otherwise>
					{"Style" : "<xsl:value-of select="$odd"/>","colspan":1, "rowspan":1, "Type" : "N", "Data" : <xsl:value-of select="format-number(@val, '###0.00','buh')"/>},
				</xsl:otherwise>
			</xsl:choose>
		</xsl:for-each>
	</xsl:template>
</xsl:stylesheet>

