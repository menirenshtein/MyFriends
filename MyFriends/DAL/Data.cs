namespace MyFriends.DAL
{
    public class Data
    {
        // str how`s holding the connection details
        string ConnectionString =
            "server = DESKTOP-CBVKIGC\\SQLEXPRESS;" +
            "initial catalog = my_friends;" +
            "user id = sa;" +
            "password = 1234;" +
            "TrustServerCertificate = yes";

        // constractor
        private Data()
        {
            // Initialize the DataLayer
            // with the provided connection string
            Layer = new DataLayer(ConnectionString);
        }
        // data type variable
        static Data GetData;

        // Public static property to provide access
        // to the DataLayer instance

        public static DataLayer Get
        {
            get
            {
                if (GetData == null)
                {
                    GetData = new Data();
                }
                return GetData.Layer;

            }
        }
        // dataLayer type property
        public DataLayer Layer { get; set; }
    }
}
