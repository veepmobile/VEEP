<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="urn:deitel:user" xmlns:js="javascript:code">
<msxsl:script language="JScript" implements-prefix="js">
function GenPages(oNodeList) 
{
	var oNode = oNodeList.nextNode();
		var PCount = oNode.getAttribute("PC");
		var Page = oNode.getAttribute("PG");
		var URL = oNode.getAttribute("URL");
var html = "";
var StartPage 
if (PCount &lt; 8)
	StartPage = 1
else	
	StartPage = ((Page-3 &gt; 0)? ((PCount-Page &lt; 3)? PCount*1 + (PCount-Page)-8 : Page-3):1);
	
if (Page*1 &gt; 3 &amp;&amp; PCount &gt; 7)
	html+='&lt;td&gt;&lt;a href="' + URL + '&amp;PG=' + (StartPage-1) + '"&gt;&lt;&lt;&lt;/a&gt;&lt;/td&gt;';
for (var i = StartPage;  i &lt; ((Page == PCount)? PCount*1+1 : ((StartPage+7 &lt; PCount)? StartPage+7: PCount*1+1)); i++)
{
if (i==Page)
		html+='&lt;td&gt;'+ i + '-я страница&lt;/td&gt;';
	else
		html+='&lt;td&gt;&lt;a href="' + URL + '&amp;PG=' + i + '"&gt;'+ i + '&lt;/a&gt;&lt;/td&gt;';
}	
if (i &lt; PCount)
	html+='&lt;td&gt;&lt;a href="' + URL + '&amp;PG=' + (Page*1+4) + '"&gt;&gt;&gt;&lt;/a&gt;&lt;/td&gt;';	
if (PCount &gt; 7)
	html+='&lt;td&gt;(Всего: &lt;a href="javascript:MoveTo(\'' + URL + '\',' + Page + ',' + PCount+')"&gt;' + PCount + ' страниц&lt;/a&gt;)&lt;/td&gt;';	
	
return html
}
function url(nl)
{
var str = nl.nextNode().text;
var rus = new Array('а','б','в','г','д','е','ё','ж','з','и','й','к','л','м','н','о','п','р','с','т','у','ф','х','ц','ч','ш','щ','ь','ы','ъ','э','ю','я');
var unic = new Array('%E0','%E1','%E2','%E3','%E4','%E5','%B8','%E6','%E7','%E8','%E9','%EA','%EB','%EC','%ED','%EE','%EF','%F0','%F1','%F2','%F3','%F4','%F5','%F6','%F7','%F8','%F9','%FC','%FB','%FA','%FD','%FE','%FF');

for (var i = 0; i &lt; rus.length; i++)
{	re = new RegExp(rus[i], "g");
	str = str.replace(re,unic[i]);
}
	return str
}
</msxsl:script>						
<xsl:output method="html" version="4.0" encoding="utf-8"/>
<xsl:template match="/">
	<xsl:apply-templates/>
</xsl:template>
<xsl:template match="iss_profile">

<!-- content -->
				
					<xsl:apply-templates select="profile">
					</xsl:apply-templates>
				
   				
<!-- end content -->
</xsl:template>
<xsl:template match="profile">
<xsl:choose>
<xsl:when test="CN/@cnt &gt; 0">
	<img src="/images/mnu_bullet_10.gif" width="14" height="11" border="0" align="absmiddle"/><font class="minicaption">
	Всего найдено: <xsl:value-of select="CN/@cnt"/> совпадений. 
	</font>
	<br/><br/>
	<table cellspacing="1"><tr>
		<xsl:value-of disable-output-escaping="yes" select="js:GenPages(PC)"/>
	</tr></table>
</xsl:when>
<xsl:otherwise>
	<xsl:if test="string-length(CN/@cnt)!=0">
	<img src="/images/icon_error.gif" width="16" height="16" border="0"/><font class="error">Нет данных соответствующих заданному условию</font> 
	</xsl:if>
