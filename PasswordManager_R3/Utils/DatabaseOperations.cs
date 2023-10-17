using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Utils;
internal class DatabaseOperations {
    #region Fields
    //maybe add File extension string?
    private readonly string _databaseName = "pass_data";    //will probably change default value
    private const string GROUPS_TABLE_NAME = "App_Groups";  //will probably change default value and rename variable
    private const string RECORDS_TABLE_NAME = "App_Records";    //will probably change default value and rename variable
    private System.Data.SQLite.SQLiteConnection _dbConnection; //might not be using SQLite; might use LiteDB instead
    //private System.Data.SQLite.SQLiteCommand _sqlCommand;      //might not be using SQLite; might use LiteDB instead
    //private System.Data.SQLite.SQLiteDataReader _sqlReader;    //might not be using SQLite; might use LiteDB instead
    private readonly string _dbFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\PMR3\\Data\\";    //will probably allow user to set this in app settings; will set this value in constructor if that is the case
    private readonly string _dbFilePath;    //might remove -- seems unnecessary
    private readonly Newtonsoft.Json.JsonSerializerSettings _serializerSettings = new() {   //might change how this is handled -- change from global, set values using app settings (?), might make a property for
        PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All,
        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize,
        NullValueHandling = Newtonsoft.Json.NullValueHandling.Include,
        ObjectCreationHandling = Newtonsoft.Json.ObjectCreationHandling.Replace
    };
    #endregion Fields

    #region Properties
    internal string DatabaseName {
        get { return _databaseName; }
    }
    internal string DatabaseFolderPath {
        get { return _dbFolderPath; }
    }
    internal string DatabaseFilePath {
        get { return _dbFilePath; }
    }
    internal System.Data.SQLite.SQLiteConnection DatabaseConnection {
        get { return _dbConnection; }
        set { _dbConnection = value; }
    }
    //internal System.Data.SQLite.SQLiteCommand SqlCommand {
    //    get { return _sqlCommand; }
    //    set { _sqlCommand = value; }
    //}
    //internal System.Data.SQLite.SQLiteDataReader SqlDataReader {
    //    get { return _sqlReader; }
    //    set { _sqlReader = value; }
    //}
    #endregion Properties

    #region Constructors
    internal DatabaseOperations() {
        _dbFilePath = _dbFolderPath + _databaseName;
        System.Diagnostics.Debug.WriteLine("DB path: " + _dbFilePath);
        //CreateConnection();
    }
    #endregion Constructors

    #region Other Methods
    /*  MIGHT CREATE EXECUTE NON QUERY AND EXECUTE SCALAR METHODS  */
    private void CreateConnection() {
        _dbConnection = new System.Data.SQLite.SQLiteConnection($"Data Source={_dbFilePath}.db;Version=3;");
        //throw new NotImplementedException("Not implemented yet...");
    }
    private void OpenConnection() {
        try {
            _dbConnection.Open();
        } catch (Exception ex) {
            //display error message
        }
        //throw new NotImplementedException("Not implemented yet...");
    }
    private void CloseConnection() {
        try {
            _dbConnection.Close();
        } catch (Exception ex) {
            //display error message
        }
        //throw new NotImplementedException("Not implemented yet...");
    }
    internal void CreateTables() {
        //declare SqlCommand
        System.Data.SQLite.SQLiteCommand sqlCommand;

        OpenConnection();

        sqlCommand = _dbConnection.CreateCommand();
        
        //create table for Group objects
        sqlCommand.CommandText = $"CREATE TABLE {GROUPS_TABLE_NAME} (RowID INTEGER UNIQUE, Data TEXT, PRIMARY KEY(RowID AUTOINCREMENT));";
        sqlCommand.ExecuteNonQuery();

        sqlCommand.Reset();

        //create table for Record objects
        sqlCommand.CommandText = $"CREATE TABLE {RECORDS_TABLE_NAME} (RowID INTEGER UNIQUE, Data TEXT, PRIMARY KEY(RowID AUTOINCREMENT));";
        sqlCommand.ExecuteNonQuery();

        sqlCommand.Dispose();

        CloseConnection();
    }
    internal void CheckForTables() {    //might make private and call at beginning of CreateTables method
        //really confused as to why I initially wrote this
        System.Data.SQLite.SQLiteCommand sqlCommand;

        OpenConnection();

        //groups table
        sqlCommand = _dbConnection.CreateCommand();
        sqlCommand.CommandText = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{GROUPS_TABLE_NAME}';";
        var result = sqlCommand.ExecuteScalar();

        if (result == null) {
            sqlCommand.Reset();
            sqlCommand.CommandText = $"CREATE TABLE {GROUPS_TABLE_NAME} (RowID INTEGER UNIQUE, Data TEXT, PRIMARY KEY(RowID AUTOINCREMENT));";
            sqlCommand.ExecuteNonQuery();
        }

        //records table
        sqlCommand.Reset();
        sqlCommand.CommandText = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{RECORDS_TABLE_NAME}';";
        result = sqlCommand.ExecuteScalar();

        if (result == null) {
            sqlCommand.Reset();
            sqlCommand.CommandText = $"CREATE TABLE {RECORDS_TABLE_NAME} (RowID INTEGER UNIQUE, Data TEXT, PRIMARY KEY(RowID AUTOINCREMENT));";
            sqlCommand.ExecuteNonQuery();
        }

        CloseConnection();
    }

