<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 	xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="urn:deitel:user">

  <xsl:output method="html" version="4.0" encoding="utf-8"/>

  <xsl:template match="/">
    <xsl:apply-templates/>
  </xsl:template>
  <xsl:decimal-format name="buh" decimal-separator="," grouping-separator=" " NaN="0,000"/>

  <xsl:template match="iss_profile">

    <!-- content -->
    <xsl:apply-templates select="profile">
    </xsl:apply-templates>
    <!-- end content -->
  </xsl:template>
  <xsl:template match="profile">
    <p class="minicaption">КАРТОТЕКА  АРБИТРАЖНЫХ  ДЕЛ</p>
    <p>Источник данных: Право.ру</p>
    <br/>
    <div class="minicaption" style="float:left;"><xsl:value-of disable-output-escaping="yes" select="//name/@val"/></div>
    <br/>
    <xsl:for-each select="cases">
      <hr/>
      <table class="profile_table" style="width:100%;">
        <tr>
          <td class="table_caption">Номер дела:<xsl:value-of select="@FirstNumber"/></td>
          <td style="text-align:right;">Дата обновления <xsl:value-of select="@ld"/></td>
        </tr>
      </table>
      <table class="profile_table" style="width:100%;">
        <tr>
          <td class="minicaption">Общие сведения</td>
          <td style="text-align:right;">
            <!--xsl:choose>
              <xsl:when test="@fin=0">Активное дело</xsl:when>
              <xsl:when test="@fin=1">Завершенное дело</xsl:when>
            </xsl:choose-->
          </td>
        </tr>
      </table>
      <table class="profile_table" width="100%">
        <tr>
          <th style="width:50%">Арбитражный суд</th>
          <td style="width:50%">
            <xsl:value-of select="@crtName"/>
          </td>
        </tr>
        <tr>
          <th>Номер дела</th>
          <td>
            <xsl:value-of select="@FirstNumber"/>
          </td>
        </tr>
        <tr>
          <th>Номера производств во всех инстанциях</th>
          <td>
            <xsl:value-of select="@all_nums"/>
          </td>
        </tr>
        <tr>
          <th>Дата поступления дела в суд</th>
          <td>
            <xsl:value-of select="@dt"/>
          </td>
        </tr>

        <tr>
          <th>Вид спора</th>
          <td>
            <xsl:value-of select="@ccdName"/>
          </td>
        </tr>
        <tr>
          <th>Категория спора</th>
          <td>
            <xsl:value-of select="@ctdName"/>
          </td>
        </tr>
        <tr>
          <th>Сумма иска, руб.</th>
          <td>
            <xsl:value-of select="format-number(@Casesum,'# ##0,00','buh')"/>
          </td>
        </tr>
      </table>

      <br/>
      <div class="minicaption">Форма процессуального участия</div>
      <xsl:if test="istect">
        <span style="font-weight:bold">Истец</span>
        <table class="data_table" width="100%" cellpadding="0" cellspacing="0">
          <tr>
            <th style="width:30%;">Участник</th>
            <th style="width:110px;">ИНН</th>
            <th style="width:110px;">ОГРН</th>
            <th>Адрес  участника</th>
          </tr>

          <xsl:for-each select="istect">
            <tr>
              <td style="width:30%;">
                <xsl:value-of disable-output-escaping ="yes" select="@sideName"/>
              </td>
              <td style="width:100px; text-align:center;">
                <xsl:value-of disable-output-escaping ="yes" select="@inn"/>
              </td>
              <td style="width:110px; text-align:center;">
                <xsl:value-of disable-output-escaping ="yes" select="@ogrn"/>
              </td>
              <td>
                <xsl:value-of disable-output-escaping ="yes" select="@address"/>
              </td>

            </tr>
          </xsl:for-each>
        </table>
      </xsl:if>
      <xsl:if test="otv">
        <br/>
        <span style="font-weight:bold">Ответчик</span>
        <table  class="data_table" width="100%" cellpadding="0" cellspacing="0">
          <tr>
            <th style="width:30%;">Участник</th>
            <th style="width:100px;">ИНН</th>
            <th style="width:110px;">ОГРН</th>
            <th>Адрес  участника</th>
          </tr>

          <xsl:for-each select="otv">
            <tr>
              <td style="width:30%;">
                <xsl:value-of disable-output-escaping ="yes" select="@sideName"/>
              </td>
              <td style="width:100px; text-align:center;">
                <xsl:value-of disable-output-escaping ="yes" select="@inn"/>
              </td>
              <td style="width:110px; text-align:center;">
                <xsl:value-of disable-output-escaping ="yes" select="@ogrn"/>
              </td>
              <td>
                <xsl:value-of disable-output-escaping ="yes" select="@address"/>
              </td>

            </tr>
          </xsl:for-each>
        </table>
      </xsl:if>
      <xsl:if test="third">
        <br/>
        <span style="font-weight:bold">Третье лицо</span>
        <table class="data_table" width="100%" cellpadding="0" cellspacing="0">
          <tr>
            <th style="width:30%;">Участник</th>
            <th style="width:100px;">ИНН</th>
            <th style="width:110px;">ОГРН</th>
            <th>Адрес  участника</th>
          </tr>

          <xsl:for-each select="third">
            <tr>
              <td style="width:30%;">
                <xsl:value-of disable-output-escaping ="yes" select="@sideName"/>
              </td>
              <td style="width:100px; text-align:center;">
                <xsl:value-of disable-output-escaping ="yes" select="@inn"/>
              </td>
              <td style="width:110px; text-align:center;">
                <xsl:value-of disable-output-escaping ="yes" select="@ogrn"/>
              </td>
              <td>
                <xsl:value-of disable-output-escaping ="yes" select="@address"/>
              </td>

            </tr>
          </xsl:for-each>
        </table>
      </xsl:if>
      <xsl:if test="others">
        <br/>
        <span style="font-weight:bold">Иные лица</span>
        <table class="data_table" width="100%" cellpadding="0" cellspacing="0">
          <tr>
            <th style="width:30%;">Участник</th>
            <th style="width:100px;">ИНН</th>
            <th style="width:110px;">ОГРН</th>
            <th>Адрес  участника</th>
          </tr>

          <xsl:for-each select="others">
            <tr>
              <td style="width:30%;">
                <xsl:value-of disable-output-escaping ="yes" select="@sideName"/>
              </td>
              <td style="width:100px; text-align:center;">
                <xsl:value-of disable-output-escaping ="yes" select="@inn"/>
              </td>
              <td style="width:110px; text-align:center;">
                <xsl:value-of disable-output-escaping ="yes" select="@ogrn"/>
              </td>
              <td>
                <xsl:value-of disable-output-escaping ="yes" select="@address"/>
              </td>

            </tr>
          </xsl:for-each>
        </table>
      </xsl:if>
      <xsl:if test="inst">

        <br/>
        <div  class="minicaption">Инстанции</div>
        <xsl:for-each select="inst">
          <div>
            <table class="data_table" style="width:100%">
              <tr>
                <td>
                  <span  style="font-weight:bold;">
                    <xsl:value-of select="@instLevel"/>:&#160;
                    <xsl:value-of select="@InstanceNum"/>,&#160;
                    <xsl:value-of select="@court_name"/>
                    <xsl:if test="@ffile" >
                      <table class="profile_table">
                        <tr>
                          <td style="width:80px;">
                            <xsl:value-of select="@data"/>

                          </td>
                          <td>
                            <a target="_blank">
                              <xsl:attribute name="href">
                                <xsl:value-of select="@ffile"/>
                              </xsl:attribute>
                              <xsl:value-of select="translate(@ffile_name,'&amp;','')"/>
                            </a>
                          </td>
                        </tr>
                      </table>
                    </xsl:if>


                  </span>

                </td>
                <td style="width:90px;white-space:nowrap;vertical-align:top;text-align:right;">
                  <span style="cursor:pointer;color:#003399;" val="0"><xsl:attribute name="onclick">SwitchInst('<xsl:number />',event)</xsl:attribute><xsl:attribute name="id">sp<xsl:number /></xsl:attribute>Подробнее</span>
                  <img src="/images/tra_e.png" alt="" style="padding-left:3px"><xsl:attribute name="id">cimg<xsl:number /></xsl:attribute></img>
                </td>
              </tr>
            </table>

            <div style="display:none"><xsl:attribute name="id">inst_<xsl:number /></xsl:attribute>
              <table class="data_table" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                  <th style="width:180px;">Дата</th>
                  <th style="width:20%;">Тип события</th>
                  <th>Заголовок события</th>
                </tr>
                <xsl:for-each select="doc">

                  <tr>
                    <td>
                      <xsl:value-of disable-output-escaping ="yes" select="@data"/>
                    </td>
                    <td>
                      <xsl:value-of disable-output-escaping ="yes" select="@Typename"/>
                    </td>
                    <td>
                      <xsl:choose>
                        <xsl:when test="@FileSize>0">
                          <a target="_blank">
                            <xsl:attribute name="href">
                              <xsl:value-of select="@file_name"/>
                            </xsl:attribute>
                            <xsl:value-of disable-output-escaping ="yes" select="translate(@ContentType,'&amp;','')"/>
                          </a>(<xsl:value-of select="@FileSize"/>К)
                        </xsl:when>
                        <xsl:otherwise>
                          <xsl:value-of disable-output-escaping ="yes" select="translate(@ContentType,'&amp;','')"/>

                        </xsl:otherwise>
                      </xsl:choose>


                    </td>
                  </tr>

                </xsl:for-each>
              </table>
            </div>
          </div>
        </xsl:for-each>


      </xsl:if>

    </xsl:for-each>
    <xsl:for-each select="Cases">
      <hr/>
      <table class="profile_table" style="width:100%;">
        <tr>
          <td class="bluecaption" >
            Номер дела:<xsl:value-of select="@reg_no"/>
          </td>
          <td style="text-align:right;">
            Дата обновления <xsl:value-of select="@ldt"/>
          </td>
        </tr>
      </table>
      <p class="minicaption">Общие сведения</p>
      <table class="profile_table" width="100%">
        <tr>
          <th  style="width:50%">Арбитражный суд</th>
          <td style="width:50%">
            <xsl:value-of select="@Court"/>
          </td>
        </tr>
        <tr>
          <th>Номер дела</th>
          <td>
            <xsl:value-of select="@reg_no"/>
          </td>
        </tr>
        <tr>
          <th>Номера производств во всех инстанциях</th>
          <td>
            <xsl:value-of select="@nos"/>
          </td>
        </tr>
        <tr>
          <th>Дата поступления дела в суд</th>
          <td>
            <xsl:value-of select="@dt"/>
          </td>
        </tr>

        <tr>
          <th>Вид спора</th>
          <td>
            <xsl:value-of select="@CName"/>
          </td>
        </tr>
        <tr>
          <th>Категория спора</th>
          <td>
            <xsl:value-of select="@DTName"/>
          </td>
        </tr>
        <tr>
          <th>Сумма иска, руб.</th>
          <td>
            <xsl:value-of select="format-number(@case_sum,'# ##0,00','buh')"/>
          </td>
        </tr>
      </table>

      <xsl:if test="PST">
        <br/>
        <div class="minicaption">Форма процессуального участия</div>
        <xsl:for-each select="PST">
          <span style="font-weight:bold">
            <xsl:value-of select="@TName"/>
          </span>
          <table class="data_table" width="100%" cellpadding="0" cellspacing="0">
            <tr>
              <th style="width:30%;">Участник</th>
              <th style="width:100px;">ИНН</th>
              <th style="width:110px;">ОГРН</th>
              <th>Адрес  участника</th>
            </tr>
            <xsl:for-each select="CS">
              <tr>
                <td style="width:30%;">
                  <xsl:value-of disable-output-escaping ="yes" select="@name"/>
                </td>
                <td style="width:100px; text-align:center;">
                  <xsl:value-of disable-output-escaping ="yes" select="@inn"/>
                </td>
                <td style="width:110px; text-align:center;">
                  <xsl:value-of disable-output-escaping ="yes" select="@ogrn"/>
                </td>
                <td>
                  <xsl:value-of disable-output-escaping ="yes" select="@address"/>
                </td>

              </tr>
            </xsl:for-each>
          </table>
        </xsl:for-each>
      </xsl:if>
      <xsl:if test="PCIL">
        <br/>
        <div  class="minicaption">Инстанции</div>
        <xsl:for-each select="PCIL">
          <div>
            <table class="data_table">
              <tr>
                <td style="width:80px;">
                  <span  style="font-weight:bold;">
                    <xsl:value-of select="@instance"/>:&#160;
                    <xsl:value-of select="@reg_no"/>,&#160;
                    <xsl:value-of select="@court_name"/>
                  </span>
                </td>
                <td style="width:90px; white-space:nowrap;vertical-align:top;">
                  <span style="cursor:pointer;color:#003399;" val="0">
                    <xsl:attribute name="onclick">
                      SwitchInst('<xsl:value-of select="@iid"/>',event)
                    </xsl:attribute><xsl:attribute name="id">sp<xsl:value-of select="@iid"/></xsl:attribute>Подробнее
                  </span>
                  <img src="/images/tra_e.png" alt="" style="padding-left:3px">
                    <xsl:attribute name="id">
                      cimg<xsl:value-of select="@iid"/>
                    </xsl:attribute>
                  </img>
                </td>
              </tr>
            </table>
            <!--<div style="float:none;margin-bottom:5px; height:24px; padding-top:2px;">
						<img width="16" border="0" align="absmiddle" height="16" title="Документ ZIP" alt="Документ ZIP" src="/images/icon_doczip_16.gif"/>
						<a target="_blank" style="cursor:pointer;padding-top:12px;font-size:11px;font-family:arial;text-decoration:none;">
							<xsl:attribute name="href">
								http://kad.arbitr.ru/Kad/PdfDocumentArchiveInstance/<xsl:value-of select="@inst_id"/>/<xsl:value-of select="translate(//Cases/@reg_no,'/','-')"/>_<xsl:value-of select="@ckod"/>.zip
							</xsl:attribute>
							Скачать документы
						</a>
						<br/>
						<span class="data_comment" style="margin-top:1px;margin-left:19px; float:left;">
							Архив  может не содержать документы при отсутствии их в первоисточнике.
						</span>
					</div>-->
            <div style="display:none">
              <xsl:attribute name="id">inst_<xsl:value-of select="@iid"/></xsl:attribute>
              <table class="data_table" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                  <th style="width:70px;">Дата</th>
                  <th style="width:20%;">Тип события</th>
                  <th>Заголовок события</th>
                </tr>
                <xsl:for-each select="PDT">
                  <tr>
                    <td>
                      <xsl:value-of disable-output-escaping ="yes" select="@rd"/>
                    </td>
                    <td>
                      <xsl:value-of disable-output-escaping ="yes" select="@event_type"/>
                    </td>
                    <td>
                      <xsl:value-of disable-output-escaping ="yes" select="@header"/>
                    </td>
                  </tr>

                </xsl:for-each>
              </table>
            </div>
          </div>
        </xsl:for-each>


      </xsl:if>

    </xsl:for-each>

    <div class="data_comment limitation">
      <hr/>
      ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника. В связи с особенностями функционирования и обновления, указанного источника информации АО «СКРИН» не может гарантировать полную актуальность и достоверность данных.
    </div>
    <script language="javascript" type="text/javascript">
      <![CDATA[ 
			function SwitchInst(id,e){
    
				if($("#sp" + id).attr("val")=="0"){
					//надо развернуть
					$("#sp" + id).html("Свернуть");
					$("#sp" + id).attr("val","1");
					$("#inst_" + id).css("display","block");
					$("#cimg" + id).attr("src","/images/tra_w.png")
				}else{				
					$("#sp" + id).html("Подробнее");
					$("#sp" + id).attr("val","0")
					$("#inst_" + id).css("display","none");
					$("#cimg" + id).attr("src","/images/tra_e.png")
				}
				if(!e){
					e=window.event;
				}
				if(e){
					if (e.stopPropagation) {
						e.stopPropagation();
					}else{
						e.cancelBubble=true;
					}    
				}

			}
      
            
      $('#xls_btn').unbind('click').addClass('disabled');
      function set_xls_function () {
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
            		

            iframepost({"id": $("#tab_id").val(), "ticker":$("#iss").val(), "xls" : "1"}, "/Tab/", "reports");
        }
			]]>
    </script>
  </xsl:template>

</xsl:stylesheet>