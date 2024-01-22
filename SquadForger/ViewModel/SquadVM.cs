using CommunityToolkit.Mvvm.ComponentModel;

namespace SquadForger.ViewModel
{
    public class SquadVM : ObservableObject
    {
        public SquadVM()
        {
            Test();
        }

        private async void Test()
        {
            var t = await Model.Champions.GetFallBackAsync();
        }
    }
}