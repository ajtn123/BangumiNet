using Microsoft.Kiota.Abstractions.Serialization;

#pragma warning disable IDE0130 // 命名空间与文件夹结构不匹配
namespace BangumiNet.Api.P1.Models;
#pragma warning restore IDE0130 // 命名空间与文件夹结构不匹配

public partial class CalendarItem : IAdditionalDataHolder, IParsable
{
    /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
    public IDictionary<string, object> AdditionalData { get; set; }
    public SlimSubject? Subject { get; set; }
    public int? Watchers { get; set; }
    public CalendarItem()
    {
        AdditionalData = new Dictionary<string, object>();
    }
    /// <summary>
    /// Creates a new instance of the appropriate class based on discriminator value
    /// </summary>
    /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
    public static CalendarItem CreateFromDiscriminatorValue(IParseNode parseNode)
    {
        ArgumentNullException.ThrowIfNull(parseNode);
        return new CalendarItem();
    }
    /// <summary>
    /// The deserialization information for the current model
    /// </summary>
    public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
    {
        return new Dictionary<string, Action<IParseNode>>
            {
                { "subject", n => { Subject = n.GetObjectValue(SlimSubject.CreateFromDiscriminatorValue); } },
                { "watchers", n => { Watchers = n.GetIntValue(); } },
            };
    }
    /// <summary>
    /// Serializes information the current object
    /// </summary>
    /// <param name="writer">Serialization writer to use to serialize this model</param>
    public virtual void Serialize(ISerializationWriter writer)
    {
        ArgumentNullException.ThrowIfNull(writer);
        writer.WriteObjectValue("subject", Subject);
        writer.WriteIntValue("watchers", Watchers);
        writer.WriteAdditionalData(AdditionalData);
    }
}
