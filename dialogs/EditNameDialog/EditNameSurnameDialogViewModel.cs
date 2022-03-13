using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using University_Lab2.dialogs.DialogService;
using University_Lab2.dialogs.DialogYesNo;

namespace University_Lab2.dialogs.EditNameDialog
{
    internal class EditNameSurnameDialogViewModel : DialogYesNoViewModel
    {
        private string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }

        private string surname;
        public string Surname { get { return surname; } set { surname = value; OnPropertyChanged("Surname"); } }

        public EditNameSurnameDialogViewModel(string message, string name, string surname) : base(message)
        {
            Name = name;
            Surname = surname;
        }
        private MyCommand productyesCommand;
        public MyCommand ProductYesCommand
        {
            get
            {
                return productyesCommand ??
                    (productyesCommand = new MyCommand(obj =>
                    {
                        this.CloseDialogWithResult(obj as Window, DialogResult.Yes);
                    },
                    (obj) =>
                    {
                        string st = null;
                        if (Name != null) { st = Name.Replace(" ", ""); }
                        string st2 = null;
                        if (Surname != null) { st2 = Surname.Replace(" ", ""); }
                        return st != null && st.Length > 0 && st2 != null && st2.Length > 0;
                    }));
            }
        }
    }
}
