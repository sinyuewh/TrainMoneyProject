using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

using WebFrame;
using WebFrame.Data;
using WebFrame.Util;
using WebFrame.Designer;
using WebFrame.ExpControl;

namespace BusinessRule
{
    class TrainTypeProfile
    {
        private ETrainType traintype;
        public ETrainType TrainType
        {
           get { return this.traintype; }
           private set { this.traintype = value; }
        }

        /// <summary>
        /// 列车类型定义
        /// </summary>
        /// <param name="traintype"></param>
        public TrainTypeProfile(ETrainType traintype)
        {
           this.traintype = traintype;
        }


        /// <summary>
        /// 设置列车类型
        /// </summary>
        private void SetTrainType()
        {
        }
    }
}
