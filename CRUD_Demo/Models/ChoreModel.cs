using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Demo.Models
{
    /// <summary>
    /// 学生的值日活动
    /// </summary>
    internal class ChoreModel
    {
        //这两条Attribute指定Id字段映射为ObjectId也就是记录条目的主键。
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }  //注意Id的数据类型为string。
        public string ChoreText { get; set; }
        public int FrequencyInDays { get;set; }
        public StudentModel? AssignedTo { get; set; }
        /// <summary>
        /// 如果该活动已经结束，则填入时间。否则为空。
        /// </summary>
        public DateTime? CompletedAt { get; set; }
    }
}
