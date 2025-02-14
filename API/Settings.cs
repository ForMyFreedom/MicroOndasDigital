namespace API
{
    public sealed class Settings
    {
        private Settings () { }
        private static Settings _instance;
        private byte[] jwtSecretKey;

        public static Settings GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ();
            }
            return _instance;
        }
        
        public void SetJWTSecretKey(byte[] key)
        {
            if (jwtSecretKey == null)
            {
                jwtSecretKey = key;
            }
        }

        public byte[] GetJWTSecretKey()
        {
            return jwtSecretKey;
        }
    }
}
