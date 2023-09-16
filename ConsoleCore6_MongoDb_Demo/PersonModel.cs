using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Simple_Demo
{
    internal class PersonModel
    {
        //这两条Attribute指定Id字段映射为ObjectId也就是记录条目的主键。
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }  //注意Id的数据类型为string。

        //对数据库中的属性字段进行Mapping。
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        [BsonElement("LastName")]
        public string LastName { get; set; }
    }
}
