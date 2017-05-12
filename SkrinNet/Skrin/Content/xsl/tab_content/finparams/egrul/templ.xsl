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
			<xsl:if test="pa">
			<span class="subcaption">СВЕДЕНИЯ О СТОИМОСТИ ЧИСТЫХ АКТИВОВ AO</span>
			
			<br/>
			<table width="100%">
				
				<tr>
						<td class="table_caption"  style="width:40%">Стоимость чистых активов общества</td>
						<td class="table_caption" style="width:20%">Дата окончания отчетного периода, за который представлены сведения</td>
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
				<xsl:for-each select="pa">
					<tr>
						<td valign="top" style="text-align:center;">
							<xsl:value-of select="format-number(@CostString,'# ###,00','buh')"/>
						</td>
						<td valign="top" style="text-align:center;">
							<xsl:value-of select="@DE"/>
						</td>
						<td valign="top" style="text-align:center;">
							<xsl:value-of select="@grn_date"/>
						</td>
						<td valign="top" style="text-align:center;">
							<xsl:value-of select="@grn_no"/>
						</td>

					</tr>
				</xsl:for-each>
			</table>
				</xsl:if>
				<span class="data_comment limitation">
					ВНИМАНИЕ: в связи с особенностями функционирования и обновления указанного источника информации АО "СКРИН" не может гарантировать абсолютную актуальность и достоверность данных.
				</span>
			</div>
			<script language="javascript" type="text/javascript">
			<![CDATA[ 
			
			xls_params={"iss":$("#iss").val(),"module" : "finparams/egrul/","x":Math.random()}
	
			
			]]>
		</script>
	</xsl:template>
</xsl:stylesheet>
