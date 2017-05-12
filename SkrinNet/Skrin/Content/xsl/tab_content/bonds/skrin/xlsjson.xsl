<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">
	<msxsl:script language="JScript" implements-prefix="js">
    function replace(str_node,find,subs)
    {
    var str= str_node
    var re=new RegExp("\"", "ig");
    str=String(str).replace(re,subs);
    return str
    }
    function GetMult(nl){
    var val=nl.toUpperCase();
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
	<xsl:output method="html" version="4.0" encoding="utf-8"/>
	<xsl:decimal-format name="buh" NaN="-" decimal-separator="." grouping-separator=" "/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="iss_profile">
		{WorkBook : [<xsl:apply-templates select="profile"></xsl:apply-templates>]}
	</xsl:template>
	<xsl:template match="profile">
		
		<xsl:if test="issues">
			{WorkSheet:"Облигации(СКРИН)",
			"Cols":[80,120,200,200,200,200,200,200,100,100,100],"IssName": "<xsl:value-of disable-output-escaping="yes" select="js:replace(string(//name/@val),'&quot;','\&quot;')"/>", "Style": "Header", "Header" : "Выпуски облигаций",
			"SheetData" : [
			{Row :[
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Дата государственной регистрации"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Государственный регистрационный номер"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Вид ценной бумаги"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Номинальная стоимость каждой ценной бумаги выпуска"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Количество размещенных ценных бумаг, шт."},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Количество в обращении ценных бумаг, шт."},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Способ размещения"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Текущее состояние ценных бумаг выпуска"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Дата погашения"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Срок погашения"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Кол-во купонов"},
			]},
			<xsl:for-each select="issues">
						{Row :[
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
							<xsl:otherwise>even</xsl:otherwise>
						</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of  select="@rd"/>"},
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
							<xsl:otherwise>even</xsl:otherwise>
							</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of  select="@reg_no"/>"},
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
							<xsl:otherwise>even</xsl:otherwise>
							</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of  select="@stype"/>"},
							
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd_right</xsl:when>
							<xsl:otherwise>even_right</xsl:otherwise>
						</xsl:choose>","colspan":1, "rowspan":1, "Type" : "N", "Data" : <xsl:value-of  select="format-number(js:GetMult(string(@face_value)),'###0.00#########################','buh')"/>},
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd_right</xsl:when>
							<xsl:otherwise>even_right</xsl:otherwise>
						</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:choose><xsl:when test="contains(@shares_declared,'/')"><xsl:value-of select="format-number(substring-before(@shares_declared,' '),'# ##0','buh')"/>.<xsl:value-of select="format-number(substring-before(substring-after(@shares_declared,' '),'/'),'# ##0','buh')"/>/<xsl:value-of select="format-number(substring-after(@shares_declared,'/'),'# ##0','buh')"/></xsl:when><xsl:otherwise><xsl:value-of select="format-number(@shares_declared,'# ##0.################','buh')"/></xsl:otherwise></xsl:choose>"},
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd_right</xsl:when>
							<xsl:otherwise>even_right</xsl:otherwise>
						</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:choose><xsl:when test="contains(@shares_rolling,'/')"><xsl:value-of select="format-number(substring-before(@shares_rolling,' '),'# ##0','buh')"/>.<xsl:value-of select="format-number(substring-before(substring-after(@shares_rolling,' '),'/'),'# ##0','buh')"/>/<xsl:value-of select="format-number(substring-after(@shares_rolling,'/'),'# ##0','buh')"/></xsl:when><xsl:otherwise><xsl:value-of select="format-number(@shares_rolling,'# ##0.################','buh')"/></xsl:otherwise></xsl:choose>"},

						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
							<xsl:otherwise>even</xsl:otherwise>
						</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of  select="@pt_name"/>"},
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
							<xsl:otherwise>even</xsl:otherwise>
						</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of  select="@ps_name"/>"},
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
							<xsl:otherwise>even</xsl:otherwise>
							</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of  select="@red"/>"},
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
							<xsl:otherwise>even</xsl:otherwise>
							</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of  select="@redemption_end_period"/>"},
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
							<xsl:otherwise>even</xsl:otherwise>
							</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:choose><xsl:when test="@coupons=0">-</xsl:when><xsl:otherwise><xsl:value-of select="@coupons"/></xsl:otherwise></xsl:choose>"},
				]},
			</xsl:for-each>
					]},
		</xsl:if>

		
	</xsl:template>
</xsl:stylesheet> 

