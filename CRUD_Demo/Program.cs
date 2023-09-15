using CRUD_Demo.Models;

namespace CRUD_Demo
{
    internal class Program
    {
        static ChoreDataAccess db;

        static async Task Main(string[] args)
        {
            db = new ChoreDataAccess();

            //await createStudent(db);
            //Console.WriteLine((await db.GetStudentById("650422cc2d681e97f304ef41")).FullName);
            //await creatChore();
            await updateChore();

            Console.WriteLine("Done");
        }

        static async Task createStudent()
        {
            StudentModel student = new StudentModel()
            {
                FirstName="Nancy",
                LastName="Brownson"
            };
            await db.CreateStudent(student);
        }

        static async Task creatChore()
        {
            StudentModel student = await db.GetStudentById("650422cc2d681e97f304ef41");
            ChoreModel chore = new ChoreModel()
            {
                AssignedTo = student,
                ChoreText = "Clean the blackboard.",
                FrequencyInDays = 8,
                CompletedAt = new DateTime(2023,5,15)
            };
            await db.CreateChore(chore);
        }

        static async Task updateChore()
        {
            ChoreModel chore = await db.GetChoreById("650429f381444f160d4129bb");
            chore.CompletedAt = new DateTime(2023,11,11);
            await db.UpdateChore(chore);

            await db.CreateChoreHistory(chore, "Yar Bora");
        }
    }
}