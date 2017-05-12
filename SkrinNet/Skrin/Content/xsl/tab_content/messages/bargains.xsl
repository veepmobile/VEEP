<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 	xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="urn:deitel:user" xmlns:js="javascript:code">
	<xsl:import href="showdocs.xsl"/>
	<msxsl:script language="JScript" implements-prefix="js">

	</msxsl:script>
	<xsl:output method="html" version="4.0" encoding="UTF-8"/>

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
		<div style="background-color: #E6E6E6; padding-bottom: 8px; padding-right: 8px;" onclick="hidepopups();">
			<table cellspacing="3" cellpadding="0" width="100%" border="0">
				<tr>
					<td nowrap="yes">Период с:</td>
					<td>
            <input type="hidden" id="mind" class="system_form" >
              <xsl:attribute name="value">
                <xsl:value-of select="../@mind"/>
              </xsl:attribute>
            </input>
            <input type="hidden"  id="maxd" class="system_form" >
              <xsl:attribute name="value">
                <xsl:value-of select="../@maxd"/>
              </xsl:attribute>
            </input>
						<input type="text" name="dfrom" id="dfrom" class="system_form" style="width:70px;">
							<xsl:attribute name="value">
								<xsl:value-of select="//@d1"/>
							</xsl:attribute>
						</input>
					</td>
					<td>по:</td>
					<td>
						<input type="text" name="dto" id="dto" class="system_form" style="width:70px;">
							<xsl:attribute name="value">
								<xsl:value-of select="//@d2"/>
							</xsl:attribute>
						</input>
					</td>
					<td nowrap="yes">Тип сообщения:</td>
					<td id="td_bt" style="width: 100%; white-space: nowrap;padding-right:5px;" title="">
						<input type="text" id="bt" name="bt" style="width: 100%; cursor: pointer;" readonly="readonly" class="system_form" onclick="show_tree_selector(event,8,1,1)" />
						<input type="hidden" id="bt_val" value="" />
						<input type="hidden" id="bt_excl" value="0" />
					</td>
					<td valign="top" style="padding-left:5px;">
						<input type="button" class="system_form btns blue" value="НАЙТИ" onclick="CommonBargPager(1)"/>
					</td>
				</tr>
			</table>
			<input type="hidden" id="iss"><xsl:attribute name="value"><xsl:value-of select="//@iss"/></xsl:attribute></input>
		</div>
			<div id="barg_found">
				<xsl:call-template name="showdocs"/>
			</div>	


	</xsl:template>

</xsl:stylesheet>