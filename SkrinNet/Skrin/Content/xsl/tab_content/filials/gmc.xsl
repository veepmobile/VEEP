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
    <input type="hidden" id="iss">
      <xsl:attribute name="value">
        <xsl:value-of select="//@iss"/>
      </xsl:attribute>
    </input>
    <span class="subcaption">Филиалы и представительства</span>
    <table width="98%" cellpadding="2px" cellspacing="0">

      <tr>
        <td class="table_caption" style="width:40%;">Наименование </td>
        <td class="table_caption" style="width:40%;">Адрес места нахождения</td>
        <td class="table_caption" style="width:20%;">Руководитель</td>
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
      <xsl:for-each select="gi">
        <tr>
          <xsl:attribute name="bgcolor">
            <xsl:call-template name="set_bg">
              <xsl:with-param name="str_num" select="position()"/>
            </xsl:call-template>
          </xsl:attribute>
          <td>
            <a target="_blank">
									<xsl:attribute name="href">/issuers/<xsl:value-of select="us/@ticker"/>/</xsl:attribute>
									<xsl:value-of select="@name"/>
						</a>
          </td>
          <td>
            <xsl:value-of select="@legal_address"/>
          </td>
          <td>
            <xsl:value-of select="@manager_name"/>
          </td>
        </tr>
      </xsl:for-each>
    </table>

    <span class="data_comment limitation">
      ВНИМАНИЕ: Сведения из раздела являются результатом обработки данных, предоставленных в ГМЦ РОССТАТа структурными подразделениями, указанными в настоящем разделе. В связи с особенностями обработки информации АО«СКРИН» не может гарантировать актуальность и достоверность сведений на текущую дату.
    </span>
    <script type="text/javascript"  language="javascript" >
      <![CDATA[ 
				xls_params={"iss":$("#iss").val(),"module" : "filials/gmc/","x":Math.random()}
        
      function goPeriod(period){
        showClock();
        $("#tab_content").load("/Tab/Index/?id=29&ticker=]]><xsl:value-of select="//@iss"/><![CDATA[&period=" + period, 
            function(data){
                hideClock();
            }
        );
      }     
      
			]]>
    </script>
  </xsl:template>
</xsl:stylesheet>
