using serch_contra_bll.StopLightFreeEgrul;
using serch_contra_bll.StopLightFreeEgrul.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skrin.BLL.Infrastructure;
using Skrin.Models.Iss.StopLight;

namespace Skrin.BLL.Iss
{
    public class StopLightUtilites
    {
        /// <summary>
        /// Есть ли дополнительная информация по данному стоп-фактору
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static bool HasInfo(StopLightData data, StopFactorTypes factor)
        {
            switch (factor)
            {
                case StopFactorTypes.EgrulExistance:
                    return data.EgrulExistance_Info != null;
                case StopFactorTypes.Legacy1:
                    return data.Legacy1_Info != null;
                case StopFactorTypes.Bankruptcy:
                    return data.Bankruptcy_Info != null;
                case StopFactorTypes.CommingStop:
                    return data.CommingStop_Info != null;
                case StopFactorTypes.StopAction:
                    return data.StopAction_Info != null;
                case StopFactorTypes.Legacy2:
                    return data.Legacy2_Info != null;
                case StopFactorTypes.Disqualification1:
                    return data.Disqualification1_Info != null;
                case StopFactorTypes.Disqualification2:
                    return data.Disqualification2_Info != null;
                case StopFactorTypes.AuthorizedReduce:
                    return data.AuthorizedReduce_Info != null;
                case StopFactorTypes.CompromiseRecords:
                    return data.CompromiseRecords_Info != null;
                case StopFactorTypes.UnfairSupplier:
                    return data.UnfairSupplier_Info != null;
                case StopFactorTypes.Account:
                    return false;
                case StopFactorTypes.Profit:
                    return data.Profit_Info != null;
                case StopFactorTypes.AssetsCost:
                    return data.AssetsCost_Indo != null;
                case StopFactorTypes.LegacyYellow:
                    return data.YellowLegacy_Info != null;
                case StopFactorTypes.BankruptcyYellow:
                    return data.BankruptcyYellow_Info != null;
            }
            return false;
        }

        /// <summary>
        /// Есть ли дополнительная информация по данному стоп-фактору
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static bool HasIPInfo(StopLightIPData data, StopFactorIPTypes factor)
        {
            switch (factor)
            {
                case StopFactorIPTypes.EgripExistance:
                    return data.EgripExistance_Info != null;
                case StopFactorIPTypes.Bankruptcy:
                    return data.Bankruptcy_Info != null;
                case StopFactorIPTypes.Legacy1:
                    return data.Legacy1_Info != null;
                case StopFactorIPTypes.StopAction:
                    return data.StopAction_Info != null;
                case StopFactorIPTypes.Legacy2:
                    return data.Legacy2_Info != null;
                case StopFactorIPTypes.Disqualification2:
                    return data.Disqualification_Info != null;
                case StopFactorIPTypes.CompromiseRecords:
                    return data.CompromiseRecords_Info != null;
                case StopFactorIPTypes.UnfairSupplier:
                    return data.UnfairSupplier_Info != null;
                default:
                    return false;
            }
        }


        /// <summary>
        /// Вытаскивает заголок для светофора
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetHeader(StopLightData data)
        {
            if (data.EgrulExistance_Info != null)
            {
                if (string.IsNullOrEmpty(data.EgrulExistance_Info.ShortName))
                {
                    return data.EgrulExistance_Info.Name;
                }
                else
                {
                    return data.EgrulExistance_Info.ShortName;
                }
            }
            else
            {
                return data.Ogrn;
            }
        }

        /// <summary>
        /// Вытаскивает заголок для светофора
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetHeaderIP(StopLightIPData data)
        {
            if (data.EgripExistance_Info != null)
            {
                return data.EgripExistance_Info.FIO;
            }
            else
            {
                return data.Ogrnip;
            }
        }

        public static List<Dictionary<int, TableRowBlock>> GetUnfairTable(UnFairInfo info)
        {
            List<Dictionary<int, TableRowBlock>> blocks = new List<Dictionary<int, TableRowBlock>>();

            foreach (var item in info.Items)
            {

                blocks.Add(_GetTableBlocks(item));
            }

            return blocks;
        }


        public static List<Dictionary<int, TableRowBlock>> GetUnfairIpTable(UnFairIpInfo info)
        {
            List<Dictionary<int, TableRowBlock>> blocks = new List<Dictionary<int, TableRowBlock>>();

            foreach (var item in info.Items)
            {

                blocks.Add(_GetTableBlocks(item));
            }

            return blocks;
        }


