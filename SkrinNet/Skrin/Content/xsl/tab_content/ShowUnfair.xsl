<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 	xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="urn:deitel:user">
    <xsl:output method="html" version="4.0" encoding="utf-8"/>
    <xsl:template match="/">
        <xsl:apply-templates/>
    </xsl:template>
    <xsl:decimal-format name="buh" decimal-separator="," grouping-separator=" "/>
    <xsl:template match="iss_profile">

        <!-- content -->
        <!--<table cellspacing="0" cellpadding="0" width="100%" class="dragg_div_top" style="-moz-user-select: none;">
            <tbody>
                <tr>
                    <td>
                        <img onclick="doPrint(&quot;news_text&quot;);" alt="Печать" class="prn_img" src="/images/printer.gif"/>
                    </td>
                    <td align="right" class="dragg_div_top">
                        <img style="cursor: default;" alt="Закрыть" onclick="closenews();" src="/images/close_div.gif"/>
                    </td>
                </tr>
            </tbody>
        </table>-->
        <div id="dcontent">
            <xsl:apply-templates select="profile">

            </xsl:apply-templates>
        </div>

        <!-- end content -->
    </xsl:template>
    <xsl:template match="profile">
        <span class="minicaption">
            РЕЕСТР НЕДОБРОСОВЕСТНЫХ ПОСТАВЩИКОВ
        </span>
        <table width="100%">
            <tr>
                <td class="explain" width="50%">Источник данных: ФАС России.</td>
                <td class="explain" style="text-align:right;">
                    Дата выгрузки результатов:
                    <xsl:value-of disable-output-escaping="yes" select="ex_i/@max_date"/>
                </td>
            </tr>
        </table>
        <p class="minicaption">
            <xsl:value-of disable-output-escaping="yes" select="ex2_i/@provider_name"/>
        </p>
        <hr style="color:#ddd;"/>
        <xsl:for-each select="rec">
            <h2>
                Реестровый номер: <xsl:value-of select="@r_num"/>
            </h2>


            <xsl:for-each select="part">
                <xsl:if test="@type=1">
                    <h4 style="margin-top:10px;">Информация о недобросовестном поставщике (исполнителе, подрядчике)</h4>
                    <table style="width:95%; margin-top:20px; " cellspacing="0" cellpadding="0" border="0" class="data_table">
                        <xsl:for-each select="data">
                            <tr>
                                <td style="width:50%">
                                    <xsl:value-of select="@name"/>
                                </td>
                                <td style="width:50%">
                                    <xsl:value-of select="@val"/>
                                </td>
                            </tr>
                        </xsl:for-each>
                    </table>
                </xsl:if>
                <xsl:if test="@type=2">
                    <br/>
                    <h4>
                        Общая информация по заявке РНП
                    </h4>
                    <table style="width:95%; margin-top:20px" cellspacing="0" cellpadding="0" border="0" class="data_table">
                        <xsl:for-each select="data">
                            <tr>
                                <td style="width:50%">
                                    <xsl:value-of select="@name"/>
                                </td>
                                <td style="width:50%">
                                    <xsl:value-of select="@val"/>
                                </td>
                            </tr>
                        </xsl:for-each>
                    </table>
                </xsl:if>
                <xsl:if test="@type=3">
                    <br/>
                    <h4>
                        Информация о заказчике, подавшем заявку на включение в Реестр
                    </h4>
                    <table style="width:95%;margin-top:20px" cellspacing="0" cellpadding="0" border="0" class="data_table">
                        <xsl:for-each select="data">
                            <tr>
                                <td style="width:50%">
                                    <xsl:value-of select="@name"/>
                                </td>
                                <td style="width:50%">
                                    <xsl:value-of select="@val"/>
                                </td>
                            </tr>
                        </xsl:for-each>
                    </table>
                </xsl:if>
                <xsl:if test="@type=4">
                    <br/>
                    <h4>
                        Сведения о проведенных торгах, запросе котировок
                    </h4>
                    <table style="width:95%;margin-top:20px" cellspacing="0" cellpadding="0" border="0" class="data_table">
                        <xsl:for-each select="data">
                            <tr>
                                <td style="width:50%">
                                    <xsl:value-of select="@name"/>
                                </td>
                                <td style="width:50%">
                                    <xsl:value-of select="@val"/>
                                </td>
                            </tr>
                        </xsl:for-each>
                    </table>
                </xsl:if>
                <xsl:if test="@type=5">
                    <br/>
                    <h4>
                        Сведения о неисполненном или ненадлежащим образом исполненном контракте
                    </h4>
                    <table style="width:95%;margin:20px 0" cellspacing="0" cellpadding="0" border="0" class="data_table">
                        <xsl:for-each select="data">
                            <tr>
                                <td style="width:50%">
                                    <xsl:value-of select="@name"/>
                                </td>
                                <td style="width:50%">
                                    <xsl:value-of select="@val"/>
                                </td>
                            </tr>
                        </xsl:for-each>
                    </table>
                </xsl:if>
            </xsl:for-each>
            <hr style="clear:both;color:#ddd;"/>
        </xsl:for-each>
        <table width="100%" cellpadding="4" cellspacing="0" border="0">
            <tr>
                <td width="100%">
                    <span class="explain" style="padding-top:20px;">
                        Внимание: в связи с особенностями функционирования и обновления указанного источника информации
                        АО "СКРИН" не может гарантировать абсолютную актуальность и достоверность данных.
                    </span>
                </td>
            </tr>
        </table>

    </xsl:template>

</xsl:stylesheet>