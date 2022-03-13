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
    internal class EditNameDialogViewModel : DialogYesNoViewModel
    {
        private string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }

        public EditNameDialogViewModel(string message, string name) : base(message)
        {
            this.name = name;
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
                        return st != null && st.Length > 0;
                    }));
            }
        }
    }
}
