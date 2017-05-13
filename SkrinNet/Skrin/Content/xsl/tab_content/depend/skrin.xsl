<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user"   xmlns:js="javascript:code">
  <xsl:import href="../../../xsl/common.xsl"/>
  <msxsl:script language="JScript" implements-prefix="js">
    function GetMult(val){
    <!--var val=String(nl.nextNode().text);-->
    var retval="0"
    var mult,exp;
    val = val.toUpperCase();
    if(val.indexOf("E") &lt; 0){
    retval=val
    }else{
    mult=val.substring(0,val.indexOf("E"));
    exp=val.substring(val.indexOf("E")+1,val.length);
    exp=Math.pow(10,exp);
    retval=mult*exp;
    }
    return retval;
    }
    function formatnumber(str) {
    <!--var amount = new String(str.nextNode().text);-->
    var amount = new String(str);
    amount = amount.split("").reverse();

    var output = "";
    for ( var i = 0; i &lt; amount.length-1; i++ ){
    output = amount[i] + output;
    if ((i+1) % 3 == 0 &amp;&amp; (amount.length-1) !== i)output = ' ' + output;
    }
    return output;
    }
    function number_format(node, decimals, point, separator)
    {<!--var number=node.nextNode().text-->
    var number = node;
    if(!isNaN(number))
    {
    point = point ? point : '.';
    number = number.split('.');
    if(separator)
    {
    var tmp_number = new Array();
    for(var i = number[0].length, j = 0; i &gt; 0; i -= 3)
    {
    var pos = i &gt; 0 ? i - 3 : i;
    tmp_number[j++] = number[0].substring(i, pos);
    }
    number[0] = tmp_number.reverse().join(separator);
    }
    if(decimals &amp;&amp; number[1])
    number[1] = Math.round((parseFloat(number[1].substr(0, decimals) + '.' + number[1].substr(decimals, number[1].length)), 10));
    return(number.join(point));
    }
    else return(null);
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
    <br/>
    <br/>
    <input type="hidden" id="iss">
      <xsl:attribute name="value">
        <xsl:value-of select="//@iss"/>
      </xsl:attribute>
    </input>
    <input type="hidden" id="per">
      <xsl:attribute name="value">
        <xsl:for-each select="periods">
          <xsl:if test="@curr=1">
            <xsl:value-of select="@yq"/>
          </xsl:if>
        </xsl:for-each>
      </xsl:attribute>
    </input>
    <span class="subcaption">Сведения об участии юридического лица в уставном (складочном) капитале юридических лиц, паевых фондов</span>
    <xsl:choose>
      <xsl:when test="part and part/@is_empty=0">
        <table width="98%" cellpadding="2" cellspacing="0">
          <tr>
            <td class="table_caption" style="width:250px;">Наименование</td>
            <td class="table_caption" style="width:50px;">ИНН</td>
            <td class="table_caption" style="width:250px;">Адрес места нахождения</td>
            <td class="table_caption" style="width:250px;">Вид деятельности</td>
            <td class="table_caption" style="width:75px;">Дата начала деятельности</td>
            <td class="table_caption">Доля в уставном капитале,%</td>
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
            <td class="table_shadow">
              <div style="width: 1px; height: 1px;">
                <spacer type="block" width="1px" height="1px" />
              </div>
            </td>
          </tr>
          <xsl:for-each select="part">
            <tr>
              <xsl:attribute name="bgcolor">
                <xsl:call-template name="set_bg">
                  <xsl:with-param name="str_num" select="position()"/>
                </xsl:call-template>
              </xsl:attribute>
              <td>
                <xsl:choose>
                  <xsl:when test="string-length(us/@ticker)>0">
                    <a target="_blank">
                      <xsl:attribute name="href">
                        /issuers/<xsl:value-of select="us/@ticker"/>/
                      </xsl:attribute>
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
              <td align="left">
                <xsl:value-of select="us/@okved_name"/>
              </td>
              <td align="center">
                <xsl:value-of select="us/@rd"/>
              </td>

              <td align="right" style="width:75px;">
                <xsl:value-of disable-output-escaping="yes" select="format-number(js:GetMult(string(@value)),'# ##0,###########', 'buh')"/>
              </td>
            </tr>
          </xsl:for-each>
        </table>

      </xsl:when>
      <xsl:otherwise>
        <br/>
        <br/>
        <span class="error">Дочерних и зависимых обществ нет.</span>

      </xsl:otherwise>
    </xsl:choose>
    <span class="data_comment limitation">
      ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и могут не отражать абсолютную актуальность и достоверность информации. Дополнительную информацию можно получить в разделе
      <a onclick="gotomenu(48);" href="#">«Квартальные отчеты»</a> или <a onclick="gotomenu(32);" href="#">«Существенные факты»</a>
    </span>
    <script type="text/javascript"  language="javascript" >
      <![CDATA[ 
				xls_params={"period": $("#per").val(),"iss":$("#iss").val(),"module" : "depend/skrin/","x":Math.random()}
        
      function goPeriod(period){
        showClock();
        $("#tab_content").load("/Tab/Index/?id=25&ticker=]]><xsl:value-of select="//@iss"/><![CDATA[&period=" + period, 
            function(data){
                hideClock();
            }
        );
      }        
			]]>
    </script>
  </xsl:template>
</xsl:stylesheet>
