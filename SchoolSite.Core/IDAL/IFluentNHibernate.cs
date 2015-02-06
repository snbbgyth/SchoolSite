using NHibernate;

namespace SchoolSite.Core.IDAL
{
    public interface IFluentNHibernate
    {
        ISession GetSession();
    }
}