</xsl:otherwise>
</xsl:choose>
<br/><br/>
<table width="100%" cellpadding="0" cellspacing="0">
<xsl:for-each select="UT">
	<xsl:if test="position()=1">
		<tr>
		<td class="table_caption_left">Текст</td>
		</tr>
		<tr>
		<td class="table_shadow_left"><img src="/images/null.gif" width="1" height="1" border="0"/></td>
		</tr>
		
	</xsl:if>
	<tr>
	
	<td>
	<xsl:choose>
		<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">
			<xsl:attribute name="bgColor">#F0F0F0</xsl:attribute>
		</xsl:when>
		<xsl:otherwise>
			<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
		</xsl:otherwise>
	</xsl:choose>
	<table cellspacing="0" cellpadding="2" border="0" width="100%">
	<xsl:choose>
		<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">
			<xsl:attribute name="bgColor">#F0F0F0</xsl:attribute>
		</xsl:when>
		<xsl:otherwise>
			<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
		</xsl:otherwise>
	</xsl:choose>
	<tr>
	<xsl:choose>
		<xsl:when test="@cat=99">
			<td  class="data_comment" align="right" width="1%">Документ:</td>
			<td  align="left" width="99%">
				<a target="_blank">
				<xsl:attribute name="href">
					<xsl:value-of select="js:url(@menuhref)"/>
				</xsl:attribute>	
				<xsl:choose>
					<xsl:when test="B/@ext='doc' or @ext='rtf'">
						<img src="/images/icon_docword_16.gif" width="16" height="16" border="0" alt="Документ Word" align="absmiddle"/>
					</xsl:when>
					<xsl:when test="B/@ext='xls'">
						<img src="/images/icon_docexcel_16.gif" width="16" height="16" border="0" alt="Документ Excel" align="absmiddle"/>
					</xsl:when>
					<xsl:when test="B/@ext='pdf'">
						<img src="/images/icon_docpdf_16.gif" width="16" height="16" border="0" alt="Документ PDF" align="absmiddle"/>
					</xsl:when>
					<xsl:when test="B/@ext='htm' or @ext='html'">
						<img src="/images/icon_dochtm_16.gif" width="16" height="16" border="0" alt="Документ HTML" align="absmiddle"/>
					</xsl:when>
					<xsl:when test="B/@ext='txt'">
						<img src="/images/icon_doctxt_16.gif" width="16" height="16" border="0" alt="Текстовый документ" align="absmiddle"/>
					</xsl:when>
					<xsl:when test="B/@ext='smm' or @ext='smml'">
						<img src="/images/icon_docsmml_16.gif" width="16" height="16" border="0" alt="Документ SMML" align="absmiddle"/>
					</xsl:when>
					<xsl:otherwise>
						<img src="/images/icon_docunknown_16.gif" width="16" height="16" border="0" alt="Документ" align="absmiddle"/>
					</xsl:otherwise>
				</xsl:choose>
				<xsl:value-of select="@headline"/>
			</a></td>
		</xsl:when>
		<xsl:otherwise>
			<td  class="data_comment" align="right" width="1%">Эмитент:</td>
			<td  align="left" width="99%"><a href="#">
				<xsl:attribute name="onclick">
						openIssuerMenu('<xsl:value-of select="@menuhref"/>','<xsl:value-of select="@rts_code"/>');

				</xsl:attribute>	
				<xsl:value-of select="@name"/>
			</a></td>
		</xsl:otherwise>
	</xsl:choose>
	
	
	</tr>
	<tr>
	
	<xsl:choose>
		<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">
			<xsl:attribute name="bgColor">#F0F0F0</xsl:attribute>
		</xsl:when>
		<xsl:otherwise>
			<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
		</xsl:otherwise>
	</xsl:choose>
	<xsl:choose>
		<xsl:when test="@cat!=99">
			<td  class="data_comment" align="right" width="1%">Раздел:</td>
			<xsl:choose>
				<xsl:when test="string-length(@headline)=0">
					<td  class="data_comment" align="left" width="99%"><xsl:value-of select="B/@Menu_Name"/></td>
				</xsl:when>
				<xsl:otherwise>
					<td  class="data_comment" align="left" width="99%"><xsl:value-of select="@headline"/></td>
				</xsl:otherwise>	
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>
			<td  class="data_comment" align="right" width="1%">Дата:</td>
			<td  class="data_comment" align="left" width="99%"><xsl:value-of select="@dt"/></td>
		</xsl:otherwise>
	</xsl:choose>
	
	</tr>		
	</table>
	</td></tr>
</xsl:for-each>
</table>
<br/><br/>
<xsl:if test="CN/@cnt &gt; 0">
<table cellspacing="1"><tr>
		<xsl:value-of disable-output-escaping="yes" select="js:GenPages(PC)"/>
	</tr></table>
</xsl:if>	
	
<script language="javascript" type="text/javascript"><![CDATA[ 
				<!--
				function MoveTo(url,page,pcount)
				{
				var sel_page = 0;
				sel_page = window.prompt('Укажите номер страницы на которую вы хотите перейти. Число должно быть в диапазоне от 1 до ' + pcount, page);
				if((sel_page >= 1) && (sel_page <= pcount)){
					window.location.href=url + '&PG='+sel_page;
				}else{
					window.location.href=url+'&PG=1';
				}
				}
				//-->
				]]></script>
<script language="javascript" type="text/javascript"><![CDATA[ 
				<!--
				function wopen(sstring)
				{
				window.opener.location.href=sstring;
				self.focus();
				}
				//-->
				]]></script>				
</xsl:template>
</xsl:stylesheet>
