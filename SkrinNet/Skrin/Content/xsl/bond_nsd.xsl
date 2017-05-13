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
		<span class='subcaption'>
			<xsl:value-of select='//@iss_name'/>
		</span>
		<xsl:for-each select="RN">

			<table width="100%" border="0">
				<tr>
					<td colspan="2">
						<b>
							Государственный регистрационный номер   <xsl:value-of select="@rnn"/>
						</b>
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<b>Выпуск, общие сведения</b>
					</td>
				</tr>
				<xsl:for-each select="v">
					<xsl:if test="position()=1">
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
					</xsl:if>
					<xsl:choose>
						<xsl:when test="@p_name='a1'">
							<tr>
								<td colspan="2">
									<hr/>
									<b>
									<xsl:value-of select="@p_val"/>
									</b>
								
								</td>
							</tr>
						</xsl:when>
						<xsl:otherwise>

							<tr>
								<xsl:attribute name="bgcolor">
									<xsl:call-template name="set_bg">
										<xsl:with-param name="str_num" select="position()+1"/>
									</xsl:call-template>
								</xsl:attribute>
								

										<xsl:choose>
											<xsl:when test="contains(@p_val,'|')">
												<td >
													<xsl:value-of select="@p_name"/> <xsl:value-of select="substring-after(@p_val,'|')"/>
												</td>
											</xsl:when>
											<xsl:otherwise>
												<td style="width:50%">
													<xsl:value-of select="@p_name"/>
												</td>
											</xsl:otherwise>
										</xsl:choose>


								
								

								<xsl:choose>
									<xsl:when test="string(number(@p_val)) = 'NaN'">

										<xsl:choose>
											<xsl:when test="contains(@p_val,'|')">
												<td style="text-align:right;">
													<xsl:value-of select="format-number(substring-before(@p_val,'|'), '# ##0.00##############', 'buh')"/>
												</td>
											</xsl:when>
											<xsl:otherwise>
												<td>
													<xsl:value-of select="@p_val"/>
												</td>
											</xsl:otherwise>
										</xsl:choose>


									</xsl:when>
									<xsl:otherwise>
										<td style="text-align:right;">
											<xsl:value-of select="format-number(js:GetMult(@p_val), '# ##0.##############', 'buh')"/>
										</td>

									</xsl:otherwise>
								</xsl:choose>
							</tr>
						</xsl:otherwise>
					</xsl:choose>

				</xsl:for-each>
			</table>
			<hr/>
		</xsl:for-each>
	</xsl:template>
	
	
</xsl:stylesheet>
