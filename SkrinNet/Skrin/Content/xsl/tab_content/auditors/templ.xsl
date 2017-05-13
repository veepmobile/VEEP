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
		<div id="auds">
			<input type="hidden" id="iss"><xsl:attribute name="value"><xsl:value-of select="//@iss"/></xsl:attribute></input>
			<input type="hidden" id="st"><xsl:attribute name="value"><xsl:value-of select="//@st"/></xsl:attribute></input>
			<br/>
			<xsl:value-of select="//dtaud/@dt"/>
			<table width="100%">
				<tr>
					<td width="120px" class="table_caption_left">Отчетный период</td>
					<td class="table_caption_left">Наименование</td>
					
				</tr>
				<tr>
					<td class="table_shadow_left">
						<div style="width: 1px; height: 1px;">
							<spacer type="block" width="1px" height="1px" />
						</div>
					</td>
					<td class="table_shadow">
						<div style="width: 1px; height: 1px;">
							<spacer type="block" style="width: 1px; height: 1px;" />
						</div>
					</td>
				</tr>
				<xsl:for-each select="aud">
					<tr><xsl:attribute name="bgcolor"><xsl:call-template name="set_bg"><xsl:with-param name="str_num" select="position()"/></xsl:call-template></xsl:attribute>
						<td style="width:120px;" class="table_item_left">
							<xsl:value-of select="@year"/>
						</td>
						<td><xsl:attribute name="id">i<xsl:value-of select="position()"/></xsl:attribute>
							<xsl:value-of select="a/@name"/>
              <span style="margin-left:30px;" class="icon-angle-down more_ico_okved" id="aman" >
                <xsl:attribute name="onclick">
                  showAuditorInfo("<xsl:value-of select="a/@id"/>","i<xsl:value-of select="position()"/>")
                </xsl:attribute>
                <xsl:attribute name="id">si<xsl:value-of select="position()"/></xsl:attribute>Подробнее</span>
							<!--<span style="cursor:pointer;color:#003399;text-decoration:underline;padding-left:5px;">
							<xsl:attribute name="onclick">showAuditorInfo("<xsl:value-of select="a/@id"/>","i<xsl:value-of select="position()"/>")</xsl:attribute>
							<xsl:attribute name="id">si<xsl:value-of select="position()"/></xsl:attribute>Подробнее</span>
              <img src="/images/tra_e.png" alt="" style="padding-left:3px">
							<xsl:attribute name="id">imgi<xsl:value-of select ="position()"/></xsl:attribute>
							</img>-->
						</td>

					</tr>
				</xsl:for-each>
			</table>
		
			<span class="data_comment limitation">
        ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и могут не отражать абсолютную актуальность и достоверность информации. Дополнительную информацию можно получить в разделе <a onclick="gotomenu(48);" href="#">«Квартальные отчеты»</a> или <a onclick="gotomenu(32);" href="#">«Существенные факты»</a>
          <!--<a onclick="setclass(42,11);tab_def=0;" href="#">«Квартальные отчеты»</a> или <a onclick="setclass(19,6);tab_def=20;" href="#">«Существенные факты»</a>-->
			</span>
		</div>
		<script language="javascript" type="text/javascript">
			<![CDATA[ 
			var prev_node=-1;
			xls_params={"iss":$("#iss").val(),"module" : "auditor/" + (($("#st").val()*1==0)? "rsbu" : "msfo") +  "/","x":Math.random()}
	
			function showAuditorInfo(id,did){
				
				var td=getObj(did);
				if(getObj("aud_div" + did)){
					getObj(did).removeChild(getObj("aud_div" + did));
					$("#s" + did).html("Подробнее");
					$("#s" + did).attr("val",0)
					$("#img" + did).attr("src","/images/tra_e.png")
          	$("span#s" + did).attr("class","more_ico_okved icon-angle-down");
				}else{
					showClock();
					prev_node=did;
					div=document.createElement("div");
					div.className="detaildiv"
					div.id="aud_div" + did;
					td.appendChild(div);
					//$("#aud_div" + did).load("/iss/content/auditor/auditor.asp",{id:id},function(data){hideClock();});
          $("#aud_div" + did).load("/Message/AuditorsEvent",{ticker:ISS,aud_id:id},function(data){hideClock();});
					$("#aud_div" + did).click(stopevent);      
					$("span#s" + did).html("Свернуть");
					$("#s" + did).attr("val",1)
         	$("span#s" + did).attr("class","more_ico_okved icon-angle-up");
					$("#img" + did).attr("src","/images/tra_w.png")					
				}
			}
			]]>
		</script>
	</xsl:template>
</xsl:stylesheet>
