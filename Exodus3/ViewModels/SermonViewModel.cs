using System;
using System.Threading.Tasks;
using Exodus3.Domain;

namespace Exodus3.ViewModels
{
    public class SermonViewModel : BaseViewModel
    {
        public Command LoadLatestSermonCommand { get; set; }
        public Sermon Sermon { get; set; }

        public SermonViewModel(Sermon sermon = null)
        {
            if (sermon != null)
            {
                Title = sermon.Name;
                Sermon = sermon;
            }
        }

        async Task ExecuteLoadLatestSermonCommand()
        {
            if (IsBusy)
                return;

            try
            {
             //   Sermon = await DataStore.

            }
            catch (Exception ex)
            {

            }

        }
    }
}
