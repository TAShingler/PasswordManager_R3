using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Models;
internal class Database_Model : System.ComponentModel.INotifyPropertyChanged {
    //idefk
    #region Fields
    private Dictionary<int, Models.Group> _groupsData = new Dictionary<int, Models.Group>();
    private Dictionary<int, Models.Record> _recordsData = new Dictionary<int, Models.Record>();

    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion Fields

    #region Properties
    internal Dictionary<int, Models.Group> DbGroups {
        get { return _groupsData; }
        set {
            _groupsData = value;
            OnPropertyChanged(nameof(DbGroups));
        }    //might not need set method
    }
    internal Dictionary<int, Models.Record> DbRecords {
        get { return _recordsData; }
        set {
            _recordsData = value;
            OnPropertyChanged(nameof(DbRecords));
        }   //might not need set method
    }
    #endregion Properties

    #region Constructors
    internal Database_Model() {
        //if (!Models.AppVariables.DatabaseConnection.Equals(null)) { return; }

        //_groupsData = Models.AppVariables.DatabaseConnection.RetrieveGroupsData();
        //_recordsData = Models.AppVariables.DatabaseConnection.RetrieveRecordsData();
    }
    #endregion Constructors

    #region Other Methods
    private void OnPropertyChanged(string propertyName) {
        if (PropertyChanged != null) {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    #endregion Other Methods
}
