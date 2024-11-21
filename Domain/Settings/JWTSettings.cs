namespace Domain.Settings
{
    public class JWTSettings
    {
        public string JWT_Secret { get; set; }
        public string JWT_ISSUER_TOKEN { get; set; }
        public string JWT_AUDIENCE_TOKEN { get; set; }
        public double JWT_DURATIONMINUTES_TOKEN { get; set; }
    }
}



