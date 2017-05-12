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
		<xsl:if test="//@PDF=-1">
     
		<form id="frm">
		Период:
            <select id="per_sel" class="system_form" onchange="doSelect()">
              <xsl:for-each select="per">
                <option>
                  <xsl:attribute name="value">
                    <xsl:value-of select="@yq"/>
                  </xsl:attribute>
                  <xsl:if test="@sel=1">
                    <xsl:attribute name="selected">selected</xsl:attribute>
                  </xsl:if>
                  <xsl:value-of select="@name"/>
                </option>
              </xsl:for-each>
            
            </select>


					
			
			<input type="hidden" id="ds_date"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:value-of select="@sname"/>,</xsl:for-each></xsl:attribute></input>
			<input type="hidden" id="ds_name"><xsl:attribute name="value"><xsl:for-each select="per"><xsl:value-of select="@name"/>,</xsl:for-each></xsl:attribute></input>
			<input type="hidden" id="fn"><xsl:attribute name="value"><xsl:value-of select="//ed/@val"/></xsl:attribute></input>
			<input type="hidden" id="iss"><xsl:attribute name="value"><xsl:value-of select="//@iss"/></xsl:attribute></input>
      <input type="hidden" id="tab_id">
        <xsl:attribute name="value">
          <xsl:value-of select="//@id"/>
        </xsl:attribute>
      </input>
		</form>
		</xsl:if>
		<xsl:if test="//@PDF=0">
			Период: <xsl:for-each select="per">
				<xsl:if test="@sel=1">
					<xsl:value-of select="@name"/>
				</xsl:if>
			</xsl:for-each><br/>
			
		</xsl:if>
		<xsl:for-each select="f">
			<span class="subcaption">
				<xsl:choose>
					<xsl:when test="@form_no=1">
						БУХГАЛТЕРСКИЙ БАЛАНС
					</xsl:when>
					<xsl:when test="@form_no=2">
						ОТЧЕТ О ПРИБЫЛЯХ И УБЫТКАХ
					</xsl:when>

				</xsl:choose>
			</span>

			<xsl:choose>
				<xsl:when test="@form_no=1">
					<font class="data_comment">* Для расчета используется значение курса ЦБ на последнюю дату отчетного периода</font>
					<br/>
					<b>
            <xsl:if  test="//cons/@ext_data=1">Consolidated </xsl:if>Balance sheet</b>

				</xsl:when>
				<xsl:when test="@form_no=2">
					<b>
            <xsl:if  test="//cons/@ext_data=1">Consolidated </xsl:if> Statements of Income</b>

				</xsl:when>
				<xsl:when test="@form_no=3">
					<b>Cash and cash equivalents</b>

				</xsl:when>
			</xsl:choose>
			<table width="100%" cellpadding="0" cellspacing="0" border="0">
				<xsl:for-each select="LA">
					<xsl:if test="position()=1">
						<tr>
							<td class="table_caption_left">Наименование показателя</td>
							
							<xsl:for-each select="cols">
								<td class="table_caption" align="center">
									<xsl:value-of select="@name"/>
									
								</td>
							</xsl:for-each>
						</tr>
						

					</xsl:if>
					<xsl:if test="../@form_no=1 and @line_code=10">
						<tr>
							<td>
								<b>ASSETS</b>
							</td>
							<xsl:for-each select="dta">
								<td>
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
							</xsl:for-each>
						</tr>
						<tr>
							<td>
								<b>NON-CURRENT ASSETS</b>
							</td>
							<xsl:for-each select="dta">
								<td>
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
							</xsl:for-each>
						</tr>

						
					</xsl:if>
					<xsl:if test="../@form_no=1 and @line_code=80">
						<tr>
							<td>
								<b>EQUITY &amp; LIABILITIES</b>
							</td>
							<xsl:for-each select="dta">
								<td>
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
							</xsl:for-each>
						</tr>
						<tr>
							<td>
								<b>LIABILITIES</b>
							</td>
							<xsl:for-each select="dta">
								<td>
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
							</xsl:for-each>
						</tr>
					</xsl:if>
					
					<xsl:if test="../@form_no=1 and @line_code=40">
						<tr>
							<td>
								<b>CURRENT ASSETS</b>
							</td>
							<xsl:for-each select="dta">
								<td>
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
							</xsl:for-each>
						</tr>
					</xsl:if>
					<xsl:if test="../@form_no=1 and @line_code=110">
							<tr>
								<td>
									<b>EQUITY</b>
								</td>
								<xsl:for-each select="dta">
									<td>
										<img src="/images/null.gif" width="1" height="1" border="0"/>
									</td>
								</xsl:for-each>
							</tr>
						</xsl:if>


						<tr>
								<xsl:choose>
									<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">
										<xsl:attribute name="bgColor">#F0F0F0</xsl:attribute>
									</xsl:when>
									<xsl:otherwise>
										<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
									</xsl:otherwise>
								</xsl:choose>
								<td class="table_item_left" width="300">
										<xsl:value-of disable-output-escaping ="yes" select="@name"/>
								</td>
								<xsl:for-each select="dta">
									<td style="text-align:right;">
										<xsl:choose>
											<xsl:when test ="@val=-0.001">-</xsl:when>
											<xsl:otherwise>
												<xsl:value-of select="format-number(@val,'# ##0,00','buh')"/>
											</xsl:otherwise>
										</xsl:choose>
									</td>
								</xsl:for-each>
							</tr>
						</xsl:for-each>

			</table>
			<br/>
			<br/>
		</xsl:for-each>
		<xsl:for-each select="gaap">
			<span class="subcaption">
				<xsl:choose>
					<xsl:when test="@form_no=1">
						БУХГАЛТЕРСКИЙ БАЛАНС
					</xsl:when>
					<xsl:when test="@form_no=2">
						ОТЧЕТ О ПРИБЫЛЯХ И УБЫТКАХ
					</xsl:when>
					<xsl:when test="@form_no=3">
						ОТЧЕТ О ДВИЖЕНИИ КАПИТАЛА
					</xsl:when>
					
				</xsl:choose>
			</span>
			<xsl:choose>
				<xsl:when test="@form_no=1">

					<br/><b>
            <xsl:if  test="//cons/@ext_data=1">Consolidated </xsl:if> Balance Sheet</b>

				</xsl:when>
				<xsl:when test="@form_no=2">
					<br/><b>
            <xsl:if  test="//cons/@ext_data=1">Consolidated </xsl:if> Statements of Income</b>

				</xsl:when>
				<xsl:when test="@form_no=3">
					<br/><b>Cash and cash equivalents</b>

				</xsl:when>
			</xsl:choose>
			<table width="100%" cellpadding="0" cellspacing="0" border="0">
				<xsl:for-each select="LA">
					<xsl:if test="position()=1">
						<tr>
							<td class="table_caption_left">Наименование показателя</td>

							<xsl:for-each select="cols">
								<td class="table_caption" align="center" width="250">
									<xsl:value-of select="@dname"/>

								</td>
							</xsl:for-each>
						</tr>
						

					</xsl:if>
					<xsl:if test="../@form_no=1 and @line_code=10">
						<tr>
							<td>
								<b>ASSETS</b>
							</td>
							<xsl:for-each select="cols">
								<td>
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
							</xsl:for-each>
						</tr>
						<tr>
							<td>
								<b>NON-CURRENT ASSETS</b>
							</td>
							<xsl:for-each select="cols">
								<td>
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
							</xsl:for-each>
						</tr>


					</xsl:if>
					<xsl:if test="../@form_no=1 and @line_code=80">
						<tr>
							<td>
								<b>EQUITY &amp; LIABILITIES</b>
							</td>
							<xsl:for-each select="cols">
								<td>
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
							</xsl:for-each>
						</tr>
						<tr>
							<td>
                                <b>EQUITY</b>
							</td>
							<xsl:for-each select="cols">
								<td>
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
							</xsl:for-each>
						</tr>
					</xsl:if>

					<xsl:if test="../@form_no=1 and @line_code=40">
						<tr>
							<td>
								<b>CURRENT ASSETS</b>
							</td>
							<xsl:for-each select="cols">
								<td>
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
							</xsl:for-each>
						</tr>
					</xsl:if>
					<xsl:if test="../@form_no=1 and @line_code=110">
						<tr>
							<td>
                                <b>LIABILITIES</b>
							</td>
							<xsl:for-each select="cols">
								<td>
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
							</xsl:for-each>
						</tr>
					</xsl:if>


					<tr>
						<xsl:choose>
							<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">
								<xsl:attribute name="bgColor">#F0F0F0</xsl:attribute>
							</xsl:when>
							<xsl:otherwise>
								<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
							</xsl:otherwise>
						</xsl:choose>
						<td class="table_item_left" width="300">
							<xsl:value-of disable-output-escaping ="yes" select="@name"/>
						</td>
						<xsl:for-each select="cols">
							<td style="text-align:right;">
								<xsl:choose>
									<xsl:when test ="@val=-0.001">-</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="format-number(@val,'# ##0,00','buh')"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
						</xsl:for-each>
					</tr>
				</xsl:for-each>

			</table>
		</xsl:for-each>
    <span style="display:none;" id="indata">
      <xsl:attribute name="iss">
        <xsl:value-of select="//indata/@iss"/>
      </xsl:attribute>
      <xsl:attribute name="per">
        <xsl:value-of select="//indata/@per"/>
      </xsl:attribute>

    </span>
		<span class="data_comment limitation">
			ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и могут не отражать абсолютную актуальность и достоверность информации. 
		</span>




			<script language="javascript" type="text/javascript">
		<![CDATA[ 
   $('#xls_btn').unbind('click').addClass('disabled');
               set_xls_function(function () {
                if (!getObj("reports")) {
                ifr = document.createElement("iframe");
                ifr.className = "service_frame"
                ifr.name = "reports"
                ifr.id = "reports"
                ifr.src = "about:blank";
                document.body.appendChild(ifr);
            } else {
                ifr = getObj("reports")
            }
            iframepost({"id": $("#tab_id").val(), 
                        "ticker":$("#iss").val(),
                        "per" :$("#per_sel").val(),
                        "fn" : $("#fn").val(), 
                        "xls" : "1","cons" : $("#fn").val()}, "/Tab/", "reports");
        });
			
   
   
    function doSelect(){
   	  showClock();
         $("#tab_content").load("/Tab/?id=" + $("#tab_id").val() + 
                                          "&ticker=" + $("#iss").val() + 
                                          "&per=" + $("#per_sel").val(),
        function (data) {
                hideClock();
        });
	 }
	
				]]>
</script>
</xsl:template>
</xsl:stylesheet>
