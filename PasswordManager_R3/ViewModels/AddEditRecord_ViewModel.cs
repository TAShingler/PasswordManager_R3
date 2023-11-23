using PasswordManager_R3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordManager_R3.ViewModels;
internal class AddEditRecord_ViewModel : ViewModelBase {
    #region Fields
    private readonly string _operationString = string.Empty;
    private readonly bool _isNewRecord = true;
    private readonly Models.Group _parentGroup;
    private const int MAX_PASSWORD_LENGTH = 128;    //may move fields, properties, and methods related to password generator Popup to their own ViewModel for the Popup -- maybe...
    private const int MIN_PASSWORD_LENGTH = 8;
    private const string LOWERCASE_LETTERS = "abcdefghijklmnopqrstuvwxyz";
    private const string NUMBERS = "0123456789";
    private const string SPECIAL_CHARACTERS = @"%-_@~! =[]&#+,."; //allowed in Windows OS, but not Linux/UNIX: $, apostrophe, quotation marks, parantheses -- not sure about accent (tilde button w/out holding shift)

    //record selected in DataGrid in Databse_View
    private readonly Models.Record? _selectedRecord = null;
    private readonly int _recordRowId;

    //fields for Properties View binds to
    private string _srTitle = string.Empty;
    private string _srUsername = string.Empty;
    private string _srEmail = string.Empty;
    private string _srPassword = string.Empty;
    private string _srUrl = string.Empty;
    private string _srTags = string.Empty;  //might get rid of tags...
    private bool _srHasExpirationDate = false;
    private string _srExpirationDate = string.Empty;
    private bool _srHasNotes = false;
    private string _srNotes = string.Empty;
    private readonly string _srCreatedDate = string.Empty;
    private readonly string _srModifiedDate = string.Empty;
    private readonly string _srGuid = string.Empty;

    //fields for Properties Popup in View binds to
    private int _pgLength = 12;
    private bool _pgIncludesUppercaseLetters = false;
    private bool _pgIncludesLowercaseLetters = false;
    private bool _pgIncludesNumbers = false;
    private bool _pgIncludesSpecialCharacters = false;

    //calendar UIElement fields
    private bool _isCalendarDisplayed = false;
    #endregion Fields

    #region Properties
    public string OperationString {
        get => _operationString;
    }
    //might make Properties not directly reference Record obj...
    public string SrTitle {
        get { return _srTitle; }
        set {
            _srTitle = value;

            OnPropertyChanged(nameof(SrTitle));
        }
    }
    public string SrUsername {
        get { return _srUsername; }
        set {
            _srUsername = value;

            OnPropertyChanged(nameof(SrUsername));
        }
    }
    public string SrEmail {
        get { return _srEmail; }
        set {
            _srEmail = value;

            OnPropertyChanged(nameof(SrEmail));
        }
    }
    public string SrPassword {
        get { return _srPassword; }
        set {
            _srPassword = value;

            OnPropertyChanged(nameof(SrPassword));
        }
    }
    public string SrUrl {
        get {
            //if (_selectedRecord != null)
            //    return _selectedRecord.URL;

            return _srUrl;
        }
        set {
            _srUrl = value;
            //System.Diagnostics.Debug.WriteLine($"SrUrl value: {SrUrl}");
            OnPropertyChanged(nameof(SrUrl));
        }
    }
    public string SrTags {
        get { return _srTags; }
        set {
            _srTags = value;

            OnPropertyChanged(nameof(SrTags));
        }
    }   //might get rid of tags...
    public bool SrHasExpirationDate {
        get { return _srHasExpirationDate; }
        set {
            _srHasExpirationDate = value;

            OnPropertyChanged(nameof(SrHasExpirationDate));
        }
    }
    public string SrExpirationDate {
        get { return _srExpirationDate; }
        set {
            _srExpirationDate = value;

            OnPropertyChanged(nameof(SrExpirationDate));
        }
    }
    public bool SrHasNotes {
        get { return _srHasNotes; }
        set {
            _srHasNotes = value;
            OnPropertyChanged(nameof(SrHasNotes));
        }
    }
    public string SrNotes {
        get { return _srNotes; }
        set {
            _srNotes = value;

            OnPropertyChanged(nameof(SrNotes));
        }
    }
    public string SrCreatedDate {
        get { return _srCreatedDate; }
        //set {
        //    _srCreatedDate = value;

        //    OnPropertyChanged(nameof(SrCreatedDate));
        //}
    }
    public string SrModifiedDate {
        get { return _srModifiedDate; }
        //set {
        //    _srModifiedDate = value;

        //    OnPropertyChanged(nameof(SrModifiedDate));
        //}
    }
    public string SrGuid {
        get { return _srGuid; }
        //set {
        //    _srGuid = value;

        //    OnPropertyChanged(nameof(SrGuid));
        //}
    }

