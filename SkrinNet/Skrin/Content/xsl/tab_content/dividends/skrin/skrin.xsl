<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">


	<xsl:output method="html" version="4.0" encoding="utf-8"/>

	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:decimal-format name="buh" decimal-separator="," grouping-separator=" " NaN=""/>
	<xsl:decimal-format name="buh1" decimal-separator="," grouping-separator=" " NaN=""/>
	<xsl:decimal-format name="coef" decimal-separator="," grouping-separator=" " NaN=" "/>
	<xsl:template match="iss_profile">

		<!-- content -->

		<xsl:apply-templates select="profile">
		</xsl:apply-templates>


		<!-- end content -->
	</xsl:template>
	<xsl:template match="profile">
		<xsl:if test="A[position()=last()]">
			<span class="subcaption">
			Сведения об объявленных (начисленных) дивидендах по акциям</span>
			<br/>
      <input type="hidden" id="iss"><xsl:attribute name="value"><xsl:value-of select="//@iss"/></xsl:attribute></input>
      <input type="hidden" id="tab_id">
        <xsl:attribute name="value"><xsl:value-of select="//@id"/></xsl:attribute>
      </input>
			<table width="100%" cellpadding="0" cellspacing="0">
				<xsl:if test="../@isAct=0">
					<xsl:attribute name="border">1</xsl:attribute>
				</xsl:if>
				<xsl:for-each select="A">
					<xsl:if test="position()=1">
						<tr>
							<td class="table_caption" rowspan="2">Тип акции</td>
							<td class="table_caption" rowspan="2">Дивидендный период</td>
							<td class="table_caption" colspan="2">Дата закрытия реестра</td>
							<td class="table_caption" colspan="2">Размер див. на 1 акцию, руб</td>
							<td class="table_caption" colspan="2">Общая сумма начисл., руб</td>
						</tr>
						<tr>
							
							<td class="table_caption">Предварительная</td>
							<td class="table_caption">Утвержденная</td>
							<td class="table_caption">Предварительный</td>
							<td class="table_caption">Утвержденный</td>
							<td class="table_caption">Предварительная</td>
							<td class="table_caption">Утвержденная</td>
						</tr>
						<xsl:if test="../../@isAct=1">
							<tr>
								<td class="table_shadow_left">
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
								<td class="table_shadow_left">
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
								<td class="table_shadow_left">
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
								<td class="table_shadow_left">
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
								<td class="table_shadow_left">
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
								<td class="table_shadow_left">
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
								<td class="table_shadow_left">
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
								<td class="table_shadow_left">
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
							</tr>
						</xsl:if>
					</xsl:if>
					<tr>
						<xsl:if test="../../@isAct=1">
							<xsl:choose>
								<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">
									<xsl:attribute name="bgColor">#F0F0F0</xsl:attribute>
								</xsl:when>
								<xsl:otherwise>
									<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
								</xsl:otherwise>
							</xsl:choose>
						</xsl:if>
						<td  class="table_item_left" align="left">
							<xsl:value-of select="@sec_type"/>
						</td>
						<td  class="table_item" align="center">
							<xsl:value-of select="@s_date"/> г.
						</td>
						<td  class="table_item" align="center">
							<xsl:value-of select="@pl_date"/>
						</td>
						<td  class="table_item" align="center">
							<xsl:value-of select="@l_date"/>
						</td>
						<td  class="table_item" align="right">
							<xsl:value-of select="@pvalue"/>
						</td>
						<td  class="table_item" align="right">
							<xsl:value-of select="@value"/>
						</td>
						<td  class="table_item" align="right">
							<xsl:value-of select="format-number(@ptotal,'# ##0,00','buh')"/>
						</td>
						<td  class="table_item" align="right">
							<xsl:value-of select="format-number(@total,'# ##0,00','buh')"/>
						</td>
					</tr>
				</xsl:for-each>
				
			</table>
			<span class="data_comment limitation">
				ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и могут не отражать абсолютную актуальность и достоверность информации. Дополнительную информацию можно получить в разделе
				<a onclick="gotomenu(48);" href="#">«Квартальные отчеты»</a> или <a onclick="gotomenu(32);" href="#">«Существенные факты»</a>
			</span>
		</xsl:if>
		<script language="javascript" type="text/javascript">
			<![CDATA[ 
      $('#xls_btn').unbind('click').addClass('disabled');
        set_xls_function(function () {
           if (!getObj("reports")) {
               
                ifr = document.createElement("iframe");
                ifr.className = "service_frame"
                ifr.name = "reports"
                ifr.id = "reports"
                ifr.src = "about:blank";
                document.body.appendChild(ifr);
            } else {
                ifr = getObj("reports")
            }
            		
            
            iframepost({"id": $("#tab_id").val(), "ticker":$("#iss").val(), "xls" : "1"}, "/Tab/", "reports");
        });			]]>
	</script>
	</xsl:template>
</xsl:stylesheet>
