<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user">
	<xsl:import href="../../../xsl/common.xsl"/>
	<xsl:output method="html" version="4.0" encoding="utf-8"/>
	<xsl:decimal-format name="buh" decimal-separator="," grouping-separator=" " NaN="0"/>

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
		<div id="regs">
			<input type="hidden" id="iss"><xsl:attribute name="value"><xsl:value-of select="//@iss"/></xsl:attribute></input>
			<div class="data_comment" >
				Выписка из ЕГРЮЛ от <xsl:value-of select="//name/@dt"/>
			</div>
			<span class="subcaption">СВЕДЕНИЯ О ДЕРЖАТЕЛЕ РЕЕСТРА АКЦИОНЕРОВ АО</span>
			
			<br/>
			<table width="100%">
				
				<tr>
						<td class="table_caption"  style="width:40%">Наименование</td>
						<td class="table_caption" style="width:20%">ОГРН</td>
						<td class="table_caption" style="width:20%">Дата внесения записи в ЕГРЮЛ</td>
					<td class="table_caption" style="width:20%">ГРН</td>
					</tr>
					<tr>
						<td class="table_shadow">
							<div style="width: 1px; height: 1px;">
								<spacer type="block" width="1px" height="1px" />
							</div>
						</td>
						<td class="table_shadow">
							<div style="width: 1px; height: 1px;">
								<spacer type="block" width="1px" height="1px" />
							</div>
						</td>
						<td class="table_shadow">
							<div style="width: 1px; height: 1px;">
								<spacer type="block" width="1px" height="1px" />
							</div>
						</td>
						<td class="table_shadow">
							<div style="width: 1px; height: 1px;">
								<spacer type="block" width="1px" height="1px" />
							</div>
						</td>
					</tr>
				<xsl:for-each select="rh">
					<tr>
						<td valign="top">
							<xsl:value-of select="@SH_Name"/>
						</td>
						<td valign="top" style="text-align:center;">
							<xsl:value-of select="@SH_Ogrn"/>
						</td>
						<td valign="top" style="text-align:center;">
							<xsl:value-of select="@EgrulDate"/>
						</td>
						<td valign="top" style="text-align:center;">
							<xsl:value-of select="@Grn"/>
						</td>

					</tr>
				</xsl:for-each>
			</table>
			<span class="data_comment limitation">
				ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и могут не отражать абсолютную актуальность и достоверность информации. Дополнительную информацию можно получить в разделе
				<a onclick="setclass(42,11);tab_def=0;" href="#">«Квартальные отчеты»</a> или <a onclick="setclass(19,6);tab_def=20;" href="#">«Существенные факты»</a>
			</span>
		</div>
		<script language="javascript" type="text/javascript">
			<![CDATA[ 
			
			xls_params={"iss":$("#iss").val(),"module" : "registr/egrul/","x":Math.random()}
	
			
			]]>
		</script>
	</xsl:template>
</xsl:stylesheet>
