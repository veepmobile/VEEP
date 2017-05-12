<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
						      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
							  xmlns:user="urn:deitel:user">
  <xsl:import href="../../../xsl/common.xsl"/>
  <xsl:output method="html" version="4.0" encoding="utf-8"/>

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
    Данные на <xsl:value-of select="//ogrn/@data"/><br/>
    <xsl:choose>
      <xsl:when test="//opf_history/@code=90">
        <span class="subcaption">ОСНОВНЫЕ СВЕДЕНИЯ</span>
      </xsl:when>
      <xsl:otherwise>
        <span class="subcaption">ОСНОВНЫЕ СВЕДЕНИЯ О ЮРИДИЧЕСКОМ ЛИЦЕ </span>
      </xsl:otherwise>
    </xsl:choose>
    <table width="98%" cellpadding="2" cellspacing="0">
      <tr>
        <td>
        </td>
        <td style="text-align:right;">
          <span val="0" onclick="switch_vis();" style="cursor:pointer;color:#003399;" id="switcher">
            Показать архив
          </span>
          <img src="/images/tra_e.png" alt="" style="padding-left:3px">
            <xsl:attribute name="id">img</xsl:attribute>
          </img>
        </td>
      </tr>
      <tr>
        <td style="width:30%;font-weight:bold;">ОГРН</td>
        <td>
          <xsl:value-of select="//ogrn/@val"/>
        </td>
      </tr>
      <tr>
        <td style="vertical-align:top; font-weight:bold;">
          ИНН
        </td>
        <td>
          <span class="act">
            <xsl:value-of select="//inn_history/@val"/>
          </span>
          <xsl:if test="inn_history">
            <table class="arc" cellpadding="0" cellspacing="0" style="display: none;" >
              <xsl:for-each select="inn_history">

                <tr>
                  <td style="border:solid 1px #999;padding:2px 8px 2px 8px;">
                    <xsl:value-of select="@data"/>
                  </td>
                  <td style="border:solid 1px #999;padding:2px 8px 2px 8px;width:100%">
                    <xsl:value-of select="@val"/>
                  </td>
                </tr>

              </xsl:for-each>
            </table>
          </xsl:if>

        </td>
      </tr>
      <tr>
        <td style="vertical-align:top; background-color:#FFF;font-weight:bold;">
          Полное наименование
        </td>
        <td style="background-color:#FFF;">
          <span class="act">
            <xsl:value-of select="//full_name_history/@val"/>
          </span>
          <xsl:if test="full_name_history">
            <table class="arc" cellpadding="0" cellspacing="0" style="display: none; width:100%;">
              <xsl:for-each select="full_name_history">

                <tr>
                  <td style="border:solid 1px #999;padding:2px 8px 2px 8px;">
                    <xsl:value-of select="@data"/>
                  </td>
                  <td style="border:solid 1px #999;padding:2px 8px 2px 8px;width:100%">
                    <xsl:value-of select="@val"/>
                  </td>
                </tr>

              </xsl:for-each>

            </table>
          </xsl:if>

        </td>
      </tr>
      <tr>
        <td style="vertical-align:top; white-space:nowrap;font-weight:bold;">
          Сокращенное наименование
        </td>
        <td>
          <span class="act">
            <xsl:value-of select="//short_name_history/@val"/>
          </span>
          <xsl:if test="short_name_history">
            <table class="arc" cellpadding="0" cellspacing="0" style="display: none;" >
              <xsl:for-each select="short_name_history">

                <tr>
                  <td style="border:solid 1px #999;padding:2px 8px 2px 8px;">
                    <xsl:value-of select="@data"/>
                  </td>
                  <td style="border:solid 1px #999;padding:2px 8px 2px 8px;width:100%">
                    <xsl:value-of select="@val"/>
                  </td>
                </tr>

              </xsl:for-each>
            </table>
          </xsl:if>

        </td>
      </tr>
      <tr>
        <td style="vertical-align:top; background-color:#FFF;white-space:nowrap;font-weight:bold;">
          Наименование на иностранном языке
        </td>
        <td style="background-color:#FFF;">
          <span class="act">
            <xsl:value-of select="//short_name_eng_history/@val"/>
          </span>
          <xsl:if test="short_name_eng_history">
            <table class="arc" cellpadding="0" cellspacing="0" style="display: none;" >
              <xsl:for-each select="short_name_eng_history">

                <tr>
                  <td style="border:solid 1px #999;padding:2px 8px 2px 8px;">
                    <xsl:value-of select="@data"/>
                  </td>
                  <td style="border:solid 1px #999;padding:2px 8px 2px 8px;width:100%">
                    <xsl:value-of select="@val"/>
                  </td>
                </tr>

              </xsl:for-each>
            </table>
          </xsl:if>

        </td>
      </tr>
      <tr>
        <td style="vertical-align:top; white-space:nowrap;font-weight:bold;">
          ОПФ
        </td>
        <td>
          <span class="act">
            <xsl:value-of select="//opf_history/@val"/>
          </span>
          <xsl:if test="opf_history">
            <table class="arc" cellpadding="0" cellspacing="0" style="display: none;" >
              <xsl:for-each select="opf_history">

                <tr>
                  <td style="border:solid 1px #999;padding:2px 8px 2px 8px;">
                    <xsl:value-of select="@data"/>
                  </td>
                  <td style="border:solid 1px #999;padding:2px 8px 2px 8px;width:100%">
                    <xsl:value-of select="@val"/>
                  </td>
                </tr>

              </xsl:for-each>
            </table>
          </xsl:if>

        </td>
      </tr>
      <tr>
        <td style="vertical-align:top; background-color:#FFF;white-space:nowrap;font-weight:bold;">
          ОКПО
        </td>
        <td style="background-color:#FFF;">
          <xsl:value-of select="//okpo/@val"/>
        </td>
      </tr>
    </table>
    <br/>
    <xsl:choose>
      <xsl:when test="//opf_history/@code=90">
        <span class="subcaption">АДРЕС (МЕСТО НАХОЖДЕНИЯ)</span>
      </xsl:when>
      <xsl:otherwise>
        <span class="subcaption">АДРЕС (МЕСТО НАХОЖДЕНИЯ) ЮРИДИЧЕСКОГО ЛИЦА</span>
      </xsl:otherwise>
    </xsl:choose>
    <table width="98%" cellpadding="2" cellspacing="0">
      <tr>
        <td style="vertical-align:top; font-weight:bold;width:30%;">Адрес (место нахождения) юр.лиц</td>
        <td>
          <span class="act">
            <xsl:value-of select="//address_history/@val"/>
          </span>
          <xsl:if test="address_history">
            <table class="arc" cellpadding="0" cellspacing="0" style="display: none;" >
              <xsl:for-each select="address_history">

                <tr>
                  <td style="border:solid 1px #999;padding:2px 8px 2px 8px;">
                    <xsl:value-of select="@data"/>
                  </td>
                  <td style="border:solid 1px #999;padding:2px 8px 2px 8px;width:100%">
                    <xsl:value-of select="@val"/>
                  </td>
                </tr>

              </xsl:for-each>
            </table>
          </xsl:if>

        </td>
      </tr>
      <tr>
        <td style="vertical-align:top; background-color:#FFF;font-weight:bold;">Телефон</td>
        <td  style="background-color:#FFF;">
          <span class="act">
            <xsl:value-of select="//phone_history/@val"/>
          </span>
          <xsl:if test="phone_history">
            <table class="arc" cellpadding="0" cellspacing="0" style="display: none;">
              <xsl:for-each select="phone_history">

                <tr>
                  <td style="border:solid 1px #999;padding:2px 8px 2px 8px;">
                    <xsl:value-of select="@data"/>
                  </td>
                  <td style="border:solid 1px #999;padding:2px 8px 2px 8px;width:100%">
                    <xsl:value-of select="@val"/>
                  </td>
                </tr>

              </xsl:for-each>
            </table>
          </xsl:if>

        </td>
      </tr>
      <tr>
        <td style="vertical-align:top; font-weight:bold;">Факс</td>
        <td>
          <span class="act">
            <xsl:value-of select="//fax_history/@val"/>
          </span>
          <xsl:if test="fax_history">
            <table class="arc" cellpadding="0" cellspacing="0" style="display: none;" >
              <xsl:for-each select="fax_history">

                <tr>
                  <td style="border:solid 1px #999;padding:2px 8px 2px 8px;">
                    <xsl:value-of select="@data"/>
                  </td>
                  <td style="border:solid 1px #999;padding:2px 8px 2px 8px;width:100%">
                    <xsl:value-of select="@val"/>
                  </td>
                </tr>

              </xsl:for-each>
            </table>
          </xsl:if>
        </td>
      </tr>
      <tr>
        <td style="vertical-align:top; background-color:#FFF;font-weight:bold;">E-mail</td>
        <td  style="background-color:#FFF;">
          <span class="act">
            <xsl:value-of select="//email_history/@val"/>
          </span>
          <xsl:if test="email_history">
            <table class="arc" cellpadding="0" cellspacing="0" style="display: none;" >
              <xsl:for-each select="email_history">

                <tr>
                  <td style="border:solid 1px #999;padding:2px 8px 2px 8px;">
                    <xsl:value-of select="@data"/>
                  </td>
                  <td style="border:solid 1px #999;padding:2px 8px 2px 8px;width:100%">
                    <xsl:value-of select="@val"/>
                  </td>
                </tr>

              </xsl:for-each>
            </table>
          </xsl:if>

        </td>
      </tr>
    </table>
    <br/>
    <xsl:choose>
      <xsl:when test="//opf_history/@code=90">
        <span class="subcaption">АКТУАЛЬНОСТЬ СВЕДЕНИЙ</span>
      </xsl:when>
      <xsl:otherwise>
        <span class="subcaption">АКТУАЛЬНОСТЬ СВЕДЕНИЙ О ЮРИДИЧЕСКОМ ЛИЦЕ</span>
      </xsl:otherwise>
    </xsl:choose>
    <table width="98%" cellpadding="2" cellspacing="0">
      <tr>
        <td width="30%"  style="vertical-align:top; background-color:#FFF;font-weight:bold;">
          Статус
        </td>
        <td>
          <xsl:value-of select="//status/@val"/>
        </td>
      </tr>
    </table>
    <br/>
    <xsl:choose>
      <xsl:when test="//opf_history/@code=90">
        <span class="subcaption">СВЕДЕНИЯ ОБ ОБРАЗОВАНИИ</span>
      </xsl:when>
      <xsl:otherwise>
        <span class="subcaption">СВЕДЕНИЯ ОБ ОБРАЗОВАНИИ ЮРИДИЧЕСКОГО ЛИЦА</span>
      </xsl:otherwise>
    </xsl:choose>
    <span class="minicaption1">СВЕДЕНИЯ О ГОСУДАРСТВЕННОЙ РЕГИСТРАЦИИ</span>
    <table width="98%" cellpadding="2" cellspacing="0" class="act">
      <tr>
        <td style="vertical-align:top; background-color:#FFF;font-weight:bold;width:30%;">ОГРН</td>
        <td style="background-color:#fff;">
          <xsl:value-of select="//reg_history/@val1"/>
        </td>
      </tr>
      <tr>
        <td style="vertical-align:top; font-weight:bold;width:30%;">Дата присвоения ОГРН</td>
        <td>
          <xsl:value-of select="//reg_history/@val2"/>
        </td>
      </tr>
      <tr>
        <td style="vertical-align:top; background-color:#FFF;font-weight:bold;width:30%;">Наименование регистрирующего органа, внесшего запись</td>
        <td style="background-color:#fff;">
          <xsl:value-of select="//reg_history/@val3"/>
        </td>
      </tr>
    </table>
    <xsl:if test="reg_history">
      <xsl:for-each select="reg_history">

        <span class="arc" style="display: none;">
          Период: <xsl:value-of select="@data"/>
        </span>
        <table class="arc" width="98%" cellpadding="2" cellspacing="0" style="border:solid 1px #999; display: none;">
          <tr>
            <td style="vertical-align:top; background-color:#FFF;width:30%;">ОГРН</td>
            <td style="background-color:#FFF;">
              <xsl:value-of select="@val1"/>
            </td>
          </tr>
          <tr>
            <td style="vertical-align:top; width:30%;">Дата присвоения ОГРН</td>
            <td>
              <xsl:value-of select="@val2"/>
            </td>
          </tr>
          <tr>
            <td style="vertical-align:top; background-color:#FFF;width:30%;">Наименование регистрирующего органа, внесшего запись</td>
            <td style="background-color:#FFF;">
              <xsl:value-of select="@val3"/>
            </td>
          </tr>
        </table>

      </xsl:for-each>
    </xsl:if>
    <div class="minicaption1" style="margin-top:25px;">ДАННЫЕ О ГОСУДАРСТВЕННОЙ РЕГИСТРАЦИИ ДО 01.07.2002 *</div>
    <table width="98%" cellpadding="2" cellspacing="0">
      <tr>
        <td style="vertical-align:top; background-color:#FFF;font-weight:bold;width:30%;">Номер свидетельства о государственной регистрации</td>
        <td style="background-color:#fff;">
          <xsl:choose>
            <xsl:when test="reg2002">
              <xsl:value-of select="//reg2002/@reg_no"/>
            </xsl:when>
            <xsl:otherwise>-</xsl:otherwise>
          </xsl:choose>
        </td>
      </tr>
      <tr>
        <td style="vertical-align:top; font-weight:bold;width:30%;">Дата внесения записи в гос. реестр</td>
        <td>
          <xsl:choose>
            <xsl:when test="reg2002">
              <xsl:value-of select="//reg2002/@reg_date"/>
            </xsl:when>
            <xsl:otherwise>-</xsl:otherwise>
          </xsl:choose>
        </td>
      </tr>
      <tr>
        <td style="vertical-align:top; background-color:#FFF;font-weight:bold;width:30%;">Орган, осуществивший государственную регистрацию</td>
        <td style="background-color:#fff;">
          <xsl:choose>
            <xsl:when test="reg2002">
              <xsl:value-of select="//reg2002/@reg_org"/>
            </xsl:when>
            <xsl:otherwise>-</xsl:otherwise>
          </xsl:choose>
        </td>
      </tr>
    </table>
    <span class="data_comment">
      *Приведена информация по организации, либо по юридическому лицу, правопреемником которого является организация.
    </span>
    <br/>
    <span class="subcaption">Коды государственной статистики</span>
    <br/>
    <table width="98%" cellpadding="2" cellspacing="0">
      <tr>
        <td class="table_caption" >Наименование классификатора</td>
        <td width="150px" class="table_caption" >Код</td>
        <td class="table_caption" >Описание</td>
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
      <xsl:for-each select="kodes">
        <tr>
          <xsl:attribute name="bgcolor">
            <xsl:call-template name="set_bg">
              <xsl:with-param name="str_num" select="position()"/>
            </xsl:call-template>
          </xsl:attribute>
          <td style="font-weight:bold;">
            <xsl:value-of select="@name"/>
          </td>
          <td style="text-align:center;">
            <xsl:value-of select="@val"/>
          </td>
          <td>
            <xsl:value-of select="@descr"/>
          </td>

        </tr>
      </xsl:for-each>
    </table>
    <br/>
    <div class="subcaption">СВЕДЕНИЯ О ВИДАХ ЭКОНОМИЧЕСКОЙ ДЕЯТЕЛЬНОСТИ</div>
    <b>ОКВЭД (Общероссийский классификатор видов экономической деятельности).</b>
    <table width="98%" cellpadding="2" cellspacing="0">
      <tr>
        <td width="150px" class="table_caption" >Код</td>
        <td class="table_caption" >Тип</td>
        <td class="table_caption" >Описание</td>
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
      <xsl:choose>
        <xsl:when test="okveds">
          <xsl:for-each select="okveds">
            <tr>
              <xsl:attribute name="bgcolor">
                <xsl:call-template name="set_bg">
                  <xsl:with-param name="str_num" select="position()"/>
                </xsl:call-template>
              </xsl:attribute>
              <td align="center" valign="top">
                <xsl:if test="@main=1">
                  <xsl:attribute name="style">font-weight:bold</xsl:attribute>
                </xsl:if>
                <xsl:value-of select="@okved"/>
              </td>
              <td valign="top">
                <xsl:if test="@main=1">
                  <xsl:attribute name="style">font-weight:bold</xsl:attribute>
                  Основной вид деятельности
                </xsl:if>
                <xsl:if test="@main=0">
                  Дополнительный вид деятельности
                </xsl:if>
              </td>
              <td>
                <xsl:if test="@main=1">
                  <xsl:attribute name="style">font-weight:bold</xsl:attribute>
                </xsl:if>
                <xsl:value-of disable-output-escaping="yes" select="@txt"/>

              </td>
            </tr>
          </xsl:for-each>
        </xsl:when>
        <xsl:otherwise>
          <tr>
            <td align="center">-</td>
            <td align="center">-</td>
          </tr>
        </xsl:otherwise>
      </xsl:choose>
    </table>
    <b>ОКОНХ (Общероссийский классификатор отраслей народного хозяйства).</b>
    <table width="98%" cellpadding="2" cellspacing="0">
      <tr>
        <td width="150px" class="table_caption" >Код</td>
        <td class="table_caption" >Описание</td>
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
      <xsl:choose>
        <xsl:when test="okonh">
          <xsl:for-each select="okonh">
            <tr>
              <xsl:attribute name="bgcolor">
                <xsl:call-template name="set_bg">
                  <xsl:with-param name="str_num" select="position()"/>
                </xsl:call-template>
              </xsl:attribute>
              <td align="center">
                <xsl:value-of select="@code"/>
              </td>
              <td>
                <xsl:value-of select="@name"/>
              </td>
            </tr>
          </xsl:for-each>
        </xsl:when>
        <xsl:otherwise>
          <tr>
            <td align="center">Нет данных</td>
            <td align="center"></td>
          </tr>
        </xsl:otherwise>
      </xsl:choose>
    </table>
    <span class="data_comment limitation">
      ВНИМАНИЕ: в связи с особенностями функционирования и обновления указанного источника информации АО "СКРИН" не может гарантировать абсолютную актуальность и достоверность данных.
    </span>
    <script language="javascript" type="text/javascript">
      <![CDATA[ 
      
			xls_params={"iss":$("#iss").val(),"module" : "work/codes/","x":Math.random()}
      
			function switch_vis(){
				$(".arc").each(function(i){
					this.style.display=$("#switcher").attr("val")=="1"?"none":"block";
		        });
				$(".act").each(function(i){
					this.style.display=$("#switcher").attr("val")=="1"?"block":"none";
		        });

				$("#switcher").attr("val",Math.abs($("#switcher").attr("val")-1));
				$("#switcher").html((($("#switcher").attr("val")=="1")? "Скрыть архив":"Показать архив"));
				$("#img").attr("src",(($("#switcher").attr("val")=="1")? "/images/tra_w.png":"/images/tra_e.png"));
			}
			]]>
    </script>

  </xsl:template>
</xsl:stylesheet>