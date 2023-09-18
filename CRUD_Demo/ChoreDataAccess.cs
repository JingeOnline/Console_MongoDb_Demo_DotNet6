using CRUD_Demo.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Demo
{
    internal class ChoreDataAccess
    {
        //数据库连接字符串
        private const string connectString = "mongodb://127.0.0.1:27017";
        //数据库名称
        private const string databaseName = "dotnet_test_db2";
        //表（collection）名
        private const string collectionStudent = "students";
        private const string collectionChore = "chores";
        private const string collectionChoreHistory = "chores_history";

        //这里的关键字in，限制传入参数的引用，但设置为只读模式。通过关键字in可以提升性能。
        private IMongoCollection<T> ConnectTomongo<T>(in string collection)
        {
            var client=new MongoClient(connectString);
            var db = client.GetDatabase(databaseName);
            return db.GetCollection<T>(collection);
        }

        /// <summary>
        /// 获取所有学生列表
        /// </summary>
        /// <returns></returns>
        internal async Task<List<StudentModel>> GetAllStudents()
        {
            var studentCollection = ConnectTomongo<StudentModel>(collectionStudent);
            var results = await studentCollection.FindAsync(_=>true);
            return results.ToList();    
        }

        /// <summary>
        /// 根据Id获取一个学生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal async Task<StudentModel> GetStudentById(string id)
        {
            var studentCollection = ConnectTomongo<StudentModel>(collectionStudent);
            var result = await studentCollection.FindAsync( s=> s.Id==id);
            return result.FirstOrDefault();
            //var studentList=result.ToList();
            //if(studentList.Count>0)
            //{
            //    return studentList[0];
            //}
            //else
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// 获取所有值日活动列表
        /// </summary>
        /// <returns></returns>
        internal async Task<List<ChoreModel>> GetAllChores()
        {
            var choreCollection = ConnectTomongo<ChoreModel>(collectionChore);
            var results=await choreCollection.FindAsync(_=>true); 
            return results.ToList();
        }

        internal async Task<ChoreModel> GetChoreById(string id)
        {
            var choreCollection = ConnectTomongo<ChoreModel>(collectionChore);
            var result=await choreCollection.FindAsync(c=>c.Id==id);

            return result.FirstOrDefault();
            //var choreList=result.ToList();
            //if(choreList.Count>0)
            //{
            //    return choreList[0];
            //}
            //else
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// 获取某个学生的值日活动列表
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        internal async Task<List<ChoreModel>> GetAllChoresByStudent(StudentModel student)
        {
            var choreCollection = ConnectTomongo<ChoreModel>(collectionChore);
            var results = await choreCollection.FindAsync(c => c.AssignedTo.Id==student.Id);
            return results.ToList();
        }

        /// <summary>
        /// 添加一个学生
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        internal async Task CreateStudent(StudentModel student)
        {
            var studentCollection = ConnectTomongo<StudentModel>(collectionStudent);
            await studentCollection.InsertOneAsync(student);
        }

        /// <summary>
        /// 更新学生信息，这里使用了Update而不是Replace。
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        internal async Task UpdateStudent(StudentModel student)
        {
            var studentCollection = ConnectTomongo<StudentModel>(collectionStudent);
            //Eq表示相等，Gt表示大于，Lt表示小于
            var filterDefinition = Builders<StudentModel>.Filter.Eq(x => x.Id, student.Id);
            var updateDefinition = Builders<StudentModel>.Update
                .Set(x => x.FirstName, student.FirstName)  //set方法，第一个参数是要更新的字段，第二个参数是要更新的值
                .Set(x=>x.LastName,student.LastName);
            //没有添加options，则如果更新对象不存在，则不进行任何操作。
            await studentCollection.UpdateOneAsync(filterDefinition, updateDefinition);
        }

        /// <summary>
        /// 添加一个值日活动
        /// </summary>
        /// <param name="chore"></param>
        /// <returns></returns>
        internal async Task CreateChore(ChoreModel chore)
        {
            var choresCollection = ConnectTomongo<ChoreModel>(collectionChore);
            await choresCollection.InsertOneAsync(chore);
        }

        /// <summary>
        /// 更新值日活动。
        /// MongoDb的更新与关系型数据库更新不一样，即使只更新一条数据中的某个属性，也会把整条记录更新一遍。（效率是很高的）
        /// </summary>
        /// <param name="chore"></param>
        /// <returns></returns>
        internal async Task UpdateChore(ChoreModel chore)
        {
            var choresCollection=ConnectTomongo<ChoreModel>(collectionChore);
            
            //独立定义一个FilterDefinition对象。
            var filter = Builders<ChoreModel>.Filter.Eq("Id",chore.Id);
            //如果存在则更新，如果不存在则创建。
            ReplaceOptions options = new ReplaceOptions() {IsUpsert=true };
            await choresCollection.ReplaceOneAsync(filter, chore, options);

            //另一种直接使用lamda表达式的方法定义filter，更常用。与上面的语句是等效的。
            //await choresCollection.ReplaceOneAsync(c=>c.Id==chore.Id,chore,options);
        }

        /// <summary>
        /// 删除指定的值日活动
        /// </summary>
        /// <param name="chore"></param>
        /// <returns></returns>
        internal async Task DeleteChore(ChoreModel chore)
        {
            var choresCollection = ConnectTomongo<ChoreModel>(collectionChore);
            //DeleteOne方法只会删除匹配到的第一个对象，DeleteMany方法会删除匹配到的所有对象。
            await choresCollection.DeleteOneAsync(c=>c.Id==chore.Id);
        }

        internal async Task CreateChoreHistory(ChoreModel chore,string updateBy)
        {
            var historyCollection = ConnectTomongo<ChoreHistoryModel>(collectionChoreHistory);
            historyCollection.InsertOneAsync(new ChoreHistoryModel(chore,updateBy));
        }
    }
}
