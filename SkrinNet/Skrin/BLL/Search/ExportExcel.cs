using ClosedXML.Excel;
using Skrin.Models.Search;
using Skrin.Models.Iss.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Skrin.Models.Iss.Debt;
using ULSearch;

namespace Skrin.BLL.Search
{
    public class ExportExcel
    {
        public XLWorkbook ExportWorkbook(List<ULDetails> list, bool only_header = false)
        {
            // Creating a new workbook
            var wb = new XLWorkbook();

            //Adding a worksheet
            var ws = wb.Worksheets.Add("Report");

            List<string> aHead = new List<string> { "Наименование", "ИНН", "Регион", "ОКПО", "ОКВЭД", "Отрасль", "ОГРН", "Дата гос.\nрегистрации", "Орган гос. \nрегистрации", "Адрес", "Руководитель", "Телефон", "Факс", "E-mail", "Сайт", "Состояние" };
            List<int> aWidth = new List<int> { 40, 15, 30, 15, 15, 50, 20, 40, 40, 40, 30, 30, 30, 30, 30, 30 };

            //Заголовки
            ws.Row(1).Style.Font.Bold = true;
            ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Row(1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
            for (int j = 0; j <= aHead.Count - 1; j++)
            {
                ws.Column(j + 1).Width = (int)aWidth[j];
                ws.Cell(1, j + 1).Value = (string)aHead[j];
            }
            if (!only_header)
            {
                for (int i = 0; i <= list.Count - 1; i++)
                {
                    ws.Row(i + 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    ws.Row(i + 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                    ws.Cell(i + 2, 1).Value = (!String.IsNullOrEmpty(list[i].nm)) ? list[i].nm : list[i].name;
                    ws.Cell(i + 2, 2).Value = list[i].inn.ToString();
                    ws.Cell(i + 2, 3).Value = list[i].region;
                    ws.Cell(i + 2, 4).Value = list[i].okpo.ToString();
                    var cel = ws.Cell(i + 2, 5);
                    string okved_code = "'" + (string)list[i].okved_code;
                    cel.Style.NumberFormat.NumberFormatId = 49;
                    cel.Value = okved_code;
                    ws.Cell(i + 2, 6).Value = list[i].okved;
                    var cell = ws.Cell(i + 2, 7);
                    string ogrn = (string)list[i].ogrn;
                    cell.Style.NumberFormat.Format = "#";
                    cell.Value = ogrn;
                    ws.Cell(i + 2, 8).Value = list[i].reg_date;
                    ws.Cell(i + 2, 9).Value = list[i].reg_org_name;
                    ws.Cell(i + 2, 10).Value = list[i].legal_address;
                    ws.Cell(i + 2, 11).Value = list[i].ruler;
                    ws.Cell(i + 2, 12).Value = list[i].legal_phone;
                    ws.Cell(i + 2, 13).Value = list[i].legal_fax;
                    ws.Cell(i + 2, 14).Value = list[i].legal_email;
                    ws.Cell(i + 2, 15).Value = list[i].www;
                    ws.Cell(i + 2, 16).Value = list[i].del;
                }
            }

            return wb;
        }
        public XLWorkbook ExportUAWorkbook(List<UADetails> list, bool only_header = false)
        {
            // Creating a new workbook
            var wb = new XLWorkbook();

            //Adding a worksheet
            var ws = wb.Worksheets.Add("Report");

            List<string> aHead = new List<string> { "Наименование", "ЕДРПОУ", "Регион", "Отрасль", "Регистрационный номер", "Дата гос.регистрации", "Орган гос. регистрации", "Адрес", "Руководитель", "Телефон", "Факс", "E-mail", "Сайт" };
            List<int> aWidth = new List<int> { 40, 15, 30, 50, 20, 40, 40, 40, 30, 30, 30, 30, 30 };

            //Заголовки
            ws.Row(1).Style.Font.Bold = true;
            ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Row(1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
            for (int j = 0; j <= aHead.Count - 1; j++)
            {
                ws.Column(j + 1).Width = (int)aWidth[j];
                ws.Cell(1, j + 1).Value = (string)aHead[j];
            }
            if (!only_header)
            {
                for (int i = 0; i <= list.Count - 1; i++)
                {
                    ws.Row(i + 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    ws.Row(i + 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                    ws.Cell(i + 2, 1).Value = (!String.IsNullOrEmpty(list[i].name)) ? list[i].name : list[i].name;
                    ws.Cell(i + 2, 2).Value = list[i].edrpou.ToString();
                    ws.Cell(i + 2, 3).Value = list[i].region;
                    ws.Cell(i + 2, 4).Value = list[i].industry;
                    var cell = ws.Cell(i + 2, 5);
                    string ogrn = (string)list[i].regno;
                    cell.Style.NumberFormat.Format = "#";
                    cell.Value = ogrn;
                    ws.Cell(i + 2, 6).Value = list[i].regdate;
                    ws.Cell(i + 2, 7).Value = list[i].regorg;
                    ws.Cell(i + 2, 8).Value = list[i].addr;
                    ws.Cell(i + 2, 9).Value = list[i].ruler;
                    ws.Cell(i + 2, 10).Value = list[i].phone;
                    ws.Cell(i + 2, 11).Value = list[i].fax;
                    ws.Cell(i + 2, 12).Value = list[i].email;
                    ws.Cell(i + 2, 13).Value = list[i].www;
                }
            }

            return wb;
        }
        public XLWorkbook ExportKZWorkbook(List<KZDetails> list, bool only_header = false)
        {
            // Creating a new workbook
            var wb = new XLWorkbook();

            //Adding a worksheet
            var ws = wb.Worksheets.Add("Report");

            List<string> aHead = new List<string> { "Наименование", "ОКРО", "Регион", "Отрасль", "Регистрационный номер", "Дата гос.регистрации", "Адрес", "Руководитель", "Телефон", "Факс", "E-mail" };
            List<int> aWidth = new List<int> { 50, 12, 15, 20, 14, 12, 50, 30, 15, 15, 15 };
            //Заголовки
            ws.Row(1).Style.Font.Bold = true;
            ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Row(1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
            for (int j = 0; j <= aHead.Count - 1; j++)
            {
                ws.Column(j + 1).Width = (int)aWidth[j];
                ws.Cell(1, j + 1).Value = (string)aHead[j];
            }
            if (!only_header)
            {
                for (int i = 0; i <= list.Count - 1; i++)
                {
                    ws.Row(i + 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    ws.Row(i + 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                    ws.Cell(i + 2, 1).Value = list[i].Name;
                    ws.Cell(i + 2, 2).Value = list[i].Code;
                    ws.Cell(i + 2, 2).Style.NumberFormat.Format = "#";
                    ws.Cell(i + 2, 3).Value = list[i].RegionName;
                    ws.Cell(i + 2, 4).Value = list[i].MainDeal;
                    ws.Cell(i + 2, 5).Value = list[i].CodeTax;
                    ws.Cell(i + 2, 5).Style.NumberFormat.Format = "#";
                    ws.Cell(i + 2, 6).Value = list[i].DateReg;
                    ws.Cell(i + 2, 7).Value = list[i].FullAddress;
                    ws.Cell(i + 2, 8).Value = list[i].Manager;
                    ws.Cell(i + 2, 9).Value = list[i].Phone;
                    ws.Cell(i + 2, 10).Value = list[i].Fax;
                    ws.Cell(i + 2, 11).Value = list[i].Email;

                }
            }

            return wb;
        }

        public XLWorkbook ExportWBElastic(List<ULExportElastic> list, bool only_header = false)
        {
            // Creating a new workbook
            var wb = new XLWorkbook();

            //Adding a worksheet
            var ws = wb.Worksheets.Add("Report");

            List<string> aHead = new List<string> { "Наименование", "ИНН", "Регион", "ОКПО", "ОКВЭД", "Отрасль", "ОГРН", "Дата гос.\nрегистрации", "Орган гос. \nрегистрации", "Адрес", "Руководитель", "Телефон", "Факс", "E-mail", "Сайт", "Статус Росстата", "Сведения о состоянии" };
            List<int> aWidth = new List<int> { 40, 15, 30, 15, 15, 50, 20, 40, 40, 40, 30, 30, 30, 30, 30, 30, 30 };

            //Заголовки
            ws.Row(1).Style.Font.Bold = true;
            ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Row(1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
            for (int j = 0; j <= aHead.Count - 1; j++)
            {
                ws.Column(j + 1).Width = (int)aWidth[j];
                ws.Cell(1, j + 1).Value = (string)aHead[j];
            }
            if (!only_header)
            {
                for (int i = 0; i <= list.Count - 1; i++)
                {
                    ws.Row(i + 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    ws.Row(i + 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                    ws.Cell(i + 2, 1).Value = (!String.IsNullOrEmpty(list[i].short_name)) ? list[i].short_name : list[i].name;
                    ws.Cell(i + 2, 2).Value = (!String.IsNullOrEmpty(list[i].inn)) ? list[i].inn.ToString() : "";
                    ws.Cell(i + 2, 3).Value = (!String.IsNullOrEmpty(list[i].region_name)) ? list[i].region_name : "";
                    ws.Cell(i + 2, 4).Value = (!String.IsNullOrEmpty(list[i].okpo)) ? list[i].okpo.ToString() : "";
                    var cel = ws.Cell(i + 2, 5);
                    string okved_code = "'" + ((!String.IsNullOrEmpty(list[i].okved)) ? (string)list[i].okved : "");
                    cel.Style.NumberFormat.NumberFormatId = 49;
                    cel.Value = okved_code;
                    ws.Cell(i + 2, 6).Value = (!String.IsNullOrEmpty(list[i].okved_name)) ? list[i].okved_name : "";
                    var cell = ws.Cell(i + 2, 7);
                    string ogrn = (!String.IsNullOrEmpty(list[i].ogrn)) ? (string)list[i].ogrn : "";
                    cell.Style.NumberFormat.Format = "#";
                    cell.Value = ogrn;
                    ws.Cell(i + 2, 8).Value = (!String.IsNullOrEmpty(list[i].reg_date)) ? list[i].reg_date : "";
                    ws.Cell(i + 2, 9).Value = (!String.IsNullOrEmpty(list[i].reg_org_name)) ? list[i].reg_org_name : "";
                    ws.Cell(i + 2, 10).Value = (!String.IsNullOrEmpty(list[i].address)) ? list[i].address : "";
                    ws.Cell(i + 2, 11).Value = (!String.IsNullOrEmpty(list[i].ruler)) ? list[i].ruler : "";
                    ws.Cell(i + 2, 12).Value = (!String.IsNullOrEmpty(list[i].phone)) ? list[i].phone : "";
                    ws.Cell(i + 2, 13).Value = (!String.IsNullOrEmpty(list[i].fax)) ? list[i].fax : "";
                    ws.Cell(i + 2, 14).Value = (!String.IsNullOrEmpty(list[i].email)) ? list[i].email : "";
                    ws.Cell(i + 2, 15).Value = (!String.IsNullOrEmpty(list[i].www)) ? list[i].www : "";
                    if (!String.IsNullOrEmpty(list[i].del_date)) list[i].del_date = "Исключено из реестра Росстата " + list[i].del_date;
                    else list[i].del_date = "";
                    ws.Cell(i + 2, 16).Value = list[i].del_date;
                    string status = "";
                    for (int j = 0; j <= list[i].status_list.Count - 1; j++)
                    {
                        status += (j > 0 ? ", " : "") + list[i].status_list[j].status_name + (!String.IsNullOrEmpty(list[i].status_list[j].status_date) ? " от " + list[i].status_list[j].status_date : "");
                    }
                    ws.Cell(i + 2, 17).Value = status;
                }
            }

            return wb;
        }

        #region Pravo Export
        public XLWorkbook ExportPravo(List<PravoDetail> list, CompanyData company)
        {
            var wb = new XLWorkbook();

            //Adding a worksheet
            var ws = wb.Worksheets.Add("Report");

            List<string> aHead = new List<string> { "Совпадение", "Дата поступления дела в суд", "Номер дела", "Категория дела", "Сумма иска в руб.", "Арбитражный суд", "Форма участия", "Истец", "Ответчик" };
            List<int> aWidth = new List<int> { 30, 20, 20, 20, 15, 40, 20, 50, 50 };

            //Заголовки
            ws.Row(1).Style.Font.Bold = true;
            ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Row(1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
            for (int j = 0; j <= aHead.Count - 1; j++)
            {
                ws.Column(j + 1).Width = (int)aWidth[j];
                ws.Cell(1, j + 1).Value = (string)aHead[j];
            }
            for (int i = 0; i <= list.Count - 1; i++)
            {
                ws.Row(i + 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Row(i + 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                switch (list[i].rmode)
                {
                    case 1:
                        ws.Cell(i + 2, 1).Value = "по ИНН и ОГРН";
                        break;
                    case 2:
                        ws.Cell(i + 2, 1).Value = "по ИНН или ОГРН";
                        break;
                    case 3:
                        ws.Cell(i + 2, 1).Value = "по наименованию";
                        break;
                }
                ws.Cell(i + 2, 2).Value = list[i].reg_date;
                ws.Cell(i + 2, 3).Value = list[i].reg_no;
                ws.Cell(i + 2, 4).Value = list[i].disput_type_categ;
                ws.Cell(i + 2, 5).Value = list[i].case_sum;
                ws.Cell(i + 2, 6).Value = list[i].cname;
                ws.Cell(i + 2, 7).Value = list[i].side_type_name;
                ws.Cell(i + 2, 8).Value = list[i].ext_ist_list;
                ws.Cell(i + 2, 9).Value = list[i].ext_otv_list;
            }
            return wb;
        }

        #endregion

        #region Debt Export
        public XLWorkbook ExportDebt(List<DebtItem> list)
        {
            var wb = new XLWorkbook();

            //Adding a worksheet
            var ws = wb.Worksheets.Add("Report");

            List<string> aHead = new List<string> { "Номер исполнительного производства", "Статус исполнительного производства", "Дата возбуждения", "Должник", "Адрес", "Регион"
                , "Предмет исполнения", "Номер сводного исполнительного производства", "Тип исполнительного документа", "Дата исполнительного документа"
                , "Номер исполнительного документа", "Требования исполнительного документа" , "Остаток задолженности (руб.)"
                , "Дата окончания (прекращения) ИП", "Причина окончания (прекращения) ИП (статья, часть, пункт основания)"
                , "Отдел судебных приставов", "Адрес отдела судебных приставов" };
            List<int> aWidth = new List<int> { 20, 13, 11, 75, 56, 25, 24, 20, 50, 11, 21, 60, 11, 11, 12, 13, 50 };

            //Заголовки
            ws.Row(1).Style.Font.Bold = true;
            ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Row(1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
            for (int j = 0; j <= aHead.Count - 1; j++)
            {
                ws.Column(j + 1).Width = (int)aWidth[j];
                ws.Cell(1, j + 1).Value = (string)aHead[j];
            }
            for (int i = 0; i <= list.Count - 1; i++)
            {
                ws.Row(i + 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Row(i + 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                ws.Cell(i + 2, 1).Value = list[i].NumProizv;
                ws.Cell(i + 2, 2).Value = (list[i].Status == 0 ? "Не завершено" : "Завершено");
                ws.Cell(i + 2, 3).Value = list[i].DateProizv;
                ws.Cell(i + 2, 4).Value = list[i].DebtorName;
                ws.Cell(i + 2, 5).Value = list[i].DebtorAddress;
                ws.Cell(i + 2, 6).Value = list[i].Region;
                ws.Cell(i + 2, 7).Value = list[i].Predmet;
                ws.Cell(i + 2, 8).Value = list[i].NumSvodPr;
                ws.Cell(i + 2, 9).Value = list[i].DocumentType;
                ws.Cell(i + 2, 10).Value = list[i].DocumentDate;
                ws.Cell(i + 2, 11).Value = list[i].DocumentNum;
                ws.Cell(i + 2, 12).Value = list[i].DocumentReq;
                ws.Cell(i + 2, 13).Value = list[i].Sum;
                ws.Cell(i + 2, 14).Value = list[i].CloseDate;
                ws.Cell(i + 2, 15).Value = list[i].CloseCause;
                ws.Cell(i + 2, 16).Value = list[i].PristavName;
                ws.Cell(i + 2, 17).Value = list[i].PristavAddress;
            }
            return wb;
        }

        #endregion

        #region Zakupki Export
        public XLWorkbook ExportZakupki(List<ZakupkiDetail> list)
        {
            var wb = new XLWorkbook();
            //Adding a worksheet
            var ws = wb.Worksheets.Add("Госконтракты");

            List<string> aHead = new List<string> { "Дата публикации закупки", "Номер сведений о закупке", "Способ  размещения закупки", "Статус закупки", "Предмет закупки", "Участники закупки", "Заказчик", "Начальная цена контракта (в рублях)", "Цена контракта (в рублях)", "Изменение начальной цены (в рублях/%)", "Изменение начальной цены (%)", "Дата публикации контракта", "Номер сведений о контракте", "Способ размещения", "Статус", "Предмет контракта / договора", "Поставщик", "Заказчик" };
            List<int> aWidth = new List<int> { 20, 40, 40, 40, 50, 50, 50, 20, 20, 20, 20, 20, 20, 40, 30, 50, 50, 50 };

            //Заголовки
            ws.Row(1).Style.Font.Bold = true;
            ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Row(1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
            ws.Range("A1", "H1").Style.Fill.BackgroundColor = XLColor.LightCoral;
            ws.Range("A1", "H1").Merge().Value = "ЗАКУПКА";
            ws.Range("I1", "R1").Style.Fill.BackgroundColor = XLColor.LightBlue;
            ws.Range("I1", "R1").Merge().Value = "КОНТРАКТ";
            ws.Row(2).Style.Font.Bold = true;
            ws.Row(2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Row(2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
            ws.Range("A2", "H2").Style.Fill.BackgroundColor = XLColor.LightCoral;
            ws.Range("I2", "R2").Style.Fill.BackgroundColor = XLColor.LightBlue;
            for (int j = 0; j <= aHead.Count - 1; j++)
            {
                ws.Column(j + 1).Width = (int)aWidth[j];
                ws.Cell(2, j + 1).Value = (string)aHead[j];
            }

            for (int i = 0; i <= list.Count - 1; i++)
            {
                ws.Row(i + 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Row(i + 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                ws.Cell(i + 3, 1).Value = list[i].not_publish_date;
                ws.Cell(i + 3, 2).Value = list[i].sourse_fz + ((list[i].pur_num != "") ? ", " + list[i].pur_num : "") + ((list[i].lot_num != "") ? ", " + list[i].lot_num : "");
                ws.Cell(i + 3, 3).Value = list[i].not_type;
                ws.Cell(i + 3, 4).Value = list[i].not_status_name;
                ws.Cell(i + 3, 5).Value = list[i].not_product;
                ws.Cell(i + 3, 6).Value = list[i].not_part;
                ws.Cell(i + 3, 7).Value = list[i].not_cust;
                ws.Cell(i + 3, 8).Value = list[i].st_not_sum;
                ws.Cell(i + 3, 9).Value = list[i].contr_sum;
                ws.Cell(i + 3, 10).Value = list[i].dif_sum;
                ws.Cell(i + 3, 11).Value = list[i].dif_per;
                ws.Cell(i + 3, 12).Value = list[i].contr_pub_date;
                var cell = ws.Cell(i + 3, 13);
                string ogrn = (string)list[i].contr_reg_num;
                cell.Style.NumberFormat.Format = "#";
                cell.Value = ogrn;
                //ws.Cell(i + 3, 13).Value = list[i].contr_reg_num;
                ws.Cell(i + 3, 14).Value = list[i].contr_placing;
                ws.Cell(i + 3, 15).Value = list[i].contr_stage;
                ws.Cell(i + 3, 16).Value = list[i].contr_product_list;
                ws.Cell(i + 3, 17).Value = list[i].contr_supliers;
                ws.Cell(i + 3, 18).Value = list[i].contr_customer;
            }
            return wb;
        }
        #endregion

        public void SaveStreamToFile(string filename, Stream stream)
        {
            if (stream.Length != 0)
                using (FileStream fileStream = File.Create(filename, (int)stream.Length))
                {
                    // Размещает массив общим размером равным размеру потока
                    // Могут быть трудности с выделением памяти для больших объемов
                    byte[] data = new byte[stream.Length];

                    stream.Read(data, 0, (int)data.Length);
                    fileStream.Write(data, 0, data.Length);
                }
        }
    }
}