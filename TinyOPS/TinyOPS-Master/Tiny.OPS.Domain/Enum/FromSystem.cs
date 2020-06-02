using System.ComponentModel.DataAnnotations;

namespace Tiny.OPS.Domain
{
    public enum FromSystem
    {
        /// <summary>
        /// Boss系统
        /// </summary>
        [Display(Name = "BOS")]
        Boss = 1000,
        /// <summary>
        /// 校管家
        /// </summary>
        [Display(Name = "XGJ")]
        SchoolKeeper = 2000,
        /// <summary>
        /// 财务系统
        /// </summary>
        [Display(Name = "EFS")]
        EFS = 3000,
        /// <summary>
        /// 爱斯
        /// </summary>
        [Display(Name = "STM")]
        STEM = 4000,
        /// <summary>
        /// 收入中心
        /// </summary>
        [Display(Name = "ECS")]
        ECS = 5000,
        /// <summary>
        /// 校管家-智培星
        /// </summary>
        [Display(Name = "ZPX")]
        Keedu = 6000,
        /// <summary>
        /// 校管家-上海少儿
        /// </summary>
        [Display(Name = "SSR")]
        OnlyChild = 7000,

        /// <summary>
        /// 开票中心
        /// </summary>
        [Display(Name = "INV")]
        INV = 8000,
    }
}