    //properties for password generator Popup
    public int PgMinPasswordLength {
        get { return MIN_PASSWORD_LENGTH; }
    }
    public int PgMaxPasswordLength {
        get { return MAX_PASSWORD_LENGTH; }
    }
    public int PgLength {
        get { return _pgLength; }
        set {
            _pgLength = value;

            OnPropertyChanged(nameof(PgLength));
        }
    }
    public bool PgIncludesUppercaseLetters {
        get { return _pgIncludesUppercaseLetters; }
        set {
            _pgIncludesUppercaseLetters = value;

            OnPropertyChanged(nameof(PgIncludesUppercaseLetters));
        }
    }
    public bool PgIncludesLowercaseLetters {
        get { return _pgIncludesLowercaseLetters; }
        set {
            _pgIncludesLowercaseLetters = value;

            OnPropertyChanged(nameof(PgIncludesLowercaseLetters));
        }
    }
    public bool PgIncludesNumbers {
        get {return _pgIncludesNumbers; }
        set {
            _pgIncludesNumbers = value;

            OnPropertyChanged(nameof(PgIncludesNumbers));
        }
    }
    public bool PgIncludesSpecialCharacters {
        get { return _pgIncludesSpecialCharacters; }
        set {
            _pgIncludesSpecialCharacters = value;

            OnPropertyChanged(nameof(PgIncludesSpecialCharacters));
        }
    }

    //calendar UIElement properties
    public bool IsCalendarDisplayed {
        get { return _isCalendarDisplayed; }
        set {
            _isCalendarDisplayed = value;

            OnPropertyChanged(nameof(IsCalendarDisplayed));
        }
    }

    //DelegateCommands
    public Utils.DelegateCommand OkButtonCommand { get; set; }
    public Utils.DelegateCommand CancelButtonCommand { get; set; }
    public Utils.DelegateCommand GeneratePasswordCommand { get; set; }
    public Utils.DelegateCommand IncrementPasswordLengthCommand { get; set; }
    public Utils.DelegateCommand DecrementPasswordLengthCommand { get; set; }
    public Utils.DelegateCommand CalendarDateSelectionChangedCommand { get; set; }
    public Utils.DelegateCommand ComboBoxExpirationDatePresetsSelectionChangedCommand { get; set; }
    #endregion Properties

    //Delegates to bubble events to MainWindow_ViewModel
    public delegate void CreateRecordEventHandler();// object sender, EventArgs e); - might re-add sender and EventArgs...
    public delegate void UpdateRecordEventHandler();// object sender, EventArgs e);
    public delegate void CancelAddEditRecordHandler();//(object sender, EventArgs e);

    //Events to bubble to MainWindow_ViewModel
    public event CreateRecordEventHandler? CreateRecord; //prob. rename...
    public event UpdateRecordEventHandler? UpdateRecord;
    public event CancelAddEditRecordHandler? CancelAddEditRecord;

