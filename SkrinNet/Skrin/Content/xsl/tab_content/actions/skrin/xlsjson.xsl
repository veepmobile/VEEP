<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">
	<msxsl:script language="JScript" implements-prefix="js">
    function replace(str_node,find,subs)
    {
    var str="";
    if(str_node.length){
    str= str_node.nextNode().text;
    var re=new RegExp("\"", "ig");
    str=String(str).replace(re,subs);
    }
    return str
    }
    function GetMult(nl){
    var val= nl.toUpperCase();
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
		<xsl:if test="Issuer_Capitals[position()=last()] or IShares[position()=last()] or Shares_Future[position()=last()]">
			{WorkSheet:"УК(СКРИН) ",
				"Cols":[80,300,100,300],"IssName": "<xsl:value-of disable-output-escaping="yes" select="js:replace(//name/@val,'&quot;','\&quot;')"/>", "Style": "Header", "Header" : "Уставный капитал",
			"SheetData" : [
			<xsl:if test="Issuer_Capitals[position()=last()]">
			{Row :[{"Style" : "bold","colspan":4, "rowspan":1, Type:"S", "Data" : "Размер уставного капитала"},]},
			{Row :[
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Дата"},
			{"Style" : "TabHeader","colspan":2, "rowspan":1, Type:"S", "Data" : "Наименование показателя"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Значение, руб."},]},
			
				<xsl:for-each select="Issuer_Capitals">
					{Row :[
					{"Style" : "<xsl:choose>
						<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
						<xsl:otherwise>even</xsl:otherwise>
					</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of  select="@dt"/>"},
					{"Style" : "<xsl:choose>
						<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
						<xsl:otherwise>even</xsl:otherwise>
					</xsl:choose>","colspan":2, "rowspan":1,  "Type" : "S", "Data" : "Размер уставного капитала:"},
					{"Style" : "<xsl:choose>
						<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd_right</xsl:when>
						<xsl:otherwise>even_right</xsl:otherwise>
					</xsl:choose>","colspan":1, "rowspan":1,  "Type" : "N", "Data" : <xsl:value-of select="format-number(@val, '###0.00','buh')"/>},
					]},
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="IShares[position()=last()]">
				{Row :[{"Style" : "bold","colspan":4, "rowspan":1, Type:"S", "Data" : "Размещенные акции"},]},
				{Row :[
				{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Дата"},
				{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Вид акций"},
				{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Номинал, руб."},
				{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Количество, шт."},]},
				<xsl:for-each select="IShares">
					{Row :[
					{"Style" : "<xsl:choose>
						<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
						<xsl:otherwise>even</xsl:otherwise>
					</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of  select="@dt"/>"},
					{"Style" : "<xsl:choose>
						<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
						<xsl:otherwise>even</xsl:otherwise>
					</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of  select="@st"/>"},
					{"Style" : "<xsl:choose>
						<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd_right</xsl:when>
						<xsl:otherwise>even_right</xsl:otherwise>
					</xsl:choose>","colspan":1, "rowspan":1, "Type" : "N", "Data" : <xsl:value-of  select="format-number(js:GetMult(string(@face_value)), '###0.00##############', 'buh')"/>},
					{"Style" : "<xsl:choose>
						<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd_right</xsl:when>
						<xsl:otherwise>even_right</xsl:otherwise>
					</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:choose>
						<xsl:when test="contains(@sn,'/')">
							<xsl:value-of select="format-number(substring-before(@sn,' '),'# ##0','buh')"/>.<xsl:value-of select="format-number(substring-before(substring-after(@sn,' '),'/'),'# ##0','buh')"/>/<xsl:value-of select="format-number(substring-after(@sn,'/'),'# ##0','buh')"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="format-number(@sn,'# ##0.################','buh')"/>
						</xsl:otherwise>
					</xsl:choose>"},]},
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="Shares_Future[position()=last()]">
				{Row :[{"Style" : "bold","colspan":4, "rowspan":1, Type:"S", "Data" : "Объявленные акции"},]},
				{Row :[
				{"Style" : "TabHeader","colspan":2, "rowspan":1, Type:"S", "Data" : "Вид акций"},
				{"Style" : "TabHeader","colspan":1, "rowspan":1,  Type:"S", "Data" : "Номинал, руб."},
				{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Количество, шт."},]},
				<xsl:for-each select="Shares_Future">
					{Row :[
					{"Style" : "<xsl:choose>
						<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
						<xsl:otherwise>even</xsl:otherwise>
					</xsl:choose>","colspan":2, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of  select="@sectype"/>"},
					{"Style" : "<xsl:choose>
						<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd_right</xsl:when>
						<xsl:otherwise>even_right</xsl:otherwise>
					</xsl:choose>","colspan":1, "rowspan":1,  "Type" : "N", "Data" : "<xsl:value-of  select="format-number(js:GetMult(string(@face_value)), '# ##0.00##############', 'buh')"/>"},
					{"Style" : "<xsl:choose>
						<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd_right</xsl:when>
						<xsl:otherwise>even_right</xsl:otherwise>
					</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of disable-output-escaping="yes" select="format-number(@shares, '# ##0', 'buh')"/>"},
					]},
				</xsl:for-each>
			</xsl:if>
			]},
		</xsl:if>
		<xsl:if test="issues">
			{WorkSheet:"Акции(СКРИН)",
			"Cols":[80,100,100,200,200,200,100,200],"IssName": "<xsl:value-of disable-output-escaping="yes" select="js:replace(//name/@val,'&quot;','\&quot;')"/>", "Style": "Header", "Header" : "Выпуски акций",
			"SheetData" : [
			{Row :[
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Дата государственной регистрации"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Государственный регистрационный номер"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Вид ценной бумаги"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Номинальная стоимость каждой ценной бумаги выпуска"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Количество ценных бумаг, подлежавших размещению, шт."},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Количество в обращении  ценных бумаг, шт."},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Способ размещения"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Текущее состояние ценных бумаг выпуска"},]},
					<xsl:for-each select="issues">
						{Row :[
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
							<xsl:otherwise>even</xsl:otherwise>
						</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of  select="@rd"/>"},
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
							<xsl:otherwise>even</xsl:otherwise>
							</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of  select="js:replace(@reg_no,'&quot;','\&quot;')"/>"},
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
							<xsl:otherwise>even</xsl:otherwise>
						</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of  select="@action_type"/>"},
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd_right</xsl:when>
							<xsl:otherwise>even_right</xsl:otherwise>
						</xsl:choose>","colspan":1, "rowspan":1, "Type" : "N", "Data" : <xsl:value-of  select="format-number(js:GetMult(string(@face_value)),'###0.00#########################','buh')"/>},
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd_right</xsl:when>
							<xsl:otherwise>even_right</xsl:otherwise>
						</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:choose>
						<xsl:when test="contains(@shares_declared,'/')"><xsl:value-of select="format-number(substring-before(@shares_declared,' '),'# ##0','buh')"/>.<xsl:value-of select="format-number(substring-before(substring-after(@shares_declared,' '),'/'),'# ##0','buh')"/>/<xsl:value-of select="format-number(substring-after(@shares_declared,'/'),'# ##0','buh')"/></xsl:when>
						<xsl:otherwise><xsl:value-of select="format-number(@shares_declared,'# ##0.################','buh')"/></xsl:otherwise>
						</xsl:choose>"},
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd_right</xsl:when>
							<xsl:otherwise>even_right</xsl:otherwise>
						</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:choose>
						<xsl:when test="contains(@shares_declared,'/')"><xsl:value-of select="format-number(substring-before(@shares_rolling,' '),'# ##0','buh')"/>.<xsl:value-of select="format-number(substring-before(substring-after(@shares_rolling,' '),'/'),'# ##0','buh')"/>/<xsl:value-of select="format-number(substring-after(@shares_rolling,'/'),'# ##0','buh')"/></xsl:when>
						<xsl:otherwise><xsl:value-of select="format-number(@shares_rolling,'# ##0.################','buh')"/></xsl:otherwise>
						</xsl:choose>"},
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
							<xsl:otherwise>even</xsl:otherwise>
						</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of  select="@pt_name"/>"},
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
							<xsl:otherwise>even</xsl:otherwise>
						</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of  select="@ps_name"/>"},
						]},
					</xsl:for-each>
					]},
		</xsl:if>

		
	</xsl:template>
</xsl:stylesheet> 

