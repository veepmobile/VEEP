<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user">
	<xsl:import href="common.xsl"/>
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
		<div class='news_text' id='adr_text'><xsl:attribute name='style'>width:<xsl:value-of select='//@ww'/>px; height:<xsl:value-of select='//@wh'/>px;</xsl:attribute>
			<xsl:for-each select="T">
				<table width="98%" cellpadding="0" cellspacing="0">
					<tr>
						<td class="table_caption" style="width:50%">Наименование показателя</td>
						<td class="table_caption" style="width:50%">Содержание (значение) показателя</td>
					</tr>
					<tr>
						<td class="table_shadow" colspan="2">
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
					<tr>
						<td>Вид ценной бумаги</td>
						<td>
							<xsl:value-of select="b/@DR_Vid"/>
						</td>
					</tr>
					<tr bgcolor="#F0F0F0">
						<td>Уровень программы</td>
						<td>
							<xsl:value-of select="b/c/@DR_Level"/>
						</td>
					</tr>
					<tr>
						<td>Категория (тип)</td>
						<td>
							<xsl:value-of select="b/c/@DR_Type"/>
						</td>
					</tr>
					<tr bgcolor="#F0F0F0">
						<td>Вид  депонированных ценных бумаг</td>
						<td>
							<xsl:value-of select="b/c/@DR_Depon"/>
						</td>
					</tr>
					<tr>
						<td>Отношение ДР к акциям</td>
						<td>
							<xsl:value-of select="b/c/@Otnosh"/>
						</td>
					</tr>
					<tr bgcolor="#F0F0F0">
						<td>Тикер</td>
						<td>
							<xsl:value-of select="b/c/a/@Ticker"/>
						</td>
					</tr>
					<tr>
						<td>Код ISIN ценных бумаг, на которые выписана расписка</td>
						<td>
							<xsl:value-of select="b/c/a/@ISIN_RU"/>
						</td>
					</tr>
					<tr bgcolor="#F0F0F0">
						<td>Код ISIN ДР</td>
						<td>
							<xsl:value-of select="b/c/a/@ISIN"/>
						</td>
					</tr>
					<tr>
						<td>Номер CUSIP ДР</td>
						<td>
							<xsl:value-of select="b/c/a/@CUSIP"/>
						</td>
					</tr>
					<tr bgcolor="#F0F0F0">
						<td>Дата допуска к торгам</td>
						<td>
							<xsl:value-of select="b/c/a/@D_Dop"/>
						</td>
					</tr>
					<tr>
						<td>Банк-депозитарий (Эмитент ДР)</td>
						<td>
							<xsl:value-of select="b/c/a/d/@Bank_Depos"/>
						</td>
					</tr>
					<tr bgcolor="#F0F0F0">
						<td>Банк-кастоди (Хранитель)</td>
						<td>
							<xsl:value-of select="b/c/a/d/e/@Bank_custody"/>
						</td>
					</tr>
					<tr>
						<td>Место обращения</td>
						<td>
							<xsl:value-of select="b/c/a/d/e/g/@Trade_place"/>
						</td>
					</tr>
					<tr bgcolor="#F0F0F0">
						<td>Валюта</td>
						<td>
							<xsl:value-of select="b/c/a/@currency"/>
						</td>
					</tr>
				</table>
			</xsl:for-each>
		</div>
	</xsl:template>
</xsl:stylesheet>