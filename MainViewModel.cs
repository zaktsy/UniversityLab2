using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using University_Lab2.dialogs.DialogService;
using University_Lab2.dialogs.DialogYesNo;
using University_Lab2.dialogs.EditNameDialog;

namespace University_Lab2
{
    internal class MainViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<Faculty> nodes;
        public ObservableCollection<Faculty> Nodes { get { return nodes; } set { nodes = value; OnPropertyChanged("Nodes"); } }

        private Object selectedItem = null;
        public Object SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }


        private universitydbContext db;

        private string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }

        public MainViewModel()
        {
            Name = "fdfdf";
            db = new universitydbContext();
            var nods = db.Faculties.Include(fac => fac.Groups).ThenInclude(group => group.Students).ToList();
            Nodes = new ObservableCollection<Faculty>(nods);
        }

        private MyCommand newCommand;
        public MyCommand NewCommand
        {
            get
            {
                return newCommand ??
                    (newCommand = new MyCommand(obj =>
                    {
                        int level = 0;
                        if(SelectedItem.GetType() == typeof(Faculty)) { level=1; }
                        if(SelectedItem.GetType() == typeof(Group)) { level=2; }
                        if(SelectedItem.GetType() == typeof(Student)) { level=3; }
                        switch (level)
                        {
                            case 1:
                                EditNameDialogViewModel vm = new EditNameDialogViewModel("Название новой группы:", "");
                                DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                                if (result == DialogResult.Yes)
                                {
                                    Faculty fac = (Faculty)SelectedItem;
                                    Group gr = new Group();
                                    gr.Name = vm.Name;
                                    gr.Facultyid = fac.Id;
                                    fac.Groups.Add(gr);
                                    db.Groups.Add(gr);
                                    db.SaveChanges();
                                    var nods = db.Faculties.Include(fac => fac.Groups).ThenInclude(group => group.Students).ToList();
                                    Nodes = new ObservableCollection<Faculty>(nods);
                                }
                                break;
                            case 2:
                                EditNameSurnameDialogViewModel vm2 = new EditNameSurnameDialogViewModel("Имя и фамилия студента:", "","");
                                DialogResult result2 = DialogService.OpenDialog(vm2, obj as Window);
                                if (result2 == DialogResult.Yes)
                                {
                                    Group gr = (Group)SelectedItem;
                                    Student st = new Student();
                                    st.Name = vm2.Name;
                                    st.Surname = vm2.Surname;
                                    st.GroupId = gr.Id;
                                    gr.Students.Add(st);
                                    db.Students.Add(st);
                                    db.SaveChanges();
                                    var nods1 = db.Faculties.Include(fac => fac.Groups).ThenInclude(group => group.Students).ToList();
                                    Nodes = new ObservableCollection<Faculty>(nods1);
                                }
                                break;
                        }
                    },
                    (obj) => SelectedItem != null && SelectedItem.GetType() != typeof(Student)));
            }
        }

        private MyCommand deleteCommand;
        public MyCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                    (deleteCommand = new MyCommand(obj =>
                    {
                        int level = 0;
                        if (SelectedItem.GetType() == typeof(Faculty)) { level = 1; }
                        if (SelectedItem.GetType() == typeof(Group)) { level = 2; }
                        if (SelectedItem.GetType() == typeof(Student)) { level = 3; }
                        switch (level)
                        {
                            case 1:
                                DialogYesNoViewModel vm = new DialogYesNoViewModel("Удалить факультет?");
                                DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                                if (result == DialogResult.Yes)
                                {
                                    db.Faculties.Remove((Faculty)SelectedItem);
                                    db.SaveChanges();
                                    var nods = db.Faculties.Include(fac => fac.Groups).ThenInclude(group => group.Students).ToList();
                                    Nodes = new ObservableCollection<Faculty>(nods);
                                }
                                
                                break;
                            case 2:
                                DialogYesNoViewModel vm2 = new DialogYesNoViewModel("Удалить группу?");
                                DialogResult result2 = DialogService.OpenDialog(vm2, obj as Window);
                                if (result2 == DialogResult.Yes)
                                {
                                    db.Groups.Remove((Group)SelectedItem);
                                    db.SaveChanges();
                                    var nods2 = db.Faculties.Include(fac => fac.Groups).ThenInclude(group => group.Students).ToList();
                                    Nodes = new ObservableCollection<Faculty>(nods2);
                                }
                                
                                break;
                            case 3:
                                DialogYesNoViewModel vm3 = new DialogYesNoViewModel("Удалить студента?");
                                DialogResult result3 = DialogService.OpenDialog(vm3, obj as Window);
                                if (result3 == DialogResult.Yes)
                                {
                                    db.Students.Remove((Student)SelectedItem);
                                    db.SaveChanges();
                                    var nods3 = db.Faculties.Include(fac => fac.Groups).ThenInclude(group => group.Students).ToList();
                                    Nodes = new ObservableCollection<Faculty>(nods3);
                                }
                                
                                break;
                        }
                    },
                    (obj) => SelectedItem != null));
            }
        }

        private MyCommand editCommand;
        public MyCommand EditCommand
        {
            get
            {
                return editCommand ??
                    (editCommand = new MyCommand(obj =>
                    {
                        int level = 0;
                        if (SelectedItem.GetType() == typeof(Faculty)) { level = 1; }
                        if (SelectedItem.GetType() == typeof(Group)) { level = 2; }
                        if (SelectedItem.GetType() == typeof(Student)) { level = 3; }
                        switch (level)
                        {
                            case 1:
                                EditNameDialogViewModel vm = new EditNameDialogViewModel("Новое имя факультета",((Faculty)SelectedItem).Name);
                                DialogResult result = DialogService.OpenDialog(vm, obj as Window);
                                if (result == DialogResult.Yes)
                                {
                                    ((Faculty)SelectedItem).Name = vm.Name;
                                    db.SaveChanges();
                                    var nods = db.Faculties.Include(fac => fac.Groups).ThenInclude(group => group.Students).ToList();
                                    Nodes = new ObservableCollection<Faculty>(nods);
                                }
                                break;
                            case 2:
                                EditNameDialogViewModel vm2 = new EditNameDialogViewModel("Новое имя группы",((Group)SelectedItem).Name);
                                DialogResult result2 = DialogService.OpenDialog(vm2, obj as Window);
                                if (result2 == DialogResult.Yes)
                                {
                                    ((Group)SelectedItem).Name = vm2.Name;
                                    db.SaveChanges();
                                    var nods = db.Faculties.Include(fac => fac.Groups).ThenInclude(group => group.Students).ToList();
                                    Nodes = new ObservableCollection<Faculty>(nods);
                                }
                                
                                break;
                            case 3:
                                EditNameSurnameDialogViewModel vm3 = new EditNameSurnameDialogViewModel("Новое имя и фамилия студента",((Student)SelectedItem).Name, ((Student)SelectedItem).Surname);
                                DialogResult result3 = DialogService.OpenDialog(vm3, obj as Window);
                                if (result3 == DialogResult.Yes)
                                {
                                    ((Student)SelectedItem).Name = vm3.Name;
                                    db.SaveChanges();
                                    var nods = db.Faculties.Include(fac => fac.Groups).ThenInclude(group => group.Students).ToList();
                                    Nodes = new ObservableCollection<Faculty>(nods);
                                }
                                break;
                        }
                    },
                    (obj) => SelectedItem != null));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
