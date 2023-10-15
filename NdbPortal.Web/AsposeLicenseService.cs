using Aspose.Words;
using NdbPortal.Web.Contracts;

namespace NdbPortal.Web
{

    public class AsposeLicenseService : IAsposeLicenseService
    {
        private readonly IConfiguration _configuration;

        public AsposeLicenseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void LoadLicense()
        {
            var licenseBase64 = _configuration["AsposeLicense"];
            if (!string.IsNullOrEmpty(licenseBase64))
            {
                byte[] licenseBytes = Convert.FromBase64String(licenseBase64);
                var license = new License();
                license.SetLicense(new MemoryStream(licenseBytes));
            }
            // Handle the case where the license is not configured
            // You may throw an exception or log a warning
        }
    }
}
