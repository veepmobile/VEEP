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
    function number_format(node, decimals, point, separator){
    var number=node.nextNode().text;
    if(!isNaN(number)){
    point = point ? point : '.';
    number = number.split('.');
    }
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
    if(decimals &amp;&amp; number[1]) {
      number[1] = Math.round(parseFloat(number[1].substr(0, decimals) + '.' + number[1].substr(decimals, number[1].length), 10));
      return(number.join(point));
    }
    else return(null);
    }
  </msxsl:script>
  <xsl:output method="html" version="4.0" encoding="utf-8"/>
  <xsl:decimal-format name="buh" decimal-separator="." grouping-separator=" " NaN="-"/>

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

    <xsl:if test="Issuer_Capitals[position()=last()]">
      <span class="subcaption">Размер  уставного  капитала</span>

      <table cellpadding="0" cellspacing="0" width="550px">
        <tr>
          <td  class="table_caption" style="width:70px;" >Дата</td>
          <td  class="table_caption"  >Наименование показателя</td>
          <td  class="table_caption"  >Значение, руб.</td>
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
        <xsl:if test="count(//Issuer_Capitals)>1 and //@PDF=-1">
          <tr>
            <td colspan="3" style="text-align:right; ">
              <span val="0" onclick="switch_arc(0);" style="cursor:pointer;color:#003399;" id="switcher0">
                Показать архив
              </span>
              <img src="/images/tra_e.png" alt="" style="padding-left:3px">
                <xsl:attribute name="id">img0</xsl:attribute>
              </img>
            </td>
          </tr>
        </xsl:if>
        <xsl:for-each select="Issuer_Capitals">
          <tr><xsl:attribute name="class"><xsl:if test="@ia=0">c0 trclosed</xsl:if></xsl:attribute><xsl:attribute name="id">ic<xsl:value-of select="position()"/></xsl:attribute>
            <td>
              <xsl:value-of select="@dt"/>
            </td>
            <td align="left"  width="50%">Размер уставного капитала:</td>
            <td align="right">
              <xsl:value-of select="format-number(@val, '# ##0.################', 'buh')"/>
            </td>
          </tr>
        </xsl:for-each>
      </table>
    </xsl:if>

    <script language="javascript" type="text/javascript">
      <![CDATA[ 
			xls_params={"iss":$("#iss").val(),"module" : "action/skrin/","x":Math.random()};

			var trclasses=new Array("trclosed","trshow")
			function switch_arc(sel){
				var newClass=trclasses[Math.abs($("#switcher" + sel).attr("val")-1)];
				var aids=new Array("ic","is");
				
				$("."+trclasses[$("#switcher" + sel).attr("val")]).each(function(i){
					if (this.id.substring(0,2)==aids[sel]){
						this.className=newClass;
					}	
		        });
				$("#switcher" + sel ).attr("val",Math.abs($("#switcher" + sel).attr("val")-1));
				$("#switcher" + sel).html((($("#switcher" + sel).attr("val")=="1")? "Скрыть архив":"Показать архив"));
				$("#img" + sel).attr("src",(($("#switcher" + sel).attr("val")=="1")? "/images/tra_w.png":"/images/tra_e.png"));
			}
			]]>
    </script> 
  </xsl:template>
</xsl:stylesheet>
