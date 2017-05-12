<?xml version="1.0" encoding="windows-1251" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:output method="html" version="4.0" encoding="utf-8"/>

	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="profile">
    <xsl:for-each select="l">
      <xsl:if test="@no=2">
        <div class="sub_menu">
          <div class="content">
            <ul class="search_tabs">
              <xsl:for-each select="level/a">
                <xsl:choose>
                  <xsl:when test="@sel=1">
                    <li class="active">
                      <span><xsl:value-of select="@name"/></span>
                    </li>
                  </xsl:when>
                  <xsl:otherwise>
                    <li>
                      <span>
                        <a><xsl:attribute name="href"><xsl:value-of select="@href"/></xsl:attribute>
                          <xsl:value-of select="@name"/>
                        </a>
                      </span>                      
                    </li>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:for-each>
            </ul>
          </div>
          <xsl:for-each select="switch/ss">
              <div id="country_selector">
                <span id="country_selector_btn">
                  <xsl:value-of select="@name"/>
                  <span class="icon-angle-down country_chooser"></span>
                </span>
                <ul id="country_selector_menu">
                  <xsl:for-each select="sl">
                    <li>
                      <a>
                        <xsl:attribute name="href">
                          <xsl:value-of select="@href"/>
                        </xsl:attribute>
                        <xsl:value-of select="@sub_name"/>
                      </a>
                    </li>
                  </xsl:for-each>
                </ul>
              </div>
          </xsl:for-each>
        </div>
      </xsl:if>
    </xsl:for-each>
	</xsl:template>
</xsl:stylesheet>