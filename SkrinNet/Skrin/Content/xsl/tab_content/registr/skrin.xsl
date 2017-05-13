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
			<input type="hidden" id="iss"><xsl:attribute name="value"><xsl:value-of select="//@iss"/></xsl:attribute></input>
			<span class="subcaption">Сведения об организациях, осуществляющих учёт прав на акции</span>
			<br/>
			Данные по состоянию на: <xsl:value-of select="//dt/@val"/>
			<table width="100%">
				
					<tr>
						<td class="table_caption" style="width:150px;">Период</td>
						<td class="table_caption">Наименование</td>
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
					</tr>
				<xsl:for-each select="RH">
					<tr>
						<td valign="top">
							<xsl:value-of select="@dt"/>
						</td>
						<td>
							
							<xsl:attribute name="id">i<xsl:value-of select="position()"/></xsl:attribute>
							<xsl:value-of select="@name"/>
							<span style="cursor:pointer;color:#003399;text-decoration:underline;;padding-left:5px;">
								<xsl:attribute name="onclick">showRHInfo("<xsl:value-of select="@id"/>","i<xsl:value-of select="position()"/>")</xsl:attribute>
								<xsl:attribute name="id">si<xsl:value-of select="position()"/></xsl:attribute>
								Подробнее
							</span>
							<img src="/images/tra_e.png" alt="" style="padding-left:3px">
								<xsl:attribute name="id">imgi<xsl:value-of select ="position()"/></xsl:attribute>
							</img>
						</td>
					</tr>
				</xsl:for-each>
			</table>
			<span class="data_comment limitation">
				ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и могут не отражать абсолютную актуальность и достоверность информации. Дополнительную информацию можно получить в разделе
				<a onclick="gotomenu(48);" href="#">«Квартальные отчеты»</a> или <a onclick="gotomenu(32);" href="#">«Существенные факты»</a>
			</span>
		</div>
		<script language="javascript" type="text/javascript">
			<![CDATA[ 
			var prev_node=-1;
			xls_params={"iss":$("#iss").val(),"module" : "registr/skrin/","x":Math.random()}
	
			function showRHInfo(id,did){
				
				var td=getObj(did);
				if(getObj("RH_div" + did)){
					getObj(did).removeChild(getObj("RH_div" + did));
					$("#s" + did).html("Подробнее");
					$("#s" + did).attr("val",0)
					$("#img" + did).attr("src","/images/tra_e.png")
				}else{
					showClock();
					prev_node=did;
					div=document.createElement("div");
					div.className="detaildiv"
					div.id="RH_div" + did;
					td.appendChild(div);
					$("#RH_div" + did).load("/Message/RegisterSkrin",{ticker:ISS,id:id},function(data){hideClock();});
					$("#RH_div" + did).click(stopevent);
					$("#s" + did).html("Свернуть");
					$("#s" + did).attr("val",1)
					$("#img" + did).attr("src","/images/tra_w.png")
				}
			}
			]]>
		</script>
	</xsl:template>
</xsl:stylesheet>
