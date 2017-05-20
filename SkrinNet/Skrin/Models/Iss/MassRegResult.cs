using Skrin.BLL.Iss;
using SkrinService.Domain.AddressSearch;
using SkrinService.Domain.AddressSearch.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss
{
    public class MassRegResult
    {
        public QueueStatus QStatus { get; set; }
        public string Street { get; set; }
        public Result SearchResult { get; set; }
        public List<HomeEditItem> Items { get; set; }
        public string OriginalAddress { get; set; }


        private HomeListEdit AddressList;

        public MassRegResult(string ticker)
        {
            AddressList = null;
            MassRegRepository rep = new MassRegRepository(ticker);
            QStatus = rep.GetQueueStatus();
            if (QStatus == QueueStatus.Ready)
            {
                string address = rep.GetAddress();
                if (!string.IsNullOrEmpty(address))
                {
                    //проверим на ошибки
                    string exeption = rep.GetExeption();
                    if (!string.IsNullOrEmpty(exeption))
                        throw new Exception("#_#" + exeption);

                    //Смотрим есть ли записанные результаты поиска по этому адресу
                    //если есть то возращаем их, иначе, выдаем статус ожидания и ставим в очередь на поиск
                    AddressList = rep.GetReady();
                    if (AddressList == null)
                    {
                        QStatus = QueueStatus.InQueue;
                        rep.InsertInMassRegQueue();
                    }
                    else
                    {
                        OriginalAddress = address;
                        ParseHomeListEdit(AddressList);
                    }
                }
            }
            else
            {
                //rep.InsertInMassRegQueue();
            }
        }

        private void ParseHomeListEdit(HomeListEdit hl)
        {
            //Street = Encoding.UTF8.GetString(Encoding.GetEncoding("windows-1251").GetBytes(hl.Street));
            Street = hl.Street;
            SearchResult = hl.SearchResult;
            Items = hl.Items;
        }

        public MassRegResult(QueueStatus status)
        {
            AddressList = null;
            QStatus = status;
        }
    }

    public enum QueueStatus
    {
        Ready = 0,
        InQueue = 1,
        NoRecord = 2,
        LowPriority = 3,
        ErrorData = 4,
        FewRecordsWithInn = 5,
        ErrorInSearch = 6,
        ErrorInAddress = 7,
        NoRights=8
    }
}