using SchoolSite.Core.DbModel;
using SchoolSite.Core.IDAL;

namespace SchoolSite.Core.DAL
{
    public class OtherLogInfoDal : DataOperationActivityBase<OtherLogInfo>, IOtherLogInfo
    {
        public static OtherLogInfoDal Current
        {
            get { return new OtherLogInfoDal(); }
        }
    }
}
