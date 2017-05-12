<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">

	<xsl:import href="common.xsl"/>
	<msxsl:script language="JScript" implements-prefix="js">
		function GetMult(nl){
		var val=String(nl.nextNode().text);
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
	</msxsl:script>
	<xsl:output method="html" version="4.0" encoding="utf-8"/>
	<xsl:decimal-format name="buh" decimal-separator="." grouping-separator=" " NaN="#"/>
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
		<span class='subcaption'>
			<xsl:value-of select='//@iss_name'/>
		</span>
		<xsl:for-each select="DKK">

			<table width="100%" border="0">
				<tr>
					<td colspan="2">
						<b>
							Государственный регистрационный номер   <xsl:value-of select="@Reg_Num"/>
						</b>
					</td>
				</tr>
				<tr>
						<td valign="top" width="60%">
							<b>Выпуск, общие сведения</b>
							<table width="100%">
								<tr>
									<td class="table_caption" style="width:50%">Наименование показателя</td>
									<td class="table_caption" style="width:50%">Содержание (значение) показателя</td>
								</tr>
								<tr>
									<td class="table_shadow" >
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
										<xsl:value-of select="@a_type"/>
									</td>
								</tr>
								<tr bgcolor="#F0F0F0">
									<td>Категория (тип), серия, форма</td>
									<td>
										<xsl:value-of select="@a_type"/>
										<xsl:value-of select="@Form"/>
									</td>

								</tr>
								<tr>
									<td>Номер серии</td>
									<td>
										<xsl:value-of select="@Issue_Num"/>
									</td>
								</tr>
								<tr bgcolor="#F0F0F0">
									<td>Дата погашения</td>
									<td>
										<xsl:value-of select="@pogash"/>

									</td>

								</tr>
								<tr>
									<td>
										Номинальная стоимость каждой ценной бумаги выпуска<xsl:value-of select="concat(', ', @Currency)"/>
									</td>
									<td style="text-align:right;">
										<xsl:value-of select="format-number(js:GetMult(@Nominal), '# ##0.00##############', 'buh')"/>
									</td>
								</tr>
								<!--tr bgcolor="#F0F0F0">
							<td>Валюта номинала</td>
							<td>
								<xsl:value-of select="@Currency"/>
							</td>
						</tr-->
								<tr>
									<td>Текущее состояние ценных бумаг выпуска</td>
									<td>
										<xsl:value-of select="@Status"/>
									</td>
								</tr>
								<tr>
									<td colspan="2">
										<hr/>
										<b>Сведения о размещении и иные сведения</b>
									</td>
								</tr>

								<tr>
									<td>Дата государственной регистрации</td>
									<td>
										<xsl:value-of select="@RD"/>
									</td>
								</tr>
								<tr  bgcolor="#F0F0F0">
									<td>Способ размещения</td>
									<td>
										<xsl:value-of select="@Floatation_name"/>
									</td>
								</tr>

								<tr >
									<td>Количество размещенных ценных бумаг, шт.</td>
									<td style="text-align:right;">

										<xsl:value-of select="format-number(@Quantity,'# ##0.################','buh')"/>

									</td>
								</tr>
								<tr bgcolor="#F0F0F0">
									<td>
										Размещённый  объём<xsl:value-of select="concat(', ', @Currency)"/>
									</td>
									<td style="text-align:right;">
										<xsl:value-of select="format-number(@volume,'# ##0.################','buh')"/>

									</td>
								</tr>
								<tr>
									<td>Дата начала размещения</td>
									<td>
										<xsl:value-of select="@FDS"/>
									</td>
								</tr>
								<tr  bgcolor="#F0F0F0">
									<td>Дата окончания размещения</td>
									<td>
										<xsl:value-of select="@FDE"/>
									</td>
								</tr>
								<tr>
									<td>
										Дата регистрации отчета или дата представления уведомления об итогах
									</td>
									<td>
										<xsl:value-of select="@FDR"/>
									</td>
								</tr>


							</table>
						</td>
					</tr>
				</table>
				<hr/>
						
			</xsl:for-each>
		<!--/div-->
	</xsl:template>
	<xsl:template name="calc_fraction">
		<xsl:param name="price" select="."/>
		<xsl:param name="amount" select=".."/>

		<xsl:choose>
			<xsl:when test="contains($amount,'/')">
				<xsl:value-of select="format-number((substring-before($amount,' ')*1+ substring-before(substring-after($amount,' '),'/') div substring-after($amount,'/')) * $price,'# ##0.00','buh')"/>
				<!--xsl:value-of select="$amount"/-->
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="format-number($amount*$price,'# ##0.00','buh')"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>
<!--doc doc_id="426F7D2620B14E3C9EFA2BEBACE19319" doc_name="Решение о выпуске" file_name="32583.pdf" size="213.59 КБ"/-->