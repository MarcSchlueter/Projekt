using De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects;
using De.HsFlensburg.ClientApp051.Logic.Ui.Base;
using System;

namespace De.HsFlensburg.ClientApp051.Logic.Ui.Wrapper
{
    public class UserViewModel : ViewModelBase<User>
    {
        public string Id
        {
            get { return Model.Id; }
            set { Model.Id = value; }
        }

        public string Username
        {
            get { return Model.Username; }
            set { Model.Username = value; }
        }

        public string DisplayName
        {
            get { return Model.DisplayName; }
            set { Model.DisplayName = value; }
        }

        public string Email
        {
            get { return Model.Email; }
            set { Model.Email = value; }
        }

        public string Password
        {
            get { return Model.Password; }
            set { Model.Password = value; }
        }

        public override void NewModelAssigned()
        {
        }
    }
}