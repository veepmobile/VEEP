<?xml version="1.0" encoding="utf-8" standalone="no" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"	xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="urn:deitel:user" xmlns:js="javascript:code">


  <xsl:output method="html" version="4.0" encoding="utf-8"/>

  <xsl:template match="/">
    <xsl:apply-templates/>
  </xsl:template>
  <xsl:decimal-format name="buh" decimal-separator="." grouping-separator=" " NaN="-"/>
  <xsl:template match="iss_profile">

    <!-- content -->

    <div id="t_content">
      <div style="color:#3a61ad;font-weight:bold;">СООБЩЕНИЕ О БАНКРОТСТВЕ</div>
    <xsl:apply-templates select="profile">
    </xsl:apply-templates>

    <hr></hr>
    <div class="explain" style="clear:both;padding-top:10px; ">
      ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника. В связи с особенностями функционирования и обновления, указанного источника информации АО «СКРИН» не может гарантировать полную актуальность и достоверность данных.
    </div>

    </div>
    <!-- end content -->
  </xsl:template>
  <xsl:template match="profile">
    <!--Карточка сообщения-->
    <table cellspacing="0" cellpadding="0" border="0">
      <tbody>
      <tr>
        <td>
          Источник данных: Единый федеральный реестр сведений о банкротстве.
          <xsl:for-each select="efrsb">
            <h3 style="margin-top:25px;">
              <xsl:value-of disable-output-escaping="yes" select="@mess_type"/>
              <!--xsl:call-template name="getMessType">
                <xsl:with-param name="MT_eng" select="xml_data/MessageData/MessageInfo/@MessageType"/>
              </xsl:call-template-->
            </h3>
            <h3>
              <xsl:value-of select="xml_data/MessageData/MessageInfo/CourtDecision/DecisionType/@Name"/>
            </h3>
            <hr/>
            <xsl:if test="annul">
              <xsl:value-of select="annul/@pre_text"/>
              <a href="#">
                <!--xsl:attribute name="href">?b_id=<xsl:value-of select="annul/@number"/-->
                <xsl:attribute name="onclick">
                  ShowBankrotUni(<xsl:value-of select="annul/@number"/>,1)
                </xsl:attribute>
                <xsl:value-of select="annul/@a_text"/>
              </a>
            </xsl:if>
            <table class="profile_table" cellspacing="0" cellpadding="0" border="0" style="float:none">
              <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ArbitralDecree'">
                <tr>
                  <th  style="width:40%;">Тип решения </th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/CourtDecision/DecisionType/@Name"/>
                  </td>
                </tr>


              </xsl:if>
              <tr>
                <th  style="width:40%;">№ сообщения</th>
                <td>
                  <xsl:value-of select="xml_data/MessageData/Number"/>
                </td>
              </tr>
              <tr>
                <th>Дата публикации</th>
                <td>
                  <xsl:call-template name="format-date">
                    <xsl:with-param name="date" select="xml_data/MessageData/PublishDate"/>
                  </xsl:call-template>
                </td>
              </tr>
              <xsl:if test="xml_data/MessageData/BankruptInfo/BankruptFirm">

                <tr>
                  <th>Наименование должника</th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/BankruptInfo/BankruptFirm/@FullName"/>
                  </td>
                </tr>
                <tr>
                  <th>Адрес</th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/BankruptInfo/BankruptFirm/@LegalAddress"/>
                  </td>
                </tr>

                <tr>
                  <th>ОГРН</th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/BankruptInfo/BankruptFirm/@OGRN"/>
                  </td>
                </tr>

                <tr>
                  <th>ИНН</th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/BankruptInfo/BankruptFirm/INN"/>
                  </td>
                </tr>
              </xsl:if>

              <xsl:if test="xml_data/MessageData/BankruptInfo/BankruptPerson">
                <tr>
                  <th>ФИО должника</th>
                  <td>
                    <xsl:value-of select="concat(xml_data/MessageData/BankruptInfo/BankruptPerson/@LastName, ' ', xml_data/MessageData/BankruptInfo/BankruptPerson/@FirstName, ' ', xml_data/MessageData/BankruptInfo/BankruptPerson/@MiddleName)"/>
                  </td>
                </tr>

                <xsl:if test="xml_data/MessageData/BankruptInfo/BankruptPerson/Birthdate">

                  <tr>
                    <th>Дата рождения</th>
                    <td>

                      <xsl:value-of select="xml_data/MessageData/BankruptInfo/BankruptPerson/Birthdate"/>


                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="xml_data/MessageData/BankruptInfo/BankruptPerson/Birthplace">
                  <tr>
                    <th>Место рождения</th>
                    <td>
                      <xsl:value-of select="xml_data/MessageData/BankruptInfo/BankruptPerson/Birthplace"/>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="@Person_Address">
                  <tr>
                    <th>Место жительства</th>
                    <td>
                      <xsl:value-of select="xml_data/MessageData/BankruptInfo/BankruptPerson/@Address"/>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="xml_data/MessageData/BankruptInfo/BankruptPerson/INN">
                  <tr>
                    <th>ИНН</th>
                    <td>
                      <xsl:value-of select="xml_data/MessageData/BankruptInfo/BankruptPerson/INN"/>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="xml_data/MessageData/BankruptInfo/BankruptPerson/SNILS">
                  <tr>
                    <th>СНИЛС</th>
                    <td>
                      <xsl:value-of select="xml_data/MessageData/BankruptInfo/BankruptPerson/SNILS"/>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="string-length(xml_data/MessageData/BankruptInfo/BankruptPerson/@OGRNIP) &gt; 0">
                  <tr>
                    <th>ОГРНИП</th>
                    <td>
                      <xsl:value-of select="xml_data/MessageData/BankruptInfo/BankruptPerson/@OGRNIP"/>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="xml_data/MessageData/BankruptInfo/BankruptPerson/NameHistory/NameHistoryItem">
                  <tr>
                    <th>Ранее имевшиеся ФИО</th>
                    <td>
                      <xsl:for-each select="xml_data/MessageData/BankruptInfo/BankruptPerson/NameHistory">
                        <xsl:value-of select="NameHistoryItem"/>
                        <br/>
                      </xsl:for-each>
                    </td>
                  </tr>
                </xsl:if>
              </xsl:if>
              <xsl:if test="xml_data/MessageData/CaseNumber">
                <tr>
                  <th>№ дела</th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/CaseNumber"/>
                  </td>
                </tr>
              </xsl:if>
              <xsl:if test="xml_data/MessageData/PublisherInfo/@PublisherType='OperatorCbrf'">
                <tr>
                  <th>Контрольный орган</th>
                  <td>
                    ЦБ РФ
                  </td>
                </tr>
              </xsl:if>
              <xsl:if test="xml_data/MessageData/PublisherInfo/ArbitrManager/@LastName">
                <tr>
                  <th>Арбитражный управляющий</th>
                  <td>
                    <xsl:value-of select="concat(xml_data/MessageData/PublisherInfo/ArbitrManager/@LastName , ' ' , xml_data/MessageData/PublisherInfo/ArbitrManager/@FirstName, ' ', xml_data/MessageData/PublisherInfo/ArbitrManager/@MiddleName, ' (ИНН:',xml_data/MessageData/PublisherInfo/ArbitrManager/@INN, ', СНИЛС: ', xml_data/MessageData/PublisherInfo/ArbitrManager/@SNILS, ')')"/>
                  </td>
                </tr>
              </xsl:if>
              <xsl:if test="xml_data/MessageData/PublisherInfo/ArbitrManager/Sro/SroName">
                <tr>
                  <th>СРО АУ</th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/PublisherInfo/ArbitrManager/Sro/SroName"/> (ИНН:<xsl:value-of select="xml_data/MessageData/PublisherInfo/ArbitrManager/Sro/INN"/>, ОГРН:<xsl:value-of select="xml_data/MessageData/PublisherInfo/ArbitrManager/Sro/OGRN"/>)
                  </td>
                </tr>
              </xsl:if>
              <xsl:if test="xml_data/MessageData/PublisherInfo/ArbitrManager/Sro/LegalAddress">

                <tr>
                  <th>Адрес СРО АУ</th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/PublisherInfo/ArbitrManager/Sro/LegalAddress"/>
                  </td>
                </tr>
              </xsl:if>
              
              <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ActReviewPersonDamages'   and string-length(@ActPersonDamagesMessageText)>0">
                <tr>
                  <th>
                    Акт о привлечении контролирующих лиц
                  </th>
                  <td>
                    <a href="#">
                      <!--xsl:attribute name="href">?b_id=<xsl:value-of select="xml_data/MessageData/MessageInfo/ActReviewPersonDamages/ActPersonDamagesMessageId"/-->
                      <xsl:attribute name="onclick">
                        ShowBankrotUni(<xsl:value-of select="xml_data/MessageData/MessageInfo/ActReviewPersonDamages/ActPersonDamagesMessageId"/>,1
                      </xsl:attribute>
                      <xsl:value-of select="@ActPersonDamagesMessageText"/>
                    </a>

                  </td>
                </tr>
              </xsl:if>
              <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ActReviewPersonSubsidiary' and string-length(@ActPersonSubsidiaryText)>0">
                <tr>
                  <th>
                    Акт о привлечении контролирующих лиц
                  </th>
                  <td>
                    <a href="#">
                      <!--xsl:attribute name="href">
                  ?b_id=<xsl:value-of select="xml_data/MessageData/MessageInfo/ActReviewPersonSubsidiary/ActPersonSubsidiaryId"/-->
                      <xsl:attribute name="onclick">
                        ShowBankrotUni(<xsl:value-of select="xml_data/MessageData/MessageInfo/ActReviewPersonSubsidiary/ActPersonSubsidiaryId"/>,1)
                      </xsl:attribute>
                      <xsl:value-of select="@ActPersonSubsidiaryText"/>
                    </a>

                  </td>
                </tr>
              </xsl:if>
              <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ActPersonDamages'  and string-length(@ActPersonDamagesText)>0">
                <tr>
                  <th>
                    Заявление  о привлечении контролирующих лиц
                  </th>
                  <td>
                    <a href="#">
                      <!--xsl:attribute name="href">
                  ?b_id=<xsl:value-of select="xml_data/MessageData/MessageInfo/ActPersonDamages/DeclarationPersonDamagesMessageId"/-->
                      <xsl:attribute name="onclick">
                        ShowBankrotUni(<xsl:value-of select="xml_data/MessageData/MessageInfo/ActPersonDamages/DeclarationPersonDamagesMessageId"/>,1)
                      </xsl:attribute>
                      <xsl:value-of select="@ActPersonDamagesText"/>
                    </a>

                  </td>
                </tr>
              </xsl:if>
              <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='BankOpenAccountDebtor'">
                <tr>
                  <th>
                    Наименование
                  </th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/BankOpenAccountDebtor/Name"/>
                  </td>
                </tr>
                 <tr>
                  <th>
                    ИНН
                  </th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/BankOpenAccountDebtor/Inn"/>
                  </td>
                </tr>
                <tr>
                  <th>
                    ОГРН
                  </th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/BankOpenAccountDebtor/Ogrn"/>
                  </td>
                </tr>
                <tr>
                  <th>
                    БИК
                  </th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/BankOpenAccountDebtor/Bik"/>
                  </td>
                </tr>
              </xsl:if>
              <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ActPersonSubsidiary'   and string-length(@DeclarationPersonSubsidiaryMessageText)>0">
                <tr>
                  <th>
                    Заявление  о привлечении контролирующих лиц
                  </th>
                  <td>
                    <a href="#">
                      <!--xsl:attribute name="href">
                  ?b_id=<xsl:value-of select="xml_data/MessageData/MessageInfo/ActPersonSubsidiary/DeclarationPersonDamagesMessageId"/-->
                      <xsl:attribute name="onclick">
                        ShowBankrotUni(<xsl:value-of select="xml_data/MessageData/MessageInfo/ActPersonSubsidiary/DeclarationPersonDamagesMessageId"/>,1)
                      </xsl:attribute>
                      <xsl:value-of select="@DeclarationPersonSubsidiaryMessageText"/>
                    </a>

                  </td>
                </tr>
              </xsl:if>
              <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ActReviewDealInvalid'  and string-length(@ActDealInvalidMessageText)>0">
                <tr>
                  <th>
                    Акт о признании/не признании сделки недействительной
                  </th>
                  <td>
                    <a href="#">
                      <!--xsl:attribute name="href">
                  ?b_id=<xsl:value-of select="xml_data/MessageData/MessageInfo/ActReviewDealInvalid/ActDealInvalidMessageId"/-->
                      <xsl:attribute name="onclick">
                        ShowBankrotUni(<xsl:value-of select="xml_data/MessageData/MessageInfo/ActReviewDealInvalid/ActDealInvalidMessageId"/>,1)
                      </xsl:attribute>
                      <xsl:value-of select="@ActDealInvalidMessageText"/>
                    </a>

                  </td>
                </tr>
              </xsl:if>
              <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ActDealInvalid'  and string-length(@DealInvalidMessageText)>0">
                <tr>
                  <th>
                    Заявление о признании сделки недействительной
                  </th>
                  <td>
                    <a href="#">
                      <!--xsl:attribute name="href">
                  ?b_id=<xsl:value-of select="xml_data/MessageData/MessageInfo/ActDealInvalid/DealInvalidMessageId"/-->
                      <xsl:attribute name="onclick">
                        ShowBankrotUni(<xsl:value-of select="xml_data/MessageData/MessageInfo/ActDealInvalid/DealInvalidMessageId"/>,1)
                      </xsl:attribute>
                      <xsl:value-of select="@DealInvalidMessageText"/>
                    </a>
                  </td>
                </tr>
                <tr>
                  <th>
                    Дата получения сведений о решении суда
                  </th>
                  <td>
                    <xsl:call-template name="format-date">
                      <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/ActDealInvalid/CourtDecisionNoticeDate"/>
                    </xsl:call-template>
                  </td>
                </tr>
              </xsl:if>
              
              <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ProcedureGrantingIndemnity'">
                <tr>
                  <th style ="width:40%">Имущество, предлагаемое в качестве отступного	</th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/ProcedureGrantingIndemnity/PropertyIndemnityOffer"/>
                   
                  </td>
                </tr>
                <tr>
                  <th>Порядок ознакомления с имуществом</th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/ProcedureGrantingIndemnity/PropertyFamiliarizationProcedure"/>
                  </td>
                </tr>
                <tr>
                  <th>Срок направления заявлений о согласии</th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/ProcedureGrantingIndemnity/ConsestApplicationPeriod"/>
                  </td>
                </tr>
              </xsl:if>
             
              <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='SaleOrderPledgedProperty'">
                <tr>
                  <th style ="width:40%">Дата и время торгов</th>
                  <td>
                    <xsl:call-template name="date-time">
                      <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/SaleOrderPledgedProperty/MeetingDate"/>
                    </xsl:call-template>
                  </td>
                </tr>
                <tr>
                  <th>Место проведения</th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/SaleOrderPledgedProperty/TradeSite"/>
                  </td>
                </tr>
              </xsl:if>
              <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='SaleContractResult2'">
                <tr>
                  <th>Торговая площадка </th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/SaleContractResult2/TradePlaceName"/>
                  </td>
                </tr>
                <tr>
                  <th>Номер торгов</th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/SaleContractResult2/TradeNumber"/>
                  </td>
                </tr>
              </xsl:if>
              <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='SaleContractResult'">
                <tr>
                  <th>Сведения о заключении договора</th>
                  <td>
                    <xsl:choose>
                      <xsl:when test="xml_data/MessageData/MessageInfo/SaleContractResult/SaleContractResultType='ContractWithWinner'">Заключение договора с победителем</xsl:when>
                      <xsl:when test="xml_data/MessageData/MessageInfo/SaleContractResult/SaleContractResultType='WinnerFailure'">Отказ или уклонение победителя от заключения договора</xsl:when>
                    </xsl:choose>
                  </td>
                </tr>
                <xsl:if test="string-length(xml_data/MessageData/MessageInfo/SaleContractResult/DateContract) &gt; 0">
                  <tr>
                    <th>Дата заключения договора</th>
                    <td>
                      <xsl:call-template name="format-date">
                        <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/SaleContractResult/DateContract"/>
                      </xsl:call-template>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="string-length(xml_data/MessageData/MessageInfo/SaleContractResult/Price) &gt; 0">
                  <tr>
                    <th>Цена приобретения имущества, руб.</th>
                    <td>
                      <xsl:value-of select="format-number(xml_data/MessageData/MessageInfo/SaleContractResult/Price,'# ###.00','buh')"/>
                    </td>
                  </tr>
                </xsl:if>

                <xsl:choose>
                  <xsl:when test="xml_data/MessageData/MessageInfo/SaleContractResult/SaleContractResultType='ContractWithWinner'">
                    <tr>
                      <th colspan="2">Информация о покупателе, с которым заключен договор</th>
                    </tr>

                    <tr>
                      <th>Наименование покупателя</th>
                      <td>
                        <xsl:value-of select="xml_data/MessageData/MessageInfo/SaleContractResult/PurchaserInfo/Name"/>
                      </td>
                    </tr>
                    <tr>
                      <th>ОГРН/ОГРНИП</th>
                      <td>
                        <xsl:value-of select="xml_data/MessageData/MessageInfo/SaleContractResult/PurchaserInfo/Ogrn"/>
                      </td>
                    </tr>
                    <tr>
                      <th>ИНН</th>
                      <td>
                        <xsl:value-of select="xml_data/MessageData/MessageInfo/SaleContractResult/PurchaserInfo/Inn"/>
                      </td>
                    </tr>
                  </xsl:when>
                  <xsl:when test="xml_data/MessageData/MessageInfo/SaleContractResult/SaleContractResultType='WinnerFailure'">
                    <tr>
                      <th colspan="2">
                        Информация о победителе, отказавшемся от исполнения договора
                      </th>
                    </tr>

                    <tr>
                      <th>Наименование покупателя</th>
                      <td>
                        <xsl:value-of select="xml_data/MessageData/MessageInfo/SaleContractResult/FailureWinnerInfo/Name"/>
                      </td>
                    </tr>
                    <tr>
                      <th>ОГРН/ОГРНИП</th>
                      <td>
                        <xsl:value-of select="xml_data/MessageData/MessageInfo/SaleContractResult/FailureWinnerInfo/Ogrn"/>
                      </td>
                    </tr>
                    <tr>
                      <th>ИНН</th>
                      <td>
                        <xsl:value-of select="xml_data/MessageData/MessageInfo/SaleContractResult/FailureWinnerInfo/Inn"/>
                      </td>
                    </tr>
                  </xsl:when>
                </xsl:choose>
              </xsl:if>
               <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ChangeAuction'">
                 <tr>
                   <th>Организатор торгов</th>
                   <td>
                     <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/PublisherInfo/FirmTradeOrganizerAgent/FirmTradeOrganizer/@ShortName"/>
                   </td>
                 </tr>
                 <tr>
                   <th>Вид торгов</th>
                   <td>
                   <xsl:call-template name="TradeTypes">
                     <xsl:with-param name="TT" select="xml_data/MessageData/MessageInfo/ChangeAuction/TradeType"/>
                   </xsl:call-template>
                   </td>
               </tr>
                 <tr>
                   <th>
                     Дата и время начала подачи заявок
                   </th>
                   <td>
                     <xsl:call-template name="date-time">
                       <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/ChangeAuction/Application/TimeBegin"/>
                     </xsl:call-template>
                     
                   </td>
               </tr>
                 <tr>
                   <th>Дата и время окончания подачи заявок
                   </th>
                   <td>
                     <xsl:call-template name="date-time">
                       <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/ChangeAuction/Application/TimeEnd"/>
                     </xsl:call-template>
                   </td>
               </tr>
                 <tr>
                   <th>Правила подачи заявок</th>
                   <td>
                    <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/ChangeAuction/Application/Rules"/>
                 </td>
               </tr>
                 <tr>
                   <th>Дата и время торгов</th>
                   <td>
                     
                   <xsl:call-template name="date-time">
                       <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/ChangeAuction/Date"/>
                     </xsl:call-template>
                 </td>
               </tr>
               <tr>
                   <th>Форма подачи предложения о цене</th>
                   <td>
                     <xsl:call-template name="PriceTypes">
                     <xsl:with-param name="TT" select="xml_data/MessageData/MessageInfo/ChangeAuction/PriceType"/>
                     </xsl:call-template>
                 </td>
               </tr>
                <tr>
                   <th>Место проведения</th>
                   <td>
                     
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/ChangeAuction/TradeSite"/>
                 </td>
               </tr>
                <tr>
                   <th>Измененное сообщение</th>
                   <td>
                    
                    <a href="#">
                      <xsl:attribute name="onclick">
                        ShowBankrotUni(<xsl:value-of select="xml_data/MessageData/MessageInfo/ChangeAuction/IdChangedMessage"/>,1)
                      </xsl:attribute>
                      <xsl:value-of select="@ChangeAuctionMessageText"/>
                    </a>

                 </td>
               </tr>
            </xsl:if>
              <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='Annul'   and string-length(@AnnuledMessageText)>0">
                <tr>
                  <th>
                    Аннулированное сообщение
                  </th>
                  <td>
                    <a href="#">
                      <!--xsl:attribute name="href">
                  ?b_id=<xsl:value-of select="xml_data/MessageData/MessageInfo/Annul/IdAnnuledMessage"/-->
                      <xsl:attribute name="onclick">
                        ShowBankrotUni(<xsl:value-of select="xml_data/MessageData/MessageInfo/Annul/IdAnnuledMessage"/>,1)
                      </xsl:attribute>
                      <xsl:value-of select="@AnnuledMessageText"/>
                    </a>

                  </td>
                </tr>
              </xsl:if>
              <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='CancelAuctionTradeResult'   and string-length(@CanceledMessageText)>0">
                <tr>
                  <th>
                    Отмененное сообщение
                  </th>
                  <td>
                    <a href="#">
                      <!--xsl:attribute name="href">
                  ?b_id=<xsl:value-of select="xml_data/MessageData/MessageInfo/Annul/IdAnnuledMessage"/-->
                       <xsl:attribute name="onclick">
                      ShowBankrotUni(<xsl:value-of select="xml_data/MessageData/MessageInfo/CancelAuctionTradeResult/IdCanceledMessage"/>,1)
                      </xsl:attribute>
                      <xsl:value-of select="@CanceledMessageText"/>
                    </a>

                  </td>
                </tr>
              </xsl:if>
              <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='CancelDeliberateBankruptcy'   and string-length(@CancelDeliberateBankruptcyMessageText)>0">
                <tr>
                  <th>
                    Отмененное сообщение
                  </th>
                  <td>
                    <a href="#">
                       <xsl:attribute name="onclick">
                      ShowBankrotUni(<xsl:value-of select="xml_data/MessageData/MessageInfo/CancelDeliberateBankruptcy/IdCanceledMessage"/>,1)
                      </xsl:attribute>
                      <xsl:value-of select="@CancelDeliberateBankruptcyMessageText"/>
                    </a>

                  </td>
                </tr>
              </xsl:if>  
                 
              <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='TradeResult'  and string-length(@AuctionMessageText)>0">
                <tr>
                  <th>
                    Объявление о проведении торгов
                  </th>
                  <td>
                    <a href="#">
                      <!--xsl:attribute name="href">
                  ?b_id=<xsl:value-of select="xml_data/MessageData/MessageInfo/TradeResult/IdAuctionMessage"/-->
                      <xsl:attribute name="onclick">
                        ShowBankrotUni(<xsl:value-of select="xml_data/MessageData/MessageInfo/TradeResult/IdAuctionMessage"/>,1)
                      </xsl:attribute>
                      <xsl:value-of select="@AuctionMessageText"/>
                    </a>

                  </td>
                </tr>
              </xsl:if>
              <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='DeliberateBankruptcy'">
                <tr>
                  <th>Признаки преднамеренного банкротства</th>
                  <td>
                    <xsl:choose>
                      <xsl:when test="xml_data/MessageData/MessageInfo/DeliberateBankruptcy/DeliberateBankruptcySigns='NotFound'">
                        Не выявлены
                      </xsl:when>
                      <xsl:otherwise>
                        Выявлены
                      </xsl:otherwise>
                    </xsl:choose>

                  </td>
                </tr>
                <tr>
                  <th>Признаки фиктивного банкротства</th>
                  <td>
                    <xsl:choose>
                      <xsl:when test="xml_data/MessageData/MessageInfo/DeliberateBankruptcy/FakeBankruptcySigns='NotFound'">
                        Не выявлены
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:value-of select="xml_data/MessageData/MessageInfo/DeliberateBankruptcy/FakeSignsNotSearchedReason"/>
                      </xsl:otherwise>
                    </xsl:choose>
                  </td>
                </tr>
              </xsl:if>
              <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ChangeDeliberateBankruptcy'">
                <tr>
                  <th>Признаки преднамеренного банкротства</th>
                  <td>
                    <xsl:choose>
                      <xsl:when test="xml_data/MessageData/MessageInfo/ChangeDeliberateBankruptcy/DeliberateBankruptcySigns='NotFound'">
                        Не выявлены
                      </xsl:when>
                      <xsl:otherwise>
                        Выявлены
                      </xsl:otherwise>
                    </xsl:choose>

                  </td>
                </tr>
                <tr>
                  <th>Признаки фиктивного банкротства</th>
                  <td>
                    <xsl:choose>
                      <xsl:when test="xml_data/MessageData/MessageInfo/ChangeDeliberateBankruptcy/FakeBankruptcySigns='NotFound'">
                        Не выявлены
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:value-of select="xml_data/MessageData/MessageInfo/ChangeDeliberateBankruptcy/FakeSignsNotSearchedReason"/>
                      </xsl:otherwise>
                    </xsl:choose>
                  </td>
                </tr>
                <tr>
                  <th>Измененное сообщение</th>
                  <td>
                    <a href="#">
                      <xsl:attribute name="onclick">
                        ShowBankrotUni(<xsl:value-of select="xml_data/MessageData/MessageInfo/ChangeDeliberateBankruptcy/IdChangedMessage"/>,1)
                      </xsl:attribute>
                      <xsl:value-of select="@ChangedMessageText"/>
                    </a>
                  </td>
                </tr>
              </xsl:if>
            </table>
             <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='PartBuildMonetaryClaim'">
               <table class="profile_table" cellspacing="0" cellpadding="0" border="0" style="float:none;margin-top:15px;">
                 <tr>
                   <th  style="width:40%;">
                     Арбитражный суд, рассматривающий дело о банкротстве
                   </th>
                   <td>
                   <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/PartBuildMonetaryClaim/ArbitralCourt"/>
                   </td>
                 </tr>
                <tr>
                   <th  style="width:40%;">
                     Последствия не предъявления требований
                   </th>
                   <td>
                   <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/PartBuildMonetaryClaim/Consequences"/>
                   </td>
                 </tr>
               </table>
               <b>Текст:</b>
               <br/>
               <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/PartBuildMonetaryClaim/Text"/>
              </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ReducingSizeShareCapital'">
              <table class="profile_table" cellspacing="0" cellpadding="0" border="0" style="float:none;margin-top:15px;">
                <tr>
                  <th style="width:40%;">
                    Уставной капитал уменьшен до суммы
                  </th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/ReducingSizeShareCapital/CharterCapitalSum"/>
                  </td>
                </tr>
                <tr>
                  <th style="width:40%;">
                    Дата принятия нормативного акта Банка России

                  </th>
                  <td>
                    <xsl:call-template name="format-date">
                      <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/ReducingSizeShareCapital/NormativeActAdoptionDate" />
                    </xsl:call-template>
                  </td>
                </tr>
                <tr>
                  <th style="width:40%;">
                    Отменено решение банка об увеличении размера уставного капитала
                  </th>
                  <td>
                    <xsl:choose>
                      <xsl:when test="xml_data/MessageData/MessageInfo/ReducingSizeShareCapital/IncreaseCapitalDecisionCanceled = 'false'">Нет</xsl:when>
                      <xsl:when test="xml_data/MessageData/MessageInfo/ReducingSizeShareCapital/IncreaseCapitalDecisionCanceled = 'true'">Да</xsl:when>
                    </xsl:choose>
                  </td>
                </tr>
              </table>
              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/ReducingSizeShareCapital/Text"/>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ViewExecRestructuringPlan'">
              <table class="profile_table" cellspacing="0" cellpadding="0" border="0" style="float:none;margin-top:15px;">
                <tr>
                  <th style="width:40%;">Дата окончания исполнения плана реструктуризации</th>
                  <td>
                    <xsl:call-template name="format-date">
                      <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/ViewExecRestructuringPlan/DateEndRestructuringPlanExecution" />
                    </xsl:call-template>
                  </td>
                </tr>
                <tr>
                  <th>
                    Место ознакомления
                  </th>
                  <td>
                    <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/ViewExecRestructuringPlan/PlaceOfAcquaintance"/>
                  </td>
                </tr>
                <tr>
                  <th colspan="2">
                    <xsl:choose>
                      <xsl:when test="xml_data/MessageData/MessageInfo/ViewExecRestructuringPlan/IncludedRegistryRequirmentsNotSatisfied='false'">
                        Требования кредиторов, включенные в план реструктуризации долгов гражданина, на дату рассмотрения отчета удовлетворены полностью.
                      </xsl:when>
                      <xsl:otherwise>
                        Требования кредиторов, включенные в план реструктуризации долгов гражданина, на дату рассмотрения отчета удовлетворены не полностью.
                      </xsl:otherwise>
                    </xsl:choose>
                  </th>
                 
                </tr>
              </table>
             
              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/ViewExecRestructuringPlan/Text"/>
            </xsl:if>
           
            
            
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='BankOpenAccountDebtor'">
              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/BankOpenAccountDebtor/Text"/>
            </xsl:if>
            
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='MeetingPartBuildResult'">
              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/MeetingPartBuildResult/Text"/>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ProcedureGrantingIndemnity'">
              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/ProcedureGrantingIndemnity/Text"/>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='RightUnsoldAsset'">
              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/RightUnsoldAsset/Text"/>
            </xsl:if>
            
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='MeetingParticipantsBuilding'">
                <table class="profile_table" cellspacing="0" cellpadding="0" border="0" style="float:none;margin-top:15px;">
                <tr>
                  <th style="width:40%;">Дата и время начала собрания</th>
                  <td>
                    <xsl:call-template name="date-time">
                      <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/MeetingParticipantsBuilding/MeetingDate" />
                    </xsl:call-template>
                  </td>
                </tr>
                <tr>
                  <th>Место проведения</th>
                  <td>
                    <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/MeetingParticipantsBuilding/MeetingSite"/>
                  </td>
                </tr>
                <tr>
                  <th>Повестка собрания участников строительства</th>
                  <td>
                    <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/MeetingParticipantsBuilding/Notice"/>
                  </td>
                </tr>
                <tr>
                  <th>Порядок ознакомления с материалами</th>
                  <td>
                    <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/MeetingParticipantsBuilding/MaterialsFamiliarizationOrder"/>
                  </td>
                </tr>
                <tr>
                  <th>Порядок регистрации участников собрания</th>
                  <td>
                    <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/MeetingParticipantsBuilding/MembersRegistrationOrder"/>
                  </td>
                </tr>
              </table>
              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/MeetingParticipantsBuilding/Text"/>
              
            </xsl:if>
            
            
            
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='MeetingResult'">
              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/MeetingResult/Text"/>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/Other/Text"/>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='Meeting'">
              <table class="profile_table" cellspacing="0" cellpadding="0" border="0" style="float:none;">

                <xsl:if test="xml_data/MessageData/MessageInfo/Meeting/MeetingDate">
                  <tr>
                    <th style="width:40%;">Дата и время начала собрания</th>
                    <td>
                      <xsl:call-template name="date-time">
                        <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/Meeting/MeetingDate"/>
                      </xsl:call-template>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="xml_data/MessageData/MessageInfo/Meeting/MeetingForm">
                  <tr>
                    <th>Форма проведения</th>
                    <td>
                      <xsl:value-of select="xml_data/MessageData/MessageInfo/Meeting/MeetingForm"/>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="xml_data/MessageData/MessageInfo/Meeting/BallotsReceptionEndDate">
                  <tr>
                    <th>Дата окончания приема бюллетеней</th>
                    <td>
                      <xsl:call-template name="date-time">
                        <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/Meeting/BallotsReceptionEndDate"/>
                      </xsl:call-template>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="xml_data/MessageData/MessageInfo/Meeting/MeetingDate">
                  <tr>
                    <th>Web-адрес для проведения электронного собрания</th>
                    <td></td>
                  </tr>
                </xsl:if>
                <xsl:if test="xml_data/MessageData/MessageInfo/Meeting/MeetingSite">
                  <tr>
                    <th>Место проведения</th>
                    <td>
                      <xsl:value-of select="xml_data/MessageData/MessageInfo/Meeting/MeetingSite"/>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="xml_data/MessageData/MessageInfo/Meeting/RegistrationDate">
                  <tr>
                    <th>Дата регистрации</th>
                    <td>
                      <xsl:call-template name="format-date">
                        <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/Meeting/RegistrationDate"/>
                      </xsl:call-template>
                      с
                      <xsl:call-template name="time-format">
                        <xsl:with-param name="time" select="xml_data/MessageData/MessageInfo/Meeting/RegistrationTimeBegin"/>
                      </xsl:call-template>
                      по
                      <xsl:call-template name="time-format">
                        <xsl:with-param name="time" select="xml_data/MessageData/MessageInfo/Meeting/RegistrationTimeEnd"/>
                      </xsl:call-template>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="xml_data/MessageData/MessageInfo/Meeting/RegistrationSite">
                  <tr>
                    <th>Место регистрации</th>
                    <td>
                      <xsl:value-of select="xml_data/MessageData/MessageInfo/Meeting/RegistrationSite"/>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="xml_data/MessageData/MessageInfo/Meeting/ExaminationDate">
                  <tr>
                    <th>Дата ознакомления</th>
                    <td>
                      <xsl:call-template name="date-time">
                        <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/Meeting/ExaminationDate"/>
                      </xsl:call-template>
                      <b>Комментарий:</b>
                      <xsl:value-of select="xml_data/MessageData/MessageInfo/Meeting/Comment"/>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="xml_data/MessageData/MessageInfo/Meeting/ExaminationSite">
                  <tr>
                    <th>Место ознакомления</th>
                    <td>
                      <xsl:value-of select="xml_data/MessageData/MessageInfo/Meeting/ExaminationSite"/>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="xml_data/MessageData/MessageInfo/Meeting/FuMailAddress">
                  <tr>
                    <th>Почтовый адрес арбитражного управляющего</th>
                    <td>
                      <xsl:value-of select="xml_data/MessageData/MessageInfo/Meeting/FuMailAddress"/>
                    </td>
                  </tr>
                </xsl:if>
              </table>
              <xsl:if test="xml_data/MessageData/MessageInfo/Meeting/Text">
                <b>Текст:</b>
                <br/>
                <xsl:value-of select="xml_data/MessageData/MessageInfo/Meeting/Text" disable-output-escaping="yes"/>
              </xsl:if>

            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='SaleContractResult2'">
              <b>Заключенные договоры</b>
              
                <xsl:for-each select="xml_data/MessageData/MessageInfo/SaleContractResult2/Contracts">
                  <table class="profile_table">
                  <tr>
                    <th>Номер лота</th>
                    <td>
                      <xsl:value-of select="SaleContractInfo/LotNumber"/>
                    </td>
                  </tr>
                  <tr>
                    <th>Описание</th>
                    <td>
                      <xsl:value-of select="SaleContractInfo/Description"/>
                    </td>
                  </tr>
                  <tr>
                    <th>Сведения о заключении договора	
                  </th>
                    <td><xsl:value-of select="SaleContractInfo/ConclusionInfo"/></td>
                  </tr>
                  <tr>
                    <th>Номер договора</th>
                    <td>
                      <xsl:value-of select="SaleContractInfo/Number"/>
                    </td>
                  </tr>
                  <tr>
                    <th>Дата договора</th>
                    <td>
                      <xsl:call-template name="format-date">
                        <xsl:with-param name="date" select="SaleContractInfo/ConclusionDate"/>
                      </xsl:call-template>
                      
                    </td>
                  </tr>
                  <tr>
                    <th>Цена приобретения имущества, руб.</th>
                    <td>
                      <xsl:value-of select="format-number(SaleContractInfo/PropertyPurchasePrice,'# ###.00','buh')"/>
                    </td>
                  </tr>
                
                 
                    </table>
                  Информация о покупателе, с которым заключен договор
                  <table class="profile_table">
                    <tr>
                      <th>Наименование покупателя</th>
                      <td>
                        <xsl:value-of select="SaleContractInfo/PurchaserInfo/Name"/>
                      </td>
                    </tr>
                    <xsl:if test="string-length(SaleContractInfo/PurchaserInfo/Ogrn) &gt; 0">
                        <tr>
                      <th>ОГРН</th>
                      <td>
                        <xsl:value-of select="SaleContractInfo/PurchaserInfo/Ogrn"/>
                      </td>
                    </tr>
                      </xsl:if>

                      <xsl:if test="string-length(SaleContractInfo/PurchaserInfo/Inn) &gt; 0">
                         <tr>
                      <th>ИНН</th>
                      <td>
                        <xsl:value-of select="SaleContractInfo/PurchaserInfo/Inn"/>
                      </td>
                    </tr>
                        
                      </xsl:if>
                          <xsl:if test="string-length(SaleContractInfo/PurchaserInfo/INN) &gt; 0">
                              <tr>
                      <th>ИНН</th>
                      <td>
                        <xsl:value-of select="SaleContractInfo/PurchaserInfo/INN"/>
                      </td>
                    </tr>
                      </xsl:if>
                 </table>
                </xsl:for-each>
              

              <b>Текст:</b>
              <br/>
              <xsl:value-of select="Text" disable-output-escaping="yes"/>
            </xsl:if>
            
            
            
            
            
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='CancelDeliberateBankruptcy'">
             <xsl:if test="xml_data/MessageData/MessageInfo/CancelDeliberateBankruptcy/Text">
                <b>Текст</b>
                <br/>
                <xsl:value-of select="xml_data/MessageData/MessageInfo/CancelDeliberateBankruptcy/Text" disable-output-escaping="yes"/>
              </xsl:if>
            
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ChangeAuction'">
              <b>Причина изменения:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/ChangeAuction/ChangeReason"/> <br/>
              <b>Текст:</b>
              <br/>
                  <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/ChangeAuction/Text"/>
              <xsl:if test="xml_data/MessageData/MessageInfo/ChangeAuction/LotTable">
              <table class="profile_table">
                <tr>
                  <th>Номер лота</th>
                  <th>Описание</th>
                  <th>Начальная цена, руб.</th>
                  <th>Шаг</th>
                  <th>Задаток</th>
                  <th>Информация о снижении цены</th>
                  <th>Классификация имущества</th>
                </tr>
                <xsl:for-each select="xml_data/MessageData/MessageInfo/ChangeAuction/LotTable/AuctionLot">
                  <tr>
                      <td>
                        <xsl:value-of select="Order"/>
                      </td>
                      <td>
                        <xsl:value-of select="Description"/>
                      </td>
                      <td>
                        <xsl:value-of select="format-number(StartPrice,'# ###.00','buh')"/>
                      </td>
                      <td>
                        <xsl:value-of select="format-number(Step,'# ###.00','buh')"/><xsl:if test="string-length(Step) &gt; 0">
                          <xsl:call-template name="units"><xsl:with-param name="unit_txt" select="AuctionStepUnit"/></xsl:call-template>
                      </xsl:if>
                      </td>
                      <td>
                        <xsl:value-of select="Advance"/><xsl:call-template name="units"><xsl:with-param name="unit_txt" select="AdvanceStepUnit"/></xsl:call-template>
                      </td>
                    <td>
                      <xsl:value-of select="PriceReduction"/>
                    </td>
                      <td>
                        <xsl:value-of select="ClassifierCollection/AuctionLotClassifier/Name"/>
                      </td>
                    </tr>
                
                </xsl:for-each>
              
              </table>
              </xsl:if>
              <br/><b>Дополнительная информация</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/ChangeAuction/AdditionalText"/>
              <br/>
              
              <xsl:for-each select="aucion_end">
                  <br/>Сообщение о результатах торгов <a href="#">
                <xsl:attribute name="onclick">
                  ShowBankrotUni(<xsl:value-of select="@number"/>,1)
                </xsl:attribute>
               <xsl:value-of select="@a_txt"/>
              </a>
              
              </xsl:for-each>
              
            
            </xsl:if>
             <!--xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ChangeAuction'">
                 <tr>
                   <th>Организатор торгов</th>
                   <td>
                     <xsl:value-of select="xml_data/MessageData/MessageInfo/ChangeAuction/Text"/>
                   </td>
                 </tr>
                 <tr>
                   <th>Вид торгов</th>
                   <td>
                   <xsl:call-template name="TradeTypes">
                     <xsl:with-param name="TT" select="xml_data/MessageData/MessageInfo/ChangeAuction/TradeType"/>
                   </xsl:call-template>
                   </td>
               </tr>
                 <tr>
                   <th>
                     Дата и время начала подачи заявок:
                   </th>
                   <td>
                     <xsl:call-template name="date-time">
                       <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/ChangeAuction/TimeBegin"/>
                     </xsl:call-template>
                   </td>
               </tr>
                 <tr>
                   <th>Дата и время окончания подачи заявок:
                   </th>
                   <td>
                     <xsl:call-template name="date-time">
                       <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/ChangeAuction/TimeEnd"/>
                     </xsl:call-template>
                   </td>
               </tr>
                 <tr>
                   <th>Правила подачи заявок</th>
                   <td>
                     
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/ChangeAuction/Rules"/>
                 </td>
               </tr>
                 <tr>
                   <th>Дата и время торгов</th>
                   <td>
                     
                   <xsl:call-template name="date-time">
                       <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/ChangeAuction/Date"/>
                     </xsl:call-template>
                 </td>
               </tr>
               <tr>
                   <th>Форма подачи предложения о цене</th>
                   <td>
                     <xsl:call-template name="PriceTypes">
                     <xsl:with-param name="TT" select="PriceType"/>
                     </xsl:call-template>
                 </td>
               </tr>
                <tr>
                   <th>Место проведения</th>
                   <td>
                     
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/ChangeAuction/TradeSite"/>
                 </td>
               </tr>
                <tr>
                   <th>Измененное сообщение</th>
                   <td>
                    <xsl:value-of select="ChangeAuctionMessageText"/>
                 </td>
               </tr>
            
               
            
            </xsl:if-->
            <!--MessageInfo MessageType="ChangeAuction">
        <ChangeAuction>
          <Text>Организатор торгов – ООО "Ермак"  (ИНН 2901252720,  КПП 290101001 Адрес: 163000 г. Архангельск, ул. Серафимовича, д. 30, оф. 204, тел. 88182243027, эл. адрес: ermak7414@)yandex.ru) действующее по поручению конкурсного управляющего ООО "Комфорт"  (164894, Архангельская область, Онежский район, рп. Малошуйка, ул. Ленина, д. 87, ИНН 2906006421, ОГРН 1062920003882), текущая процедура банкротства - конкурсное производство - открыта на основании решения Арбитражного суда Архангельской области от 24 ноября 2015 г. по делу № А05-4445/2015) Багрецовой Еленой Константиновной (СНИЛС 043-074-714-30, ИНН 292600357344, 163071, г. Архангельск, ул. Воскресенская, 95-492), член Союз СРО АУ "Северо-Запада" (адрес: г. Санкт-Петербург, ул. Смольного 1/3, подъезд 6, № реестровой записи 001-3 от 19 декабря 2002 года, ИНН 7825489593, ОГРН 1027809209471), сообщает о реализации имущества ООО "Комфорт" на электронных торгах в форме открытого аукциона с закрытой формой представления предложений о цене. Предмет торгов: Лот № 1: Торговое оборудование (62 наименования - перечень оборудования указан в сообщении № 901812 от 21.01.2016 года на сайте Единого Федерального реестра сведений о банкротстве http://www.fedresurs.ru,), начальная цена 1 464 682.87 рублей без НДС, размер задатка: 10 % начальной цены. Лот № 2: Автотопливозаправщик АТЗ-5633 ПТС 16 НМ 200080 от 18.04.2012 г., VIN Х89594321С0DM6072, 2012 года выпуска, оранжевый, начальная цена 1 600 000.00 рублей без НДС, размер задатка: 10% начальной цены. Залогом имущества, указанного в лотах, обеспечено требование конкурсного кредитора ПАО «Сбербанк России» . Имущество находится по адресу ООО "Комфорт". С предметом торгов можно ознакомиться по адресу Архангельская область, Онежский район, рп. Малошуйка, по предварительной записи у организатора торгов. Дополнительную информацию об имуществе можно получить у организатора торгов по телефону или эл. почте.&lt;br /&gt;Аукцион проводится на электронной торговой площадке ОАО «Российский аукционный дом» , сайт  www.lot-online.ru. &lt;br /&gt; Представление заявок на участие в торгах производится оператору электронной площадки по адресу www.lot-online.ru. ежедневно в рабочие дни с 10-00 до 15-00. Начало приема заявок 28.03.2016 г. с 10:00. Окончание приема заявок 04.05.2016 г. в 10:00 Заявка на участие в торгах оформляется в электронной форме, на русском языке и должна содержать следующие сведения: 1) обязательство участника открытых торгов соблюдать требования, указанные в сообщении о проведении открытых торгов; 2)  наименование, место  нахождения, почтовый адрес (для юридического лица), фамилию, имя, отчество, паспортные данные, сведения о месте жительства (для физического лица), номер контактного телефона, адрес электронной почты заявителя; 3) предложение о цене имущества; 4) сведения о наличии или отсутствии заинтересованности заявителя по отношению к должнику, кредиторам, конкурсному управляющему и о характере этой заинтересованности, сведения об участии в капитале заявителя конкурсного управляющего, а также Союза СРО АУ "Северо-Запада" членом или руководителем которой является конкурсный управляющий; 5) номер контактного телефона, адрес электронной почты заявителя. К заявке должны прилагаться: 1) выписка из ЕГРЮЛ (для юридического лица) , выписка из ЕГРИП (для ИП),  копии документов, удостоверяющих личность (для физического лица); надлежащим образом заверенный перевод на русский язык документов о государственной регистрации юридического лица или государственной регистрации физического лица в качестве индивидуального предпринимателя в соответствии с законодательством соответствующего государства (для иностранного лица); 2) документ, подтверждающий полномочия лица на осуществление действий от имени заявителя. Документы, прилагаемые к заявке, представляются в форме электронных документов, подписанных электронной цифровой подписью.  Задаток вносится до даты подачи заявки на участие в торгах и не позднее 3 дней до даты окончания приема заявок  по следующим реквизитам: Получатель: ООО «Ермак»  ИНН 2901252720,  КПП 290101001 р/с 40702810722100000438 в ПАО АКБ «АВАНГАРД» г. Москва к/с 301018100000000000201,БИК 044525201 В назначении платежа необходимо указать: «Задаток для участия в торгах по продаже имущества ООО "Комфорт", проводимых «__» ______ ___ г. на ЭТП www.lot-online.ru,, лот № __». Организатор торгов заключает договоры о задатке с участниками торгов&lt;br /&gt;Оператор электронной площадки регистрирует представленную заявку в журнале заявок на участие в торгах, присвоив заявке порядковый номер в указанном журнале, направляет заявителю в электронной форме подтверждение о регистрации представленной заявки на участие в торгах.   Заявитель вправе изменить или отозвать свою заявку на участие в торгах в любое время до окончания срока представления заявок на участие в торгах, направив об этом уведомление оператору электронной площадки.&lt;br /&gt; Не позднее одного часа с момента окончания представления заявок на участие в торгах оператор электронной площадки направляет Организатору торгов все зарегистрированные заявки, представленные до истечения установленного срока окончания представления заявок.&lt;br /&gt;Решение Организатора торгов о допуске заявителей к участию в открытых торгах принимается в течение пяти дней по результатам рассмотрения всех представленных заявок на участие в открытых торгах и оформляется протоколом об определении участников торгов. &lt;br /&gt; Решение об отказе в допуске заявителя к участию в торгах принимается в случае, если:&lt;br /&gt;1) заявка на участие в торгах не соответствует требованиям, установленным настоящим Порядком;&lt;br /&gt;2) представленные заявителем документы не соответствуют установленным к ним требованиям или сведения, содержащиеся в них, недостоверны;3) поступление задатка на счета, указанные в сообщении о проведении торгов, не подтверждено на дату составления протокола об определении участников торгов.&lt;br /&gt;Организатор торгов рассматривает предложения участников о цене имущества и определяет победителя торгов 05.05.2016 г. с 10:00 до 11:00. Победителем аукциона признается участник, предложивший наиболее высокую цену за имущество, включенное в лот (не ниже начальной цены). В случае, если была предложена цена продажи, равная цене, предложенной другим (другими) участником (участниками) торгов, победителем торгов признается участник, представивший  первым предложение о цене продажи имущества должника. Если к участию в торгах был допущен только один участник, заявка которого на участие в торгах содержит предложение о цене имущества должника не ниже установленной начальной цены имущества должника, договор купли-продажи заключается организатором торгов с этим участником торгов в соответствии с представленным им Предложением о цене имущества должника. В случае, если не были представлены заявки на участие в торгах, организатор торгов принимает решение о признании торгов несостоявшимися.&lt;br /&gt; В течение двух рабочих дней с даты подписания протокола о результатах проведения торгов Организатор торгов направляет победителю торгов и конкурсному управляющему копию протокола. В течение пяти дней с даты подписания этого протокола конкурсный управляющий направляет победителю торгов предложение заключить договор купли-продажи имущества с приложением проекта данного договора в соответствии с представленным победителем торгов предложением о цене имущества. Договор купли-продажи имущества должен быть заключен в течение пяти дней с даты получения победителем торгов предложения о заключении данного договора. Победитель торгов перечисляет денежные средства в оплату приобретенного имущества в течение тридцати дней со дня подписания договора купли-продажи имущества по следующим реквизитам: Получатель ООО «Комфорт» ИНН 2906006421. КПП 290601001, р/с 40702810315040000007 в Архангельском филиале «БАНК СГБ» г. Архангельск к/с  30101810400000000903 БИК 041117903.  При заключении договора с лицом, выигравшим торги, сумма внесенного им задатка засчитывается в счет исполнения договора. Расходы по оформлению перехода права собственности на имущество возлагаются на победителя торгов.&lt;br /&gt;В случае отказа или уклонения победителя торгов от подписания  договора купли-продажи в течение пяти дней с даты получения указанного предложения конкурсного управляющего внесенный задаток ему не возвращается и конкурсный управляющий вправе предложить заключить договор купли-продажи имущества участнику торгов, которым предложена наиболее высокая цена имущества по сравнению с ценой имущества, предложенной другими участниками торгов, за исключением победителя торгов.</Text>
          <IsRepeat>false</IsRepeat>
          <Date>2016-05-05T11:00:00</Date>
          <TradeType>OpenedAuction</TradeType>
          <PriceType>Private</PriceType>
          <Application>
            <TimeBegin>2016-03-28T10:00:00</TimeBegin>
            <TimeEnd>2016-05-04T10:00:00</TimeEnd>
            <Rules>Представление заявок на участие в торгах производится оператору электронной площадки по адресу www.lot-online.ru. ежедневно в рабочие дни с 10-00 до 15-00. Начало приема заявок 28.03.2016 г. с 10:00. Окончание приема заявок 04.05.2016 г. в 10:00 Заявка на участие в торгах оформляется в электронной форме, на русском языке и должна содержать следующие сведения: 1) обязательство участника открытых торгов соблюдать требования, указанные в сообщении о проведении открытых торгов; 2)  наименование, место  нахождения, почтовый адрес (для юридического лица), фамилию, имя, отчество, паспортные данные, сведения о месте жительства (для физического лица), номер контактного телефона, адрес электронной почты заявителя; 3) предложение о цене имущества; 4) сведения о наличии или отсутствии заинтересованности заявителя по отношению к должнику, кредиторам, конкурсному управляющему и о характере этой заинтересованности, сведения об участии в капитале заявителя конкурсного управляющего, а также Союза СРО АУ "Северо-Запада" членом или руководителем которой является конкурсный управляющий; 5) номер контактного телефона, адрес электронной почты заявителя. К заявке должны прилагаться: 1) выписка из ЕГРЮЛ (для юридического лица) , выписка из ЕГРИП (для ИП),  копии документов, удостоверяющих личность (для физического лица); надлежащим образом заверенный перевод на русский язык документов о государственной регистрации юридического лица или государственной регистрации физического лица в качестве индивидуального предпринимателя в соответствии с законодательством соответствующего государства (для иностранного лица); 2) документ, подтверждающий полномочия лица на осуществление действий от имени заявителя. Документы, прилагаемые к заявке, представляются в форме электронных документов, подписанных электронной цифровой подписью.</Rules>
          </Application>
          <TradeSite>Российский аукционный дом</TradeSite>
          <IdTradePlace>124</IdTradePlace>
          <LotTable>
            <AuctionLot>
              <Order>1</Order>
              <StartPrice>1464682.87</StartPrice>
              <Step xsi:nil="true" />
              <Advance>10</Advance>
              <Description>Торговое оборудование</Description>
              <AuctionStepUnit>Percent</AuctionStepUnit>
              <AdvanceStepUnit>Percent</AdvanceStepUnit>
              <ClassifierCollection>
                <AuctionLotClassifier>
                  <Code>0104020</Code>
                  <Name>Торгово-складское оборудование, инструмент и комплектующие</Name>
                </AuctionLotClassifier>
              </ClassifierCollection>
            </AuctionLot>
            <AuctionLot>
              <Order>2</Order>
              <StartPrice>1600000.00</StartPrice>
              <Step xsi:nil="true" />
              <Advance>10</Advance>
              <Description>Автотопливозаправщик АТЗ-5633 ПТС 16 НМ 200080 от 18.04.2012 г., VIN Х89594321С0DM6072, 2012 года выпуска, оранжевый.</Description>
              <AuctionStepUnit>Percent</AuctionStepUnit>
              <AdvanceStepUnit>Percent</AdvanceStepUnit>
              <ClassifierCollection>
                <AuctionLotClassifier>
                  <Code>0106008</Code>
                  <Name>Автомобили</Name>
                </AuctionLotClassifier>
              </ClassifierCollection>
            </AuctionLot>
          </LotTable>
          <Participants />
          <AdditionalText />
          <IdChangedMessage>983185</IdChangedMessage>
          <ChangeReason>В сообщении допущена опечатка в дате проведения торгов</ChangeReason>
        </ChangeAuction>
      </MessageInfo-->
              
              
              
                
                  
                  
                    
                      
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='TransferOwnershipRealEstate'">
              <br/>
            Приобретатель прав на объект строительства и земельный участок
            <table class="profile_table" cellspacing="0" cellpadding="0" border="0" style="float:none;">
              <tr>
                <th   style="width:40%;">Дата вынесения определения суда о передаче имущества и обязательств застройщика</th>
                <td>
                  <xsl:call-template name="format-date">
                    <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/TransferOwnershipRealEstate/CourtDecisionDate"/>
                  </xsl:call-template>
                </td>
              </tr>
             <tr>
                <th>Дата государственной регистрации перехода прав</th>
                <td>
                  <xsl:call-template name="format-date">
                    <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/TransferOwnershipRealEstate/TransferOwnershipStateRegistrationDate"/>
                  </xsl:call-template>
               </td>
              </tr> 
                <tr>
                <th>Наименование приобретателя</th>
                <td>
                  <xsl:value-of select="xml_data/MessageData/MessageInfo/TransferOwnershipRealEstate/AcquirerName"/>
                </td>
              </tr> 
                <tr>
                <th>Адрес приобретателя</th>
                <td><xsl:value-of select="xml_data/MessageData/MessageInfo/TransferOwnershipRealEstate/AcquirerAddress"/></td>
              </tr>
             <tr>
                <th>ОГРН</th>
                <td><xsl:value-of select="xml_data/MessageData/MessageInfo/TransferOwnershipRealEstate/AcquirerOgrn"/></td>
              </tr>
             <tr>
                <th>ИНН</th>
                <td><xsl:value-of select="xml_data/MessageData/MessageInfo/TransferOwnershipRealEstate/AcquirerInn"/></td>
              </tr>
            </table>
            Объект незавершенного строительства и земельный участок<br/>
             <xsl:if test="xml_data/MessageData/MessageInfo/TransferOwnershipRealEstate/UncompletedBuildingProjects">
            Объект незавершенного строительства  
            <xsl:for-each select="xml_data/MessageData/MessageInfo/TransferOwnershipRealEstate/UncompletedBuildingProjects/UncompletedBuildingProject">
              <table class="profile_table" cellspacing="0" cellpadding="0" border="0" style="float:none;">
                <tr>
                  <th   style="width:40%;">Строительный адрес</th>
                  <td>
                    <xsl:value-of select="Address"/>
                </td>
                </tr>
                <tr>
                  <th>Кадастровый номер</th>
                  <td>
                    <xsl:value-of select="CadastralNumber"/>
                </td>
                </tr>
                
                <tr>
                  <th>Дополнительная информация</th>
                  <td>
                    <xsl:call-template name="empty-string">
                      <xsl:with-param  name="str" select="AdditionalInfo"/>
                    </xsl:call-template>
                </td>
                </tr>
              </table>
            
             </xsl:for-each>
            
            </xsl:if>
              <xsl:if test="xml_data/MessageData/MessageInfo/TransferOwnershipRealEstate/LandPlots">
            Земельный участок              
            <xsl:for-each select="xml_data/MessageData/MessageInfo/TransferOwnershipRealEstate/LandPlots/LandPlot">
              <table class="profile_table" cellspacing="0" cellpadding="0" border="0" style="float:none;">
                <tr>
                  <th   style="width:40%;">Кадастровый номер</th>
                  <td>
                    <xsl:value-of select="CadastralNumber"/>
                </td>
                </tr>
                <tr>
                  <th>Описание прав на участок</th>
                  <td>
                    <xsl:value-of select="OwnershipRightDescription"/>
                </td>
                </tr>
                <tr>
                  <th>Дополнительная информация</th>
                  <td>
                    <xsl:call-template name="empty-string">
                      <xsl:with-param  name="str" select="AdditionalInfo"/>
                    </xsl:call-template>
                </td>
                </tr>

              </table>
            
             </xsl:for-each>
            
            </xsl:if>
            <b>Текст</b>
                <br/>
                <xsl:value-of select="xml_data/MessageData/MessageInfo/TransferOwnershipRealEstate/Text" disable-output-escaping="yes"/>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='TradeResult'">
              <xsl:if test="xml_data/MessageData/MessageInfo/TradeResult/Text">
                <b>Текст</b>
                <br/>
                <xsl:value-of select="xml_data/MessageData/MessageInfo/TradeResult/Text" disable-output-escaping="yes"/>
              </xsl:if>
              <xsl:if test="xml_data/MessageData/MessageInfo/TradeResult/LotTable/TradeResultLot">
                <table class="profile_table" cellspacing="0" cellpadding="0" border="0" style="float:none;">
                  <tr>
                    <th>Номер лота</th>
                    <th>Описание</th>
                    <th>Победитель</th>
                    <th>Лучшая цена, руб. / Обоснование</th>
                    <th>Классификация имущества</th>
                  </tr>

                  <xsl:for-each select="xml_data/MessageData/MessageInfo/TradeResult/LotTable/TradeResultLot">
                    <tr>
                      <td>
                        <xsl:value-of select="Order"/>
                      </td>
                      <td>
                        <xsl:value-of select="Description"/>
                      </td>
                      <td>
                        <xsl:choose>
                          <xsl:when test="Winner">
                            <xsl:value-of select="concat(Winner/ParticipantPerson/LastName, ' ',Winner/ParticipantPerson/FirstName,' ', Winner/ParticipantPerson/MiddleName, ' (', Winner/ParticipantPerson/Address, ', ИНН:', Winner/ParticipantPerson/INN,')')"/>
                          </xsl:when>
                          <xsl:otherwise>
                            <xsl:if test="LotStatus='TradeFailed'">
                              торги признаны несостоявшимися
                            </xsl:if>
                          </xsl:otherwise>
                        </xsl:choose>

                      </td>
                      <td>
                        <xsl:choose>
                          <xsl:when test="Basis">
                            <xsl:value-of select="Basis"/>
                          </xsl:when>
                          <xsl:otherwise>
                            <xsl:choose>
                              <xsl:when test="number(Winner/ParticipantPerson/PriceOffer) = Winner/ParticipantPerson/PriceOffer">
                                <xsl:value-of select="format-number(Winner/ParticipantPerson/PriceOffer,'# ###','buh')"/>
                              </xsl:when>
                              <xsl:otherwise>
                                <xsl:value-of select="Winner/ParticipantPerson/PriceOffer"/>
                              </xsl:otherwise>
                            </xsl:choose>
                          </xsl:otherwise>
                        </xsl:choose>
                      </td>
                      <td>
                        <xsl:for-each select="ClassifierCollection/AuctionLotClassifier">
                          <xsl:value-of select="Name"/>
                          <br/>
                        </xsl:for-each>
                      </td>
                    </tr>
                  </xsl:for-each>
                </table>
              </xsl:if>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ArbitralDecree'">
              <xsl:if test="xml_data/MessageData/MessageInfo/CourtDecision/CourtDecree">
                <table class="profile_table" cellspacing="0" cellpadding="0" border="0" >
                  <tr>
                    <th> Наименование суда</th>
                    <th>№ дела</th>
                    <th>Дата решения</th>
                  </tr>
                  <xsl:for-each select="xml_data/MessageData/MessageInfo/CourtDecision/CourtDecree">
                    <tr>
                      <td>
                        <xsl:value-of select="CourtName"/>
                      </td>
                      <td>
                        <xsl:value-of select="FileNumber"/>
                      </td>
                      <td>
                        <xsl:call-template name="format-date">
                          <xsl:with-param name="date" select="DecisionDate"/>
                        </xsl:call-template>
                      </td>
                    </tr>
                  </xsl:for-each>
                </table>
              </xsl:if>
              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/CourtDecision/Text"/>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='MeetingWorker'">
              <table class="profile_table" cellspacing="0" cellpadding="0" border="0" >
                <tr>
                  <th style="width:40%">Форма проведения собрания</th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/MeetingWorker/MeetingForm"/>
                  </td>
                </tr>
                <xsl:if test="string-length(xml_data/MessageData/MessageInfo/MeetingWorker/MeetingDate) &gt; 0">
                  <tr>
                    <th>Дата и время проведения</th>
                    <td>
                      <xsl:call-template name="format-date">
                        <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/MeetingWorker/MeetingDate"/>
                      </xsl:call-template>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="string-length(xml_data/MessageData/MessageInfo/MeetingWorker/BallotsReceptionEndDate) &gt; 0">
                  <tr>
                    <th>
                      Дата окончания приема бюллетеней
                    </th>
                    <td>
                      <xsl:call-template name="format-date">
                        <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/MeetingWorker/BallotsReceptionEndDate"/>
                      </xsl:call-template>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="string-length(xml_data/MessageData/MessageInfo/MeetingWorker/MeetingSite) &gt; 0">
                  <tr>
                    <th>Место проведения</th>
                    <td>
                      <xsl:value-of select="xml_data/MessageData/MessageInfo/MeetingWorker/MeetingSite"/>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="string-length(xml_data/MessageData/MessageInfo/MeetingWorker/BallotsSendPostAddress) &gt; 0">
                  <tr>
                    <th>
                      Почтовый адрес направления бюллетеней
                    </th>
                    <td>
                      <xsl:value-of select="xml_data/MessageData/MessageInfo/MeetingWorker/BallotsSendPostAddress"/>
                    </td>
                  </tr>
                </xsl:if>
                <tr>
                  <th>Повестка дня собрания</th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/MeetingWorker/Notice"/>
                  </td>
                </tr>
              </table>

              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/MeetingWorker/Text"/>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='MeetingWorkerResult'">
              <table class="profile_table" cellspacing="0" cellpadding="0" border="0" >
                <xsl:if test="xml_data/MessageData/MessageInfo/MeetingWorkerResult/IsLeadByArbitrManager='true'">
                  <tr>
                    <th colspan="2">Собрание проведено арбитражным управляющим</th>
                  </tr>
                </xsl:if>
                <xsl:if test="string-length(xml_data/MessageData/MessageInfo/MeetingWorkerResult/MeetingDate) &gt; 0">
                  <tr>
                    <th style="width:40%">Дата проведения собрания</th>
                    <td>
                      <xsl:call-template name="format-date">
                        <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/MeetingWorkerResult/MeetingDate"/>
                      </xsl:call-template>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="string-length(xml_data/MessageData/MessageInfo/MeetingWorkerResult/WorkersCount) &gt; 0">
                  <tr>
                    <th>
                      Количество присутствовавших работников
                      (бывших работников)
                    </th>
                    <td>

                      <xsl:value-of select="xml_data/MessageData/MessageInfo/MeetingWorkerResult/WorkersCount"/>

                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="string-length(xml_data/MessageData/MessageInfo/MeetingWorkerResult/RequirementSumm) &gt; 0">
                  <tr>
                    <th>Сумма требований второй очереди, руб.</th>
                    <td>
                      <xsl:value-of select="format-number(xml_data/MessageData/MessageInfo/MeetingWorkerResult/RequirementSumm,'# ##0.00','buh')"/>
                    </td>
                  </tr>
                </xsl:if>
              </table>

              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/MeetingWorkerResult/Text"/>


            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='CommitteeResult'">
              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/Other/Text"/>

            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='Committee'">
              <table class="profile_table" cellspacing="0" cellpadding="0" border="0" >
                <xsl:if test="string-length(xml_data/MessageData/MessageInfo/Committee/MeetingDate) &gt; 0">
                  <tr>
                    <th   style="width:40%;">Дата и время начала собрания</th>
                    <td>
                      <xsl:call-template name="date-time">
                        <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/Committee/MeetingDate"/>
                      </xsl:call-template>
                    </td>
                  </tr>
                </xsl:if>
                <tr>
                  <th style="width:40%">Место проведения</th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/Committee/MeetingSite"/>
                  </td>
                </tr>

              </table>
              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/Committee/Text"/>

            </xsl:if>

            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ViewDraftRestructuringPlan'">
              <table class="profile_table" cellspacing="0" cellpadding="0" border="0" >


                <xsl:if test="string-length(xml_data/MessageData/MessageInfo/ViewDraftRestructuringPlan/BankruptcyAcknowledgmentAndStartOfRestructuringMessageId) &gt; 0">
                  <tr>
                    <th style="width:40%">
                      Сообщение о признании обоснованным заявления
                      о признании гражданина банкротом
                      и введении реструктуризации его долгов
                    </th>
                    <td>
                      <a href="#">
                        <!--xsl:attribute name="href">?b_id=<xsl:value-of select="xml_data/MessageData/MessageInfo/ViewDraftRestructuringPlan/BankruptcyAcknowledgmentAndStartOfRestructuringMessageId"/>
                </xsl:attribute-->
                        <xsl:attribute name="onclick">
                          ShowBankrotUni(<xsl:value-of select="xml_data/MessageData/MessageInfo/ViewDraftRestructuringPlan/BankruptcyAcknowledgmentAndStartOfRestructuringMessageId"/>
                          ,1)
                        </xsl:attribute>
                        <xsl:value-of select="@BankruptcyAcknowledgmentAndStartOfRestructuringMessageText"/>
                      </a>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="string-length(xml_data/MessageData/MessageInfo/ViewDraftRestructuringPlan/PlaceOfAcquaintance) &gt; 0">
                  <tr>
                    <th>Место ознакомления</th>
                    <td>
                      <xsl:value-of select="xml_data/MessageData/MessageInfo/ViewDraftRestructuringPlan/PlaceOfAcquaintance"/>
                    </td>
                  </tr>
                </xsl:if>
              </table>

              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/ViewDraftRestructuringPlan/Text"/>


            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='AppointAdministration'">
              <table class="profile_table" cellspacing="0" cellpadding="0" border="0" >


                <tr>
                  <th style="width:40%">
                    Акт о назначении временной администрации
                  </th>
                  <td>
                    <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/AppointAdministration/DecisionName"/>
                    от
                    <xsl:call-template name="format-date">
                      <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/AppointAdministration/DecisionDate"/>
                    </xsl:call-template>
                    №
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/AppointAdministration/DecisionNumber"/>
                  </td>
                </tr>
                <tr>
                  <th>
                    Руководитель временной администрации
                  </th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/AppointAdministration/Director/Name"/>
                  </td>
                </tr>
                <tr>
                  <th>Адрес для корреспонденции</th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/AppointAdministration/Director/Address"/>
                  </td>
                </tr>
                <tr>
                  <th>Дата назначения временной администрации</th>
                  <td>
                    <xsl:call-template name="format-date">
                      <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/AppointAdministration/AdministrationDateFrom"/>
                    </xsl:call-template>
                  </td>
                </tr>
                <tr>
                  <th>Срок действия временной администрации</th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/AppointAdministration/AdministrationPeriod"/>
                  </td>
                </tr>
                <tr>
                  <th> Полномочия исполнительных органов</th>
                  <td>
                    <xsl:choose>
                      <xsl:when test="xml_data/MessageData/MessageInfo/AppointAdministration/AuthorityCredentionalsLimitation='Limited'">
                        ограниченны
                      </xsl:when>
                      <xsl:when test="xml_data/MessageData/MessageInfo/AppointAdministration/AuthorityCredentionalsLimitation='Suspended'">
                        приостановлены
                      </xsl:when>

                    </xsl:choose>
                  </td>
                </tr>
                <tr>
                  <th>Основания назначения временной администрации</th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/AppointAdministration/Reasons"/>
                  </td>
                </tr>
              </table>

              <b>Дополнительная информация:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/AppointAdministration/Text"/>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='TerminationAdministration'">
              <table class="profile_table" cellspacing="0" cellpadding="0" border="0" >


                <tr>
                  <th style="width:40%">
                    Акт о прекращении действия временной администрации
                  </th>
                  <td>
                    <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/TerminationAdministration/DecisionName"/>
                    от
                    <xsl:call-template name="format-date">
                      <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/TerminationAdministration/DecisionDate"/>
                    </xsl:call-template>
                    №
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/TerminationAdministration/DecisionNumber"/>
                  </td>
                </tr>
                <tr>
                  <th>
                    Руководитель временной администрации
                  </th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/TerminationAdministration/Director/Name"/>
                  </td>
                </tr>
                <tr>
                  <th>Адрес для корреспонденции</th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/TerminationAdministration/Director/Address"/>
                  </td>
                </tr>

                <tr>
                  <th>
                    Основания прекращения деятельности временной администрации
                  </th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/TerminationAdministration/OtherCauseDescription"/>
                  </td>
                </tr>
              </table>



            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='DemandAnnouncement'">
              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/Other/Text"/>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ChangeAdministration'">
              <table class="profile_table" cellspacing="0" cellpadding="0" border="0" >
                <tr>
                  <th style="width:40%">
                    Приказ Банка России от 
                  </th>
                  <td>
                    <xsl:call-template name="format-date">
                      <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/ChangeAdministration/DecisionDate"/>
                    </xsl:call-template>
                  </td>
                </tr>
                <tr>
                  <th>Номер приказа
                  </th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/ChangeAdministration/DecisionNumber"/>
                  </td>
                
                </tr>
                <tr>
                  <th>
                    Руководитель временной администрации
                  </th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/ChangeAdministration/Director/Name"/>
                  </td>
                </tr>
                <tr>
                  <th>Адрес для корреспонденции</th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/ChangeAdministration/Director/Address"/>
                  </td>
                </tr>
                <tr>
                  <th>
                    Основания изменения состава временной администрации:
                  </th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/ChangeAdministration/Reasons"/>
                  </td>
                </tr>
              </table>

              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/ChangeAdministration/Text"/>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='CourtAssertAcceptance'">
              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/Other/Text"/>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ReceivingCreditorDemand'">
              <table class="profile_table" cellspacing="0" cellpadding="0" border="0" >
                <tr>
                  <th style="width:40%">
                    Дата заявления требований
                  </th>
                  <td>

                    <xsl:call-template name="format-date">
                      <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/ReceivingCreditorDemand/DemandDate"/>
                    </xsl:call-template>

                  </td>
                </tr>
                <tr>
                  <th>
                    Сумма требований
                  </th>
                  <td>
                    <xsl:value-of select="format-number(xml_data/MessageData/MessageInfo/ReceivingCreditorDemand/DemandSum,'# ###.#','buh')"/>
                  </td>
                </tr>
                <tr>
                  <th>Наименование кредитора</th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/ReceivingCreditorDemand/CreditorName"/>
                  </td>
                </tr>
                <tr>
                  <th>
                    Основание возникновения
                  </th>
                  <td>
                    <xsl:value-of select="xml_data/MessageData/MessageInfo/ReceivingCreditorDemand/ReasonOccurence"/>
                  </td>
                </tr>
              </table>

              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/ReceivingCreditorDemand/Text"/>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='DeliberateBankruptcy'">
              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/DeliberateBankruptcy/Text"/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/Other/Text"/>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='AssetsReturning'">
              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/Other/Text"/>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='FinancialStateInformation'">
              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/Other/Text"/>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ActReviewPersonDamages'">
              Контролирующие должника лица
              <table class="profile_table" cellspacing="0" cellpadding="0" border="0" >
                <tr>
                  <th>Наименование/ФИО</th>
                  <th>Резидент</th>
                  <th>ИНН</th>
                  <th>СНИЛС</th>
                  <th>Привлечение к ответственности</th>
                  <th>Размер ответственности, руб.</th>
                </tr>
                <xsl:for-each select="xml_data/MessageData/MessageInfo/ActReviewPersonDamages/BankruptSupervisoryPersons/BankruptSupervisoryPerson">
                  <tr>
                    <td>
                      <xsl:value-of select="Name"/>
                    </td>
                    <td>
                      <xsl:if test="@xsi:type='BankruptSupervisoryPersonRussian'">
                        РФ
                      </xsl:if>
                    </td>
                    <td>
                      <xsl:choose>
                        <xsl:when test="string-length(Code)=12">
                          <xsl:value-of select="Code"/>
                        </xsl:when>
                        <xsl:otherwise>-</xsl:otherwise>
                      </xsl:choose>
                    </td>
                    <td>
                      <xsl:choose>
                        <xsl:when test="string-length(Code)=10">
                          <xsl:value-of select="Code"/>
                        </xsl:when>
                        <xsl:otherwise>-</xsl:otherwise>
                      </xsl:choose>
                    </td>
                    <td>
                      <xsl:choose>
                        <xsl:when test="IsArraignment='true'">привлечен</xsl:when>
                        <xsl:when test="IsArraignment='false'">не привлечен</xsl:when>
                        <xsl:otherwise>-</xsl:otherwise>
                      </xsl:choose>
                    </td>
                    <td>
                      <xsl:value-of select="format-number(ResponsibilityAmount,'# ###.00','buh')"/>
                    </td>

                  </tr>
                </xsl:for-each>
              </table>

              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/ActReviewPersonDamages/Text"/>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ActReviewPersonSubsidiary'">
              Контролирующие должника лица
              <table class="profile_table" cellspacing="0" cellpadding="0" border="0" >
                <tr>
                  <th>Наименование/ФИО</th>
                  <th>Резидент</th>
                  <th>ИНН</th>
                  <th>СНИЛС</th>
                  <th>Привлечение к ответственности</th>
                  <th>Размер ответственности, руб.</th>
                </tr>
                <xsl:for-each select="xml_data/MessageData/MessageInfo/ActReviewPersonSubsidiary/BankruptSupervisoryPersons/BankruptSupervisoryPerson">
                  <tr>
                    <td>
                      <xsl:value-of select="Name"/>
                    </td>
                    <td>
                      <xsl:if test="@xsi:type='BankruptSupervisoryPersonRussian'">
                        РФ
                      </xsl:if>
                    </td>
                    <td>
                      <xsl:choose>
                        <xsl:when test="string-length(Code)=12">
                          <xsl:value-of select="Code"/>
                        </xsl:when>
                        <xsl:otherwise>-</xsl:otherwise>
                      </xsl:choose>
                    </td>
                    <td>
                      <xsl:choose>
                        <xsl:when test="string-length(Code)=11">
                          <xsl:value-of select="Code"/>
                        </xsl:when>
                        <xsl:otherwise>-</xsl:otherwise>
                      </xsl:choose>
                    </td>
                    <td>
                      <xsl:choose>
                        <xsl:when test="IsArraignment='true'">привлечен</xsl:when>
                        <xsl:when test="IsArraignment='false'">не привлечен</xsl:when>
                        <xsl:otherwise>-</xsl:otherwise>
                      </xsl:choose>
                    </td>
                    <td>
                      <xsl:value-of select="format-number(ResponsibilityAmount,'# ###.00','buh')"/>
                    </td>

                  </tr>
                </xsl:for-each>
              </table>

              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/ActReviewPersonSubsidiary/Text"/>
            </xsl:if>

            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ActPersonSubsidiary'">
              Контролирующие должника лица
              <table class="profile_table" cellspacing="0" cellpadding="0" border="0" >
                <tr>
                  <th>Наименование/ФИО</th>
                  <th>Резидент</th>
                  <th>ИНН</th>
                  <th>СНИЛС</th>
                  <th>Привлечение к ответственности</th>
                  <th>Размер ответственности, руб.</th>
                </tr>
                <xsl:for-each select="xml_data/MessageData/MessageInfo/ActPersonSubsidiary/BankruptSupervisoryPersons/BankruptSupervisoryPerson">
                  <tr>
                    <td>
                      <xsl:value-of select="Name"/>
                    </td>
                    <td>
                      <xsl:if test="@xsi:type='BankruptSupervisoryPersonRussian'">
                        РФ
                      </xsl:if>
                    </td>
                    <td>
                      <xsl:choose>
                        <xsl:when test="string-length(Code)=12">
                          <xsl:value-of select="Code"/>
                        </xsl:when>
                        <xsl:otherwise>-</xsl:otherwise>
                      </xsl:choose>
                    </td>
                    <td>
                      <xsl:choose>
                        <xsl:when test="string-length(Code)=11">
                          <xsl:value-of select="Code"/>
                        </xsl:when>
                        <xsl:otherwise>-</xsl:otherwise>
                      </xsl:choose>
                    </td>
                    <td>
                      <xsl:choose>
                        <xsl:when test="IsArraignment='true'">привлечен</xsl:when>
                        <xsl:when test="IsArraignment='false'">не привлечен</xsl:when>
                        <xsl:otherwise>-</xsl:otherwise>
                      </xsl:choose>
                    </td>
                    <td>
                      <xsl:value-of select="format-number(ResponsibilityAmount,'# ###.00','buh')"/>
                    </td>

                  </tr>
                </xsl:for-each>
              </table>

              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/ActPersonSubsidiary/Text"/>
            </xsl:if>


            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ActPersonDamages'">
              Контролирующие должника лица
              <table class="profile_table" cellspacing="0" cellpadding="0" border="0" >
                <tr>
                  <th>Наименование/ФИО</th>
                  <th>Резидент</th>
                  <th>ИНН</th>
                  <th>СНИЛС</th>
                  <th>Привлечение к ответственности</th>
                  <th>Размер ответственности, руб.</th>
                </tr>
                <xsl:for-each select="xml_data/MessageData/MessageInfo/ActPersonDamages/BankruptSupervisoryPersons/BankruptSupervisoryPerson">
                  <tr>
                    <td>
                      <xsl:value-of select="Name"/>
                    </td>
                    <td>
                      <xsl:if test="@xsi:type='BankruptSupervisoryPersonRussian'">
                        РФ
                      </xsl:if>
                    </td>
                    <td>
                      <xsl:choose>
                        <xsl:when test="string-length(Code)=12">
                          <xsl:value-of select="Code"/>
                        </xsl:when>
                        <xsl:otherwise>-</xsl:otherwise>
                      </xsl:choose>
                    </td>
                    <td>
                      <xsl:choose>
                        <xsl:when test="string-length(Code)=11">
                          <xsl:value-of select="Code"/>
                        </xsl:when>
                        <xsl:otherwise>-</xsl:otherwise>
                      </xsl:choose>
                    </td>
                    <td>
                      <xsl:choose>
                        <xsl:when test="IsArraignment='true'">привлечен</xsl:when>
                        <xsl:when test="IsArraignment='false'">не привлечен</xsl:when>
                        <xsl:otherwise>-</xsl:otherwise>
                      </xsl:choose>
                    </td>
                    <td>
                      <xsl:value-of select="format-number(ResponsibilityAmount,'# ###.00','buh')"/>
                    </td>

                  </tr>
                </xsl:for-each>
              </table>
              <xsl:if test="xml_data/MessageData/MessageInfo/ActPersonDamages/AnotherPersonsForResponsibility/PersonForResponsibility">
                Список иных лиц, привлеченных к ответственности
                <table class="profile_table" cellspacing="0" cellpadding="0" border="0" >
                  <tr>
                    <th>Наименование/ФИО</th>
                    <th>Тип привлекаемого лица</th>
                    <th>Размер ответственности, руб.</th>
                    <th>Привлечение к ответственности</th>

                  </tr>
                  <xsl:for-each select="xml_data/MessageData/MessageInfo/ActPersonDamages/AnotherPersonsForResponsibility/PersonForResponsibility">
                    <tr>
                      <td>
                        <xsl:value-of select="Fio"/>
                      </td>
                      <td>
                        <xsl:value-of select="Type"/>
                      </td>
                      <td>
                        <xsl:value-of select="format-number(ResponsibilityAmount,'# ###.00','buh')"/>
                      </td>
                      <td>
                        <xsl:choose>
                          <xsl:when test="IsArraignment='true'">привлечен</xsl:when>
                          <xsl:when test="IsArraignment='false'">не привлечен</xsl:when>
                          <xsl:otherwise>-</xsl:otherwise>
                        </xsl:choose>
                      </td>


                    </tr>
                  </xsl:for-each>
                </table>
              </xsl:if>
              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/ActPersonDamages/Text"/>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='DeclarationPersonDamages'">
              Контролирующие должника лица
              <table class="profile_table" cellspacing="0" cellpadding="0" border="0" >
                <tr>
                  <th>Наименование/ФИО</th>
                  <th>Резидент</th>
                  <th>ИНН</th>
                  <th>СНИЛС</th>
                  <th>Размер ответственности, руб.</th>
                </tr>
                <xsl:for-each select="xml_data/MessageData/MessageInfo/DeclarationPersonDamages/BankruptSupervisoryPersons/BankruptSupervisoryPerson">
                  <tr>
                    <td>
                      <xsl:value-of select="Name"/>
                    </td>
                    <td>
                      <xsl:if test="@xsi:type='BankruptSupervisoryPersonRussian'">
                        РФ
                      </xsl:if>
                    </td>
                    <td>
                      <xsl:choose>
                        <xsl:when test="string-length(Code)=12">
                          <xsl:value-of select="Code"/>
                        </xsl:when>
                        <xsl:otherwise>-</xsl:otherwise>
                      </xsl:choose>
                    </td>
                    <td>
                      <xsl:choose>
                        <xsl:when test="string-length(Code)=11">
                          <xsl:value-of select="Code"/>
                        </xsl:when>
                        <xsl:otherwise>-</xsl:otherwise>
                      </xsl:choose>
                    </td>
                    <td>
                      <xsl:value-of select="format-number(ResponsibilityAmount,'# ###.00','buh')"/>
                    </td>

                  </tr>
                </xsl:for-each>
              </table>
              <xsl:if test="xml_data/MessageData/MessageInfo/DeclarationPersonDamages/AnotherPersonsForResponsibility/PersonForResponsibility">
                Список иных лиц, привлеченных к ответственности
                <table class="profile_table" cellspacing="0" cellpadding="0" border="0" >
                  <tr>
                    <th>Наименование/ФИО</th>
                    <th>Тип привлекаемого лица</th>
                    <th>Размер ответственности, руб.</th>

                  </tr>
                  <xsl:for-each select="xml_data/MessageData/MessageInfo/DeclarationPersonDamages/AnotherPersonsForResponsibility/PersonForResponsibility">
                    <tr>
                      <td>
                        <xsl:value-of select="Fio"/>
                      </td>
                      <td>
                        <xsl:value-of select="Type"/>
                      </td>
                      <td>
                        <xsl:value-of select="format-number(ResponsibilityAmount,'# ###.00','buh')"/>
                      </td>


                    </tr>
                  </xsl:for-each>
                </table>
              </xsl:if>
              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/DeclarationPersonDamages/Text"/>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='DeclarationPersonSubsidiary'">
              Контролирующие должника лица
              <table class="profile_table" cellspacing="0" cellpadding="0" border="0" >
                <tr>
                  <th>Наименование/ФИО</th>
                  <th>Резидент</th>
                  <th>ИНН</th>
                  <th>СНИЛС</th>
                  <th>Размер ответственности, руб.</th>
                </tr>
                <xsl:for-each select="xml_data/MessageData/MessageInfo/DeclarationPersonSubsidiary/BankruptSupervisoryPersons/BankruptSupervisoryPerson">
                  <tr>
                    <td>
                      <xsl:value-of select="Name"/>
                    </td>
                    <td>
                      <xsl:if test="@xsi:type='BankruptSupervisoryPersonRussian'">
                        РФ
                      </xsl:if>
                    </td>
                    <td>
                      <xsl:choose>
                        <xsl:when test="string-length(Code)=12">
                          <xsl:value-of select="Code"/>
                        </xsl:when>
                        <xsl:otherwise>-</xsl:otherwise>
                      </xsl:choose>
                    </td>
                    <td>
                      <xsl:choose>
                        <xsl:when test="string-length(Code)=11">
                          <xsl:value-of select="Code"/>
                        </xsl:when>
                        <xsl:otherwise>-</xsl:otherwise>
                      </xsl:choose>
                    </td>
                    <td>
                      <xsl:value-of select="format-number(ResponsibilityAmount,'# ###.00','buh')"/>
                    </td>

                  </tr>
                </xsl:for-each>
              </table>

              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/DeclarationPersonSubsidiary/Text"/>
            </xsl:if>

            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ActReviewDealInvalid'">
              <table class="profile_table" cellspacing="0" cellpadding="0" border="0"  style="float:none;">
                <tr>
                  <th style="width:40%">Дата получения сведений о решении суда</th>
                  <td>
                    <xsl:call-template name="date-time">
                      <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/ActReviewDealInvalid/CourtDecisionNoticeDate"/>
                    </xsl:call-template>
                  </td>
                </tr>
              </table>
              <xsl:if test="xml_data/MessageData/MessageInfo/ActReviewDealInvalid/DealNotValid='true'">
                Сделка признана недействительной
              </xsl:if>
               <xsl:if test="xml_data/MessageData/MessageInfo/ActReviewDealInvalid/DealNotValid='false'">
                Отказано в признании сделки недействительной
              </xsl:if>
              <xsl:if test="xml_data/MessageData/MessageInfo/ActReviewDealInvalid/DealParticipants/DealParticipant">
                <br/>
                Участники сделки
                <table class="profile_table" cellspacing="0" cellpadding="0" border="0" >
                  <tr>
                    <th>Наименование/ФИО</th>
                    <th>Резидент</th>
                    <th>ИНН</th>
                  </tr>
                  <xsl:for-each select="xml_data/MessageData/MessageInfo/ActReviewDealInvalid/DealParticipants/DealParticipant">
                    <tr>
                      <td>
                        <xsl:value-of select="Name"/>
                      </td>
                      <td>
                        <xsl:if test="@xsi:type='DealParticipantRussian'">
                          РФ
                        </xsl:if>
                      </td>
                      <td>
                        <xsl:value-of select="Code"/>
                      </td>

                    </tr>
                  </xsl:for-each>
                </table>
              </xsl:if>
              <xsl:if test="xml_data/MessageData/MessageInfo/ActReviewDealInvalid/Text">
                <br/>
                <b>Текст:</b>
                <br/>
                <xsl:value-of select="xml_data/MessageData/MessageInfo/ActReviewDealInvalid/Text" disable-output-escaping="yes"/>
              </xsl:if>

            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ActDealInvalid'">
              <xsl:choose>
                <xsl:when test="xml_data/MessageData/MessageInfo/ActDealInvalid/DealNotValid='true'">Сделка признана недействительной </xsl:when>
                <xsl:otherwise>Отказано в признании сделки недействительной</xsl:otherwise>
              </xsl:choose>
              <br/>
              <xsl:if test="xml_data/MessageData/MessageInfo/ActDealInvalid/DealParticipants/DealParticipant">
                <br/>Участники сделки
                <table class="profile_table" cellspacing="0" cellpadding="0" border="0" >
                  <tr>
                    <th>Наименование/ФИО</th>
                    <th>Резидент</th>
                    <th>ИНН</th>
                  </tr>
                  <xsl:for-each select="xml_data/MessageData/MessageInfo/ActDealInvalid/DealParticipants/DealParticipant">
                    <tr>
                      <td>
                        <xsl:value-of select="Name"/>
                      </td>
                      <td>
                        <xsl:if test="@xsi:type='DealParticipantRussian'">РФ</xsl:if>
                      </td>
                      <td>
                        <xsl:value-of select="Code"/>
                      </td>
                    </tr>
                  </xsl:for-each>
                </table>
              </xsl:if>
              <xsl:if test="xml_data/MessageData/MessageInfo/ActDealInvalid/Text">
                <br/>
                <b>Текст:</b>
                <br/>
                <xsl:value-of select="xml_data/MessageData/MessageInfo/ActDealInvalid/Text" disable-output-escaping="yes"/>
              </xsl:if>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='DealInvalid'">
              <xsl:choose>
                <xsl:when test="xml_data/MessageData/MessageInfo/DealInvalid/IsApplyByArbitrManager='true'">
                  Заявление подано арбитражным управляющим<br/>
                </xsl:when>
                <xsl:otherwise>
                  Заявление подано третьим лицом<br/>
                </xsl:otherwise>
              </xsl:choose>
              <table class="profile_table" cellspacing="0" cellpadding="0" border="0" >
                <xsl:if test="string-length(xml_data/MessageData/MessageInfo/DealInvalid/DeclarationNoticeDate) &gt; 0">
                  <tr>
                    <th style="width:40%">Дата получения сведений о подаче заявления</th>
                    <td>
                      <xsl:call-template name="format-date">
                        <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/DealInvalid/DeclarationNoticeDate"/>
                      </xsl:call-template>
                    </td>
                  </tr>
                </xsl:if>
                <tr>
                  <th  style="width:40%">Дата подачи заявления</th>
                  <td>
                    <xsl:call-template name="format-date">
                      <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/DealInvalid/DeclarationDate"/>
                    </xsl:call-template>
                  </td>
                </tr>

              </table>

              <xsl:if test="xml_data/MessageData/MessageInfo/DealInvalid/DealParticipants/DealParticipant">
                <br/>Участники сделки
                <table class="profile_table" cellspacing="0" cellpadding="0" border="0" >
                  <tr>
                    <th>Наименование/ФИО</th>
                    <th>Резидент</th>
                    <th>ИНН</th>
                  </tr>
                  <xsl:for-each select="xml_data/MessageData/MessageInfo/DealInvalid/DealParticipants/DealParticipant">
                    <tr>
                      <td>
                        <xsl:value-of select="Name"/>
                      </td>
                      <td>
                        <xsl:if test="@xsi:type='DealParticipantRussian'">РФ</xsl:if>
                      </td>
                      <td>
                        <xsl:value-of select="Code"/>
                      </td>
                    </tr>
                  </xsl:for-each>
                </table>
              </xsl:if>
              <xsl:if test="xml_data/MessageData/MessageInfo/DealInvalid/Text">
                <br/>
                <b>Текст:</b>
                <br/>
                <xsl:value-of select="xml_data/MessageData/MessageInfo/DealInvalid/Text" disable-output-escaping="yes"/>
              </xsl:if>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='SaleOrderPledgedProperty'">
              <xsl:if test="xml_data/MessageData/MessageInfo/SaleOrderPledgedProperty/Text">
                <br/>
                <b>Текст:</b>
                <br/>
                <xsl:value-of select="xml_data/MessageData/MessageInfo/SaleOrderPledgedProperty/Text" disable-output-escaping="yes"/>
              </xsl:if>
              <table class="profile_table" cellspacing="0" cellpadding="0" border="0" >
                <tr>
                  <th >Номер лота</th>
                  <th >Описание</th>
                  <th >Начальная цена, руб</th>
                  <th >Классификация имущества</th>
                </tr>
                <xsl:for-each select="xml_data/MessageData/MessageInfo/SaleOrderPledgedProperty/LotTable/PledgedPropertyLot">
                  <tr>
                  <td>
                    <xsl:value-of select="Order"/>
                  </td>

                  <td>
                    <xsl:value-of select="Description"/>
                  </td>
                  <td>
                    <xsl:value-of select="format-number(StartPrice,'# ###.#','buh')"/>
                  </td>
                  <td>
                    <xsl:value-of select="ClassifierCollection/AuctionLotClassifier/Name"/>

                  </td>
                    </tr>
                </xsl:for-each>
              </table>
              <b>Условия обеспечения сохранности предмета залога:</b>
              <br/>
              <xsl:value-of select="xml_data/MessageData/MessageInfo/SaleOrderPledgedProperty/AdditionalText"/>
            </xsl:if>
            
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='SaleContractResult'">
              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/SaleContractResult/Text"/>
            </xsl:if>
             <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='CancelAuctionTradeResult'">
              <b>Текст</b>
              <br/>
              <xsl:value-of select="xml_data/MessageData/MessageInfo/CancelAuctionTradeResult/Text" disable-output-escaping="yes"/>
              <br/>
             
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='Annul'">
              <b>Причина аннулирования:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/Annul/Text"/>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='Other'">
              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/Other/Text"/>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='ChangeDeliberateBankruptcy'">
              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/ChangeDeliberateBankruptcy/Text"/>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='PropertyEvaluationReport'">
              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/PropertyEvaluationReport/Text"/>
            </xsl:if>
            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='PropertyInventoryResult'">
              <b>Текст:</b>
              <br/>
              <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/PropertyInventoryResult/Text"/>
            </xsl:if>

            <!--
      			
      <MessageInfo MessageType="SaleContractResult">
        <SaleContractResult>
          <Text>Решением Арбитражного суда Ямало-Ненецкого автономного округа от 12.03.2015 г. по делу №А81-3870/2014 ООО «СтройСиб» (629800, ЯНАО, г. Ноябрьск, территория пелей, промузел, промзона, ИНН 8905041985, ОГРН 1078905006630) признано несостоятельным (банкротом) в отношении него введено конкурсное производство, конкурсным управляющим ООО «СтройСиб» утвержден Дмитриев Николай Борисович ИНН 720300921660, СНИЛС №062-597-879-10, член Ассоциации арбитражных управляющих «Сибирский центр экспертов антикризисного управления» (Юридический адрес: 630091, г. Новосибирск, ул. Писарева, 4; Фактический и почтовый адрес: 630132,г.Новосибирск,ул.Советская,77в; тел./факс: +7(383) 383-000-5, ИНН5406245522, ОГРН 1035402470036, включено в ЕГРСО АУ 18 июля 2003 г. за № 0010).&lt;br /&gt;Определением Арбитражного суда Ямало-Ненецкого автономного округа от 09.03.2016 г. по делу №А81-3870/2014 срок конкурсного производства ООО «СтройСиб» продлен на два месяца. Дата судебного заседания по рассмотрению отчета конкурсного управляющего - 10 мая 2016 г. в 10 часов 20 минут в здании Арбитражного суда ЯНАО по адресу: г. Салехард, ул. Республики, 102, каб. №217.&lt;br /&gt;11.03.2016 г. конкурсный управляющий ООО "СтройСиб" Дмитриев Николай Борисович заключил договоры купли-продажи с Рукавишниковым Евгением Николаевичем (паспорт: серия 7103 № 907379, выдан УВД Калининского округа г. Тюмени 28.05.2003 г. место жительства: 625062, Россия, Тюменская область, ул. Самарцева, д. 20, кв. 76) как с победителем по лотам №5,7 и как с единственным участником торгов по лотам №1,3,4,6 по продаже имущества ООО "СтройСиб" :&lt;br /&gt;Лот №1-Автобус Волжанин 5270, 2000 г.в., цена согласно договору купли-продажи составляет - 26700,00 рублей.&lt;br /&gt;Лот №3 - Автобус Волжанин 5270, 2001 г.в., цена согласно договору купли-продажи составляет - 28000,00 рублей.&lt;br /&gt;Лот №4 - Автобус КАВЗ 3976-020, 2001 г.в., цена согласно договору купли-продажи составляет - 16000,00 рублей.&lt;br /&gt;Лот №5 - Автобус САРЗ 33976, 1995 г.в., цена согласно договору купли-продажи составляет - 15720,00 рублей.&lt;br /&gt;Лот №6 - Автобус КАВЗ 3976-011, 2000 г.в., цена согласно договору купли-продажи составляет - 14980,00 рублей.&lt;br /&gt;Лот №7 - Автобус KAROSA C 734.40, 1993 г.в., цена согласно договору купли-продажи составляет - 24100,00 рублей.&lt;br /&gt;Общая сумма по договорам купли-продажи заключенных с Рукавишниковым Евгением Николаевичем составляет 125 500,00 рублей.</Text>
          <FnsDepartmentName>Федеральная налоговая служба</FnsDepartmentName>
          <SaleContractResultType>ContractWithWinner</SaleContractResultType>
          <DateContract>2016-03-11T00:00:00</DateContract>
          <Price>125500</Price>
          <FailureWinnerInfo>
            <Name />
            <Ogrn />
            <Inn />
          </FailureWinnerInfo>
          <PurchaserInfo>
            <Name>Рукавишиников Евгений Николаевич</Name>
            <Ogrn />
            <Inn>720411998348</Inn>
          </PurchaserInfo>
        </SaleContractResult>
      </MessageInfo>
      -->





            <xsl:if test="xml_data/MessageData/MessageInfo/@MessageType='Auction'">
              <table class="profile_table" cellspacing="0" cellpadding="0" border="0"  style="float:none;">
                <xsl:if test="xml_data/MessageData/MessageInfo/Auction/TradeType">
                  <tr>
                    <th style="width:40%">Вид торгов</th>
                    <td>
                      <xsl:call-template name="TradeTypes">
                        <xsl:with-param name="TT" select="xml_data/MessageData/MessageInfo/Auction/TradeType"/>
                      </xsl:call-template>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="xml_data/MessageData/MessageInfo/Auction/Application/TimeBegin">
                  <tr>
                    <th>Дата и время начала подачи заявок:</th>
                    <td>
                      <xsl:call-template name="date-time">
                        <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/Auction/Application/TimeBegin"/>
                      </xsl:call-template>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="xml_data/MessageData/MessageInfo/Auction/Application/TimeEnd">
                  <tr>
                    <th>Дата и время окончания подачи заявок:</th>
                    <td>
                      <xsl:call-template name="date-time">
                        <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/Auction/Application/TimeEnd"/>
                      </xsl:call-template>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="xml_data/MessageData/MessageInfo/Auction/Application/Rules">
                  <tr>
                    <th>Правила подачи заявок:</th>
                    <td>
                      <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/Auction/Application/Rules"/>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="xml_data/MessageData/MessageInfo/Auction/Date">
                  <tr>
                    <th>Дата и время торгов:</th>
                    <td>
                      <xsl:call-template name="date-time">
                        <xsl:with-param name="date" select="xml_data/MessageData/MessageInfo/Auction/Date"/>
                      </xsl:call-template>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="xml_data/MessageData/MessageInfo/Auction/PriceType">
                  <tr>
                    <th>Форма подачи предложения о цене:</th>
                    <td>
                      <xsl:call-template name="PriceTypes">
                        <xsl:with-param name="TT" select="xml_data/MessageData/MessageInfo/Auction/PriceType"/>
                      </xsl:call-template>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="xml_data/MessageData/MessageInfo/Auction/TradeSite">
                  <tr>
                    <th>Место проведения:</th>
                    <td>
                      <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/Auction/TradeSite"/>
                    </td>
                  </tr>
                </xsl:if>
                <xsl:if test="aucion_end">
                  <tr>
                    <th>Сообщение о завершении торгов</th>
                    <td>
                      <a href="#">
                        <!--xsl:attribute name="href">
                    ?b_id=<xsl:value-of select="aucion_end/@number"/>
                  </xsl:attribute-->
                        <xsl:attribute name="onclick">
                          ShowBankrotUni(<xsl:value-of select="aucion_end/@number"/>,1)
                        </xsl:attribute>
                        <xsl:value-of select="aucion_end/@a_txt"/>
                      </a>
                    </td>
                  </tr>
                </xsl:if>
              </table>
              <xsl:if test="xml_data/MessageData/MessageInfo/Auction/IsRepeat">

                <b>Это повторные торги</b>
                <br/>

              </xsl:if>


              <xsl:if test="xml_data/MessageData/MessageInfo/Auction/Text">
                <br/>
                <b>Текст:</b>
                <br/>
                <xsl:value-of disable-output-escaping="yes" select="xml_data/MessageData/MessageInfo/Auction/Text"/>

              </xsl:if>
              <xsl:if test="xml_data/MessageData/MessageInfo/Auction/LotTable">
                <table class="profile_table" cellspacing="0" cellpadding="0" border="0"  style="float:none;">
                  <tr>
                    <th>
                      Номер лота
                    </th>
                    <th>Описание</th>
                    <th>Начальная цена, руб</th>
                    <th>Шаг</th>
                    <th>Задаток</th>
                    <th>Информация о снижении цены</th>
                    <th>Классификация имущества</th>
                  </tr>
                  <xsl:for-each select="xml_data/MessageData/MessageInfo/Auction/LotTable/AuctionLot">
                    <tr>
                      <td>
                        <xsl:value-of select="Order"/>
                      </td>
                      <td>
                        <xsl:value-of select="Description"/>
                      </td>
                      <td>
                        <xsl:value-of select="StartPrice"/>
                      </td>
                      <td>
                        <xsl:value-of select="format-number(Step,'# ###.00','buh')"/>%
                      </td>
                      <td>
                        <xsl:value-of select="format-number(Advance,'# ###.00','buh')"/>%
                      </td>
                      <td>
                        <xsl:value-of select="PriceReduction"/>
                      </td>
                      <td>
                        <xsl:value-of select="ClassifierCollection/AuctionLotClassifier/Name"/>
                      </td>
                    </tr>
                  </xsl:for-each>
                </table>

              </xsl:if>
            </xsl:if>
          </xsl:for-each>
          <xsl:if test="//efrsb/docs">
            <br/>
            <b>Прикреплённые файлы</b>
            <ul style="margin-left:15px;">
              <xsl:for-each select="//efrsb/docs/items">
                <li>
                  <a target="_blank">
                    <xsl:attribute name="href">
                      <xsl:value-of select="@doc_path"/>
                    </xsl:attribute>
                    <xsl:value-of select="@urlName"/> 
                  </a>(<xsl:value-of select="@size"/>)
                </li>
              </xsl:for-each>
            </ul>
          </xsl:if>
        </td>
      </tr>
       
      </tbody>        
    </table>
   
  </xsl:template>
  <xsl:template name="units">
    <xsl:param name="unit_txt"/>
    <xsl:choose>
    <xsl:when test="$unit_txt='Percent'">%</xsl:when>
    <xsl:otherwise> </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="format-date">
    <xsl:param name="date"/>
    <xsl:value-of select="concat(substring($date, 9, 2), '.', substring($date, 6, 2), '.', substring($date, 1, 4))"/>
  </xsl:template>
  <xsl:template name="empty-string">
    <xsl:param name="str"/>
    <xsl:if test="string-length($str) = 0">
      -
    </xsl:if>
    <xsl:if test="string-length($str) &gt; 0">
      <xsl:value-of select="$str"/>
    </xsl:if>
  
  </xsl:template>
  <xsl:template name="TradeTypes">
    <xsl:param name="TT"/>
    <xsl:choose>
      <xsl:when test="$TT='PublicOffer'">
        Открытый аукцион
      </xsl:when>
      <xsl:when test="$TT='ClosePublicOffer'">
        Закрытое публичное предложение
      </xsl:when>
      <xsl:when test="$TT='OpenedAuction'">
        Открытый аукцион
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$TT"/>-неизвестно.
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="PriceTypes">
    <xsl:param name="TT"/>
    <xsl:choose>
      <xsl:when test="$TT='Public'">
        Открытая
      </xsl:when>
       <xsl:when test="$TT='Private'">
        Закрытая
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$TT"/>-неизвестно.
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="date-time">
    <xsl:param name="date"/>
    <xsl:value-of select="concat(substring($date, 9, 2), '.', substring($date, 6, 2), '.', substring($date, 1, 4), ' ', substring($date,12,5))"/>
  </xsl:template>
  <xsl:template name="date-interval">
    <xsl:param name="d_start"/>
    <xsl:param name="d_end"/>
    <xsl:if test="substring($d_start,1,10)=substring($d_end,1,10)">
      <xsl:value-of select="concat(substring($d_start, 9, 2), '.', substring($d_start, 6, 2), '.', substring($d_start, 1, 4), ' с ', substring($d_start,12,5), ' по ', substring($d_end,12,5))"/>
    </xsl:if>
    <xsl:if test="substring($d_start,1,10)!=substring($d_end,1,10)">
      <xsl:value-of select="concat('с ', substring($d_start, 9, 2), '.', substring($d_start, 6, 2), '.', substring($d_start, 1, 4), ' ', substring($d_start,12,5), ' по ', substring($d_end, 9, 2), '.', substring($d_end, 6, 2), '.', substring($d_end, 1, 4), substring($d_end,12,5))"/>
    </xsl:if>
  </xsl:template>
  <xsl:template name="time-format">
    <xsl:param name="time"/>
    <xsl:value-of select="substring($time,1,5)"/>
  </xsl:template>
  <xsl:template name="getMessType">
    <xsl:param name="MT_eng"/>
    <xsl:choose>
      <xsl:when test="$MT_eng='TradeResult'">
        Организация и проведение реализации имущества<br/> Сообщение о результатах торгов
      </xsl:when>
      <xsl:when test="$MT_eng='ArbitralDecree'">Сообщение о судебном акте</xsl:when>
      <xsl:when test="$MT_eng='Auction'">
        Организация и проведение реализации имущества<br/>Объявление о проведении торгов
      </xsl:when>
      <xsl:when test="$MT_eng='PropertyInventoryResult'">
        Сведения об активах<br/>Сведения о результатах инвентаризации имущества должника
      </xsl:when>
      <xsl:when test="$MT_eng='Other'">
        Иное сообщение<br/>	Другое
      </xsl:when>
      <xsl:when test="$MT_eng='Meeting'">
        Собрания и комитеты кредиторов<br/> Сообщение о собрании кредиторов
      </xsl:when>
      <xsl:when test="$MT_eng='PropertyEvaluationReport'">
        Сведения об активах<br/>	Отчет оценщика об оценке имущества должника
      </xsl:when>
      <xsl:when test="$MT_eng='Annul'">	Аннулирование ранее опубликованного сообщения</xsl:when>
      <xsl:when test="$MT_eng='CourtAssertAcceptance'">
        Решения арбитражного суда<br/>	Объявление о принятии арбитражным судом заявления
      </xsl:when>
      <xsl:when test="$MT_eng='FinancialStateInformation'">
        Сведения об активах<br/>	Информация о финансовом состоянии
      </xsl:when>
      <xsl:when test="$MT_eng='ChangeAdministration'">
        Сообщения по финансовым организациям<br/>	Изменение состава временной администрации
      </xsl:when>
      <xsl:when test="$MT_eng='DemandAnnouncement'">
        Сообщения по финансовым организациям<br/>	Извещение о возможности предъявления требований
      </xsl:when>
      <xsl:when test="$MT_eng='TerminationAdministration'">
        Сообщения по финансовым организациям<br/>	Прекращение деятельности временной администрации
      </xsl:when>
      <xsl:when test="$MT_eng='AssetsReturning'">
        Сведения об активах<br/>	Объявление о возврате ценных бумаг и иного имущества
      </xsl:when>
      <xsl:when test="$MT_eng='AppointAdministration'">
        Сообщения по финансовым организациям<br/> Решение о назначении временной администрации
      </xsl:when>
      <xsl:when test="$MT_eng='SaleContractResult'">
        Организация и проведение реализации имущества<br/>	Сведения о заключении договора купли-продажи
      </xsl:when>
      <xsl:when test="$MT_eng='MeetingResult'">
        Собрания и комитеты кредиторов<br/>	Сообщение о результатах проведения собрания кредиторов
      </xsl:when>
      <xsl:when test="$MT_eng='ReceivingCreditorDemand'">	Уведомление о получении требования кредитора</xsl:when>
      <xsl:when test="$MT_eng='Committee'">
        Собрания и комитеты кредиторов<br/>	Уведомление о проведении комитета кредиторов
      </xsl:when>
      <xsl:when test="$MT_eng='CommitteeResult'">
        Собрания и комитеты кредиторов<br/>	Сообщение о результатах проведения комитета кредиторов
      </xsl:when>
      <xsl:when test="$MT_eng='SaleOrderPledgedProperty'">
        Организация и проведение реализации имущества<br/> Определение об определении начальной продажной цены, утверждении порядка и условий проведения торгов по реализации предмета залога, порядка и условий  обеспечения сохранности предмета залога
      </xsl:when>
      <xsl:when test="$MT_eng='DeliberateBankruptcy'">	Сообщение о наличии или об отсутствии признаков преднамеренного или фиктивного банкротства</xsl:when>
      <xsl:when test="$MT_eng='DealInvalid'">
        Оспаривание сделки<br/>	Заявление о признании сделки должника  недействительной
      </xsl:when>
      <xsl:when test="$MT_eng='DeclarationPersonSubsidiary'">
        Ответственность  контролирующих лиц<br/>	Заявление о привлечении контролирующих должника лиц  к субсидиарной ответственности
      </xsl:when>
      <xsl:when test="$MT_eng='DeclarationPersonDamages'">
        Ответственность  контролирующих лиц<br/> Заявление о привлечении контролирующих должника лиц, а также иных лиц, к ответственности в виде возмещения убытков
      </xsl:when>
      <xsl:when test="$MT_eng='MeetingWorker'">
        Собрание работников должника<br/>	Уведомление о проведении собрания работников, бывших работников должника
      </xsl:when>
      <xsl:when test="$MT_eng='ActPersonSubsidiary'">
        Ответственность  контролирующих лиц<br/>	Акт о привлечении контролирующих должника лиц к субсидиарной ответственности
      </xsl:when>
      <xsl:when test="$MT_eng='ActDealInvalid'">
        Оспаривание сделки<br/>	Акт о признании/не признании сделки должника недействительной
      </xsl:when>
      <xsl:when test="$MT_eng='MeetingWorkerResult'">
        Собрание работников должника<br/>	Сведения о решениях, принятых собранием работников, бывших работников должника
      </xsl:when>
      <xsl:when test="$MT_eng='ActPersonDamages'">
        Ответственность  контролирующих лиц<br/>	Акт о привлечении контролирующих должника лиц, а также иных лиц, к ответственности в виде возмещения убытков
      </xsl:when>
      <xsl:when test="$MT_eng='ActReviewDealInvalid'">
        Оспаривание сделки<br/>	Акт о пересмотре признания/не признания сделки должника недействительной
      </xsl:when>
      <xsl:when test="$MT_eng='ActReviewPersonSubsidiary'">
        Ответственность  контролирующих лиц<br/>	Акт о пересмотре судебного акта о привлечении контролирующих должника лиц к субсидиарной ответственности
      </xsl:when>
      <xsl:when test="$MT_eng='ActReviewPersonDamages'">
        Ответственность  контролирующих лиц<br/>	Акт о пересмотре судебного акта о привлечении контролирующих должника лиц, а также иных лиц, к ответственности в виде возмещения убытков плана реструктуризации
      </xsl:when>
      <xsl:when test="$MT_eng='ViewDraftRestructuringPlan'">
        Сведения об исполнении плана рестрктуризации<br/>	Сведения о порядке и месте ознакомления с проектом плана реструктуризации
      </xsl:when>
      <xsl:when test="$MT_eng='TransferOwnershipRealEstate'">
        Сведения об активах<br/>	Сообщение о переходе права собственности на объект незавершенного строительства и прав на земельный участок
      </xsl:when>
      <xsl:when test="$MT_eng='BankPayment'">
        Сообщения по финансовым организациям<br/> Объявление о выплатах Банка России
      </xsl:when>
      <xsl:when test="$MT_eng='IntentionCreditOrg'">
        Сообщения по финансовым организациям<br/>	Сообщение о намерении исполнить объязательства кредитной организации
      </xsl:when>
      <xsl:when test="$MT_eng='LiabilitiesCreditOrg'">
        Сообщения по финансовым организациям<br/>	Сообщение о признании исполнения заявителем объязательств кредитной организации несостоявшимся
      </xsl:when>
      <xsl:when test="$MT_eng='PerformanceCreditOrg'">
        Сообщения по финансовым организациям<br/>	Сообщение о исполнении объязательств кредитной организации
      </xsl:when>
      <xsl:when test="$MT_eng='BuyingProperty'">
        Сообщения по финансовым организациям<br/>
        Сообщение о преимущественном праве выкупа имущества
      </xsl:when>
      <xsl:when test="$MT_eng='BeginExecutoryProcess'">
        Сообщение судебного пристава<br/> Начало исполнительного производства
      </xsl:when>
      <xsl:when test="$MT_eng='TransferAssertsForImplementation'">
        Сообщение судебного пристава<br/> Передача имущества на реализацию
      </xsl:when>
      <xsl:when test="$MT_eng='ViewExecRestructuringPlan'">
        Сведения об исполнении плана рестрктуризации<br/>	Сведения о порядке и месте ознакомления с отчетом о результатах исполнения плана реструктуризации
      </xsl:when>
      <xsl:when test="$MT_eng='ChangeDeliberateBankruptcy'">Сообщение об изменении сообщения о наличии или об отсутствии признаков преднамеренного или фиктивного банкротства</xsl:when>
      <xsl:otherwise>Неизвестный тип</xsl:otherwise>
    </xsl:choose>

  </xsl:template>
</xsl:stylesheet>