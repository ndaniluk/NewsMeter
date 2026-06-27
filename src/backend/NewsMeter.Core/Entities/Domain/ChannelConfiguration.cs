using System.Text.Json.Serialization;

namespace NewsMeter.Core.Entities.Domain;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(EmailConfiguration), "email")]
public abstract class ChannelConfiguration {}