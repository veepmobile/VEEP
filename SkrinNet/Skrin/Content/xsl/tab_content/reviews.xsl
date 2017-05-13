<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 	xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="urn:deitel:user" xmlns:js="javascript:code">
  <xsl:import href="../../xsl/common.xsl"/>
  <msxsl:script language="JScript" implements-prefix="js">
    function GenPages(PCount,Page)
    {
      var html = '&lt;table class="tabPaginator" cellpadding="0" cellspacing="0"  style="border:none;" &gt;&lt;tr&gt; ';
      var StartPage
      if (PCount &lt; 8)
      StartPage = 1
      else
      StartPage = ((Page-3 &gt; 0)? ((PCount-Page &lt; 3)? PCount*1 + (PCount-Page)-8 : Page-3):1);
    if (Page*1 &gt; 3 &amp;&amp; PCount &gt; 7)
      html = html + '&lt;td &gt;&lt;a href="#" onclick="CommonPager(' + (StartPage-1) + ')"&gt;&lt;&lt;&lt;/a&gt;&lt;/td&gt;';
      for (var i = StartPage;  i &lt; ((Page == PCount)? PCount*1+1 : ((StartPage+7 &lt; PCount)? StartPage+7: PCount*1+1)); i++)
      {
      if (i==Page)
      html = html + '&lt;td style="border-left:none;border-top:none;"&gt;'+ i + '-я страница&lt;/td&gt;';
      else
      html = html + '&lt;td style="border-left:none;border-top:none;"&gt;&lt;a href="#" onclick="CommonPager(' + i + ')"&gt;'+ i + '&lt;/a&gt;&lt;/td&gt;';
      }
      if (i &lt; PCount)
      html = html + '&lt;td style="border-left:none;border-top:none;"&gt;&lt;a href="#" onclick="CommonPager(' + (Page*1+4) + ')"&gt;&gt;&gt;&lt;/a&gt;&lt;/td&gt;';
      if (PCount &gt; 7)
      html = html + '&lt;td style="border-left:none;border-top:none;"&gt;(Всего: &lt;a href="#" id="amover" onclick="MoveTo(' + Page + ',' + PCount+')"&gt;' + PCount + ' страниц&lt;/a&gt;)&lt;/td&gt;' + '&lt;/tr&gt;&lt;/table&gt;';
  return html
  }
</msxsl:script>
  
  <xsl:output method="html" version="4.0" encoding="utf-8"/>
  <xsl:template match="/">
    <xsl:apply-templates/>
  </xsl:template>
  <xsl:decimal-format name="buh" decimal-separator="," grouping-separator=" " NaN="-"/>
  <xsl:template match="iss_profile">

    <!-- content -->
    <xsl:if test="profile/PC/@CNT>0">
      <font class="minicaption">
        Всего обзоров: <xsl:value-of select="profile/PC/@CNT"/>.
      </font>
      <br/>
    </xsl:if>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
      <tr>
        <td width="10%" class="table_caption">Дата</td>
        <td width="65%" class="table_caption_left">Вид документа (информации)</td>
        <td width="25%" class="table_caption_left">Автор</td>
      </tr>
      <xsl:apply-templates select="profile">
      </xsl:apply-templates>
    </table>
    <!-- end content -->
  </xsl:template>
  <xsl:template match="profile">
    <xsl:for-each select="issReview">
      <tr>
        <xsl:attribute name="bgcolor">
          <xsl:call-template name="set_bg">
            <xsl:with-param name="str_num" select="position()"/>
          </xsl:call-template>
        </xsl:attribute>
        <td style="width:10%; text-align:center" class="table_item_left">
          <xsl:value-of select="@date"/>
        </td>
        <td style="width:50%;" class="table_item_left">
                <xsl:call-template name="img_by_ext">
                  <xsl:with-param name="ext" select="@ext"/>
                </xsl:call-template>
                <a id="fileName" target="_blank">
                  <xsl:attribute name="href">
                    /Documents/Index?iss=<xsl:value-of select="//@iss"/>&amp;id=<xsl:value-of select="@doc_id"/>&amp;fn=<xsl:value-of select="@file_name"/>&amp;doc_type=1
                  <!--/docs/<xsl:value-of select="@author_id"/>/<xsl:value-of select="@doc_id"/>/<xsl:value-of select="@file_name"/>-->
                  </xsl:attribute>                 
                  <xsl:value-of select="@name"/>
                </a>
          <span style="float:right">
            <xsl:if test="@size!=''">
                (<xsl:value-of select="@size"/>)
            </xsl:if>
          </span>
        </td>
        <td style="width:40%;" class="table_item_left">
          <xsl:value-of select="@author_name"/>
        </td>
      </tr>
    </xsl:for-each>
    <xsl:if test="PC/@PC &gt; 1">
      <xsl:value-of disable-output-escaping="yes" select="js:GenPages(string(PC/@PC),string(PC/@PG))"/>
    </xsl:if>

    <script language="javascript" type="text/javascript">
      <![CDATA[ 		
        var CommonPager = function (PG) {    
              var Id = $('#tabId').val();              
              showClock();
              $.get("/tab/", { "id": Id, "ticker": ISS, "PG": PG }, function (data) {
                  hideClock();
                  $("#tab_content").html(data);
              }, "html").fail(function (jqXHR, textStatus, errorThrown) {
                  hideClock();
                  $("#tab_content").html(textStatus);
              });      				
			}
        var MoveTo = function(Page)
        {
        var sHtml = '';
          showwin('asd','helloworld');
        }
        
			]]>
    </script>
  </xsl:template>
</xsl:stylesheet>
