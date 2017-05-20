<?xml version="1.0" encoding="windows-1251" standalone="no" ?>
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
	<xsl:output method="html" version="4.0" encoding="WINDOWS-1251"/>
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
				<img src="/images/mnu_bullet_10.gif" width="14" height="11" border="0" align="absmiddle"/>
				<font class="minicaption">
					Всего найдено: <xsl:value-of select="CN/@cnt"/> совпадений.
				</font>
				<br/>
				<br/>
				<table cellspacing="1">
					<tr>
						<xsl:value-of disable-output-escaping="yes" select="js:GenPages(PC)"/>
					</tr>
				</table>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="string-length(CN/@cnt)!=0">
					<img src="/images/icon_error.gif" width="16" height="16" border="0"/>
					<font class="error">Нет данных соответствующих заданному условию</font>
				</xsl:if>
			</xsl:otherwise>
		</xsl:choose>
		<br/>
		<br/>
		<table width="100%" cellpadding="0" cellspacing="0">
			<xsl:for-each select="UT">
				<xsl:if test="position()=1">
					<tr>
						<td class="table_caption_left">Текст</td>
					</tr>
					<tr>
						<td class="table_shadow_left">
							<img src="/images/null.gif" width="1" height="1" border="0"/>
						</td>
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
								<td  class="data_comment" align="right" width="1%">Предприятие:</td>
										<td  align="left" width="99%">
											<a href="#">
												<xsl:attribute name="onclick">
													javascript:openIssuerMenu('<xsl:value-of select="@menuhref"/>','<xsl:value-of select="@rts_code"/>');
												</xsl:attribute>
												<xsl:value-of select="@name"/>
											</a>
										</td>

							</tr>
							
						</table>
					</td>
				</tr>
			</xsl:for-each>
		</table>
		<br/>
		<br/>
		<xsl:if test="CN/@cnt &gt; 0">
			<table cellspacing="1">
				<tr>
					<xsl:value-of disable-output-escaping="yes" select="js:GenPages(PC)"/>
				</tr>
			</table>
		</xsl:if>

		<script language="javascript" type="text/javascript">
			<![CDATA[ 
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
				]]>
		</script>
		<script language="javascript" type="text/javascript">
			<![CDATA[ 
				<!--
				function wopen(sstring,ticker)
				{
				var width="1024", height="900";
				var left = (screen.width/2) - width/2;
				var top = (screen.height/2) - height/2;
				var styleStr = 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no,width='+width+',height='+height+',left='+left+',top='+top+',screenX='+left+',screenY='+top;
				window.open(sstring, ticker, styleStr);
				}
				//-->
				]]>
		</script>
	</xsl:template>
</xsl:stylesheet>
