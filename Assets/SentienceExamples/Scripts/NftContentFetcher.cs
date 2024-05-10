using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sentience.Utils;

namespace Sentience.Demo
{
    public class NftContentFetcher : INftContentFetcher
    {
        private IContentFetcher _contentFetcher;
        
        public NftContentFetcher(IContentFetcher contentFetcher)
        {
            _contentFetcher = contentFetcher;
        }
        
        public event Action<FetchNftContentResult> OnNftFetchSuccess;
        
        public async Task FetchContent(int maxToFetch)
        {
            FetchNftContentResult result = await _contentFetcher.FetchNftContent(maxToFetch);
            OnNftFetchSuccess?.Invoke(result);
        }

        public void Refresh()
        {
            _contentFetcher.RefreshNfts();
        }
    }
}