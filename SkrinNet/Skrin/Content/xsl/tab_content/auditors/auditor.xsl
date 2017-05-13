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
		<table width="100%" style="cursor:default;text-decoration:none;color:#000;">
		<xsl:for-each select="aud">
			<tr><xsl:attribute name="bgcolor"><xsl:call-template name="set_bg"><xsl:with-param name="str_num" select="position()"/></xsl:call-template></xsl:attribute>
				<td style="text-decoration:none;color:#000;">
					<xsl:value-of select="@p_name"/>
				</td>
				<td style="color:#000;">
					<xsl:if test="position()=last()">
						<xsl:attribute name="id">ahref</xsl:attribute>
						<xsl:attribute name="style">cursor:pointer;text-decoration:underline;color:#003399</xsl:attribute>
						<xsl:attribute name="onclick">stopevent(event);showAuditor("<xsl:value-of select="//id/@val"/>")
						</xsl:attribute>
					</xsl:if>
					<span style="text-decoration:none;">
						<xsl:value-of select="@p_val"/>
					</span>
				</td>
			</tr>
		</xsl:for-each>
	</table>
	<script language="javascript" type="text/javascript">
		<![CDATA[ 
		function showAuditor(id){
		var d;
		if(!getObj("auddiv")){

		d=document.createElement("div");
		d.id="auddiv";
		d.style.height="320px";
		d.className="infodiv"
		var boundes=$("#ahref").position();

		d.style.top=(boundes.top + 80 + $("#ahref").height()) + "px";
		d.style.left=(boundes.left +260)+ "px";
		d.style.width="540px";
		document.body.appendChild(d);
		}else{
		document.body.removeChild(getObj("auddiv"));
		}
		if(getObj("auddiv")){
		$.post("/Message/AuditorClients",{"id" : id},
		function(data){
    show_dialog({ "content": data, "is_print": true });
		d.innerHTML=data;
		$("html").click(closeAuds);
		})
		}

		}
		function closeAuds(){
	    if(getObj("auddiv")){
        document.body.removeChild(getObj("auddiv"));
        $("html").unbind();
        
    }
}
					]]>
	</script>
	</xsl:template>
</xsl:stylesheet>
