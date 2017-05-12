<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html" version="4.0" encoding="utf-8"/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:decimal-format name="buh" decimal-separator="," grouping-separator=" "/>
	<xsl:template match="iss_profile">
		<table width="100%" cellpadding="0" cellspacing="0" border="0">
			<tbody>
				<tr>
					<td background="/images/form_top.gif" style="width:100%">
						<img src="/images/null.gif" style="width:234px; height:17px;" alt=""/>
					</td>
					<td  colspan="2">
						<img src="/images/form_angle.gif" width="26" height="17" border="0"/>
					</td>
				</tr>
				<tr>
					<td colspan="2" background="/images/menu_bg.gif" style="width:100%">
						<img src="/images/null.gif" style="width:1px;height:10px;" alt=""/>
					</td>
					<td background="/images/form_right_bg.gif">

					</td>
				</tr>
				<tr>
					<td colspan="2" width="100%" style="background-color:#e6e6e6">
						<table style="width:100%;" border="0" cellpadding="1" cellspacing="1">
							<tbody>
								<xsl:apply-templates select="profile">
									<xsl:with-param name="pid">0</xsl:with-param>
								</xsl:apply-templates>
							</tbody>
						</table>
					</td>
					<td background="/images/form_right_bg.gif">
						
					</td>
				</tr>
			</tbody>
		</table>
				
		<table width="100%" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td background="/images/form_bottom_bg5.gif" style="padding-top: 0px; height:24px">
					<img src="/images/null.gif" style="width:235px; height:24px;"/>
				</td>
				<td width="26" height="24">
					<img src="/images/form_right_bottom5.gif" width="26" height="24" border="0"/>
				</td>
			</tr>
		</table>
	</xsl:template>
	<xsl:template match="profile">
		<xsl:param name="pid"/>
			<xsl:for-each select="menu[@parent_id = $pid]">
				<tr>
					<td>
						<xsl:attribute name="class">
							<xsl:call-template name="set_class">
								<xsl:with-param name="is_on" select="b/@is_on"/>
								<xsl:with-param name="pid" select="$pid"/>
								<xsl:with-param name="is_parent" select="b/@is_parent"/>
							</xsl:call-template>						
						</xsl:attribute>
						<xsl:attribute name="id">mtd<xsl:value-of select="@id"/></xsl:attribute>
						<xsl:if test="b/@is_on=1 and b/@is_parent=0">
									<xsl:attribute name="onclick">
										setclass(<xsl:value-of select="@id"/>,<xsl:value-of select="$pid"/>);tab_def=0;
									</xsl:attribute>
							</xsl:if>
								<xsl:value-of select="@name"/>
						
					</td>
					<td>
						<xsl:if test="b/@is_on=1 and b/@is_parent=0 and b/par/@is_prn=1">
							<img src="/images/chbox.gif" alt="" class="cb_image" title="Включить в отчет">
								<xsl:attribute name="onclick">switch_rc(<xsl:value-of select="@id"/>,this)</xsl:attribute>
								<xsl:attribute name="id">ci<xsl:value-of select="@id"/></xsl:attribute>
							</img>
						</xsl:if>
					</td>
				</tr>
				<xsl:apply-templates select="..">
					<xsl:with-param name="pid" select="@id"/>
				</xsl:apply-templates>
			</xsl:for-each>
</xsl:template>
	<xsl:template name="set_class">
		<xsl:param name="pid" select="."/>
		<xsl:param name="is_on" select="."/>
		<xsl:param name="is_parent" select="."/>
		<xsl:choose>
			
			<xsl:when test="$pid=0">
				<xsl:choose>
					<xsl:when test="$is_parent=1">
						tdparentmenu
					</xsl:when>
					<xsl:otherwise>
						<xsl:call-template name="onoff">
							<xsl:with-param name="is_on" select="$is_on"/>
							<xsl:with-param name="pref">tdmenu</xsl:with-param>
						</xsl:call-template>

					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="onoff">
					<xsl:with-param name="is_on" select="$is_on"/>
					<xsl:with-param name="pref">tdsubmenu</xsl:with-param>
				</xsl:call-template>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="onoff">
		<xsl:param name="is_on" select="."/>
		<xsl:param name="pref" select="."/>
		<xsl:choose>
			<xsl:when test="$is_on=1">
				<xsl:value-of select="$pref"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$pref"/>_dis
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet> 

