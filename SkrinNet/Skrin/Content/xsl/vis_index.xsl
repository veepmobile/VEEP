<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output doctype-public="HTML" doctype-system=""/>
  <xsl:template match="/">
    <xsl:apply-templates/>
  </xsl:template>
  <xsl:template match="iss_profile">
    <!-- content -->
    <html>
      <head>
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
        <!-- CSS Files -->
        <link type="text/css" href="/Content/vis/base.css?123" rel="stylesheet" />
        <link type="text/css" href="/Content/vis/Spacetree.css?133" rel="stylesheet" />
        <link href="/Content/forms.css" rel="stylesheet" />
        <link href="/Content/iss/iss_main.css" rel="stylesheet" />
        <!--JS Files-->
        <script language="javascript" type="text/javascript" src="/Content/vis/js/jit.js"></script>
        <script language="javascript" type="text/javascript" src="/Content/vis/js/st_fsns.js"></script>
        <script language="javascript" type="text/javascript" src="/Content/vis/js/html2canvas.js"></script>
        <script language="javascript" type="text/javascript" src="/Content/vis/js/jspdf.js"></script>
        <script type="text/javascript" src="/Scripts/jquery144.js" ></script>
        <script type="text/javascript" src="/Scripts/lib.js"></script>
        <script type="text/javascript" src="/Scripts/jquery-ui-1.11.4.custom/jquery-ui.min.js" ></script>
        <title>
         Связи :: <xsl:value-of select="//data/@sn"/>
        </title>
      </head>
      <xsl:apply-templates select="profile">
      </xsl:apply-templates>
    </html>
    <!-- end content -->
  </xsl:template>
  <xsl:template match="profile">
    
     
      <body onload="init_data();" >
        <div class="content">
        <h1>
      
          <xsl:value-of select="//data/@sn"/>
        </h1>
        <h3>Визуализация взаимосвязей</h3>
        <div id="container" >
          <div id="caption" style="position:relative; padding-top:7px; margin-left:11px; height:25px;text-align:left;"></div>

          <div id="prn">
            <div id="infovis" style="border:solid 1px #ABABAB;float:left;" >
             
            </div>
            <div style="float:none;">
              <div class="labtab m lt_legend" style="margin-left:15px;float:right;" >Учредитель/ участник</div>
              <div class="labtab r lt_legend" style="float:right;">Руководитель</div>
              <div class="labtab p lt_legend" style="float:right;">Учрежденное ЮЛ</div>
              <div class="labtab mr lt_legend" style="float:right;">Руководимое ЮЛ</div>
              <div style="float:right;margin-right:22px;" >
                <span style="font-size:16px">≈</span> - найдено по ФИО
              </div>
            </div>
          </div>
          <div id="log"></div>
          <div class="explain" id="cmt" style="margin-top:5px;    bottom: 0;    left: 0;    margin-left: 5px;    margin-right: 5px;    position: relative;  float:left;  text-align: justify;    z-index: 800;" >
            
            <div style="float:left">
              В связи с особенностями функционирования интернет-браузеров старого поколения функциональность данного сервиса в полном объеме поддерживается только современными версиями браузеров (Internet Explorer 9+, Google Chrome 20+, Mozilla Firefox 10+)
              <br /><span class="explain" onclick="dosave()">ВНИМАНИЕ: Представленная информация является результатом обработки данных первоисточника (данные организации, сведения ресурсов ЕГРЮЛ и ГМЦ Росстата). В связи с особенностями формирования базы данных первоисточника в приведенной схеме могут отсутствуют персональные данные по физическим лицам, а также могут отсутствовать или быть неактуальными сведения об акционерах акционерных обществ. АО «СКРИН» не несет ответственности за точность представленной информации или ее достоверность. Информация, содержащаяся в настоящей схеме, не является рекомендацией для принятия (или непринятия) каких-либо коммерческих, инвестиционных или иных решений.</span>
            </div>
          </div>

        </div>
        <div id="clock_div1" style="display:none;"></div>
        <input type="hidden" id="iss">
          <xsl:attribute name="value">
            <xsl:value-of select="//@iss"/>
          </xsl:attribute>
        </input>
        </div>
      </body>
    
    <script language="javascript" type="text/javascript">
			<![CDATA[ 
       function init_data() {
        showClock();
        
        start_data = $.ajax({ url: "/vis/GetNodes", async: false, data: ({ "id": $("#iss").val(), "is_first": 1, "type": "0","p" :"","pt" : 0 }), dataType: "text" }).responseText

        ds = -1;
        hideClock();
        first_node = eval(start_data);
        init();

    }
    function nop(){
        void(0);
    }

    $(document).ready(function(){
        $("#infovis").html('<div class="scale_plus" onclick="res(1.25)"></div><div class="scale_minus" onclick="res(.8)"></div>');
    }) 
      ]]>
    </script>
      
  </xsl:template>
</xsl:stylesheet>