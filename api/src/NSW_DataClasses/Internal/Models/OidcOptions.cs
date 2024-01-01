namespace NSW.Data.Internal.Models
{
	public class OidcOptions
	{
		public string InternalAddressPart { get; set; }
		public string ExternalAddressPart { get; set; }
		public string Authority {get;set;}
		public string ClientId {get;set;}
		public string ClientSecret {get;set;}
		public string MetadataAddress {get;set;}
		public bool RequireHttpsMetadata {get;set;}
		// public string CallbackPath {get;set;}
		public string ResponseType {get;set;}
		public bool GetClaimsFromUserInfoEndpoint {get;set;}
		public bool SaveTokens {get;set;}
	}
}
