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
                            Основные сведения<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p class="MsoNormal" align="center" style="text-align:center">
                    <br/>
                    <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                        <tbody>
                            <xsl:for-each select="fl_data">
                                <tr>
                                    <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                        <p align="left" style="text-align:left" class="MsoNormal">
                                            <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                <xsl:value-of disable-output-escaping="yes" select="@name"/><o:p></o:p>
                                            </span>
                                        </p>
                                    </td>
                                    <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                        <p align="left" style="text-align:left" class="MsoNormal">
                                            <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                <xsl:value-of disable-output-escaping="yes" select="@val"/><o:p></o:p>
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
                            Регистрационные данные<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="reg_data[position()=last()]">
                        <xsl:for-each select="reg_data">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. ОГРНИП<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@ogrnip_shown"/><o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Сведения о статусе<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@status"/><o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата прекращения деятельности<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@finish_date"/><o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Вид предпринимателя<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@enter"/><o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Наименование регоргана, в котором находится регдело<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@reg_name"/><o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРНИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip"/><o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip_date"/><o:p></o:p>
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
                            Сведения, идентифицирующие ФЛ<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="ident[position()=last()]">
                        <xsl:for-each select="ident">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. ОГРНИП<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@ogrnip_shown"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@lastName"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@firstName"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@middlename"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Фамилия латинскими буквами<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@lastNameEng"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Имя латинскими буквами<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@firstNameEng"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Отчество латинскими буквами<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@middleNameEng"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата рождения<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@birth_date"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Место рождения<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@birth_place"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Пол<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@sex"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРНИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip_date"/>
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
                            Сведения о гражданстве<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="citizenship[position()=last()]">
                        <xsl:for-each select="citizenship">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. ОГРНИП<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@ogrnip_shown"/>
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
                                                    Вид гражданства<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@citizenType"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Страна гражданства<o:p></o:p>
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
                                                    ГРНИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip_date"/>
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
                            Документ, подтверждающий приобретение дееспособности несовершеннолетним<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="child_deals[position()=last()]">
                        <xsl:for-each select="child_deals">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. ОГРНИП<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@ogrnip_shown"/>
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
                                                    Основание приобретения дееспособности<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@reason"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Вид документа<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@doc_type"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Номер документа<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@document_no"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата выдачи документа<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@document_date"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Кем выдан<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@document_org"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРНИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip_date"/>
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
                    <xsl:if test="okved[position()=last()]">
                        <xsl:for-each select="okved">
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
                                          <xsl:value-of disable-output-escaping="yes" select="@code"/>
                                          <o:p></o:p>
                                        </span>
                                      </p>
                                    </td>
                                  </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Тип сведений<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@type"/>
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
                                                    ГРНИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip_date"/>
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
                    <xsl:if test="register[position()=last()]">
                        <xsl:for-each select="register">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. ОГРНИП<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@ogrnip_shown"/>
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
                                                    Дата постановки на учет в налоговом органе<o:p></o:p>
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
                                                    Причина постановки на учет<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@reg_reason"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@finish_date"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Причина снятия<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@finish_reason"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@reg_org"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРНИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip_date"/>
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
                    <xsl:if test="pf_reg[position()=last()]">
                        <xsl:for-each select="pf_reg">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. ОГРНИП<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@ogrnip_shown"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@lastname"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@firstname"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@middlename"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@pf_number"/>
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
                                                    Дата снятия с учета<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@finish_date"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@name_reg"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРНИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip_date"/>
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
                    <xsl:if test="fss_reg[position()=last()]">
                        <xsl:for-each select="fss_reg">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. ОГРНИП<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@ogrnip_shown"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@lastname"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@firstname"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@middlename"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@fss_number"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@first_reg_date"/>
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
                                                    Дата снятия с учета<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@finish_date"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@name_reg"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРНИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip_date"/>
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
                    <xsl:if test="foms_reg[position()=last()]">
                        <xsl:for-each select="foms_reg">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. ОГРНИП<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@ogrnip_shown"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@lastname"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@firstname"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@middlename"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@foms_number"/>
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
                                                    Дата снятия с учета<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@finish_date"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@name_reg"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРНИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip_date"/>
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
                    <xsl:if test="licence[position()=last()]">
                        <xsl:for-each select="licence">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. ОГРНИП<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@ogrnip_shown"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@lastname"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@firstname"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@middlename"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Номер лицензии<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@licence_number"/>
                                                    <o:p></o:p>
                                                </span>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@licence_date"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@licence_state"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@licence_org"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@begin_date"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@finish_date"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРНИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip_date"/>
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
                            Сведения о регистрации до 01.01.2004 г.<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="reg_before[position()=last()]">
                        <xsl:for-each select="reg_before">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. ОГРНИП<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@ogrnip_shown"/>
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
                                                    Наименование регоргана<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@reg_org"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРНИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip_date"/>
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
                            Сведения о записях в ЕГРИП<o:p></o:p>
                        </span>
                    </b>
                </p>
                <p align="center" style="text-align:center" class="MsoNormal">
                    <xsl:if test="rec[position()=last()]">
                        <xsl:for-each select="rec">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. ГРНИП<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@grnip"/>
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
                                                    ОГРНИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@ogrnip_shown"/>
                                                    <o:p></o:p>
                                                </span>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@lastname"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@firstname"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@middlename"/>
                                                    <o:p></o:p>
                                                </span>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip_date"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@status"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@event"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@rec_org"/>
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
                    <xsl:if test="cert[position()=last()]">
                        <xsl:for-each select="cert">
                            <br/>
                            <table width="650" cellspacing="0" cellpadding="0" border="1" style="border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
                                <tbody>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@no"/>. ОГРНИП<o:p></o:p>
                                                    </span>
                                                </b>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <b style="mso-bidi-font-weight:normal">
                                                    <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                        <xsl:value-of disable-output-escaping="yes" select="@ogrnip_shown"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@cert_ser"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Номер свидетельства<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@cert_num"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@cert_date"/>
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
                                                    <xsl:value-of disable-output-escaping="yes" select="@cert_org"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Состояние свидетельства<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@cert_state"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    ГРНИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip"/>
                                                    <o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="217" valign="top" style="width:162.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    Дата внесения записи в ЕГРИП<o:p></o:p>
                                                </span>
                                            </p>
                                        </td>
                                        <td width="433" valign="top" style="width:324.75pt;border:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
                                            <p align="left" style="text-align:left" class="MsoNormal">
                                                <span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:&quot;Times New Roman CYR&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">
                                                    <xsl:value-of disable-output-escaping="yes" select="@grnip_date"/>
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

            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>
