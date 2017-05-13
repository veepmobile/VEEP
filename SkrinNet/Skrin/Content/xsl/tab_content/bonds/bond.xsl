<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">

  <xsl:import href="../../../xsl/common.xsl"/>
	<msxsl:script language="JScript" implements-prefix="js">
		function GetMult(nl){
		<!--var val=String(nl.nextNode().text);-->
    var val = nl.toUpperCase();
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
		<!--table width="100%" class="dragg_div_top" cellpadding="0" cellspacing="0">
			<tr>
				<th style="text-align:left;">
					<img src="/images/printer.gif" class="prn_img" alt="Печать" onclick="doPrint(&quot;act_text&quot;);"/>
				</th>
				<th class="dragg_div_top" style="text-align:right;">
					<img src="/images/close_div.gif" onclick="document.body.removeChild(getObj('pap_div'));" alt="Закрыть" style="cursor:default;"/>
				</th>
			</tr>
		</table>
			<div class='act_text' id='act_text'><xsl:attribute name='style'>width:<xsl:value-of select='//@ww'/>px; height:<xsl:value-of select='//@wh'/>px;</xsl:attribute-->
			<span class='subcaption'>
				<xsl:value-of select='//@iss_name'/>
			</span>
			<xsl:for-each select="issues">
                <div class="minicaption" style="margin-top:10px">
                    Государственный регистрационный номер   <xsl:value-of select="@reg_no"/>
                </div>
                <span style="font-weight: bold;">Выпуск, общие сведения</span>
                <div>
                    <div style="width:66%; float:left;">
                        <table width="100%">
					        <tr>
						        <td class="table_caption" style="width:50%">Наименование показателя</td>
						        <td class="table_caption" style="width:50%">Содержание (значение) показателя</td>
					        </tr>
					        <tr>
						        <td>Вид ценной бумаги</td>
						        <td>
							        <xsl:call-template name="nulldisp">
								        <xsl:with-param name="val" select="@vid"/>
							        </xsl:call-template>
						        </td>
					        </tr>
					        <tr bgcolor="#F0F0F0">
						        <td>Категория (тип), серия, форма</td>
						        <td>
							        <xsl:call-template name="nulldisp">
								        <xsl:with-param name="val" select="@stype"/>
							        </xsl:call-template>
						        </td>
					        </tr>
					        <tr>
						        <td>Номер серии</td>
						        <td>
							        <xsl:call-template name="nulldisp">
								        <xsl:with-param name="val" select="@bond_series_no"/>
							        </xsl:call-template>
						        </td>
					        </tr>
					        <tr bgcolor="#F0F0F0">
						        <td>Тип дохода</td>
						        <td>
							        <xsl:call-template name="nulldisp">
								        <xsl:with-param name="val" select="@income_type"/>
							        </xsl:call-template>
						        </td>
					        </tr>
					        <tr bgcolor="#F0F0F0">
						        <td>Количество купонов </td>
						        <td>
							        <xsl:call-template name="nulldisp">
								        <xsl:with-param name="val" select="@coupons"/>
							        </xsl:call-template>
						        </td>
					        </tr>
					        <tr>
						        <td>Централизованное хранение</td>
						        <td>
							        <xsl:call-template name="nulldisp">
								        <xsl:with-param name="val" select="@storage"/>
							        </xsl:call-template>
						        </td>
									
					        </tr>
					        <tr bgcolor="#F0F0F0">
						        <td>Обеспечение</td>
						        <td>
							        <xsl:call-template name="nulldisp">
								        <xsl:with-param name="val" select="@provision"/>
							        </xsl:call-template>
						        </td>
					        </tr>
					        <tr>
						        <td>Возможность досрочного погашения</td>
						        <td>
							        <xsl:call-template name="nulldisp">
								        <xsl:with-param name="val" select="@can_repay"/>
							        </xsl:call-template>
						        </td>
					        </tr>
					        <tr bgcolor="#F0F0F0">
						        <td>Расчетная дата  погашения</td>
						        <td>
							        <xsl:call-template name="nulldisp">
								        <xsl:with-param name="val" select="@red"/>
							        </xsl:call-template>
						        </td>
					        </tr>
					        <tr>
						        <td>Период обращения</td>
						        <td>
							        <xsl:call-template name="nulldisp">
								        <xsl:with-param name="val" select="@redemption_end_period"/>
							        </xsl:call-template>
						        </td>
					        </tr>
					        <tr bgcolor="#F0F0F0">
						        <td>Размещение среди квалифицированных инвесторов</td>
						        <td>
							        <xsl:call-template name="nulldisp">
								        <xsl:with-param name="val" select="@QualInv"/>
							        </xsl:call-template>
						        </td>
					        </tr>
					        <tr>
						        <td>Название ценной бумаги на русском языке</td>
						        <td>
							        <xsl:call-template name="nulldisp">
								        <xsl:with-param name="val" select="@iname"/>
							        </xsl:call-template>
						        </td>
					        </tr>
					        <tr bgcolor="#F0F0F0">
						        <td>Название ценной бумаги на английском языке</td>
						        <td>
							        <xsl:call-template name="nulldisp">
								        <xsl:with-param name="val" select="@iname_eng"/>
							        </xsl:call-template>
						        </td>
					        </tr>
					        <tr>
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
					        <tr  bgcolor="#F0F0F0">
						        <td>Номинальная стоимость каждой ценной бумаги выпуска<xsl:value-of select="concat(', ', @curr_short_name)"/>
					        </td>
						        <td style="text-align:right;">
							        <xsl:value-of select="format-number(js:GetMult(string(@face_value)), '# ##0.00##############', 'buh')"/>
						        </td>
					        </tr>
					        <tr  bgcolor="#F0F0F0">
						        <td>Общий объем по номинальной стоимости<xsl:value-of select="concat(', ', @curr_short_name)"/>
						        </td>
						        <td style="text-align:right;">
							        <xsl:call-template name="calc_fraction">
								        <xsl:with-param name="price" select="js:GetMult(string(@face_value))"/>
								        <xsl:with-param name="amount" select="@shares_rolling"/>
							        </xsl:call-template>
						        </td>
					        </tr>
					        <tr>
						        <td>Текущее состояние ценных бумаг выпуска</td>
						        <td>
							        <xsl:call-template name="nulldisp">
								        <xsl:with-param name="val" select="@ps_name"/>
							        </xsl:call-template>
						        </td>
					        </tr>
					        <tr  bgcolor="#F0F0F0">
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
                                    <span style="font-weight: bold;">Сведения о размещении и иные сведения</span>
						        </td>
					        </tr>
					        <tr>
						        <td>Дата государственной регистрации/Дата допуска к торгам на фондовой бирже в процессе размещения</td>
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
								        <xsl:with-param name="price" select="js:GetMult(string(@face_value))"/>
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
								        <xsl:with-param name="price" select="js:GetMult(string(@face_value))"/>
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
							        <xsl:value-of select="@report_reg_date"/>
							        <xsl:call-template name="nulldisp">
								        <xsl:with-param name="val" select="@val"/>
							        </xsl:call-template>
						        </td>
					        </tr>
					        <tr>
						        <td>
							        Погашение  ценных бумаг.
						        </td>
						        <td>
							        <xsl:choose>
								        <xsl:when test="repayment/redemptions">
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
													        <xsl:value-of select="@doc_id"/>
												        </td>
												        <td style="text-align:right;">
                                  
													        <xsl:choose>
														        <xsl:when test="contains(@doc_name,'/')">
															        <table  cellpadding="0" cellspacing="0" border="0"  align="right">
																        <tr>
																	        <td rowspan="2" class="numbers" style="padding-right:3px;">
																		        <xsl:value-of select="format-number(substring-before(@doc_name,' '),'# ##0','buh')"/>
																	        </td>
																	        <td class="numbers">
																		        <xsl:value-of select="format-number(substring-before(substring-after(@doc_name,' '),'/'),'# ##0','buh')"/>
																	        </td>
																        </tr>
																        <tr>
																	        <td style="border-top:solid 1px #000000" class="numbers">
																		        <xsl:value-of select="format-number(substring-after(@doc_name,'/'),'# ##0','buh')"/>
																	        </td>
																        </tr>
															        </table>
														        </xsl:when>
														        <xsl:otherwise>
															        <xsl:value-of select="format-number(@doc_name,'# ##0.################','buh')"/>
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
				        </table>
                    </div>
                    <div style="width:33%; float:left;margin-left:6px">
                        <xsl:if test="docs">
					        <table width="100%">
						        <tr>
							        <td class="table_caption" colspan="2">Вид документа(информации)</td>
						        </tr>
						        <xsl:for-each select="docs/doc">
							        <tr>
								        <td style="width:80%">
									        <xsl:call-template name="img_by_ext">
										        <xsl:with-param name="ext" select="@ext"/>
									        </xsl:call-template>
									        <a  target="_blank">
										        <xsl:attribute name="href">
											        <!--/issuers/<xsl:value-of select="//@iss"/>/documents/<xsl:value-of select="@file_name"/>?doc_id=-50&amp;id=<xsl:value-of select="@doc_id"/>-->
                        /Documents/Index?iss=<xsl:value-of select="//@iss"/>&amp;id=<xsl:value-of select="@doc_id"/>&amp;fn=<xsl:value-of select="@file_name"/>&amp;doc_type=0
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
                    </div>
                </div>
            </xsl:for-each>
		<!--/div-->
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
