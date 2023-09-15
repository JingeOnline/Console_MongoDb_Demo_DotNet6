using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CRUD_Demo.Models
{
    internal class StudentModel
    {
        //这两条Attribute指定Id字段映射为ObjectId也就是记录条目的主键。
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }  //注意Id的数据类型为string。
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
