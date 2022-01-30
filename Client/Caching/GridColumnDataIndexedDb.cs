using DnetIndexedDb;
using Microsoft.JSInterop;

namespace Client.Caching
{
    public class GridColumnDataIndexedDb : IndexedDbInterop
    {
        public GridColumnDataIndexedDb(IJSRuntime jsRuntime, IndexedDbOptions<GridColumnDataIndexedDb> options)
            : base(jsRuntime, options)
        {
        }
    }
}