<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">
	<msxsl:script language="JScript" implements-prefix="js">
		function replace(str_node,find,subs)
		{
		var str="";
		if(str_node.length){
		str= str_node;
		var re=new RegExp("\"", "ig");
		str=String(str).replace(re,subs);
		}
		return str
		}
		function GetMult(nl){
		var val=0
		var retval="0"
		var mult,exp;
		if(nl.length){
		val=String(nl.nextNode().text);
		if(val.indexOf("E") &lt; 0){
		retval=val
		}else{
		mult=val.substring(0,val.indexOf("E"));
		exp=val.substring(val.indexOf("E")+1,val.length);
		exp=Math.pow(10,exp);
		retval=mult*exp;
		}
		}
		return retval;
		}
	</msxsl:script>
	<xsl:output method="html" version="4.0" encoding="utf-8"/>
	<xsl:decimal-format name="buh" NaN="-" decimal-separator="." grouping-separator=" "/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="iss_profile">
		{WorkBook : [<xsl:apply-templates select="profile"></xsl:apply-templates>]}
	</xsl:template>
	<xsl:template match="profile">
		
		<xsl:if test="DR">
			{WorkSheet:"DR",
			"Cols":[80,120,200,200],"IssName": "<xsl:value-of disable-output-escaping="yes" select="js:replace(string(//name/@val),'&quot;','\&quot;')"/>", "Style": "Header", "Header" : "Депозитарные расписки",
			"SheetData" : [
			{Row :[
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Дата допуска к торгам"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Вид ценной бумаги"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Вид  депонированных ценных бумаг"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Отношение ДР к акциям"},
			]},
			<xsl:for-each select="DR">
						{Row :[
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
							<xsl:otherwise>even</xsl:otherwise>
						</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of  select="@dt"/>"},
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
							<xsl:otherwise>even</xsl:otherwise>
							</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of  select="DRT/@name"/>"},
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
							<xsl:otherwise>even</xsl:otherwise>
							</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of  select="DRT/@DR_Depon"/>"},
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
							<xsl:otherwise>even</xsl:otherwise>
							</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of  select="DRT/@Otnosh"/>"},
			]},
		</xsl:for-each>
					]},
		</xsl:if>

		
	</xsl:template>
</xsl:stylesheet> 