    //Create operations -- maybe split into two separate methods?
    internal void InsertData(object obj) {
        System.Data.SQLite.SQLiteCommand sqlCommand;

        //serialize object passed to method to JSON string
        string serializedObj = Newtonsoft.Json.JsonConvert.SerializeObject(obj, _serializerSettings);   //may need try block

        //encrypt serialized object string
        string encryptedObj = Utils.EncryptionTools.EncryptObjectStringToBase64String(serializedObj);   //may need try block

        //write encrypted serialized object string to DB
        OpenConnection();

        sqlCommand = _dbConnection.CreateCommand();

        //is obj Group or Record
        if (obj is Models.Group) {
            sqlCommand.CommandText = $"INSERT INTO {GROUPS_TABLE_NAME} (Data) VALUES (@encryptedObj);";
            //sqlCommand.CommandText = $"INSERT INTO {GROUPS_TABLE_NAME} (Data) VALUES ({encryptedObj});";
        }

        if (obj is Models.Record) {
            sqlCommand.CommandText = $"INSERT INTO {RECORDS_TABLE_NAME} (Data) VALUES (@encryptedObj);";
            //sqlCommand.CommandText = $"INSERT INTO {RECORDS_TABLE_NAME} (Data) VALUES ({encryptedObj});";
        }

        try {
            sqlCommand.Parameters.Add("@encryptedObj", System.Data.DbType.String, encryptedObj.Length).Value = encryptedObj;
            sqlCommand.ExecuteNonQuery();
        } catch(Exception ex) {
            System.Windows.MessageBox.Show(
                $"Failed to write object of type {obj.GetType().FullName} to database.\nAborting Insert operation...",
                $"{ex.GetType().Name} Exception",
                System.Windows.MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Error);
            return;
        }

        sqlCommand.Dispose();
        CloseConnection();
    }

    //Read Operations
    internal Dictionary<int, Models.Group> RetrieveGroupsData() {
        Dictionary<int, Models.Group> groups = new Dictionary<int, Models.Group>();
        System.Data.SQLite.SQLiteCommand sqlCommand;
        System.Data.SQLite.SQLiteDataReader sqlReader;

        OpenConnection();
        sqlCommand = _dbConnection.CreateCommand();
        sqlCommand.CommandText = $"SELECT * FROM {GROUPS_TABLE_NAME}";

        sqlReader = sqlCommand.ExecuteReader();
        while(sqlReader.Read()) {
            //read int from DB
            int sqlReaderInt = sqlReader.GetInt32(0);

            //read string from DB
            string sqlReaderString = sqlReader.GetString(1);

            string decryptedSqlReaderString = Utils.EncryptionTools.DecryptBase64StringToObjectString(sqlReaderString);

            var decryptedObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Group>(decryptedSqlReaderString);

            groups.Add(sqlReaderInt, decryptedObj);
        }

        CloseConnection();

        return groups;
    }
    internal Dictionary<int, Models.Record> RetrieveRecordsData() {
        Dictionary<int, Models.Record> records = new Dictionary<int, Models.Record>();
        System.Data.SQLite.SQLiteCommand sqlCommand;
        System.Data.SQLite.SQLiteDataReader sqlReader;

        OpenConnection();
        sqlCommand = _dbConnection.CreateCommand();
        sqlCommand.CommandText = $"SELECT * FROM {RECORDS_TABLE_NAME}";

        sqlReader = sqlCommand.ExecuteReader();
        while (sqlReader.Read()) {
            //get int from DB
            int sqlReaderInt = sqlReader.GetInt32(0);

            //get string from DB
            string sqlReaderString = sqlReader.GetString(1);

            string decryptedSqlReaderString = Utils.EncryptionTools.DecryptBase64StringToObjectString(sqlReaderString);

            var decryptedObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Record>(decryptedSqlReaderString);

            records.Add(sqlReaderInt, decryptedObj);
        }

        CloseConnection();

        return records;
    }

