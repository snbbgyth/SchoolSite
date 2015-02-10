using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolSite.Core.DbModel;

namespace SchoolSite.Core.IDAL
{
    public interface IWebContentDal : IDataOperationActivity<WebContent>
    {
        IEnumerable<WebContent> QueryByWebContentTypeIds(List<int> ids);

        Task<IEnumerable<WebContent>> QueryByWebContentTypeIdsAsync(List<int> ids);
    }
}
