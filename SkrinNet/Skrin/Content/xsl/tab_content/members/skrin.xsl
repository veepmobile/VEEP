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
    if(val.indexOf("e") &lt; 0){
    retval=val;
    }else{
    mult=val.substring(0,val.indexOf("e"));
    exp=val.substring(val.indexOf("e")+1,val.length);
    exp=Math.pow(10,exp);
    retval=mult*exp;
    }
    return retval;
    }
  </msxsl:script>  
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
		<xsl:if test="//@PDF=-1">
			Период: <select name="period" id="period" class="system_form" onchange="goPeriod(this.value)">
				<xsl:for-each select="periods">
					<option>
						<xsl:attribute name="value">
							<xsl:value-of select="@yq"/>
						</xsl:attribute>
						<xsl:if test="@curr=1">
							<xsl:attribute name="selected">selected</xsl:attribute>
						</xsl:if>
						<xsl:value-of select="@quarter"/>-й кв. <xsl:value-of select="@year"/> г.
					</option>
				</xsl:for-each>
			</select>
		</xsl:if>
		<xsl:if test="//@PDF=0">
			Период:
			<xsl:for-each select="periods">
				<xsl:if test="@curr=1">
					<xsl:value-of select="@quarter"/>-й кв. <xsl:value-of select="@year"/> г.
				</xsl:if>
			</xsl:for-each>
		</xsl:if>
        <input type="hidden" id="iss">
      <xsl:attribute name="value">
        <xsl:value-of select="//@iss"/>
      </xsl:attribute>
    </input>
		<br/><br/>
		<xsl:if test="issuer_shareholders[position()=1]">
			<span class="subcaption">
				Сведения об общем количестве акционеров(участников)
		</span><br/>
			<table cellpadding="0" cellspacing="0">
				<tr>
					<td class="table_caption" style="width:350px;">Наименование показателя</td>
					<td class="table_caption" style="width:95px;">Значение</td>
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
				<tr>
					<td>Общее  количество акционеров(участников)</td>
					<td>
						<xsl:value-of select="issuer_shareholders/@shareholders"/>
					</td>
				</tr>
			</table>
			<br/>
		</xsl:if>
		<input type="hidden" id="iss"><xsl:attribute name="value"><xsl:value-of select="//@iss"/></xsl:attribute></input>
		<input type="hidden" id="per"><xsl:attribute name="value"><xsl:for-each select="periods"><xsl:if test="@curr=1"><xsl:value-of select="@yq"/></xsl:if></xsl:for-each></xsl:attribute></input>

		<span class="subcaption">Сведения об учредителях/участниках</span><br/>
		<table width="98%" cellpadding="0" cellspacing="0">
			<tr>
				<td class="table_caption" style="width:250px;">Наименование учредителя или участника</td>
				<td class="table_caption" style="width:75px;">ИНН</td>
				<td class="table_caption" style="width:250px;">Адрес места нахождения</td>
				<td class="table_caption" style="width:75px;">Вид зарегистрированного лица</td>
				<td class="table_caption" style="width:75px;">Доля в уставном капитале,%</td>
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
			<xsl:for-each select="part">
				<tr><xsl:attribute name="bgcolor"><xsl:call-template name="set_bg"><xsl:with-param name="str_num" select="position()"/></xsl:call-template></xsl:attribute>
					<td>
						<xsl:choose>
							<xsl:when test="string-length(us/@ticker)>0">
								<a target="_blank">
									<xsl:attribute name="href">/issuers/<xsl:value-of select="us/@ticker"/>/</xsl:attribute>
									<xsl:value-of select="@name"/>
								</a>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="@name"/>		
							</xsl:otherwise>
						</xsl:choose>
					</td>
					<td align="center" style="width:75px;">
						<xsl:value-of select="@inn"/>
					</td>
					<td>
						<xsl:value-of select="@address"/>
					</td>
					<td align="center">
						<xsl:value-of select="@is_owner"/>
					</td>
					<td align="right" style="width:75px;" nowrap="nowrap">
						<xsl:value-of select="format-number(js:GetMult(string(@rev_value)),'# ##0,#######','buh')"/>
						<!--<xsl:value-of select="format-number(@rev_value,'# ##0,###########','buh')"/>-->
					</td>
				</tr>
			</xsl:for-each>
		</table>
		<xsl:if test="golden_shares[position()=1]">
			<br/><b>Специальное право  ("золотая акция")</b>
			<br/>
			<xsl:value-of disable-output-escaping="yes" select="golden_shares/@comment"/>
		</xsl:if>
		
		<span class="data_comment limitation">
			ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и могут не отражать абсолютную актуальность и достоверность информации. Дополнительную информацию можно получить в разделе
			<a onclick="gotomenu(48);" href="#">«Квартальные отчеты»</a> или <a onclick="gotomenu(32);" href="#">«Существенные факты»</a>,  <a onclick="gotomenu(28);" href="#">«Списки аффилированных лиц»</a>
		</span>
		<script type="text/javascript"  language="javascript" >
			<![CDATA[ 
				xls_params={"period": $("#per").val(),"iss":$("#iss").val(),"module" : "members/skrin/","x":Math.random()}
        
          function goPeriod(period){
        showClock();
        $("#tab_content").load("/Tab/Index/?id=23&ticker=]]><xsl:value-of select="//@iss"/><![CDATA[&src=0&period=" + period, 
            function(data){
                hideClock();
            }
        );
        }
			]]>
		</script>
	</xsl:template>
</xsl:stylesheet>
