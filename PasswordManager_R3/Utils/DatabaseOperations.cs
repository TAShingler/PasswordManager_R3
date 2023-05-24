using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Utils;
internal class DatabaseOperations {
    #region Fields
    private readonly string _databaseName = "pass_data";    //will probably change default value
    private const string GROUPS_TABLE_NAME = "App_Groups";  //will probably change default value and rename variable
    private const string RECORDS_TABLE_NAME = "App_Records";    //will probably change default value and rename variable
    //private SQLiteConnection _dbConnection; //might not be using SQLite; might use LiteDB instead
    //private SQliteCommand _sqlCommand;      //might not be using SQLite; might use LiteDB instead
    //private SQLiteDataReader _sqlReader;    //might not be using SQLite; might use LiteDB instead
    private readonly string _dbFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\PasswordManager_R3\\Data\\";
    private readonly string _dbFilePath;
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
    /* MAY BE SWITCHING TO LiteDB INSTEAD OF USING SQLite
    internal SQliteConnection DatabaseConnection {
        get { return _dbConnection; }
        set { _dbConnection = value; }
    }
    internal SQLiteCommand SqlCommand {
        get { return _sqlCommand; }
        set { _sqlCommand = value; }
    }
    internal SQLiteDataReader SqlDataReader {
        get { return _sqlReader; }
        set { _sqlReader = value; }
    }
    */
    #endregion Properties

    #region Constructors
    internal DatabaseOperations() {
        _dbFilePath = _dbFolderPath + _databaseName;
        CreateConnection();
    }
    #endregion Constructors

    #region Other Methods
    private void CreateConnection() {
        throw new NotImplementedException("Not implemented yet...");
        //_dbConnection = new SQLiteConnection($"Data Source={_dbFilePath}.db;Version=3;");
    }
    private void OpenConnection() {
        throw new NotImplementedException("Not implemented yet...");
        //try {
        //    _dbConnection.Open();
        //} catch (Exception ex) {
        //    //display error message
        //}
    }
    private void CloseConnection() {
        throw new NotImplementedException("Not implemented yet...");
        //try {
        //    _dbConnection.close();
        //} catch (Exception ex) {
        //    //display error message
        //}
    }
    internal void CreateTables() {
        OpenConnection();

        _sqlCommand = _dbConnection.CreateCommand();
        
        //create table for Group objects
        _sqlCommand.CommandText = $"CREATE TABLE {GROUPS_TABLE_NAME} (RowID INTEGER UNIQUE, Data TEXT, PRIMARY KEY(RowID AUTOINCREMENT));";
        _sqlCommand.ExecuteNonQuery();

        _sqlCommand.Reset();

        //create table for Record objects
        _sqlCommand.CommandText = $"CREATE TABLE {RECORDS_TABLE_NAME} (RowID INTEGER UNIQUE, Data TEXT, PRIMARY KEY(RowID AUTOINCREMENT));";
        _sqlCommand.ExecuteNonQuery();

        _sqlCommand.Dispose();

        CloseConnection();
    }
    internal void CheckForTables() {    //might make private and call at beginning of CreateTables method
        //really confused as to why I initially wrote this
    }
    //Create operation
    internal void InsertData() {

    }
    //Read operation
    internal void UpdateData() {

    }
    #endregion Other Methods
}