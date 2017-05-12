<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">
	<msxsl:script language="JScript" implements-prefix="js">
    function GetMult(nl){
    var val= nl.toUpperCase(); <!--String(nl.nextNode().text);-->
    var retval="0"
    var mult,exp;
    if(val.indexOf("E") &lt; 0){
    retval=val
    }else{
    mult=val.substring(0,val.indexOf("E"));
    exp=val.substring(val.indexOf("E")+1,val.length);
    exp=Math.pow(10,exp);
    retval=mult*exp;
    }
    return retval;
    }
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
		{WorkSheet:"ФП МСФО",
		"Cols":[300,120],
		"IssName": "<xsl:value-of disable-output-escaping="yes" select="//name/@val"/>", "Style": "Header", "Header" : "Финансовые показатели МСФО за <xsl:for-each select="per"><xsl:if test="@sel=1"><xsl:value-of select="@name"/></xsl:if></xsl:for-each>",
		"SheetData" : [
		<xsl:for-each select="f">
			{Row :[{"Style" : "subcaption","colspan":2, "rowspan":1, Type:"S", "Data" : "<xsl:choose>
				<xsl:when test="@form_no=1">БУХГАЛТЕРСКИЙ БАЛАНС</xsl:when>
				<xsl:when test="@form_no=2">ОТЧЕТ О ПРИБЫЛЯХ И УБЫТКАХ</xsl:when>
			</xsl:choose>"},]},
			{Row :[{"Style" : "subcaption","colspan":2, "rowspan":1, Type:"S", "Data" : "<xsl:choose>
				<xsl:when test="@form_no=1">Consolidated balance sheet</xsl:when>
				<xsl:when test="@form_no=2">Consolidated Statements of Income</xsl:when>
			</xsl:choose>"},]},
			<xsl:for-each select="LA">
				<xsl:if test="position()=1">
					{Row :[{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Наименование показателя"},
					<xsl:for-each select="dta">
						{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "<xsl:value-of select="@yn"/>"},
					</xsl:for-each>]},
					</xsl:if>
					<xsl:if test="//f/@form_no=1 and @line_code=30">
						{Row :[{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : "ASSETS"},
							<xsl:for-each select="dta">
								{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
							</xsl:for-each>
							]},
					</xsl:if>
				
					<xsl:if test="//f/@form_no=2 and @line_code=100">
						{Row :[{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : "EQUITY and LIABILITIES"},
						<xsl:for-each select="dta">
							{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
						</xsl:for-each>
						]},
					</xsl:if>
				
					{Row :[{"Style" : "<xsl:choose><xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when><xsl:otherwise>even</xsl:otherwise></xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of disable-output-escaping ="yes" select="@name"/>"},
						<xsl:call-template name="num_nodes">
							<xsl:with-param name="odd">
								<xsl:choose>
									<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd_right</xsl:when>
									<xsl:otherwise>even_right</xsl:otherwise>
								</xsl:choose>
							</xsl:with-param>
							<xsl:with-param name="nodes" select="dta"/>
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
			<xsl:when test ="@val=-0.001">
				{"Style" : "<xsl:value-of select="$odd"/>","colspan":1, "rowspan":1, "Type" : "N", "Data" : "-"},
			</xsl:when>
			<xsl:otherwise>
				{"Style" : "<xsl:value-of select="$odd"/>","colspan":1, "rowspan":1, "Type" : "N", "Data" : <xsl:value-of select="format-number(js:GetMult(string(@val)), '###0.00','buh')"/>},
			</xsl:otherwise>
		</xsl:choose>
	</xsl:for-each>
</xsl:template>

</xsl:stylesheet> 

