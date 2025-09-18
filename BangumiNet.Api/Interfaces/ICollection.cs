using BangumiNet.Api.ExtraEnums;

namespace BangumiNet.Api.Interfaces;

public interface ICollection
{
    /// <summary><inheritdoc cref="CollectionType.Done"/> <see cref="CollectionType.Done"/></summary>
    int? Collect { get; set; }
    /// <summary><inheritdoc cref="CollectionType.Doing"/> <see cref="CollectionType.Doing"/></summary>
    int? Doing { get; set; }
    /// <summary><inheritdoc cref="CollectionType.Dropped"/> <see cref="CollectionType.Dropped"/></summary>
    int? Dropped { get; set; }
    /// <summary><inheritdoc cref="CollectionType.OnHold"/> <see cref="CollectionType.OnHold"/></summary>
    int? OnHold { get; set; }
    /// <summary><inheritdoc cref="CollectionType.Wish"/> <see cref="CollectionType.Wish"/></summary>
    int? Wish { get; set; }
}
