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
		<input type="hidden" id="iss"><xsl:attribute name="value"><xsl:value-of select="//@iss"/></xsl:attribute></input>
		<span class="subcaption">Сведения об организациях, осуществляющих учёт прав на акции</span>
		<br/>
		<table width="100%">
		<xsl:for-each select="a">

		
			<a RegID="13631" FullName="Открытое акционерное общество &quot;Регистратор &quot;НИКойл&quot;" Name="ОАО &quot;Регистратор &quot;НИКойл&quot;" Reg_Num="001.152.089" OGRN="1027700060607" OGRN_Date="2002-07-25T00:00:00" INN="7730081453" Post_Addr="Россия, 121108, Москва, ул. Ивана Франко, д. 8" Phone_Fax="926-81-61, 926-81-73; 926-81-78" EMail="rcnikoil@rcnikoil.ru" type_reg="1" RegionalFactor="1.000000" Real_Addr="Россия, 121108, Москва, ул. Ивана Франко, д. 8" Licence="Лицензия: На осуществление деятельности по ведению реестра, серия: 03 №000444, №10-000-1-00290, выдана: ФКЦБ России&#xA;Без ограничения срока действия, дата начала: 17.06.2003" Work_Time="с 10.00 до 16.00, обед с 13.00 до 14.00&#xA;пятница с 10.00 до 15.00" update_date="2011-05-06T09:55:46.927"/>

			<tr>
				<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
				<td width="40%" class="profile_table_caption">Полное наименование: </td>
				<td width="60%">
					<xsl:value-of select="@FullName"/>
				</td>
			</tr>
			<tr>
				<xsl:attribute name="bgColor">#F0F0F0</xsl:attribute>
				<td width="40%" class="profile_table_caption">ИНН: </td>
				<td width="60%">
					<xsl:value-of select="@INN"/>
				</td>
			</tr>
			<tr>
				<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
				<td width="40%" class="profile_table_caption">ОГРН: </td>
				<td width="60%">
					<xsl:value-of select="@OGRN"/>
				</td>
			</tr>
			<tr>
				<xsl:attribute name="bgColor">#F0F0F0</xsl:attribute>
				<td width="40%" class="profile_table_caption">Местонахождение: </td>
				<td width="60%">
					<xsl:value-of select="@Real_Addr"/>
				</td>
			</tr>
			<tr>
				<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
				<td width="40%" class="profile_table_caption">Почтовый адрес: </td>
				<td width="60%">
					<xsl:value-of select="@Post_Addr"/>
				</td>
			</tr>
			<tr>
				<xsl:attribute name="bgColor">#F0F0F0</xsl:attribute>
				<td width="40%" class="profile_table_caption">Контактные телефоны, факс: </td>
				<td width="60%">
					<xsl:value-of select="@Phone_Fax"/>
				</td>
			</tr>
			
			<tr>
				<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
				<td width="40%" class="profile_table_caption">E-mail: </td>
				<td width="60%">
					<a>
						<xsl:attribute name="href">
							<xsl:value-of select="concat('mailto:',@EMail)"/>
						</xsl:attribute>
						<xsl:value-of select="@EMail"/>
					</a>
				</td>
			</tr>
			<tr>
				<xsl:attribute name="bgColor">#F0F0F0</xsl:attribute>
				<td width="40%" class="profile_table_caption">Лицензия: </td>
				<td width="60%">
					<xsl:value-of select="@Licence"/>
				</td>
			</tr>
			
				<tr>
					<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
					<td width="40%" class="profile_table_caption">График работы: </td>
					<td width="60%">
						<xsl:value-of disable-output-escaping="yes" select="@Work_Time"/>
					</td>
				</tr>
			<xsl:if test="//@PDF=-1">
			<tr>
				<td width="100%" colspan="2">
					<table width="100%" cellpadding="4" cellspacing="0" border="0">
						<tr>
							<td width="16" bgcolor="#FFFFFF">
								<img src="/images/icon_information.gif" width="16" height="16" border="0"/>
							</td>
							<td width="100%" id="reghref">
								<a href="#" >
									<xsl:attribute name="onclick">
										showRegister('<xsl:value-of select="@RegID"/>',1);
									</xsl:attribute>
									Список эмитентов регистратора  <xsl:value-of select="@Name"/>
								</a>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			</xsl:if>
			<tr>
				<td width="100%" colspan="2">
					<hr width="100%" size="1" color="#ABABAB"/>
				</td>
			</tr>
		
		</xsl:for-each>
		</table>
	
		<span class="data_comment limitation">
			ВНИМАНИЕ: в связи с особенностями функционирования и обновления указанного источника информации АО "СКРИН" не может гарантировать абсолютную актуальность и достоверность данных.
		</span>
		<script language="javascript" type="text/javascript">
			<![CDATA[ 
			xls_params={"iss":$("#iss").val(),"module" : "registr/dkk/","x":Math.random()}
			]]>
		</script>
	</xsl:template>
</xsl:stylesheet>
