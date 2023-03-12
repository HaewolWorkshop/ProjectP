
namespace HaewolWorkshop
{
    public class Global
    {

        private static Global Instance = null;
        public static Global I
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new Global();
                }
                return Instance;
            }
        }

        private Global()
        {
            dataManager = new GameDataManager();
            dataManager.Initialize();
        }


        public GameDataManager dataManager { get; private set; }
    }
}
