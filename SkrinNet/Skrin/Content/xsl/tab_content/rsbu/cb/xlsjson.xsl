<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">
	<msxsl:script language="JScript" implements-prefix="js">
    function remspace(str_node)
    {
    var str = str_node;
    var re=new RegExp("\s", "ig")
    str=String(str).replace(re,"");
    return str
    }

    function STransform(str_node){
    var str = str_node;
    var fb=str.substring(0,1).toUpperCase();
    var body=str.substring(1,512).toLowerCase();
    return fb+body;
    }
    function remCRLF(str_node){
    var str = String(str_node);
    str=str.replace("\n","");
    var re=new RegExp("\"", "ig")
    str=String(str).replace(re,"'");
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
	<xsl:output method="text" version="4.0" encoding="utf-8"/>
	<xsl:decimal-format name="buh" NaN="" decimal-separator="." grouping-separator=" "/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="iss_profile">
		{WorkBook : [<xsl:apply-templates select="profile"></xsl:apply-templates>]}
	</xsl:template>
	<xsl:template match="profile">
		<xsl:if test="count(//Per101)>0">
			{WorkSheet:"Форма №101",
			"Cols":[60,220,75,75,75,75,75,75,75,75,75,75,75,75],"IssName": "<xsl:value-of disable-output-escaping="yes"  select="//name/@val"/>", "Style": "Header", "Header" : "Оборотная ведомость по счетам бухгалтерского учета (Форма №101) за <xsl:for-each select="//per"><xsl:if test="@sel=1"><xsl:value-of select="@name"/></xsl:if></xsl:for-each>, тыс. рублей.",
			"SheetData" : [
			{Row :[
			{"Style" : "TabHeader","colspan":2, "rowspan":3, Type:"S", "Data" : "Номер счета второго порядка"},
			{"Style" : "TabHeader","colspan":3, "rowspan":2, Type:"S", "Data" : "Входящие остатки"},
			{"Style" : "TabHeader","colspan":6, "rowspan":1, Type:"S", "Data" : "Обороты за отчетный период"},
			{"Style" : "TabHeader","colspan":3, "rowspan":2, Type:"S", "Data" : "Исходящие остатки"},
			]},

			{Row :[
			{"Style" : "TabHeader","colspan":3, "rowspan":1, Index:5, Type:"S", "Data" : "по дебету"},
			{"Style" : "TabHeader","colspan":3, "rowspan":1, Type:"S", "Data" : "по кредиту"},
			]},
			{Row : [
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Index:2, Type:"S", "Data" : "В рублях"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Ин.вал., драг. металлы"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "итого"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "В рублях"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Ин.вал., драг. металлы"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "итого"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "В рублях"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Ин.вал., драг. металлы"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "итого"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "В рублях"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Ин.вал., драг. металлы"},
			{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "итого"},
			]},
					<xsl:for-each select="Per101">
						
						{Row : [
							{"Style" : "subcaption","colspan":14, "rowspan":1, Type:"S", "Data" : "<xsl:value-of select="@sect"/>"},
						]},
						
						<xsl:for-each select="QIV">
							
								{Row : [
								{"Style" : "razdel","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
								{"Style" : "razdel","colspan":1, "rowspan":1, Type:"S", "Data" : "<xsl:if test="@ap=1">Актив</xsl:if><xsl:if test="@ap=2">Пассив</xsl:if>"},
								{"Style" : "razdel","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
								{"Style" : "razdel","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
								{"Style" : "razdel","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
								{"Style" : "razdel","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
								{"Style" : "razdel","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
								{"Style" : "razdel","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
								{"Style" : "razdel","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
								{"Style" : "razdel","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
								{"Style" : "razdel","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
								{"Style" : "razdel","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
								{"Style" : "razdel","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
								{"Style" : "razdel","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
								]},
								<xsl:for-each select="F101">
									
										{Row : [
										{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : "<xsl:value-of select="@LN"/>"},
										{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : "<xsl:value-of select="js:remCRLF(string(@NM))"/>"},
										{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
										{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
										{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
										{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
										{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
										{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
										{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
										{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
										{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
										{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
										{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
										{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
										]},
										<xsl:for-each select="b">
											{Row : [
											{"Style" : "<xsl:choose><xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when><xsl:otherwise>even</xsl:otherwise></xsl:choose>", "colspan":1, "rowspan":1, Type:"S", "Data" : "<xsl:value-of select="@line_code"/>"},
											{"Style" : "<xsl:choose><xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when><xsl:otherwise>even</xsl:otherwise></xsl:choose>", "colspan":1, "rowspan":1, Type:"S", "Data" : "<xsl:value-of select="js:remCRLF(string(@name))"/>"},
											<xsl:variable name ="line" select ="cols"/>
											<xsl:variable name ="odd">
												<xsl:choose>
													<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd_right_0</xsl:when>
													<xsl:otherwise>even_right_0</xsl:otherwise>
												</xsl:choose>
											</xsl:variable>
											<xsl:for-each select="//Cols101">
												{"Style" : "<xsl:value-of select="$odd"/>","colspan":1, "rowspan":1, Type:"N", "Data" : "<xsl:variable name="code" select="@col_code"/><xsl:for-each select ="$line">
													
													<xsl:if test="$code=@col_code">
														<xsl:choose>
															<xsl:when test="js:remspace(string(@val)) != ''">
																<xsl:value-of select="format-number(js:GetMult(string(@val)),'###0.#','buh')"/>
															</xsl:when>
															<xsl:otherwise>"-"</xsl:otherwise>
														</xsl:choose>
													</xsl:if>


												</xsl:for-each><xsl:variable name ="ex">
													<xsl:call-template name="has_item">
														<xsl:with-param name="line" select="$line"/>
														<xsl:with-param name="col_code" select="@col_code"/>
													</xsl:call-template>
												</xsl:variable>
												<xsl:if test="string-length($ex) = 0">0</xsl:if>"},
											</xsl:for-each>
											]},

										</xsl:for-each>
									
								</xsl:for-each>
							
						</xsl:for-each>
							
					</xsl:for-each>
			]},
		</xsl:if>
		<xsl:if test="count(//F102)">
			{WorkSheet:"Форма №102",
			"Cols":[50,260,70,120,120,120],"IssName": "<xsl:value-of select="//name/@val"/>", "Style": "Header", "Header" : "Отчет о прибылях и убытках (Форма №102) за <xsl:for-each select="//per"><xsl:if test="@sel=1"><xsl:value-of select="@name"/></xsl:if></xsl:for-each>, тыс. рублей.",
      "SheetData" : [
      {Row :[
      {"Style" : "TabHeader","colspan":1, "rowspan":2, Type:"S", "Data" : "Номер п/п"},
      {"Style" : "TabHeader","colspan":1, "rowspan":2, Type:"S", "Data" : "Наименование статей"},
      {"Style" : "TabHeader","colspan":1, "rowspan":2, Type:"S", "Data" : "Символы"},
      {"Style" : "TabHeader","colspan":2, "rowspan":1, Type:"S", "Data" : "Суммы в рублях от операций"},
      {"Style" : "TabHeader","colspan":1, "rowspan":2, "Index":5, Type:"S", "Data" : "Всего"},
      ]},
      {Row :[
      {"Style" : "TabHeader","colspan":1, "rowspan":1, Index:3,Type:"S", "Data" : "В рублях"},
      {"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "В ин. валюте и драг.металлах"},
      ]},
      <xsl:for-each select="F102">
						{Row : [
						{"Style" : "<xsl:choose><xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when><xsl:otherwise>even</xsl:otherwise></xsl:choose>", "colspan":1, "rowspan":1, Type:"S", "Data" : "<xsl:value-of select="position()"/>"},
						{"Style" : "<xsl:choose><xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when><xsl:otherwise>even</xsl:otherwise></xsl:choose>", "colspan":1, "rowspan":1, Type:"S", "Data" : "<xsl:value-of select="js:remCRLF(string(@NM))"/>"},
						{"Style" : "<xsl:choose><xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when><xsl:otherwise>even</xsl:otherwise></xsl:choose>", "colspan":1, "rowspan":1, Type:"S", "Data" : "<xsl:value-of select="@LN"/>"},
							<xsl:variable name ="line" select ="Per102"/>
							<xsl:variable name ="odd">
								<xsl:choose>
									<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd_right_0</xsl:when>
									<xsl:otherwise>even_right_0</xsl:otherwise>
								</xsl:choose>
							</xsl:variable>
							<xsl:for-each select="//Cols102">
								{"Style" :"<xsl:value-of select="$odd"/>","colspan":1, "rowspan":1, Type:"S", "Data" :"<xsl:variable name="code" select="@col_code"/><xsl:for-each select ="$line"><xsl:if test="$code=@col_code"><xsl:choose><xsl:when test="js:remspace(string(@val)) != ''"><xsl:value-of select="format-number(js:GetMult(string(@val)),'###0.#','buh')"/></xsl:when><xsl:otherwise>-</xsl:otherwise></xsl:choose></xsl:if></xsl:for-each>"},
							</xsl:for-each>]},
					</xsl:for-each>
			]},
		</xsl:if>

	</xsl:template>
	<xsl:template name="has_item">
		<xsl:param name="line"/>
		<xsl:param name="col_code"/>
		<xsl:for-each select="$line">
			<xsl:if test="@col_code=$col_code">1</xsl:if>
		</xsl:for-each>
	</xsl:template>
</xsl:stylesheet> 

