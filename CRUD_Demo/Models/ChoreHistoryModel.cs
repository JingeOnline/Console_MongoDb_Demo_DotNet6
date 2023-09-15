using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime.Internal.Settings;

namespace CRUD_Demo.Models
{
    /// <summary>
    /// 值日活动的历史记录
    /// </summary>
    internal class ChoreHistoryModel
    {
        //这两条Attribute指定Id字段映射为ObjectId也就是记录条目的主键。
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }  //注意Id的数据类型为string。
        //public string ChoreId { get; set; }
        //public string ChoreText { get; set; }
        public ChoreModel Chore { get; set; }
        public DateTime UpdateDateTime { get; set; }
        //public StudentModel AssignedStudent { get; set; }
        public string UpdateBy { get; set; }

        public ChoreHistoryModel()
        {
            
        }

        public ChoreHistoryModel(ChoreModel chore, string updateBy)
        {
            //ChoreId = chore.Id;
            Chore = chore;
            UpdateDateTime = DateTime.Now;
            //AssignedStudent = chore.AssignedTo;
            //ChoreText = chore.ChoreText;
            UpdateBy = updateBy;
        }
    }
}