    //Update operations
    internal void UpdateData(int rowId, object updatedObj) {
        System.Data.SQLite.SQLiteCommand sqlCommand;

        string serializedObj = Newtonsoft.Json.JsonConvert.SerializeObject(updatedObj, _serializerSettings);

        string encryptedObj = Utils.EncryptionTools.EncryptObjectStringToBase64String(serializedObj);

        OpenConnection();

        sqlCommand = _dbConnection.CreateCommand();
        
        if (updatedObj is Models.Group) {
            sqlCommand.CommandText = $"UPDATE {GROUPS_TABLE_NAME} SET Data = '{encryptedObj}' WHERE RowID = {rowId};";
        } else {
            sqlCommand.CommandText = $"UPDATE {RECORDS_TABLE_NAME} SET Data = '{encryptedObj}' WHERE RowID = {rowId};";
        }

        sqlCommand.ExecuteNonQuery();

        sqlCommand.Dispose();
        CloseConnection();
    }
    internal void UpdateGroupExpandedState(int rowId, bool expandedState) {
        System.Data.SQLite.SQLiteCommand sqlCommand;

        OpenConnection();

        sqlCommand = _dbConnection.CreateCommand();
        sqlCommand.CommandText = $"UPDATE {GROUPS_TABLE_NAME} SET IsExpanded = '{expandedState}' WHERE RowID = {rowId};";
        sqlCommand.ExecuteScalar();

        sqlCommand.Dispose();
        CloseConnection();
    }

    //Delete operations -- might combine into one method
    internal void DeleteGroupData(int rowId) {
        System.Data.SQLite.SQLiteCommand sqlCommand;

        OpenConnection();
        sqlCommand = _dbConnection.CreateCommand();

        sqlCommand.CommandText = $"DELETE FROM {GROUPS_TABLE_NAME} WHERE RowID = {rowId};";

        sqlCommand.ExecuteScalar();

        sqlCommand.Dispose();
        CloseConnection();
    }
    internal void DeleteRecordData(int rowId) {
        System.Data.SQLite.SQLiteCommand sqlCommand;

        OpenConnection();
        sqlCommand = _dbConnection.CreateCommand();

        sqlCommand.CommandText = $"DELETE FROM {RECORDS_TABLE_NAME} WHERE RowID = {rowId};";

        sqlCommand.ExecuteScalar();

        sqlCommand.Dispose();
        CloseConnection();
    }
    //generic delete operation method
    //internal void DeleteData(int rowId, string objType) { //maybe make DeleteData<T> and use the type passed to the method (?)
    //    System.Data.SQLite.SQLiteCommand sqlCommand;

    //    OpenConnection();

    //    sqlCommand = _dbConnection.CreateCommand();

    //    switch (objType) {
    //        case "Group":
    //            sqlCommand.CommandText = $"DELETE FROM {GROUPS_TABLE_NAME} WHERE RowID = {rowId};";
    //            break;
    //        case "Record":
    //            sqlCommand.CommandText = $"DELETE FROM {RECORDS_TABLE_NAME} WHERE RowID = {rowId};";
    //            break;
    //        default:
    //            break;
    //    }

    //    sqlCommand.ExecuteScalar();
        
    //    sqlCommand.Dispose();
    //    CloseConnection();
    //}
    #endregion Other Methods
}