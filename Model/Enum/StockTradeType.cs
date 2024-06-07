using System.ComponentModel;

namespace Sample_AP.Model.Enum;

/// <summary>
/// 股票交易方式
/// </summary>
public enum StockTradeType
{
    /// <summary>
    /// 上市
    /// </summary>
    [Description("上市")]
    TSE = 0,

    /// <summary>
    /// 上櫃
    /// </summary>
    [Description("上櫃")]
    OTC = 1,

    /// <summary>
    /// 興櫃
    /// </summary>
    [Description("興櫃")]
    ROTC = 2
}