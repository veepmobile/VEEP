<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user" xmlns:js="javascript:code">
	<xsl:import href="../../../xsl/common.xsl"/>
	<msxsl:script language="JScript" implements-prefix="js">
		function GetMult(nl){
    var val=nl;
		var retval="0";
		var mult,exp;
		if(val.indexOf("E") &lt; 0){
    retval=val;
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
	<xsl:decimal-format name="buh" decimal-separator="." grouping-separator=" " NaN="#"/>

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
	

		<xsl:if test="gks[position()=last()]">
			<span class="subcaption">Размер уставного капитала</span>
			
			
			<table width="500px" cellpadding="0" cellspacing="0" >
			<tr>
				<td class="table_caption">Дата</td>
				<td class="table_caption">Наименование показателя</td>
				<td class="table_caption">Значение, руб.</td>
				
			</tr>
			<tr>
				<td class="table_shadow">
					<div style="width: 1px; height: 1px;">
						<spacer type="block" width="1px" height="1px" />
					</div>
				</td>
				<td class="table_shadow">
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
				<xsl:if test="count(//gks)>1 and //@PDF=-1">
				<tr>
					<td colspan="3" style="text-align:right;">
						<span val="0" onclick="switch_vis();" style="cursor:pointer;color:#003399;" id="switcher">
							Показать архив
						</span>
						<img src="/images/tra_e.png" alt="" style="padding-left:3px">
							<xsl:attribute name="id">img</xsl:attribute>
						</img>
					</td>
				</tr>
				</xsl:if>
				<xsl:for-each select="gks">
				<tr><xsl:attribute name="bgcolor"><xsl:call-template name="set_bg"><xsl:with-param name="str_num" select="position()+1"/></xsl:call-template></xsl:attribute><xsl:attribute name="class"><xsl:if test="@act=0">trclosed</xsl:if></xsl:attribute>
					<td valign="top">
						<xsl:value-of select="@rd"/>		
					</td>
					<td valign="top">
						Размер уставного капитала
					</td>
					<td valign="top" align="right">
						<xsl:value-of select="format-number(js:GetMult(string(@capital)),'# ##0.#######','buh')"/>
					</td>
					
				</tr>
			</xsl:for-each>
				
		</table>
		<input type="hidden" id="iss"><xsl:attribute name="value"><xsl:value-of select="//@iss"/></xsl:attribute></input>
			<span class="data_comment limitation">
				ВНИМАНИЕ: в связи с особенностями функционирования и обновления указанного источника информации АО "СКРИН" не может гарантировать абсолютную актуальность и достоверность данных.
			</span>
		</xsl:if>
		<script language="javascript" type="text/javascript">
			<![CDATA[ 
			xls_params={"iss":$("#iss").val(),"module" : "action/gmc/","x":Math.random()};
			var trclasses=new Array("trclosed","trshow")
			function switch_vis(){
				var newClass=trclasses[Math.abs($("#switcher").attr("val")-1)];
				
				$("."+trclasses[$("#switcher").attr("val")]).each(function(i){
					this.className=newClass;
		        });
				$("#switcher").attr("val",Math.abs($("#switcher").attr("val")-1));
				$("#switcher").html((($("#switcher").attr("val")=="1")? "Скрыть архив":"Показать архив"));
				$("#img").attr("src",(($("#switcher").attr("val")=="1")? "/images/tra_w.png":"/images/tra_e.png"));

				
			}
			]]>
</script>
</xsl:template>
</xsl:stylesheet>
