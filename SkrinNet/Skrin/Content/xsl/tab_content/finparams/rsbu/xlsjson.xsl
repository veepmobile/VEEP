<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">
	<msxsl:script language="JScript" implements-prefix="js">
    function upper(node)
    {
    var str = node;
    return str.toUpperCase();
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

    function FormatLineCode(nl,cnt){
    var str=nl;
    var ntypes=cnt;

    str=str.substr(0,str.length-1);
    var aCodes=str.split("/");
    var aCodesMod=new Array();
    var aTmp = new Array();
    var index=-1
    var ret_val="";
    for(var i=0; i &lt; aCodes.length; i++){
		aTmp=aCodes[i].split("_");
		index=aScan(aCodesMod,aTmp[0])
		if(index &lt; 0){
		aCodesMod[aCodesMod.length]=aTmp;
		}else{
		aCodesMod[index][1] +="," + aTmp[1];
		}
		}
		for(var i=0; i &lt; aCodesMod.length; i++){
		ret_val = ret_val + ((i==0)?"":" / ") + aCodesMod[i][0]+((ntypes &gt; 1)? "(" + aCodesMod[i][1] + ")":"");
		}
		return ret_val;
		}


		function aScan(arr,val){
		var ret_val=-1;
		for(var i=0; i &lt; arr.length; i++){
		if(arr[i][0]==val){
		ret_val=i;
		i=arr.length+1
		}

		}
		return ret_val
		}
	</msxsl:script>
	<xsl:output method="text" version="4.0" encoding="utf-8"/>
	<xsl:decimal-format name="buh" NaN="-" decimal-separator="."/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="iss_profile">
		{WorkBook : [<xsl:apply-templates select="profile"></xsl:apply-templates>]}
	</xsl:template>
	<xsl:template match="profile">
		{WorkSheet:"ОФП РСБУ",
		"Cols":[370,70,<xsl:for-each select="col_headers">120<xsl:if test="position()!=last()">,</xsl:if></xsl:for-each>],
		"IssName": "<xsl:value-of disable-output-escaping="yes" select="//name/@val"/>", "Style": "Header", "Header" : "Финансовые показатели по РСБУ  по данным <xsl:choose><xsl:when test="//ed/@val=0">неконсолидированной</xsl:when><xsl:when test="//ed/@val=1">консолидированной</xsl:when></xsl:choose> бухгалтерской отчетности, <xsl:for-each select="//currency"><xsl:if test="@sel=1"><xsl:value-of select ="@name"/></xsl:if></xsl:for-each>",
		"SheetData" : [
			<xsl:for-each select="f">
				{Row : [{"Style" : "subcaption","colspan":<xsl:value-of select="count(//f[1]/headers[1]/LA[1]/dta)+2"/>, "rowspan":1, Type:"S", "Data" : "<xsl:value-of select="@form_header"/>"},]},
			<xsl:for-each select="headers/LA">
				<xsl:if test="position()=1">
					{Row : [{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Наименование показателя"},
					{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Код строки"},
					    <xsl:for-each select="dta">
								{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "<xsl:value-of select="@yn"/>"},
					    </xsl:for-each>
						]},
						<xsl:if test="//cur">
							{Row :[{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : "Курс <xsl:value-of select="//kurs_header/@val"/>"},{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : ""},<xsl:for-each select="//cur">{"Style" : "even_right8","colspan":1, "rowspan":1, Type:"N", "Data" : <xsl:value-of select="@kurs"/>},</xsl:for-each>]},
							{Row :[{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : "Курс на дату"},{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : ""},<xsl:for-each select="//cur">{"Style" : "even_date","colspan":1, "rowspan":1, Type:"D", "Data" : "<xsl:value-of select="@cd"/>"},</xsl:for-each>]},
						</xsl:if>

					</xsl:if>
					<xsl:choose>
					<xsl:when test="@id=6">
						{Row :[{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : "АКТИВ"},
						    <xsl:for-each select="dta">
									{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
							</xsl:for-each>
							]},
						</xsl:when>
					<xsl:when test="@id=30">
						{Row :[{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : "ПАССИВ"},
						    <xsl:for-each select="dta">
								{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
							</xsl:for-each>
							]},
						</xsl:when>
                        
                    <xsl:when test="count(../@parent_name)&gt;0 and ../@parent_name != ''">
                        {Row :[{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : "<xsl:value-of disable-output-escaping="yes" select="../@parent_name"/>"},
                            <xsl:for-each select="dta">
                                {"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
                            </xsl:for-each>
                            ]},
                        </xsl:when>
                    </xsl:choose>
<!--				{Row :[{"Style" : "<xsl:choose><xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when><xsl:otherwise>even</xsl:otherwise></xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:if test="../../@form_no=1"><xsl:value-of disable-output-escaping ="yes" select="js:upper(string(../@name))"/></xsl:if><xsl:if test="../../@form_no &gt; 1"><xsl:value-of disable-output-escaping ="yes" select="@name"/></xsl:if>"}, -->
                    {Row :[{"Style" : "<xsl:choose><xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when><xsl:otherwise>even</xsl:otherwise></xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:if test="../../@form_no=1"><xsl:if test="count(../../@form_tp)=0 and (contains(@name,'Итого') or contains(@name,'ИТОГО'))"><xsl:value-of disable-output-escaping ="yes" select="js:upper(string(../@name))"/></xsl:if><xsl:if test="count(../../@form_tp)&gt;0"><xsl:value-of disable-output-escaping ="yes" select="../@name"/></xsl:if></xsl:if><xsl:if test="../../@form_no &gt; 1"><xsl:value-of disable-output-escaping ="yes" select="@name"/></xsl:if><xsl:if test="not(contains(@name,'Итого') or contains(@name,'ИТОГО') ) and ../../@form_no=1"><xsl:value-of disable-output-escaping ="yes" select="@name"/></xsl:if>"},
					{"Style" : "<xsl:choose><xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when><xsl:otherwise>even</xsl:otherwise></xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of select="js:FormatLineCode(string(@line_agg),string(//ntypes/@val))"/>"},
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
		<xsl:for-each select="comments"	>
			{Row :[{"Style" : "even","colspan":4, "rowspan":1, Type:"S", "Data" : "(<xsl:value-of select="@idx"/>) - <xsl:value-of select="tt/@comment"/>"},]},
		</xsl:for-each>
		]},
		
</xsl:template>
<xsl:template name="num_nodes">
	<xsl:param name="odd"/>
	<xsl:param name="nodes"/>
	<xsl:for-each select="$nodes">
<!--	{"Style" : "<xsl:value-of select="$odd"/>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:choose><xsl:when test ="@val=-0.001">-</xsl:when><xsl:otherwise><xsl:value-of select="format-number(@val div 1000,'###0.00','buh')"/></xsl:otherwise></xsl:choose>"}, -->
        {"Style" : "<xsl:value-of select="$odd"/>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:choose><xsl:when test ="@val=-0.001">-</xsl:when><xsl:otherwise><xsl:if test="count(@dv)&gt;0"><xsl:value-of select="format-number(js:GetMult(string(@val)) div @dv ,'###0.00','buh')"/></xsl:if><xsl:if test="count(@dv)=0"><xsl:value-of select="format-number(js:GetMult(string(@val)) div 1000,'###0.00','buh')"/></xsl:if></xsl:otherwise></xsl:choose>"},
    </xsl:for-each>
</xsl:template>
</xsl:stylesheet> 

