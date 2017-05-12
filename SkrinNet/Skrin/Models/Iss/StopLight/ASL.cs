using serch_contra_bll.ActionStoplight;
using Skrin.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.StopLight
{
    public class ASL
    {
        public int? total_count { get; set; }
        public List<asl_item> items { get; set; }


        public static ASL Create(ActionStopLight sl)
        {
            if (sl == null)
                return null;
            return new ASL(sl);
        }

        private ASL(ActionStopLight sl)
        {
            items = new List<asl_item>();
            total_count = sl.TotalCount;
            foreach (var item in sl.Factors)
            {
                items.Add(new asl_item
                {
                    factor = item.Key.GetDescription<ActionFactorType>(),
                    info = item.Value.Info,
                    color = item.Value.Color
                });
            }
        }
    }

    public class asl_item
    {
        public string factor { get; set; }
        public string info { get; set; }
        public ColorRate color { get; set; }
        public string color_class
        {
            get
            {
                switch (color)
                {
                    case ColorRate.Red:
                        return "border-red";
                    case ColorRate.Yellow:
                        return "border-yellow";
                    case ColorRate.Green:
                        return "border-green";
                    default:
                        return "";
                }
            }
        }
    }
}