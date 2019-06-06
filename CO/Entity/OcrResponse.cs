using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CO.Entity
{
    class OcrResponse
    {

        //log_id  是 uint64  唯一的log id，用于问题定位
        //words_result    是 array() 识别结果数组
        //words_result_num    是 uint32  识别结果数，表示words_result的元素个数
        //+words 否   string 识别结果字符串
        //probability 否   float 识别结果中每一行的置信度值，包含average：行置信度平均值，variance：行置信度方差，min：行置信度最小值
        //language	false	int32 当detect_language = true时存在
        /// <summary>
        ///    direction 否   int32 图像方向，当detect_direction=true时存在。
        ///- -1:未定义，
        ///- 0:正向，
        ///- 1: 逆时针90度，
        ///- 2:逆时针180度，
        ///- 3:逆时针270度
        /// </summary>
       // public int Direction { set; get; }
      
        /// <summary>
        /// 唯一的log id，用于问题定位
        /// </summary>
        [JsonProperty("log_id")]
        public long LogId { set; get; }
        /// <summary>
        ///  识别结果数组
        /// </summary>
        [JsonProperty("words_result")]
        public List<OcrWord> WordsResult { set; get; }

        [JsonProperty("words_result_num")]
        /// <summary>
        /// 识别结果数，表示words_result的元素个数
        /// </summary>
        public int WordsResultNum { set; get; }

        [JsonProperty("language")]
        /// <summary>
        /// 当detect_language = true时存在
        /// </summary>
        public int Language { set; get; }
    }
}
