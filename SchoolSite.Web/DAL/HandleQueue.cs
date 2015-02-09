﻿using SchoolSite.Core.DAL;
using SchoolSite.Core.DbModel;
using SchoolSite.Web.DAL.Manage;

namespace SchoolSite.Web.DAL
{

    public class HandleQueue : BaseQueueDal<dynamic>
    {
        private static readonly object SyncObj = new object();

        private static HandleQueue _instance;

        public static HandleQueue Instance
        {
            get
            {
                lock (SyncObj)
                {
                    if (_instance == null)
                        _instance = new HandleQueue();
                }
                return _instance;
            }
        }

        private HandleQueue()
        {

        }

        public override void OnNotify(dynamic entity)
        {
            //if (entity is Picture)
            //    HandlePicture(entity as Picture);
        }

        //public void HandlePicture(Picture picture)
        //{
        //    ImageManage.GenerateThumbnail(picture);
        //}
    }
}