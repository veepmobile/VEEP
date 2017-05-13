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
      html = html + '&lt;td &gt;&lt;a href="#" onclick="CommonBargPager(' + (StartPage-1) + ')"&gt;&lt;&lt;&lt;/a&gt;&lt;/td&gt;';
      for (var i = StartPage;  i &lt; ((Page == PCount)? PCount*1+1 : ((StartPage+7 &lt; PCount)? StartPage+7: PCount*1+1)); i++)
      {
      if (i==Page)
      html = html + '&lt;td style="border-left:none;border-top:none;"&gt;'+ i + '-я страница&lt;/td&gt;';
      else
      html = html + '&lt;td style="border-left:none;border-top:none;"&gt;&lt;a href="#" onclick="CommonBargPager(' + i + ')"&gt;'+ i + '&lt;/a&gt;&lt;/td&gt;';
      }
      if (i &lt; PCount)
      html = html + '&lt;td style="border-left:none;border-top:none;"&gt;&lt;a href="#" onclick="CommonBargPager(' + (Page*1+4) + ')"&gt;&gt;&gt;&lt;/a&gt;&lt;/td&gt;';
      if (PCount &gt; 7)
      html = html + '&lt;td style="border-left:none;border-top:none;"&gt;(Всего: &lt;a href="#" id="amover" onclick="MoveTo(' + Page + ',' + PCount+')"&gt;' + PCount + ' страниц&lt;/a&gt;)&lt;/td&gt;' + '&lt;/tr&gt;&lt;/table&gt;';
      return html
    }
  </msxsl:script>
	<xsl:output method="html" version="4.0" encoding="UTF-8"/>

	<xsl:template name="showdocs">
		<!--xsl:if test="count(//Docs) = 0">
			<div class="error">
			<img src="/images/icon_error.gif" width="16" height="16" border="0"/>
			Нет данных соответствующих заданному условию</div>
		</xsl:if>
		<xsl:if test="//@CNT &gt; 0">
			
			<font class="minicaption">
				Всего найдено <xsl:value-of select="//@CNT"/> сообщений.
			</font>
		</xsl:if-->
		<xsl:choose>
			<xsl:when test="//@CNT &gt; 0">
				<div class="minicaption" style="margin-top:10px;margin-bottom:10px;">
					Всего найдено <xsl:value-of select="//@CNT"/> сообщений.
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
		<xsl:if test="Docs[position()=last()]">

			<table width="100%" cellpadding="0" cellspacing="0">
				<xsl:if test="../@isAct=0">
					<xsl:attribute name="border">1</xsl:attribute>
				</xsl:if>
				<xsl:for-each select="Docs">
					<xsl:if test="position()=1">
						<tr>
							<td class="table_caption" align="center" style="width:105px;" colspan="2">Дата</td>
							<td class="table_caption_left" >Вид документа (информации)</td>

						</tr>
						<xsl:if test="../../@isAct=1">
							<tr>
								<td class="table_shadow_left"  colspan="2">
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
								<td class="table_shadow">
									<img src="/images/null.gif" width="1" height="1" border="0"/>
								</td>
							</tr>
						</xsl:if>
					</xsl:if>
					<tr>
						<xsl:if test="../../@isAct=1">
							<xsl:choose>
								<xsl:when test="ceiling(position() div 2)- position() div 2 = 0">
									<xsl:attribute name="bgColor">#F0F0F0</xsl:attribute>
								</xsl:when>
								<xsl:otherwise>
									<xsl:attribute name="bgColor">#FFFFFF</xsl:attribute>
								</xsl:otherwise>
							</xsl:choose>
						</xsl:if>
						<td class="table_item" align="center"  style="width:20px;">
							<img src="/images/null.gif" border="0" width="5"/>
							<xsl:if test="@canbuy=1">
							<input type="checkbox"><xsl:attribute name="id">ch<xsl:value-of select="position()"/></xsl:attribute><xsl:attribute name="value"><xsl:value-of select="@id"/>_<xsl:choose><xsl:when test="string-length(@file_name) &gt;0">b</xsl:when><xsl:otherwise>n</xsl:otherwise></xsl:choose></xsl:attribute>
							</input>
							</xsl:if>
						</td>
						<td class="table_item" align="center" >
							<xsl:value-of select="@rd"/>
						</td>
						<td class="table_item_left" align="left">
							<img>
								<xsl:attribute name="src">
									<xsl:value-of select="@icon"/>
								</xsl:attribute>
							</img>
							<xsl:if test="@canbuy=0">
								<a target="_blank">
									<xsl:attribute name="href">
										/issuers/<xsl:value-of select="../../@ticker"/>/documents/<xsl:value-of select="@file_name"/>?doc_id=4&amp;id=<xsl:value-of select="@id"/>
									</xsl:attribute>
									<xsl:value-of select="@name"/>
								</a>
							</xsl:if>
							<xsl:if test="@canbuy=1">
								<a href="#">
									<xsl:attribute name="onclick">
										<xsl:choose>
									<xsl:when test="string-length(@file_name) &gt; 0">
										showbarg('<xsl:value-of select="@id"/>','<xsl:value-of select="@issuer_id"/>','<xsl:value-of select="@file_name"/>')
									</xsl:when>
									<xsl:otherwise>
										Shownews('<xsl:value-of select="@id"/>','','<xsl:value-of select="../../@iss"/>',0,1,0)
									</xsl:otherwise>
								</xsl:choose>
									</xsl:attribute>
									<xsl:value-of select="@name"/>
								</a>

							</xsl:if>
						</td>

					</tr>
				</xsl:for-each>
			</table>
			<br/>
			<br/>

		</xsl:if>
		<xsl:if test="PC/@PC &gt; 1">
			<table cellspacing="1">
				<tr>
					<xsl:value-of disable-output-escaping="yes" select="js:GenPages(string(PC/@PC),string(PC/@PG))"/>
				</tr>
			</table>
		</xsl:if>
		<xsl:if test="count(//Docs) &gt; 0">
			<table width="100%" cellspacing="4" cellpadding="0" border="0">
				<tr>
					<td valign="top" width="16">
						<img src="/images/icon_only_selected.gif" width="16" height="16" border="0"/>
					</td>
					<td>
						<a href="#">
							<xsl:attribute name="onclick">
								javascript:showSelectedBarg("<xsl:value-of select="//@iss"/>")
							</xsl:attribute>

							<b>Посмотреть выбранные документы</b>
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
								javascript:showOnPageBarg("<xsl:value-of select="//@iss"/>")
							</xsl:attribute>
							<b>Посмотреть все документы на странице</b>
						</a>

						<br/>
					</td>
				</tr>
			</table>
		</xsl:if>
		
		<script language="javascript" type="text/javascript">
			<![CDATA[ 
			$(function() {
					var sch = window.innerHeight ? window.innerHeight : document.documentElement.offsetHeight; 
					var scw = window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth; 
					var	ww,wh,top,left;
					ww=(scw<640)?scw-44:640;
					wh=(sch<480)?sch-30:480
					$( "#dialog_div" ).dialog({
						autoOpen: false,
						height: wh,
						width: ww,
						modal: false,
						resizable: true,
						draggable: true,
						closeOnEscape: true,
						close: function() {
							void(0);
						}
					});
					$("#prnb").unbind()
					$("#prnb").click(function(){
					   doPrint("dcontent");
					})
			});
			function showOnPageBarg(iss){
				$("input:checkbox").each(function(i){
					if(this.value.length>=34){
						this.checked=true;
					}
				});
					showSelectedBarg(iss);
			}
			function showSelectedBarg(iss){
				var ids="";
				showClock();
				$("input:checkbox:checked").each(function(i){
						ids+=this.value + ","	
				})
							$.post("/iss/modules/operations.asp", {action:19,iss:iss, ids:ids,is_barg:1},
								function(data){
									hideClock()
									$("#dcontent").html(data);
									
									$( "#dialog_div" ).dialog( "open" );
									$("#dialog_div").scrollTop(-5)
									
							});    
			}
      
            var CommonBargPager = function (PG) 
            {                     
                     var pnum = $('#pnum').val();
                     if (typeof (pnum) !== 'undefined') PG = pnum;
                      var Id = $('#tabId').val();                 
                      var DBeg = $('#dfrom').val();
                      var DEnd = $('#dto').val();                    
                      showClock();
                      $.get("/tab/", { "id": Id, "ticker": ISS, "PG": PG, "DBeg":DBeg,"DEnd":DEnd}, function (data) {
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
              var wintext = 'Введите номер страницы: <br/><input type="text" id="pnum"/> <input type="button" value="Перейти" onclick="CommonBargPager(1)"/>';                  
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