namespace eCommerce.Extensions.Data.MongoRepository
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System.Runtime.Serialization;

    /// <summary>
    /// Abstract Entity for all the BusinessEntities.
    /// </summary>
    [DataContract]
    [BsonIgnoreExtraElements(Inherited = true)]
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// Gets or sets the id for this object (the primary record for an entity).
        /// </summary>
        /// <value>The id for this object (the primary record for an entity).</value>
        [DataMember]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _Id { get; set; }
    }
}
