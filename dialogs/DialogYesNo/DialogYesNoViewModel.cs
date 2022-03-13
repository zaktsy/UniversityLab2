using System.Windows;

namespace University_Lab2.dialogs.DialogYesNo
{
    class DialogYesNoViewModel: DialogService.DialogViewModelBase
    {
        public DialogYesNoViewModel(string message) : base(message)
        {
        }
        private MyCommand yesCommand;
        public MyCommand YesCommand
        {
            get
            {
                return yesCommand ??
                    (yesCommand = new MyCommand(obj =>
                    {
                        this.CloseDialogWithResult(obj as Window, DialogService.DialogResult.Yes);
                    }
                    ));
            }
        }

        private MyCommand noCommand;
        public MyCommand NoCommand
        {
            get
            {
                return noCommand ??
                    (noCommand = new MyCommand(obj =>
                    {
                        this.CloseDialogWithResult(obj as Window, DialogService.DialogResult.No);
                    }
                    ));
            }
        }

    }
}