    #region Constructors
    //pass selectedGroup as well? RoutedEventArgs for slectedGroup and Record objects?
    public AddEditRecord_ViewModel(ViewModelBase parentVM, Models.Record? selectedRecord = null) : base(parentVM) { //eventually get rid of...
        if (selectedRecord != null) {
            _isNewRecord = false;
            _selectedRecord = selectedRecord;

            SrTitle = selectedRecord.Title;
            SrUsername = selectedRecord.Username;
            SrEmail = selectedRecord.Email;
            SrPassword = selectedRecord.Password;
            SrUrl = selectedRecord.URL;
            SrTags = selectedRecord.Tags;
            SrHasExpirationDate = selectedRecord.HasExpirationDate;
            SrExpirationDate = selectedRecord != null ? selectedRecord.ExpirationDate.ToString() : string.Empty;
            SrHasNotes = selectedRecord.HasNotes;
            SrNotes = selectedRecord.Notes;
            _srCreatedDate = selectedRecord.CreatedDate.ToString();
            _srModifiedDate = selectedRecord.ModifiedDate.ToString();
            _srGuid = selectedRecord.GUID;
        }

        _operationString = _isNewRecord == true ? "Group Name \u2022 Add Record" : "Group Name \u2022 Edit Record";
        SetDelegateCommands();
    }
    public AddEditRecord_ViewModel(ViewModelBase parentVM, Models.Group parentGroup, Models.Record? selectedRecord = null, int rowId = -1) : base(parentVM) {
        if (selectedRecord != null) {
            _isNewRecord = false;
            _selectedRecord = selectedRecord;

            SrTitle = selectedRecord.Title;
            SrUsername = selectedRecord.Username;
            SrEmail = selectedRecord.Email;
            SrPassword = selectedRecord.Password;
            SrUrl = selectedRecord.URL;
            SrTags = selectedRecord.Tags;
            SrHasExpirationDate = selectedRecord.HasExpirationDate;
            SrExpirationDate = selectedRecord != null ? selectedRecord.ExpirationDate.ToString() : string.Empty;
            SrHasNotes = selectedRecord.HasNotes;
            SrNotes = selectedRecord.Notes;
            _srCreatedDate = selectedRecord.CreatedDate.ToString();     //may remove from View eventually...
            _srModifiedDate = selectedRecord.ModifiedDate.ToString();   //may remove from View eventually...
            _srGuid = selectedRecord.GUID;                              //may remove from View eventually...
        }

        _recordRowId = rowId;
        _parentGroup = parentGroup;
        _operationString = _isNewRecord == true ? (parentGroup.Title + " \u2022 Add Record") : (_selectedRecord?.Title + " \u2022 Edit Record");
        SetDelegateCommands();

        /* FOR TESTING */

        ////get Icon resources from App.xaml
        //var appResourceDictionaries = App.Current.Resources.MergedDictionaries;//.Where(rd => rd.Source.AbsoluteUri == "ms-resource:///Files/ResourceDictionaries/RecordIcons.xaml").FirstOrDefault(); //App.Current.Resources.MergedDictionaries.Cast<ResourceDictionary>().ToList();
        ////System.Diagnostics.Debug.WriteLine($"appResourcesDictionary is null : {(appResourceDictionaries == null ? true : false)}");
        //System.Diagnostics.Debug.WriteLine("appResourceDictionaries:");
        //foreach (var resource in appResourceDictionaries) {
        //    System.Diagnostics.Debug.WriteLine($"resource.Source.IsAbsoluteUri = {resource.Source.IsAbsoluteUri}");
        //}

        /* FOR TESTING */
    }
    #endregion Constructors

    private void SetDelegateCommands() {
        OkButtonCommand = new Utils.DelegateCommand(OnOkButtonCommand);
        CancelButtonCommand = new Utils.DelegateCommand(OnCancelButtonCommand);

        //password generator Popup
        GeneratePasswordCommand = new Utils.DelegateCommand(OnGeneratePasswordCommand);
        IncrementPasswordLengthCommand = new(OnIncrementPasswordLengthCommand);
        DecrementPasswordLengthCommand = new(OnDecrementPasswordLengthCommand);

        CalendarDateSelectionChangedCommand = new(OnCalendarDateSelectionChangedCommand);

        ComboBoxExpirationDatePresetsSelectionChangedCommand = new(OnComboBoxExpirationDatePresetsSelectionChangedCommand);
    }

