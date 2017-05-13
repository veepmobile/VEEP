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
		<span class="minicaption">РЕЕСТР ХОЗЯЙСТВУЮЩИХ СУБЪЕКТОВ (ЗА ИСКЛЮЧЕНИЕМ ФИНАНСОВЫХ ОРГАНИЗАЦИЙ), ИМЕЮЩИХ ДОЛЮ НА РЫНКЕ ОПРЕДЕЛЕННОГО ТОВАРА В РАЗМЕРЕ БОЛЕЕ ЧЕМ 35  %  ИЛИ ЗАНИМАЮЩИХ ДОМИНИРУЮЩЕЕ ПОЛОЖЕНИЕ НА РЫНКЕ ОПРЕДЕЛЕННОГО ТОВАРА, ЕСЛИ В ОТНОШЕНИИ ТАКОГО РЫНКА ДРУГИМИ ФЕДЕРАЛЬНЫМИ ЗАКОНАМИ В ЦЕЛЯХ ИХ ПРИМЕНЕНИЯ УСТАНОВЛЕНЫ СЛУЧАИ ПРИЗНАНИЯ ДОМИНИРУЮЩИМ ПОЛОЖЕНИЯ ХОЗЯЙСТВУЮЩИХ СУБЪЕКТОВ</span>
		<table  width="100%" border="0">
			<tr>
				<td >
					Источник данных: ФАС России.
				</td>
				<td style="text-align:right;" >
					Дата выгрузки результатов: <xsl:value-of select="//dt/@val"/>
				</td>
			</tr>
		</table>
		<span class='issuercaption'>
			<xsl:value-of select='//name/@val'/>
		</span>
		<hr/>
		<table  width="100%" border="0">
			<xsl:for-each select="r">
				<tr>
					<td colspan="2" style="text-align:center">
						<span class="minicaption">
							<xsl:value-of select="@region_name"/>
						</span>
					</td>
				</tr>
				<xsl:for-each select="fi">
					<tr>
						<td colspan="2" class="bluecaption" style="text-align:center">
							Приказы о включении ХС в реестр/внесении изменений <xsl:value-of select="@ordernum"/>
						</td>
					</tr>
					<xsl:for-each select="v">

						<xsl:choose>
							<xsl:when test="@p_name='h1' or @p_name='h2' or @p_name='h3'">
								<tr>
									<td colspan="2" style="font-weight:bold;">
										<xsl:value-of select="@p_val"/>
									</td>
								</tr>
								<tr>
									<td class="table_caption" style="width:50%">
										<div style="width: 1px; height: 1px;"></div>
									</td>
									<td class="table_caption" style="width:50%">
										<div style="width: 1px; height: 1px;"></div>
									</td>
								</tr>
								<tr>
									<td class="table_shadow">
										<div style="width: 1px; height: 1px;"></div>
									</td>
									<td class="table_shadow">
										<div style="width: 1px; height: 1px;"></div>
									</td>
								</tr>
							</xsl:when>
							<xsl:otherwise>
								<tr>
									<td style="width:50%">
										<xsl:value-of select="@p_name"/>
									</td>
									<td style="width:50%">
										<xsl:value-of disable-output-escaping="yes" select="@p_val"/>
									</td>
								</tr>
							</xsl:otherwise>
						</xsl:choose>
					</xsl:for-each>
					<tr>
						<td colspan="2">
							<hr/>
						</td>
					</tr>
				</xsl:for-each>
			</xsl:for-each>
		</table>
		<span class="data_comment limitation">
			Внимание: в связи с особенностями функционирования и обновления указанного источника информации АО "СКРИН" не может гарантировать абсолютную актуальность и достоверность данных.
		</span>
	</xsl:template>


</xsl:stylesheet>
