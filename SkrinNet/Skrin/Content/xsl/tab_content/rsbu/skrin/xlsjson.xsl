<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">
	<msxsl:script language="JScript" implements-prefix="js">
		function replace(str_node,find,subs)
		{
		var str = str_node;
		var re=new RegExp("&lt;\/?[^&gt;]+(&gt;|$)", "ig")
    str=String(str).replace(re,subs);
    return str
    }
    function STransform(str_node){
    var str = str_node;
    var retval="";
    var fb="";
    fb=str.substring(0,1).toUpperCase();
    var body="";
    body=str.substring(1,512).toLowerCase();

    return String.Concat(fb,body);
    }
    function remCRLF(str_node){
    var str = String(str_node);
    str=str.replace("\n","");
    return str
    }
    function FormatLineCode(nl,cnt){
    var str=String(nl);
    var ntypes=String(cnt);

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
    aCodesMod[index][1] =aCodesMod[index][1] + "," + aTmp[1];
    }
    }
    for(var i=0; i &lt; aCodesMod.length; i++){
    ret_val = ret_val + ((i==0)?"":" / ") + aCodesMod[i][0]+((ntypes &gt; 1)? "(" + aCodesMod[i][1] + ")":"");
		}
		return ret_val;
		}
		function aScan(arr,val,ind){
		var ret_val=-1;
		for(var i=0; i &lt; arr.length; i++){
    if(arr[i][0]==val){
    ret_val=i;
    i=arr.length+1
    }

    }
    return ret_val
    }
    function GetMult(nl){
    var val= nl.toUpperCase();
    var retval="0";
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
	<xsl:decimal-format name="buh" NaN="'-'" decimal-separator="." grouping-separator=" "/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="iss_profile">
		{WorkBook : [<xsl:apply-templates select="profile"></xsl:apply-templates>]}
	</xsl:template>
	<xsl:template match="profile">

		<xsl:if test="f">
			
		<xsl:for-each select="f">
			<xsl:for-each select="s">
				{WorkSheet:"ФОРМА № <xsl:value-of select="../@form_no"/><xsl:if test="@id &gt;0">(<xsl:value-of select="@id"/>)</xsl:if> (СКРИН)",
				"Cols":[310,80,<xsl:for-each select="fa[1]/Form[1]/c">120<xsl:if test="position()!=last()">,</xsl:if></xsl:for-each>],"IssName": "<xsl:value-of disable-output-escaping="yes" select="//name/@val"/>", "Style": "Header", "Header" : "Бухгалтерская отчетность по РСБУ за <xsl:for-each select="//per"><xsl:if test="@sel=1"><xsl:value-of select="@name"/></xsl:if></xsl:for-each> по данным <xsl:choose><xsl:when test="//fn/@val=0">неконсолидированной</xsl:when><xsl:when test="//fn/@val=1">консолидированной</xsl:when></xsl:choose> бухгалтерской отчетности, <xsl:for-each select="//currency"><xsl:if test="@sel=1"><xsl:value-of select ="@name"/></xsl:if></xsl:for-each>",
				"SheetData" : [
				{Row :[{"Style" : "subcaption","colspan":<xsl:value-of select="count(fa[1]/Form[1]/c)+2"/>, "rowspan":1, "Type" : "S", "Data" : "<xsl:if test="position()>1">
					<xsl:value-of select="js:STransform(string(@name))"/>
				</xsl:if><xsl:if test="position()=1"><xsl:value-of select="@name"/>(ФОРМА № <xsl:value-of select="../@form_no"/>)</xsl:if>"},]},
				<xsl:for-each select="fa">
					<xsl:if test="position()=1">
						{Row :[{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Наименование показателя"},
						{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Код строки"},
						<xsl:for-each select="Form[1]/c">
							{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "<xsl:value-of select="js:remCRLF(string(@col))"/>"},
						</xsl:for-each>]},
						<xsl:if test="//cs/@val &gt; 1 ">
							{Row :[{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : "Курс <xsl:value-of select="//kurs_header/@val"/>"},{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : ""},<xsl:for-each select="Form[1]/c">
								{"Style" : "even","colspan":1, "rowspan":1, Type:"N", "Data" : <xsl:value-of select="v/@kurs"/>},
							</xsl:for-each>]},
							{Row :[{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : "Курс на дату"},{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : ""},<xsl:for-each select="Form[1]/c">
								{"Style" : "even","colspan":1, "rowspan":1, Type:"D", "Data" : "<xsl:value-of select="v/@cd"/>"},
							</xsl:for-each>]},
						</xsl:if>

					</xsl:if>
					<xsl:if test="string-length(@name) &gt; 0">
						<xsl:if test="(../../@form_no=1 and position()=1)">
							{Row :[{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : "Актив"},{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : ""},<xsl:for-each select="Form[1]/c">{"Style" : "bold","colspan":1, "rowspan":1, Type:"N", "Data" : ""},</xsl:for-each>]},
						</xsl:if>
						{Row :[{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : "<xsl:value-of select="@name"/>"},{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : ""},<xsl:for-each select="Form[1]/c">{"Style" : "bold","colspan":1, "rowspan":1, Type:"N", "Data" : ""},</xsl:for-each>]},
					</xsl:if>
					<xsl:for-each select="Form">
						{Row :[{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
							<xsl:otherwise>even</xsl:otherwise>
						</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of disable-output-escaping ="yes" select="@name"/>"},
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
							<xsl:otherwise>even</xsl:otherwise>
						</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of disable-output-escaping ="yes" select="@line_code"/>"},
						<xsl:call-template name="num_nodes">
							<xsl:with-param name="odd">
								<xsl:choose>
									<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd_right</xsl:when>
									<xsl:otherwise>even_right</xsl:otherwise>
								</xsl:choose>
							</xsl:with-param>
							<xsl:with-param name="nodes" select="c"/>
						</xsl:call-template>
						]},

						<xsl:if test="//f/@form_no=1">
							<xsl:if test="@line_code=300 or @line_code=1600">
								{Row :[{"Style" : "bold","colspan":1, "rowspan":1, Type:"S", "Data" : "Пассив"},{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : ""},<xsl:for-each select="c">{"Style" : "bold","colspan":1, "rowspan":1, Type:"N", "Data" : ""},</xsl:for-each>]},
							</xsl:if>
						</xsl:if>

					</xsl:for-each>
				</xsl:for-each>
				]},

			</xsl:for-each>
		</xsl:for-each>
		</xsl:if>
		

		
		<xsl:if test="headers">
			{WorkSheet:"Сводный отчет",
			<xsl:for-each select="headers">
				<xsl:if test="position()=1">
					"Cols":[280,120,<xsl:for-each select="LA/dta">
						120<xsl:if test="position()!=last()">,</xsl:if>
					</xsl:for-each>],
					"IssName": "<xsl:value-of select="//name/@val"/>", "Style": "Header",
					"Header" : "Сводный отчет по данным <xsl:choose><xsl:when test="//fn/@val=0">неконсолидированной</xsl:when><xsl:when test="//fn/@val=1">консолидированной</xsl:when></xsl:choose> бухгалтерской отчетности, <xsl:for-each select="//currency"><xsl:if test="@sel=1"><xsl:value-of select ="@name"/></xsl:if></xsl:for-each>",
					"SheetData" : [
					{Row :[{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Наименование показателя"},
					{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "Код строки"},
					<xsl:for-each select="LA/dta">{"Style" : "TabHeader","colspan":1, "rowspan":1, Type:"S", "Data" : "<xsl:value-of select="@yn"/>"},</xsl:for-each>]},
				</xsl:if>
				<xsl:if test="//cur and position()=1">
					{Row :[{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : "Курс <xsl:value-of select="//kurs_header/@val"/>"},{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : ""},<xsl:for-each select="//cur">
						{"Style" : "even","colspan":1, "rowspan":1, Type:"N", "Data" : <xsl:value-of select="@kurs"/>},
					</xsl:for-each>]},
					{Row :[{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : "Курс на дату"},{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : ""},<xsl:for-each select="//cur">
            {"Style" : "even_date","colspan":1, "rowspan":1, Type:"D", "Data" : "<xsl:value-of select="@cd"/>"},
					</xsl:for-each>]},
				</xsl:if>
				{Row :[{"Style" : "<xsl:choose><xsl:when test="contains(@name,'font')">subcaption</xsl:when><xsl:otherwise>razdel</xsl:otherwise></xsl:choose>","colspan":<xsl:value-of select="count(LA[1]/dta)+2"/>, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of  disable-output-escaping="yes" select ="js:replace(string(@name),'&lt;font style=&quot;color: #0066CC;font-size: 15px;font-weight: 700;&quot;&gt;','')"/>"},]},
				<xsl:if test="string-length(LA/@line_agg)>0">
					<xsl:for-each select ="LA">
						{Row :[{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
							<xsl:otherwise>even</xsl:otherwise>
						</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of disable-output-escaping ="yes" select="@name"/>"},
						{"Style" : "<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
							<xsl:otherwise>even</xsl:otherwise>
						</xsl:choose>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of disable-output-escaping ="yes" select="js:FormatLineCode(string(@line_agg),string(//ntypes/@val))"/>"},
						<xsl:call-template name="num_nodes_mult">
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
				</xsl:if>
			</xsl:for-each>
			<xsl:for-each select="comments"	>
				{Row :[{"Style" : "even","colspan":4, "rowspan":1, Type:"S", "Data" : "(<xsl:value-of select="@idx"/>) - <xsl:value-of select="tt/@comment"/>"},]},
			</xsl:for-each>
			]},

		</xsl:if>
	</xsl:template>

	<xsl:template name="num_nodes">
		<xsl:param name="odd"/>
		<xsl:param name="nodes"/>
		<xsl:for-each select="$nodes">
			<xsl:choose>
				<xsl:when test="v/@val=-0.001 or string-length(v/@val)=0">
					{"Style" : "<xsl:value-of select="$odd"/>","colspan":1, "rowspan":1, "Type" : "N", "Data" : "-"},
				</xsl:when>
				<xsl:otherwise>
					{"Style" : "<xsl:value-of select="$odd"/>","colspan":1, "rowspan":1, "Type" : "N", "Data" : <xsl:value-of select="format-number(js:GetMult(string(v/@val)) div 1000, '###0.00','buh')"/>},
				</xsl:otherwise>
			</xsl:choose>
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="num_nodes_mult">
		<xsl:param name="odd"/>
		<xsl:param name="nodes"/>
		<xsl:for-each select="$nodes">
				<xsl:choose>
					<xsl:when test ="@val=-0.001">
						{"Style" : "<xsl:value-of select="$odd"/>","colspan":1, "rowspan":1, "Type" : "N", "Data" : "-"},
					</xsl:when>
					<xsl:otherwise>
							{"Style" : "<xsl:value-of select="$odd"/>","colspan":1, "rowspan":1, "Type" : "N", "Data" : <xsl:value-of select="format-number(js:GetMult(string(@val)) div 1000, '###0.00','buh')"/>},
					</xsl:otherwise>
				</xsl:choose>
		</xsl:for-each>
	</xsl:template>
</xsl:stylesheet>