        private static Dictionary<int, TableRowBlock> _GetTableBlocks(UnFairInfoItem item)
        {
            Dictionary<int, TableRowBlock> ret = new Dictionary<int, TableRowBlock>();

            TableRowBlock bl = new TableRowBlock
            {
                Id = 1,
                BlockHeader = "Информация о недобросовестном поставщике (исполнителе, подрядчике)"
            };

            bl.Rows.Add(new TableRow { RowHeader = "Наименование/ФИО недобросовестного поставщика", RowValue = item.ProviderName });
            bl.Rows.Add(new TableRow { RowHeader = "ИНН (или аналог ИНН для иностранного поставщика)", RowValue = item.ProviderInn });
            bl.Rows.Add(new TableRow { RowHeader = "КПП", RowValue = item.ProviderKpp });
            bl.Rows.Add(new TableRow { RowHeader = "Страна", RowValue = item.ProviderCountry });
            bl.Rows.Add(new TableRow { RowHeader = "Адрес поставщика", RowValue = item.ProviderAddress });

            AddNotEmptyBlock(bl, ret);

            bl = new TableRowBlock
            {
                Id = 2,
                BlockHeader = "Общая информация по заявке РНП"
            };

            bl.Rows.Add(new TableRow { RowHeader = "Уполномоченный орган, осуществивший включение сведений в реестр", RowValue = item.RegOrg });
            bl.Rows.Add(new TableRow { RowHeader = "Причина для внесения в Реестр", RowValue = item.AddingReason });
            bl.Rows.Add(new TableRow { RowHeader = "Реестровый номер", RowValue = item.RegNumber });
            bl.Rows.Add(new TableRow { RowHeader = "Дата включения сведений в Реестр", RowValue = item.RegStartDate.ToRusString() });
            bl.Rows.Add(new TableRow { RowHeader = "Дата для исключения", RowValue = item.RegFinishDate.ToRusString() });

            AddNotEmptyBlock(bl, ret);

            bl = new TableRowBlock
            {
                Id = 3,
                BlockHeader = "Информация о заказчике, подавшем заявку на включение в Реестр"
            };

            bl.Rows.Add(new TableRow { RowHeader = "Наименование заказчика", RowValue = item.CustomerName });
            bl.Rows.Add(new TableRow { RowHeader = "ИНН", RowValue = item.CustomerInn });
            bl.Rows.Add(new TableRow { RowHeader = "КПП", RowValue = item.CustomerKpp });
            bl.Rows.Add(new TableRow { RowHeader = "Адрес заказчика", RowValue = item.CustomerAddress });

            AddNotEmptyBlock(bl, ret);

            bl = new TableRowBlock
            {
                Id = 4,
                BlockHeader = "Информация о проведенных закупках"
            };

            bl.Rows.Add(new TableRow { RowHeader = "Наименование заказа/закупки", RowValue = item.TenderSubject });
            bl.Rows.Add(new TableRow { RowHeader = "Дата проведения", RowValue = item.TenderDate.ToRusString() });
            bl.Rows.Add(new TableRow { RowHeader = "Наименование подтверждающего документа", RowValue = item.TenderDoc });
            bl.Rows.Add(new TableRow { RowHeader = "Дата подтверждающего документа", RowValue = item.TenderDocDate.ToRusString() });
            bl.Rows.Add(new TableRow { RowHeader = "Номер подтверждающего документа", RowValue = item.TenderDocNum });


            AddNotEmptyBlock(bl, ret);

            bl = new TableRowBlock
            {
                Id = 5,
                BlockHeader = "Информация о контракте"
            };

            bl.Rows.Add(new TableRow { RowHeader = "Цена контракта", RowValue = item.ContractPrice.ConvertTextSum() });
            bl.Rows.Add(new TableRow { RowHeader = "Валюта контракта", RowValue = item.ContractCurrency });
            bl.Rows.Add(new TableRow { RowHeader = "Дата заключения", RowValue = item.ContractDate.ToRusString() });
            bl.Rows.Add(new TableRow { RowHeader = "Номер реестровой записи в реестре контрактов", RowValue = item.ContractRegNum });
            bl.Rows.Add(new TableRow { RowHeader = "Предмет контракта (наименование товара, работ, услуг)", RowValue = item.ContractServiceCode });
            bl.Rows.Add(new TableRow { RowHeader = "Плановая дата исполнения", RowValue = item.ContractPerfomanceDate.ToRusString() });
            bl.Rows.Add(new TableRow { RowHeader = "Дата расторжения контракта", RowValue = item.ContractCancelDate.ToRusString() });
            bl.Rows.Add(new TableRow { RowHeader = "Основание для расторжения контракта", RowValue = item.ContractCancelBase });

            AddNotEmptyBlock(bl, ret);

            return ret;
        }

        private static void AddNotEmptyBlock(TableRowBlock bl, Dictionary<int, TableRowBlock> dic)
        {
            bl = bl.GetNotEmptyTableRowBlock();
            if (bl != null)
            {
                dic.Add(bl.Id, bl);
            }
        }


        
    }
}