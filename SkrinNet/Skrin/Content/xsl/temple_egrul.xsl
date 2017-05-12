<?xml version="1.0" encoding="windows-1251" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:msxsl="urn:schemas-microsoft-com:xslt"
xmlns:user="urn:deitel:user">
    <xsl:output method="html" version="4.0" encoding="WINDOWS-1251"/>
    <xsl:template match="/">
        <xsl:apply-templates/>
    </xsl:template>
    <xsl:decimal-format name="buh" grouping-separator=" " decimal-separator="." NaN="0"/>

    <xsl:template match="iss_profile">
        <!-- content -->
        <xsl:apply-templates select="profile">
        </xsl:apply-templates>
        <!-- end content -->
    </xsl:template>
    <xsl:template match="profile">
        <html xmlns="http://www.w3.org/TR/REC-html40" xmlns:w="urn:schemas-microsoft-com:office:word" xmlns:o="urn:schemas-microsoft-com:office:office">
            <head>
                <meta content="text/html; charset=windows-1251" http-equiv="Content-Type"/>
                <meta content="Word.Document" name="ProgId"/>
                <style>&lt;!--@font-face{font-family:'Times New Roman CYR';panose-1:2 2 6 3 5 4 5 2 3 4;mso-font-charset:204;mso-generic-font-family:roman;mso-font-pitch:variable;mso-font-signature:536902279 -2147483648 8 0 511 0;}p.MsoNormal, li.MsoNormal, div.MsoNormal{mso-style-parent:'';margin:0cm;margin-bottom:.0001pt;mso-pagination:widow-orphan;font-size:12.0pt;font-family:'Times New Roman';mso-fareast-font-family:'Times New Roman';}@page Section1{size:595.3pt 841.9pt;margin:2.0cm 42.5pt 2.0cm 3.0cm;mso-header-margin:35.4pt;mso-footer-margin:35.4pt;mso-paper-source:0;}div.Section1{page:Section1;}--&gt;</style>
            </head>
            <body lang="RU" style="tab-interval:35.4pt">
                <p class="MsoNormal" align="center" style="text-align:center">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:14.0pt;mso-bidi-font-size:14.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Основные сведения о ЮЛ<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p class="MsoNormal" align="center" style="text-align:center">
                    <br/>
                    <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                        <tbody>
                            <xsl:for-each select="u2_data">
                                <tr>
                                    <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                        <p align="left" style="text-align:left" class="MsoNormal">
                                            <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                <xsl:value-of disable-output-escaping="yes" select="@name"/>
                                                <o:p></o:p>
                                            </span>
                                        </p>
                                    </td>
                                    <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                        <p align="left" style="text-align:left" class="MsoNormal">
                                            <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                <xsl:value-of disable-output-escaping="yes" select="@val"/>
                                                <o:p></o:p>
                                            </span>
                                        </p>
                                    </td>
                                </tr>
                            </xsl:for-each>
                        </tbody>
                    </table>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Адрес (место нахождения) ЮЛ<o:p></o:p>
                        </span>
                    </b>
                </p>
                <xsl:choose>
                    <xsl:when test="address_data[position()=last()]">
                        <p align="center" style="text-align:center" class="MsoNormal">
                            <xsl:for-each select="address_data">
                                <br/>
                                <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                    <tbody>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of disable-output-escaping="yes" select="@no"/>. ОГРН<o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of disable-output-escaping="yes" select="@showed_ogrn"/>
                                                            <o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Полное наименование<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@full_name"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование органа (лица)<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@body_name"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Адрес (место нахождения) юр.лица<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@legal_address"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Код города<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@phone_code"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Телефон<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@phone"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Факс<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@fax"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@grn_date"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ГРН<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@grn_no"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </xsl:for-each>
                            <br/>
                            <br/>
                        </p>
                    </xsl:when>
                    <xsl:otherwise>
                        <p align="center" style="text-align:center" class="MsoNormal">
                            <br/>
                            <br/>
                        </p>
                    </xsl:otherwise>
                </xsl:choose>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения об уставном капитале<o:p></o:p>
                        </span>
                    </b>
                </p>
                <xsl:choose>
                    <xsl:when test="capital_data[position()=last()]">
                        <p align="center" style="text-align:center" class="MsoNormal">
                            <xsl:for-each select="capital_data">
                                <br/>
                                <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                    <tbody>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of disable-output-escaping="yes" select="@no"/>. Размер (в рублях)<o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of disable-output-escaping="yes" select="@capital_string"/>
                                                            <o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Вид капитала<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@name"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@grn_date"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ГРН<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@grn_no"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </xsl:for-each>
                            <br/>
                            <br/>
                        </p>
                    </xsl:when>
                    <xsl:otherwise>
                        <p align="center" style="text-align:center" class="MsoNormal">
                            <br/>
                            <br/>
                        </p>
                    </xsl:otherwise>
                </xsl:choose>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения о долях в уставном капитале OOO, принадлежащих обществу<o:p></o:p>
                        </span>
                    </b>
                </p>
                <xsl:choose>
                    <xsl:when test="authshare_data[position()=last()]">
                        <p align="center" style="text-align:center" class="MsoNormal">
                            <xsl:for-each select="authshare_data">
                                <br/>
                                <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                    <tbody>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of disable-output-escaping="yes" select="@no"/>. Размер (в рублях)<o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of disable-output-escaping="yes" select="@SizeRubString"/>
                                                            <o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Размер (в рублях и долей рубля в виде простой дроби)<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@SizeRubFraction"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Доля в уставном капитале(в виде простой дроби)<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@ShareFraction"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Доля в уставном капитале(в процентах)<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@SharePercentString"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Доля в уставном капитале<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@Share"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Вид обременения<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@ChargeType"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Срок обременения или порядок определения срока<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@ChargePeriod"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Прекращение обременения<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@ChargeEnd"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@grn_date"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ГРН<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@grn_no"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </xsl:for-each>
                            <br/>
                            <br/>
                        </p>
                    </xsl:when>
                    <xsl:otherwise>
                        <p align="center" style="text-align:center" class="MsoNormal">
                            <br/>
                            <br/>
                        </p>
                    </xsl:otherwise>
                </xsl:choose>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения о стоимости чистых активов AO<o:p></o:p>
                        </span>
                    </b>
                </p>
                <xsl:choose>
                    <xsl:when test="accost_data[position()=last()]">
                        <br/>
                        <p align="center" style="text-align:center" class="MsoNormal">
                            <xsl:for-each select="accost_data">
                                <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                    <tbody>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of disable-output-escaping="yes" select="@no"/>. Стоимость чистых активов общества<o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of disable-output-escaping="yes" select="@CostString"/>
                                                            <o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата окончания отчетного периода, за который представлены сведения<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@DateEnd"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@grn_date"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ГРН<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@grn_no"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </xsl:for-each>
                            <br/>
                            <br/>
                        </p>
                    </xsl:when>
                    <xsl:otherwise>
                        <p align="center" style="text-align:center" class="MsoNormal">
                            <br/>
                            <br/>
                        </p>
                    </xsl:otherwise>
                </xsl:choose>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения о том, что общество находится в процессе уменьшения уставного капитала<o:p></o:p>
                        </span>
                    </b>
                </p>
                <xsl:choose>
                    <xsl:when test="aured_data[position()=last()]">
                        <p align="center" style="text-align:center" class="MsoNormal">
                            <xsl:for-each select="aured_data">
                                <br/>
                                <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                    <tbody>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of disable-output-escaping="yes" select="@no"/>. Величина, на которую уменьшается уставный капитал<o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of disable-output-escaping="yes" select="@CostDifferenceString"/>
                                                            <o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Способ уменьшения уставного капитала<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@ReduceMethod"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата принятия решения об уменьшении уставного капитала<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@DateDesicion"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@grn_date"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ГРН<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@grn_no"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </xsl:for-each>
                            <br/>
                            <br/>
                        </p>
                    </xsl:when>
                    <xsl:otherwise>
                        <p align="center" style="text-align:center" class="MsoNormal">
                            <br/>
                            <br/>
                        </p>
                    </xsl:otherwise>
                </xsl:choose>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения о состоянии юридического лица<o:p></o:p>
                        </span>
                    </b>
                </p>
                <xsl:choose>
                    <xsl:when test="status_data[position()=last()]">
                        <p align="center" style="text-align:center" class="MsoNormal">
                            <xsl:for-each select="status_data">
                                <br/>
                                <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                    <tbody>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of disable-output-escaping="yes" select="@no"/>. Сведения о состоянии ЮЛ<o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of disable-output-escaping="yes" select="@status_name"/>
                                                            <o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Код регоргана<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@org_code"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование регоргана<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@org_name"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@grn_date"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ГРН<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@grn_no"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </xsl:for-each>
                            <br/>
                            <br/>
                        </p>
                    </xsl:when>
                    <xsl:otherwise>
                        <p align="center" style="text-align:center" class="MsoNormal">
                            <br/>
                            <br/>
                        </p>
                    </xsl:otherwise>
                </xsl:choose>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения об образовании юридического лица<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="start_data[position()=last()]">
                        <xsl:for-each select="start_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. Cпособ образования<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@name"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата регистрации<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@reg_date"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@showed_ogrn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Регномер до 01.07.2002<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@reg_no"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Код регоргана<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@org_code"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Наименование регоргана<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@org_name"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения о прекращении деятельности<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="stop_data[position()=last()]">
                        <xsl:for-each select="stop_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. Cпособ прекращения деятельности<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@name"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата регистрации<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DateReg"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Grn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Код регоргана<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@OrgCode"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Наименование регоргана<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@OrgName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения об учредителях (участниках) - Российских ЮЛ<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="constul_data[position()=last()]">
                        <xsl:for-each select="constul_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. ОПФ<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@opf"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование учредителя (участника) - юр.лица<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@name"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Размер вклада (в рублях)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@share_pecuniary_string"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН учредителя<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@const_ogrn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Регномер до 01.07.2002<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@reg_no"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата регистрации при создании<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@reg_date"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ИНН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@inn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    КПП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@kpp"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Адрес учредителя (участника) - российского юр.лица<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@legal_address"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Код города<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@phone_code"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Телефон<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@phone"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Факс<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@fax"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН учрежденного ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@showed_ogrn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование учрежденного ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@showed_name"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Сведения о состоянии юр.лица<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@foundedstate"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grn_date"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grn_no"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Вид обременения<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@encumbrance_type"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Срок обременения<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@encumbrance_per"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Номинальная стоимость доли или части доли, находящейся в залоге или ином обременении(в рублях и долей рубля в виде простой дроби)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@nominal_secur_cost_rub_fraction"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Номинальная стоимость доли или части доли, находящейся в залоге или ином обременении(в рублях)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@nominal_secur_cost_rub"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Размер доли или части доли, находящейся в залоге или ином обременении по отношению к уставному капиталу общества(в виде простой дроби)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@secur_size_fraction"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Размер доли или части доли, находящейся в залоге или ином обременении по отношению к уставному капиталу общества(в процентах)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@secur_size_percent"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Размер доли или части доли, находящейся в залоге или ином обременении по отношению к уставному капиталу общества<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@secur_size"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения об учредителях (участниках) - иностранных ЮЛ<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="constiul_data[position()=last()]">
                        <xsl:for-each select="constiul_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. Полное наименование учредителя (участника) - юр.лица<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@name"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Размер вклада (в рублях)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@share_pecuniary_string"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Страна регистрации (инкорпорации)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@country"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата регистрации в стране регистрации (инкорпорации)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@reg_date"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Регистрационный номер<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@reg_no"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Наименование регистрирующего органа<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@org_name"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Место нахождения в стране регистрации (инкорпорации)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@legal_address"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН учрежденного ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@showed_ogrn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование учрежденного ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@showed_name"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Сведения о состоянии юр.лица<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@foundedstate"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grn_date"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grn_no"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Вид обременения<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@encumbrance_type"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Срок обременения<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@encumbrance_per"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Номинальная стоимость доли или части доли, находящейся в залоге или ином обременении(в рублях и долей рубля в виде простой дроби)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@nominal_secur_cost_rub_fraction"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Номинальная стоимость доли или части доли, находящейся в залоге или ином обременении(в рублях)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@nominal_secur_cost_rub"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Размер доли или части доли, находящейся в залоге или ином обременении по отношению к уставному капиталу общества(в виде простой дроби)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@secur_size_fraction"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Размер доли или части доли, находящейся в залоге или ином обременении по отношению к уставному капиталу общества(в процентах)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@secur_size_percent"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Размер доли или части доли, находящейся в залоге или ином обременении по отношению к уставному капиталу общества<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@secur_size"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения об учредителях (участниках) - физических лицах<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="constfl_data[position()=last()]">
                        <xsl:for-each select="constfl_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. Фамилия<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@F_name"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Имя<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@I_name"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Отчество<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@O_name"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ИНН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@inn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Размер вклада (в рублях)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@share_pecuniary_string"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН учрежденного ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@showed_ogrn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование учрежденного ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@showed_name"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Сведения о состоянии юр.лица<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@foundedstate"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grn_date"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grn_no"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Вид обременения<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@encumbrance_type"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Срок обременения<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@encumbrance_per"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Номинальная стоимость доли или части доли, находящейся в залоге или ином обременении(в рублях и долей рубля в виде простой дроби)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@nominal_secur_cost_rub_fraction"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Номинальная стоимость доли или части доли, находящейся в залоге или ином обременении(в рублях)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@nominal_secur_cost_rub"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Размер доли или части доли, находящейся в залоге или ином обременении по отношению к уставному капиталу общества(в виде простой дроби)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@secur_size_fraction"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Размер доли или части доли, находящейся в залоге или ином обременении по отношению к уставному капиталу общества(в процентах)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@secur_size_percent"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Размер доли или части доли, находящейся в залоге или ином обременении по отношению к уставному капиталу общества<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@secur_size"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения об учредителях (участниках) - Российская Федерация(ЮЛ управляющее долей собственника)<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="f_rful_data[position()=last()]">
                        <xsl:for-each select="f_rful_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. Вид управляющего долей<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@FounderType"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ОПФ<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@opf"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование учредителя (участника) - юр.лица<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FounderFullName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Размер вклада (в рублях)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DepositSizeString"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН учредителя<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FounderOgrn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Регномер до 01.07.2002<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@RegNmbBefore"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата регистрации при создании<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DateReg"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ИНН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Inn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    КПП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Kpp"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Адрес учредителя (участника) - российского юр.лица<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FounderAddress"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Код города<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@CityCode"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Телефон<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Phone"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Факс<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Fax"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН учрежденного ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FoundedOgrn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование учрежденного ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FoundedFullName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@EgrulDate"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Grn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения об учредителях (участниках) - Российская Федерация(ФЛ управляющее долей собственника)<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="f_rffl_data[position()=last()]">
                        <xsl:for-each select="f_rffl_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. Вид управляющего долей<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@FounderType"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Фамилия<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@LastName"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Имя<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FirstName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Отчество<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@MiddleName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ИНН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Inn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Размер вклада (в рублях)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DepositSizeString"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН учрежденного ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Ogrn_showed"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование учрежденного ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FullName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@EgrulDate"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Grn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения об учредителях (участниках) - Субъект Российской Федерация(ЮЛ управляющее долей собственника)<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="f_subul_data[position()=last()]">
                        <xsl:for-each select="f_subul_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. Вид управляющего долей<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@FounderType"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ОПФ<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@opf"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование учредителя (участника) - юр.лица<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FounderFullName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Размер вклада (в рублях)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DepositSizeString"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН учредителя<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FounderOgrn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Регномер до 01.07.2002<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@RegNmbBefore"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата регистрации при создании<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DateReg"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ИНН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Inn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    КПП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Kpp"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Адрес учредителя (участника) - российского юр.лица<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FounderAddress"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Код города<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@CityCode"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Телефон<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Phone"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Факс<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Fax"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН учрежденного ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FoundedOgrn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование учрежденного ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FoundedFullName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@EgrulDate"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Grn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения об учредителях (участниках) - Субъект Российской Федерация(ФЛ управляющее долей собственника)<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="f_rffl_data[position()=last()]">
                        <xsl:for-each select="f_subfl_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. Вид управляющего долей<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@FounderType"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Фамилия<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@LastName"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Имя<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FirstName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Отчество<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@MiddleName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ИНН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Inn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Размер вклада (в рублях)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DepositSizeString"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН учрежденного ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Ogrn_showed"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование учрежденного ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FullName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@EgrulDate"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Grn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения об учредителях (участниках) - Муниципальное образование(ЮЛ управляющее долей собственника)<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="f_moul_data[position()=last()]">
                        <xsl:for-each select="f_moul_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. Вид управляющего долей<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@FounderType"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ОПФ<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@opf"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование учредителя (участника) - юр.лица<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FounderFullName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Размер вклада (в рублях)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DepositSizeString"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН учредителя<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FounderOgrn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Регномер до 01.07.2002<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@RegNmbBefore"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата регистрации при создании<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DateReg"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ИНН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Inn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    КПП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Kpp"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Адрес учредителя (участника) - российского юр.лица<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FounderAddress"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Код города<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@CityCode"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Телефон<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Phone"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Факс<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Fax"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН учрежденного ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FoundedOgrn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование учрежденного ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FoundedFullName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@EgrulDate"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Grn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения об учредителях (участниках) - Муниципальное образование(ФЛ управляющее долей собственника)<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="f_mofl_data[position()=last()]">
                        <xsl:for-each select="f_mofl_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. Вид управляющего долей<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@FounderType"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Фамилия<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@LastName"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Имя<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FirstName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Отчество<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@MiddleName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ИНН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Inn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Размер вклада (в рублях)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DepositSizeString"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН учрежденного ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Ogrn_showed"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование учрежденного ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FullName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@EgrulDate"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Grn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения об учредителях (участниках) - Паевой инвестиционный фонд(ЮЛ управляющее долей собственника)<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="f_piful_data[position()=last()]">
                        <xsl:for-each select="f_piful_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. Вид управляющего долей<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@FounderType"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ОПФ<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@opf"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование учредителя (участника) - юр.лица<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FounderFullName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Размер вклада (в рублях)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DepositSizeString"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН учредителя<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FounderOgrn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Регномер до 01.07.2002<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@RegNmbBefore"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата регистрации при создании<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DateReg"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ИНН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Inn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    КПП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Kpp"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Адрес учредителя (участника) - российского юр.лица<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FounderAddress"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Код города<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@CityCode"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Телефон<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Phone"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Факс<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Fax"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН учрежденного ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FoundedOgrn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование учрежденного ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FoundedFullName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@EgrulDate"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Grn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения об учредителях (участниках) - Паевой инвестиционный фонд(ФЛ управляющее долей собственника)<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="f_piffl_data[position()=last()]">
                        <xsl:for-each select="f_piffl_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. Вид управляющего долей<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@FounderType"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Фамилия<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@LastName"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Имя<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FirstName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Отчество<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@MiddleName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ИНН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Inn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Размер вклада (в рублях)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DepositSizeString"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН учрежденного ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Ogrn_showed"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование учрежденного ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FullName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@EgrulDate"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Grn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения о держателе реестра акционеров АО<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="sharhold_data[position()=last()]">
                        <xsl:for-each select="sharhold_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. Наименование держателя реестра акционеров<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@SH_Name"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@SH_Ogrn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@EgrulDate"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Grn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения о юр.лицах - предшественниках при реорганизации<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="pred_data[position()=last()]">
                        <xsl:for-each select="pred_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. ОПФ<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@opf"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Predc_Ogrn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование реорганизованного юр.лица<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FullName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Регномер до 01.07.2002<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@RegNumbrBefore"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата регистрации<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DateReg"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Наименование регоргана<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@RegOrgName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ИНН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Inn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    КПП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Kpp"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Адрес (место нахождения) юр.лица<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Address"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Код города<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Code"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Телефон<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Phone"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Факс<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Fax"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@EgrulDate"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Grn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения о юр.лицах - преемниках при реорганизации<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="succ_data[position()=last()]">
                        <xsl:for-each select="succ_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. ОПФ<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@opf"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Suc_Ogrn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование реорганизованного юр.лица<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FullName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Регномер до 01.07.2002<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@RegNumbrBefore"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата регистрации<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DateReg"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Наименование регоргана<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@RegOrgName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ИНН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Inn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    КПП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Kpp"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Адрес (место нахождения) юр.лица<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Address"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Код города<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Code"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Телефон<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Phone"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Факс<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Fax"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@EgrulDate"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Grn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения о начале процедуры реорганизации<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="reorg_proc[position()=last()]">
                        <xsl:for-each select="reorg_proc">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. Состояние реорганизующегося<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@ReorgState"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОПФ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Opf"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@ShowedOgrn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование реорганизованного юр.лица<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FullName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Регномер до 01.07.2002<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@RegBefore"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата регистрации<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DateReg"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Наименование регоргана<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@RegOrg"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ИНН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Inn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    КПП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Kpp"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Адрес (место нахождения) юр.лица<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Address"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Код города<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@PhoneCode"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Телефон<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Phone"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Факс<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Fax"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@EgrulDate"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Grn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения о физических лицах, имеющих право действовать без доверенности<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="manag_data[position()=last()]">
                        <xsl:for-each select="manag_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. Должность<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@position"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Фамилия<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@F_name"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Имя<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@I_name"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Отчество<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@O_name"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ИНН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@inn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@showed_ogrn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@full_name"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Сведения о состоянии юр.лица<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@foundedstate"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grn_date"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grn_no"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения об управляющей компании<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="mancomp_data[position()=last()]">
                        <xsl:for-each select="mancomp_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. ОГРН управляющей компании<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@ogrn_mc"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Регистрационный номер<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@reg_no"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Наименование юр.лица - управляющей компании<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@name"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ИНН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@inn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Адрес (место нахождения)<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@address"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Код города<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@phone_code"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Телефон<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@phone"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Факс<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@fax"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    КПП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@kpp"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@showed_ogrn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@showed_full_name"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grn_date_str"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grn_no"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения о филиалах юр.лица<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="branch_data[position()=last()]">
                        <xsl:for-each select="branch_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. ОГРН<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@showed_ogrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@name"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Адрес (место нахождения) филиала<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@address"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Код города<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@phone_code"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Телефон<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@phone"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Факс<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@fax"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grn_date"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grn_no"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения о представительствах юр.лица<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="out_data[position()=last()]">
                        <xsl:for-each select="out_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. ОГРН<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@showed_ogrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@name"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Адрес (место нахождения) представительства<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@address"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Код города<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@phone_code"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Телефон<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@phone"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Факс<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@fax"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grn_date"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grn_no"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения о видах экономической деятельности<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="okved_data[position()=last()]">
                        <xsl:for-each select="okved_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                  <tr>
                                    <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                      <p align="left" style="text-align:left" class="MsoNormal">
                                        <b style="mso-bidi-font-weight:normal">
                                          <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                            <xsl:value-of disable-output-escaping="yes" select="@no"/>. Версия справочника<o:p></o:p>
                                          </span>
                                        </b>
                                      </p>
                                    </td>
                                    <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                      <p align="left" style="text-align:left" class="MsoNormal">
                                        <b style="mso-bidi-font-weight:normal">
                                          <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                            <xsl:value-of disable-output-escaping="yes" select="@dic_version"/>
                                            <o:p></o:p>
                                          </span>
                                        </b>
                                      </p>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                      <p align="left" style="text-align:left" class="MsoNormal">
                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                          Код по ОКВЭД<o:p></o:p>
                                        </span>
                                      </p>
                                    </td>
                                    <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                      <p align="left" style="text-align:left" class="MsoNormal">
                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                          <xsl:value-of disable-output-escaping="yes" select="@okved"/>
                                          <o:p></o:p>
                                        </span>
                                      </p>
                                    </td>
                                  </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Тип<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@okved_type"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Наименование вида деятельности<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@okved_name"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grn_date"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grn_no"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения о постановке на учет в налоговом органе<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="rec_data[position()=last()]">
                        <xsl:for-each select="rec_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. ИНН<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@inn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    КПП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@kpp"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата постановки на учет в налоговом органе<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@record_date"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата снятия с учета<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@end_date"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Код налогового органа<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@org_code"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Наименование налогового органа<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@org_name"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grn_date"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grn_no"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения о регистрации в ПФ России<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="pfreg_data[position()=last()]">
                        <xsl:for-each select="pfreg_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. ОГРН<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@Showed_Ogrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FullName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Регномер ПФ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@RegNumbr"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата регистрации<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DateReg"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата снятия с учета<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DateUnReg"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Наименование территориального органа ПФ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@PFName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@EgrulDate"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Grn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения о регистрации в ФСС России<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="fssreg_data[position()=last()]">
                        <xsl:for-each select="fssreg_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. ОГРН<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@Showed_Ogrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FullName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Регномер ФСС<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@RegNumbr"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата первичной регистрации<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FirstDateReg"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата регистрации<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DateReg"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата снятия с учета<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DateUnReg"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Наименование исполнительного органа ФСС<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FSSName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@EgrulDate"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Grn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения о регистрации в ФОМС России<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="fomsreg_data[position()=last()]">
                        <xsl:for-each select="fomsreg_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. ОГРН<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@Showed_Ogrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FullName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Регномер ФОМС<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@RegNumbr"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата регистрации<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DateReg"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата снятия с учета<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DateUnReg"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Наименование территориального ФОМС<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FOMSName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@EgrulDate"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Grn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения о лицензиях<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="lic_data[position()=last()]">
                        <xsl:for-each select="lic_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. Номер лицензии<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@LicenceNumbr"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата лицензии<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@LicenceDate"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Состояние лицензии<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@name"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Лицензирующий орган, выдавший лицензию<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@LicenceOrg"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата начала действия лицензии<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DateBegining"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата окончания действия лицензии<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DateEnding"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Место действия<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Place"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Showed_Ogrn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@FullName"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@EgrulDate"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Grn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения о записях в ЕГРЮЛ<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="grn_data[position()=last()]">
                        <xsl:for-each select="grn_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. ГРН<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@grn_no"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grn_date_str"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Событие, с которым связано внесение записи<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@vidreg_name"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Наименование регоргана, в котором внесена запись<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@org_name"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Статус<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@name"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ОГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@showed_ogrn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Полное наименование ЮЛ<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@ul_fullname"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <b style="mso-bidi-font-weight:Normal ">
                        <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                            Сведения о выданных свидетельствах<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="cer_data[position()=last()]">
                        <xsl:for-each select="cer_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. Номер свидетельства<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@Numbr"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Серия свидетельства<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Series"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата выдачи<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@DateGet"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Наименование регоргана, выдавшего свидетельство<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@OrgGet"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Статус<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@name"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРН<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@Grn"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </xsl:for-each>
                    </xsl:if>
                    <br/>
                    <br/>
                </p>

                <xsl:if test="version/@val=2">
                    <hr/>
                    <p class="MsoNormal" align="center" style="text-align:center">
                        <b style="mso-bidi-font-weight:Normal ">
                            <span style="font-size:14.0pt;mso-bidi-font-size:14.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">Сведения об изменениях</span>
                        </b>
                    </p>
                    <p align="center" style="text-align:center" class="MsoNormal">
                        <b style="mso-bidi-font-weight:Normal ">
                            <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                Сведения об учредителях (участниках) - Российских ЮЛ<o:p></o:p>
                            </span>
                        </b>
                    </p>
                    <p align="center" style="text-align:center" class="MsoNormal">
                        <xsl:if test="const_ul_history[position()=last()]">
                            <xsl:for-each select="const_ul_history">
                                <br/>
                                <table cellspacing="0" cellpadding="0" border="1" width="650" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                    <tbody>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of select="@no"/>. ОПФ<o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of disable-output-escaping="yes" select="@Opf"/>
                                                            <o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Полное наименование учредителя (участника) - юр.лица<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@FounderFullName"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Размер вклада (в рублях)<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@DepositSizeString"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ОГРН учредителя<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@FounderOgrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Регномер до 01.07.2002<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@RegNmbBefore"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата регистрации при создании<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@DateReg"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ИНН<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@Inn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        КПП<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@Kpp"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Адрес учредителя (участника) - российского юр.лица<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@FounderAddress"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Код города<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@CityCode"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Телефон<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@Phone"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Факс<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@Fax"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ОГРН учрежденного ЮЛ<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@FoundedOgrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Полное наименование учрежденного ЮЛ<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@FoundedFullName"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ОГРН/ГРН на основании которого сведения внесены<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@OldGrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата внесения сведений<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@OldGrnDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование регоргана, в котором внесена запись<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@OldGrnOrg"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата начала действия сведений<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@StartDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата окончания действия сведений<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@FinsishDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ГРН записи, которой внесены изменения<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@NewGrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование регоргана, в котором внесены изменения<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@NewGrnOrg"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ГРН по которому сведения признаны недействительными<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@CancelGrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата признания сведений недействительными<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@CancelGrnDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование регоргана, в котором сведения признаны недействительными<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@CancelGrnOrg"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </xsl:for-each>
                        </xsl:if>
                        <br/>
                        <br/>
                    </p>
                    <p align="center" style="text-align:center" class="MsoNormal">
                        <b style="mso-bidi-font-weight:Normal ">
                            <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                Сведения об учредителях (участниках) - иностранных ЮЛ<o:p></o:p>
                            </span>
                        </b>
                    </p>
                    <p align="center" style="text-align:center" class="MsoNormal">
                        <xsl:if test="const_uil_history[position()=last()]">
                            <xsl:for-each select="const_uil_history">
                                <br/>
                                <table cellspacing="0" cellpadding="0" border="1" width="650" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                    <tbody>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of select="@no"/>. Полное наименование учредителя (участника) - юр.лица<o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of disable-output-escaping="yes" select="@FounderFullName"/>
                                                            <o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Размер вклада (в рублях)<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@DepositSizeString"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Страна регистрации (инкорпорации)<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@Country"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата регистрации в стране регистрации (инкорпорации)<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of  select="@DateReg"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Регистрационный номер<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@RegNumbr"/>
                                                        <o:p></o:p>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование регистрирующего органа<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@RegOrg"/>
                                                        <o:p></o:p>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Место нахождения в стране регистрации (инкорпорации)<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@Address"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ОГРН учрежденного ЮЛ<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of  select="@FoundedOgrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Полное наименование учрежденного ЮЛ<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@FoundedFullName"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ОГРН/ГРН на основании которого сведения внесены<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@OldGrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата внесения сведений<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@OldGrnDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование регоргана, в котором внесена запись<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@OldGrnOrg"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата начала действия сведений<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@StartDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата окончания действия сведений<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@FinsishDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ГРН записи, которой внесены изменения<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@NewGrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование регоргана, в котором внесены изменения<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@NewGrnOrg"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ГРН по которому сведения признаны недействительными<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@CancelGrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата признания сведений недействительными<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@CancelGrnDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование регоргана, в котором сведения признаны недействительными<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@CancelGrnOrg"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </xsl:for-each>
                        </xsl:if>
                        <br/>
                        <br/>
                    </p>
                    <p align="center" style="text-align:center" class="MsoNormal">
                        <b style="mso-bidi-font-weight:Normal ">
                            <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                Сведения об учредителях (участниках) - физических лицах<o:p></o:p>
                            </span>
                        </b>
                    </p>
                    <p align="center" style="text-align:center" class="MsoNormal">
                        <xsl:if test="const_fl_history[position()=last()]">
                            <xsl:for-each select="const_fl_history">
                                <br/>
                                <table cellspacing="0" cellpadding="0" border="1" width="650" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                    <tbody>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of select="@no"/>. Фамилия<o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of disable-output-escaping="yes" select="@LastName"/>
                                                            <o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Имя<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@FirstName"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Отчество<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@MiddleName"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ИНН<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@Inn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Размер вклада (в рублях)<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@DepositSizeString"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ОГРН учрежденного ЮЛ<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@ShowedOgrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Полное наименование учрежденного ЮЛ<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@FullName"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ОГРН/ГРН на основании которого сведения внесены<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@OldGrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата внесения сведений<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@OldGrnDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование регоргана, в котором внесена запись<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@OldGrnOrg"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата начала действия сведений<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@StartDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата окончания действия сведений<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@FinsishDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ГРН записи, которой внесены изменения<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@NewGrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование регоргана, в котором внесены изменения<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@NewGrnOrg"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ГРН по которому сведения признаны недействительными<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@CancelGrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата признания сведений недействительными<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@CancelGrnDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование регоргана, в котором сведения признаны недействительными<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@CancelGrnOrg"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </xsl:for-each>
                        </xsl:if>
                        <br/>
                        <br/>
                    </p>
                    <p align="center" style="text-align:center" class="MsoNormal">
                        <b style="mso-bidi-font-weight:Normal ">
                            <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                Наименование ЮЛ<o:p></o:p>
                            </span>
                        </b>
                    </p>
                    <p align="center" style="text-align:center" class="MsoNormal">
                        <xsl:if test="name_history[position()=last()]">
                            <xsl:for-each select="name_history">
                                <br/>
                                <table cellspacing="0" cellpadding="0" border="1" width="650" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                    <tbody>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of select="@no"/>. ОГРН<o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of select="@ShownOgrn"/>
                                                            <o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ИНН<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@Inn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Полное наименование ЮЛ<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@FullName"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Сокращенное наименование ЮЛ<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@ShortName"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Фирменное наименование ЮЛ<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@TradeName"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование ЮЛ на языке народов РФ<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@NationalName"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Национальный язык<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@NationalLanguage"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование ЮЛ на иностранном языке<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@ForeignName"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Иностранный язык<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@ForeignLanguage"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ОГРН/ГРН на основании которого сведения внесены<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@OldGrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата внесения сведений<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@OldGrnDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование регоргана, в котором внесена запись<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@OldGrnOrg"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата начала действия сведений<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@StartDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата окончания действия сведений<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@FinsishDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ГРН записи, которой внесены изменения<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@NewGrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование регоргана, в котором внесены изменения<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@NewGrnOrg"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ГРН по которому сведения признаны недействительными<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@CancelGrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата признания сведений недействительными<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@CancelGrnDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование регоргана, в котором сведения признаны недействительными<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@CancelGrnOrg"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </xsl:for-each>
                        </xsl:if>
                        <br/>
                        <br/>
                    </p>
                    <p align="center" style="text-align:center" class="MsoNormal">
                        <b style="mso-bidi-font-weight:Normal ">
                            <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                Адрес (место нахождения) ЮЛ<o:p></o:p>
                            </span>
                        </b>
                    </p>
                    <p align="center" style="text-align:center" class="MsoNormal">
                        <xsl:if test="address_history[position()=last()]">
                            <xsl:for-each select="address_history">
                                <br/>
                                <table cellspacing="0" cellpadding="0" border="1" width="650" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                    <tbody>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of select="@no"/>. ОГРН<o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of select="@ShowedOgrn"/>
                                                            <o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Полное наименование<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@FullName"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование органа (лица)<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@BodiesName"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Адрес (место нахождения) юр.лица<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@Address"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Код города<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@PhoneCode"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Телефон<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@Phone"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Факс<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@Fax"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ОГРН/ГРН на основании которого сведения внесены<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@OldGrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата внесения сведений<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@OldGrnDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование регоргана, в котором внесена запись<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@OldGrnOrg"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата начала действия сведений<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@StartDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата окончания действия сведений<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@FinsishDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ГРН записи, которой внесены изменения<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@NewGrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование регоргана, в котором внесены изменения<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@NewGrnOrg"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ГРН по которому сведения признаны недействительными<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@CancelGrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата признания сведений недействительными<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@CancelGrnDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование регоргана, в котором сведения признаны недействительными<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@CancelGrnOrg"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </xsl:for-each>
                        </xsl:if>
                        <br/>
                        <br/>
                    </p>
                    <p align="center" style="text-align:center" class="MsoNormal">
                        <b style="mso-bidi-font-weight:Normal ">
                            <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                Сведения о физических лицах, имеющих право действовать без доверенности<o:p></o:p>
                            </span>
                        </b>
                    </p>
                    <p align="center" style="text-align:center" class="MsoNormal">
                        <xsl:if test="managers_history[position()=last()]">
                            <xsl:for-each select="managers_history">
                                <br/>
                                <table cellspacing="0" cellpadding="0" border="1" width="650" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                    <tbody>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of select="@no"/>. Должность<o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of disable-output-escaping="yes" select="@Position"/>
                                                            <o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Фамилия<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@LastName"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Имя<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@FirstName"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Отчество<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@MiddleName"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ИНН<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@Inn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ОГРН<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@ShownOgrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Полное наименование ЮЛ<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@FullNameUL"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ОГРН/ГРН на основании которого сведения внесены<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@OldGrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата внесения сведений<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@OldGrnDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование регоргана, в котором внесена запись<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@OldGrnOrg"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата начала действия сведений<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@StartDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата окончания действия сведений<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@FinsishDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ГРН записи, которой внесены изменения<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@NewGrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование регоргана, в котором внесены изменения<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@NewGrnOrg"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ГРН по которому сведения признаны недействительными<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@CancelGrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата признания сведений недействительными<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@CancelGrnDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование регоргана, в котором сведения признаны недействительными<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@CancelGrnOrg"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </xsl:for-each>
                        </xsl:if>
                        <br/>
                        <br/>
                    </p>
                    <p align="center" style="text-align:center" class="MsoNormal">
                        <b style="mso-bidi-font-weight:Normal ">
                            <span style="font-size:12.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                Сведения об управляющей компании<o:p></o:p>
                            </span>
                        </b>
                    </p>
                    <p align="center" style="text-align:center" class="MsoNormal">
                        <xsl:if test="mc_history[position()=last()]">
                            <xsl:for-each select="mc_history">
                                <br/>
                                <table cellspacing="0" cellpadding="0" border="1" width="650" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                    <tbody>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of select="@no"/>. ОГРН управляющей компании<o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <b style="mso-bidi-font-weight:normal">
                                                        <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                            <xsl:value-of select="@ManOgrn"/>
                                                            <o:p></o:p>
                                                        </span>
                                                    </b>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование юр.лица - управляющей компании<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@ManName"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ИНН<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@Inn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Адрес (место нахождения)<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@Address"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Код города<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@Code"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Телефон<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@Phone"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Факс<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@Fax"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        КПП<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of  select="@KPP"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ОГРН<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of  select="@ShownOgrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Полное наименование ЮЛ<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@FullName"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ОГРН/ГРН на основании которого сведения внесены<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@OldGrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата внесения сведений<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@OldGrnDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование регоргана, в котором внесена запись<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@OldGrnOrg"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата начала действия сведений<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@StartDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата окончания действия сведений<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@FinsishDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ГРН записи, которой внесены изменения<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@NewGrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование регоргана, в котором внесены изменения<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@NewGrnOrg"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        ГРН по которому сведения признаны недействительными<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@CancelGrn"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Дата признания сведений недействительными<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of select="@CancelGrnDate"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        Наименование регоргана, в котором сведения признаны недействительными<o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                            <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                                <p align="left" style="text-align:left" class="MsoNormal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@CancelGrnOrg"/>
                                                        <o:p></o:p>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </xsl:for-each>
                        </xsl:if>
                        <br/>
                        <br/>
                    </p>
                </xsl:if>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>