    //DelegateCommand event handlers
    private void OnOkButtonCommand(object obj) {
        //will need to create event args and bubble to Main Window?? -- will need to bubble regardless to change view
        //CreateRecord?.Invoke(this, EventArgs.Empty);
        DateTime? userExpirationDate;

        if (String.IsNullOrEmpty(SrExpirationDate) == false) {
            try {
                userExpirationDate = DateTime.Parse(SrExpirationDate);
            } catch (FormatException ex) {
                MessageBox.Show(
                    "Expriation Date entered is not formatted correctly.",
                    "Expriation Date Format Exception",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
        } else {
            userExpirationDate = null;
        }

        //System.Diagnostics.Debug.WriteLine(
        //    $"\n\nParentGUID = {_parentGroup?.GUID}" +
        //    $"\nParent Group = {_parentGroup?.Title}" +
        //    $"\nSrTitle = {SrTitle}" +
        //    $"\nSrUsername = {SrUsername}" +
        //    $"\nSrEmail = {SrEmail}" +
        //    $"\nSrPassword = {SrPassword}" +
        //    $"\nSrUrl = {SrUrl}" +
        //    $"\nSrTags = {SrTags}" +
        //    $"\nSrHasExpirationDate = {SrHasExpirationDate}" +
        //    $"\nSrExpirationDate = {SrExpirationDate}" +
        //    $"\nSrHasNotes = {SrHasNotes}" +
        //    $"\nSrNotes = {SrNotes}" +
        //    $"\nSrCreatedDate = {SrCreatedDate}" +
        //    $"\nSrModifiedDate = {SrModifiedDate}" +
        //    $"\nSrGuid = {SrGuid}\n\n");

        System.Diagnostics.Debug.WriteLine("SrPassword = " + SrPassword);
        System.Diagnostics.Debug.WriteLine("_srPassword = " + _srPassword);

        //check whether is new record
        if (_isNewRecord == true) { //true
            //create Models.Record obj and set values from corresponding UIElements for created obj
            Models.Record newRecord = new() {
                ParentGUID = _parentGroup.GUID,
                ParentGroup = _parentGroup,
                GUID = Guid.NewGuid().ToString(),   //will need method to loop through all objects in database to avoid duplicate GUIDs
                Title = SrTitle,
                Username = SrUsername,
                Email = SrEmail,
                Password = SrPassword,
                URL = SrUrl,
                Tags = SrTags,
                HasExpirationDate = SrHasExpirationDate,
                ExpirationDate = userExpirationDate,
                HasNotes = SrHasNotes,
                Notes = SrNotes
            };
            //System.Diagnostics.Debug.WriteLine("AddEditRecord_ViewModel _isNewRecord = true");

            //write obj to database
            ((App)App.Current).DatabaseOps?.InsertData(newRecord);

            //invoke CreateRecord event to change view
            CreateRecord?.Invoke();// this, EventArgs.Empty);
        } else {    //false
            //set values from corresponding UIElements for _selectedRecord
            _selectedRecord.Title = SrTitle;
            _selectedRecord.Username = SrUsername;
            _selectedRecord.Email = SrEmail;
            _selectedRecord.Password = SrPassword;
            _selectedRecord.URL = SrUrl;
            _selectedRecord.Tags = SrTags;
            _selectedRecord.HasExpirationDate = SrHasExpirationDate;
            _selectedRecord.ExpirationDate = userExpirationDate;
            _selectedRecord.HasNotes = SrHasNotes;
            _selectedRecord.Notes = SrNotes;
            _selectedRecord.ModifiedDate = DateTime.Now;
            System.Diagnostics.Debug.WriteLine("AddEditRecord_ViewModel SrUrl = " + SrUrl);

            //write updated obj to database
            ((App)App.Current).DatabaseOps?.UpdateData(_recordRowId, _selectedRecord);

            //invoke UpdateRecord event to change view
            UpdateRecord?.Invoke();// this, EventArgs.Empty);
        }
    }
    private void OnCancelButtonCommand(object obj) {
        CancelAddEditRecord?.Invoke();// this, EventArgs.Empty);
    }
    private void OnGeneratePasswordCommand(object obj) {
        //System.Diagnostics.Debug.WriteLine(
        //    $"\n\nPgLength = {PgLength}" +
        //    $"\nPgIncludesUppercaseLetters = {PgIncludesUppercaseLetters}" +
        //    $"\nPgIncludesLowercaseLetters = {PgIncludesLowercaseLetters}" +
        //    $"\nPgIncludesNumbers = {PgIncludesNumbers}" +
        //    $"\nPgIncludesSpecialCharacters = {PgIncludesSpecialCharacters}\n\n"
        //);
        if (PgIncludesUppercaseLetters == false &
            PgIncludesLowercaseLetters == false &
            PgIncludesNumbers == false &
            PgIncludesSpecialCharacters == false) {
            //MessageBox.Show();
            System.Diagnostics.Debug.WriteLine("No options selected to generate password...");
            return;
        }

        StringBuilder sb = new StringBuilder(); //used to create string of all possible characters in password, then used for generated password

        if (PgIncludesUppercaseLetters == true)
            sb.Append(LOWERCASE_LETTERS.ToUpper());

        if (PgIncludesLowercaseLetters == true)
            sb.Append(LOWERCASE_LETTERS.ToLower());

        if (PgIncludesNumbers == true)
            sb.Append(NUMBERS);

        if (PgIncludesSpecialCharacters == true)
            sb.Append(SPECIAL_CHARACTERS);

        string possibleCharacters = sb.ToString();
        Random rng = new Random();

        sb.Clear();

        for (int i=0; i<PgLength; i++) {
            sb.Append(possibleCharacters[rng.Next(possibleCharacters.Length)]);
        }

        //System.Diagnostics.Debug.WriteLine("Generated password: " + sb.ToString());
        SrPassword = sb.ToString();
    }
    private void OnIncrementPasswordLengthCommand(object obj) {
        if (PgLength < MAX_PASSWORD_LENGTH)
            PgLength++;
    }
    private void OnDecrementPasswordLengthCommand(object obj) {
        if (PgLength > MIN_PASSWORD_LENGTH)
            PgLength--;
    }
    private void OnCalendarDateSelectionChangedCommand(object obj) {
        if (obj == null)
            return;

        DateTime selectedDate = ((System.Windows.Controls.SelectedDatesCollection)obj).ElementAt(0);

        //System.Windows.Controls.SelectedDatesCollection objAsSelectedDatesCollection = (System.Windows.Controls.SelectedDatesCollection)obj;

        //foreach (var item in (System.Windows.Controls.SelectedDatesCollection)obj) {
        //    System.Diagnostics.Debug.WriteLine($"OnCalendarDateSelectionChangedCommand obj = {item.ToString()}");
        //    System.Diagnostics.Debug.WriteLine($"OnCalendarDateSelectionChangedCommand obj type = {item.GetType()}");
        //}

        System.Diagnostics.Debug.WriteLine($"OnCalendarDateSelectionChangedCommand selectedDate = {selectedDate.ToString()}");
        
        SrExpirationDate = selectedDate.ToString();
        IsCalendarDisplayed = false;
    }
    private void OnComboBoxExpirationDatePresetsSelectionChangedCommand(object obj) {
        if (obj == null)
            return;

        var objAsInt = (int)obj;

        switch (objAsInt) {
            case 0:
                SrExpirationDate = DateTime.Now.AddHours(12).ToString();
                break;
            case 1:
                SrExpirationDate = DateTime.Now.AddHours(24).ToString();
                break;
            case 3:
                SrExpirationDate = DateTime.Now.AddDays(7).ToString();
                break;
            case 4:
                SrExpirationDate = DateTime.Now.AddDays(14).ToString();
                break;
            case 5:
                SrExpirationDate = DateTime.Now.AddDays(21).ToString();
                break;
            case 7:
                SrExpirationDate = DateTime.Now.AddMonths(1).ToString();
                break;
            case 8:
                SrExpirationDate = DateTime.Now.AddMonths(3).ToString();
                break;
            case 9:
                SrExpirationDate = DateTime.Now.AddMonths(6).ToString();
                break;
            case 11:
                SrExpirationDate = DateTime.Now.AddYears(1).ToString();
                break;
            case 12:
                SrExpirationDate = DateTime.Now.AddYears(2).ToString();
                break;
            case 13:
                SrExpirationDate = DateTime.Now.AddYears(3).ToString();
                break;
        }

        SrHasExpirationDate = true;
        //SrExpirationDate = DateTime.Now.Add();
    }
}