using BangumiNet.Api.V0.ExtraEnums;

namespace BangumiNet.Api.Interfaces;

public interface ICollection
{
    /// <summary><inheritdoc cref="CollectionType.Done"/> <see cref="CollectionType.Done"/></summary>
    public int? Collect { get; set; }
    /// <summary><inheritdoc cref="CollectionType.Doing"/> <see cref="CollectionType.Doing"/></summary>
    public int? Doing { get; set; }
    /// <summary><inheritdoc cref="CollectionType.Dropped"/> <see cref="CollectionType.Dropped"/></summary>
    public int? Dropped { get; set; }
    /// <summary><inheritdoc cref="CollectionType.OnHold"/> <see cref="CollectionType.OnHold"/></summary>
    public int? OnHold { get; set; }
    /// <summary><inheritdoc cref="CollectionType.Wish"/> <see cref="CollectionType.Wish"/></summary>
    public int? Wish { get; set; }
}
