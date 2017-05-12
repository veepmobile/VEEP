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
      Период: <select name="period" id="period" class="system_form" onchange="goPeriod(this.value)">
        <xsl:for-each select="periods">
          <option>
            <xsl:attribute name="value">
              <xsl:value-of select="@dt"/>
            </xsl:attribute>
            <xsl:if test="@curr=1">
              <xsl:attribute name="selected">selected</xsl:attribute>
            </xsl:if>
            <xsl:value-of select="@dt"/>
          </option>
        </xsl:for-each>
      </select><br/><br/>
    </xsl:if>

    <xsl:if test="//@PDF=0">
      Период: <xsl:for-each select="periods">
        <xsl:if test="@curr=1">
          <xsl:value-of select="@dt"/>
          <br/>
          <br/>
        </xsl:if>
      </xsl:for-each>
    </xsl:if>
    <input type="hidden" id="iss">
      <xsl:attribute name="value">
        <xsl:value-of select="//@iss"/>
      </xsl:attribute>
    </input>
    <input type="hidden" id="per">
      <xsl:attribute name="value">
        <xsl:for-each select="periods">
          <xsl:if test="@curr=1">
            <xsl:value-of select="@dt"/>
          </xsl:if>
        </xsl:for-each>
      </xsl:attribute>
    </input>

    <span class="subcaption">Лицо, исполняющее функции единоличного исполнительного органа</span>
    <table width="98%" cellpadding="0" cellspacing="0">
      <tr>
        <td class="table_caption" style="width:50%">Ф.И.О</td>
        <td class="table_caption" style="width:50%">Должность занимаемая лицом</td>
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
      <xsl:for-each select="gks">
        <tr>
          <xsl:attribute name="bgcolor">
            <xsl:call-template name="set_bg">
              <xsl:with-param name="str_num" select="position()"/>
            </xsl:call-template>
          </xsl:attribute>
          <td>
            <a>
              <xsl:attribute name="href">
                  javascript:openProfileFL('<xsl:value-of select="@manager_name"/>','')
              </xsl:attribute>
              <xsl:value-of select="@manager_name"/>
            </a>
          </td>
          <td>
            <xsl:value-of select="@manager_position"/>
          </td>
        </tr>
      </xsl:for-each>
    </table>
    <span class="data_comment limitation">
      ВНИМАНИЕ: в связи с особенностями функционирования и обновления указанного источника информации АО "СКРИН" не может гарантировать абсолютную актуальность и достоверность данных.
    </span>

    <script language="javascript" type="text/javascript">
      <![CDATA[ 
			xls_params={"period": $("#per").val(),"iss":$("#iss").val(),"module" : "isporg/gmc/","x":Math.random()}
      
      function goPeriod(period){
        showClock();
        $("#tab_content").load("/Tab/Index/?id=16&ticker=]]><xsl:value-of select="//@iss"/><![CDATA[&period=" + period, 
            function(data){
                hideClock();
            }
        );
       
}
			]]>
    </script>

    <input id="tabId" type="hidden" value="16" />
  </xsl:template>
</xsl:stylesheet>
