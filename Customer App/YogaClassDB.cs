using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_App
{
    public class YogaClassDB
    {
        static SQLiteConnection databaseConnection;

        public const string CLASS_INSTANCE_TABLE = "ClassInstance";
		public const string DAY_OF_WEEK_COLUMN = "classDay";
        public const string TEACHER_NAME_COLUMN = "teacher";
		public const string CLASS_DATE_COLUMN = "date";
		public const string CLASS_TIME_COLUMN = "classTime";

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
				databaseConnection.CreateTable<ClassInstance>();
			}
			catch (SQLiteException ex)
			{
				CurrentState += ex.Message;
			}
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
				Debug.WriteLine(query);	

				return databaseConnection.Query<ClassInstance>(query, time);
			}
			catch (Exception ex)
			{
				CurrentState = $"Failed to retrieve data: {ex.Message}";
				return new List<ClassInstance>();
			}

		}
	}
}
