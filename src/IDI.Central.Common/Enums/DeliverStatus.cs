﻿namespace IDI.Central.Common.Enums
{
    public enum DeliverStatus
    {
        /// <summary>
        /// 0：在途，即货物处于运输过程中；
        /// </summary>
        InTransit = 0,
        /// <summary>
        /// 1：揽件，货物已由快递公司揽收并且产生了第一条跟踪信息；
        /// </summary>
        TakingExpress = 1,
        /// <summary>
        /// 2：疑难，货物寄送过程出了问题；
        /// </summary>
        Difficulty = 2,
        /// <summary>
        /// 3：签收，收件人已签收；
        /// </summary>
        Signed = 3,
        /// <summary>
        /// 4：退签，即货物由于用户拒签、超区等原因退回，而且发件人已经签收；
        /// </summary>
        SignBack = 4,
        /// <summary>
        /// 5：派件，即快递正在进行同城派件；
        /// </summary>
        Delivering = 5,
        /// <summary>
        /// 6：退回，货物正处于退回发件人的途中；
        /// </summary>
        TurnBack = 6,
    }
}
