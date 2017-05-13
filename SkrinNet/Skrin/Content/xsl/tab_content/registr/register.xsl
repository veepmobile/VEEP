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
			<table width="100%">
				<xsl:for-each select="A">

					<xsl:choose>
						<xsl:when test="@name='Реестр ведется эмитентом самостоятельно'">
							<tr>
								<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
								<td colspan="2" class="profile_table_caption">Реестр ведется эмитентом самостоятельно</td>
							</tr>

						</xsl:when>
						<xsl:otherwise>
							<tr>
								<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
								<td width="40%" class="profile_table_caption">Наименование: </td>
								<td width="60%">
									<xsl:value-of select="@name"/>
								</td>
							</tr>
							<tr>
								<xsl:attribute name="bgColor">#F0F0F0</xsl:attribute>
								<td width="40%" class="profile_table_caption">Местонахождение: </td>
								<td width="60%">
									<xsl:value-of select="@live_address"/>
								</td>
							</tr>
							<!--tr>
								<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
								<td width="40%" class="profile_table_caption">Ф.И.О. и должность руководителя: </td>
								<td width="60%">
									<xsl:value-of select="@manager"/>
								</td>
							</tr-->
							<tr>
								<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
								<td width="40%" class="profile_table_caption">Почтовый адрес: </td>
								<td width="60%">
									<xsl:value-of select="@mail_address"/>
								</td>
							</tr>
							<tr>
								<xsl:attribute name="bgColor">#F0F0F0</xsl:attribute>
								<td width="40%" class="profile_table_caption">Контактные телефоны: </td>
								<td width="60%">
									<xsl:value-of select="@phone"/>
								</td>
							</tr>
							<tr>
								<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
								<td width="40%" class="profile_table_caption">Факс: </td>
								<td width="60%">
									<xsl:value-of select="@fax"/>
								</td>
							</tr>
							<tr>
								<xsl:attribute name="bgColor">#F0F0F0</xsl:attribute>
								<td width="40%" class="profile_table_caption">E-mail: </td>
								<td width="60%">
									<a>
										<xsl:attribute name="href">
											<xsl:value-of select="concat('mailto:',@email)"/>
										</xsl:attribute>
										<xsl:value-of select="@email"/>
									</a>
								</td>
							</tr>
							<tr>
								<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
								<td width="40%" class="profile_table_caption">Web-сайт: </td>
								<td width="60%">
									<a>
										<xsl:choose>
											<xsl:when test="contains(@www,'http://')">
												<xsl:attribute name="href">
													<xsl:value-of select="@www"/>
												</xsl:attribute>
											</xsl:when>
											<xsl:otherwise>
												<xsl:attribute name="href">
													<xsl:value-of select="concat('http://',@www)"/>
												</xsl:attribute>
											</xsl:otherwise>
										</xsl:choose>
										<xsl:value-of select="@www"/>
									</a>
								</td>

							</tr>
							<tr>
								<xsl:attribute name="bgColor">#F0F0F0</xsl:attribute>
								<td width="40%" class="profile_table_caption">Номер лицензии: </td>
								<td width="60%">
									<xsl:value-of select="@lic_no"/>
								</td>
							</tr>
							<tr>
								<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
								<td width="40%" class="profile_table_caption">Лицензирующий орган: </td>
								<td width="60%">
									<xsl:value-of select="B/@b_name"/>
								</td>
							</tr>
							<tr>
								<xsl:attribute name="bgColor">#F0F0F0</xsl:attribute>
								<td width="40%" class="profile_table_caption">Дата начала действия: </td>
								<td width="60%">
									<xsl:value-of select="B/@lic_start_date"/>
								</td>
							</tr>
							<tr>
								<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
								<td width="40%" class="profile_table_caption">Статус лицензии: </td>
								<td width="60%">
									<xsl:choose>
										<xsl:when test="@liv - 1 = 0">
											<xsl:value-of select="B/@lic_valid"/>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="concat(B/@lic_valid,'(Дата окончания лицензии:', B/@lic_end_date,')')"/>
										</xsl:otherwise>
									</xsl:choose>
								</td>
							</tr>
							<xsl:if test="WT[position()=last()]">
								<tr>
									<xsl:attribute name="bgColor">#F0F0F0</xsl:attribute>
									<td width="40%" class="profile_table_caption">График работы: </td>
									<td width="60%">
										<xsl:value-of disable-output-escaping="yes" select="WT/@Work_time"/>
									</td>
								</tr>
							</xsl:if>
							<tr>
								<td width="100%" colspan="2">
									<table width="100%" cellpadding="4" cellspacing="0" border="0">
										<tr>
											<td width="16" bgcolor="#FFFFFF">
												<img src="/images/icon_information.gif" width="16" height="16" border="0"/>
											</td>
											<td width="100%"  id="reghref">
												<a href="#" ><xsl:attribute name="onclick">showRegister('<xsl:value-of select="@id"/>',0);</xsl:attribute>
													Список эмитентов регистратора  <xsl:value-of select="@name"/>
												</a>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</table>
		</div>
	</xsl:template>
</xsl:stylesheet>
