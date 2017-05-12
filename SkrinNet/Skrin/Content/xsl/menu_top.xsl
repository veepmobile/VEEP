<?xml version="1.0" encoding="windows-1251" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html" version="4.0" encoding="utf-8"/>

	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="profile">
    <xsl:for-each select="l">
      <xsl:if test="@no=1">
        <xsl:for-each select="level/a">
          <xsl:choose>
            <xsl:when test="@sel=1">
              <a href="#" class="selected">
                <xsl:value-of select="@name" disable-output-escaping="yes"/>
              </a>
            </xsl:when>
            <xsl:otherwise>
              <a>
                <xsl:choose>
                  <xsl:when test="@need_login=1 and //@u_id=0">
                    <xsl:attribute name="onclick">need_login();return false;</xsl:attribute>
                    <xsl:attribute name="href">#</xsl:attribute>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:attribute name="href">
                      <xsl:value-of select="@href"/>
                    </xsl:attribute>
                  </xsl:otherwise>
                </xsl:choose>
                <xsl:value-of select="@name" disable-output-escaping="yes"/>
              </a>
            </xsl:otherwise>
          </xsl:choose>
        </xsl:for-each>
      </xsl:if>
    </xsl:for-each>
	</xsl:template>
</xsl:stylesheet>