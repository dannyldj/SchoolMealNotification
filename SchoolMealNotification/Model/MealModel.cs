using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMealNotification.Model
{
    public class RESULT
    {
        public string CODE { get; set; }
        public string MESSAGE { get; set; }
    }

    public class Head
    {
        // 원인불명으로 Null이 입력되는 경우 존재
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int list_total_count { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public RESULT RESULT { get; set; }
    }

    public class Row
    {
        public string ATPT_OFCDC_SC_CODE { get; set; }
        public string ATPT_OFCDC_SC_NM { get; set; }
        public string SD_SCHUL_CODE { get; set; }
        public string SCHUL_NM { get; set; }
        public string MMEAL_SC_CODE { get; set; }
        public string MMEAL_SC_NM { get; set; }
        public string MLSV_YMD { get; set; }
        public string MLSV_FGR { get; set; }
        public string DDISH_NM { get; set; }
        public string ORPLC_INFO { get; set; }
        public string CAL_INFO { get; set; }
        public string NTR_INFO { get; set; }
        public string MLSV_FROM_YMD { get; set; }
        public string MLSV_TO_YMD { get; set; }
    }

    public class MealServiceDietInfo
    {
        public List<Head> head { get; set; }
        public List<Row> row { get; set; }
    }

    public class MealModel : BindableBase
    {
        public List<MealServiceDietInfo> mealServiceDietInfo { get; set; }
    }
}
