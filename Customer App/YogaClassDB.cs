using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_App
{
    public class YogaClassDB
    {
        static SQLiteConnection databaseConnection;

        public const string CLASS_INSTANCE_TABLE = "ClassInstance";
		public const string DAY_OF_WEEK_COLUMN = "DayOfTheWeek";
        public const string TEACHER_NAME_COLUMN = "TeacherName";
		public const string CLASS_DATE_COLUMN = "ClassDate";
		public const string CLASS_TIME_COLUMN = "ClassTime";

		public const string DatabaseFilename = "YogaClassSQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

        public string CurrentState;

        public YogaClassDB() { 
            Init();
        }

        private void Init()
        {
            try
            {
                if (databaseConnection != null)
                {
                    CurrentState = "Database exists";
                    return;
                }

                databaseConnection = new SQLiteConnection(DatabasePath, Flags);

                databaseConnection.CreateTable<ClassInstance>();
				databaseConnection.CreateTable<User>();

				CurrentState = "Database created";
            }
            catch (SQLiteException ex)
            {
                CurrentState += ex.Message;
            }
        }
        
        public void ResetDatabase()
        {
            try
            {
                databaseConnection.DropTable<ClassInstance>();
				databaseConnection.DropTable<User>();
				databaseConnection.CreateTable<ClassInstance>();
				databaseConnection.CreateTable<User>();
			}
			catch (SQLiteException ex)
			{
				CurrentState += ex.Message;
			}
		}

		public void SeedDatabase()
		{
			// Check if the ClassInstance table is empty
			if (!databaseConnection.Table<ClassInstance>().Any())
			{
				var classInstances = new List<ClassInstance>
				{
					new() { ClassId = 1411, ClassDate = DateTime.Now.AddDays(1), TeacherName = "John Doe", DayOfTheWeek = "Monday", ClassTime = "10:00 AM" },
					new() { ClassId = 1412, ClassDate = DateTime.Now.AddDays(2), TeacherName = "Jane Smith", DayOfTheWeek = "Tuesday", ClassTime = "2:00 PM" },
					new() { ClassId = 1413, ClassDate = DateTime.Now.AddDays(3), TeacherName = "Emily Johnson", DayOfTheWeek = "Wednesday", ClassTime = "4:00 PM" },
					new() { ClassId = 1414, ClassDate = DateTime.Now.AddDays(4), TeacherName = "Michael Brown", DayOfTheWeek = "Thursday", ClassTime = "6:00 PM" },
					new() { ClassId = 1415, ClassDate = DateTime.Now.AddDays(5), TeacherName = "Sarah Davis", DayOfTheWeek = "Friday", ClassTime = "1:00 PM" },
					new() { ClassId = 1416, ClassDate = DateTime.Now.AddDays(6), TeacherName = "William Wilson", DayOfTheWeek = "Saturday", ClassTime = "3:00 PM" },
					new() { ClassId = 1417, ClassDate = DateTime.Now.AddDays(7), TeacherName = "William Wilson", DayOfTheWeek = "Saturday", ClassTime = "6:00 PM" },
					new() { ClassId = 1418, ClassDate = DateTime.Now.AddDays(8), TeacherName = "William Wilson", DayOfTheWeek = "Saturday", ClassTime = "8:00 PM" }
				};

				foreach (var classInstance in classInstances)
				{
					databaseConnection.Insert(classInstance);
				}
			}
		}

		public int SaveUser(User user)
		{
			var existingUser = databaseConnection.Table<User>().FirstOrDefault(u => u.Email == user.Email);
			if (existingUser != null)
			{
				return databaseConnection.Update(user);
			}
			else
			{
				return databaseConnection.Insert(user);
			}
		}

        public User GetUser(string email)
        {
			return databaseConnection.Table<User>().FirstOrDefault(u => u.Email == email);
        }

		public int SaveClassInstance(ClassInstance classInstance)
        {
            if (classInstance.Id > 0)
            {
                return databaseConnection.Update(classInstance);
            }
            else
            {
                return databaseConnection.Insert(classInstance);
            }
        }

		public List<ClassInstance> GetClassInstanceList()
        {
            try
            {
                return databaseConnection.Table<ClassInstance>().ToList();
            }
            catch(SQLiteException ex)
            {
                CurrentState = string.Format("Failed to retrieve data {0}", ex.Message);
            }

            return new List<ClassInstance>();
        }

		public List<ClassInstance> SearchClassInstanceByDayOfWeek(string dayOfWeek)
		{
			try
			{
				string query = $"SELECT * FROM {CLASS_INSTANCE_TABLE} WHERE {DAY_OF_WEEK_COLUMN} LIKE '%' || ? || '%';";

				return databaseConnection.Query<ClassInstance>(query, dayOfWeek);
			}
			catch (Exception ex)
			{
				CurrentState = $"Failed to retrieve data: {ex.Message}";
				return new List<ClassInstance>();
			}
		}


		public List<ClassInstance> SearchClassInstanceByTimeOfDay(string time)
		{
			try
			{
				string query = $"SELECT * FROM {CLASS_INSTANCE_TABLE} WHERE {CLASS_TIME_COLUMN} LIKE '%' || ? || '%';";

				return databaseConnection.Query<ClassInstance>(query, time);
			}
			catch (Exception ex)
			{
				CurrentState = $"Failed to retrieve data: {ex.Message}";
				return new List<ClassInstance>();
			}

		}

        public int DeleteClassInstance(ClassInstance classInstance)
        {
            try
            {
                return databaseConnection.Delete(classInstance);
            } 
            catch(Exception ex)
            {
				CurrentState = string.Format("Failed to retrieve data {0}", ex.Message);
			}
            return 0;
        }
	}
}
