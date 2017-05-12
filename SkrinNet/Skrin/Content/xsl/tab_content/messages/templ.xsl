<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 	xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="urn:deitel:user" xmlns:js="javascript:code">
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
    
    <!--function MarkMatch(nl,KW_node)
    {
    var txt=nl.nextNode().text;
    var KW=String(KW_node.nextNode().text);
    if (KW.length>0){
    reg_expr = new RegExp("([^ ]*" + KW + "[^ ,.!?():-=]*)", "img");
    txt = txt.replace(reg_expr, "&lt;font class=\"search_text\"&gt;$1&lt;/font&gt;");
		}
		return txt
		}
		function Asterix(nl){
		var str="";
		try {
		str = nl.nextNode().text;
		}
		catch(e){str=String(nl.text)}
		finally{}
		re = new RegExp('[A-zА-я0-9]', "g");
		str = str.replace(re,"*");
		re = new RegExp('\n', "g");
		str = str.replace(re,"&lt;br&gt;");
		re = new RegExp("&lt;br&gt;&lt;br&gt;","g");
		str = str.replace(re,"&lt;br&gt;");

		return str
		}-->
	</msxsl:script>
	<xsl:output method="html" version="4.0" encoding="UTF-8"/>

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
		<div style="height:30px; background-color:#E6E6E6; width:98%">
			<form name="news_form" id="news_form" onkeypress="events_search(event);">
				<table cellspacing="3" cellpadding="0" width="100%">
					<tr>
						<td nowrap="yes">Период с:</td>
						<td>
             <input type="hidden" id="mind" class="system_form" >
               <xsl:attribute name="value">
                 <xsl:value-of select="../@mind"/>
               </xsl:attribute>
             </input>
             <input type="hidden"  id="maxd" class="system_form" >
               <xsl:attribute name="value">
                 <xsl:value-of select="../@maxd"/>
               </xsl:attribute>
             </input>
							<input type="text" name="dfrom" id="dfrom" class="system_form" style="width:70px;">
								<xsl:attribute name="value">
									<xsl:value-of select="../@d1"/>
								</xsl:attribute>
							</input>
						</td>
						<td>по:</td>
						<td>
							<input type="text" name="dto" id="dto" class="system_form" style="width:70px;">
								<xsl:attribute name="value">
									<xsl:value-of select="../@d2"/>
								</xsl:attribute>
							</input>
						</td>
						<td nowrap="yes">Тип события:  </td>
						<td>              
							<select name="eventstype" class="system_form" id="eventstype" >
								<option value="0">Все типы</option>
								<xsl:for-each select="event_types">
									<option>
										<xsl:attribute name="value">
											<xsl:value-of select="@id"/>
										</xsl:attribute>
										<xsl:if test="../../@type_id=@id">
											<xsl:attribute name="selected">yes</xsl:attribute>
										</xsl:if>
										<xsl:value-of select="@name"/>
									</option>
								</xsl:for-each>
							</select>
						</td>
						<!--td>
							Текст:
						</td>
						<td width="100%">
							<input type="text" name="kw" id="kw"  class="system_form" style="width:100%;">
								<xsl:attribute name="value">
									<xsl:value-of select="../@kw"/>
								</xsl:attribute>
							</input>
						</td-->
						<td valign="top" style="width:100%">
							<input type="button" class="system_form btns blue" value="НАЙТИ" onclick="CommonPager(1)"/>
						</td>
					</tr>
				</table>
			</form>
		</div>
		<xsl:choose>
			<xsl:when test="us/@RM &gt; 0">
				<div class="minicaption" style="margin-top:20px;margin-bottom:10px;">
					Всего найдено <xsl:value-of select="us/@RM"/> событий.
				</div>
			</xsl:when>
			<xsl:otherwise>
				<table class="notfound" width="100%">
					<tr>
						<td class="notfound">Нет данных соответствующих заданному условию</td>
					</tr>

				</table>
			</xsl:otherwise>
		</xsl:choose>
		<hr style="height:1px;color:#003399"/>
		<xsl:if test="us[position()=last()]">
			<div style="margin-top:25px;">
				<form name="info">
					<table width="100%" cellpadding="0" cellspacing="0" border="0">

						<xsl:for-each select="us">

							<tr>

								<td class="table_item_left" align="left">
									<table width="100%" cellspacing="0" cellpadding="0" border="0">
										<xsl:if test="position()=1">
											<tr>
												<td class="table_caption" align="center" style="width:145px;" colspan="2" >Дата</td>
												<td class="table_caption_left" >Вид документа (информации)</td>

											</tr>
											<tr>
												<td class="table_shadow_left" colspan="2" style="width:145px;">
													<img src="/images/null.gif" width="1" height="1" border="0"/>
												</td>
												<td class="table_shadow">
													<img src="/images/null.gif" width="1" height="1" border="0"/>
												</td>
											</tr>
										</xsl:if>
										<xsl:for-each select="a">
											<tr>
												<xsl:choose>
													<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">
														<xsl:attribute name="bgColor">#F0F0F0</xsl:attribute>
													</xsl:when>
													<xsl:otherwise>
														<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
													</xsl:otherwise>
												</xsl:choose>
												<td style="width:20px; white-spacing:nowrap;">
													<img src="/images/null.gif" border="0" width="5"/>
													<xsl:if test="string-length(et/@enr_news) &gt;=32">
														<input type="checkbox">
															<xsl:attribute name="value">
																<xsl:value-of select="@id"/>,<xsl:if test="string-length(et/Event_Close/@ECID)=32">
																	<xsl:value-of select="et/Event_Close/@ECID"/>,
																</xsl:if><xsl:if test="string-length(et/Event_Close/Close_Event/@CEID)=32">
																	<xsl:value-of select="et/Event_Close/Close_Event/@CEID"/>,
																</xsl:if>
															</xsl:attribute>
														</input>
													</xsl:if>
												</td>
												<td class="table_item_left" align="center" valign="top" nowrap="yes" style="width:125px;">
													<xsl:value-of select ="@rd"/>
												</td>

												<td class="table_item_left" align="left" >
													<xsl:attribute name="id">
														tn<xsl:value-of select="position()"/>
													</xsl:attribute>
													<xsl:choose>
														<xsl:when test="string-length(et/@enr_news) &gt;=32">
															<a href="#">
																<xsl:attribute name="onclick">
																	Showevent('<xsl:value-of select="//@kw"/>','<xsl:value-of select="//us/@ticker"/>','<xsl:value-of select="@id"/>',0)
																</xsl:attribute>
																<xsl:value-of select ="et/@name"/>
															</a>
														</xsl:when>
														<xsl:otherwise>
															<xsl:value-of select ="et/@name"/>
														</xsl:otherwise>
													</xsl:choose>
													<xsl:if test="string-length(et/Event_Close/@ec_date)=10">
														<table  cellspacing="0" cellpadding="0" border="0">
															<xsl:for-each select ="et/Event_Close">
																<tr>
																	<td class="data_comment" nowrap="yes" >
																		<xsl:value-of select ="@ec_date"/>
																	</td>
																	<td width="5px">
																		<img src="/images/null.gif" width="5"/>

																	</td>
																	<td class="data_comment" >
																		<xsl:attribute name="id">
																			tn<xsl:value-of select="position()"/>
																		</xsl:attribute>
																		<xsl:choose>
																			<xsl:when test="string-length(@ec_news) &gt;=32">
																				<a href="#" class="data_comment">
																					<xsl:attribute name="onclick">
																						Showevent('<xsl:value-of select="//@kw"/>','<xsl:value-of select="//us/@ticker"/>','<xsl:value-of select="@ECID"/>',0)
																					</xsl:attribute>
																					<xsl:value-of select ="@ec_headline"/>
																				</a>
																			</xsl:when>
																			<xsl:otherwise>
																				<xsl:value-of select ="@ec_headline"/>
																			</xsl:otherwise>
																		</xsl:choose>

																	</td>
																</tr>
															</xsl:for-each>
														</table>

													</xsl:if>
													<xsl:if test="string-length(et/Event_Close/Close_Event/@ce_date)=10">
														<table  cellspacing="0" cellpadding="0" border="0">
															<xsl:for-each select ="et/Event_Close/Close_Event">
																<tr>
																	<td class="data_comment" nowrap="yes" >
																		<xsl:value-of select ="@ce_date"/>
																	</td>
																	<td width="5px">
																		<img src="/images/null.gif" width="5"/>
																	</td>
																	<td class="data_comment" >
																		<xsl:attribute name="id">
																			tn<xsl:value-of select="position()"/>
																		</xsl:attribute>
																		<xsl:choose>
																			<xsl:when test="string-length(@ce_news) &gt;=32">
																				<a href="#" class="data_comment">
																					<xsl:attribute name="onclick">
																						Showevent('<xsl:value-of select="//@kw"/>','<xsl:value-of select="//us/@ticker"/>','<xsl:value-of select="@CEID"/>',0)
																					</xsl:attribute>
																					<xsl:value-of select ="@ce_headline"/>
																				</a>
																			</xsl:when>
																			<xsl:otherwise>
																				<xsl:value-of select ="@ce_headline"/>
																			</xsl:otherwise>
																		</xsl:choose>
																	</td>
																</tr>
															</xsl:for-each>
														</table>

													</xsl:if>
												</td>
											</tr>
										</xsl:for-each>
									</table>

								</td>


							</tr>
						</xsl:for-each>

					</table>



				</form>
			</div>
		</xsl:if>
		<div style="margin-top:35px;">
		<xsl:if test="us/@RM &gt; 30">
			<table cellspacing="1">
				<tr>
					<xsl:value-of disable-output-escaping="yes" select="js:GenPages(string(us/@PC),string(us/@PG))"/>
				</tr>
			</table>
		</xsl:if>
		</div>
		<br/>
		<xsl:if test="count(//us) &gt; 0">
			<table width="100%" cellspacing="4" cellpadding="0" border="0">
				<tr>
					<td valign="top" width="16">
						<img src="/images/icon_only_selected.gif" width="16" height="16" border="0"/>
					</td>
					<td>
						<a href="#">
							<xsl:attribute name="onclick">
								javascript:showSelectedEvents('<xsl:value-of select="PC/@KW"/>')
							</xsl:attribute>

							<b>Посмотреть выбранные события 
						</b>
						</a>

						<br/>
					</td>
				</tr>
				<tr>
					<td valign="top" width="16">
						<img src="/images/icon_only_selected.gif" width="16" height="16" border="0"/>
					</td>
					<td>
						<a href="#">
							<xsl:attribute name="onclick">
								javascript:showonpageEvents('<xsl:value-of select="PC/@KW"/>')
							</xsl:attribute>

							<b>Посмотреть все события на странице</b>
						</a>

						<br/>
					</td>
				</tr>
			</table>
		</xsl:if>
		
		<span class="data_comment limitation">
			ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и могут не отражать абсолютную актуальность и достоверность информации. Дополнительную информацию можно получить в разделе 
			<a onclick="TabSelect(event,20)" href="#">«Существенные факты»</a>
		</span>
		

		<script language="javascript" type="text/javascript">
			<![CDATA[ 
            
            var CommonPager = function (PG) 
            {                     
                     var pnum = $('#pnum').val();
                     if (typeof (pnum) !== 'undefined') PG = pnum;
                      var Id = $('#tabId').val(); 
                      var DBeg = $('#dfrom').val();
                      var DEnd = $('#dto').val();
                      var type_id=$("#eventstype").val();     
                      var kw = $('#kw').val();
                      showClock();
                      $.get("/tab/", { "id": Id, "ticker": ISS, "PG": PG, "DBeg":DBeg,"DEnd":DEnd,"Type_ID" : type_id , "KW":kw}, function (data) {
                              hideClock(); 
                              closeSpan();
                              $("#tab_content").html(data);
                          }, "html").fail(function (jqXHR, textStatus, errorThrown) {
                              hideClock();       
                              closeSpan()
                              $("#tab_content").html(textStatus);
                          });      				
	       		};
            
            var MoveTo = function(Page,Pcount){             
              var wintext = 'Введите номер страницы: <br/><input type="text" id="pnum"/> <input type="button" value="Перейти" onclick="CommonPager(1)"/>';                  
              showwin("info",wintext,0);
            };
            
            $(document).ready(function(){ 
                var mind = $('#mind').val();
                var maxd = $('#maxd').val();               
                var dates = $("#dfrom, #dto").datepicker({
			                                                     defaultDate: "-1m",
			                                                     changeMonth: true,
			                                                     showButtonPanel: true,
			                                                     changeYear: true,
			                                                     yearRange: mind.substr(6,4) + ':' + maxd.substr(6,4),
			                                                     onSelect: function( selectedDate ) {
				                                                                                    var option = this.id == "dfrom" ? "minDate" : "maxDate",
					                                                                                instance = $( this ).data( "datepicker" );
					                                                                                date = $.datepicker.parseDate(
						                                                                            instance.settings.dateFormat ||
						                                                                            $.datepicker._defaults.dateFormat,
						                                                                            selectedDate, instance.settings );
				                                                                                    dates.not( this ).datepicker( "option", option, date );
			                                                                              }
		                                                    });
            });
    
            

				]]>
		</script>
	</xsl:template>

</xsl:stylesheet>