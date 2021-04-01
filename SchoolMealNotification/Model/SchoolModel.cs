using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMealNotification.Model.School
{
    public class RESULT
    {
        [JsonProperty("CODE")]
        public string code { get; set; }

        [JsonProperty("MESSAGE")]
        public string message { get; set; }
    }

    public class Head
    {
        // 원인불명으로 Null이 입력되는 경우 존재
        [JsonProperty("list_total_count", NullValueHandling = NullValueHandling.Ignore)]
        public int listTotalCount { get; set; }

        [JsonProperty("RESULT", NullValueHandling = NullValueHandling.Ignore)]
        public RESULT result { get; set; }
    }

    public class Row
    {
        public string ATPT_OFCDC_SC_CODE { get; set; }
        public string ATPT_OFCDC_SC_NM { get; set; }
        public string SD_SCHUL_CODE { get; set; }
        public string SCHUL_NM { get; set; }
        public string ENG_SCHUL_NM { get; set; }
        public string SCHUL_KND_SC_NM { get; set; }
        public string LCTN_SC_NM { get; set; }
        public string JU_ORG_NM { get; set; }
        public string FOND_SC_NM { get; set; }
        public string ORG_RDNZC { get; set; }
        public string ORG_RDNMA { get; set; }
        public string ORG_RDNDA { get; set; }
        public string ORG_TELNO { get; set; }
        public string HMPG_ADRES { get; set; }
        public string COEDU_SC_NM { get; set; }
        public string ORG_FAXNO { get; set; }
        public string HS_SC_NM { get; set; }
        public string INDST_SPECL_CCCCL_EXST_YN { get; set; }
        public string HS_GNRL_BUSNS_SC_NM { get; set; }
        public string SPCLY_PURPS_HS_ORD_NM { get; set; }
        public string ENE_BFE_SEHF_SC_NM { get; set; }
        public string DGHT_SC_NM { get; set; }
        public string FOND_YMD { get; set; }
        public string FOAS_MEMRD { get; set; }
        public string LOAD_DTM { get; set; }
    }

    public class SchoolInfo
    {
        public List<Head> head { get; set; }
        public List<Row> row { get; set; }
    }

    public class SchoolModel : BindableBase
    {
        public List<SchoolInfo> schoolInfo { get; set; }
    }
}
