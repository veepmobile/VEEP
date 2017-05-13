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
		<!--table width="100%"  style="height:inherit;" cellpadding="0" cellspacing="0">
			<tr>
				<th style="text-align:left;height:25px;" class="dragg_div_top">
					<img src="/images/printer.gif" class="prn_img" alt="Печать" onclick="doPrint(&quot;act_text&quot;);"/>
				</th>
				<th class="dragg_div_top" style="text-align:right;" >
					<img src="/images/close_div.gif" onclick="document.body.removeChild(getObj('pap_div'));" alt="Закрыть" style="cursor:default;"/>
				</th>
			</tr>

			<tr>
				<td style='width:100%; height:inherit' colspan='2'>
					<div class='act_text' id='act_text'>
						<xsl:attribute name='style'>
							width:<xsl:value-of select='//@ww'/>px; height:<xsl:value-of select='//@wh'/>px;
						</xsl:attribute-->
						<span class='subcaption'>
							<xsl:value-of select='//@iss_name'/>
						</span>
						<xsl:for-each select="issues">

							<table width="100%" border="0">
								<tr>
									<td colspan="2">
										<b>
											Государственный регистрационный номер   <xsl:value-of select="@reg_no"/>
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
													<xsl:value-of select="@action_type"/>
												</td>
											</tr>
											<tr bgcolor="#F0F0F0">
												<td>Категория (тип), серия, форма</td>
												<td>
													<xsl:call-template name="nulldisp">
														<xsl:with-param name="val" select="@st_name"/>
													</xsl:call-template>
												</td>
											</tr>
											<tr>
												<td>Размещение среди квалифицированных инвесторов</td>
												<td>
													<xsl:call-template name="nulldisp">
														<xsl:with-param name="val" select="@QualInv"/>
													</xsl:call-template>

												</td>
											</tr>
											<tr bgcolor="#F0F0F0">
												<td>Название ценной бумаги на русском языке</td>
												<td>
													<xsl:call-template name="nulldisp">
														<xsl:with-param name="val" select="@iname"/>
													</xsl:call-template>

												</td>
											</tr>
											<tr>
												<td>Название ценной бумаги на английском языке</td>
												<td>
													<xsl:call-template name="nulldisp">
														<xsl:with-param name="val" select="@iname_eng"/>
													</xsl:call-template>
												</td>
											</tr>
											<tr bgcolor="#F0F0F0">
												<td>Количество в обращении  ценных бумаг, шт.</td>
												<td style="text-align:right;">
													<xsl:choose>
														<xsl:when test="contains(@shares_rolling,'/')">
															<table  cellpadding="0" cellspacing="0" border="0"  align="right">
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
											</tr>
											<tr>
												<td>
													Номинальная стоимость каждой ценной бумаги выпуска<xsl:value-of select="concat(', ', @curr_short_name)"/>
												</td>
												<td style="text-align:right;">
													<xsl:value-of select="format-number(js:GetMult(@face_value), '# ##0.00##############', 'buh')"/>
												</td>
											</tr>
											<!--tr bgcolor="#F0F0F0">
									<td>Валюта номинала</td>
									<td>
										<xsl:call-template name="nulldisp">
											<xsl:with-param name="val" select="@curr_name"/>
										</xsl:call-template>

									</td>
								</tr-->
											<tr>
												<td>
													Общий объем по номинальной стоимости в обращении<xsl:value-of select="concat(', ', @curr_short_name)"/>
												</td>
												<td style="text-align:right;">
													<xsl:call-template name="calc_fraction">
														<xsl:with-param name="price" select="js:GetMult(@face_value)"/>
														<xsl:with-param name="amount" select="@shares_rolling"/>
													</xsl:call-template>
												</td>
											</tr>
											<tr bgcolor="#F0F0F0">
												<td>Текущее состояние ценных бумаг выпуска</td>
												<td>
													<xsl:call-template name="nulldisp">
														<xsl:with-param name="val" select="@ps_name"/>
													</xsl:call-template>

												</td>
											</tr>
											<tr>
												<td>
													Наличие зарегистрированного проспекта ценных бумаг, дата регистрации проспекта
												</td>
												<td>
													<xsl:call-template name="nulldisp">
														<xsl:with-param name="val" select="@prospect"/>
													</xsl:call-template>

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
													<xsl:call-template name="nulldisp">
														<xsl:with-param name="val" select="@rd"/>
													</xsl:call-template>

												</td>
											</tr>
											<tr bgcolor="#F0F0F0">
												<td>Орган, зарегистрировавший выпуск/присвоивший идентификационный номер</td>
												<td>
													<xsl:call-template name="nulldisp">
														<xsl:with-param name="val" select="@e_name"/>
													</xsl:call-template>
												</td>
											</tr>
											<tr>
												<td>Способ размещения</td>
												<td>
													<xsl:call-template name="nulldisp">
														<xsl:with-param name="val" select="@pt_name"/>
													</xsl:call-template>
												</td>
											</tr>
											<tr bgcolor="#F0F0F0">
												<td>Количество ценных бумаг, подлежавших размещению, шт.</td>
												<td style="text-align:right;">
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
											</tr>
											<tr>
												<td>
													Объявленный  объём<xsl:value-of select="concat(', ', @curr_short_name)"/>
												</td>
												<td style="text-align:right;">
													<xsl:call-template name="calc_fraction">
														<xsl:with-param name="price" select="js:GetMult(@face_value)"/>
														<xsl:with-param name="amount" select="@shares_declared"/>
													</xsl:call-template>
												</td>
											</tr>
											<tr bgcolor="#F0F0F0">
												<td>Количество размещенных ценных бумаг, шт.</td>
												<td style="text-align:right;">
													<xsl:choose>
														<xsl:when test="contains(@shares_placed,'/')">
															<table  cellpadding="0" cellspacing="0" border="0"  align="right">
																<tr>
																	<td rowspan="2" class="numbers" style="padding-right:3px;">
																		<xsl:value-of select="format-number(substring-before(@shares_placed,' '),'# ##0','buh')"/>
																	</td>
																	<td class="numbers">
																		<xsl:value-of select="format-number(substring-before(substring-after(@shares_placed,' '),'/'),'# ##0','buh')"/>
																	</td>
																</tr>
																<tr>
																	<td style="border-top:solid 1px #000000" class="numbers">
																		<xsl:value-of select="format-number(substring-after(@shares_placed,'/'),'# ##0','buh')"/>
																	</td>
																</tr>
															</table>
														</xsl:when>
														<xsl:otherwise>
															<xsl:value-of select="format-number(@shares_placed,'# ##0.################','buh')"/>
														</xsl:otherwise>
													</xsl:choose>
												</td>
											</tr>
											<tr>
												<td>
													Размещённый  объём<xsl:value-of select="concat(', ', @curr_short_name)"/>

												</td>
												<td style="text-align:right;">
													<xsl:call-template name="calc_fraction">
														<xsl:with-param name="price" select="js:GetMult(@face_value)"/>
														<xsl:with-param name="amount" select="@shares_placed"/>
													</xsl:call-template>
												</td>
											</tr>
											<tr bgcolor="#F0F0F0">
												<td>Дата начала размещения</td>
												<td>
													<xsl:call-template name="nulldisp">
														<xsl:with-param name="val" select="@plac_start_date"/>
													</xsl:call-template>
												</td>
											</tr>
											<tr>
												<td>Дата окончания размещения</td>
												<td>
													<xsl:call-template name="nulldisp">
														<xsl:with-param name="val" select="@plac_end_date"/>
													</xsl:call-template>
												</td>
											</tr>
											<tr bgcolor="#F0F0F0">
												<td>
													Дата регистрации отчета или дата представления уведомления об итогах
												</td>
												<td>
													<xsl:call-template name="nulldisp">
														<xsl:with-param name="val" select="@report_reg_date"/>
													</xsl:call-template>
												</td>
											</tr>
											<tr>
												<td>
													Код  дополнительного выпуска
												</td>
												<td>
													<xsl:call-template name="nulldisp">
														<xsl:with-param name="val" select="@ext_code"/>
													</xsl:call-template>
												</td>
											</tr>
											<tr bgcolor="#F0F0F0">
												<td>
													Дата аннулирования индивидуального номера (кода)
												</td>
												<td>
													<xsl:call-template name="nulldisp">
														<xsl:with-param name="val" select="@ext_code_end_date"/>
													</xsl:call-template>
												</td>
											</tr>
											<tr>
												<td>
													Погашение  ценных бумаг.
												</td>
												<td>
													<xsl:choose>
														<xsl:when test="repayment">
															<table width="100%">
																<tr>
																	<td>
																		<b>Дата</b>
																	</td>
																	<td>
																		<b>Количество</b>
																	</td>
																</tr>
																<xsl:for-each select="repayment/redemptions">
																	<tr>
																		<td>
																			<xsl:value-of select="@data"/>
																		</td>
																		<td style="text-align:right;">
																			<xsl:choose>
																				<xsl:when test="contains(@shares,'/')">
																					<table  cellpadding="0" cellspacing="0" border="0"  align="right">
																						<tr>
																							<td rowspan="2" class="numbers" style="padding-right:3px;">
																								<xsl:value-of select="format-number(substring-before(@shares,' '),'# ##0','buh')"/>
																							</td>
																							<td class="numbers">
																								<xsl:value-of select="format-number(substring-before(substring-after(@shares,' '),'/'),'# ##0','buh')"/>
																							</td>
																						</tr>
																						<tr>
																							<td style="border-top:solid 1px #000000" class="numbers">
																								<xsl:value-of select="format-number(substring-after(@shares,'/'),'# ##0','buh')"/>
																							</td>
																						</tr>
																					</table>
																				</xsl:when>
																				<xsl:otherwise>
																					
																					<xsl:choose>
																						<xsl:when test="contains(@shares,'.')">
																							<xsl:value-of select="format-number(substring-before(@shares,'.'),'# ##0','buh')"/>.<xsl:value-of select="substring-after(@shares,'.')"/>
																						</xsl:when>
																						<xsl:otherwise>
																							<xsl:value-of select="format-number(@shares,'# ##0.################','buh')"/>
																						</xsl:otherwise>
																					</xsl:choose>

																				</xsl:otherwise>
																			</xsl:choose>
																		</td>
																	</tr>
																</xsl:for-each>
															</table>
														</xsl:when>
														<xsl:otherwise>-</xsl:otherwise>
													</xsl:choose>
												</td>
											</tr>
											<tr bgcolor="#F0F0F0">
												<td>
													Состояние ценных бумаг выпуска
												</td>
												<td>
													<table width="100%">
														<tr>
															<td>
																<b>Дата</b>
															</td>
															<td>
																<b>Статус </b>
															</td>
														</tr>
														<xsl:for-each select="states/state">
															<tr>
																<td>
																	<xsl:value-of select="@data"/>
																</td>
																<td>
																	<xsl:value-of select="@state_name"/>
																</td>
															</tr>
														</xsl:for-each>
													</table>
												</td>
											</tr>
											<tr>
												<td>Код   ISIN</td>
												<td>

													<xsl:call-template name="nulldisp">
														<xsl:with-param name="val" select="@icin1"/>
													</xsl:call-template>
												</td>
											</tr>
											<xsl:for-each select="codes">
												<tr>
													<xsl:attribute name="bgcolor">
														<xsl:call-template name="set_bg">
															<xsl:with-param name="str_num" select="position()+1"/>
														</xsl:call-template>
													</xsl:attribute>
													<td>
														Код  <xsl:value-of select="@bname"/>
													</td>
													<td>
														<xsl:call-template name="nulldisp">
															<xsl:with-param name="val" select="@val"/>
														</xsl:call-template>
													</td>
												</tr>
											</xsl:for-each>
											<xsl:if test="nsd">
												<nsd code_nsd="RU000A0JP5V6" cfi="ESVXXR">
													<br sec_type_br_name="Акции кредитных организаций-резидентов (обыкновенные)" />
												</nsd>
												<tr  bgcolor="#F0F0F0">
													<td>
														Код НРД
													</td>
													<td>
														<xsl:value-of select="nsd/@code_nsd"/>
													</td>
												</tr>
												<tr  bgcolor="#FFFFFF">
													<td>
														Код CFI
													</td>
													<td>
														<xsl:value-of select="nsd/@cfi"/>
													</td>
												</tr>
												<tr  bgcolor="#F0F0F0">
													<td>
														Тип ценной бумаги по классификации Банка России

													</td>
													<td>
														<xsl:value-of select="nsd/br/@sec_type_br_name"/>
													</td>
												</tr>

											</xsl:if>
										</table>
									</td>
									<td valign="top" width="40%">
										<xsl:if test="docs">
											<br/>
											<table width="100%">
												<tr>
													<td class="table_caption" colspan="2">Вид документа(информации)</td>

												</tr>
												<tr>
													<td class="table_shadow" colspan="2">
														<div style="width: 1px; height: 1px;">
															<spacer type="block" width="1px" height="1px" />
														</div>
													</td>

												</tr>
												<xsl:for-each select="docs/doc">
													<tr>
														<td style="width:80%">
															<xsl:call-template name="img_by_ext">
																<xsl:with-param name="ext" select="@ext"/>
															</xsl:call-template>
															<a  target="_blank">
																<xsl:attribute name="href">
																	/issuers/<xsl:value-of select="//@iss"/>/documents/<xsl:value-of select="@file_name"/>?doc_id=-50&amp;id=<xsl:value-of select="@doc_id"/>
																</xsl:attribute>

																<xsl:value-of select="@doc_name"/>
															</a>
														</td>
														<td style="width:20%;text-align:right;" >
															<xsl:value-of select="@size"/>

														</td>
													</tr>

												</xsl:for-each>
											</table>
										</xsl:if>
									</td>
								</tr>
								
							</table>
							<hr/>
						</xsl:for-each>
					<!--/div>
				</td>
			</tr>
		</table-->
	</xsl:template>
	<xsl:template name="nulldisp">
		<xsl:param name="val" select="."/>
		<xsl:choose>
			<xsl:when test="string-length($val) &gt;0">
				<xsl:value-of select="$val"/>
			</xsl:when>
			<xsl:otherwise>-</xsl:otherwise>
		</xsl:choose>
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
