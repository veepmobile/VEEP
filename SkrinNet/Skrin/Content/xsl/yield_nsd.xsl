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
	<xsl:decimal-format name="buh" decimal-separator="." grouping-separator=" " NaN="-"/>
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
		<span class="issuercaption">
			<xsl:value-of select="//@iss_name"/>
		</span>
		<br/>
		<br/>
		<span class="subcaption">Выпуск облигаций</span>
		<br/>


		<table width="100%" cellpadding="0" cellspacing="0" border="0">
			<thead>
				<tr>
					<td class="table_caption">Дата государственной регистрации</td>
					<td class="table_caption">Государственный регистрационный номер</td>
					<td class="table_caption">Вид ценной бумаги</td>
					<td class="table_caption">Номинальная стоимость каждой ценной бумаги выпуска</td>
					<td class="table_caption">Количество ценных бумаг, подлежавших размещению, шт.</td>
					<td class="table_caption">Количество в обращении ценных бумаг, шт.</td>
					<td class="table_caption">Способ размещения</td>
					<td class="table_caption">Состояние ценных бумаг выпуска</td>
					<td class="table_caption">Расчетная дата погашения</td>
					<td class="table_caption">Период обращения</td>
					<td class="table_caption">Кол-во купонов</td>

				</tr>
			</thead>
			<tbody>
				<tr>
					<td class="table_shadow">
						<div style="width: 1px; height: 1px;">
							<spacer type="block" width="1px" height="1px" />
						</div>
					</td>
					<td class="table_shadow" colspan="2">
						<div style="width: 1px; height: 1px;">
							<spacer type="block" width="1px" height="1px" />
						</div>
					</td>
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



				<xsl:for-each select="issues">
					<tr>
						<xsl:attribute name="bgcolor">
							<xsl:call-template name="set_bg">
								<xsl:with-param name="str_num" select="position()+1"/>
							</xsl:call-template>
						</xsl:attribute>
						<td align="center" valign="top">
							<xsl:value-of select="@rd"/>
						</td>
						<td align="center" valign="top">

							<xsl:value-of select="@reg_no"/>
						</td>
						<td align="center" valign="top">
							<xsl:value-of select="@stype"/>
						</td>
						<td align="right" valign="top">
							<xsl:value-of select="format-number(js:GetMult(@face_value),'# ##0.###########################','buh')"/> <!--руб.-->
							&#160;<xsl:value-of select="@curr_name"/>
						</td>
						<td valign="top"  class="numbers" align="right">
							<xsl:choose>
								<xsl:when test="contains(@shares_declared,'/')">
									<table  cellpadding="0" cellspacing="0" border="0"  align="right">
										<tr>
											<td rowspan="2" class="numbers" style="padding-right:3px;">
												<xsl:value-of select="format-number(substring-before(@shares_declared,' '),'# ##0','buh')"/>
											</td>
											<td class="numbers">
												<xsl:value-of select="format-number(substring-before(substring-after(@shares_declared,' '),'/'),'# ##0','buh')"/>
											</td>
										</tr>
										<tr>
											<td style="border-top:solid 1px #000000" class="numbers">
												<xsl:value-of select="format-number(substring-after(@shares_declared,'/'),'# ##0','buh')"/>
											</td>
										</tr>
									</table>
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="format-number(@shares_declared,'# ##0.################','buh')"/>
								</xsl:otherwise>
							</xsl:choose>
						</td>
						<td valign="top"  class="numbers" align="right">
							<xsl:choose>
								<xsl:when test="contains(@shares_rolling,'/')">
									<table cellpadding="0" cellspacing="0" border="0" align="right">
										<tr>
											<td rowspan="2" class="numbers" style="padding-right:3px;">
												<xsl:value-of select="format-number(substring-before(@shares_rolling,' '),'# ##0','buh')"/>
											</td>
											<td class="numbers">
												<xsl:value-of select="format-number(substring-before(substring-after(@shares_rolling,' '),'/'),'# ##0','buh')"/>
											</td>
										</tr>
										<tr>
											<td style="border-top:solid 1px #000000" class="numbers">
												<xsl:value-of select="format-number(substring-after(@shares_rolling,'/'),'# ##0','buh')"/>
											</td>
										</tr>
									</table>
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="format-number(@shares_rolling,'# ##0.################','buh')"/>
								</xsl:otherwise>
							</xsl:choose>
						</td>
						<td align="center" valign="top" >
							<xsl:value-of select="@pt_name"/>
						</td>
						<td align="center" valign="top">
							<xsl:value-of select="@ps_name"/>
						</td>
						<td align="center" valign="top">
							<xsl:value-of select="@red"/>
						</td>
						<td align="center" valign="top">
							<xsl:value-of select="@redemption_end_period"/>
						</td>
						<td align="center" valign="top">
							<xsl:choose>
								<xsl:when test="@coupons=0">
									-
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="@coupons"/>
								</xsl:otherwise>
							</xsl:choose>

						</td>
					</tr>
				</xsl:for-each>

			</tbody>
		</table>
		<xsl:if test="issue_coupons">
			<br/>
			<span class="subcaption">Выплаты по Купонам</span>
			<br/>


			<table width="100%" cellpadding="0" cellspacing="0" border="0">

				<thead>
					<tr>
						<td class="table_caption">№  купона</td>
						<td class="table_caption">Расчетная Дата окончания купонного периода</td>
						<td class="table_caption">Плановая  Дата окончания купонного периода</td>
						<td class="table_caption">Ставка купона в % годовых</td>
						<td class="table_caption">Размер выплаты купонного дохода на 1 Ц.Б. (в валюте платежа)</td>
						<td class="table_caption">Фактическая дата выплаты</td>
					</tr>
				</thead>
				<tbody>
					<tr>
						<td class="table_shadow">
							<div style="width: 1px; height: 1px;">
								<spacer type="block" width="1px" height="1px" />
							</div>
						</td>
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
					<xsl:for-each select="issue_coupons">
						<tr>
							<xsl:attribute name="bgcolor">
								<xsl:call-template name="set_bg">
									<xsl:with-param name="str_num" select="position()+1"/>
								</xsl:call-template>
							</xsl:attribute>
							<td style="text-align:center;">
								<xsl:value-of select="@no"/>
							</td>
							<td style="text-align:center;">
								<xsl:value-of select="@d_date"/>
							</td>
							<td style="text-align:center;">
								<xsl:value-of select="@p_date"/>
							</td>
							<td style="text-align:right">
								<xsl:value-of select="format-number(@rate,'# ##0.###########################','buh')"/>
							</td>
							<td  style="text-align:right">
								<xsl:value-of select="format-number(@sum_value,'# ##0.###########################','buh')"/>
							</td>
							<td style="text-align:center">
								<xsl:value-of select="@sd"/>
							</td>
						</tr>
					</xsl:for-each>
				</tbody>
			</table>
		</xsl:if>
		<xsl:if test="issue_repayments">
			<br/>
			<span class="subcaption">Погашение номинальной стоимости (амортизация)</span>
			<br/>

			<table width="100%" cellpadding="0" cellspacing="0" border="0">

				<thead>
					<tr>
						<td class="table_caption">№</td>
						<td class="table_caption">Расчетная Дата  погашения</td>
						<td class="table_caption">Плановая  Дата  погашения</td>

						<td class="table_caption">Доля в % погашения части номинальной стоимости</td>
						<td class="table_caption">Размер погашаемой части</td>
						<td class="table_caption">Фактическая дата погашения</td>
						<td class="table_caption">Примечание</td>
						

					</tr>
				</thead>
				<tbody>
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
					<xsl:for-each select="issue_repayments">
						<tr>
							<xsl:attribute name="bgcolor">
								<xsl:call-template name="set_bg">
									<xsl:with-param name="str_num" select="position()+1"/>
								</xsl:call-template>
							</xsl:attribute>
							<td>
								<xsl:value-of select="@no"/>
							</td>
							<td style="text-align:center">
								<xsl:value-of select="@d_date"/>
								
							</td>
							<td style="text-align:center">
								<xsl:value-of select="@p_date"/>
							</td>
							
							<td  style="text-align:right">
								<xsl:choose>
									<xsl:when test="string(number(@percent)) = 'NaN'">
										<xsl:value-of select="@percent"/>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="format-number(@percent,'# ##0.###########################','buh')"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td  style="text-align:right">
								<xsl:value-of select="format-number(@sum_value,'# ##0.###########################','buh')"/>
							</td>
							<td style="text-align:center">
								<xsl:value-of select="@f_date"/>
							</td>
							<td style="text-align:center">
								<xsl:value-of select="@action_type"/>
							</td>
						</tr>
					</xsl:for-each>
				</tbody>
			</table>
		</xsl:if>
		
	</xsl:template>
</xsl:stylesheet>
