<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">
	<msxsl:script language="JScript" implements-prefix="js">
		function replace(str_node,find,subs)
		{
		var str = str_node.nextNode().text;
		var re=new RegExp("&lt;\/?[^&gt;]+(&gt;|$)", "ig")
		str=String(str).replace(re,subs);
		return str
		}
	</msxsl:script>
	<xsl:output method="html" version="4.0" encoding="utf-8"/>
	<xsl:decimal-format name="buh" NaN="" decimal-separator="." grouping-separator=" "/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="iss_profile">
		{WorkBook : [<xsl:apply-templates select="profile"></xsl:apply-templates>]}
	</xsl:template>
	<xsl:template match="profile">
		{WorkSheet:"Дивиденды (СКРИН)",
		"Cols":[130,80,80,80,80,80,80,130,130],"IssName": "<xsl:value-of disable-output-escaping="yes" select="//name/@val"/>", "Style": "Header", "Header" : "Сведения об объявленных (начисленных) дивидендах по акциям.",
		"SheetData" : [
				<xsl:for-each select="A">
					<xsl:if test="position()=1">
						{Row :[{"Style" : "TabHeader","colspan":1, "rowspan":2, Type:"S", "Data" : "Тип акции"},
						{"Style" : "TabHeader","colspan":1, "rowspan":2, Type:"S", "Data" : "Дата начала периода"},
						{"Style" : "TabHeader","colspan":1, "rowspan":2, Type:"S", "Data" : "Дата окгчания периода"},
						{"Style" : "TabHeader","colspan":2, "rowspan":1, Type:"S", "Data" : "Дата закрытия реестра"},
						{"Style" : "TabHeader","colspan":2, "rowspan":1, Type:"S", "Data" : "Размер див. на 1 акцию, руб"},
						{"Style" : "TabHeader","colspan":2, "rowspan":1, Type:"S", "Data" : "Общая сумма начисл., руб"},]},
						{Row :[{"Style" : "TabHeader","colspan":1, "rowspan":1, "Index":3,Type:"S", "Data" : "Предварительная "},
						{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Утвержденная "},
						{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Предварительный "},
						{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Утвержденный "},
						{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Предварительная "},
						{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Утвержденная "},]},
					</xsl:if>
					{Row :[{"Style" : "<xsl:choose>
						<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
						<xsl:otherwise>even</xsl:otherwise>
					</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of select="@sec_type"/>"},
					{"Style" : "<xsl:choose>
						<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
						<xsl:otherwise>even</xsl:otherwise>
					</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of select="substring-before(@s_date,'-')"/>"},
					{"Style" : "<xsl:choose>
						<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
						<xsl:otherwise>even</xsl:otherwise>
					</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of select="substring-after(@s_date,'-')"/>"},
					{"Style" : "<xsl:choose>
						<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
						<xsl:otherwise>even</xsl:otherwise>
					</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of select="@l_date"/>"},
					{"Style" : "<xsl:choose>
						<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
						<xsl:otherwise>even</xsl:otherwise>
					</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of select="@pl_date"/>"},
					{"Style" : "<xsl:choose>
						<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd_right</xsl:when>
						<xsl:otherwise>even_right</xsl:otherwise>
					</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of select="@pvalue"/>"},
					{"Style" : "<xsl:choose>
						<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd_right</xsl:when>
						<xsl:otherwise>even_right</xsl:otherwise>
					</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of select="@value"/>"},
					{"Style" : "<xsl:choose>
						<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd_right</xsl:when>
						<xsl:otherwise>even_right</xsl:otherwise>
					</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of select="format-number(@ptotal,'###0.00','buh')"/>"},
					{"Style" : "<xsl:choose>
						<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd_right</xsl:when>
						<xsl:otherwise>even_right</xsl:otherwise>
					</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of select="format-number(@total,'###0.00','buh')"/>"},

					]},
				</xsl:for-each>
				]},

	</xsl:template>
</xsl:stylesheet> 

