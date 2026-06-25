/// <summary>
/// 変数用のクラス
/// </summary>
public class Enums
{
    /// <summary>
    /// 部屋タイプ
    /// </summary>
    public enum RoomType
    {
        Bedroom,  //寝室
        Toilet,   //トイレ
        Bathroom, //浴室
        Washroom, //洗面所
        Entrance, //玄関
        LDK
    }

    /// <summary>
    /// 洗剤タイプ
    /// </summary>
    public enum DetergentType
    {
        Chlorine,       //塩素系薬品
        Alkalis,        //アルカリ性薬品
        OrganicSolvent, //有機溶剤薬品
        Acid            //酸性薬品
    }

    /// <summary>
    /// 汚れタイプ
    /// </summary>
    public enum DirtType
    {
        Mold,        // カビ
        UrineStone,  // 尿石
        WaterScale,  // 水垢
        WAX,         // ワックス
        Oil,         // 油汚れ
        TobaccoTar,  // タバコ・ヤニ
        Seal,        // シール
        Rust,        // 錆
        SebumStains, //皮脂汚れ
        CarpetStain, // その他の汚れ(カーペット)
        AirconStain  // その他の汚れ(エアコン)
    }
}
