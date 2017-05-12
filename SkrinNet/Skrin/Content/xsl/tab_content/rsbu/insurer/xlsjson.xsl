<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">
	<msxsl:script language="JScript" implements-prefix="js">

        function norm_str(str_node) {
        var str = str_node;
        var re=new RegExp("\"", "ig")
        str=String(str).replace(re,"'");
        return str
        }

        function remCRLF(str_node){
        var str = String(str_node);
        str=str.replace("\n","");
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
	<xsl:template match="profile"  >
		<xsl:for-each select="forms">
            <xsl:variable name="f_no" select="@form_no" />
            <xsl:variable name="no_nm" select="@no_name" />
            <xsl:variable name="f_header" select="@form_header"/>
            <xsl:for-each select="//secs[@form_no=$f_no]">
                <xsl:variable name="s_no" select="@id" />
                <xsl:variable name="h_cnt" select="//cols[@form_no=$f_no and @section_no=$s_no]/@row_cnt" />
                <xsl:variable name="col_cnt" select="count(//cols[@form_no=$f_no and @section_no=$s_no and count(@col_span)=0])+2"/>
                {WorkSheet:"<xsl:value-of select="$no_nm"/><xsl:if test="@id!=0">(<xsl:value-of select="@id"/>)</xsl:if>",
                    "Cols":[510,80,<xsl:for-each select="//cols[@form_no=$f_no and @section_no=$s_no and count(@col_span)=0]">120<xsl:if test="position()!=last()">,</xsl:if></xsl:for-each>],"IssName": "<xsl:value-of disable-output-escaping="yes" select="//name/@val"/>", "Style": "Header", "Header" : "Бухгалтерская отчетность по РСБУ по данным банка России за <xsl:value-of select="//per[@sel=1]/@name"/><xsl:if test="@dim != ''"><xsl:text>, </xsl:text><xsl:value-of select="@dim" /></xsl:if>",
                    "SheetData" : [
                        {Row :[{"Style" : "subcaption","colspan":<xsl:value-of select="$col_cnt"/>, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of select="$f_header"/>"},]},
                        {Row :[{"Style" : "TabHeader","colspan":<xsl:value-of select="$col_cnt"/>, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of select="@header"/>"},]},

                        {Row :[{"Style" : "TabHeader","colspan":1, "rowspan":<xsl:value-of select="$h_cnt"/>, "Type":"S", "Data" : "Наименование показателя"},
                               {"Style" : "TabHeader","colspan":1, "rowspan":<xsl:value-of select="$h_cnt"/>, "Type":"S", "Data" : "Код строки"},
                                <xsl:for-each select="//cols[@form_no=$f_no and @section_no=$s_no and @hrow_no=1]">
                                    {"Style" : "TabHeader", "colspan":<xsl:if test="count(@col_span)=0">1</xsl:if><xsl:if test="count(@col_span)!=0"><xsl:value-of select="@col_span"/></xsl:if>, "rowspan":<xsl:if test="count(@row_span)=0">1</xsl:if><xsl:if test="count(@row_span)!=0"><xsl:value-of select="@row_span"/></xsl:if>, Type:"S", "Data" : "<xsl:value-of select="js:remCRLF(string(@colname))"/>"},
                                </xsl:for-each>]},
                        <xsl:if test="$h_cnt&gt;1">
                            {Row:[<xsl:for-each select="//cols[@form_no=$f_no and @section_no=$s_no and @hrow_no=2]">
                                    {"Style" : "TabHeader", "colspan":1, "rowspan":1, <xsl:if test="position()=1">"Index":<xsl:value-of select="@order_no+1"/>,</xsl:if> "Type":"S", "Data" : "<xsl:value-of select="js:remCRLF(string(@colname))"/>"},
                                </xsl:for-each>]},
                        </xsl:if>
                        <xsl:if test="//currency[@id = 1]/@sel = 0">
                            {Row :[{"Style" : "even","colspan":1, "rowspan":1, "Type":"S", "Data" : "Курс <xsl:value-of select="//kurs_header/@val"/>"},{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : ""},
                            <xsl:for-each select="//cols[@form_no=$f_no and @section_no=$s_no and count(@col_span)=0]">
                                {"Style" : "even","colspan":1, "rowspan":1, "Type":"N", "Data" : <xsl:value-of select="@kurs"/>},
                            </xsl:for-each>]},
                            {Row :[{"Style" : "even","colspan":1, "rowspan":1, "Type":"S", "Data" : "Курс на дату"},{"Style" : "even","colspan":1, "rowspan":1, "Type":"S", "Data" : ""},
                            <xsl:for-each select="//cols[@form_no=$f_no and @section_no=$s_no and count(@col_span)=0]">
                                {"Style" : "even","colspan":1, "rowspan":1, "Type":"S", "Data" : "<xsl:value-of select="@ld"/>"},
                            </xsl:for-each>]},
                        </xsl:if>

                        <xsl:call-template name="tbl_blok">
                            <xsl:with-param name="f_no" select="$f_no"/>
                            <xsl:with-param name="s_no" select="$s_no"/>
                            <xsl:with-param name="p_no" select="0"/>
                        </xsl:call-template>

                        <xsl:if test="count(//c[@form_no=$f_no and @section_no=$s_no]) != 0">
                            {Row :[{"Style" : "even","colspan":<xsl:value-of select="$col_cnt"/>, "rowspan":1, "Type":"S", "Data" : ""},]},
                            <xsl:for-each select="//c[@form_no=$f_no and @section_no=$s_no]">
                                {Row :[{"Style" : "even","colspan":<xsl:value-of select="$col_cnt"/>, "rowspan":1, "Type":"S", "Data" : "(<xsl:value-of select="@no"/>)  <xsl:value-of disable-output-escaping="yes" select="@text"/>"},]},
                            </xsl:for-each>
                        </xsl:if>

                ]},
            </xsl:for-each>
        </xsl:for-each>
	</xsl:template>

    <xsl:template name="tbl_blok">
        <xsl:param name="f_no"/>
        <xsl:param name="s_no"/>
        <xsl:param name="p_no"/>

        <xsl:for-each select="//rows[@form_no=$f_no and @section_no=$s_no and @parent_no=$p_no]">
            <xsl:variable name="r_no" select="@row_no"/>
            <xsl:variable name="r_style">
                <xsl:choose>
                    <xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
                    <xsl:otherwise>even</xsl:otherwise>
                </xsl:choose>
            </xsl:variable>

            <xsl:if test="@parent_name!=''">
                {Row :[{"Style" : "bold","colspan":1, "rowspan":1, "Type":"S", "Data" : "<xsl:value-of disable-output-escaping="yes" select="js:norm_str(string(@parent_name))"/>"},{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : ""},<xsl:for-each select="//cols[@form_no=$f_no and @section_no=$s_no and count(@col_span)=0]">{"Style" : "bold","colspan":1, "rowspan":1, Type:"N", "Data" : ""},</xsl:for-each>]},
            </xsl:if>
            <xsl:if test="count(//rows[@form_no=$f_no and @section_no=$s_no and @parent_no=$r_no]) != 0">
                {Row :[{"Style" : "bold","colspan":1, "rowspan":1, "Type":"S", "Data" : "<xsl:value-of disable-output-escaping="yes" select="js:norm_str(string(@row_name))"/><xsl:if test="@comment_no!=0">(<xsl:value-of select="@comment_no"/>)</xsl:if>"},{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : ""},<xsl:for-each select="//cols[@form_no=$f_no and @section_no=$s_no and count(@col_span)=0]">{"Style" : "bold","colspan":1, "rowspan":1, Type:"N", "Data" : ""},</xsl:for-each>]},
                <xsl:call-template name="tbl_blok">
                    <xsl:with-param name="f_no" select="$f_no"/>
                    <xsl:with-param name="s_no" select="$s_no"/>
                    <xsl:with-param name="p_no" select="$r_no"/>
                </xsl:call-template>
            </xsl:if>
            <xsl:if test="count(//rows[@form_no=$f_no and @section_no=$s_no and @parent_no=$r_no]) = 0">
                {Row :[{"Style" : "<xsl:value-of select="$r_style"/>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of disable-output-escaping ="yes" select="js:norm_str(string(@row_name))"/><xsl:if test="@comment_no!=0">(<xsl:value-of select="@comment_no"/>)</xsl:if>"},
                       {"Style" : "<xsl:value-of select="$r_style"/>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of disable-output-escaping ="yes" select="@line_code"/>"},
                <xsl:for-each select="//cols[@form_no=$f_no and @section_no=$s_no and (count(@col_span)=0 or @col_span='')]">
                    <xsl:variable name="c_no" select="@col_no" />
                    <xsl:choose>
                        <xsl:when test ="count(//vals[@form_no=$f_no and @section_no=$s_no]/r[@line_code=$r_no]/c[@col_no=$c_no])=0">
                            {"Style" : "<xsl:value-of select="$r_style"/>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "-"},
                        </xsl:when>
                        <xsl:when test ="//vals[@form_no=$f_no and @section_no=$s_no]/r[@line_code=$r_no]/c[@col_no=$c_no]/@val=-0.001">
                            {"Style" : "<xsl:value-of select="$r_style"/>","colspan":1, "rowspan":1, "Type" : "S", "Data" : "-"},
                        </xsl:when>
                        <xsl:otherwise>
                            {"Style" : "<xsl:value-of select="$r_style"/>_right","colspan":1, "rowspan":1, "Type" : "N", "Data" : <xsl:value-of select="format-number(string(//vals[@form_no=$f_no and @section_no=$s_no]/r[@line_code=$r_no]/c[@col_no=$c_no]/@val),'###0.00','buh')"/>},
                        </xsl:otherwise>
                    </xsl:choose>
                </xsl:for-each>]},
            </xsl:if>
        </xsl:for-each>

    </xsl:template>
</xsl:stylesheet>

