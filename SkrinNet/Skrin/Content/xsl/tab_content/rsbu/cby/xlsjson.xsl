<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">
	<msxsl:script language="JScript" implements-prefix="js">
    var IdxCnt = 0;
    function clrCnt() {
      IdxCnt = 0;
      return(0);
    }
    
		function replace(str_node,find,subs)
		{
		    var str = str_node.nextNode().text;
		    var re=new RegExp("&lt;\/?[^&gt;]+(&gt;|$)", "ig")
    str=String(str).replace(re,subs);
    return str
    }

   
    function getColIndex1(cols, pos, r_spn, spn, c_rspn, col_no){


    spn = (spn == "0" ? 1 : spn * 1);
    c_rspn = (c_rspn == "0" ? 1 : c_rspn * 1);
    var col_span = 0;
    var idx = 0;
    for(var i=0; i&lt;cols.Count; i++) {
      cols.MoveNext();
      var n=cols.Current;
      col_span = (n.GetAttribute("colspan","") == "0" ? 1: n.GetAttribute("colspan","")*1);
      if (n.GetAttribute("col_no","") - col_no == 0) {
        break;
      } else {
        idx += col_span;
      }
    }
    if (pos - col_no == 0 &amp;&amp; spn*1 > 1 ){
      return ("Index:" + idx + ", ");
    }
    if (col_no - pos != 0 &amp;&amp; pos == 1 ) {
    return ("Index:" + (col_no - 1) + ", ");
    }
  
    return "";
    }


    function calc_cols(lst){
    var cnt = 0;
    for (var i=0; i&lt;lst.length; i++) {
    var n = lst.nextNode();
    cnt += (n.getAttribute("colspan") == "0" ? 1:n.getAttribute("colspan")*1);
    }
    return(cnt);
    }

    function span_cols(str_node, pos){
    if (pos == 1) return "80";
    if (pos == 2) return "300";
    var str = str_node;
    var ret = "120,";
    if (str != "" &amp;&amp; str*1 > 0 ) {
      for(var i=1; i&lt;str*1; i++)
      {
        ret += "120,";
      }
    }
    return (ret.substr(0, ret.length-1));
    }

    function span_val(str_node){
    var str = str_node;
    return (str=="0" || str=="" ? "1":str);
    }

    function norm_str(str_node) {
    var str = str_node;
    var re=new RegExp("\"", "ig")
    str=String(str).replace(re,"'");
    return str
    }
    function STransform(str_node){
    var str = str_node.nextNode().text;
    var fb=str.substring(0,1).toUpperCase();
    var body=str.substring(1,512).toLowerCase();
    return fb+body;
    }
    function remCRLF(str_node){
    var str = String(str_node.nextNode().text);
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
		aCodesMod[index][1] +="," + aTmp[1];
		}
		}
		for(var i=0; i &lt; aCodesMod.length; i++){
		ret_val += ((i==0)?"":" / ") + aCodesMod[i][0]+((ntypes &gt; 1)? "(" + aCodesMod[i][1] + ")":"");
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
	</msxsl:script>
  <xsl:output method="text" version="4.0" encoding="utf-8"/>
  <xsl:decimal-format name="buh" NaN="" decimal-separator="." grouping-separator=" "/>	
  <xsl:template match="/"><xsl:apply-templates/></xsl:template>
	<xsl:template match="iss_profile">
	  {WorkBook : [<xsl:apply-templates select="profile"></xsl:apply-templates>]}
  </xsl:template>
	<!--   в <xsl:template name="compiler:generated">(XmlQueryRuntime {urn:schemas-microsoft-com:xslt-debug}runtime, XPathNavigator {urn:schemas-microsoft-com:xslt-debug}current, Double col_cnt, IList`1 col_lst, IList`1 s_no, IList`1 frm_no, IList`1 no_nm, IList`1 form_header, Double sc_cnt, IList`1 f_id)-->
  <xsl:template match="profile"  >
      <xsl:for-each select="forms">
            <xsl:variable name="f_id" select="@id" />
            <xsl:variable name="sc_cnt" select="count(//sections[@form_id = $f_id])" />
            <xsl:variable name="form_header" select="js:norm_str(string(@name))" />
            <xsl:variable name="no_nm" select="@no_name" />
            <xsl:variable name="frm_no" select="@form_no"/>
            <xsl:for-each select="//sections[@form_id = $f_id]">
              <xsl:variable name="s_no" select="@no" />
              <xsl:variable name="col_lst" select="//cols[@form_id = $f_id and @section_no = $s_no]" />
              <xsl:variable name="col_cnt" select="count(//cols[@form_id = $f_id and @section_no = $s_no])"/>
              {WorkSheet:"<xsl:if test="$no_nm != ''"><xsl:value-of select="$no_nm"/></xsl:if>
                          <xsl:if test="$no_nm = ''">Форма № <xsl:value-of select="$frm_no"/></xsl:if>
                          <xsl:if test="$sc_cnt > 1">
                            <!--xsl:if test="@no_name != ''"><xsl:value-of select="@no_name"/></xsl:if-->
                            <!--xsl:if test="@no_name = ''">Раздел № <xsl:value-of select="position()"/></xsl:if-->
                            <xsl:text disable-output-escaping="yes"> стр. </xsl:text><xsl:value-of select="position()"/>
                          </xsl:if>",
                  "Cols":[<xsl:for-each select="//cols[@form_id = $f_id and @section_no=$s_no]"><xsl:value-of select="js:span_cols(string(@colspan), position())"/><xsl:if test="position()!=last()">,</xsl:if></xsl:for-each>], "IssName": "<xsl:value-of disable-output-escaping="yes" select="//name/@val"/>",
                  "Style": "Header",
                  "Header" : "Бухгалтерская отчетность по РСБУ по данным банка России за <xsl:value-of select="//per[@sel=1]/@name"/><xsl:if test="@dimantion != ''"><xsl:text>, </xsl:text><xsl:value-of select="@dimension" /></xsl:if>", 
                  "SheetData" : [
                        {Row :[{"Style" : "subcaption","colspan":<xsl:value-of select="$col_cnt"/>, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of disable-output-escaping="yes" select="$form_header"/>"},]},
                         <xsl:if test="$sc_cnt > 1" >
                             <xsl:if test="@name != ''">
                               {Row :[{"Style" : "subcaption","colspan":<xsl:value-of select="$col_cnt"/>, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of disable-output-escaping="yes" select="js:norm_str(string(@name))"/>"},]},
                             </xsl:if>
                             <xsl:if test="@no_name != ''" >
                               {Row :[{"Style" : "subcaption","colspan":<xsl:value-of select="$col_cnt"/>, "rowspan":1, "Type" : "S", "Data" : "<xsl:value-of disable-output-escaping="yes" select="js:norm_str(string(@no_name))"/>"},]},
                             </xsl:if>
                         </xsl:if>
        
                        <xsl:for-each select="//header_rows[@form_id = $f_id and @section_no = $s_no]">
                          <xsl:variable name="hr_no" select="@row_no" />
                          <xsl:variable name="r_spn" select="js:span_val(string(@rowspan))" />
                          <xsl:variable name="hdx_cnt" select="js:clrCnt()"/>
                          {Row :[
                          <xsl:variable name="hed_lst" select="//headers[@form_id = $f_id and @section_no = $s_no and @row_no = $hr_no]"/>
                          <xsl:for-each select="$hed_lst">
                            <xsl:variable name="htd" select="."/>
                                   {"Style" : "TabHeader","colspan":<xsl:value-of select="js:span_val(string(@colspan))"/>, "rowspan":<xsl:value-of select="js:span_val(string(@rowspan))"/>,  <xsl:value-of select="js:getColIndex1($hed_lst, position(), $r_spn, string($htd/@colspan),string($htd/@rowspan),string($htd/@col_no))"/> "Type":"S", "Data" : "<xsl:value-of select="js:norm_str(string(@title))"/>"},
                          </xsl:for-each>
                          ]},
                        </xsl:for-each>
                       <!--{Row :[
                        <xsl:for-each select="//cols[@form_id = $f_id and @section_no=$s_no]">
                           {"Style" : "TabHeader","colspan":1, "rowspan":1, "Type":"S", "Data" : ""},
                             <xsl:if test="@colspan = 2">
                                {"Style" : "TabHeader","colspan":1, "rowspan":1, "Type":"S", "Data" : ""},
                             </xsl:if>
                        </xsl:for-each>
                        ]},-->
                        <xsl:if test="//fn/@val != 2 and //currency[@id=1]/@sel = 0">
                          {Row :[{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : ""},{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : "Курс <xsl:value-of select="//kurs_header/@val"/>"},<xsl:for-each select="//cols[@form_id = $f_id and @section_no = $s_no]">{"Style" : "even","colspan":<xsl:value-of select="js:span_val(string(@colspan))"/>, "rowspan":1, Type:"N", "Data" : <xsl:value-of select="@kurs"/>},</xsl:for-each>]},
                          {Row :[{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : ""},{"Style" : "even","colspan":1, "rowspan":1, Type:"S", "Data" : "Курс на дату"},<xsl:for-each select="//cols[@form_id = $f_id and @section_no = $s_no]">{"Style" : "even_date","colspan":<xsl:value-of select="js:span_val(string(@colspan))"/>, "rowspan":1, Type:"D", "Data" : "<xsl:value-of select="@ld"/>"},</xsl:for-each>]},
                        </xsl:if>

              <xsl:for-each select="//rgrp[@form_id = $f_id and @section_no=$s_no]">
                <xsl:variable name="g_no" select="@no" />
                <xsl:if test="@name!=''">
                  {Row :[{"Style" : "even","colspan":<xsl:value-of select="$col_cnt"/>, "rowspan":1, Index:0, Type:"S", "Data" : "<xsl:value-of select="@name"/>"},]},
                </xsl:if>
                <xsl:for-each select="//trs[@form_id = $f_id and @section_no=$s_no and @group_no = $g_no]">
                  <xsl:variable name="r_no" select="@row_no" />
                  <xsl:variable name="tdr_spn" select="js:span_val(string(@rowspan))" />
                  <xsl:variable name="stl">
                    <xsl:choose>
                      <xsl:when test="ceiling(position() div 2)- position() div 2 = 0">odd</xsl:when>
                      <xsl:otherwise>even</xsl:otherwise>
                    </xsl:choose>
                  </xsl:variable>
                  <xsl:variable name="idx_cnt" select="js:clrCnt()"/>
                  {Row :[
                  <xsl:for-each select="//tds[@form_id = $f_id and @section_no=$s_no and @group_no = $g_no and @row_no = $r_no]">
                    <xsl:variable name="td" select="."/>
                    <xsl:variable name="TYP">
                      <xsl:choose>
                         <xsl:when test="@val_type = 0 and @value != ''">N</xsl:when>
                         <xsl:otherwise>S</xsl:otherwise>
                      </xsl:choose>
                    </xsl:variable>
                    <xsl:variable name="VAL">
                      <xsl:choose>
                        <xsl:when test="@value = '' and @val_type = 0">-</xsl:when>
                        <xsl:when test="$TYP = 'N'"><xsl:value-of select="format-number(@value,'###0.00','buh')"/></xsl:when>
                        <xsl:otherwise><xsl:value-of select="js:norm_str(string(@value))"/></xsl:otherwise>
                      </xsl:choose>
                    </xsl:variable>
                    <xsl:variable name="vstl">
                      <xsl:choose>
                        <xsl:when test="$TYP = 'N'"><xsl:value-of select="$stl"/>_right</xsl:when>
                        <xsl:otherwise><xsl:value-of select="$stl"/></xsl:otherwise>
                      </xsl:choose>
                    </xsl:variable>
                    {"Style" : "<xsl:value-of select="$vstl"/>","colspan":<xsl:value-of select="js:span_val(string(@colspan))"/>, "rowspan":<xsl:value-of select="js:span_val(string(@rowspan))"/>, <xsl:value-of select="js:getColIndex1($col_lst, position(), $tdr_spn, string($td/@colspan), string($td/@rowspan), string($td/@col_no))"/>  
                    Type:"<xsl:value-of select="$TYP"/>", "Data" : <xsl:if test="$TYP='S'">"'</xsl:if><xsl:value-of select="$VAL"/><xsl:if test="$TYP='S'">"</xsl:if>},
                  </xsl:for-each>
                  ]},
                </xsl:for-each>
              </xsl:for-each>

              ]},
            </xsl:for-each>
		</xsl:for-each>
	</xsl:template>

</xsl:stylesheet>

