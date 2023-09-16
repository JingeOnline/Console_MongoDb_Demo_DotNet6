using MongoDB.Driver;
using Simple_Demo;

//视频教程：https://www.youtube.com/watch?v=exXavNOqaVo&t=1907s&pp=ygUKQyMgTW9uZ29kYg%3D%3D
//视频教程：https://www.youtube.com/watch?v=iWTdJ1IYGtg&ab_channel=kudvenkat

namespace ConsoleCore6_MongoDb_Demo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //数据库连接字符串
            string connectString = "mongodb://127.0.0.1:27017";
            //数据库名称
            string databaseName = "dotnet_test_db1";
            //表（collection）名
            string collectionName = "people";

            var client=new MongoClient(connectString);
            var db=client.GetDatabase(databaseName);
            var collection = db.GetCollection<PersonModel>(collectionName);
            
            //await Insert(collection);
            await FindAll(collection);
        }

        /// <summary>
        /// 执行该方法之后，如果数据库和表不存在，会自动创建库和表，然后插入数据。
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static async Task Insert(IMongoCollection<PersonModel> collection)
        {
            PersonModel model = new PersonModel() { FirstName="Tim", LastName="Corey"};
            await collection.InsertOneAsync(model);
            Console.WriteLine("Inserted");
        }

        /// <summary>
        /// 返回指定表中的所有记录
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static async Task FindAll(IMongoCollection<PersonModel> collection)
        {
            //该正则表达式会返回表中的所有结果
            var people = await collection.FindAsync(_=>true);
            List<PersonModel> personList=people.ToList();
            foreach(var person in personList)
            {
                Console.WriteLine($"Id={person.Id}, FirstName={person.FirstName}, LastName={person.LastName}");
            }
        }
    }
}