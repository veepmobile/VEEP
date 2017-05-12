<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:import href="common.xsl"/>
	<xsl:output method="html" version="4.0" encoding="utf-8"/>
	<xsl:template match="/">
		
							<table width="60%" height="100%" cellpadding="0" cellspacing="0" border="0">
								<tr>
									<td width="100%" valign="top">
										<div style="width: 720px; height: 1px;">
											<spacer type="block" width="620px" height="1px" />
										</div>
										
										<font class="subcaption">
											Сведения о структуре и компетенции органов управления.
										</font>
										<br/>
										<br/>
										<xsl:call-template name="set_br">
											<xsl:with-param name="text" select="main/структура_органов_управления/описание"/>
										</xsl:call-template>
										
									</td>
								</tr>
								<xsl:if test="count(main/структура_органов_управления/компетенция_общ_собрания_акционеров) &gt; 0">
									<tr>
										<td width="100%" valign="top">
											<div style="width: 720px; height: 1px;">
												<spacer type="block" width="620px" height="1px" />
											</div>
											<img src="/images/mnu_bullet_10.gif" width="14" height="11" border="0" align="absmiddle"/>
											<font class="subcaption">
												Компетенция общего собрания акционеров (участников) эмитента в соответствии с его уставом (учредительными документами).
											</font>
											<br/>
											<br/>
											<xsl:call-template name="set_br">
												<xsl:with-param name="text" select="main/структура_органов_управления/компетенция_общ_собрания_акционеров"/>
											</xsl:call-template>

										</td>
									</tr>
								</xsl:if>
								<xsl:if test="count(main/структура_органов_управления/компетенция_совета_директоров) &gt; 0">
									<tr>
										<td width="100%" valign="top">
											<div style="width: 720px; height: 1px;">
												<spacer type="block" width="620px" height="1px" />
											</div>
											<img src="/images/mnu_bullet_10.gif" width="14" height="11" border="0" align="absmiddle"/>
											<font class="subcaption">
												Члены совета директоров (наблюдательного совета) эмитента.
											</font>
											<br/>
											<br/>
											<xsl:call-template name="set_br">
												<xsl:with-param name="text" select="main/структура_органов_управления/компетенция_совета_директоров"/>
											</xsl:call-template>
										</td>
									</tr>
								</xsl:if>
								<xsl:if test="count(main/структура_органов_управления/компетенция_исп_органа) &gt; 0">
									<tr>
										<td width="100%" valign="top">
											<div style="width: 720px; height: 1px;">
												<spacer type="block" width="620px" height="1px" />
											</div>
											<img src="/images/mnu_bullet_10.gif" width="14" height="11" border="0" align="absmiddle"/>
											<font class="subcaption">
												Единоличный и коллегиальный органы управления эмитента и должностные лица управляющего эмитента.
											</font>
											<br/>
											<br/>
											<xsl:call-template name="set_br">
												<xsl:with-param name="text" select="main/структура_органов_управления/компетенция_исп_органа"/>
											</xsl:call-template>

										</td>
									</tr>
								</xsl:if>
							</table>
			
	</xsl:template>

</xsl:stylesheet>
