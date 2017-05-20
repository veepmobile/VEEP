<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="urn:deitel:user">
  <xsl:output method="html" version="4.0" encoding="utf-8"/>
  <xsl:template match="/">
    <xsl:apply-templates/>
  </xsl:template>
  <xsl:decimal-format name="buh" decimal-separator="." grouping-separator=" " NaN="-"/>

  <xsl:template match="/iss_profile">
    <!-- content -->
    <xsl:apply-templates select="dates">
    </xsl:apply-templates>
    <!-- end content -->
  </xsl:template>
  <xsl:template match="dates">
    <div class="filter_form_horisontal">
      <div class="filter_block">
        <div class="form-group">
          <label>Год</label>
          <select name="cyear" id="cyear" class="form-control" style="width:100px">
            <xsl:for-each select="years/val">
              <option>
                <xsl:if test="@sel=1">
                  <xsl:attribute name="selected">selected</xsl:attribute>
                </xsl:if>
                <xsl:attribute name="value">
                  <xsl:value-of select="@y"/>
                </xsl:attribute>
                <xsl:value-of select="@y"/>
              </option>
            </xsl:for-each>
          </select>
        </div>
      </div>
      <div id="extra_search">
      <div class="filter_block">
        <div class="form-group">
          <label>Месяц</label>
          <select name="cmonth" id="cmonth" class="form-control" style="width:180px">
            <xsl:for-each select="months/val">
              <option>
                <xsl:if test="@sel=1">
                  <xsl:attribute name="selected">selected</xsl:attribute>
                </xsl:if>
                <xsl:attribute name="value">
                  <xsl:value-of select="@m"/>
                </xsl:attribute>
                <xsl:choose>
                  <xsl:when test="@m=0">Выберите месяц</xsl:when>
                  <xsl:when test="@m=1">январь</xsl:when>
                  <xsl:when test="@m=2">февраль</xsl:when>
                  <xsl:when test="@m=3">март</xsl:when>
                  <xsl:when test="@m=4">апрель</xsl:when>
                  <xsl:when test="@m=5">май</xsl:when>
                  <xsl:when test="@m=6">июнь</xsl:when>
                  <xsl:when test="@m=7">июль</xsl:when>
                  <xsl:when test="@m=8">август</xsl:when>
                  <xsl:when test="@m=9">сентябрь</xsl:when>
                  <xsl:when test="@m=10">октябрь</xsl:when>
                  <xsl:when test="@m=11">ноябрь</xsl:when>
                  <xsl:when test="@m=12">декабрь</xsl:when>
                </xsl:choose>
              </option>
            </xsl:for-each>
          </select>
        </div>
      </div>
      <div class="filter_block">
        <div class="form-group">
          <label>Наименование органа гос. контроля</label>
          <select name="organ" id="organ" class="form-control" style="width:300px">
            <option value="">Все</option>
            <xsl:for-each select="genproc/organ/item">
              <option>
                <xsl:attribute name="value">
                  <xsl:value-of select="@name"/>
                </xsl:attribute>
                <xsl:value-of select="@name"/>
              </option>
            </xsl:for-each>
          </select>
        </div>
      </div>
      </div>
      <div class="filter_block button_block">
        <div class="form-group">
          <input type="button" value="Найти" class="btns darkblue" id="btn_find"></input>
        </div>
      </div>
    </div>
    <div id="search_count" style="margin-top:6px;">
      <div class="minicaption">
        <xsl:value-of disable-output-escaping="yes" select='genproc/data/@warning'/>
      </div>
    </div>
    <xsl:if test='genproc/data/item[position()=last()]'>
      <table width="100%" cellspacing="0" cellpadding="0" border="0" id="search_result">
        <tr>
          <xsl:choose>
            <xsl:when test="genproc/year &gt; 2012">
              <td class="table_caption">Месяц даты начала проведения проверки</td>
              <td class="table_caption">Место нахождения объекта</td>
              <td class="table_caption">Цель проведения проверки</td>
              <td class="table_caption">Наименование органа государственного контроля</td>
            </xsl:when>
            <xsl:otherwise>
              <td class="table_caption">Дата и срок проведения плановой проверки</td>
              <td class="table_caption">Наименование органа государственного контроля</td>
            </xsl:otherwise>
          </xsl:choose>
        </tr>
        <xsl:choose>
          <xsl:when test="genproc/year &gt; 2012">
            <xsl:for-each select="genproc/data/item">
              <xsl:if test="@hid!=0">
                <tr>
                  <td>
                    <a style='cursor:pointer;'>
                      <xsl:attribute name='onclick'>
                        ShowCase(<xsl:value-of select='@hid'/>,'<xsl:value-of select='@horgan'/>')
                      </xsl:attribute>
                      <xsl:value-of select='@dt'/>
                    </a>
                  </td>
                  <td>
                    <xsl:value-of select='@address'/>
                  </td>
                  <td>
                    <xsl:value-of select='@purpose'/>
                  </td>
                  <td>
                    <xsl:value-of select='@organ'/>
                  </td>
                </tr>
              </xsl:if>
            </xsl:for-each>
          </xsl:when>
          <xsl:otherwise>
            <xsl:for-each select="genproc/data/item">
              <xsl:if test="@hid!=0">
                <tr>
                  <td>
                    <a style='cursor:pointer;'>
                      <xsl:attribute name='onclick'>
                        ShowCase(<xsl:value-of select='@hid'/>,'<xsl:value-of select='@horgan'/>')
                      </xsl:attribute>
                      <xsl:value-of select='@dt'/>
                    </a>
                  </td>
                  <td>
                    <xsl:value-of select='@organ'/>
                  </td>
                </tr>
              </xsl:if>
            </xsl:for-each>
          </xsl:otherwise>
        </xsl:choose>
      </table>
    </xsl:if>
    <input id="SearchName" type="hidden" name="SearchName">
      <xsl:attribute name="value">
        <xsl:value-of select="@name"/>
      </xsl:attribute>
    </input>
    
    <script language="javascript" type="text/javascript">
      <![CDATA[ 
      /*	$(function() {
        if($("#div_corgan").html().length>0){
          $("#organ").find("option:contains('" + $("#div_corgan").html() + "')").attr("selected", "selected");
          }
        })*/
		]]>
    </script>
  </xsl:template>
</xsl:stylesheet>
