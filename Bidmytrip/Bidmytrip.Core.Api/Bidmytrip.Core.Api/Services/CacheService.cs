using Bidmytrip.Core.Api.Dtos;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidmytrip.Core.Api.Services
{
    public static class CacheService
    {
        private const string StorageAccountConnectionString = "DefaultEndpointsProtocol=https;AccountName=bidmytripcoreapiv1;AccountKey=+513cr4mjof+unqsfyjn7oCHeM6XYiHChB7sSB2ky/5KE2ujgiF5FbnwrSMfgl/w8XoZ620jcpiTkMMraCFSiw==;EndpointSuffix=core.windows.net";
        private const string ContainerName = "bidmytripdb";
        private const string DataFileName = "Data.json";

        private static readonly CloudBlobClient _blobClient;
        private static readonly CloudBlobContainer _cloudBlobContainer;
        private static readonly CloudBlockBlob _cloudBlockBlob;
        private static readonly IList<ProposalDto> _proposals = new List<ProposalDto>();       

        static CacheService()
        {
            CloudStorageAccount storageAccount;
            CloudStorageAccount.TryParse(StorageAccountConnectionString, out storageAccount);

            _blobClient = storageAccount.CreateCloudBlobClient();
            _cloudBlobContainer = _blobClient.GetContainerReference(ContainerName);
            _cloudBlobContainer.CreateIfNotExistsAsync().Wait();
            _cloudBlockBlob = _cloudBlobContainer.GetBlockBlobReference(DataFileName);
        }

        public static async Task<IList<ProposalDto>> GetFullDb()
        {
            var dataAsJson = await _cloudBlockBlob.DownloadTextAsync();

            if(dataAsJson == "[]")
            {
                return new List<ProposalDto>();
            }
            else
            {
                var data = JsonConvert.DeserializeObject<ProposalListDto>(dataAsJson);
                return data.Proposals.ToList();
            }                        
        }

        public static async Task UpdataDb(IList<ProposalDto> fullDb)
        {
            var prosalList = new ProposalListDto() { Proposals = fullDb };

            var dataAsJson = JsonConvert.SerializeObject(prosalList);

            await _cloudBlockBlob.UploadTextAsync(dataAsJson);
        }
    }
}
