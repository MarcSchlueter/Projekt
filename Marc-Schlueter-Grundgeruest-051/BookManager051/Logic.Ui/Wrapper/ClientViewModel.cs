using De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects;
using De.HsFlensburg.ClientApp051.Logic.Ui.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace De.HsFlensburg.ClientApp051.Logic.Ui.Wrapper
{
    public class ClientViewModel: ViewModelBase<Client>
    {
        public int Id
        {
            get
            {
                return Model.Id;
            }

            set
            {
                Model.Id = value;
            }
        }

        public string Name
        {
            get
            {
                return Model.Name;
            }

            set
            {
                Model.Name = value;
            }
        }

        public override void NewModelAssigned()
        {
            throw new NotImplementedException();
        }


    }
}
