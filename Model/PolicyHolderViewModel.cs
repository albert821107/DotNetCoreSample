namespace OrganizationSample;

public class PolicyHolderViewModel
{
    public PolicyHolderViewModel() { }

    /// <summary>
    /// 保戶編號
    /// </summary>
    public string? Code { get; set; }

    /// <summary>s
    /// 保戶姓名
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 加入日
    /// </summary>
    public DateTime? RegistrationDate { get; set; }

    /// <summary>
    /// 介紹人保戶編號
    /// </summary>
    public string? IntroducerCode { get; set; }

    /// <summary>
    /// 左樹
    /// </summary>
    public PolicyHolder? LeftTree { get; set; }

    /// <summary>
    /// 右樹
    /// </summary>
    public PolicyHolder? RightTree { get; set; }

}
